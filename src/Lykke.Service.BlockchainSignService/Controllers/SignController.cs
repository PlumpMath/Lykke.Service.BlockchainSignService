using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Models;
using Lykke.Service.BlockchainSignService.Models.Requests;
using Lykke.Service.BlockchainSignService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.BlockchainSignService.Controllers
{
    [Route("api/sign")]
    public class SignController : Controller
    {
        private readonly ISignService _signService;

        public SignController(ISignService signService)
        {
            _signService = signService;
        }

        [HttpPost]
        [SwaggerOperation("SignTransaction")]
        [ProducesResponseType(typeof(SignTransactionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWalletAsync(SignTransactionRequest signTransactionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponse.Create(ModelState));
            }

            string signedTransaction = 
                await _signService.SignTransactionAsync(signTransactionRequest.WalletId, signTransactionRequest.TransactionHex);

            return Ok(new SignTransactionResponse()
            {
                SignedTransaction = signedTransaction
            });
        }
    }
}
