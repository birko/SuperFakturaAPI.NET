using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request
{
    public abstract class Data
    {
        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "checksum", NullValueHandling = NullValueHandling.Ignore)]
        public string CheckSum { get; set; }
    }

    public  class DataData : Data
    {
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
    }
}
