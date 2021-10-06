using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using BankingApplication.UserInteraction;
namespace BankingApplication.Services
{
    public class AccountCreationService : Account
    {
        //creates a new account
        public void CreateAccount(Account newAccount)
        {

            //Account newAccount = new Account();
            newAccount.accountType = UserInput.AskUser("Account Type(Savings/Current)");
            newAccount.balance = Account.MIN_BALANCE;
            do
            {
                Random random = new Random();          //account number generator.
                string r = "";
                int i;
                for (i = 1; i < 11; i++)
                {
                    r += random.Next(0, 9).ToString();
                }
                newAccount.accountNumber = Convert.ToDouble(r);


            } while (accounts.ContainsKey(newAccount.accountNumber));

            accounts.Add(newAccount.accountNumber, new Dictionary<string, string>());
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
            
            UserOutput.AccountCreationSuccess(newAccount.name, newAccount.accountNumber.ToString());

            //writing to json
            DataReaderWriter.writeAccounts(accounts);





        }
    }
}
