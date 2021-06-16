﻿namespace Shared.Hosting.Options
{
    public class JwtSettings
    {
        public string Authority { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}