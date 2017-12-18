using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    public class SignTransactionRequest
    {
        public Guid WalletId { get; set; }

        public string TransactionHex { get; set; }
    }
}
