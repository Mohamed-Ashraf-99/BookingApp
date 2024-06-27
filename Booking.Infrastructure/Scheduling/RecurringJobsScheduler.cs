using Booking.Domain.Repositories;
using Booking.Infrastructure.Repositories;
using Hangfire;


namespace Booking.Infrastructure.Scheduling;

public class JobsScheduler
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IReservationRepository _reservationRepository;

    public JobsScheduler(IRecurringJobManager recurringJobManager, IReservationRepository reservationService)
    {
        _recurringJobManager = recurringJobManager;
        _reservationRepository = reservationService;
    }

    public void ScheduleRecurringJobs()
    {
        _recurringJobManager.AddOrUpdate(
        "DeleteExpiredReservations",
                () => _reservationRepository.DeleteExpiredReservationsAsync(),
                "*/5 * * * *"); // Every 5 minutes
    }
}
