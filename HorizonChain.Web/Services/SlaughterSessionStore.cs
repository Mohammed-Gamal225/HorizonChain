using Horizon.Application.Abstractions.Common.Models;
using Horizon.Application.Abstractions.Services;

namespace HorizonChain.Web.Services;

public class SlaughterSessionStore
    : ISlaughterSessionStore
{

    private SlaughterSession? _session;

    public void StartSession(SlaughterSession session)
    {
        _session = session;
    }

    public SlaughterSession? GetCurrent() => _session;

    public void Increment()
    {
        if (_session is not null)
            _session.QuartersReceived++;
    }

    public void Clear() => _session = null;
}
