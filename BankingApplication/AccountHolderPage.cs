using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankingApplication.CLI
{
    public class AccountHolderPage
    {
        public void UserInterface()
        {
            Console.WriteLine("=================CUSTOMER LOGIN================");
            string userName = UserInput.GetInputValue("Username");
            string password = UserInput.GetInputValue("Password");
            Console.WriteLine();
            AccountService accountService = new AccountService();
            Account userAccount = accountService.GetAccountByUserName(userName);

            if (userAccount==null)
            {
                Console.WriteLine("Invalid Credentials\n");
            }
            else
            {
                TransactionService transService = new TransactionService();
                BankService bankService =  new BankService();
                Bank bank = bankService.GetBankByBankId(userAccount.BankId);
                while (true)
                {
                    try
                    {

                        AccountHolderMenu choice = UserInput.ShowAccountHolderMenu();
                        switch (choice)
                        {
                            case AccountHolderMenu.Deposit:
                                Console.WriteLine("\t-------Money Deposit-------\n");
                                decimal amount = Convert.ToInt32(UserInput.GetInputValue("Amount to Deposit"));
                                if (amount>0)
                                {
                                    string currencyName = UserInput.GetInputValue("Currency Name");
                                    Currency currency = bank.SupportedCurrency.FirstOrDefault(c => (c.CurrencyName.ToLower())==(currencyName.ToLower()));
                                    if (currency != null)
                                    {
                                        accountService.DepositAmount(userAccount, amount,currency);
                                        UserOutput.ShowMessage("Credited successfully\n");
                                    }
                                    else
                                    {
                                        UserOutput.ShowMessage("Unsupported currency type");
                                    }
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Please enter valid amount.\n");
                                }
                                break;

                            case AccountHolderMenu.Withdraw:
                                Console.WriteLine("\n-------Amount Withdrawl-------\n");
                                amount = Convert.ToDecimal(UserInput.GetInputValue("Amount to Withdraw"));
                                if (amount > 0)
                                {
                                    if (amount <= userAccount.Balance)
                                    {
                                        accountService.WithdrawAmount(userAccount, amount,bank);
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
                                string receiverAccNumber = UserInput.GetInputValue("Receiver Account Number");
                                Account recipientAccount = accountService.GetAccountByAccNumber(receiverAccNumber);

                                if (recipientAccount!=null)
                                {
                                    amount = Convert.ToDecimal(UserInput.GetInputValue("Amount to Transfer"));
                                    if (amount > 0)
                                    {
                                        if (amount >= userAccount.Balance)
                                        {
                                            ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(UserInput.GetInputValue("mode of transfer\n1.RTGS \n2.IMPS."));
                                            accountService.TransferAmount(userAccount,bank, recipientAccount, amount, mode);
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
                                }
                                else
                                {
                                    UserOutput.ShowMessage("Recipient account doesn't exists.\n");
                                }
                                break;
                            case AccountHolderMenu.PrintStatement:
                                Console.WriteLine("\n-------Transaction History-------\n");
                                UserOutput.ShowTransactions(userAccount.Transactions);
                                break;
                            case AccountHolderMenu.CheckBalance:
                                Console.WriteLine($"\nCurrent Balance - {userAccount.Balance} INR\n");
                                break;
                            case AccountHolderMenu.LogOut:
                                Program.WelcomeMenu();
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


    }
}
