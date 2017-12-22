using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    [DataContract]
    public class SignTransactionRequest
    {
        [Required]
        [DataMember(Name = "publicAddresses")]
        public IEnumerable<string> PublicAddresses { get; set; }

        [Required]
        [DataMember(Name = "transactionHex")]
        public string TransactionHex { get; set; }
    }
}
