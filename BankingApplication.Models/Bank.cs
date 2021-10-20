using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Bank
    {

        public List<Account> BankAccounts { get; set; }
        public string BankId { get ;  set ;  }
        public string Name { get;  set ;  }
        public string Branch { get; set; }
        public string Ifsc { get; set; }
        public decimal SelfRTGS { get; set; }
        public decimal SelfIMPS { get; set; }
        public decimal OtherRTGS { get; set; }
        public decimal OtherIMPS { get; set; }
        public Currency CurrencyType { get; set; }

    }
}
