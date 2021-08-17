using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using VirtualMind.Model;
using VirtualMind.Service;

namespace VirtualMind.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VirtualMindController : ControllerBase
    {
        private readonly ILogger<VirtualMindController> _logger;
        private readonly PurchaseService _purchasService;
        private readonly LimitService _limitService;
        private const string apiQuoteExternal = "http://www.bancoprovincia.com.ar/Principal/Dolar";

        public VirtualMindController(ILogger<VirtualMindController> logger, PurchaseService purchasService, LimitService limitService)
        {
            _logger = logger;
            _purchasService = purchasService;
            _limitService = limitService;
        }

        [HttpGet]
        [Route("/quote/{currency}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetQuote(Currency currency)
        {
            if (ModelState.IsValid)
            {
                decimal exchange = this.GetExchange(currency);
                if (exchange > 0) return Ok(exchange);
                else return BadRequest("Invalid Currency");
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("/purchase")]

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SavePurchase([FromBody] ExchageDto exchageDto)
        {
            if (ModelState.IsValid)
            {
                decimal exchange = this.GetExchange(exchageDto.Currency);
                if (exchange > 0)
                {
                    SeedData.Initialize();
                    var list = this._limitService.GetRange(exchageDto.UserId, exchageDto.Currency, DateTime.Now);
                    var converted = exchageDto.Amount / exchange;

                    if (list.Any() && list.Where(x => x.Amount >= converted).Any())
                    {
                        Purchase entity = new Purchase();
                        entity.Currency = exchageDto.Currency;
                        entity.Amount = exchageDto.Amount;
                        entity.UserId = entity.UserId;
                        entity.Exchange = exchange;
                        entity.Date = DateTime.Now;
                        entity.Result = converted;
                        return Ok(this._purchasService.AddPurchase(entity));
                    }
                    else return BadRequest("Invalid Range");
                }
                else return BadRequest("Invalid Currency");
            }
            return BadRequest(ModelState);
        }

        private decimal GetExchange(Currency currency)
        {
            var response = JsonConvert.DeserializeObject<List<string>>(this.GetHttpRequest().Result);
            decimal exchange = 0;
            switch (currency)
            {
                case Currency.USD:
                    exchange = Convert.ToDecimal(response[0]);
                    break;
                case Currency.BRL:
                    exchange = (Convert.ToDecimal(response[0]) / 4);
                    break;
                default:
                    exchange = 0;
                    break;
            }
            return exchange;
        }

        private async Task<string> GetHttpRequest()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiQuoteExternal);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
