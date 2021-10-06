using BankingApplication.Database;
using BankingApplication.Models;
using System;
using BankingApplication.Exceptions;
using BankingApplication.UserInteraction;
namespace BankingApplication
{
    public class DepositAmountService
    {
        
        public void DepositAmount(double accNumber, int amount)
        {
                //deposits money and updates account details.

                //load data into accounts dictionary first.
                DataLoader.LoadData();
                if (!Account.accounts.ContainsKey(accNumber))
                {
                throw new AccountDoesntExistException("Invalid account number. Please provide a valid one.");
                }

                else if (amount > 0)
                {
                    //update current object's available balance.
                    string details = DateTime.Now + " " + amount + "INR" + " Credited";
                    amount = Convert.ToInt32(Account.accounts[accNumber]["balance"]) + amount;
                    Account.accounts[accNumber]["balance"] = Convert.ToString(amount);
                    DataReaderWriter.writeAccounts(Account.accounts);
                    UserOutput.Success("Credited");
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
                    throw new InvalidAmountException("Invalid amount to deposit.");
                    
                }
        }
            
    }
}

