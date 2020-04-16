using System;
using Microsoft.AspNetCore.Authentication;

namespace CatBasicExample.Controllers.Filter
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<AuthOptions> configureOptions)
        {
            return builder.AddScheme<AuthOptions, AuthHandler>(AuthOptions.DefaultScheme, configureOptions);
        }
    }
}