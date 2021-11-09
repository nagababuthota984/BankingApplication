﻿using System;
using System.Collections.Generic;
using System.Linq;
using BankingApplication.Models;
using BankingApplication.Services;
namespace BankingApplication.CLI
{

    public class BankEmployeePage
    {
        private readonly IBankService bankService;
        private readonly IAccountService accountService;
        private readonly ITransactionService transactionService;
        private readonly Program program;
        public BankEmployeePage()
        {
            bankService = Factory.CreateBankService();
            accountService = Factory.CreateAccountService();
            transactionService = Factory.CreateTransactionService();
            program = new Program();
        }
        public void EmployeeInterface()
        {
            Console.WriteLine(Constant.employeeInterfaceHeader);
            string userName = UserInput.GetUserName();
            string password = UserInput.GetPassword();
            Console.WriteLine();

            if (!bankService.IsValidEmployee(userName, password))
            {
                UserOutput.ShowMessage(Constant.invalidCredentialsError);
                if (Console.ReadLine() == "0")
                    program.WelcomeMenu();
                else
                    EmployeeInterface();
            }
            else
            {
                try
                {
                    EmployeeActions();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void EmployeeActions()
        {
            Console.WriteLine(Constant.employeeMenuHeader);
            switch (GetBankEmployeeMenuByInput(UserInput.GetIntegerInput(Constant.employeeMenu)))
            {
                case BankEmployeeMenu.CreateAccount:
                    CreateAccountInterface();
                    break;
                case BankEmployeeMenu.AddBank:
                    AddBankInterface();
                    break;
                case BankEmployeeMenu.UpdateAccount:
                    UpdateAccountInterface();
                    break;
                case BankEmployeeMenu.DeleteAccount:
                    DeleteAccountInterface();
                    break;
                case BankEmployeeMenu.AddNewEmployee:
                    AddNewEmployeeInterface();
                    break;
                case BankEmployeeMenu.AddNewCurrency:
                    AddNewCurrencyInterface();
                    break;
                case BankEmployeeMenu.ModifyServiceCharge:
                    ModifyServiceChargeInterface();
                    break;
                case BankEmployeeMenu.ViewTransactionsForAccount:
                    ViewTransactionsForAccountInterface();
                    break;
                case BankEmployeeMenu.ViewTransactions:
                    ViewTransactionsInterface();
                    break;
                case BankEmployeeMenu.RevertTransaction:
                    RevertTransactionInterface();
                    break;
                case BankEmployeeMenu.Logout:
                    SessionContext.Employee = null;
                    SessionContext.Bank = null;
                    program.WelcomeMenu();
                    return;
            }
            EmployeeActions();
        }

        private void ViewTransactionsInterface()
        {
            Console.WriteLine(Constant.viewTransactionsHeader);
            switch(UserInput.GetIntegerInput(Constant.viewTransactionsOptions))
            {
                case 1:
                    DateTime date = GetDateTimeInput(UserInput.GetInputValue("Date DD-MM-YYYY"));
                    UserOutput.ShowTransactions(bankService.GetTransactionsByDate(date,SessionContext.Bank));
                    break;
                case 2:
                    UserOutput.ShowTransactions(bankService.GetTransactions(SessionContext.Bank));
                    break;
                default:
                    return;
            }
        }

        private void CreateAccountInterface()
        {
            Console.WriteLine(Constant.accountCreationHeader);
            string name = GetName();
            int age = GetAge();
            Gender gender = GetGenderByInput(UserInput.GetIntegerInput(Constant.genderOptions));
            DateTime dob = GetDateTimeInput(UserInput.GetInputValue(Constant.dateOfBirth));
            string contactNumber = UserInput.GetInputValue(Constant.contactNumber);
            long aadharNumber = UserInput.GetLongInput(Constant.aadharNumber);
            string panNumber = UserInput.GetInputValue(Constant.panNumber);
            string address = UserInput.GetInputValue(Constant.address);
            AccountType accountType = (AccountType)UserInput.GetIntegerInput(Constant.accountTypeOptions);
            Customer newCustomer = new Customer(name, age, gender, dob, contactNumber, aadharNumber, panNumber, address);
            Account newAccount = new Account(newCustomer, accountType, SessionContext.Bank);
            bankService.CreateAndAddAccount(newAccount, SessionContext.Bank);
            UserOutput.ShowMessage($"Account has been created!\nCredentials:Username - {newAccount.UserName}\nPassword - {newAccount.Password}\nAccount Number - {newAccount.AccountNumber}\n");
        }
        private void AddBankInterface()
        {
            string bankName = GetName();
            if (!RBIStorage.banks.Any(bank => bank.BankName.Equals(bankName,StringComparison.OrdinalIgnoreCase)))
            {
                string branch = UserInput.GetInputValue(Constant.branch);
                string ifsc = UserInput.GetInputValue(Constant.Ifsc);
                Bank newBank = bankService.CreateAndGetBank(bankName, branch, ifsc);
                if (newBank == null)
                    UserOutput.ShowMessage(Constant.bankNotCreated);
                else
                    UserOutput.ShowMessage($"Bank created with bank id - {newBank.BankId}");
            }
            else
            {
                UserOutput.ShowMessage(Constant.bankAlreadyExists);
            }
        }
        private void UpdateAccountInterface()
        {
            string accountId = UserInput.GetInputValue(Constant.accountId);
            Account userAccount = accountService.GetAccountById(accountId);
            if (userAccount != null)
            {
                UpdateAccountHandler(userAccount);
                Console.WriteLine(Constant.updateConfirmation);
                if (Console.ReadLine().EqualInvariant("y"))
                {
                    bankService.UpdateAccount(userAccount);
                    Console.WriteLine(Constant.updateSuccess);
                }
                else
                    Console.WriteLine(Constant.updateFail);
            }
            else
                UserOutput.ShowMessage(Constant.accountNotFoundError);
        }
        private void UpdateAccountHandler(Account userAccount)
        {

            while (true)
            {
                Console.WriteLine(Constant.updateMenuHeader);
                Console.WriteLine(Constant.customerPropertiesMenu);
                switch (GetCustomerPropertyByInteger(Convert.ToInt32(Console.ReadLine())))
                {
                    case CustomerProperties.Name:
                        Console.WriteLine($"[Existing : {userAccount.Customer.Name}]");
                        userAccount.Customer.Name = GetName();
                        break;
                    case CustomerProperties.Age:
                        Console.WriteLine($"[Existing : {userAccount.Customer.Age}]");
                        userAccount.Customer.Age = GetAge();
                        break;
                    case CustomerProperties.Dob:
                        Console.WriteLine($"[Existing : {userAccount.Customer.Dob}]");
                        userAccount.Customer.Dob = GetDateTimeInput(UserInput.GetInputValue("Date of Birth"));
                        break;
                    case CustomerProperties.AadharNumber:
                        Console.WriteLine($"[Existing : {userAccount.Customer.AadharNumber}]");
                        userAccount.Customer.AadharNumber = UserInput.GetLongInput("Aadhar number");
                        break;
                    case CustomerProperties.PanNumber:
                        Console.WriteLine($"[Existing : {userAccount.Customer.PanNumber}]");
                        userAccount.Customer.PanNumber = UserInput.GetInputValue("Pan number");
                        break;
                    case CustomerProperties.ContactNumber:
                        Console.WriteLine($"[Existing : {userAccount.Customer.ContactNumber}]");
                        userAccount.Customer.ContactNumber = UserInput.GetInputValue("Contact number");
                        break;
                    case CustomerProperties.Address:
                        Console.WriteLine($"[Existing : {userAccount.Customer.Address}]");
                        userAccount.Customer.Address = UserInput.GetInputValue("Address");
                        break;
                    default:
                        return;

                }
            }
        }
        private void DeleteAccountInterface()
        {
            string accountId = UserInput.GetInputValue(Constant.accountId);
            Account userAccount = accountService.GetAccountById(accountId);
            if (userAccount != null)
            {
                Console.WriteLine(Constant.deleteAccountConfirmation);
                if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    if (bankService.DeleteAccount(userAccount))
                    {
                        UserOutput.ShowMessage(Constant.accountDeleteSuccess);
                    }
                    else
                    {
                        UserOutput.ShowMessage(Constant.accountDeleteFail);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                UserOutput.ShowMessage(Constant.accountNotFoundError);
            }
        }
        private void AddNewEmployeeInterface()
        {
            Console.WriteLine(Constant.addNewEmployeeHeader);
            string name = GetName();
            int age = GetAge();
            DateTime dob = GetDateTimeInput((UserInput.GetInputValue(Constant.dateOfBirth)));
            Gender gender = GetGenderByInput(UserInput.GetIntegerInput(Constant.genderOptions));
            EmployeeDesignation role = (EmployeeDesignation)UserInput.GetIntegerInput(Constant.designationOptions);
            Employee newEmployee = bankService.CreateAndGetEmployee(name, age, dob, gender, role, SessionContext.Bank);
            UserOutput.ShowMessage($"Employee {newEmployee.Name} has been added! Credentials:\n{newEmployee.UserName}\n{newEmployee.Password}\n");
        }
        private void AddNewCurrencyInterface()
        {
            string newCurrencyName = UserInput.GetInputValue(Constant.newCurrencyName);
            decimal exchangeRate = UserInput.GetDecimalInput(Constant.newExchangeRate);
            if (exchangeRate > 0)
            {
                if (bankService.AddNewCurrency(SessionContext.Bank, newCurrencyName, exchangeRate))
                    UserOutput.ShowMessage(Constant.currencyAdded);
                else
                    UserOutput.ShowMessage(Constant.currencyAlreadyExists);
            }
            else
            {
                UserOutput.ShowMessage(Constant.invalidExchangeRate);
            }
        }
        private void ModifyServiceChargeInterface()
        {
            ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(UserInput.GetInputValue(Constant.transferModeOptions));
            bool isSelfBankTransfer = UserInput.GetIntegerInput(Constant.selfOrOtherOptions).Equals(1);
            decimal value = UserInput.GetDecimalInput(Constant.newChargeValue);
            Console.WriteLine(Constant.updateConfirmation);
            if (Console.ReadLine().EqualInvariant("y"))
            {
                if (bankService.ModifyServiceCharge(mode, isSelfBankTransfer, SessionContext.Bank, value))
                {
                    UserOutput.ShowMessage(Constant.updateSuccess);
                }
                else
                {
                    UserOutput.ShowMessage(Constant.updateFail);
                }
            }
        }
        private void ViewTransactionsForAccountInterface()
        {
            string accountId = UserInput.GetInputValue(Constant.accountId);
            List<Transaction> transactions = bankService.GetAccountTransactions(accountId);
            if (transactions != null)
            {
                UserOutput.ShowTransactions(transactions);
            }
            else
            {
                UserOutput.ShowMessage(Constant.accountNotFoundError);
            }
        }
        private void RevertTransactionInterface()
        {
            string transactionId = UserInput.GetInputValue(Constant.transactionId);
            Transaction transaction = transactionService.GetTransactionById(transactionId);
            if (transaction != null)
            {
                if (transaction.SenderBankId.Equals(transaction.ReceiverBankId, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(Constant.revertConfirmation);
                    if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        if (bankService.RevertTransaction(transaction, SessionContext.Bank))
                        {
                            Console.WriteLine(Constant.revertSuccess);
                        }
                        else
                        {
                            Console.WriteLine(Constant.revertFail);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(Constant.otherBankInvolvedRevertFail);
                }
            }
            else
            {
                UserOutput.ShowMessage(Constant.noSuchTransaction);
            }
        }
        private CustomerProperties GetCustomerPropertyByInteger(int v)
        {
            if (v == 1)
                return CustomerProperties.Name;
            else if (v == 2)
                return CustomerProperties.Age;
            else if (v == 3)
                return CustomerProperties.Gender;
            else if (v == 4)
                return CustomerProperties.Dob;
            else if (v == 5)
                return CustomerProperties.AadharNumber;
            else if (v == 6)
                return CustomerProperties.PanNumber;
            else if (v == 7)
                return CustomerProperties.ContactNumber;
            else if (v == 8)
                return CustomerProperties.Address;
            else
                return CustomerProperties.None;


        }
        private BankEmployeeMenu GetBankEmployeeMenuByInput(int v)
        {
            if (v == 1)
                return BankEmployeeMenu.CreateAccount;
            else if (v == 2)
                return BankEmployeeMenu.AddBank;
            else if (v == 3)
                return BankEmployeeMenu.UpdateAccount;
            else if (v == 4)
                return BankEmployeeMenu.DeleteAccount;
            else if (v == 5)
                return BankEmployeeMenu.AddNewEmployee;
            else if (v == 6)
                return BankEmployeeMenu.AddNewCurrency;
            else if (v == 7)
                return BankEmployeeMenu.ModifyServiceCharge;
            else if (v == 8)
                return BankEmployeeMenu.ViewTransactionsForAccount;
            else if(v==9)
                return BankEmployeeMenu.ViewTransactions;
            else if (v == 10)
                return BankEmployeeMenu.RevertTransaction;
            else
                return BankEmployeeMenu.Logout;
        }
        private Gender GetGenderByInput(int v)
        {
            if (v == 1)
            {
                return Gender.Male;
            }
            else if (v == 2)
            {
                return Gender.Female;
            }
            else
            {
                return Gender.PreferNotToSay;
            }
        }
        private string GetName()
        {
            Console.WriteLine("Please enter name:");
            string name = Console.ReadLine();
            if(hasSpecialChar(name))
            {
                Console.WriteLine("Name should not contain special characters. please enter again");
                return GetName();
            }
            while (name.Length < 3 || name.Any(Char.IsDigit))
            {
                Console.WriteLine(Constant.invalidNameFormat);
                name = Console.ReadLine();
            }
            return name;
        }
        private bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        private int GetAge()
        {
            int age = UserInput.GetIntegerInput("age");
            if (age <= 0 || age > 100)
            {
                Console.WriteLine(Constant.invalidAgeFormat);
                return GetAge();
            }
            return age;
        }
        private DateTime GetDateTimeInput(string datetime)
        {
            DateTime dob;
            try
            {
                return Convert.ToDateTime(datetime);
            }
            catch (Exception e)
            {
                Console.WriteLine(Constant.invalidDateFormat);
                dob = GetDateTimeInput(Console.ReadLine());

            }
            return dob;

        }


    }
}
