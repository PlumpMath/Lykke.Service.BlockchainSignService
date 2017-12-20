using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.BlockchainSignService.Client.AutorestClient;
using Lykke.Service.BlockchainSignService.Client.AutorestClient.Models;
using Lykke.Service.BlockchainSignService.Client.Exceptions;
using Lykke.Service.BlockchainSignService.Client.Models;

namespace Lykke.Service.BlockchainSignService.Client
{
    public class BlockchainSignServiceClient : IBlockchainSignServiceClient, IDisposable
    {
        private readonly ILog _log;
        private readonly IBlockchainSignServiceAPI _api;
        
        public BlockchainSignServiceClient(string serviceUrl, ILog log)
        {
            _log = log;
            _api = new BlockchainSignServiceAPI(new Uri(serviceUrl));
        }

        /// <exception cref="ErrorResponseException"/>
        /// <exception cref="UnknownResponseException"/>
        public async Task<WalletModel> CreateWalletAsync()
        {
            var response = await _api.CreateWalletAsync();
            WalletResponse wallet = ConvertToOrHandleErrorResponse<WalletResponse>(response);

            return new WalletModel(wallet.WalletId, wallet.PublicAddress);
        }

        /// <exception cref="ErrorResponseException"/>
        /// <exception cref="UnknownResponseException"/>
        public async Task<SignedTransactionModel> SignTransactionAsync(SignRequestModel requestModel)
        {
            var request = new SignTransactionRequest(requestModel.WalletIds?.ToList(), requestModel.TransactionHex);
            var response = await _api.SignTransactionAsync(request);
            SignTransactionResponse signTransactionResponse = ConvertToOrHandleErrorResponse<SignTransactionResponse>(response);

            return new SignedTransactionModel(signTransactionResponse.SignedTransaction);
        }

        /// <exception cref="ErrorResponseException"/>
        /// <exception cref="UnknownResponseException"/>
        public async Task<IEnumerable<WalletModel>> GetAllWalletsAsync()
        {
            var response = await _api.GetAllWalletsAsync();

            var result = ConvertToOrHandleErrorResponse<WalletsResponse>(response);

            return result.Wallets.Select(x => new WalletModel(x.WalletId, x.PublicAddress));
        }

        /// <exception cref="ErrorResponseException"/>
        /// <exception cref="UnknownResponseException"/>
        public async Task<WalletModel> GetWalletByIdAsync(Guid walletId)
        {
            var response = await _api.GetByWalletIdAsync(walletId);
            WalletResponse wallet = ConvertToOrHandleErrorResponse<WalletResponse>(response);

            if (wallet == null)
            {
                return null;
            }

            return new WalletModel(wallet.WalletId, wallet.PublicAddress);
        }

        /// <exception cref="ErrorResponseException"/>
        /// <exception cref="UnknownResponseException"/>
        public async Task<WalletModel> GetWalletByPublicAddressAsync(string publicAddress)
        {
            var response = await _api.GetByPublicAddressAsync(publicAddress);
            WalletResponse wallet = ConvertToOrHandleErrorResponse<WalletResponse>(response);

            if (wallet == null)
            {
                return null;
            }

            return new WalletModel(wallet.WalletId, wallet.PublicAddress);
        }

        public void Dispose()
        {
            if (_api == null)
                return;
            _api.Dispose();
        }

        private T ConvertToOrHandleErrorResponse<T>(object data)
        {
            if (data is T response)
            {
                return response;
            }

            if (data is ErrorResponse errorResponse)
            {
                throw new ErrorResponseException(errorResponse.ErrorMessage, errorResponse.ModelErrors);
            }

            throw new UnknownResponseException(data.ToJson());
        }
    }
}
