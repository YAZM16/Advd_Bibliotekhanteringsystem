using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Linq;
namespace Advd_Bibliotekhanteringsystem
{
    public class Program
    {
        public static void Main()
        {
            // Ladda data vid programstart
            var bibliotek = DataFil.LaddaData() ?? new Bibliotek();
            bool fortsätt = true;
            int valtAlternativ = 0;
            while (fortsätt)
            {
                // Rensa skärmen och visa menyn
                // Läs användarens val
                Console.Clear();
                VisaMeny(valtAlternativ);

                ConsoleKeyInfo knapp = Console.ReadKey(true);
                switch (knapp.Key)
                {
                    case ConsoleKey.UpArrow:
                        valtAlternativ = valtAlternativ == 0 ? 13 : valtAlternativ - 1; // Gå upp, wrap runt
                        break;

                    case ConsoleKey.DownArrow:
                        valtAlternativ = (valtAlternativ + 1) % 13; // Gå ner, wrap runt
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (valtAlternativ)
                        {
                            case 0:
                                LäggTillBok(bibliotek);
                                break;
                            case 1:
                                LäggTillFörfattare(bibliotek);
                                break;
                            case 2:
                                UppdateraBok(bibliotek);
                                break;
                            case 3:
                                UppdateraFörfattare(bibliotek);
                                break;
                            case 4:
                                TaBortBok(bibliotek);
                                break;
                            case 5:
                                TaBortFörfattare(bibliotek);
                                break;
                            case 6:
                                bibliotek.ListaAllaBöcker();
                                break;
                            case 7:
                                bibliotek.ListaAllaFörfattare();
                                break;
                            case 8:
                                FiltreraBöckerEfterGenre(bibliotek);
                                break;
                            case 9:
                                FiltreraBöckerEfterFörfattare(bibliotek);
                                break;
                            case 10:
                                // Sortera böcker efter publiceringsår med LINQ
                                ListaBöckerSorteradeEfterPubliceringsår(bibliotek);
                                break;

                            case 11:
                                // Sortera författare efter namn med LINQ
                                ListaFörfattareSorteradeEfterNamn(bibliotek);
                                break;
                            case 12:
                                LäggTillBetygFörBok(bibliotek);
                                break;
                            case 13:
                                DataFil.SparaData(bibliotek);
                                Console.WriteLine("Data sparad. Programmet avslutas.");
                                fortsätt = false;
                                break;
                        }
                        Console.WriteLine("\nTryck på en tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void VisaMeny(int valtAlternativ)
        {
            string[] menyAlternativ =
            {
                "Lägg till ny bok",
                "Lägg till ny författare",
                "Uppdatera bokdetaljer",
                "Uppdatera författardetaljer",
                "Ta bort bok",
                "Ta bort författare",
                "Lista alla böcker",
                "Lista alla författare",
                "Filtrera böcker efter genre",
                "Filtrera böcker efter författare",
                "Sortera böcker efter publiceringsår",
                "Sortera författare efter namn",
                "Lägg till betyg för bok",
                "Avsluta och spara data"
            };

            Console.WriteLine("--- Bibliotek Meny ---");
            for (int i = 0; i < menyAlternativ.Length; i++)
            {
                if (i == valtAlternativ)
                {
                    Console.WriteLine($"--> {menyAlternativ[i]}");
                }
                else
                {
                    Console.WriteLine($"    {menyAlternativ[i]}");
                }
            }
        }


        static void LäggTillBok(Bibliotek bibliotek)
        {
            Console.Write("Ange boktitel: ");
            string titel = Console.ReadLine();

            Console.Write("Ange författarens ID: ");
            if (!int.TryParse(Console.ReadLine(), out int författareId))
            {
                Console.WriteLine("Ogiltigt författar-ID.");
                return;
            }

            Console.Write("Ange genre: ");
            string genre = Console.ReadLine();

            Console.Write("Ange publiceringsår: ");
            if (!int.TryParse(Console.ReadLine(), out int publiceringsår))
            {
                Console.WriteLine("Ogiltigt årtal.");
                return;
            }

            Console.Write("Ange ISBN: ");
            string isbn = Console.ReadLine();

            var nyBok = new Bok { Titel = titel, FörfattareId = författareId, Genre = genre, Publiceringsår = publiceringsår, Isbn = isbn };
            bibliotek.LäggTillBok(nyBok);
            Console.WriteLine("Ny bok har lagts till.");
        }

        static void LäggTillFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författarens namn: ");
            string namn = Console.ReadLine();

            Console.Write("Ange land: ");
            string land = Console.ReadLine();

            var nyFörfattare = new Författare { Namn = namn, Land = land };
            bibliotek.LäggTillFörfattare(nyFörfattare);
            Console.WriteLine("Ny författare har lagts till.");
        }

        static void UppdateraBok(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID för uppdatering: ");
            if (!int.TryParse(Console.ReadLine(), out int bokId))
            {
                Console.WriteLine("Ogiltigt bok-ID.");
                return;
            }

            Console.Write("Ange ny titel: ");
            string titel = Console.ReadLine();

            Console.Write("Ange ny författare-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int författareId))
            {
                Console.WriteLine("Ogiltigt författar-ID.");
                return;
            }

            Console.Write("Ange ny genre: ");
            string genre = Console.ReadLine();

            Console.Write("Ange nytt publiceringsår: ");
            if (!int.TryParse(Console.ReadLine(), out int publiceringsår))
            {
                Console.WriteLine("Ogiltigt årtal.");
                return;
            }

            Console.Write("Ange nytt ISBN: ");
            string isbn = Console.ReadLine();

            var uppdateradBok = new Bok { Id = bokId, Titel = titel, FörfattareId = författareId, Genre = genre, Publiceringsår = publiceringsår, Isbn = isbn };
            bibliotek.UppdateraBok(bokId, uppdateradBok);
            Console.WriteLine("Bokdetaljer har uppdaterats.");
        }

        static void UppdateraFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författar-ID för uppdatering: ");
            if (!int.TryParse(Console.ReadLine(), out int författareId))
            {
                Console.WriteLine("Ogiltigt författar-ID.");
                return;
            }

            Console.Write("Ange nytt namn: ");
            string namn = Console.ReadLine();

            Console.Write("Ange nytt land: ");
            string land = Console.ReadLine();

            var uppdateradFörfattare = new Författare { Id = författareId, Namn = namn, Land = land };
            bibliotek.UppdateraFörfattare(författareId, uppdateradFörfattare);
            Console.WriteLine("Författardetaljer har uppdaterats.");
        }

        static void TaBortBok(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out int bokId))
            {
                Console.WriteLine("Ogiltigt bok-ID.");
                return;
            }

            bibliotek.TaBortBok(bokId);
            Console.WriteLine("Bok har tagits bort.");
        }

        static void TaBortFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författar-ID att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out int författareId))
            {
                Console.WriteLine("Ogiltigt författar-ID.");
                return;
            }

