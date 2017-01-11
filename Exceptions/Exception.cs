using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Exceptions
{
    public class Exception : System.Exception
    {
        public int Error { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }

        public Exception(int code, string message) : this(code, message, string.Empty, null)
        { }
        public Exception(int code, string message, Exception inner) : this(code, message, string.Empty, inner)
        { }
        public Exception(int code, string message, string errorMessage) : this(code, message, errorMessage, null)
        { }
        public Exception(int code, string message, string errorMessage, Exception inner)
            : base(string.Format("\nCode: {0},\nMessage: {1}\nErrorMessage: {2}", code, message, errorMessage), inner)
        {
            Error = code;
            ErrorMessage = errorMessage;
            ResponseMessage = message;
        }

        public Response.Response<object> CreateResponse()
        {
            return new Response.Response<object>()
            {
                Error = Error,
                Message = ResponseMessage,
                ErrorMessage = ErrorMessage,
                Data = (object)null
            };
        }

    }
}
