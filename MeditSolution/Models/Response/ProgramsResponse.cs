using System;

namespace MeditSolution
{
	public class ProgramsResponse
	{
		public string _id;
		public string label;
		public string color;
		public DateTime lastUpdatedAt;
		public DateTime createdAt;
		public string included;
		public decimal? price;
		public bool?	 availableWithSubscription;
		public string description;
	}
}
