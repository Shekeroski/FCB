namespace FootballclubBarcelona.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Price { get; set; }
    public string CategoryName { get; set; }
    public bool IsBought { get; set; }
    
    public string Size { get; set; }
    
    public ICollection<TeamAndPlayers> Players { get; set; }
    
}