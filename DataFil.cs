using System;
using System.IO;
using System.Text.Json;

namespace Advd_Bibliotekhanteringsystem
{
    public class DataFil
    {
        private const string Filnamn = "LibraryData.json";  // Namnet på filen

        // Metod för att spara bibliotekets data till en JSON-fil
        public static void SparaData(Bibliotek bibliotek)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(bibliotek, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Filnamn, jsonData);
                Console.WriteLine("Data har sparats.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av data: {ex.Message}");
            }
        }

        // Metod för att ladda bibliotekets data från en JSON-fil
        public static Bibliotek LaddaData()
        {
            try
            {
                if (File.Exists(Filnamn))
                {
                    var jsonData = File.ReadAllText(Filnamn);
                    var bibliotek = JsonSerializer.Deserialize<Bibliotek>(jsonData);
                    return bibliotek;
                }
                else
                {
                    Console.WriteLine("Ingen datafil hittades, ett nytt bibliotek skapas.");
                    return new Bibliotek();  // Returnera ett nytt bibliotek om filen inte finns
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid laddning av data: {ex.Message}");
                return new Bibliotek();  // Om det uppstår ett fel, returnera ett nytt bibliotek
            }
        }
    }
}
