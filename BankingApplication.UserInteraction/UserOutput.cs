using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.UserInteraction
{
    public class UserOutput
    {
        public static void AccountCreationSuccess(string name,string accNumber)
        {
            Console.WriteLine("Hey {0}!, Your account was created successfully. Note your Account Number[{1}] to access our services.",name,accNumber);
        }
        public static void GreetUser(string name)
        {
            Console.WriteLine("Hello! {0}", name);
        }
        public static void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void Success (string action)
        {
            Console.WriteLine("Amount has been {0} successfully!",action);
        }
        public static void Success(string receiverName, int amount)
        {
            Console.WriteLine("{0} has been successfully transferred to {1}",amount,receiverName);
        }
        public static void ShowBalance(int balance)
        {
            Console.WriteLine("Current Balance: {0} ",balance);
        }
        public static void Statement(int serialNumber, string transaction)
        {
            Console.WriteLine("{0}\t{1}",serialNumber,transaction);
        }
    }
}
