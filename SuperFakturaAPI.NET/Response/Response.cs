using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Birko.SuperFaktura.Response
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringIntConverter))]
        public int? Error { get; internal set; } = null;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Error: {Error}");

            return builder.ToString();
        }
    }

    public class ErrorMessageResponse: ErrorResponse
    {
        [JsonProperty(PropertyName = "error_message", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; internal set; } = null;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"ErrorMessage: {ErrorMessage}");

            return builder.ToString();
        }
    }

    public class TagResponse : ErrorMessageResponse
    {
        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> Tag { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"Tag: {Tag?.Count()}");

            return builder.ToString();
        }
    }

    public class StringMessageResponse : ErrorMessageResponse
    {
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; internal set; } = null;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"Message: {Message}");

            return builder.ToString();
        }
    }

    public class RedirectResponse : StringMessageResponse
    {
        [JsonProperty(PropertyName = "redirect_url", NullValueHandling = NullValueHandling.Ignore)]
        public string RedirectURL { get; internal set; } = null;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"RedirectURL: {RedirectURL}");

            return builder.ToString();
        }
    }

    public class Response<T> : StringMessageResponse
    {
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public virtual T Data { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"Data: {((Data != null) ? Data.ToString() : "NULL")}");

            return builder.ToString();
        }
    }

    public class ListResponse<T> : Response<IEnumerable<T>>
    {
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ListConverter))]
        public override IEnumerable<T> Data { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Data: { \n");
            if (Data != null)
            {
                foreach (var item in Data)
                {
                    builder.AppendFormat("\t {0} \n", item.ToString());
                }
            }
            else
            {
                builder.AppendFormat("\t {0} \n", "NULL");
            }
            builder.Append("},");
            return builder.ToString();
        }
    }

    public class StatusResponse<T> : Response<T>
    {
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; internal set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"Status: {Status}");

            return builder.ToString();
        }
    }

    public class StateResponse<T> : Response<T>
    {
        [JsonProperty(PropertyName = "state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; internal set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"State: {State}");

            return builder.ToString();
        }
    }
}
