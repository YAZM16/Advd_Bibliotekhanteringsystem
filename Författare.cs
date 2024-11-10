using System.Collections.Generic;

namespace Advd_Bibliotekhanteringsystem
{
    public class Författare
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Land { get; set; }
        public List<int> BokIds { get; set; } = new List<int>();
    }
}
