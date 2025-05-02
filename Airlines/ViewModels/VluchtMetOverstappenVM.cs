namespace Airlines.ViewModels
{
    public class VluchtMetOverstappenVM
    {
        public VluchtVM GewoneVlucht { get; set; } = null!;
        public List<VluchtVM> OverstapVluchten { get; set; } = null!;
    }
}
