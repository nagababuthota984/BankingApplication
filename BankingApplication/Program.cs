using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {

        static void Main(string[] args) //welcome page shows AccountHolder Login, Bank Staff Login.
        {
            MainMenu choice;
            Console.WriteLine("\t\tWelcome to Technovert Banking Solutions.\n1.Account Holder Login\n2.Bank Staff Login.\n");
            choice = (MainMenu)int.Parse(Console.ReadLine());
            switch(choice)
            {
                case MainMenu.AccountHolder:
                    AccountHolderPage.ShowMenu();
                    break;
                case MainMenu.BankStaff:
                    BankStaffPage.ShowMenu();
                    break;
            }
        }
    }
}
