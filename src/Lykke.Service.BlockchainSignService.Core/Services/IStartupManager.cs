using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IStartupManager
    {
        Task StartAsync();
    }
}