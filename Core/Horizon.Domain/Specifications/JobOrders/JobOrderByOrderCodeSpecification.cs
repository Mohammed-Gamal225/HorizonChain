using Horizon.Domain.Contracts.Specifications;
using Horizon.Domain.Entities;

namespace Horizon.Domain.Specifications.JobOrders;
public class JobOrderByOrderCodeSpecification(string orderCode)
    : BaseSpecifications<JobOrder>(j => j.OrderCode == orderCode);
