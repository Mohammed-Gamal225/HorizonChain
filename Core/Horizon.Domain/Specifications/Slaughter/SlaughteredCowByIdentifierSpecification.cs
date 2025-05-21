using Horizon.Domain.Contracts.Specifications;
using Horizon.Domain.Entities;

namespace Horizon.Domain.Specifications.Slaughter;
public class SlaughteredCowByIdentifierSpecification(string cowIdentifier)
    : BaseSpecifications<SlaughteredCow>(c => c.CowIdentifier == cowIdentifier);
