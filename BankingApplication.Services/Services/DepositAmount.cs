using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    class DepositAmount
    {
        int amount;
        double accNumber;
        public DepositAmount()
        {
            amount = 0;
        }
        public void depositAmount()
        {
            //deposits money and updates account details.

            //load data into accounts dictionary first.
            DataLoader.LoadData();
            Console.WriteLine("Enter your Account Number: ");
            accNumber = Convert.ToDouble(Console.ReadLine());

            if (Account.accounts.ContainsKey(accNumber))   //check for validity.
            {
                Console.WriteLine("Hello! {0}", Account.accounts[accNumber]["name"]);
                Console.WriteLine("Enter amount to deposit: ");
                amount = Convert.ToInt32(Console.ReadLine());
                if (amount > 0)
                {
                    //update current object's available balance.
                    string details = DateTime.Now + " " + amount + "INR" + " Credited";
                    amount = Convert.ToInt32(Account.accounts[accNumber]["balance"]) + amount;
                    Account.accounts[accNumber]["balance"] = Convert.ToString(amount);
                    DataReaderWriter.writeAccounts(Account.accounts);
                    Console.WriteLine("Amount has been credited successfully!");
                    Console.WriteLine("Current balance ->{0}\n\n", Account.accounts[accNumber]["balance"]);

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
                    Console.WriteLine("Invalid amount to deposit.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist!");
            }
        }
    }
}
