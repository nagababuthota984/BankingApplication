using BankingApplication.Services;
using System;
using BankingApplication.Models;
using BankingApplication.Database;

namespace BankingApplication.CLI
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while (true)
            {

                WelcomeMenu choice = UserInput.ShowMenu();
                switch (choice)
                {
                    //done
                    case WelcomeMenu.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Account newAccount = new Account();
                        newAccount.name = UserInput.AskUser("Name");
                        newAccount.age = int.Parse(UserInput.AskUser("age"));
                        newAccount.gender = UserInput.AskUser("Gender");
                        newAccount.dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        newAccount.contactNumber = Double.Parse(UserInput.AskUser("Contact Number"));
                        newAccount.aadharNumber = Double.Parse(UserInput.AskUser("Aadhar Number"));
                        newAccount.panNumber = Double.Parse(UserInput.AskUser("PAN Number"));
                        newAccount.address = UserInput.AskUser("Address");
                        newAccount.accountType = UserInput.AskUser("Account Type(Savings/Current)");
                        
                        AccountCreationService acc = new AccountCreationService();
                        Account FreshAccount = acc.CreateAccount(newAccount);
                        UserOutput.AccountCreationSuccess(FreshAccount.name, FreshAccount.accountNumber.ToString());
                        break;
                    case WelcomeMenu.Deposit:
                        Console.WriteLine("\t-------Money Deposit-------\n");
                        double AccNumber = Double.Parse(UserInput.AskUser("Account Number"));
                        int amount = int.Parse(UserInput.AskUser("amount to Deposit"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            DepositAmountService deposit = new DepositAmountService();
                            deposit.DepositAmount(AccNumber, amount);
                            UserOutput.Success("Credited");
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["balance"]));
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
                        AccNumber = Double.Parse(UserInput.AskUser("Account number"));
                        amount = int.Parse(UserInput.AskUser("Amount to Withdraw"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            UserOutput.GreetUser(DataStructures.Accounts[AccNumber]["name"]);
                            WithdrawService ws = new WithdrawService();
                            ws.WithdrawAmount(AccNumber, amount);
                            UserOutput.Success("Debited");
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["balance"]));
                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidAmountException e)
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
                        AccNumber = Double.Parse(UserInput.AskUser("Sender Account number"));
                        double receiver = Double.Parse(UserInput.AskUser("Receiver Account Number"));
                        amount = int.Parse(UserInput.AskUser("Amount to Transfer"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            AccountValidatorService.ValidateAccount(receiver);
                            TransferService ts = new TransferService();
                            ts.TransferAmount(AccNumber, receiver, amount);
                            UserOutput.Success(DataStructures.Accounts[receiver]["name"], amount);
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["balance"]));

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


                        break;
                    case WelcomeMenu.PrintStatement:
                        Console.WriteLine("-------Transaction History-------\n");
                        AccNumber = Double.Parse(UserInput.AskUser("AccountNumber"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            PrintStatementService ps = new PrintStatementService();
                            UserOutput.ShowTransactions(ps.FetchTransactionHistory(AccNumber));

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
