using BankingApplication.Database;
using BankingApplication.Models;
using System;
using BankingApplication.Exceptions;
using BankingApplication.UserInteraction;

namespace BankingApplication.Services
{
    public class TransferService
    {
        public void TransferAmount(double senderAcc, double receiverAcc, int amount)
        {
            //transfers money from one accc to another
            DataLoader.LoadData();
            
            if (Account.accounts.ContainsKey(senderAcc))//check whether both of the accounts exist.
            {
                if (Account.accounts.ContainsKey(receiverAcc))
                {
                    
                    if (amount > 0)
                    {
                        if (amount <= Convert.ToInt32(Account.accounts[senderAcc]["balance"]))
                        {
                            //generate transaction details for both sender and receiver.
                            string receiverDetails = DateTime.Now + " " + amount + "INR" + " Transfered from " + Account.accounts[senderAcc]["name"];
                            string senderDetails = DateTime.Now + " " + amount + "INR" + " Transfered to " + Account.accounts[receiverAcc]["name"];

                            int senderBalance = Convert.ToInt32(Account.accounts[senderAcc]["balance"]) - amount;   //calculate sender and receiver balances
                            int receiverBalance = Convert.ToInt32(Account.accounts[receiverAcc]["balance"]) + amount;

                            Account.accounts[senderAcc]["balance"] = Convert.ToString(senderBalance);            //update sender and receiver balances.
                            Account.accounts[receiverAcc]["balance"] = Convert.ToString(receiverBalance);

                            DataReaderWriter.writeAccounts(Account.accounts);
                            UserOutput.Success(Account.accounts[receiverAcc]["name"], amount);
                            UserOutput.ShowBalance(int.Parse(Account.accounts[senderAcc]["balance"]));


                            //lgging the transaction for sender
                            if (!Account.transactions.ContainsKey(senderAcc))
                            {
                                Account.transactions.Add(senderAcc, senderDetails);
                            }
                            else
                            {
                                Account.transactions[senderAcc] += "," + senderDetails;
                            }
                            //logging the transaction for receiver.
                            if (!Account.transactions.ContainsKey(receiverAcc))
                            {
                                Account.transactions.Add(receiverAcc, receiverDetails);
                            }
                            else
                            {
                                Account.transactions[receiverAcc] += "," + receiverDetails;
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
                        throw new AccountDoesntExistException("Invalid receipient account number.");
                    }

                }
                else
                {
                    throw new AccountDoesntExistException("Invalid Sender account number.");
                }

            }
        }
    }
}
