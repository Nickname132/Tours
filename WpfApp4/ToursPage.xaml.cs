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

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();

            var allTypes = ToursBaseEntities.GetContext().Type.ToList();
            allTypes.Insert(0, new Type { name = "Все типа" });
            ComboType.ItemsSource = allTypes;

            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            UpdateTours();

            //var currentTours = ToursBaseEntities.GetContext().Tour.ToList();
            //LViewTours.ItemsSource = currentTours;
        }

        private void UpdateTours()
        {
            var currentTours = ToursBaseEntities.GetContext().Tour.ToList();

            if(ComboType.SelectedIndex > 0)
            {
                currentTours = currentTours.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();
            }
            currentTours = currentTours.Where(p => p.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            if (CheckActual.IsChecked.Value)
            {
                currentTours = currentTours.Where(p => p.isActual).ToList();
            }
            LViewTours.ItemsSource = currentTours.OrderBy(p => p.ticketCount).ToList();

        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
