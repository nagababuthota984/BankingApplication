using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Account : BaseBank
    {
        public Customer Customer { get; set; }
        public string AccountNumber { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Transaction> Transactions = new List<Transaction>();

    }
}
