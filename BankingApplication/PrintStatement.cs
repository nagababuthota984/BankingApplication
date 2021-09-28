using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    class PrintStatement
    {
        double accNumber = 0;
        public PrintStatement()
        {
            
        }
        public void printTransactionHistory()
        {
            //prints user's acount history
            Console.WriteLine("Enter your account number: ");
            double accNumber = Convert.ToDouble(Console.ReadLine());
            if (Account.accounts.ContainsKey(accNumber))
            {
                if (Account.transactions.ContainsKey(accNumber))
                {
                    string trans = Account.transactions[accNumber];
                    string[] transList = trans.Split(",");
                    int count = 1;
                    foreach (string transaction in transList)
                    {
                        Console.WriteLine("{0}\t{1}", count, transaction);
                        count++;
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("None transactions recorded so far!");
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return;
            }

        }
    }
}
