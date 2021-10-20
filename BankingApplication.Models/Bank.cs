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
        public int Default_RTGS { get; set; }
        public int Default_IMPS { get; set; }
        public int Other_RTGS { get; set; }
        public int Other_IMPS { get; set; }
        public Currency CurrencyType { get; set; }

    }
}
