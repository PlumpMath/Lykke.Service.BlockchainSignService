// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.BlockchainSignService.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SignTransactionRequest
    {
        /// <summary>
        /// Initializes a new instance of the SignTransactionRequest class.
        /// </summary>
        public SignTransactionRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SignTransactionRequest class.
        /// </summary>
        public SignTransactionRequest(System.Guid walletId, string transactionHex = default(string))
        {
            WalletId = walletId;
            TransactionHex = transactionHex;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "WalletId")]
        public System.Guid WalletId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TransactionHex")]
        public string TransactionHex { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
