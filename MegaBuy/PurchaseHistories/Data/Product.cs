
namespace MegaBuy.PurchaseHistories.Data
{
    public sealed class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }

        public Product(string id, string name, decimal price, ProductCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
