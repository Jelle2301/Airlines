namespace Airlines.ViewModels
{
    public class MijnBoekingVM
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }

        public DateOnly DatumBoeking { get; set; }

        public string Opstap { get; set; }
        public string Bestemming { get; set; }
        public string Status { get; set; }
    }
}
