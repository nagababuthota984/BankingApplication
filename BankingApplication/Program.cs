using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {
        private AccountHolderPage accountHolderPage;
        private BankEmployeePage employeePage;
        public static void Main()
        {
            RBIStorage.banks = JsonFileHelper.GetData<Bank>();
            Program p = new Program();
            p.InitializeUI();
        }

        private void InitializeUI()
        {
            accountHolderPage = new AccountHolderPage();
            employeePage = new BankEmployeePage();
            WelcomeMenu();
        }

        public void WelcomeMenu()
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
                            employeePage.EmployeeInterface();
                            break;
                        case MainMenu.None:
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
                return MainMenu.None;
        }


    }


}
