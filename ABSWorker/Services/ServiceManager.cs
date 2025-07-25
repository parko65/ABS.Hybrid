using ABSWorker.Configuration;
using ABSWorker.Contracts;
using Microsoft.Extensions.Options;

namespace ABSWorker.Services;
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IPLCReadService> _pLCReadService;

    public ServiceManager(ILoggerManager logger, IOptionsMonitor<PlcSettings> plcSettings)
    {
        _pLCReadService = new Lazy<IPLCReadService>(() => new PLCReadService(logger, plcSettings));
    }

    public IPLCReadService PLCReadService => _pLCReadService.Value;
}
