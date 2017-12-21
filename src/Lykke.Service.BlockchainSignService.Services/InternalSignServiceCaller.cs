using Common;
using Common.Log;
using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Exceptions;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Services
{
    public class InternalSignServiceCaller : IInternalSignServiceCaller
    {
        private readonly HttpClient _httpClient;
        private readonly string _signServiceUrl;
        private readonly ILog _log;
        private readonly JSchemaGenerator _jSchemaGenerator;

        public InternalSignServiceCaller(BlockchainSignServiceSettings settings, ILog log)
        {
            var pipeline = new JsonApiTypeHandler()
            {
                InnerHandler = new HttpClientHandler()
            };

            _httpClient = new HttpClient(pipeline);
            _signServiceUrl = settings.SignServiceUrl;
            _log = log;
            _jSchemaGenerator = new JSchemaGenerator();
        }

        public async Task<KeyModelResponse> CreateWalletAsync()
        {
            HttpResponseMessage message = await _httpClient.PostAsync($"{_signServiceUrl}/api/wallet", new StringContent(""));
            string serializedResponse = await message.Content.ReadAsStringAsync();
            KeyModelResponse keyModelResponse = await ConvertToOrThrow<KeyModelResponse>(serializedResponse);

            return keyModelResponse;
        }

        public async Task<SignedTransactionResponse> SignTransactionAsync(IEnumerable<string> privateKeys, string transactionRaw)
        {
            string serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(
                new SignRequest() { PrivateKeys =  privateKeys, TransactionHex = transactionRaw });
            HttpResponseMessage message = await _httpClient.PostAsync($"{_signServiceUrl}/api/sign", new StringContent(serializedRequest));
            string serializedResponse = await message.Content.ReadAsStringAsync();
            SignedTransactionResponse response = await ConvertToOrThrow<SignedTransactionResponse>(serializedResponse);

            return response;
        }

        private async Task<T> ConvertToOrThrow<T>(string serializedData) where T : class, new()
        {
            T response = TryParseJson<T>(serializedData);

            if (response != null)
            {
                return response;
            }

            await _log.WriteWarningAsync(nameof(InternalSignServiceCaller), serializedData, "not succesful response", DateTime.UtcNow);

            ErrorResponse errorResponse = TryParseJson<ErrorResponse>(serializedData);

            if (errorResponse == null)
            {
                throw new ClientSideException("Unknown reponse from sign service", ClientSideException.ClientSideExceptionType.SignServiceError);
            }

            throw new ClientSideException($"Error happened in sign service: {errorResponse.ToJson()}", ClientSideException.ClientSideExceptionType.SignServiceError);
        }

        public T TryParseJson<T>(string json) where T : class, new()
        {
            JSchema parsedSchema = _jSchemaGenerator.Generate(typeof(T));
            JObject jObject = JObject.Parse(json);

            return jObject.IsValid(parsedSchema) ?
                JsonConvert.DeserializeObject<T>(json) : null;
        }
    }

    internal class JsonApiTypeHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            return await base.SendAsync(request, cancellationToken);
        }
    }

    #region InternalClassses

    internal class ErrorResponse
    {
        /// <summary>
        /// </summary>
        [Required()]
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "modelErrors")]
        public IDictionary<string, IList<string>> ModelErrors { get; private set; }

    }

    #endregion
}
