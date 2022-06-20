using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;
using LiteDB;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        DispatcherTimer time = new DispatcherTimer();
        private Singleton globalTBLastName = new Singleton();
        public static DB db = new DB();
        int i = 0;
        public TaskWindow()
        {
            InitializeComponent();
            time.Interval = TimeSpan.FromMinutes(0.0010);
            time.Tick += Loop;
            time.Start();
            db.ShowHistoryPerson(globalTBLastName.Getin().TBLastName);
        }

        private void ShowHistory()
        {
            using (var db = new LiteDatabase(@"bank.db"))
            {
                var col = db.GetCollection<Customer>("Customer");
                var result = col.FindOne(x => x.phone.Equals(globalTBLastName.Getin().TBLastName));
                while (i < result.history.Count())
                {
                    TextBlock hstr = new TextBlock() { Name = "txt", Text = result.history[i].time.ToString() + "\n" + result.history[i].message };
                    hstr.Margin = new Thickness(20, 10, 20, 10);
                    hstr.MinHeight = 30;
                    hstr.FontSize = 16;
                    hstr.MaxWidth = 500;
                    hstr.TextWrapping = TextWrapping.WrapWithOverflow;
                    hstr.Padding = new Thickness(5);
                    hstr.HorizontalAlignment = HorizontalAlignment.Left;
                    stackP2.Children.Add(hstr);
                    stackP2.Children.Add(new Separator());
                    i++;
                }
            }
        }
        private void Loop(object sender, EventArgs e)
        {
            Transfer_visible_balance.Text = db.ShowBalance(globalTBLastName.Getin().TBLastName) + "тг";
            MyBank_Card.Text = db.ShowCardNumber(globalTBLastName.Getin().TBLastName);
            MyBank_Balance.Text = db.ShowBalance(globalTBLastName.Getin().TBLastName) + "тг";
            rec_iin.Text = db.ShowIIN(globalTBLastName.Getin().TBLastName);
            rec_klient.Text = db.ShowFIO(globalTBLastName.Getin().TBLastName);
            rec_num.Text = db.ShowIBAN(globalTBLastName.Getin().TBLastName);
            profile_name.Text = db.ShowFIO(globalTBLastName.Getin().TBLastName);
            profile_num.Text = globalTBLastName.Getin().TBLastName;
            transport_plt_balance.Text = "Баланс:" + db.ShowBalance(globalTBLastName.Getin().TBLastName) + "тг";
            //photo.ImageSource = (ImageSource)imageSourceConverter.ConvertFrom(db.ShowImagePerson(globalTBLastName.Getin().TBLastName));
            Card_perevod_show_sum.Text = db.ShowBalance(globalTBLastName.Getin().TBLastName) + "тг";
            Transfer_Results.Text = db.FIndPersonByPhone(Transfer_Phone.Text);
            Card_perevod_ShowName.Text = db.FIndPersonByCard(Card_perevod_num.Text);
            ShowHistory();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            MyBank.IsSelected = true;
            e.Handled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            Pay.IsSelected = true;
            e.Handled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            Transaction.IsSelected = true;
            e.Handled = true;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            Mail.IsSelected = true;
            e.Handled = true;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            History.IsSelected = true;
            e.Handled = true;
        }


        private void cb_mb(object sender, RoutedEventArgs e)
        {
            mybank_page.Visibility = Visibility.Visible;
            recvisits.Visibility = Visibility.Hidden;
        }


        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
        private void but_rcvst(object sender, RoutedEventArgs e)
        {
            mybank_page.Visibility = Visibility.Hidden;
            recvisits.Visibility = Visibility.Visible;
        }
        private void Bus(object sender, RoutedEventArgs e)
        {
            transport_plt.Visibility = Visibility.Visible;
            platM.Visibility = Visibility.Hidden;
        }

        private void Comeb(object sender, RoutedEventArgs e)
        {
            Tabb.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Hidden;
            Contacts.Visibility = Visibility.Hidden;
            profilepp.Visibility = Visibility.Hidden;
        }

        private void my_prfl(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Tabb.Visibility = Visibility.Hidden;
            Contacts.Visibility = Visibility.Hidden;
            arrow.Visibility = Visibility.Visible;
            profilepp.Visibility = Visibility.Visible;
        }

        private void _myprf_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void contacts(object sender, RoutedEventArgs e)
        {
            Tabb.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Hidden;
            arrow.Visibility = Visibility.Visible;
            Contacts.Visibility = Visibility.Visible;
            profilepp.Visibility = Visibility.Hidden;

        }

        /*private void settings(object sender, RoutedEventArgs e)
        {
            Tabb.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Visible;
            arrow.Visibility = Visibility.Visible;
            Contacts.Visibility = Visibility.Hidden;
            profilepp.Visibility = Visibility.Hidden;


        }*/

        private void edit(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.SaveFileDialog ol = new System.Windows.Forms.SaveFileDialog();
            op.Title = "Select a picture";
            op.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ol.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            //Проверяем на то, что пользователь выбрал картинку и нажал ОК
            if (ol.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //СОхраняю в стринговой переменной путь к файлу
                string image = ol.FileName;
                //Указываю на картинке путь к файлу через преобразование в битмап
                photo.ImageSource = new BitmapImage(new Uri(image));
                //Перевожу путь к файлу к стрингу
                string _img = photo.ImageSource.ToString();
                //Метод сохраняющий картинку в базе
                db.AddImagePerson(globalTBLastName.Getin().TBLastName, _img);
                db.AddHistoryPerson(globalTBLastName.Getin().TBLastName, "Вы успешно сменили фотографию профиля!");
                ShowHistory();
            }
        }

        private void mss(object sender, RoutedEventArgs e)
        {

        }

        private void klient(object sender, RoutedEventArgs e)
        {
            perevod.Visibility = Visibility.Hidden;
            klient_perevod.Visibility = Visibility.Visible;
            cb_per.Visibility = Visibility.Visible;
        }
        private void Transfer_Phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Transfer_Results.Text = db.FIndPersonByPhone(Transfer_Phone.Text);
                photo_prr.ImageSource = (ImageSource)imageSourceConverter.ConvertFrom(db.ShowImagePerson(Transfer_Phone.Text));
            }
        }

        private void Card_perevod_num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Card_perevod_ShowName.Text = db.FIndPersonByCard(Card_perevod_num.Text);
            }
        }

        private void another(object sender, RoutedEventArgs e)
        {
            perevod.Visibility = Visibility.Hidden;
            ano_perevod.Visibility = Visibility.Visible;
            cb_per.Visibility = Visibility.Visible;
        }

        private void Charity(object sender, RoutedEventArgs e)
        {

        }

        private void Study(object sender, RoutedEventArgs e)
        {

        }

        private void Phone(object sender, RoutedEventArgs e)
        {

        }
        private void cb_plat(object sender, RoutedEventArgs e)
        {
            transport_plt.Visibility = Visibility.Hidden;
            platM.Visibility = Visibility.Visible;

        }
        private void oplatit(object sender, RoutedEventArgs e)
        {
            if (transport_plt_num.Text.LongCount() == 19)
            {
                if (db.TransferByOnay(globalTBLastName.Getin().TBLastName, transport_plt_sum.Text) == 1)
                {
                    MessageBox.Show("Оплата прошла успешно");
                    db.AddHistoryPerson(globalTBLastName.Getin().TBLastName, "Вы пополнили карту Onay" + "\n" + transport_plt_num + "\n" + "на сумму: " + transport_plt_sum);
                }
                else MessageBox.Show("У вас недостаточно средств");
            }
            else MessageBox.Show("Вы ввели неверный формат онай");
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Card_perevesti(object sender, RoutedEventArgs e)
        {
            if (Card_perevod_sum.Text != "")
            {
                if (db.TransferByCardNumber(globalTBLastName.Getin().TBLastName, Card_perevod_num.Text, Card_perevod_sum.Text) == 1)
                {
                    db.AddHistoryPerson(globalTBLastName.Getin().TBLastName, "Перевод на сумму " + Transfer_Balance.Text + " выполнен успешно на имя: " + Card_perevod_ShowName.Text);
                    MessageBox.Show("Перевод выполнен");
                    klient_perevod.Visibility = Visibility.Hidden;
                    ano_perevod.Visibility = Visibility.Hidden;
                    perevod.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Недостаточно средств");
            }
            else MessageBox.Show("Введите сумму для перевода");
        }
        private void perevesti(object sender, RoutedEventArgs e)
        {
            if (Transfer_Balance.Text != "")
            {
                if (db.TransferByPhone(globalTBLastName.Getin().TBLastName, Transfer_Phone.Text, Transfer_Balance.Text) == 1)
                {
                    db.AddHistoryPerson(globalTBLastName.Getin().TBLastName, "Перевод на сумму " + Transfer_Balance.Text + " выполнен успешно на имя: " + Transfer_Results.Text);
                    db.AddHistoryPerson(Transfer_Phone.Text, "Вы получили перевод на сумму: " + Transfer_Balance.Text);
                    MessageBox.Show("Перевод выполнен");
                    klient_perevod.Visibility = Visibility.Hidden;
                    ano_perevod.Visibility = Visibility.Hidden;
                    perevod.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Недостаточно средств");
            }
            else MessageBox.Show("Введите сумму для перевода");
        }

        private void cb_perev(object sender, RoutedEventArgs e)
        {
            klient_perevod.Visibility = Visibility.Hidden;
            perevod.Visibility = Visibility.Visible;
            cb_per.Visibility = Visibility.Hidden;
            ano_perevod.Visibility = Visibility.Hidden;
        }

        private void edit_n(object sender, RoutedEventArgs e)
        {
            tb_change_phone.Visibility = Visibility.Visible;
            change_phone.Visibility = Visibility.Visible;
            editPsswrd.Visibility = Visibility.Hidden;
            editNum.Visibility = Visibility.Hidden;
            tb_change_password.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Hidden;
            cancel2.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Hidden;
            exxit.Visibility = Visibility.Hidden;
            lnewPhone.Visibility = Visibility.Visible;
        }

        private void edit_p(object sender, RoutedEventArgs e)
        {
            tb_change_password.Visibility = Visibility.Visible;
            change_password.Visibility = Visibility.Visible;
            editNum.Visibility = Visibility.Hidden;
            editPsswrd.Visibility = Visibility.Hidden;
            tb_change_phone.Visibility = Visibility.Hidden;
            change_phone.Visibility = Visibility.Hidden;
            cancel2.Visibility = Visibility.Hidden;
            cancel.Visibility = Visibility.Visible;
            exxit.Visibility = Visibility.Hidden;
            lnewPsswrd.Visibility = Visibility.Visible;
        }

        private void settings(object sender, RoutedEventArgs e)
        {
            settings_n.Visibility = Visibility.Visible;
            exxit.Visibility = Visibility.Visible;
            set_but.Visibility = Visibility.Hidden;
            log_out.Visibility = Visibility.Hidden;
        }

        private void comebbb(object sender, RoutedEventArgs e)
        {
            settings_n.Visibility = Visibility.Hidden;
            exxit.Visibility = Visibility.Hidden;
            set_but.Visibility = Visibility.Visible;
            log_out.Visibility = Visibility.Visible;
            change_password.Visibility = Visibility.Hidden;
            change_phone.Visibility = Visibility.Hidden;
            tb_change_password.Visibility = Visibility.Hidden;
            tb_change_phone.Visibility = Visibility.Hidden;
            cancel2.Visibility = Visibility.Hidden;
            cancel.Visibility = Visibility.Hidden;

        }
        private void question(object sender, RoutedEventArgs e)
        {
            questionList.Visibility = Visibility.Visible;
        }

        private void question1(object sender, RoutedEventArgs e)
        {
            questionList.Visibility = Visibility.Hidden;
            Label cont = new Label() { Name = "txt", Content = "Как поменять пароль или номер телефона?" };
            // cont.Background = new LinearGradientBrush(Colors.DarkGreen, Colors.SlateBlue, 90);
            cont.Margin = new Thickness(20, 10, 20, 10);
            cont.MinHeight = 30;
            cont.Padding = new Thickness(5);
            cont.FontSize = 16;

            cont.HorizontalAlignment = HorizontalAlignment.Right;
            stackP.Children.Add(cont);

            if (true)
            {
                //Thread.Sleep(2000);

                TextBlock contt = new TextBlock() { Name = "txt", Text = "Зайдите в свой профиль. Нажмите на шестеренку в правом верхнем углу. Выберите нужное Вам действие и запишите новый номер или пароль. При завершении, нажмите на галочку для сохранения." };
                contt.Margin = new Thickness(20, 10, 20, 10);
                contt.MinHeight = 30;
                contt.FontSize = 16;
                contt.MaxWidth = 500;
                contt.TextWrapping = TextWrapping.WrapWithOverflow;
                contt.Padding = new Thickness(5);
                contt.HorizontalAlignment = HorizontalAlignment.Left;
                stackP.Children.Add(contt);

            }
        }

        private void question2(object sender, RoutedEventArgs e)
        {
            questionList.Visibility = Visibility.Hidden;
            Label cont = new Label() { Name = "txt", Content = "Как с вами связаться?" };
            // cont.Background = new LinearGradientBrush(Colors.DarkGreen, Colors.SlateBlue, 90);
            cont.Margin = new Thickness(20, 10, 20, 10);
            cont.MinHeight = 30;
            cont.Padding = new Thickness(5);
            // cont.Foreground= Brushes.White;
            cont.FontSize = 16;
            cont.HorizontalAlignment = HorizontalAlignment.Right;
            stackP.Children.Add(cont);

            if (true)
            {
                //Thread.Sleep(2000);

                TextBlock contt = new TextBlock() { Name = "txt", Text = "Связаться с нами Вы можете по номеру телефона, указанный в разделе «контакты» или написать нам на почту baobank_@gmail.com." };
                contt.Margin = new Thickness(20, 10, 20, 10);
                contt.MinHeight = 30;
                contt.FontSize = 16;
                contt.MaxWidth = 500;
                contt.TextWrapping = TextWrapping.WrapWithOverflow;
                //      contt.MaxWidth = ;
                contt.Padding = new Thickness(5);
                contt.HorizontalAlignment = HorizontalAlignment.Left;
                stackP.Children.Add(contt);

            }
        }

        private void question3(object sender, RoutedEventArgs e)
        {

        }

        private void question4(object sender, RoutedEventArgs e)
        {

        }
        private void bt_change_phone(object sender, RoutedEventArgs e)
        {
            editPsswrd.Visibility = Visibility.Visible;
            editNum.Visibility = Visibility.Visible;
            cancel2.Visibility = Visibility.Hidden;
            cancel.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Hidden;
            change_phone.Visibility = Visibility.Hidden;
            tb_change_password.Visibility = Visibility.Hidden;
            tb_change_phone.Visibility = Visibility.Hidden;
            exxit.Visibility = Visibility.Visible;
            lnewPhone.Visibility = Visibility.Hidden;
            lnewPsswrd.Visibility = Visibility.Hidden;
            if (db.CheckPhoneReg(tb_change_phone.Text) == true)
            {
                db.ChangePhone(tb_change_phone.Text, globalTBLastName.Getin().TBLastName);
                globalTBLastName.Getin().TBLastName = tb_change_phone.Text;
            }
            else MessageBox.Show("Вы ввели неверный формат телефона!!!");
        }

        private void bt_change_password(object sender, RoutedEventArgs e)
        {
            editNum.Visibility = Visibility.Visible;
            editPsswrd.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Hidden;
            cancel2.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Hidden;
            change_phone.Visibility = Visibility.Hidden;
            tb_change_password.Visibility = Visibility.Hidden;
            tb_change_phone.Visibility = Visibility.Hidden;
            exxit.Visibility = Visibility.Visible;
            lnewPhone.Visibility = Visibility.Hidden;
            lnewPsswrd.Visibility = Visibility.Hidden;
            if (tb_change_password.Password.Length >= 8)
            {
                if (db.ChangePassword(tb_change_password.Password, globalTBLastName.Getin().TBLastName) == 0)
                {
                    MessageBox.Show("Пароль успешно изменен!");
                }
                else if (db.ChangePassword(tb_change_password.Password, globalTBLastName.Getin().TBLastName) == 1)
                {
                    MessageBox.Show("Доступна только английская раскладка");
                }
                else if (db.ChangePassword(tb_change_password.Password, globalTBLastName.Getin().TBLastName) == 2)
                {
                    MessageBox.Show("Добавьте один из следующих символов: _ - !");
                }
                else if (db.ChangePassword(tb_change_password.Password, globalTBLastName.Getin().TBLastName) == 3)
                {
                    MessageBox.Show("Добавьте как минимум одну букву Верхнего регистра");
                }
                else if (db.ChangePassword(tb_change_password.Password, globalTBLastName.Getin().TBLastName) == 4)
                {
                    MessageBox.Show("Добавьте хотя бы одну цифру");
                }
            }
            else MessageBox.Show("Минимальная длина пароля 8 символов");



        }

        private void cantellation(object sender, RoutedEventArgs e)
        {
            editNum.Visibility = Visibility.Visible;
            editPsswrd.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Hidden;
            cancel2.Visibility = Visibility.Hidden;
            change_password.Visibility = Visibility.Hidden;
            change_phone.Visibility = Visibility.Hidden;
            tb_change_password.Visibility = Visibility.Hidden;
            tb_change_phone.Visibility = Visibility.Hidden;
            exxit.Visibility = Visibility.Visible;
            lnewPhone.Visibility = Visibility.Hidden;
            lnewPsswrd.Visibility = Visibility.Hidden;
        }
    }
}
