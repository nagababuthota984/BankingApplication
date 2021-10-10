using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;
namespace BankingApplication.CLI
{
    public class UserInput
    {
        public static string AskUser(string property)
        {
            Console.WriteLine("Enter your {0}:", property);
            return Console.ReadLine();
        }
        public static WelcomeMenu ShowMenu()
        {
            Console.WriteLine("\nChoose any one option:\n1.Create Account\n2.Deposit\n3.Withdraw\n4.Transfer Amount\n5.Print Transaction history\n");
            return (WelcomeMenu)int.Parse(Console.ReadLine());
        }
    }
}
