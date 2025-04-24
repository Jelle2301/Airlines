using System.ComponentModel.DataAnnotations;

namespace Airlines.ViewModels
{
    public class TicketGekozenVM
    {
        [Required]
        public string Voornaam { get; set; } = null!;
        [Required]
        public string Achternaam { get; set; } = null!;
        public double TotaalPrijs { get; set; }

    }
}
