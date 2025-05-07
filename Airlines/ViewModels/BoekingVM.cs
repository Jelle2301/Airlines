namespace Airlines.ViewModels
{
    public class BoekingVM
    {

        public int TicketId { get; set; }

        public DateOnly DatumBoeking { get; set; }

        public string VoornaamBoeking { get; set; } = null!;

        public string AchternaamBoeking { get; set; } = null!;

        public double TotalePrijs { get; set; }

        public string UserId { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
