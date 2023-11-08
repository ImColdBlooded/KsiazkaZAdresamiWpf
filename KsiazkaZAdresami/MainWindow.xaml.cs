using KsiazkaZAdresami.Classes;
using KsiazkaZAdresami.Pages;
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

namespace KsiazkaZAdresami
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<AddressData> addres;
        private int currentPage = 1;
        private int itemsPerPage = 10;

        string imie, nazwisko, telefon, email;

        public MainWindow()
        {
            InitializeComponent();
            Database.InitializeDatabase();

            addres = new ObservableCollection<AddressData>(Database.ReadData());
            int startIndex = 0;
            var pageData = addres.Skip(startIndex).Take(itemsPerPage).ToList();
            listaAdresow.ItemsSource = pageData;
            UpdateProgressBar();

        }

        private void UpdateListView()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            var pageData = addres.Skip(startIndex).Take(itemsPerPage).ToList();
            listaAdresow.ItemsSource = pageData;
            UpdateProgressBar();
        }

        private void DeleteAddres(object sender, RoutedEventArgs e)
        {
            AddressData selected = listaAdresow.SelectedItem as AddressData;

            if (selected != null)
            {   
                var result = MessageBox.Show("Czy na pewno chcesz usunąć ten adres?", "Question", MessageBoxButton.YesNoCancel);
                if(result == MessageBoxResult.Yes)
                {
                    Database.DeleteData(selected);

                    addres = new ObservableCollection<AddressData>(Database.ReadData());
                    listaAdresow.ItemsSource = addres;
                }
                else
                {
                    MessageBox.Show("Akcja została anulowana", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać daną", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddNewAddress(object sender, RoutedEventArgs e)
        {
            AddAddressPage addAddressPage = new AddAddressPage();
            addAddressPage.Show();

            this.Close();
        }

        private void SearchTextList(object sender, TextChangedEventArgs e)
        {
            string search = searchBar.Text.ToLower();

            var filtered = addres.Where(p =>
                p.imie.ToLower().Contains(search) ||
                p.nazwisko.ToLower().Contains(search) ||
                p.telefon.ToLower().Contains(search) ||
                p.email.ToLower().Contains(search)
            ).ToList();

            listaAdresow.ItemsSource = filtered;
        }

        private void EditAddres(object sender, RoutedEventArgs e)
        {
            AddressData selected = listaAdresow.SelectedItem as AddressData;
            
            if (selected != null)
            {
                int id = selected.Id;
                string name = selected.imie;
                string surname = selected.nazwisko;
                string phone = selected.telefon;
                string email = selected.email;

                EditAddressPage edit = new(id, name, surname, phone, email);
                edit.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Prosze wybrac wartość z listy", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Poprzednia_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateListView();
            }
        }

        private void Nastepna_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < (int)Math.Ceiling((double)addres.Count / itemsPerPage))
            {
                currentPage++;
                UpdateListView();
            }
        }

        private void UpdateProgressBar()
        {
            int totalPages = (int)Math.Ceiling((double)addres.Count / itemsPerPage);
            progressBar.Maximum = totalPages;
            progressBar.Value = currentPage;
        }
    }
}
