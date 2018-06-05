using System.Collections.Generic;
using Newtonsoft.Json;


namespace MeditSolution.Models.DataObjects
{
    public class SubscriptionPremium
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}