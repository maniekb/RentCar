using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Common.Authentication.Results
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }

        public AuthenticationResult(bool isAuthenticated, bool isAdmin)
        {
            IsAuthenticated = isAuthenticated;
            IsAdmin = isAdmin;
        }

        public static AuthenticationResult Unauthenticated => new AuthenticationResult(false, false);
        public static AuthenticationResult Authenticated => new AuthenticationResult(true, false);
        public static AuthenticationResult AuthenticatedAdmin => new AuthenticationResult(true, true);
    }
}
