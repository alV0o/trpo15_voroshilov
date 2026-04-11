using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class ManagerPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        CategoryService categoryService = new();
        TagService tagService = new();
        BrandService brandService = new();


        private object _collection;
        public object collection 
        { 
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        private object _selected;
        public object selected 
        { 
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _type = null!;
        public string type 
        { 
            get => _type;
            set => SetProperty(ref _type, value);
        }
        private string _name = null!;
        public string name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICollectionView managerView { get; set; }
        public ManagerPage(object obj)
        {
            if (obj is Tag)
            {
                collection = tagService.Tags;
                selected = new Tag();
                type = "Теги";
            }
            else if (obj is Category)
            {
                collection = categoryService.Categories;
                selected = new Category();
                type = "Категории";
            }
            else if (obj is Brand)
            {
                collection = brandService.Brands;
                selected = new Brand();
                type = "Бренды";
            }
            managerView = CollectionViewSource.GetDefaultView(collection);
            InitializeComponent();
            DataContext = this;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            Window window = Application.Current.MainWindow;
            window.Title = "Каталог (менеджер)";
        }

        private void newItem_Click(object sender, RoutedEventArgs e)
        {
            if (selected is Tag)
            {
                selected = new Tag();
                Tag temp = (Tag)selected;
                name = temp.Name;
            }
            else if (selected is Category)
            {
                selected = new Category();
                Category temp = (Category)selected;
                name = temp.Name;
            }
            else if (selected is Brand)
            {
                selected = new Brand();
                Brand temp = (Brand)selected;
                name = temp.Name;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(nameBox))
            {
                MessageBox.Show("Введите название!");
                return;
            }
            if (selected is Tag)
            {
                var tagNames = tagService.Tags.Where(x => x.Name == name).ToList();
                if(tagNames.Count > 0)
                {
                    MessageBox.Show("Такой тег уже существует!");
                    return;
                }
                Tag temp = (Tag)selected;
                if (temp.Id != 0)
                {
                    selected = null;
                    temp.Name = name;
                    selected = temp;
                    tagService.Commit();
                }
                else
                {
                    temp.Name = name;
                    selected = temp;
                    tagService.Add((Tag)selected);
                }
                selected = new Tag();
            }
            else if (selected is Category)
            {
                var categoryNames = categoryService.Categories.Where(x => x.Name == name).ToList();
                if (categoryNames.Count > 0)
                {
                    MessageBox.Show("Такая категория уже существует!");
                    return;
                }
                Category temp = (Category)selected;
                if (temp.Id != 0)
                {

                    selected = null;
                    temp.Name = name;
                    selected = temp;
                    categoryService.Commit();
                }
                else
                {
                    temp.Name = name;
                    selected = temp;
                    categoryService.Add((Category)selected);
                }
                selected = new Category();
            }
            else if (selected is Brand)
            {
                var brandNames = brandService.Brands.Where(x => x.Name == name).ToList();
                if (brandNames.Count > 0)
                {
                    MessageBox.Show("Такой бренд уже существует!");
                    return;
                }

                Brand temp = (Brand)selected;
                if (temp.Id != 0)
                {
                    selected = null;
                    temp.Name = name;
                    selected = temp;
                    brandService.Commit();
                }
                else
                {
                    temp.Name = name;
                    selected = temp;
                    brandService.Add((Brand)selected);
                }
                selected = new Brand();
            }
            name = null!;
            managerView.Refresh();
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selected is Tag)
            {
                Tag temp = (Tag)selected;
                name = temp.Name;
            }
            else if ( selected is Category)
            {
                Category temp = (Category)selected;
                name = temp.Name;
            }
            else if (selected is Brand)
            {
                Brand temp = (Brand)selected;
                name = temp.Name;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selected is Tag)
            {
                Tag temp = (Tag)selected;
                name = temp.Name;
            }
            else if(selected is Brand)
            {
                Brand temp = (Brand)selected;
                name = temp.Name;
            }
            else if (selected is Category)
            {
                Category temp = (Category)selected;
                name = temp.Name;
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

            if (selected is Tag)
            {
                Tag temp = (Tag)selected;
                if (temp.Id == 0)
                {
                    MessageBox.Show("Выберите тег");
                    return;
                }
                if (MessageBox.Show($"Вы уверены что хотите удалить тег - {temp.Name}?", "Удаление", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    return;
                }
                tagService.Remove((Tag)selected);
                selected = new Tag();
            }
            else if (selected is Brand)
            {
                Brand temp = (Brand)selected;
                if (temp.Id == 0)
                {
                    MessageBox.Show("Выберите бренд");
                    return;
                }
                int count = brandService.Brands.Where(x => x.Name == temp.Name).ToArray()[0].Products.Count();
                if (MessageBox.Show($"Бренд - \"{temp.Name}\" хранит {count} товаров. Вы уверены что хотите удалить бренд - \"{temp.Name}\"?", "Удаление", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    return;
                }
                brandService.Remove((Brand)selected);
                selected = new Brand();
            }
            else if (selected is Category)
            {
                Category temp = (Category)selected;
                if (temp.Id == 0)
                {
                    MessageBox.Show("Выберите категорию");
                    return;
                }
                int count = categoryService.Categories.Where(x => x.Name == temp.Name).ToArray()[0].Products.Count();
                if (MessageBox.Show($"Категория - \"{temp.Name}\" хранит {count} товаров. Вы уверены что хотите удалить категорию - \"{temp.Name}\"?", "Удаление", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    return;
                }
                categoryService.Remove((Category)selected);
                selected = new Category();
            }
            name = null!;

        }
    }
}
