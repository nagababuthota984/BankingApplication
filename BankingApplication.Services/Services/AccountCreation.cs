using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;
using BankingApplication.Database;

namespace BankingApplication.Services
{
    class AccountCreation : Account
    {
        //creates a new account
        public void createAccount()
        {

            Account newAccount = new Account();
            //collects basic details and creates an account.
            Console.WriteLine("Enter your Full Name(as in ID Proof): ");
            newAccount.name = Console.ReadLine();
            Console.WriteLine("Enter your Age: ");
            newAccount.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your Gender: ");
            newAccount.gender = Console.ReadLine();
            Console.WriteLine("Enter your Phone number: ");
            newAccount.contactNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Date of Birth: ");
            newAccount.dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter your Complete Address: ");
            newAccount.address = Console.ReadLine();
            Console.WriteLine("Enter your Aadhar Number: ");
            newAccount.aadharNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Pan Number: ");
            newAccount.panNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter the Account type: ");
            newAccount.accountType = Console.ReadLine();
            newAccount.balance = Account.MIN_BALANCE;

            Random random = new Random();          //account number generator.
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            newAccount.accountNumber = Convert.ToDouble(r);
            Console.WriteLine("Hey {0}!\n This is your account number - {1}\nNote it and use it when you need any service.\n",newAccount.name,newAccount.accountNumber);




            if (!accounts.ContainsKey(newAccount.accountNumber))
            {
                accounts.Add(newAccount.accountNumber, new Dictionary<string, string>());
                //Console.WriteLine(accounts[newAccount.accountNumber]); 
            }
            accounts[newAccount.accountNumber]["name"] = newAccount.name;
            accounts[newAccount.accountNumber]["age"] = Convert.ToString(newAccount.age);
            accounts[newAccount.accountNumber]["gender"] = newAccount.gender;
            accounts[newAccount.accountNumber]["contactNumber"] = Convert.ToString(newAccount.contactNumber);
            accounts[newAccount.accountNumber]["dob"] = Convert.ToString(newAccount.dob);
            accounts[newAccount.accountNumber]["address"] = newAccount.address;
            accounts[newAccount.accountNumber]["aadharNumber"] = Convert.ToString(newAccount.aadharNumber);
            accounts[newAccount.accountNumber]["panNumber"] = Convert.ToString(newAccount.panNumber);
            accounts[newAccount.accountNumber]["accountType"] = newAccount.accountType;
            accounts[newAccount.accountNumber]["balance"] = Convert.ToString(newAccount.balance);

            //writing to json
            DataReaderWriter.writeAccounts(accounts);





        }
    }
}
