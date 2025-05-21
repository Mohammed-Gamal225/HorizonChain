using Horizon.Domain.Contracts.Specifications;
using Horizon.Domain.Entities;

namespace Horizon.Domain.Specifications.JobOrders;
public class JobOrderCowByIdSpecification(string cowIdentifier)
    : BaseSpecifications<JobOrderCow>(c => c.CowIdentifier == cowIdentifier);
