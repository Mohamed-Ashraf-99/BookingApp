﻿namespace Booking.Application.Authentication.Helpers
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateIssuerSignInKey { get; set; }
    }
}
