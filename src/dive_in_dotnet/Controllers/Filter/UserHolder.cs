using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace CatBasicExample.Controllers.Filter
{

    public class AuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "jwt";
        public string Scheme => DefaultScheme;

    }

}