            bibliotek.TaBortFörfattare(författareId);
            Console.WriteLine("Författare och dess böcker har tagits bort.");
        }

        // Filtrera böcker efter genre
        static void FiltreraBöckerEfterGenre(Bibliotek bibliotek)
        {
            Console.Write("Ange genre för att filtrera böcker: ");
            string genre = Console.ReadLine();

            var filtreradeBöcker = bibliotek.Böcker
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filtreradeBöcker.Any())
            {
                Console.WriteLine("Inga böcker hittades för den angivna genren.");
                return;
            }

            Console.WriteLine("--- Filtrerade Böcker ---");
            foreach (var bok in filtreradeBöcker)
            {
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare-ID: {bok.FörfattareId}, Genre: {bok.Genre}");
            }
        }
        // Filtrera böcker efter författare
        static void FiltreraBöckerEfterFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författarens namn för att filtrera böcker: ");
            string författarensNamn = Console.ReadLine();

            var filtreradeBöcker = bibliotek.Böcker
                .Where(b => bibliotek.Författare
                    .Any(f => f.Id == b.FörfattareId && f.Namn.Contains(författarensNamn, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (!filtreradeBöcker.Any())
            {
                Console.WriteLine("Inga böcker hittades för den angivna författaren.");
                return;
            }

            Console.WriteLine("--- Filtrerade Böcker ---");
            foreach (var bok in filtreradeBöcker)
            {
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare-ID: {bok.FörfattareId}, Genre: {bok.Genre}");
            }
        }

        static void LäggTillBetygFörBok(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID för att lägga till betyg: ");
            if (!int.TryParse(Console.ReadLine(), out int bokId))
            {
                Console.WriteLine("Ogiltigt bok-ID.");
               
            }

            Console.Write("Ange betyg (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int betyg) || betyg < 1 || betyg > 5)
            {
                Console.WriteLine("Betyg måste vara mellan 1 och 5.");
                
            }

            var bokAttBetygsätta = bibliotek.Böcker.FirstOrDefault(b => b.Id == bokId);
            if (bokAttBetygsätta != null)
            {
                bokAttBetygsätta.LäggTillBetyg(betyg);
                DataFil.SparaData(bibliotek); // Spara ändringarna
                Console.WriteLine($"Betyget {betyg} har lagts till boken \"{bokAttBetygsätta.Titel}\".");
                Console.WriteLine($"Genomsnittsbetyg: {bokAttBetygsätta.BeräknaGenomsnittBetyg():0.00}");
            }
            else
            {
                Console.WriteLine("Ingen bok med angivet ID hittades.");
            }
               
        }
        // Sortera böcker efter publiceringsår
        static void ListaBöckerSorteradeEfterPubliceringsår(Bibliotek bibliotek)
        {
            var sorteradeBöcker = bibliotek.Böcker
                .OrderBy(b => b.Publiceringsår)
                .ToList();

            if (!sorteradeBöcker.Any())
            {
                Console.WriteLine("Inga böcker att visa.");
                return;
            }

            Console.WriteLine("--- Böcker Sorterade Efter Publiceringsår ---");
            foreach (var bok in sorteradeBöcker)
            {
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare-ID: {bok.FörfattareId}, Publiceringsår: {bok.Publiceringsår}");
            }
        }

        // Sortera författare efter namn
        static void ListaFörfattareSorteradeEfterNamn(Bibliotek bibliotek)
        {
            var sorteradeFörfattare = bibliotek.Författare
                .OrderBy(f => f.Namn)
                .ToList();

            if (!sorteradeFörfattare.Any())
            {
                Console.WriteLine("Inga författare att visa.");
                return;
            }

            Console.WriteLine("--- Författare Sorterade Efter Namn ---");
            foreach (var författare in sorteradeFörfattare)
            {
                Console.WriteLine($"ID: {författare.Id}, Namn: {författare.Namn}, Land: {författare.Land}");
            }
        }
    }
}
       







