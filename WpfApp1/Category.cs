using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace WpfApp1
{
    public class Category
    {
        public Guid id { get; set; }
        public string cat_img { get; set; }
        public string name { get; set; }
        public string count { get; set; }
        public List<Product> product { get; set; }
    }
}
