using System;
using MeditSolution.DataStore.Abstraction;
using Newtonsoft.Json;

namespace MeditSolution.Models.DataObjects
{
	public class Meditation : BaseDataObject
    {
		
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("label_en")]
        public string Label_EN { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("length")]
        public int? Length { get; set; }
        [JsonProperty("meditationorder")]
        public int? meditationOrder { get; set; } = 0;
        [JsonProperty("level_1_fr_man")] 
		public MeditationFile Level1FrMan { get; set; }
        [JsonProperty("level_1_fr_woman")]
        public MeditationFile Level1FrWoman { get; set; }
        [JsonProperty("level_1_en_man")]
        public MeditationFile Level1EnMan { get; set; }
        [JsonProperty("level_1_en_woman")]
        public MeditationFile Level1EnWoman { get; set; }
        [JsonProperty("level_2_fr_man")]
        public MeditationFile Level2FrMan { get; set; }
        [JsonProperty("level_2_fr_woman")]
        public MeditationFile Level2FrWoman { get; set; }
        [JsonProperty("level_2_en_man")]
        public MeditationFile Level2EnMan { get; set; }
        [JsonProperty("level_2_en_woman")]
        public MeditationFile Level2EnWoman { get; set; }
        [JsonProperty("level_3_fr_man")]
        public MeditationFile Level3FrMan { get; set; }
        [JsonProperty("level_3_fr_woman")]
        public MeditationFile Level3FrWoman { get; set; }
        [JsonProperty("level_3_en_man")]
        public MeditationFile Level3EnMan { get; set; }
        [JsonProperty("level_3_en_woman")]
        public MeditationFile Level3EnWoman { get; set; }
        [JsonProperty("programId")]
        public string ProgramId { get; set; }
    }
}
