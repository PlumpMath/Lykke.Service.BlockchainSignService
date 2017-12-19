using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}