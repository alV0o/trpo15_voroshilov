using Microsoft.IdentityModel.Tokens;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using trpo15_primer.Models;

using trpo15_primer.Service;

namespace trpo15_primer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Trpo15PrimerVoroshilovContext db = DBService.Instance.Context;
        public ObservableCollection<Form> forms { get; set; } = new();
        public ICollectionView formsView { get; set; }
        public string searchQuery { get; set; } = null!;
        public MainWindow()
        {
            formsView = CollectionViewSource.GetDefaultView(forms);
            formsView.Filter = FilterForms;
            InitializeComponent();
        }
        public void LoadList(object sender, EventArgs e)
        {
            forms.Clear();
            foreach (var form in db.Forms.ToList())
                forms.Add(form);
        }
        public string filterHeightFrom { get; set; } = null!;
        public string filterHeightTo { get; set; } = null!;
        public bool FilterForms(object obj)
        {
            if (obj is not Form)
                return false;
            var form = (Form)obj;
            if (searchQuery != null && !form.Name.Contains(searchQuery,
            StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!filterHeightFrom.IsNullOrEmpty() && Convert.ToInt32(filterHeightFrom) > form.Height)
                return false;
            if (!filterHeightTo.IsNullOrEmpty() && Convert.ToInt32(filterHeightTo) < form.Height)
                return false;

            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            formsView.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            formsView.SortDescriptions.Clear();
            var cb = (ComboBox)sender;
            var selected = (ComboBoxItem)cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Age":
                    formsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
                    break;
                case "Weight":
                    formsView.SortDescriptions.Add(new SortDescription("Weight", ListSortDirection.Ascending));
                    break;
                case "Height":
                    formsView.SortDescriptions.Add(new SortDescription("Height", ListSortDirection.Ascending));
                    break;
            }
            formsView.Refresh();
        }
    }
}