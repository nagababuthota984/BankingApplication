using System.Collections.Generic;
using BankingApplication.Database;

namespace BankingApplication.Models
{
    public class Account : Person
    {

        // public double accountNumber;
        public double accountNumber;
        public string accountType;
        public double balance;
        public const int MIN_BALANCE = 250;
        public static Dictionary<double, Dictionary<string, string>> accounts;
        public static Dictionary<double, string> transactions;
        public Account()
        {
            double accountNumber = 0;
            string accountType = "";
            double balance = 0;
            accounts = DataReaderWriter.readAccounts();
            transactions = DataReaderWriter.readTransactions();


        }
    
        
        
    }
}
