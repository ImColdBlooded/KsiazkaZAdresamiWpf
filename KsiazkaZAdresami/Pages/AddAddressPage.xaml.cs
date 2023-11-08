using KsiazkaZAdresami.Classes;
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
using System.Windows.Shapes;

namespace KsiazkaZAdresami.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AddAddressPage.xaml
    /// </summary>
    public partial class AddAddressPage : Window
    {
        public AddAddressPage()
        {
            InitializeComponent();
        }

        private void AddToDatabase(object sender, RoutedEventArgs e)
        {
            AddressData addressData = new AddressData();
            addressData.nazwisko = nazwisko.Text;
            addressData.imie = imie.Text;
            addressData.telefon = telefon.Text;
            addressData.email = email.Text;

            Database.CreateData(addressData);
            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void CancelAddToDatabase(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void RemoveDataFromInputs(object sender, RoutedEventArgs e)
        {
            nazwisko.Text = string.Empty;
            imie.Text = string.Empty;
            telefon.Text = string.Empty;
            email.Text = string.Empty;
        }
    }
}
