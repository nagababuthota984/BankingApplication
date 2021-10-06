using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.UserInteraction
{
    public class UserInput
    {
        public static string AskUser(string property)
        {
            Console.WriteLine("Enter your {0}:", property);
            return Console.ReadLine();
        }
    }
}
