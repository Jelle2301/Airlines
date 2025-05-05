using Domains.Entities;

namespace Airlines.ViewModels
{
    public class ZitplaatsVM
    {

        public int ZitplaatsId { get; set; }

        public string Zitnummer { get; set; } = null!;

        public int VluchtId { get; set; }

        public bool IsBezet { get; set; }

    }
}
