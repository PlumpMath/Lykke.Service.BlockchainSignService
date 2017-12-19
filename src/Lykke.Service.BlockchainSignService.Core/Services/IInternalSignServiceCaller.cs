using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using System;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IInternalSignServiceCaller
    {
        Task<KeyModelResponse> CreateWalletAsync();

        Task<SignedTransactionResponse> SignTransactionAsync(string privateKey, string transactionRaw);
    }
}
