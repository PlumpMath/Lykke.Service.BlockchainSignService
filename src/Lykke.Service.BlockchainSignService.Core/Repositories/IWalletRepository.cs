using Lykke.Service.BlockchainSignService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Repositories
{
    public interface IWalletRepository
    {
        Task<IWallet> GetWalletAsync(Guid walletId);

        Task<IEnumerable<IWallet>> GetAllAsync();

        Task SaveAsync(IWallet wallet);

        Task<IWallet> GetWalletByPublicAddressAsync(string publicAddress);
    }
}
