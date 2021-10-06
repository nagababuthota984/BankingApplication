using BankingApplication.Exceptions;
using BankingApplication.Models;
using BankingApplication.UserInteraction;
using System;

namespace BankingApplication.Services
{
    public class PrintStatementService
    {
    
        public void PrintTransactionHistory(double accNumber)
        {
            //prints user's acount history
            DataLoader.LoadData();
            if (Account.accounts.ContainsKey(accNumber))     //user should have an account
            {
                if (Account.transactions.ContainsKey(accNumber))    //Should contain atleast one transaction.
                {
                    string trans = Account.transactions[accNumber];
                    string[] transList = trans.Split(",");
                    int count = 1;
                    UserOutput.GreetUser(Account.accounts[accNumber]["name"]);
                    foreach (string transaction in transList)
                    {
                        UserOutput.Statement(count,transaction);
                        count++;
                    }
                }
                else
                {
                    UserOutput.ErrorMessage("None transactions recorded so far!");
                }
            }
            else
            {
                throw new AccountDoesntExistException("Account Doesnt Exist.");
            }

        }
    }
}
