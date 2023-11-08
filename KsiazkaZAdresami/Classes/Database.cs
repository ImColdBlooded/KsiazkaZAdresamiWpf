using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KsiazkaZAdresami.Classes
{
    public class Database
    {
        public static void InitializeDatabase()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AddressBook.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = "CREATE TABLE IF NOT EXISTS AddressBookTable (Id INTEGER PRIMARY KEY, Imie TEXT NULL, Nazwisko TEXT NULL, Telefon TEXT NULL, Email TEXT NULL)";
                var createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void CreateData(AddressData address)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AddressBook.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO AddressBookTable VALUES (NULL, @Surname, @Name, @Phone, @Email)";
                insertCommand.Parameters.AddWithValue("@Surname", address.nazwisko);
                insertCommand.Parameters.AddWithValue("@Name", address.imie);
                insertCommand.Parameters.AddWithValue("@Phone", address.telefon);
                insertCommand.Parameters.AddWithValue("@Email", address.email); 
                insertCommand.ExecuteReader();
            }
        }

        public static void UpdateData(AddressData address)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AddressBook.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.CommandText = "UPDATE AddressBookTable SET Imie=@Name, Nazwisko=@Surname, Telefon=@Phone, Email=@Email WHERE Id=@Id";

                updateCommand.Parameters.AddWithValue("@Id", address.Id);
                updateCommand.Parameters.AddWithValue("@Surname", address.nazwisko);
                updateCommand.Parameters.AddWithValue("@Name", address.imie);
                updateCommand.Parameters.AddWithValue("@Phone", address.telefon);
                updateCommand.Parameters.AddWithValue("@Email", address.email);
                updateCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteData(AddressData address)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AddressBook.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.CommandText = "DELETE FROM AddressBookTable WHERE Id=@Id";

                deleteCommand.Parameters.AddWithValue("@Id", address.Id);
                deleteCommand.ExecuteNonQuery();
            }
        }

        public static List<AddressData> ReadData()
        {
            var entries = new List<AddressData>();
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AddressBook.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string query = "SELECT * FROM AddressBookTable";
                var selectCommand = new SqliteCommand(query, db);
                
                SqliteDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    AddressData address = new AddressData();
                    address.Id = reader.GetInt32(0);
                    address.nazwisko = reader.GetString(1);
                    address.imie = reader.GetString(2);
                    address.telefon = reader.GetString(3);
                    address.email = reader.GetString(4);
                    entries.Add(address);
                }
            }
            return entries;
        }
    }
}
