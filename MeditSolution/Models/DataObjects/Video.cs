using MeditSolution.DataStore.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditSolution.Models.DataObjects
{
    public class Video : BaseDataObject
    {        
        [JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("name_en")]
		public string Name_EN { get; set; }
        
		[JsonProperty("path")]
        public string Path { get; set; }

		[JsonProperty("path_en")]
		public string Path_EN { get; set; }
        
		[JsonProperty("lastUpdatedAt")]
        public DateTime LastUpdatedAt { get; set; }
        
		[JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        
		[JsonProperty("length")]
        public string Length { get; set; }
    }
}
