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
    public class DB
    {
        //------------------------------
        //Основная работа с БД
        //------------------------------
        //Совершение покукпи(Списание средств)
        public int Buy(string _lgn, string _price)
        {
            int _sum = int.Parse(_price);
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_lgn));
                if (Bao.balance >= _sum)
                {
                    Bao.balance -= _sum;
                    col.Update(Bao);
                    return check = 1;
                }
                return check;
            }
        }
        public void Test()
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    Console.WriteLine(c._id + "||" + c.email + "\t" + "||"
                        + c.iin + "\t" + "||"
                        + c.name + "\t" + "||"
                        + c.first_name + "\t" + "||"
                        + c.middle_name);
                    Console.WriteLine("||" + c.phone + "||" + "\t" + c.password);
                    Console.WriteLine("||" + c.card_number + "||" + "\t" + c.iban + "\t" + c.balance
                            + "\n" + "=================================================================");

                }
            }
        }
        //Добавление данных
        public void AddDataDetail(string _email, string _iin, string _name, string _fn, string _mn, string _phn, string _psswrd)
        {
            Random random = new Random();
            int s = random.Next(1000, 9999);
            int _ibn1 = random.Next(100000000, 999999999);
            int _ibn2 = random.Next(100000000, 999999999);
            string s1 = "5169 4931 6537";
            string s2 = s.ToString();
            string s3 = s1 + ' ' + s2;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = new Customer
                {
                    email = _email,
                    iin = _iin,
                    name = _name,
                    first_name = _fn,
                    middle_name = _mn,
                    phone = _phn,
                    password = _psswrd,
                    card_number = s3,
                    iban = "KZ" + _ibn1.ToString() + _ibn2.ToString(),
                    balance = 10000,
                    user_img = "..\\ava.jpg"
                };
                col.Insert(Bao);

            }
        }
        //Добавление истории
        public void AddHistoryPerson(string _phn, string _message)
        {
            DateTime date = DateTime.Now;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_phn));

                if (Bao.history == null)
                {
                    Bao.history = new List<History>();
                }
                Bao.history.Add(new History
                {
                    time = date,
                    message = _message
                });
                col.Update(Bao);

            }
        }
        //Добавлении фотографии
        public void AddImagePerson(string _phn, string _img)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_phn));
                Bao.user_img = _img;
                col.Update(Bao);
            }
        }
        //Смена номера
        public bool ChangePhone(string _phn, string _customer_phn)
        {
            string pattern = @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$";
            while (true)
            {
                if (Regex.IsMatch(_phn, pattern, RegexOptions.IgnoreCase))
                {
                    using (var db = new LiteDatabase(@"bank.db"))
                    {
                        var col = db.GetCollection<Customer>("Customer");
                        var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                        Bao.phone = _phn;
                        col.Update(Bao);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //Смена пароля
        public int ChangePassword(string _password, string _customer_phn)
        {
            int check = 0;
            bool en = false;
            bool symbol = false;
            bool registr = false;
            bool number = false;
            for (int i = 0; i < _password.Length; i++) // перебираем символы
            {
                if (_password[i] >= 'A' && _password[i] <= 'z') en = true;
                if (_password[i] >= '0' && _password[i] <= '9') number = true;
                if (_password[i] >= 'A' && _password[i] <= 'Z') registr = true;
                if (_password[i] == '_' || _password[i] == '-' || _password[i] == '!') symbol = true;
            }
            if (!en)
                return check = 1;/*MessageBox.Show("Доступна только английская раскладка");*/
            else if (!symbol)
                return check = 2;/*MessageBox.Show("Добавьте один из следующих символов: _ - !");*/
            else if (!registr)
                return check = 3;/*MessageBox.Show("Добавьте как минимум одну букву Верхнего регистра");*/
            else if (!number)
                return check = 4;/*MessageBox.Show("Добавьте хотя бы одну цифру");*/
            if (en == true && symbol == true && registr == true && number == true)
            {
                using (var db = new LiteDatabase(@"bank.db"))
                {
                    var col = db.GetCollection<Customer>("Customer");
                    var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                    Bao.password = _password;
                    col.Update(Bao);
                    return check;
                }
            }
            else return check;

        }
        //Проверка почты на правильность
        public bool CheckEmailReg(string _email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            while (true)
            {
                if (Regex.IsMatch(_email, pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //Проверка формата телефона
        public bool CheckPhoneReg(string _phn)
        {
            string pattern = @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$";
            while (true)
            {
                if (Regex.IsMatch(_phn, pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //Поиск телефона по дб
        public int CheckPhoneInDB(string _phn)
        {
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.phone == _phn)
                    {
                        return 3;
                    }

                    if (check == 3) break;
                }
                return check;
            }
        }
        //Поверка формы регистрации на наличии уже имеющихся данных в базе
        public int CheckRegForm(string _email, string _iin, string _phn)
        {
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    Console.WriteLine(c.email + "\t"
                        + c.iin + "\t"
                        + c.name + "\t"
                        + c.first_name + "\t"
                        + c.middle_name);
                    if (c.email == _email)
                    {
                        return 1;
                    }
                    if (c.iin == _iin)
                    {
                        return 2;
                    }
                    Console.WriteLine(c.phone
                            + "\n" + "=================================================================");
                    if (c.phone == _phn)
                    {
                        return 3;
                    }

                    if (check == 1 || check == 2 || check == 3) break;
                }
                return check;
            }
        }
        //Считывание данных с БД для входа в кабинет
        public int GetDataDetail(string _lgn, string _psswrd)
        {
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    Console.WriteLine(c.email + "\t"
                        + c.iin + "\t"
                        + c.name + "\t"
                        + c.first_name + "\t"
                        + c.middle_name);
                    Console.WriteLine(c.phone + "\t" + c.password
                        + "\n" + "=================================================================");
                    if ((c.phone == _lgn && c.password == _psswrd) || (c.email == _lgn && c.password == _psswrd))
                    {
                        check = 1;
                    }
                }


            }
            return check;
        }
        //Поиск пользователя по телефону
        public string FIndPersonByPhone(string _phn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.phone == _phn)
                    {
                        return c.name + " " + c.first_name[0] + "." + c.middle_name[0] + ".";
                    }

                }
            }
            return "Пользователь не найден";
        }
        //Поиск пользователя по номеру карты
        public string FIndPersonByCard(string _card_number)
        {
            string crd;
            if (_card_number.Length == 16)
            {
                crd = _card_number.Insert(4, " ");
                _card_number = crd.Insert(9, " ");
                crd = _card_number.Insert(14, " ");
                using (var db = new LiteDatabase(@"bank.db"))
                {
                    var col = db.GetCollection<Customer>("Customer");
                    var result = col.FindAll();
                    foreach (Customer c in result)
                    {
                        if (c.card_number == crd)
                        {
                            return c.name + " " + c.first_name[0] + "." + c.middle_name[0] + ".";
                        }


                    }
                }
                return "Пользователь не найден";
            }
            else if (_card_number.Length == 19)
            {
                crd = _card_number;
                using (var db = new LiteDatabase(@"bank.db"))
                {
                    var col = db.GetCollection<Customer>("Customer");
                    var result = col.FindAll();
                    foreach (Customer c in result)
                    {
                        if (c.card_number == crd)
                        {
                            return c.name + " " + c.first_name[0] + "." + c.middle_name[0] + ".";
                        }

                    }
                }
                return "Пользователь не найден";
            }
            else return "";
        }
        //Оплата онай
        public int TransferByOnay(string _customer_phn,string sum)
        {
            int _sum = int.Parse(sum);
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                if (Bao.balance >= _sum)
                {
                    Bao.balance -= _sum;
                    col.Update(Bao);
                    return check = 1;
                }
                else return check;

            }
        }
        //Перевод по телефону
        public int TransferByPhone(string _customer_phn, string _phn, string sum)
        {
            int _sum = int.Parse(sum);
            int check = 0;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                if (Bao.balance >= _sum)
                {
                    Bao.balance -= _sum;
                    col.Update(Bao);
                    Bao = col.FindOne(x => x.phone.Equals(_phn));
                    Bao.balance += _sum;
                    col.Update(Bao);
                    return check = 1;
                }
                else return check;

            }
        }
        //Перевод по номеру карты
        public int TransferByCardNumber(string _customer_phn, string _card_number, string sum)
        {
            string crd;
            int check = 0;
            int _sum = int.Parse(sum);
            if (_card_number.Length == 16)
            {
                crd = _card_number.Insert(4, " ");
                _card_number = crd.Insert(9, " ");
                crd = _card_number.Insert(14, " ");
                using (var db = new LiteDatabase(@"bank.db"))
                {
                    var col = db.GetCollection<Customer>("Customer");
                    var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                    if (Bao.balance >= _sum)
                    {
                        //Для снятия
                        Bao.balance -= _sum;
                        col.Update(Bao);
                        //Для пополнения
                        Bao = col.FindOne(x => x.card_number.Equals(crd));
                        Bao.balance += _sum;
                        col.Update(Bao);
                        return check = 1;
                    }
                    else return check;
                }
            }
            else
            {
                crd = _card_number;
                using (var db = new LiteDatabase(@"bank.db"))
                {
                    var col = db.GetCollection<Customer>("Customer");
                    var Bao = col.FindOne(x => x.phone.Equals(_customer_phn));
                    if (Bao.balance >= _sum)
                    {
                        //Для снятия
                        Bao.balance -= _sum;
                        col.Update(Bao);
                        //Для пополнения
                        Bao = col.FindOne(x => x.card_number.Equals(crd));
                        Bao.balance += _sum;
                        col.Update(Bao);
                        return check = 1;
                    }
                    else return check;
                }
            }
        }
        //Выввод баланса
        public string ShowBalance(string _lgn)
        {
            string blnc;
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {
                        blnc = c.balance.ToString();
                        return blnc;
                    }
                }
                return null;
            }
        }
        //Выввод номера карты
        public string ShowCardNumber(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {
                        return c.card_number;
                    }
                }
                return null;
            }
        }
        //Выввод изображения
        public string ShowImagePerson(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {

                        return c.user_img;
                    }
                }
                return null;
            }
        }
        //Выввод истории
        public void ShowHistoryPerson(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.phone == _lgn || c.email == _lgn)
                    {
                        for (int i = 0; i < c.history.Count(); i++)
                        {
                            string time = c.history[i].time.ToString();
                            Console.WriteLine(c.history[i].time);
                            Console.WriteLine(c.history[i].message);
                        }
                    }
                }
            }
        }
        //------------------------------
        //Реквизиты
        //------------------------------
        //Выввод IBAN
        public string ShowIBAN(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {
                        return c.iban;
                    }
                }
                return null;
            }
        }
        //Выввод ИИН
        public string ShowIIN(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {
                        return c.iin;
                    }
                }
                return null;
            }
        }
        //Выввод ФИО
        public string ShowFIO(string _lgn)
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindAll();
                foreach (Customer c in result)
                {
                    if (c.email == _lgn || c.phone == _lgn)
                    {
                        return c.first_name + " " + c.name + " " + c.middle_name;
                    }
                }
                return null;
            }
        }
    }
}

