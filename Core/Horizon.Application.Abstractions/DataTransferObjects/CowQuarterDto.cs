namespace Horizon.Application.Abstractions.DataTransferObjects;
public record CowQuarterDto
{
    public string QuarterCode { get; set; } = default!;
    public string Type { get; set; } = default!;
    public double Weight { get; set; }
}
