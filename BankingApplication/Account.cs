using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BankingApplication
{
    public class Account : Person
    {

        // public double accountNumber;
        public double accountNumber;
        public string accountType;
        public double balance;
        public const int MIN_BALANCE = 250;
        public Account()
        {
            double accountNumber = 0;
            string accountType = "";
            double balance = 0;

        }
        public void createAccount()
        {
            
            
            //collects basic details and creates an account.
            Console.WriteLine("Enter your Full Name(as in ID Proof): ");
            this.name = Console.ReadLine();
            Console.WriteLine("Enter your Age: ");
            this.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your Gender: ");
            this.gender = Console.ReadLine();
            Console.WriteLine("Enter your Phone number: ");
            this.contactNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Date of Birth: ");
            this.dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter your Complete Address: ");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter your Aadhar Number: ");
            this.aadharNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your Pan Number: ");
            this.panNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter the Account type: ");
            this.accountType = Console.ReadLine();
            this.balance = Account.MIN_BALANCE;
            
            Random random = new Random();          //account number generator.
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            this.accountNumber = Convert.ToDouble(r);
            Console.WriteLine(this.accountNumber);


            Dictionary<double,Dictionary<string,string>> accounts = readAccounts();
            
            if(!accounts.ContainsKey(this.accountNumber))
            { 
                accounts.Add(this.accountNumber, new Dictionary<string,string>());
                //Console.WriteLine(accounts[this.accountNumber]); 
            }
            accounts[this.accountNumber]["name"] = this.name;
            accounts[this.accountNumber]["age"] = Convert.ToString(this.age);
            accounts[this.accountNumber]["gender"] = this.gender;
            accounts[this.accountNumber]["contactNumber"] = Convert.ToString(this.contactNumber);
            accounts[this.accountNumber]["dob"] = Convert.ToString(this.dob);
            accounts[this.accountNumber]["address"] = this.address;
            accounts[this.accountNumber]["aadharNumber"] = Convert.ToString(this.aadharNumber);
            accounts[this.accountNumber]["panNumber"] = Convert.ToString(this.panNumber);
            accounts[this.accountNumber]["accountType"] = this.accountType;
            accounts[this.accountNumber]["balance"] = Convert.ToString(this.balance);

            //writing to json
            writeAccounts(accounts);

           



        }
        public static Dictionary<double,Dictionary<string,string>> readAccounts()//reads data from json file
        {

            Dictionary<double, Dictionary<string,string>> jsonObject = new Dictionary<double, Dictionary<string,string>>();
            string data = File.ReadAllText("accounts.json");
            jsonObject = JsonConvert.DeserializeObject<Dictionary<double,Dictionary<string,string>>>(data);
            return jsonObject;
            

        }
        public static void writeAccounts(Dictionary<double, Dictionary<string, string>> dataToWrite)    //writes to json file
        {
            string serializedData = JsonConvert.SerializeObject(dataToWrite,Formatting.Indented);
            File.WriteAllText("accounts.json",serializedData);
            //data not being written :(
        }
        public void depositAmount()
        {
            //deposits money and updates account details.
            //int amount;
            //double accNumber;
            //Console.WriteLine("Enter your Account Number: ")
            //accNumber = Convert.ToInt64(Console.WriteLine());
            //if(accNumber == 1)   //check for validity.
            //{
            //    Console.WriteLine("Enter amount to deposit: ");
            //    amount = Convert.ToInt32(Console.WriteLine);
            //    if(amount>0)
            //    {
            //          //update current object's available balance.
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid amount to deposit.");
            //        return;
            //    }
            //}
        }
        public void withdrawAmount()
        {
            //withdraws money and updates account details.
            //int amount = 0;
            //Console.WriteLine("Enter amount to withdraw: ");
            //amount = Convert.ToInt32(Console.ReadLine());
            //if(amount>0)
            //{
            //    //do
            //}
            //else if (amount > Account.TRANS_LIMIT)
            //{
            //    Console.WriteLine("Couldn't perform transaction. Transaction limit reached.\n")
            //}
            //else if(amount>Account.DAY_LIMIT)
            //{
            //    Console.WriteLine("Couldn't perform transaction. Daylimit reached!\n");
            //    return;
            //}
            
        }
        public void transferAmount()
        {
            //transfers money from one accc to another
            //double senderAcc;
            //double receiverAcc;
            //int amount;
            //Console.WriteLine("Enter Sender's account number: ");
            //senderAcc = Convert.ToInt64(Console.ReadLine());
            //Console.WriteLine("Enter Receiver's account number: ");
            //receiverAcc = Convert.ToInt64(Console.ReadLine());
            //if(senderAcc ==1 && receiverAcc ==1)//check whether both of the accounts exist.
            //{
            //    Console.WriteLine("Enter amount to Transfer: ");

            //}

        }
        public void printTransactionHistory()
        {
            //prints user's acount history
        }
    }
}
