namespace OrganicaCommerce.Contracts.Products
{
    public class UpdateStockRequest
    {
        public int Quantity { get; set; }
        public bool IsIncrease { get; set; }
    }
}