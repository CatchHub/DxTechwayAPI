namespace ProductWebAPI.DTOs
{
    public class ProductsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Detail { get; set; }
        public string Brand { get; set; }
        public string CurrencyCode { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
