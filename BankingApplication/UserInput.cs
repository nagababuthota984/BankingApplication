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
            Console.WriteLine("Enter {0}:", property);
            return Console.ReadLine();
        }
        public static AccountHolderMenu ShowMenu()
        {
            Console.WriteLine("\nChoose any one option:\n1.Deposit\n2.Withdraw\n3.Transfer Amount\n4.Print Transaction history\n");
            return (AccountHolderMenu)int.Parse(Console.ReadLine());
        }
    }
}
