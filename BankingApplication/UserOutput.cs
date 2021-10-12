using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.CLI
{
    public class UserOutput
    {
        public static void AccountCreationSuccess(string Name,string AccNumber)
        {
            Console.WriteLine("Hey {0}!, Your account was created successfully. Note your Account Number[{1}] to access our services.",Name,AccNumber);
        }
        public static void GreetUser(string Name)
        {
            Console.WriteLine("Hello! {0}", Name);
        }
        public static void ErrorMessage(string Message)
        {
            Console.WriteLine(Message);
        }
        public static void Success (string Action)
        {
            Console.WriteLine("Amount has been {0} successfully!",Action);
        }
        public static void Success(string ReceiverName, int Amount)
        {
            Console.WriteLine("{0} has been successfully transferred to {1}",Amount,ReceiverName);
        }
        public static void ShowBalance(int Balance)
        {
            Console.WriteLine("Current Balance: {0} ",Balance);
        }
        public static void Statement(int serialNumber, string Transaction)
        {
            Console.WriteLine("{0}\t{1}",serialNumber,Transaction);
        }
        public static void ShowTransactions(string[] Transactions)
        {
            int Count = 1;
            foreach( string Trans in Transactions)
            {
                Console.WriteLine("{0}\t{1}\n",Count,Trans);
                Count++;
            }
        }
    }
}
