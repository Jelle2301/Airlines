namespace Airlines.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM>? Carts { get; set; }
        public double ComputeTotalValue() =>
            Carts.Sum(e => e.Ticket.Prijs);
    }
    public class CartVM
    {
        public TicketVM? Ticket { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
