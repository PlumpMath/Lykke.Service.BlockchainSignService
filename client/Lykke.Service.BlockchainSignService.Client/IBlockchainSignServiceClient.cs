
using Lykke.Service.BlockchainSignService.Client.Models;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Client
{
    public interface IBlockchainSignServiceClient
    {
        Task<WalletModel> CreateWalletAsync();

        Task<SignedTransactionModel> SignTransactionAsync(SignRequestModel requestModel);
    }
}
