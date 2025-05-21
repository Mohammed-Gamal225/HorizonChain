namespace Horizon.Domain.Entities;
public class JobOrderCow
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public JobOrder JobOrder { get; set; } = default!;
    public string CowIdentifier { get; set; } = default!;
    // TODO : Create Enum For the Cow Type 
    public string Type { get; set; } = default!;
    public string TypeId { get; set; } = default!;
    public double Weight { get; set; }

}
