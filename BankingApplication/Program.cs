using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {
        private AccountHolderPage accountHolderPage;
        public static void Main()
        {
            
            RBIStorage.banks = FileHelper.GetData();
            WelcomeMenu();
        }

        public static void WelcomeMenu()
        {
            AccountHolderPage accPage = new AccountHolderPage();
            BankEmployeePage EmployeePage = new BankEmployeePage();
            Console.WriteLine("\n\n==============================Welcome to Technovert Banking Solutions=============================.\n********************\n1.Account Holder Login\n2.Bank Employee Login.\n\nPlease enter one option\n********************");
            MainMenu choice = GetMainMenuByInput(Convert.ToInt32(Console.ReadLine()));
            while (true)
            {
                try
                {
                    switch (choice)
                    {
                        case MainMenu.AccountHolder:
                            accPage.UserInterface();
                            break;
                        case MainMenu.BankEmployee:
                            EmployeePage.UserInterface();
                            break;
                        case MainMenu.CloseApplication:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static MainMenu GetMainMenuByInput(int value)
        {
            if (value == 1)
                return MainMenu.AccountHolder;
            else if (value == 2)
                return MainMenu.BankEmployee;
            else
                return MainMenu.CloseApplication;
        }
    }
}
