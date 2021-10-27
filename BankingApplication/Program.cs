using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {

        public static void Main()
        {
            
            RBIStorage.banks = FileHelper.GetData();
            Console.Clear();    
            Console.WriteLine("\n\n==============================Welcome to Technovert Banking Solutions=============================.\n********************\n1.Account Holder Login\n2.Bank Staff Login.\n\nPlease enter one option\n********************");
            MainMenu choice = GetMainMenuByInput(Convert.ToInt32(Console.ReadLine()));
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
                return MainMenu.BankStaff;
            else
                return MainMenu.CloseApplication;
        }
    }
}
