using System;

namespace MeditSolution.Models.DataObjects
{
    
    public class ExplicitSubscription
    {
        public string ProductID { get; set; }

        public DateTime ActivatedDate { get; set; } 

        public DateTime ExpirationDate { get; set; } 
    }

}
