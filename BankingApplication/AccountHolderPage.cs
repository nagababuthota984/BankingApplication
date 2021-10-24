using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{
    public class AccountHolderPage
    {
        public void UserInterface()
        {
            string userName = UserInput.AskUser("Username");
            string password = UserInput.AskUser("Password");
            if (!IsValidUser(userName, password))
            {
                Console.WriteLine("Invalid Credentials\n");
            }
            else
            {
                TransactionService transService = new TransactionService();
                Account userAccount = AccountService.FetchAccountByUserName(userName);
                while (true)
                {
                    try
                    {

                        AccountHolderMenu Choice = UserInput.ShowMenu();

                        switch (Choice)
                        {
                            //done


                            case AccountHolderMenu.Deposit:
                                Console.WriteLine("\t-------Money Deposit-------\n");
                                decimal amount = int.Parse(UserInput.AskUser("Amount to Deposit"));
                                transService.DepositAmount(userAccount, amount);
                                UserOutput.Success("Credited");

                                break;

                            //done
                            case AccountHolderMenu.Withdraw:
                                //Console.WriteLine("\n-------Amount Withdrawl-------\n");
                                amount = decimal.Parse(UserInput.AskUser("Amount to Withdraw"));
                                transService.WithdrawAmount(userAccount, amount);
                                UserOutput.Success("Debited");
                                break;
                            //done
                            case AccountHolderMenu.Transfer:
                                Console.WriteLine("-------Amount Transfer-------\n");
                                string receiverAccNumber = UserInput.AskUser("Receiver Account Number");
                                Account recipientAccount = AccountService.FetchAccountByAccNumber(receiverAccNumber);
                                amount = decimal.Parse(UserInput.AskUser("Amount to Transfer"));
                                ModeOfTransfer mode = (ModeOfTransfer)int.Parse(UserInput.AskUser("mode of transfer\n1.RTGS \n2.IMPS."));
                                transService.TransferAmount(userAccount, recipientAccount, amount, mode);
                                UserOutput.Success("Transferred");
                                break;
                            case AccountHolderMenu.PrintStatement:
                                Console.WriteLine("-------Transaction History-------\n");
                                UserOutput.ShowTransactions(transService.FetchTransactionHistory(userAccount));
                                break;

                            default:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }


        private static bool IsValidUser(string username, string password)
        {
            if (RBIStorage.banks != null)
            {
                foreach (var bank in RBIStorage.banks)
                {
                    foreach (var account in bank.Accounts)
                    {
                        if (account.UserName.Equals(username))
                        {
                            if (account.Password.Equals(password))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
