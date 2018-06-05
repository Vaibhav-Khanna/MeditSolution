using Newtonsoft.Json;

namespace MeditSolution.Models.DataObjects
{
    public class MeditationFile
    {
        [JsonProperty("originalFileName")]
        public string OriginalFileName { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("length")]
        public decimal? Length { get; set; }
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }
    }
}
