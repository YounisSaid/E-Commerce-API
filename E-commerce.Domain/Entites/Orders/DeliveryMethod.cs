namespace E_commerce.Domain.Entites.Orders
{
    public class DeliveryMethod : Entity<int>
    {
        public DeliveryMethod() { }

        public DeliveryMethod(string shortName, string deliveryTime, double price)
        {
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Price = price;
        }
        public string ShortName { get; set; }
        public string Description  { get; set; }

        public string DeliveryTime { get; set; }
        public double Price { get; set; }
    }
}