using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain.SignService
{
    [DataContract]
    public class SignRequest
    {
        [DataMember(Name = "privateKeys")]
        public IEnumerable<string> PrivateKeys { get; set; }

        [DataMember(Name = "transactionHex")]
        public string TransactionHex { get; set; }
    }
}
