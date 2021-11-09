﻿using System.IO;

namespace BankingApplication.Models
{
    public class Constant
    {
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        public static string filePath = $"{projectDirectory}\\BankingApplication.Database\\Data.json";
        public static string employeeMenu = "Choice\n1.Create Account\n2.AddBank\n3.UpdateAccount\n4.Delete an Account\n5.AddNewEmployee\n6.AddNewCurrency\n7.ModifyServiceCharge\n8.ViewTransactionsForAccount\n9.View Transactions\n10.RevertTransaction\nAny other key to logout.";
        public static string welcomeMessage = "\n\n==============================Welcome to Technovert Banking Solutions=============================.\n********************\n1.Account Holder Login\n2.Bank Employee Login.\n\nPlease enter one option\n********************";
        public static string accountNotFoundError = "Account Does not exists.\n";
        public static string customerPropertiesMenu = "Please enter an option:\n1.Name\n2.Age\n3.Gender\n4.Dob\n5.Aadhar\n6.PAN\n7.Contact Number\n8.Address\nAny other key to exit.";
        public static string accountHolderOptions = "\nChoose any one option:\n1.Deposit\n2.Withdraw\n3.Transfer Amount\n4.Print Transaction history\n5.Check Balance\n6.Logout";
        public static string invalidCredentialsError = "Invalid Credentials. Please try again or enter 0 for Main menu\n";
        public static string genderOptions = "Gender:\n1.Male\n2.Female\n3.Prefer Not to say";
        public static string accountTypeOptions= "Account Type(1.Savings/2.Current)";
        public static string bankAlreadyExists= "A Bank with same name already exists!";
        public static string updateConfirmation= "Are you sure you want the changes to save?(Y/N)";
        public static string deleteAccountConfirmation = "\nAre you sure you want to delete this account?(Y/N)";
        public static string employeeMenuHeader = "\n========================BANK STAFF MENU==========================\n";
        public static string addNewEmployeeHeader = "\n-----------Add New Employee-----------\n";
        public static string designationOptions = "Employee Designation\n1.Manager\n2.Accounts Manager\n3.Financial Analyst\n4.LoanOfficer";
        public static string updateSuccess = "Updated successfully";
        public static string updateFail = "Changes aren't applied!";
        public static string accountDeleteSuccess = "Account Deleted";
        public static string accountDeleteFail= "Account was not deleted. Try again later.";
        public static string bankNotCreated = "Bank not created! Try again.";
        public static string currencyAlreadyExists = "Currency Already Exists!";
        public static string invalidExchangeRate= "Invalid exchange rate";
        public static string revertConfirmation = "Are you sure you want to revert the transaction(Y/N)?\n";
        public static string revertSuccess = "Reverted Successfully!\n";
        public static string revertFail = "Could not revert!";
        public static string otherBankInvolvedRevertFail = "Cannot revert transaction involving other bank.";
        public static string transferModeOptions = "Choose Transfer mode:\n1.RTGS\n2.IMPS";
        public static string selfOrOtherOptions = "Charge type:\n1.Money Transfer Within bank.\n2.Money transfer to other banks";
        public static string noSuchTransaction= "No such transaction found!\n";
        public static string invalidDateFormat= "Invalid Date. Please enter date in valid format YYYYMMDD";
        public static string invalidNameFormat= "Name should not contain digits and should be of length greater than 3.Please enter the valid name. Example: Sam Daniels:\n";
        public static string invalidAgeFormat= "Please enter valid age. Re-enter age:";
        public static string accountCreationHeader= "\t-------Account Creation-------\n";
        public static string employeeInterfaceHeader = "\n===================BANK EMPLOYEE LOGIN===================\n";
        public static string customerInterfaceHeader= "=================CUSTOMER LOGIN================";
        public static string moneyDepositHeader= "\t-------Money Deposit-------\n";
        public static string creditSuccess= "Credited successfully\n";
        public static string unsupportedCurrency= "Unsupported currency type";
        public static string invalidAmount= "Please enter valid amount.\n";
        public static string withdrawlHeader= "\n-------Amount Withdrawl-------\n";
        public static string debitSuccess= "Debited successfully";
        public static string insufficientFunds= "Insufficient funds.\n";
        public static string transferHeader= "-------Amount Transfer-------\n";
        public static string transferSuccess= "Transferred successfully";
        public static string recipientAccountNotFound="Recipient account doesn't exists.\n";
        public static string transactionHistoryHeader= "\n-------Transaction History-------\n";
        public static string dateOfBirth = "Date of Birth";
        public static string contactNumber= "Contact Number";
        public static string aadharNumber= "Aadhar Number";
        public static string panNumber= "PAN Number";
        public static string address= "Address";
        public static string bankName= "Name of the bank";
        public static string branch= "Name of the bank";
        public static string accountId="Account Id";
        public static string updateMenuHeader = "\n--------------UPDATE MENU----------------";
        public static string newCurrencyName= "new currency name";
        public static string currencyName = "Currency Name";
        public static string newExchangeRate= "exchange rate";
        public static string currencyAdded= "exchange rate";
        public static string newChargeValue= "New Charge Value:";
        public static string transactionId= "Transaction Id";
        public static string amountToDeposit = "amount to deposit";
        public static string amountToWithdraw= "amount to withdraw";
        public static string receiverAccountNumber= "Receiver Account Number";
        public static string amountToTransfer= "Amount to Transfer";
        public static string Ifsc = "IFSC";
        public static string viewTransactionsHeader = "\n-----------View Transactions----------\n";
        public static string viewTransactionsOptions = "\n1.On Particular date\n2.All transactions\nPlease enter your choice";
    }
}
