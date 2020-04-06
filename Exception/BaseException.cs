using System.Collections;
using System.Text.Json.Serialization;

namespace CatBasicExample.Exception
{
    public class BaseException : System.Exception
    {
        private readonly string errorCode;
        private readonly string statusCode;
        private readonly string message;

        public BaseException(string message = "", string statusCode = "500", string errorCode = "internal_error")
        {
            this.message = message;
            this.statusCode = statusCode;
            this.errorCode = errorCode;
        }

        public string StatusCode => statusCode;

        public override string Message => message;

        public string ErrorCode => errorCode;

        [JsonIgnore]
        public override string StackTrace { get; }

        [JsonIgnore]
        public override IDictionary Data { get; }

    }
}