using System;
using System.Collections.Generic;
using BankingApplication.Services;
namespace BankingApplication.CLI
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while(true)
            {

                Console.WriteLine("Choose any one option:\n1.Create Account\n2.Deposit\n3.Withdraw\n4.Transfer Amount\n5.Print Transaction history\n");
                int choice = Convert.ToInt32(Console.ReadLine());
                TransactionManager manager = new TransactionManager();
                switch (choice)
                {
                   
                    case 1:
                            Console.WriteLine("\t-------Account Creation-------\n");
                            manager.CreateAccount();
                            break;
                    case 2:
                            Console.WriteLine("\t-------Money Deposit-------\n");
                            manager.Deposit();
                            break;
                    case 3: Console.WriteLine("\n-------Amount Withdrawl-------\n");
                            manager.Withdraw();
                            break;
                    case 4: Console.WriteLine("-------Amount Transfer-------\n");
                            manager.TransferToAccount();
                            break;
                    case 5: Console.WriteLine("-------Transaction History-------\n");
                            manager.Statement();
                            break;
                            
                    default:
                        Environment.Exit(0);
                        break;
                }


            }
        }
    }
}
