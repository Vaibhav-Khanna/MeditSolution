using System.Collections.Generic;
using MeditSolution.DataStore.Abstraction;

namespace MeditSolution.Responses
{
	public class PaginationResponse<T> where T : BaseDataObject
	{		
        public int limit { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public int total { get; set; }
        public List<T> rows { get; set; }

	}
}
