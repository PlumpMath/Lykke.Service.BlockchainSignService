using System;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface ISignService
    {
        Task<string> SignTransactionAsync(Guid walletId, string transactionRaw);
    }
}
