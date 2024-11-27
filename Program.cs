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
                        valtAlternativ = valtAlternativ == 0 ? 11 : valtAlternativ - 1; // Gå upp, wrap runt
                        break;

                    case ConsoleKey.DownArrow:
                        valtAlternativ = (valtAlternativ + 1) % 12; // Gå ner, wrap runt
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
                                LäggTillBetygFörBok(bibliotek);
                                break;
                            case 11:
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

        static void FiltreraBöckerEfterGenre(Bibliotek bibliotek)
        {
            Console.Write("Ange genre för att filtrera böcker: ");
            string genre = Console.ReadLine();
            bibliotek.FiltreraBöckerEfterGenre(genre);
        }

        static void FiltreraBöckerEfterFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författarens namn för att filtrera böcker: ");
            string namn = Console.ReadLine();
            bibliotek.FiltreraBöckerEfterFörfattare(namn);
        }

        static void LäggTillBetygFörBok(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID för att lägga till betyg: ");
            if (!int.TryParse(Console.ReadLine(), out int bokId))
            {
                Console.WriteLine("Ogiltigt bok-ID.");
                return;
            }

            Console.Write("Ange betyg (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int betyg) || betyg < 1 || betyg > 5)
            {
                Console.WriteLine("Betyg måste vara mellan 1 och 5.");
                return;
            }

            var bok = bibliotek.Böcker.FirstOrDefault(b => b.Id == bokId);
            if (bok != null)
            {
                bok.Betyg = betyg;
                Console.WriteLine("Betyg har lagts till.");
            }
            else
            {
                Console.WriteLine("Bok hittades inte.");
            }
        }
    }
}






