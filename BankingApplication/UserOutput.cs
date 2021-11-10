using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.CLI
{
    public class UserOutput
    {
        
        
        public static void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        public static void ShowTransactions(List<Transaction> transactions)
        {
            int count = 1;

            if (transactions.Count>=1)
            {
                Console.WriteLine(Constant.showTransactionsHeader);
                foreach (Transaction trans in transactions.OrderBy(tr => tr.On))
                {
                    string output = $"{count,5}|{trans.TransId,19}   |{trans.Type,14}|{trans.TransactionAmount,7}|{trans.BalanceAmount,10}|{trans.On}";
                    Console.WriteLine(output);
                    count++;
                    Console.WriteLine();
                } 
            }
            else
            {
                Console.WriteLine(Constant.noTransactions);
            }
        }

        internal static void ShowMessage(string output)
        {
            Console.WriteLine(output);
        }
    }
}
