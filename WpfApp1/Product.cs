﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Product
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string product_img { get; set; }
    }
}
