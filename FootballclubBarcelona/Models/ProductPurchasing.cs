namespace FootballclubBarcelona.Models;

public class ProductPurchasing
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    
    public string PlayerName { get; set; }
}