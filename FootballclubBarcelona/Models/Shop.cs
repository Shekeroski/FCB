namespace FootballclubBarcelona.Models;

public class Shop
{
    public string Id { get; set; }
    public string ShopName { get; set; }
    public ICollection<Product>? Products { get; set; }
    
}