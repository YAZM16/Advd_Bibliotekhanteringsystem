using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

namespace Advd_Bibliotekhanteringsystem
{
    public class DataFil
    {
        private const string Filnamn = "LibraryData.json";  // Namnet på filen

        // Metod för att spara bibliotekets data till en JSON-fil
        public static void SparaData(Bibliotek bibliotek)
        {
            // Kontrollera om filen ska skapas eller om det är en ny fil
            string filnamn = "bibliotek.json";

            // Serialisera objektet till JSON-sträng
            string json = JsonConvert.SerializeObject(bibliotek, Formatting.Indented);

            // Skriv JSON-strängen till fil
            File.WriteAllText(filnamn, json);

            // Debugging: skriva ut
            Console.WriteLine("Data sparad: ");
        }

        // Metod för att ladda bibliotekets data från en JSON-fil
        public static Bibliotek LaddaData()
        {
            try
            {
                if (!File.Exists(Filnamn))
                    return new Bibliotek();

                string json = File.ReadAllText(Filnamn);
                return JsonConvert.DeserializeObject<Bibliotek>(json);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid laddning av data: {ex.Message}");
            }

            return new Bibliotek();
        }
    }
}
