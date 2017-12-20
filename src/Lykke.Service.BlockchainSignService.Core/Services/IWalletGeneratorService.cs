using Lykke.Service.BlockchainSignService.Core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IWalletService
    {
        Task<WalletCreationResult> CreateWalletAsync();

        Task<IEnumerable<WalletCreationResult>> GetAllWalletsAsync();

        Task<WalletCreationResult> GetByWalletIdAsync(Guid walletId);

        Task<WalletCreationResult> GetByPublicAddressAsync(string publicAddress);
    }
}
