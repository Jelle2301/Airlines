namespace Airlines.ViewModels
{
    public class ShoppingCartVM
    {
        public List<TicketVM>? Carts { get; set; }
        public double ComputeTotalValue() =>
            Carts.Sum(e => e.Prijs);
    }
    
}
