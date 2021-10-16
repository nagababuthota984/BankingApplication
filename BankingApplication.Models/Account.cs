using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Account
    {

        // public double accountNumber;
        public string BankBranch;
        public string BankIfsc;
        public string BankName;
        public string Name;
        public int Age;
        public string Gender;
        public double ContactNumber;
        public System.DateTime Dob;
        public string Address;
        public double AadharNumber;
        public string PanNumber;
        public string AccountNumber;
        public string AccountId;
        public string AccountType;
        public decimal Balance;
        public List<Transaction> Transactions { get; set; }




    }
}
