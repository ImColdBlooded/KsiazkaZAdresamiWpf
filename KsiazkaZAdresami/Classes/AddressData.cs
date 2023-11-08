using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsiazkaZAdresami.Classes
{
    public class AddressData
    {
        public int Id { get; set; }

        public string nazwisko { get; set; }
        public string imie { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }

        public AddressData()
        {

        }

        public AddressData(string nazwisko, string imie, string telefon, string email)
        {
            this.nazwisko = nazwisko;
            this.imie = imie;
            this.telefon = telefon;
            this.email = email;
        }   
    }
}
