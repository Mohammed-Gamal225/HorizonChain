namespace Horizon.Application.Abstractions.DataTransferObjects.Slaughter;
public class PlannedCowRequest
{
    public string CowIdentifier { get; set; } = null!;
    public string OrderCode { get; set; } = null!;
}
