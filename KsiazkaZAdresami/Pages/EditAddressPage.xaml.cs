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
    /// Logika interakcji dla klasy EditAddressPage.xaml
    /// </summary>
    public partial class EditAddressPage : Window
    {
        private int id;
        private string Imie, Nazwisko, Telefon, Email;

        public EditAddressPage(int id, string Name, string Surname, string Phone, string Mail)
        {
            InitializeComponent();
            this.id = id;
            imie.Text = Imie = Name;
            nazwisko.Text = Nazwisko = Surname;
            telefon.Text = Telefon = Phone;
            email.Text = Email = Mail;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            AddressData addres = new();
            addres.Id = id;
            addres.imie = imie.Text;
            addres.nazwisko = nazwisko.Text;
            addres.telefon = telefon.Text;
            addres.email = email.Text;

            Database.UpdateData(addres);

            MainWindow main = new();
            main.Show();
            this.Close();
        }

        private void CancelEditData(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();

            this.Close();
        }
    }
}
