namespace FootballclubBarcelona.Models;

public class Match
{
    public int Id { get; set; }
    public string Competition { get; set; }
    public string Opponent { get; set; }
    public string Date { get; set; }
    public string Stadium { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
}