using Newtonsoft.Json;

namespace DevilCatClient.Models
{
    public class OdataState<T>
    {
        [JsonProperty("@odata.context")]
        public string? Context { get; set; }
        public IEnumerable<T>? Value { get; set; }
    }
}
