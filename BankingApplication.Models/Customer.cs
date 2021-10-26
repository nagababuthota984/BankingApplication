using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Customer
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string AadharNumber { get; set; }
        public string PanNumber { get; set; }
    }
}
