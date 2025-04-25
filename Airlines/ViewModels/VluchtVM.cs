namespace Airlines.ViewModels
{
    public class VluchtVM
    {
        public int VluchtId { get; set; }

        public double BeginPrijs { get; set; }

        public DateTime TijdVertrek { get; set; }

        public DateTime TijdAankomst { get; set; }


        public string VliegtuigNaam { get; set; } = null!;

        public int AantalTotalePlaatsen { get; set; }

        public int AantalGewonePlaatsen { get; set; }

        public int AantalBusinessPlaatsen { get; set; }

        public int AantalEconomyPlaatsen { get; set; }
        public string VertrekNaam { get; set; } = null!;
        public string OverstapNaam { get; set; } = null!;
        public string BestemmingNaam { get; set; } = null!;



    }
}
