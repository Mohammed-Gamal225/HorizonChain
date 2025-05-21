namespace Horizon.Application.Abstractions.DataTransferObjects.Slaughter;
public class SlaughteredCowDto
{
    public string CowIdentifier { get; set; } = default!;
    public List<CowQuarterDto> Quarters { get; set; } = [];
}
