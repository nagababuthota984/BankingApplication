using BankingApplication.Database;
using System.Collections.Generic;

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
            
            accounts = DataReaderWriter.readAccounts();
            transactions = DataReaderWriter.readTransactions();


        }



    }
}
