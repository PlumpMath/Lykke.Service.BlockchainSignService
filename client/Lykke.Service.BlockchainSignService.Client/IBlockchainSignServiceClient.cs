
using Lykke.Service.BlockchainSignService.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Client
{
    public interface IBlockchainSignServiceClient
    {
        Task<IEnumerable<WalletModel>> GetAllWalletsAsync();

        Task<WalletModel> GetWalletByIdAsync(Guid walletId);

        Task<WalletModel> GetWalletByPublicAddressAsync(string publicAddress);

        Task<WalletModel> CreateWalletAsync();

        Task<SignedTransactionModel> SignTransactionAsync(SignRequestModel requestModel);
    }
}
