using Horizon.Application.Abstractions.Common.Models;

namespace Horizon.Application.Abstractions.Services;
public interface ISlaughterSessionStore
{
    void StartSession(SlaughterSession session);
    SlaughterSession? GetCurrent();
    void Increment();
    void Clear();
}
