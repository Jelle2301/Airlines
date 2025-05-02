namespace Airlines.ViewModels
{
    public class TicketMogelijkhedenVM
    {
        
        public List<MaaltijdVM> Maaltijden { get; set; } = null!;

        public List<ReisklasseVM> Reisklassen { get; set; } = null!;

        public SeizoenVM Seizoen { get; set; } = null!;
        public VluchtVM Vlucht { get; set; } = null!;

        public ZitplaatsVM Zitplaats { get; set; } = null!;
        public double TotaalPrijs { get; set; } = 0;
    }
}
