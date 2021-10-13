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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotels _currentHotel = new Hotels();

        public AddEditPage(Hotels selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = Tours1Entities2.GetContext().Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Кол-во звезд - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.id == 0)
                Tours1Entities2.GetContext().Hotels.Add(_currentHotel);

            try
            {
                Tours1Entities2.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
