using Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
using Mapster;

namespace Horizon.Application.Configuration.Mapping;
internal static class MapsterConfig
{

    public static void AddMapsterConfig(this IServiceCollection _)
    {
        RegisterJobOrderMappings();
    }
    public static void RegisterJobOrderMappings()
    {
        TypeAdapterConfig<JobOrder, JobOrderSummaryResponse>.NewConfig()
            .Map(d => d.OrderCode, src => src.OrderCode)
            .Map(d => d.ClientName, src => src.ClientName)
            .Map(d => d.OrderDate, src => src.OrderDate)
            .Map(d => d.ClientCode, src => src.ClientCode);

        TypeAdapterConfig<JobOrder, JobOrderDetailsResponse>.NewConfig()
            .Map(d => d.Code, src => src.OrderCode)
            .Map(d => d.Type, src => src.OrderType)
            .Map(d => d.Cows, src => src.Cows.Adapt<List<CowResponse>>());

        TypeAdapterConfig<JobOrderCow, CowResponse>.NewConfig()
            .Map(d => d.Code, src => src.CowIdentifier)
            .Map(d => d.Weight, src => src.Weight);
    }



    public static void RegisterCowMappings()
    {
        // ✅ Unplanned Cow
        TypeAdapterConfig<UnplannedCowRequest, SlaughteredCow>
            .NewConfig()
            .Map(dest => dest.Id, _ => Guid.NewGuid())
            .Map(dest => dest.SlaughteredAt, _ => DateTime.UtcNow)
            .Map(dest => dest.JobOrderId, _ => (Guid?)null)
            .Map(dest => dest.OrderCode, _ => (string?)null)
            .Map(dest => dest.IsUnplanned, _ => true)
            .Map(dest => dest.Quarters, _ => new List<CowQuarter>());

        // ✅ Planned Cow (from JobOrderCow)
        TypeAdapterConfig<JobOrderCow, SlaughteredCow>
            .NewConfig()
            .Map(dest => dest.Id, _ => Guid.NewGuid())
            .Map(dest => dest.SlaughteredAt, _ => DateTime.UtcNow)
            .Map(dest => dest.CowIdentifier, src => src.CowIdentifier)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.Weight, src => src.Weight)
            .Map(dest => dest.OrderCode, src => src.OrderCode)
            .Map(dest => dest.JobOrderId, src => src.JobId)
            .Map(dest => dest.Quarters, _ => new List<CowQuarter>())
            .Map(dest => dest.IsUnplanned, _ => false);
    }
}
