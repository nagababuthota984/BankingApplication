using BankingApplication.Database;
using BankingApplication.Models;
using System;
using BankingApplication.Exceptions;
using BankingApplication.UserInteraction;

namespace BankingApplication.Services
{
    public class WithdrawService
    {
       
        public void WithdrawAmount(double accNumber, int amount)
        {
            //withdraws money and updates account details.


            DataLoader.LoadData();
            if (Account.accounts.ContainsKey(accNumber))   //check for validity.
            {
                UserOutput.GreetUser(Account.accounts[accNumber]["name"]);
                if (amount > 0)
                {
                    if (amount <= Convert.ToInt32(Account.accounts[accNumber]["balance"]))
                    {
                        //update current object's available balance.
                        string details = DateTime.Now + " " + amount + "INR" + " Dedited";
                        amount = Convert.ToInt32(Account.accounts[accNumber]["balance"]) - amount;
                        Account.accounts[accNumber]["balance"] = Convert.ToString(amount);
                        DataReaderWriter.writeAccounts(Account.accounts);
                        System.Threading.Thread.Sleep(1000);
                        UserOutput.Success("Debited");
                        System.Threading.Thread.Sleep(1000);
                        UserOutput.ShowBalance(int.Parse(Account.accounts[accNumber]["balance"]));
                        //making the transaction
                        if (!Account.transactions.ContainsKey(accNumber))
                        {
                            Account.transactions.Add(accNumber, details);
                        }
                        else
                        {
                            Account.transactions[accNumber] += "," + details;
                        }
                        DataReaderWriter.writeTransactions(Account.transactions);


                    }
                    else
                    {
                        throw new InsufficientBalanceException("Insufficient account balance.\n");
                    }


                }
                else
                {
                    throw new InvalidAmountException("Invalid amount to withdraw.\n");
                }
            }
            else
            {
                throw new AccountDoesntExistException("\nInvalid account Number. Please provide a valid one.");
            }
        }
    }
}
