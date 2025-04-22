using Domains.Entities;

namespace Airlines.ViewModels
{
    public class TicketVM
    {
     

        public string Voornaam { get; set; } = null!;

        public string Achternaam { get; set; } = null!;

        public double Prijs { get; set; }

        public MaaltijdVM Maaltijd { get; set; } = null!;

        public ReisklasseVM Reisklasse { get; set; } = null!;

        public SeizoenVM Seizoen { get; set; } = null!;
        public VluchtVM Vlucht { get; set; } = null!;

        public ZitplaatsVM Zitplaats { get; set; } = null!;
    }
}
