using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Staff
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string BankId { get; set; }
        public int Age { get; set; }
        public string StaffId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StaffDesignation Designation { get; set; }
    }
}
