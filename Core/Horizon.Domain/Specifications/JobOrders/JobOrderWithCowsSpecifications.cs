using Horizon.Domain.Contracts.Specifications;
using Horizon.Domain.Entities;

namespace Horizon.Domain.Specifications.JobOrders;
public class JobOrderWithCowsSpecifications
    : BaseSpecifications<JobOrder>
{
    public JobOrderWithCowsSpecifications()
        : base(null)
    {
        AddInclude(j => j.Cows);
    }

    public JobOrderWithCowsSpecifications(string orderCode)
        : base(o => o.OrderCode == orderCode)
    {
        AddInclude(j => j.Cows);
    }
}
