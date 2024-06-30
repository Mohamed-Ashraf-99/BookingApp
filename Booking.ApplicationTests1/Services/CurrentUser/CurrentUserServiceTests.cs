using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Booking.Application.Services.CurrentUser;
using Booking.Domain.Entities;
using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;

namespace Booking.Application.Services.CurrentUser.Tests
{
    public class CurrentUserServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly CurrentUserService _currentUserService;

        public CurrentUserServiceTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _currentUserService = new CurrentUserService(_httpContextAccessorMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task GetUserId_ValidUserIdClaim_ReturnsUserId()
        {
            // Arrange
            var userId = "1";
            var claims = new List<Claim> { new Claim(nameof(UserClaimModel.Id), userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Act
            var result = await _currentUserService.GetUserId();

            // Assert
            Xunit.Assert.Equal(int.Parse(userId), result);
        }

        [Fact]
        public async Task GetUserId_NoUserIdClaim_ThrowsNotFoundException()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _currentUserService.GetUserId());
        }

        [Fact]
        public async Task GetUserId_InvalidUserIdClaim_ThrowsException()
        {
            // Arrange
            var userId = "invalid";
            var claims = new List<Claim> { new Claim(nameof(UserClaimModel.Id), userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<Exception>(() => _currentUserService.GetUserId());
        }

        [Fact]
        public async Task GetUserAsync_ValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId };
            _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(user);

            var userIdClaim = new Claim(nameof(UserClaimModel.Id), userId.ToString());
            var identity = new ClaimsIdentity(new[] { userIdClaim });
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Act
            var result = await _currentUserService.GetUserAsync();

            // Assert
            Xunit.Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserAsync_UserNotFound_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var userId = 1;
            _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync((User)null);

            var userIdClaim = new Claim(nameof(UserClaimModel.Id), userId.ToString());
            var identity = new ClaimsIdentity(new[] { userIdClaim });
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<UnauthorizedAccessException>(() => _currentUserService.GetUserAsync());
        }
    }
}
