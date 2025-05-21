using Horizon.Application.Abstractions.DataTransferObjects.Slaughter;

namespace Horizon.Application.Abstractions.Common.Models;
public class SlaughterSession
{
    public Guid SlaughteredCowId { get; set; }
    public string CowIdentifier { get; set; } = null!;
    public List<CowQuarterDto> Quarters { get; set; } = [];
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}
