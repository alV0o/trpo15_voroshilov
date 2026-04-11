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
using trpo15_voroshilov.Models;

namespace trpo15_voroshilov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewProductPage.xaml
    /// </summary>
    public partial class ViewProductPage : Page
    {
        private Product Product = new();
        public ViewProductPage(Product _product)
        {
            InitializeComponent();
            Product = _product;
            DataContext = Product;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            Window window = Application.Current.MainWindow;
            window.Title = "Каталог";
        }
    }
}
