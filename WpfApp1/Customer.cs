using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.IO;

namespace WpfApp1
{
    public class Customer
    {
        public Guid _id { get; set; }
        public string email { get; set; }
        public string iin { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string card_number { get; set; }
        public string iban { get; set; }
        public int balance { get; set; }
        public string user_img { get; set; }
        public List<History> history { get; set; }
    }
}
