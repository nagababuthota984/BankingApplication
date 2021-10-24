using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {

        public static void Main(string[] args) //welcome page shows AccountHolder Login, Bank Staff Login.
        {
            RBIStorage.banks = FileHelper.GetData();
            Console.WriteLine("\t\tWelcome to Technovert Banking Solutions.\n1.Account Holder Login\n2.Bank Staff Login.\n");
            MainMenu choice = (MainMenu)int.Parse(Console.ReadLine());
            while (true)
            {
                try
                {
                    switch (choice)
                    {
                        case MainMenu.AccountHolder:
                            new AccountHolderPage().UserInterface();
                            break;
                        case MainMenu.BankStaff:
                            new BankStaffPage().UserInterface();
                            break;
                        default:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
