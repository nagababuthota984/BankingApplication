using BankingApplication.Database;
using BankingApplication.Models;
using System;

namespace BankingApplication.Services
{
    public class WithdrawService
    {
       
        public void WithdrawAmount(double accNumber, int amount)
        {
            //withdraws money and updates account details.


            DataLoaderService.LoadData();
            try
            {
                BalanceValidatorService.ValidateBalance(accNumber, amount);
                //update current object's available balance.
                string details = DateTime.Now + " " + amount + "INR" + " Dedited";
                amount = Convert.ToInt32(DataStructures.Accounts[accNumber]["balance"]) - amount;
                DataStructures.Accounts[accNumber]["balance"] = Convert.ToString(amount);
                DataReaderWriter.writeAccounts(DataStructures.Accounts);
                System.Threading.Thread.Sleep(1000);
                System.Threading.Thread.Sleep(1000);
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
            catch (InsufficientBalanceException e)
            {
                throw new InsufficientBalanceException(e.Message);
            }
            catch (InvalidAmountException e)
            {
                throw new InvalidAmountException(e.Message);
            }
        }
    }
}
