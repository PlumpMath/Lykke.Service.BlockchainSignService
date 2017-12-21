using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain.SignService
{
    [DataContract]
    public class SignedTransactionResponse
    {
        [Required]
        [DataMember(Name = "signedTransaction")]
        public string SignedTransaction { get; set; }
    }
}
