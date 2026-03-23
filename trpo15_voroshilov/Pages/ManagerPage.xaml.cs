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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public object collection { get; set; }
        CategoryService categoryService = new();
        TagService tagService = new();
        BrandService brandService = new();
        public ManagerPage(object obj)
        {
            InitializeComponent();
            if (obj is Tag) collection = tagService.Tags;
            else if (obj is Category) collection = categoryService.Categories;
            else if (obj is Brand) collection = brandService.Brands;

            DataContext = this;
        }
    }
}
