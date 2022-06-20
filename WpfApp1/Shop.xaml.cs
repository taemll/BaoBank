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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string _img;
        public static Shopp sh = new Shopp();
        private Singleton globalTBLastName = new Singleton();
        public static DB db = new DB();
        static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        public Window1()
        {
            InitializeComponent();
            sh.GetDataProduct();
        }
        private void buy(object sender, RoutedEventArgs e)
        {
            if (db.Buy(globalTBLastName.Getin().TBLastName, product_price.Text) == 1)
            {
                MessageBox.Show("Покупка прошла успегша!");
            }
            else MessageBox.Show("Не хватает средств!");
        }

        private void bascket(object sender, RoutedEventArgs e)
        {

        }

        private void img_mouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grd = new Grid();
            grd.Height = 100;
            grd.Width = 100;
            grd.Background = System.Windows.Media.Brushes.Gray;

        }

        private void more(object sender, RoutedEventArgs e)
        {
            product.Visibility = Visibility.Visible;
            MainW.Visibility = Visibility.Hidden;
            cmbck.Visibility = Visibility.Visible;
            cmbck1.Visibility = Visibility.Hidden;
            sh.ShowProductCount(name.Text);
            product_description.Text = sh.ShowProductDescription(name.Text);
            product_img.Source = (ImageSource)imageSourceConverter.ConvertFrom(sh.ShowProductImage(name.Text));
            product_name.Text = sh.ShowProductName(name.Text);
            product_price.Text = sh.ShowProductPrice(name.Text);
            /*product_description.Text = sh.ShowProductDescription(smartphones_name.Text);
            product_img.Source = (ImageSource)imageSourceConverter.ConvertFrom(sh.ShowProductImage(smartphones_name.Text));
            product_name.Text = sh.ShowProductName(smartphones_name.Text);
            product_price.Text = sh.ShowProductPrice(smartphones_name.Text);
            
            product_description.Text = sh.ShowProductDescription(smartphones_name2.Text);
            product_img.Source = (ImageSource)imageSourceConverter.ConvertFrom(sh.ShowProductImage(smartphones_name2.Text));
            product_name.Text = sh.ShowProductName(smartphones_name2.Text);
            product_price.Text = sh.ShowProductPrice(smartphones_name2.Text);*/
        }
        private void more_1(object sender, RoutedEventArgs e)
        {
            product.Visibility = Visibility.Visible;
            MainW.Visibility = Visibility.Hidden;
            cmbck.Visibility = Visibility.Visible;
            cmbck1.Visibility = Visibility.Hidden;
            product_description.Text = sh.ShowProductDescription(smartphones_name.Text);
            product_img.Source = (ImageSource)imageSourceConverter.ConvertFrom(sh.ShowProductImage(smartphones_name.Text));
            product_name.Text = sh.ShowProductName(smartphones_name.Text);
            product_price.Text = sh.ShowProductPrice(smartphones_name.Text);
        }
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            add_product.Visibility = Visibility.Visible;
            MainW.Visibility = Visibility.Hidden;
        }
        private void add_save(object sender, RoutedEventArgs e)
        {
            sh.AddProduct(add_product_category.Text, add_product_name.Text, add_product_description.Text, add_product_price.Text, add_product_count.Text, _img);
            MessageBox.Show("Товар добавлен");
        }
        private void product_change_img(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.SaveFileDialog ol = new System.Windows.Forms.SaveFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ol.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            //Проверяем на то, что пользователь выбрал картинку и нажал ОК
            if (ol.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //СОхраняю в стринговой переменной путь к файлу
                string image = ol.FileName;
                //Указываю на картинке путь к файлу через преобразование в битмап
                add_product_img.Source = new BitmapImage(new Uri(image));
                //Перевожу путь к файлу к стрингу
                _img = add_product_img.Source.ToString();
            }
        }
        private void bt_cmbck(object sender, RoutedEventArgs e)
        {
            product.Visibility = Visibility.Hidden;
            MainW.Visibility = Visibility.Visible;
            cmbck.Visibility = Visibility.Hidden;
        }

        private void bt_phones(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Visible;
            smartphones.Visibility = Visibility.Visible;
            tech.Visibility = Visibility.Hidden;
            clothes.Visibility = Visibility.Hidden;
            shoes.Visibility = Visibility.Hidden;
            sport.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Hidden;
        }

        private void bt_tech(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Visible;
            tech.Visibility = Visibility.Visible;
            smartphones.Visibility = Visibility.Hidden;
            clothes.Visibility = Visibility.Hidden;
            shoes.Visibility = Visibility.Hidden;
            sport.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Hidden;
        }

        private void bt_clothes(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Visible;
            clothes.Visibility = Visibility.Visible;
            smartphones.Visibility = Visibility.Hidden;
            tech.Visibility = Visibility.Hidden;
            shoes.Visibility = Visibility.Hidden;
            sport.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Hidden;
        }

        private void bt_shoes(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Visible;
            shoes.Visibility = Visibility.Visible;
            smartphones.Visibility = Visibility.Hidden;
            tech.Visibility = Visibility.Hidden;
            clothes.Visibility = Visibility.Hidden;
            sport.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Hidden;
        }

        private void bt_sport(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Visible;
            sport.Visibility = Visibility.Visible;
            smartphones.Visibility = Visibility.Hidden;
            tech.Visibility = Visibility.Hidden;
            clothes.Visibility = Visibility.Hidden;
            shoes.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Hidden;
        }

        private void bt_cmbck1(object sender, RoutedEventArgs e)
        {
            cmbck1.Visibility = Visibility.Hidden;
            sport.Visibility = Visibility.Hidden;
            smartphones.Visibility = Visibility.Hidden;
            tech.Visibility = Visibility.Hidden;
            clothes.Visibility = Visibility.Hidden;
            shoes.Visibility = Visibility.Hidden;
            gr_menu.Visibility = Visibility.Visible;
        }

        private void bt_ccmmbb(object sender, RoutedEventArgs e)
        {
            ccmmbb.Visibility = Visibility.Hidden;
            MainW.Visibility = Visibility.Visible;
            add_product.Visibility = Visibility.Hidden;
        }
    }
}
