using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Account : BaseBank
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public System.DateTime Dob { get; set; }
        public string Address { get; set; }
        public string AadharNumber { get; set; }
        public string PanNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountId { get; set; }
        public AccountType TypeOfAccount { get; set; }
        public decimal Balance { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
