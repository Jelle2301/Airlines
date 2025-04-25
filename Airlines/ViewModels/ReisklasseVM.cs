using Domains.Entities;

namespace Airlines.ViewModels
{
    public class ReisklasseVM
    {
        public string SoortReisklasse { get; set; } = null!;
        public double ExtraPrijsReisklasse { get; set; }
        public int ReisklasseId { get; set; }

    }
}
