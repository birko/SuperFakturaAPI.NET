﻿using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response
{
    public class Response<T>
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public int? Error { get; internal set; } = null;
        [JsonProperty(PropertyName = "error_message", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic ErrorMessage { get; internal set; } = null;
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public String Message { get; internal set; } = null;
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; internal set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n");
            builder.AppendFormat(" Error: {0}, \n ErrorMessage: {1}, \n Message: {2}, \n", Error, ErrorMessage, Message);
            builder.AppendFormat(" Data: {0}\n ", (Data != null) ? Data.ToString() : "NULL");
            builder.Append("}");

            return builder.ToString();
        }
    }
}
