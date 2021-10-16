using BankingApplication.Services;
using System;
using BankingApplication.Models;
using BankingApplication.Database;
using System.Collections.Generic;

namespace BankingApplication.CLI
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while (true)
            {

                WelcomeMenu Choice = UserInput.ShowMenu();
                switch (Choice)
                {
                    //done
                    case WelcomeMenu.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Account NewAccount = new Account();
                        NewAccount.Name = UserInput.AskUser("Name");
                        NewAccount.Age = int.Parse(UserInput.AskUser("age"));
                        NewAccount.Gender = UserInput.AskUser("Gender");
                        NewAccount.Dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        NewAccount.ContactNumber = Double.Parse(UserInput.AskUser("Contact Number"));
                        NewAccount.AadharNumber = Double.Parse(UserInput.AskUser("Aadhar Number"));
                        NewAccount.PanNumber = UserInput.AskUser("PAN Number");
                        NewAccount.Address = UserInput.AskUser("Address");
                        NewAccount.AccountType = UserInput.AskUser("Account Type(Savings/Current)");
                        NewAccount.BankName = UserInput.AskUser("Name of the bank");
                        NewAccount.BankBranch = UserInput.AskUser("Bank Branch");
                        NewAccount.BankIfsc = UserInput.AskUser("IFSC");
                        
                        BankService acc = new BankService();
                        List<string> Details = acc.CreateAccount(NewAccount);
                        UserOutput.AccountCreationSuccess(Details[0], Details[1].ToString());
                        break;

                    case WelcomeMenu.Deposit:
                        Console.WriteLine("\t-------Money Deposit-------\n");
                        string AccNumber = UserInput.AskUser("Account Number");
                        string BankName = UserInput.AskUser("Name of the bank");
                        decimal Amount = int.Parse(UserInput.AskUser("Amount to Deposit"));
                        try
                        {
                            string UserName = BankService.ValidateAccount(AccNumber,BankName);
                            UserOutput.GreetUser(UserName);
                            AccountService Service = new AccountService();
                            decimal Balance = Service.DepositAmount(AccNumber, Amount, BankName);
                            UserOutput.Success("Credited");
                            UserOutput.ShowBalance(Balance);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    //done
                    case WelcomeMenu.Withdraw:
                        Console.WriteLine("\n-------Amount Withdrawl-------\n");
                        AccNumber = UserInput.AskUser("Account number");
                        BankName = UserInput.AskUser("Bank Name");
                        Amount = decimal.Parse(UserInput.AskUser("Amount to Withdraw"));
                        try
                        {
                            string UserName = BankService.ValidateAccount(AccNumber,BankName);
                            UserOutput.GreetUser(UserName);
                            AccountService Service = new AccountService();
                            decimal Balance = Service.WithdrawAmount(AccNumber,BankName, Amount);
                            UserOutput.Success("Debited");
                            UserOutput.ShowBalance(Balance);
                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidBankException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        break;
                    //done
                    case WelcomeMenu.Transfer:
                        Console.WriteLine("-------Amount Transfer-------\n");
                        string SenderAccNumber = UserInput.AskUser("Sender Account number");
                        string SenderBank = UserInput.AskUser("Sender Bank Name");

                        string ReceiverAccNumber = UserInput.AskUser("Receiver Account Number");
                        string ReceiverBank = UserInput.AskUser("Receiver Bank Name");

                        Amount = decimal.Parse(UserInput.AskUser("Amount to Transfer"));
                        try
                        {
                            BankService.ValidateAccount(SenderAccNumber, SenderBank);
                            BankService.ValidateAccount(ReceiverAccNumber, ReceiverBank);
                            AccountService Service = new AccountService();
                            List<string> TransDetails = Service.TransferAmount(SenderAccNumber,SenderBank, ReceiverAccNumber, ReceiverBank, Amount);
                            UserOutput.Success(TransDetails[0],decimal.Parse(TransDetails[1]));
                            UserOutput.ShowBalance(decimal.Parse(TransDetails[2]));

                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidBankException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        break;
                    case WelcomeMenu.PrintStatement:
                        Console.WriteLine("-------Transaction History-------\n");
                        AccNumber = UserInput.AskUser("AccountNumber");
                        string bankname = UserInput.AskUser("BankName");

                        try
                        {
                            AccountService Service = new AccountService();
                            UserOutput.ShowTransactions(Service.FetchTransactionHistory(AccNumber,bankname));

                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }


            }
        }
    }
}
