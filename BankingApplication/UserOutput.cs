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
        public static void Success(string ReceiverName, decimal Amount)
        {
            Console.WriteLine("{0} has been successfully transferred to {1}",Amount,ReceiverName);
        }
        public static void ShowBalance(decimal Balance)
        {
            Console.WriteLine("Current Balance: {0} ",Balance);
        }
        public static void Statement(int serialNumber, string Transaction)
        {
            Console.WriteLine("{0}\t{1}",serialNumber,Transaction);
        }
        public static void ShowTransactions(List<string> Transactions)
        {
            int Count = 1;

            foreach( string Trans in Transactions)
            {
                string[] temp = Trans.Split("|");
                Console.WriteLine("{0}\t"+"TID - "+ "{1}\n\t\t" + "Type - " + "{2}\n\t\t" + "Amount - " + "{3}\n\t\t" + "Balance - " + "{4}\n\t\t" + "DoneOn - " + "{5}", Count,temp[0],temp[1],temp[2],temp[3],temp[4]);
                Count++;
                Console.WriteLine();
            }
        }
    }
}
