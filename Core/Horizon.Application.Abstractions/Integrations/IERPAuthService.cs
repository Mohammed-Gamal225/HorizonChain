using Horizon.Application.Abstractions.Common;

namespace Horizon.Application.Abstractions.Integrations;
public interface IERPAuthService
{
    Task<Result<string>> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
