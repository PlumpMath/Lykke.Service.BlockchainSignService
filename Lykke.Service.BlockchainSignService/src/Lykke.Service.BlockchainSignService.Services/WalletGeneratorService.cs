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
        private readonly AppSettings _settings;

        public WalletGeneratorService(
            IWalletRepository walletRepository,
            IInternalSignServiceCaller internalSignServiceCaller,
            IEncryptionService encryptionService,
            AppSettings settings
            )
        {
            _walletRepository = walletRepository;
            _internalSignServiceCaller = internalSignServiceCaller;
            _encryptionService = encryptionService;
            _settings = settings;
        }

        public async Task<WalletCreationResult> CreateWallet()
        {
            Guid walletId = Guid.NewGuid();

            KeyModelResponse response = await _internalSignServiceCaller.CreateWalletAsync();
            string encryptedPrivateKey = 
                _encryptionService.EncryptAesString(response.PrivateKey, _settings.BlockchainSignServiceService.PasswordBytes);
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
