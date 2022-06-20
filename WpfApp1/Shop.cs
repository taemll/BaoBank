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
    public class Shopp
    {
        //Получение данных о продукте
        public void GetDataProduct()
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach (Category c in Bao)
                {
                    foreach (Product p in c.product)
                    {
                        Console.WriteLine("=======================" + "\n" +c.name + "\n" + p.name + "\n " + p.product_img + "\n" + p.price);
                    }
                }

            }
        }
        //Добавление категории(Метод вызывать вручную)
        public void AddCategory(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = new Category
                {
                    name = _name
                };
                col.Insert(Bao);
            }
        }
        //Добавление товара
        public void AddProduct(string _ctg, string _name, string _dcrpt, string _price, string _count, string _img)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindOne(x => x.name.Equals(_ctg));

                if (Bao.product == null)
                {
                    Bao.product = new List<Product>();
                }
                Bao.product.Add(new Product
                {
                    name = _name,
                    description = _dcrpt,
                    price = int.Parse(_price),
                    count = int.Parse(_count),
                    product_img = _img
                });
                col.Update(Bao);
            }

        }
        /*public void AddShopping(string _lgn, string _name, string _price)
        {
            DateTime date = DateTime.Now;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_lgn));

                if (Bao.basket == null)
                {
                    Bao.basket = new List<Shopping>();
                }
                Bao.basket.Add(new Shopping
                {
                    time = date.ToString(),
                    name = _name,
                    price = _price
                });
                col.Update(Bao);

            }
        }*/
        //Вывод Названия товара
        public string ShowProductName(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach(Category c in Bao)
                {
                    foreach(Product p in c.product)
                    {
                        if (p.name == _name) return p.name;
                    }
                }
                return "Не найден";
                
            }
        }
        //Вывод Изображения товара
        public string ShowProductImage(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach (Category c in Bao)
                {
                    foreach (Product p in c.product)
                    {
                        if (p.name == _name) return p.product_img;
                    }
                }
                return "Не найден";

            }
        }
        //Вывод Описания товара
        public string ShowProductDescription(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach (Category c in Bao)
                {
                    foreach (Product p in c.product)
                    {
                        if (p.name == _name) return p.description;
                    }
                }
                return "Не найден";

            }
        }
        //Вывод Цены товара
        public string ShowProductPrice(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach (Category c in Bao)
                {
                    foreach (Product p in c.product)
                    {
                        if (p.name == _name) return p.price.ToString();
                    }
                }
                return "Не найден";

            }
        }
        //Вывод Количество товара
        public string ShowProductCount(string _name)
        {
            using (var db = new LiteDatabase(@"shop.db"))
            {
                var col = db.GetCollection<Category>("Category");
                var Bao = col.FindAll();
                foreach (Category c in Bao)
                {
                    foreach (Product p in c.product)
                    {
                        if (p.name == _name) return p.count.ToString();
                    }
                }
                return "Не найден";

            }
        }
    }
}
        

