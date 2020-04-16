namespace CatBasicExample.Domain.Exception
{
    public class UnAuthorized : BaseException
    {
        public UnAuthorized(string message = "Login is require!") : base(message, "401", "unauthorized")
        {
        }
    }
}