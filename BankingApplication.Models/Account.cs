using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Account : BaseBank
    {
        public string BankId { get; set; }
        public Customer CustomerOfAccount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountId { get; set; }
        public AccountType TypeOfAccount { get; set; }
        public decimal Balance { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
