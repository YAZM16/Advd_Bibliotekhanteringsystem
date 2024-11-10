using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Advd_Bibliotekhanteringsystem
{
    public class Bibliotek
    {
        [JsonIgnore]
        public List<Bok> Böcker { get; set; } = new List<Bok>();
        [JsonIgnore]
        public List<Författare> Författare { get; set; } = new List<Författare>();

        private const string Filnamn = "LibraryData.json";

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

        // Lista alla böcker
        public void ListaAllaBöcker()
        {
            if (Böcker.Any())
            {
                foreach (var bok in Böcker)
                {
                    Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Genre: {bok.Genre}, Publiceringsår: {bok.Publiceringsår}");
                }
            }
            else
            {
                Console.WriteLine("Inga böcker finns.");
            }
        }

        // Lista alla författare
        public void ListaAllaFörfattare()
        {
            if (Författare.Any())
            {
                foreach (var författare in Författare)
                {
                    Console.WriteLine($"ID: {författare.Id}, Namn: {författare.Namn}, Land: {författare.Land}");
                }
            }
            else
            {
                Console.WriteLine("Inga författare finns.");
            }
        }
    }
}


