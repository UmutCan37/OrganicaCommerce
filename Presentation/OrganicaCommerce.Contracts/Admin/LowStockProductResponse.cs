namespace OrganicaCommerce.Contracts.Admin
{
    public class LowStockProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}