using BankingApplication.Database;
using System;
using System.Collections.Generic;
using System.Text;


namespace BankingApplication.Models
{
    public class DataLoader
    {
        public static void LoadData()
        {
            
            Account.accounts = DataReaderWriter.readAccounts();
            Account.transactions = DataReaderWriter.readTransactions();
        }
    }
}
