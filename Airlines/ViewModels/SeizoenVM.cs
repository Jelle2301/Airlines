using Domains.Entities;

namespace Airlines.ViewModels
{
    public class SeizoenVM
    {
        public double Tarief { get; set; }

        public string SoortPeriode { get; set; } = null!;

        public DateOnly BeginDatum { get; set; }

        public DateOnly EindDatum { get; set; }


    }
}
