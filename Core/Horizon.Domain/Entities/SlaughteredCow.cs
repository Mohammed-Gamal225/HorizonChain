namespace Horizon.Domain.Entities;
public class SlaughteredCow
{
    public Guid Id { get; set; }
    public string CowIdentifier { get; set; } = default!;
    public DateTime SlaughteredAt { get; set; }
    public string Type { get; set; } = default!;
    public bool IsUnplanned { get; set; } = true;
    public Guid? JobOrderId { get; set; }
    public string OrderCode { get; set; } = null!;
    public double Weight { get; set; }
    public ICollection<CowQuarter> Quarters { get; set; } = [];
}
