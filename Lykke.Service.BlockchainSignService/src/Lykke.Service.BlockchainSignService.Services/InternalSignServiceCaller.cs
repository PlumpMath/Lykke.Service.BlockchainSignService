using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Exceptions;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Services
{
    public class InternalSignServiceCaller : IInternalSignServiceCaller
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly string _signServiceUrl;

        public InternalSignServiceCaller(AppSettings appSettings)
        {
            var pipeline = new JsonApiTypeHandler()
            {
                InnerHandler = new HttpClientHandler()
            };
            _httpClient = new HttpClient(pipeline);
            _appSettings = appSettings;
            _signServiceUrl =  _appSettings.BlockchainSignServiceService.SignServiceUrl;
        }

        public async Task<KeyModelResponse> CreateWalletAsync()
        {
            HttpResponseMessage message = await _httpClient.PostAsync($"{_signServiceUrl}/api/wallet", new StringContent(""));
            string serializedResponse = await message.Content.ReadAsStringAsync();
            if (!message.IsSuccessStatusCode)
            {
                //TODO
                //throw new ClientSideException();
            }
            KeyModelResponse keyModelResponse = JsonConvert.DeserializeObject<KeyModelResponse>(serializedResponse);

            return keyModelResponse;
        }

        public async Task<SignedTransactionResponse> SignTransactionAsync(string privateKey, string transactionRaw)
        {
            string serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(
                new SignRequest() { PrivateKey = privateKey , TransactionHex = transactionRaw});
            HttpResponseMessage message = await _httpClient.PostAsync($"{_signServiceUrl}/api/sign", new StringContent(serializedRequest));
            string serializedResponse = await message.Content.ReadAsStringAsync();
            SignedTransactionResponse response = JsonConvert.DeserializeObject<SignedTransactionResponse>(serializedResponse);

            return response;
        }
    }

    internal class JsonApiTypeHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //request.Headers.Add("Content-Type", "application/json");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
