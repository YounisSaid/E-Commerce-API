namespace E_commerce.Domain.Entites.Orders
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem() { }
        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PicutreURL = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string PicutreURL { get; set; }
    }
}