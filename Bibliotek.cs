using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Advd_Bibliotekhanteringsystem
{
    public class Bibliotek
    {
       
        public List<Bok> Böcker { get; set; } = new List<Bok>();
        
        public List<Författare> Författare { get; set; } = new List<Författare>();

         private const string Filnamn = @"C:\Users\user\source\repos\Advd_Bibliotekhanteringsystem_1\LibraryData.json";


        public Bibliotek()
        {
            LaddaData();
        }

        // Metod för att ladda data från filen
        private void LaddaData()
        {
            if (File.Exists(Filnamn))
            {
                try
                {
                    var jsonData = File.ReadAllText(Filnamn);
                    if (string.IsNullOrEmpty(jsonData))
                    {
                        var data = JsonConvert.DeserializeObject<Bibliotek>(jsonData);
                        if (data != null)
                        {
                            Böcker = data.Böcker ?? new List<Bok>();
                            Författare = data.Författare ?? new List<Författare>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid läsning av fil: {ex.Message}");
                }
            }
        }

        // Metod för att spara data till filen
        public void SparaData()
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(Filnamn, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid skrivning till fil: {ex.Message}");
            }
        }

        // Lägg till bok
        public void LäggTillBok(Bok nyBok)
        {
            nyBok.Id = Böcker.Any() ? Böcker.Max(b => b.Id) + 1 : 1;
            Böcker.Add(nyBok);
            SparaData();
        }

        // Lägg till författare
        public void LäggTillFörfattare(Författare nyFörfattare)
        {
            nyFörfattare.Id = Författare.Any() ? Författare.Max(f => f.Id) + 1 : 1;
            Författare.Add(nyFörfattare);
            SparaData();
        }

        // Uppdatera bok
        public void UppdateraBok(int bokId, Bok uppdateradBok)
        {
            var bok = Böcker.FirstOrDefault(b => b.Id == bokId);
            if (bok != null)
            {
                bok.Titel = uppdateradBok.Titel;
                bok.FörfattareId = uppdateradBok.FörfattareId;
                bok.Genre = uppdateradBok.Genre;
                bok.Publiceringsår = uppdateradBok.Publiceringsår;
                bok.Isbn = uppdateradBok.Isbn;
                SparaData();
            }
        }

        // Uppdatera författare
        public void UppdateraFörfattare(int författareId, Författare uppdateradFörfattare)
        {
            var författare = Författare.FirstOrDefault(f => f.Id == författareId);
            if (författare != null)
            {
                författare.Namn = uppdateradFörfattare.Namn;
                författare.Land = uppdateradFörfattare.Land;
                SparaData();
            }
        }

        // Ta bort bok
        public void TaBortBok(int bokId)
        {
            Böcker.RemoveAll(b => b.Id == bokId);
            SparaData();
        }

        // Ta bort författare
        public void TaBortFörfattare(int författareId)
        {
            Författare.RemoveAll(f => f.Id == författareId);
            Böcker.RemoveAll(b => b.FörfattareId == författareId);
            SparaData();
        }

       public void ListaAllaBöcker()
        {
            if (!Böcker.Any()) // Kontrollerar om det finns några böcker i listan.
            {
                Console.WriteLine("Inga böcker finns i biblioteket."); // Meddelar användaren om listan är tom.
                return;
            }

            // Sorterar böckerna i alfabetisk ordning baserat på deras titel.
            var sorteradeBöcker = Böcker.OrderBy(b => b.Titel).ToList();

            // Loopar igenom varje bok och skriver ut dess detaljer.
            foreach (var bok in sorteradeBöcker)
            {
                var författare = Författare.FirstOrDefault(f => f.Id == bok.FörfattareId)?.Namn ?? "Okänd författare";
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare: {författare}, Genre: {bok.Genre}, Publiceringsår: {bok.Publiceringsår}");
            }
        }

        /// <summary>
        /// Filtrerar böcker baserat på en angiven genre.
        /// </summary>
        /// <param name="genre">Den genre som böcker ska filtreras efter.</param>
        // Lista alla författare
        public void ListaAllaFörfattare()
        {
            if (!Författare.Any())
            {
                Console.WriteLine("Inga författare finns i biblioteket.");
                return;
            }

            foreach (var författare in Författare)
            {
                Console.WriteLine($"ID: {författare.Id}, Namn: {författare.Namn}, Land: {författare.Land}");
            }
        }

        // Filtrera böcker efter författare
        public void FiltreraBöckerEfterFörfattare(string författarensNamn)
        {
            var författare = Författare.FirstOrDefault(f => f.Namn.Equals(författarensNamn, StringComparison.OrdinalIgnoreCase));
            if (författare == null)
            {
                Console.WriteLine($"Ingen författare hittades med namnet: {författarensNamn}");
                return;
            }

            var filtreradeBöcker = Böcker.Where(b => b.FörfattareId == författare.Id).ToList();
            if (!filtreradeBöcker.Any())
            {
                Console.WriteLine($"Inga böcker hittades för författaren: {författarensNamn}");
                return;
            }

            foreach (var bok in filtreradeBöcker)
            {
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Genre: {bok.Genre}, Publiceringsår: {bok.Publiceringsår}");
            }
        }
        public void FiltreraBöckerEfterGenre(string genre)
        {
            var filtreradeBöcker = Böcker.Where(bok => bok.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtreradeBöcker.Count == 0)
            {
                Console.WriteLine($"Inga böcker hittades med genren: {genre}");
            }
            else
            {
                Console.WriteLine($"Böcker i genren {genre}:");
                foreach (var bok in filtreradeBöcker)
                {
                    Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare ID: {bok.FörfattareId}, År: {bok.Publiceringsår}, ISBN: {bok.Isbn}");
                }
            }
        }

    }
}