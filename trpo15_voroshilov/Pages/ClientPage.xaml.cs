using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ICollectionView productsView { get; set; } 
        public string searchQuery { get; set; } = null!;
        public CategoryService categoryService { get; set; } = new();
        List<string> categoryNames = new();
        public BrandService brandService { get; set; } = new();
        List<string> brandNames = new();
        public int startSum { get; set; } = 0;
        public int endSum { get; set; } = 0;

        public bool isManager { get; set; } = false;
        public ClientPage(bool _isManager)
        {
            isManager = _isManager;
            productsView = CollectionViewSource.GetDefaultView(service.Products);
            productsView.Filter = FilterProducts;
            InitializeComponent();
        }

        public bool FilterProducts(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product)obj;
            if (searchQuery != null && !product.Name.Contains(searchQuery,
            StringComparison.CurrentCultureIgnoreCase))
                return false;

            bool isPresentedCategory = false;
            foreach(string category in categoryNames)
            {
                if (product.Category.Name.Contains(category, StringComparison.CurrentCultureIgnoreCase))
                {
                    isPresentedCategory = true;
                    break;
                }
            }

            if(!isPresentedCategory && categoryNames.Count>0)
                return false;

            bool isPresentedBrand = false;
            foreach (string brand in brandNames)
            {
                if (product.Brand.Name.Contains(brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    isPresentedBrand = true;
                    break;
                }
            }

            if (!isPresentedBrand && brandNames.Count > 0)
                return false;

            if (startSum > 0 && startSum > product.Price)
                return false;
            
            if (endSum > 0 && endSum < product.Price)
                return false;

            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            productsView.Refresh();
        }

        private void categoryCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == true)
            {
                if (!categoryNames.Contains(cb.Content)) categoryNames.Add(cb.Content.ToString());
            }
            else
            {
                if (categoryNames.Contains(cb.Content)) categoryNames.Remove(cb.Content.ToString());
            }
            productsView.Refresh();
        }

        private void categoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                categoryNames.Clear();
            }
            productsView.Refresh();
        }

        private void brandCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == true)
            {
                if (!brandNames.Contains(cb.Content)) brandNames.Add(cb.Content.ToString());
            }
            else
            {
                if (brandNames.Contains(cb.Content)) brandNames.Remove(cb.Content.ToString());
            }
            productsView.Refresh();
        }

        private void brandBtn_Click(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                brandNames.Clear();
            }
            productsView.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsView.SortDescriptions.Clear();
            var cb = (ComboBox)sender;
            var selected = (ComboBoxItem)cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Name":
                    productsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    break;
                case "LowPrice":
                    productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                    break;
                case "HighPrice":
                    productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                    break;
                case "LowCount":
                    productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Descending));
                    break;
                case "HighCount":
                    productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Ascending));
                    break;
            }
            productsView.Refresh();
        }

        private void categoryManager_Click(object sender, RoutedEventArgs e)
        {
            Category c = new();
            NavigationService.Navigate(new ManagerPage(c));
        }

        private void tagManager_Click(object sender, RoutedEventArgs e)
        {
            Tag t = new();
            NavigationService.Navigate(new ManagerPage(t));
        }

        private void brandManager_Click(object sender, RoutedEventArgs e)
        {
            Brand b = new();
            NavigationService.Navigate(new ManagerPage(b));
        }
    }
}
