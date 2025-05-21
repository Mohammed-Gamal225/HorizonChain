namespace Horizon.Domain.Entities;
public class JobOrder
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public int NumberOfCows { get; set; }
    public string ClientName { get; set; } = default!;
    public string ClientCode { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string OrderType { get; set; } = default!;
    public ICollection<JobOrderCow> Cows { get; set; } = [];
}


