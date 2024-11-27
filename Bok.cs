using System.Collections.Generic;
using System.Linq;

namespace Advd_Bibliotekhanteringsystem
{
    public class Bok
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public int FörfattareId { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Publiceringsår { get; set; }
        public string Isbn { get; set; }
        public List<int> Recensioner { get; set; } = new List<int>();

        public void LäggTillBetyg(int betyg)
        {
            if (betyg >= 1 && betyg <= 5)
                Recensioner.Add(betyg);
            else
                Console.WriteLine("Betyget måste vara mellan 1 och 5.");
        }

        public double BeräknaGenomsnittBetyg() => Recensioner.Count > 0 ? Recensioner.Average() : 0;
    }
}
