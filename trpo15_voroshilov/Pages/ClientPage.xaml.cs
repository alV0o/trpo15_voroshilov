using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using trpo15_voroshilov.Service;

namespace trpo15_voroshilov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public Trpo15VoroshilovContext db = DBService.Instance.Context;
        public ProductService service { get; set; } = new();
        public ClientPage()
        {
            InitializeComponent();
        }
    }
}
