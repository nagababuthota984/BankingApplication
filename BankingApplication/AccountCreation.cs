using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    class AccountCreation : Account
    {
        //creates a new account
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




            if (!accounts.ContainsKey(this.accountNumber))
            {
                accounts.Add(this.accountNumber, new Dictionary<string, string>());
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
            DataReaderWriter.writeAccounts(accounts);





        }
    }
}
