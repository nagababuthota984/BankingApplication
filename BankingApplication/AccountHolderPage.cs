using BankingApplication.Services;
using System;
using BankingApplication.Models;
using System.Collections.Generic;

namespace BankingApplication.CLI
{
    public class AccountHolderPage
    {
        public static void ShowMenu()
        {
            Console.WriteLine("\nWelcome to Technovert Banking Solutions!\n");

            while (true)
            {

                AccountHolderMenu Choice = UserInput.ShowMenu();
                switch (Choice)
                {
                    //done
                    case AccountHolderMenu.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Customer newCustomer = new Customer();
                        Account NewAccount = new Account();
                        newCustomer.Name = UserInput.AskUser("Name");
                        newCustomer.Age = int.Parse(UserInput.AskUser("age"));
                        newCustomer.Gender = UserInput.AskUser("Gender");
                        newCustomer.Dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        newCustomer.ContactNumber = UserInput.AskUser("Contact Number");
                        newCustomer.AadharNumber = UserInput.AskUser("Aadhar Number");
                        newCustomer.PanNumber = UserInput.AskUser("PAN Number");
                        newCustomer.Address = UserInput.AskUser("Address");
                        NewAccount.BankName = UserInput.AskUser("Name of the bank");
                        NewAccount.TypeOfAccount = (AccountType)int.Parse(UserInput.AskUser("Account Type(1.Savings/2.Current)"));
                        NewAccount.Branch = UserInput.AskUser("Bank Branch");
                        NewAccount.Ifsc = UserInput.AskUser("IFSC");

                        BankService acc = new BankService();
                        List<string> Details = acc.CreateAccount(newCustomer,NewAccount);
                        UserOutput.AccountCreationSuccess(Details[0], Details[1].ToString());
                        break;

                    case AccountHolderMenu.Deposit:
                        Console.WriteLine("\t-------Money Deposit-------\n");
                        string AccNumber = UserInput.AskUser("Account Number");
                        string BankName = UserInput.AskUser("Name of the bank");
                        decimal Amount = int.Parse(UserInput.AskUser("Amount to Deposit"));
                        try
                        {
                            Utilities.ValidateAccount(AccNumber, BankName);
                            //UserOutput.GreetUser(UserName);
                            AccountService Service = new AccountService();
                            Service.DepositAmount(AccNumber, Amount, BankName);
                            UserOutput.Success("Credited");
                            UserOutput.ShowBalance(Utilities.FetchBalance(AccNumber,BankName));
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    //done
                    case AccountHolderMenu.Withdraw:
                        Console.WriteLine("\n-------Amount Withdrawl-------\n");
                        AccNumber = UserInput.AskUser("Account number");
                        BankName = UserInput.AskUser("Bank Name");
                        Amount = decimal.Parse(UserInput.AskUser("Amount to Withdraw"));
                        try
                        {
                            Utilities.ValidateAccount(AccNumber, BankName);
                            //UserOutput.GreetUser(UserName);
                            AccountService Service = new AccountService();
                            Service.WithdrawAmount(AccNumber, BankName, Amount);
                            UserOutput.Success("Debited");
                            UserOutput.ShowBalance(Utilities.FetchBalance(AccNumber, BankName));
                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidBankException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        break;
                    //done
                    case AccountHolderMenu.Transfer:
                        Console.WriteLine("-------Amount Transfer-------\n");
                        string SenderAccNumber = UserInput.AskUser("Sender Account number");
                        string SenderBank = UserInput.AskUser("Sender Bank Name");

                        string ReceiverAccNumber = UserInput.AskUser("Receiver Account Number");
                        string ReceiverBank = UserInput.AskUser("Receiver Bank Name");

                        Amount = decimal.Parse(UserInput.AskUser("Amount to Transfer"));
                        try
                        {
                            Utilities.ValidateAccount(SenderAccNumber, SenderBank);
                            Utilities.ValidateAccount(ReceiverAccNumber, ReceiverBank);
                            AccountService Service = new AccountService();
                            Service.TransferAmount(SenderAccNumber, SenderBank, ReceiverAccNumber, ReceiverBank, Amount);

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
                    case AccountHolderMenu.PrintStatement:
                        Console.WriteLine("-------Transaction History-------\n");
                        AccNumber = UserInput.AskUser("AccountNumber");
                        string bankname = UserInput.AskUser("BankName");

                        try
                        {
                            AccountService Service = new AccountService();
                            UserOutput.ShowTransactions(Service.FetchTransactionHistory(AccNumber, bankname));

                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidBankException e)
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
