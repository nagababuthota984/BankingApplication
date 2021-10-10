using BankingApplication.Database;
using BankingApplication.Models;
using System;


namespace BankingApplication.Services
{
    public class TransferService
    {
        public void TransferAmount(double senderAcc, double receiverAcc, int amount)
        {
            //transfers money from one accc to another
            DataLoaderService.LoadData();
            try
            {
                //balance validator
                BalanceValidatorService.ValidateBalance(senderAcc, amount);

                string receiverDetails = DateTime.Now + " " + amount + "INR" + " Transfered from " + DataStructures.Accounts[senderAcc]["name"];
                string senderDetails = DateTime.Now + " " + amount + "INR" + " Transfered to " + DataStructures.Accounts[receiverAcc]["name"];

                int senderBalance = Convert.ToInt32(DataStructures.Accounts[senderAcc]["balance"]) - amount;   //calculate sender and receiver balances
                int receiverBalance = Convert.ToInt32(DataStructures.Accounts[receiverAcc]["balance"]) + amount;

                DataStructures.Accounts[senderAcc]["balance"] = Convert.ToString(senderBalance);            //update sender and receiver balances.
                DataStructures.Accounts[receiverAcc]["balance"] = Convert.ToString(receiverBalance);
                DataReaderWriter.writeAccounts(DataStructures.Accounts);


                //lgging the transaction for sender
                if (!DataStructures.Transactions.ContainsKey(senderAcc))
                {
                    DataStructures.Transactions.Add(senderAcc, senderDetails);
                }
                else
                {
                    DataStructures.Transactions[senderAcc] += "," + senderDetails;
                }
                //logging the transaction for receiver.
                if (!DataStructures.Transactions.ContainsKey(receiverAcc))
                {
                    DataStructures.Transactions.Add(receiverAcc, receiverDetails);
                }
                else
                {
                    DataStructures.Transactions[receiverAcc] += "," + receiverDetails;
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

