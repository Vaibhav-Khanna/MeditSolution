using MeditSolution.DataStore.Abstraction;
using Newtonsoft.Json;

namespace MeditSolution.Models.DataObjects
{
	public class Program : BaseDataObject
	{
		[JsonProperty("label")]
		public string Label { get; set; }

		[JsonProperty("label_en")]
		public string Label_EN { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }

		[JsonProperty("icon")]
		public string Icon { get; set; }

		[JsonProperty("cover")]
		public string Cover { get; set; }

		[JsonProperty("total_meditations")]
		public int TotalMeditations { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("description_en")]
		public string Description_EN { get; set; }

		[JsonProperty("price")]
		public int? Price { get; set; }

		[JsonProperty("programorder")]
		public int? programOrder { get; set; } = 0;

		[JsonProperty("availableWithSubscription")]
		public bool? AvailableWithSubscription { get; set; }

		[JsonProperty("isInitiation")]
		public bool? IsInitiation { get; set; }

		[JsonProperty("isTraining")]
		public bool? IsTraining { get; set; }
	}
}
