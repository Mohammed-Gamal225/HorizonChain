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
}
