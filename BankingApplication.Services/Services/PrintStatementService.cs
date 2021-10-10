using BankingApplication.Database;
using BankingApplication.Models;
using System;

namespace BankingApplication.Services
{
    public class PrintStatementService
    {
    
        public string[] FetchTransactionHistory(double AccNumber)
        {
            //prints user's acount history
            DataLoaderService.LoadData();
            if (DataStructures.Transactions.ContainsKey(AccNumber))    //If there is atleast one transaction.
            {
                string Trans = DataStructures.Transactions[AccNumber];
                string[] TransList = Trans.Split(",");
                return TransList;
            }
            else
            {
                return(new string[] { "None transactions recorded so far!" });
            }
            

        }
    }
}
