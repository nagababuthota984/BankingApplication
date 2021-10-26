using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.CLI
{
    public class UserOutput
    {
        
        
        public static void ErrorMessage(string Message)
        {
            Console.WriteLine(Message);
        }
        
        public static void ShowTransactions(List<Transaction> Transactions)
        {
            int Count = 1;

            foreach( Transaction trans in Transactions)
            {
                Console.WriteLine("{0}\t"+"TID - "+ "{1}\n\t\t" + "Type - " + "{2}\n\t\t" + "Amount - " + "{3}\n\t\t" + "Balance - " + "{4}\n\t\t" + "DoneOn - " + "{5}", Count,trans.TransId,trans.Type,trans.TransactionAmount,trans.BalanceAmount,trans.On);
                Count++;
                Console.WriteLine();
            }
        }

        internal static void ShowMessage(string output)
        {
            Console.WriteLine(output);
        }
    }
}
