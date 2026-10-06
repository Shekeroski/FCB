namespace FootballclubBarcelona.Models;

public class TicketPurchasing
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    public int MatchId { get; set; }
    public Match? Match { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}