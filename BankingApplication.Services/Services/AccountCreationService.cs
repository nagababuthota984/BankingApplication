using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
namespace BankingApplication.Services
{
    public class AccountCreationService : Account
    {
        //creates a new account
        public Account CreateAccount(Account newAccount)
        {

            //Account newAccount = new Account();
            DataLoaderService.LoadData();
            newAccount.balance = 0;
            newAccount.accountNumber = GenerateAccountNumber();
            DataStructures.Accounts.Add(newAccount.accountNumber, new Dictionary<string, string>());
            DataStructures.Accounts[newAccount.accountNumber]["name"] = newAccount.name;
            DataStructures.Accounts[newAccount.accountNumber]["age"] = Convert.ToString(newAccount.age);
            DataStructures.Accounts[newAccount.accountNumber]["gender"] = newAccount.gender;
            DataStructures.Accounts[newAccount.accountNumber]["contactNumber"] = Convert.ToString(newAccount.contactNumber);
            DataStructures.Accounts[newAccount.accountNumber]["dob"] = Convert.ToString(newAccount.dob);
            DataStructures.Accounts[newAccount.accountNumber]["address"] = newAccount.address;
            DataStructures.Accounts[newAccount.accountNumber]["aadharNumber"] = Convert.ToString(newAccount.aadharNumber);
            DataStructures.Accounts[newAccount.accountNumber]["panNumber"] = Convert.ToString(newAccount.panNumber);
            DataStructures.Accounts[newAccount.accountNumber]["accountType"] = newAccount.accountType;
            DataStructures.Accounts[newAccount.accountNumber]["balance"] = Convert.ToString(newAccount.balance);
            

            //writing to json
            DataReaderWriter.writeAccounts(DataStructures.Accounts);
            return newAccount;




        }
        private static double GenerateAccountNumber()
        {
            double Number = 0;

            do
            {
                Random random = new Random();          //account number generator.
                string r = "";
                int i;
                for (i = 1; i < 11; i++)
                {
                    r += random.Next(0, 9).ToString();
                }
                Number = Convert.ToDouble(r);


            } while (DataStructures.Accounts.ContainsKey(Number));
            return Number;

        }

    }
}
