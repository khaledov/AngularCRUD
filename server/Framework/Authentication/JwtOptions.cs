﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Authentication
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
    }
}
