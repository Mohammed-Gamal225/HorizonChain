using Horizon.Domain.Enums;

namespace Horizon.Domain.Entities;

public class CowQuarter
{
    public Guid Id { get; set; }

    public int Sequence { get; set; }
    public CowQuarterType QuarterType { get; set; }
    public double Weight { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid SlaughteredCowId { get; set; }
    public SlaughteredCow SlaughteredCow { get; set; } = null!;

    public string CowIdentifier { get; set; } = default!;
}

