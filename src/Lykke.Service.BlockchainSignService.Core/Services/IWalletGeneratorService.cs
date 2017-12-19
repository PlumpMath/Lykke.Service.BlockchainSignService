using Lykke.Service.BlockchainSignService.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IWalletGeneratorService
    {
        Task<WalletCreationResult> CreateWallet();
    }
}
