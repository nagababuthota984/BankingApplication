using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankingApplication.CLI
{
    public class AccountHolderPage
    {
        IAccountService accountService;
        Program program;
        public AccountHolderPage()
        {
            accountService = Factory.CreateAccountService();
            program = new Program(); 
        }
        public void CustomerInterface()
        {
            Console.WriteLine("=================CUSTOMER LOGIN================");
            string userName = UserInput.GetUserName();
            string password = UserInput.GetPassword();
            Console.WriteLine();
            if (!accountService.IsValidCustomer(userName, password))
            {
                Console.WriteLine("Invalid Credentials. Please try again or enter 0 for Main Menu\n");
                if (Console.ReadLine() == "0")
                {
                    program.WelcomeMenu();
                }
                else
                {
                    CustomerInterface();
                }
            }
            else
            {
                while (true)
                {
                    try
                    {

                        switch (UserInput.ShowAccountHolderMenu())
                        {
                            case AccountHolderMenu.Deposit:
                                Console.WriteLine("\t-------Money Deposit-------\n");
                                decimal amount = UserInput.GetDecimalInput("amount to deposit");

                                if (amount > 0)
                                {
                                    string Name = UserInput.GetInputValue("Currency Name");
                                    Currency currency = SessionContext.Bank.SupportedCurrency.FirstOrDefault(c => c.Name.EqualInvariant(Name));
                                    if (currency != null)
                                    {
                                        accountService.DepositAmount(SessionContext.Account, amount, currency);
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
                                amount = UserInput.GetDecimalInput("amount to withdraw");
                                if (amount > 0)
                                {
                                    if (amount <= SessionContext.Account.Balance)
                                    {
                                        accountService.WithdrawAmount(SessionContext.Account, amount);
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

                                if (recipientAccount != null)
                                {
                                    amount = UserInput.GetDecimalInput("Amount to Transfer");
                                    if (amount > 0)
                                    {
                                        if (amount <= SessionContext.Account.Balance)
                                        {
                                            ModeOfTransfer mode = (ModeOfTransfer)UserInput.GetIntegerInput("mode of transfer\n1.RTGS \n2.IMPS.");
                                            accountService.TransferAmount(SessionContext.Account, SessionContext.Bank, recipientAccount, amount, mode);
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
                                UserOutput.ShowTransactions(SessionContext.Account.Transactions);
                                break;
                            case AccountHolderMenu.CheckBalance:
                                Console.WriteLine($"\nCurrent Balance - {SessionContext.Account.Balance} INR\n");
                                break;
                            case AccountHolderMenu.LogOut:
                                SessionContext.Employee = null;
                                SessionContext.Bank = null;
                                program.WelcomeMenu();
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
