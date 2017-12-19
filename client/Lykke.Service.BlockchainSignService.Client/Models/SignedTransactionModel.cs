namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SignedTransactionModel
    {
        /// <summary>
        /// Initializes a new instance of the SignTransactionResponse class.
        /// </summary>
        public SignedTransactionModel(string signedTransaction = default(string))
        {
            SignedTransaction = signedTransaction;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SignedTransaction")]
        public string SignedTransaction { get; set; }

    }
}
