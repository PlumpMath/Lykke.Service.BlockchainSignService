using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Services
{
    public class WalletGeneratorService : IWalletGeneratorService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IInternalSignServiceCaller _internalSignServiceCaller;
        private readonly IEncryptionService _encryptionService;
        private readonly byte[] _passwordBytes;

        public WalletGeneratorService(
            IWalletRepository walletRepository,
            IInternalSignServiceCaller internalSignServiceCaller,
            IEncryptionService encryptionService,
            byte[] passwordBytes
            )
        {
            _walletRepository = walletRepository;
            _internalSignServiceCaller = internalSignServiceCaller;
            _encryptionService = encryptionService;
            _passwordBytes = passwordBytes;
        }

        public async Task<WalletCreationResult> CreateWallet()
        {
            Guid walletId = Guid.NewGuid();

            KeyModelResponse response = await _internalSignServiceCaller.CreateWalletAsync();
            string encryptedPrivateKey = 
                _encryptionService.EncryptAesString(response.PrivateKey, _passwordBytes);
            await _walletRepository.SaveAsync(new Wallet()
            {
                EncryptedPrivateKey = encryptedPrivateKey,
                PublicAddress = response.PublicAddress,
                WalletId = walletId,
            });

            return new WalletCreationResult()
            {
                PublicAddress = response.PublicAddress,
                WalletId = walletId
            };
        }
    }
}
