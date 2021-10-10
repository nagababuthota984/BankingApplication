using BankingApplication.Database;
using BankingApplication.Models;
using System;
namespace BankingApplication.Services
{
    public class DepositAmountService
    {
        
        public void DepositAmount(double accNumber, int amount)
        {

            //load data into accounts dictionary first.
            DataLoaderService.LoadData();
            //update current object's available balance.
            try
            {
                BalanceValidatorService.ValidateBalance(accNumber, 0, amount);
                string details = DateTime.Now + " " + amount + "INR" + " Credited";
                amount = Convert.ToInt32(DataStructures.Accounts[accNumber]["balance"]) + amount;
                DataStructures.Accounts[accNumber]["balance"] = Convert.ToString(amount);
                DataReaderWriter.writeAccounts(DataStructures.Accounts);
                //UserOutput.Success("Credited");
                //UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[accNumber]["balance"]));
                //making the transaction
                if (!DataStructures.Transactions.ContainsKey(accNumber))
                {
                    DataStructures.Transactions.Add(accNumber, details);
                }
                else
                {
                    DataStructures.Transactions[accNumber] += "," + details;
                }
                DataReaderWriter.writeTransactions(DataStructures.Transactions);
                return;
            }
            catch(InvalidAmountException e)
            {
                throw new InvalidAmountException(e.Message);
            }
        }
                
    }
            
}


