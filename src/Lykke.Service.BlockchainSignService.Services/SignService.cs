using Common;
using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Exceptions;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Services
{
    public class SignService : ISignService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IInternalSignServiceCaller _internalSignServiceCaller;
        private readonly IEncryptionService _encryptionService;
        private readonly byte[] _passwordBytes;

        public SignService(
            IWalletRepository walletRepository,
            IInternalSignServiceCaller internalSignServiceCaller,
            IEncryptionService encryptionService,
            BlockchainSignServiceSettings settings
            )
        {
            _walletRepository = walletRepository;
            _internalSignServiceCaller = internalSignServiceCaller;
            _encryptionService = encryptionService;
            _passwordBytes = settings.PasswordBytes;
        }

        public async Task<string> SignTransactionAsync(IEnumerable<Guid> walletIds, string transactionRaw)
        {
            IEnumerable<IWallet> wallets = null;

            try
            {
                wallets = await walletIds.SelectAsync(async walletId =>
                {
                    IWallet wallet = await _walletRepository.GetWalletAsync(walletId);

                    if (wallet == null)
                    {
                        throw new ClientSideException($"Wallet with id {walletId} does not exist in DB",
                            ClientSideException.ClientSideExceptionType.EntityDoesNotExist);
                    }

                    return wallet;
                });
            }
            catch (AggregateException exc)
            {
                foreach (var exception in exc.InnerExceptions)
                {
                    if (exception is ClientSideException clientSideExc)
                    {
                        throw clientSideExc;
                    }
                }
            }
            catch (Exception exc)
            {
                throw;
            }

            IEnumerable<string> privateKeys = wallets.Select(wallet =>
            {
                string privateKey = _encryptionService.DecryptAesString(wallet.EncryptedPrivateKey, _passwordBytes);

                return privateKey;
            });

            SignedTransactionResponse signedTransaction = await _internalSignServiceCaller.SignTransactionAsync(privateKeys, transactionRaw);

            return signedTransaction.SignedTransaction;
        }
    }
}
