using System;

namespace Advd_Bibliotekhanteringsystem
{
    public class Program
    {
        public static void Main()
        {
            // Ladda data vid programstart
            var bibliotek = DataFil.LaddaData();
            bool fortsätt = true;
            while (fortsätt)
            {
                // Rensa skärmen och visa menyn
                Console.Clear();
                VisaMeny();

                // Läs användarens val
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
                    Console.WriteLine("Felaktig inmatning, försök igen.");
                    continue;
                }

                switch (val)
                {
                    case 1:
                        Console.Write("Ange boktitel: ");
                        string titel = Console.ReadLine();

                        Console.Write("Ange författarens ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int författareId))
                        {
                            Console.WriteLine("Ogiltigt författar-ID.");
                            break;
                        }

                        Console.Write("Ange genre: ");
                        string genre = Console.ReadLine();

                        Console.Write("Ange publiceringsår: ");
                        if (!int.TryParse(Console.ReadLine(), out int publiceringsår))
                        {
                            Console.WriteLine("Ogiltigt årtal.");
                            break;
                        }

                        Console.Write("Ange ISBN: ");
                        string isbn = Console.ReadLine();

                        var nyBok = new Bok { Titel = titel, FörfattareId = författareId, Genre = genre, Publiceringsår = publiceringsår, Isbn = isbn };
                        bibliotek.LäggTillBok(nyBok);
                        Console.WriteLine("Ny bok har lagts till.");
                        break;

                    case 2:
                        Console.Write("Ange författarens namn: ");
                        string namn = Console.ReadLine();

                        Console.Write("Ange land: ");
                        string land = Console.ReadLine();

                        var nyFörfattare = new Författare { Namn = namn, Land = land };
                        bibliotek.LäggTillFörfattare(nyFörfattare);
                        Console.WriteLine("Ny författare har lagts till.");
                        break;

                    case 3:
                        Console.Write("Ange bok-ID för uppdatering: ");
                        if (!int.TryParse(Console.ReadLine(), out int bokId))
                        {
                            Console.WriteLine("Ogiltigt bok-ID.");
                            break;
                        }

                        Console.Write("Ange ny titel: ");
                        titel = Console.ReadLine();

                        Console.Write("Ange ny författare-ID: ");
                        if (!int.TryParse(Console.ReadLine(), out författareId))
                        {
                            Console.WriteLine("Ogiltigt författar-ID.");
                            break;
                        }

                        Console.Write("Ange ny genre: ");
                        genre = Console.ReadLine();

                        Console.Write("Ange nytt publiceringsår: ");
                        if (!int.TryParse(Console.ReadLine(), out publiceringsår))
                        {
                            Console.WriteLine("Ogiltigt årtal.");
                            break;
                        }

                        Console.Write("Ange nytt ISBN: ");
                        isbn = Console.ReadLine();

                        var uppdateradBok = new Bok { Id = bokId, Titel = titel, FörfattareId = författareId, Genre = genre, Publiceringsår = publiceringsår, Isbn = isbn };
                        bibliotek.UppdateraBok(bokId, uppdateradBok);
                        Console.WriteLine("Bokdetaljer har uppdaterats.");
                        break;

                    case 4:
                        Console.Write("Ange författar-ID för uppdatering: ");
                        if (!int.TryParse(Console.ReadLine(), out int författareUppId))
                        {
                            Console.WriteLine("Ogiltigt författar-ID.");
                            break;
                        }

                        Console.Write("Ange nytt namn: ");
                        namn = Console.ReadLine();

                        Console.Write("Ange nytt land: ");
                        land = Console.ReadLine();

                        var uppdateradFörfattare = new Författare { Id = författareUppId, Namn = namn, Land = land };
                        bibliotek.UppdateraFörfattare(författareUppId, uppdateradFörfattare);
                        Console.WriteLine("Författardetaljer har uppdaterats.");
                        break;

                    case 5:
                        Console.Write("Ange bok-ID att ta bort: ");
                        if (!int.TryParse(Console.ReadLine(), out bokId))
                        {
                            Console.WriteLine("Ogiltigt bok-ID.");
                            break;
                        }

                        bibliotek.TaBortBok(bokId);
                        Console.WriteLine("Bok har tagits bort.");
                        break;

                    case 6:
                        Console.Write("Ange författar-ID att ta bort: ");
                        if (!int.TryParse(Console.ReadLine(), out författareUppId))
                        {
                            Console.WriteLine("Ogiltigt författar-ID.");
                            break;
                        }

                        bibliotek.TaBortFörfattare(författareUppId);
                        Console.WriteLine("Författare och dess böcker har tagits bort.");
                        break;

                    case 7:
                        bibliotek.ListaAllaBöcker();
                        break;

                    case 8:
                        bibliotek.ListaAllaFörfattare();
                        break;

                    case 9:
                        bibliotek.SparaData();
                        Console.WriteLine("Data sparat. Programmet avslutas.");
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                // Vänta en stund innan menyn visas igen
                Console.WriteLine("\nTryck på en tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        static void VisaMeny()
        {
            Console.WriteLine("\n--- Bibliotek Meny ---");
            Console.WriteLine("1. Lägg till ny bok");
            Console.WriteLine("2. Lägg till ny författare");
            Console.WriteLine("3. Uppdatera bokdetaljer");
            Console.WriteLine("4. Uppdatera författardetaljer");
            Console.WriteLine("5. Ta bort bok");
            Console.WriteLine("6. Ta bort författare");
            Console.WriteLine("7. Lista alla böcker");
            Console.WriteLine("8. Lista alla författare");
            Console.WriteLine("9. Avsluta och spara data");
            Console.Write("Välj ett alternativ: ");
        }
    }
}
