using Domains.Entities;

namespace Airlines.ViewModels
{
    public class TicketVM
    {
     

        public string Voornaam { get; set; } = null!;

        public string Achternaam { get; set; } = null!;

        public double Prijs { get; set; }

        public MaaltijdVM maaltijd { get; set; } = null!;

        public ReisklasseVM reisklasse { get; set; } = null!;

        public SeizoenVM seizoen { get; set; } = null!;
        public VluchtVM vlucht { get; set; } = null!;

        public ZitplaatsVM zitplaats { get; set; } = null!;
    }
}
