﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BankingApplication
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
                Account acc = new Account();
                switch (choice)
                {
                   
                    case 1:
                            Console.WriteLine("\t-------Account Creation-------\n");
                            AccountCreation create = new AccountCreation();
                            create.createAccount();
                            break;
                    case 2:
                            Console.WriteLine("\t-------Money Deposit-------\n");
                            DepositAmount trans = new DepositAmount();
                            trans.depositAmount();
                            break;
                    case 3: Console.WriteLine("\n-------Amount Withdrawl-------\n");
                            Withdraw wd = new Withdraw();
                            wd.withdrawAmount();
                            break;
                    case 4: Console.WriteLine("-------Amount Transfer-------\n");
                            Transfer tr = new Transfer();
                            tr.transferAmount();
                            break;
                    case 5: Console.WriteLine("-------Transaction History-------\n");
                            PrintStatement ps = new PrintStatement();
                            ps.printTransactionHistory();
                            break;
                            
                    default:
                        Environment.Exit(0);
                        break;
                }


            }
        }
    }
}
