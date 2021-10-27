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
            Console.WriteLine("=================CUSTOMER LOGIN================");
            string userName = UserInput.AskUser("Username");
            string password = UserInput.AskUser("Password");
            Console.WriteLine();
            if (!IsValidUser(userName, password))
            {
                Console.WriteLine("Invalid Credentials\n");
            }
            else
            {
                TransactionService transService = new TransactionService();
                Account userAccount = new AccountService().FetchAccountByUserName(userName);
                while (true)
                {
                    try
                    {

                        AccountHolderMenu choice = UserInput.ShowAccountHolderMenu();

                        switch (choice)
                        {


                            case AccountHolderMenu.Deposit:
                                Console.WriteLine("\t-------Money Deposit-------\n");
                                decimal amount = Convert.ToInt32(UserInput.AskUser("Amount to Deposit"));
                                if (amount>0)
                                {
                                    string currencyName = UserInput.AskUser("Currency Name");
                                    transService.DepositAmount(userAccount, amount, currencyName);
                                    UserOutput.ShowMessage("Credited successfully\n"); 
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Please enter valid amount.\n");
                                }
                                break;

                            case AccountHolderMenu.Withdraw:
                                Console.WriteLine("\n-------Amount Withdrawl-------\n");
                                amount = Convert.ToDecimal(UserInput.AskUser("Amount to Withdraw"));
                                if (amount > 0)
                                {
                                    if (amount <= userAccount.Balance)
                                    {
                                        transService.WithdrawAmount(userAccount, amount);
                                        UserOutput.ShowMessage("Debited successfully");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Insufficient funds.\n");
                                    }
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Invalid amount to withdraw.\n");
                                }
                                break;
                            case AccountHolderMenu.Transfer:
                                Console.WriteLine("-------Amount Transfer-------\n");
                                string receiverAccNumber = UserInput.AskUser("Receiver Account Number");
                                Account recipientAccount = new AccountService().FetchAccountByAccNumber(receiverAccNumber);
                                amount = Convert.ToDecimal(UserInput.AskUser("Amount to Transfer"));
                                if (amount>0 )
                                {
                                    if (amount>=userAccount.Balance)
                                    {
                                        ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(UserInput.AskUser("mode of transfer\n1.RTGS \n2.IMPS."));
                                        transService.TransferAmount(userAccount, recipientAccount, amount, mode);
                                        UserOutput.ShowMessage("Transferred successfully"); 
                                    } 
                                    else
                                    {
                                        UserOutput.ShowMessage("Insufficient Funds.\n");
                                    }
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Please enter valid amount.\n");
                                }
                                break;
                            case AccountHolderMenu.PrintStatement:
                                Console.WriteLine("\n-------Transaction History-------\n");
                                UserOutput.ShowTransactions(transService.FetchTransactionHistory(userAccount));
                                break;
                            case AccountHolderMenu.CheckBalance:
                                Console.WriteLine($"\nCurrent Balance - {userAccount.Balance} INR\n");
                                break;
                            case AccountHolderMenu.LogOut:
                                UserInterface();
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
                        if (account.UserName.Equals(username) && account.Password.Equals(password))
                        {
                            return true;
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
