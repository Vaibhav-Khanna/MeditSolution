using System;
using Newtonsoft.Json;

namespace MeditSolution.Models.DataObjects
{
    public class PaidProgram
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("paidAt")]
        public int? paidAt { get; set; }

        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

    }
}
