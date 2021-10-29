using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public static class SessionContent
    {
        public static Bank Bank { get; set; }
        public static Account Account { get; set; }
        public static Employee Employee { get; set; }
    }
}
