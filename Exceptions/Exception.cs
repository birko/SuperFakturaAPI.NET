using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Exceptions
{
    public class Exception : System.Exception
    {
        public int? Error { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }
        public string URL { get; set; }

        public Exception(int? code, string message) : this(code, message, string.Empty, null,  null)
        { }
        public Exception(int? code, string message, System.Exception inner) : this(code, message, string.Empty, inner, null)
        { }
        public Exception(int? code, string message, System.Exception inner, string url) : this(code, message, string.Empty, inner, url)
        { }
        public Exception(int? code, string message, string errorMessage) : this(code, message, errorMessage, null, null)
        { }
        public Exception(int? code, string message, string errorMessage, string url) : this(code, message, errorMessage, null, url)
        { }
        public Exception(int? code, string message, string errorMessage, System.Exception inner) : this(code, message, errorMessage, inner, null)
        { }
        public Exception(int? code, string message, string errorMessage, System.Exception inner, string url)
            : base(string.Format("\nCode: {0},\nMessage: {1}\nErrorMessage: {2}\nUrl: {3}", code, message, errorMessage, url), inner)
        {
            Error = code;
            ErrorMessage = errorMessage;
            ResponseMessage = message;
            URL = url;
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

    public class ParseException : Exception
    {
        public ParseException(string message) : this(message, string.Empty, null)
        { }
        public ParseException(string message, System.Exception inner) : this(message, string.Empty, inner)
        { }
        public ParseException(string message, string errorMessage) : this(message, errorMessage, null)
        { }
        public ParseException(string message, string errorMessage, System.Exception inner)
            : base(null, message, errorMessage, inner)
        {
        }
    }
}
