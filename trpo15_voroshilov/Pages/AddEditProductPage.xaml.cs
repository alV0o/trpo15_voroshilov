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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private Product resetProduct = new();
        public Product Product { get; set; }
        public ObservableCollection<Brand> Brands { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<Tag> Tags { get; set; } = new();
        public AddEditProductPage(Product _product)
        {
            InitializeComponent();
            if (_product != null)
            {
                SetProduct(resetProduct, _product);
                Product = _product;
            }
            else Product = new();

            Brands = new BrandService().Brands;
            Categories = new CategoryService().Categories;
            Tags = new TagService().Tags;

            foreach(var tag in Product.Tags)
            {
                if (Tags.Contains(tag)) Tags.Remove(tag);
            }

            DataContext = this;
        }

        private string GetDataProduct(Product _product)
        {
            if (string.IsNullOrEmpty(_product.Name) ||
                string.IsNullOrEmpty(_product.Description) ||
                string.IsNullOrEmpty(_product.Price.ToString()) ||
                string.IsNullOrEmpty(_product.Rating.ToString()) ||
                _product.Brand == null ||
                _product.Category == null ||
                string.IsNullOrEmpty(_product.Stock.ToString()))
            {
                return string.Empty;
            }


            List<string> data = new() {
                _product.Name,
                _product.Description,
                _product.Price.ToString(),
                _product.Rating.ToString(),
                _product.Brand.Name,
                _product.Category.Name,
                _product.Stock.ToString()
            };
            foreach(var tag in _product.Tags)
            {
                data.Add(tag.Name);
            }

            return string.Join(" ", data);
        }

        private void SetProduct(Product toProduct, Product senderProduct)
        {
            toProduct.Name = senderProduct.Name;
            toProduct.Description = senderProduct.Description;
            toProduct.Category = senderProduct.Category;
            toProduct.Rating = senderProduct.Rating;
            toProduct.Tags.Clear();
            foreach(var tag in senderProduct.Tags)
            {
                toProduct.Tags.Add(tag);
            }
            toProduct.Price = senderProduct.Price;
            toProduct.Brand = senderProduct.Brand;
            toProduct.CreatedAt = senderProduct.CreatedAt;
            toProduct.Stock = senderProduct.Stock;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GetDataProduct(resetProduct) != GetDataProduct(Product))
            {
                Save_Click(sender, e);
                SetProduct(Product, resetProduct);
            }
            NavigationService.GoBack();
            Window window = Application.Current.MainWindow;
            window.Title = "Каталог (менеджер)";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            if (Validation.GetHasError(priceBox) ||
                Validation.GetHasError(nameBox) ||
                Validation.GetHasError(descBox) ||
                Validation.GetHasError(stockBox) ||
                Validation.GetHasError(ratingBox) ||
                Product.Brand == null ||
                Product.Category == null)
            {
                MessageBox.Show("Введите корректные данные!");
                return;
            }

            if (Product.Id == 0)
            {
                Product.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
                new ProductService().Add(Product);
            }
            else
            {
                new ProductService().Commit();
            }

            SetProduct(resetProduct, Product);
            MessageBox.Show("Сохранено");
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            SetProduct(Product, resetProduct);
            foreach(var tag in new TagService().Tags)
            {
                if (!Tags.Contains(tag)) Tags.Add(tag);
            }
            foreach (var tag in Product.Tags)
            {
                if (Tags.Contains(tag)) Tags.Remove(tag);
            }
        }

        private void RemoveTag_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Tags.Add((Tag)listBox.SelectedItem);
            Product.Tags.Remove((Tag)listBox.SelectedItem);
        }

        private void AddTag_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Product.Tags.Add((Tag)listBox.SelectedItem);
            Tags.Remove((Tag)listBox.SelectedItem);
        }
    }
}
