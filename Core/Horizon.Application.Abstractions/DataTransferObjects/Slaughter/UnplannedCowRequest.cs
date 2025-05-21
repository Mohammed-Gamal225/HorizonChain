namespace Horizon.Application.Abstractions.DataTransferObjects.Slaughter;
public class UnplannedCowRequest
{
    public string CowIdentifier { get; set; } = default!;
    public double Weight { get; set; }
    public string? Type { get; set; }
}