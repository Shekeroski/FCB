namespace FootballclubBarcelona.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public ICollection<TicketPurchasing>?  TicketsPurchasings { get; set; }
    public ICollection<ProductPurchasing>?  Products { get; set; }
}