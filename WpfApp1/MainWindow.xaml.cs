using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private Singleton globalTBLastName = new Singleton();
        public string name;
        public static DB db = new DB();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Write_login(object sender, RoutedEventArgs e)
        {

        }
        private void OPen(object sender, RoutedEventArgs e)
        {
            if (db.GetDataDetail(Login.Text, Passwordd.Password) == 1)
            {
                db.ShowHistoryPerson(Login.Text);
                globalTBLastName.Getin().TBLastName = Login.Text;
                TaskWindow taskWindow = new TaskWindow();
                taskWindow.Show();
                this.Close();
                db.Test();
            }
            else MessageBox.Show("Вы ввели неверный логин или пароль");

        }
        private void bClick_RegForm(object sender, RoutedEventArgs e)
        {
            LogIn.Visibility = Visibility.Hidden;
            RegistrationForm.Visibility = Visibility.Visible;
            //db.Test();
        }
        private void TestClick(object sender, RoutedEventArgs e)
        {
        }
        private void bClick_Reg(object sender, RoutedEventArgs e)
        {
            bool en = false;
            bool symbol = false;
            bool registr = false;
            bool number = false;
            if (db.CheckRegForm(Email.Text, Iin.Text, PhoneNumber.Text) == 1)
                MessageBox.Show("Пользователь с данным почтовым адресом уже зарегистрирован!");
            else if (db.CheckRegForm(Email.Text, Iin.Text, PhoneNumber.Text) == 2)
                MessageBox.Show("Пользователь с данным ИИН уже зарегистрирован!");
            else if (db.CheckRegForm(Email.Text, Iin.Text, PhoneNumber.Text) == 3)
                MessageBox.Show("Пользователь с данным номером телефона уже зарегистрирован!");
            else
            {
                if (db.CheckEmailReg(Email.Text) == true)
                {
                    if (db.CheckPhoneReg(PhoneNumber.Text) == true)
                    {
                        if (Iin.Text.Length == 12)
                        {
                            if (Password.Password.Length >= 2)
                            {
                                for (int i = 0; i < Password.Password.Length; i++) // перебираем символы
                                {
                                    if (Password.Password[i] >= 'A' && Password.Password[i] <= 'z') en = true;
                                    if (Password.Password[i] >= '0' && Password.Password[i] <= '9') number = true;
                                    if (Password.Password[i] >= 'A' && Password.Password[i] <= 'Z') registr = true;
                                    if (Password.Password[i] == '_' || Password.Password[i] == '-' || Password.Password[i] == '!') symbol = true;
                                }
                                if (!en)
                                    MessageBox.Show("Доступна только английская раскладка");
                                else if (!symbol)
                                    MessageBox.Show("Добавьте один из следующих символов: _ - !");
                                else if (!registr)
                                    MessageBox.Show("Добавьте как минимум одну букву Верхнего регистра");
                                else if (!number)
                                    MessageBox.Show("Добавьте хотя бы одну цифру");
                                if (en == true && symbol == true && registr == true && number == true)
                                {
                                    db.AddDataDetail(Email.Text, Iin.Text, Name.Text, Lname.Text, Mname.Text, PhoneNumber.Text, Password.Password);
                                    MessageBox.Show("Вы зарегистрировались!");
                                    LogIn.Visibility = Visibility.Visible;
                                    RegistrationForm.Visibility = Visibility.Hidden;
                                    db.AddHistoryPerson(PhoneNumber.Text, "ПОздравляем, вы являетесь новым пользоваталем нашего банка." + "\n" +
                                        " В качестве бонуса вам на счет зачислено 10 000тг");
                                    db.ShowHistoryPerson(PhoneNumber.Text);
                                }
                            }
                            else MessageBox.Show("Пароль должен быть больше 8 символов");
                        }
                        else MessageBox.Show("Вы ввели неккоректный ИИН");
                    }
                    else MessageBox.Show("Вы ввели неккоректный номер телефона");
                }
                else MessageBox.Show("Вы ввели неккоректный почтовый адрес");
            }
        }
        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
        private void Comebbb(object sender, RoutedEventArgs e)
        {
            RegistrationForm.Visibility = Visibility.Hidden;
            LogIn.Visibility = Visibility.Visible;
        }

    }

}
