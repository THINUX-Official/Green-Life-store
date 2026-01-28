namespace GreenLifeStore.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        public double SubTotal =>
            (Price - (Price * Discount / 100.0)) * Quantity;
    }
}
