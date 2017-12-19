using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Models;
using Lykke.Service.BlockchainSignService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.BlockchainSignService.Controllers
{
    [Route("api/wallet")]
    public class WalletController : Controller
    {
        private readonly IWalletGeneratorService _walletGeneratorService;

        public WalletController(IWalletGeneratorService walletGeneratorService)
        {
            _walletGeneratorService = walletGeneratorService;
        }

        [HttpPost]
        [SwaggerOperation("CreateWallet")]
        [ProducesResponseType(typeof(WalletCreationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWalletAsync()
        {
            WalletCreationResult walletCreationResult = await _walletGeneratorService.CreateWallet();

            return Ok(new WalletCreationResponse()
            {
                PublicAddress = walletCreationResult.PublicAddress,
                WalletId = walletCreationResult.WalletId
            });
        }
    }
}
