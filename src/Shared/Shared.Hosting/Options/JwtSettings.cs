﻿namespace Shared.Hosting.Options
{
    public class JwtSettings
    {
        public string Authority { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}