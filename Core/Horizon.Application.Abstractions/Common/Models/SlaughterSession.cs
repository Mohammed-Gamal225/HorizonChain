namespace Horizon.Application.Abstractions.Common.Models;
public class SlaughterSession
{
    public Guid SlaughteredCowId { get; set; }
    public string CowIdentifier { get; set; } = null!;
    public int QuartersReceived { get; set; } = 0;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}
