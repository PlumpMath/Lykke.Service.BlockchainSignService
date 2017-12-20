﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    public class SignTransactionRequest
    {
        [Required]
        public IEnumerable<Guid> WalletIds { get; set; }

        [Required]
        public string TransactionHex { get; set; }
    }
}
