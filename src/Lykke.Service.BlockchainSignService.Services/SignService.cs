﻿using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Exceptions;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using System;
using System.Collections.Generic;
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
            string signedTransaction = transactionRaw;
            foreach (var wallet in walletIds)
            {
                signedTransaction = await SignTransactionAsync(wallet, signedTransaction);
            }

            return signedTransaction;
        }

        public async Task<string> SignTransactionAsync(Guid walletId, string transactionRaw)
        {
            IWallet wallet = await _walletRepository.GetWalletAsync(walletId);
            if (wallet == null)
            {
                throw new ClientSideException($"Wallet with id {walletId} does not exist in DB", 
                    ClientSideException.ClientSideExceptionType.EntityDoesNotExist);
            }

            string privateKey = _encryptionService.DecryptAesString(wallet.EncryptedPrivateKey, _passwordBytes);
            SignedTransactionResponse signedTransaction = await _internalSignServiceCaller.SignTransactionAsync(privateKey, transactionRaw);

            return signedTransaction.SignedTransaction;
        }
    }
}
