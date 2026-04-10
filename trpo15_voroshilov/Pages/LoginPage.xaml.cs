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

namespace trpo15_voroshilov.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public string pinCode { get; set; } = null!;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void To_ManagerPage(object sender, RoutedEventArgs e)
        {
            if (pinCode == "1234")
            {
                NavigationService.Navigate(new ClientPage(true));
                MessageBox.Show("Для взаимодействия с товарами нажмите по ним ПКМ");
                Window window = Application.Current.MainWindow;
                window.Title = "Каталог (менеджер)";
            }
            else
            {
                MessageBox.Show("Неверный пинкод!");
            }
        }

        private void To_ClientPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage(false));
            Window window = Application.Current.MainWindow;
            window.Title = "Каталог";
        }
    }
}
