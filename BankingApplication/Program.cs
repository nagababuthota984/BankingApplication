using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {
        private static AccountHolderPage accountHolderPage = new AccountHolderPage();
        private static BankEmployeePage EmployeePage = new BankEmployeePage();

        public static void Main()
        {
            
            RBIStorage.banks = FileHelper.GetData();
            WelcomeMenu();
        }

        public static void WelcomeMenu()
        {
            Console.WriteLine(Constant.welcomeMessage);
            while (true)
            {
                try
                {
                    switch (GetMainMenuByInput(Convert.ToInt32(Console.ReadLine())))
                    {
                        case MainMenu.AccountHolder:
                            accountHolderPage.CustomerInterface();
                            break;
                        case MainMenu.BankEmployee:
                            EmployeePage.EmployeeInterface();
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
