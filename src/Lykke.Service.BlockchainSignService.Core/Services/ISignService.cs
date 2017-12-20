using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface ISignService
    {
        Task<string> SignTransactionAsync(IEnumerable<Guid> walletIds, string transactionRaw);
    }
}
