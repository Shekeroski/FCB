namespace FootballclubBarcelona.Models;

public class Ticket
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public Match? Match { get; set; }
    public string Side { get; set; }
    public string Block { get; set; }
    public int Seat { get; set; }
    public string price { get; set; }
    
    public bool IsSold { get; set; }
}