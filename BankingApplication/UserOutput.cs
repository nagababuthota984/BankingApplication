using BankingApplication.Models;
using System;
using System.Collections.Generic;
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
                string heading = "Sno  | Transaction Id\t\t\t\t|  Type  | Amount | Balance | Transaction On";
                Console.WriteLine(heading);
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                foreach (Transaction trans in transactions)
                {
                    string output = $"{count,5}|{trans.TransId,19}   |{trans.Type,7}|{trans.TransactionAmount,7}|{trans.BalanceAmount,10}|{trans.On}";
                    Console.WriteLine(output);
                    count++;
                    Console.WriteLine();
                } 
            }
            else
            {
                Console.WriteLine("\nNo transactions to show!\n");
            }
        }

        internal static void ShowMessage(string output)
        {
            Console.WriteLine(output);
        }
    }
}
