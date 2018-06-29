using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoPay.Data;
using VideoPay.Dtos;
using VideoPay.Entities;
using VideoPay.Pay;

namespace VideoPay.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TongYiPay _tongYiPay;
        private readonly OrderDbContext _dbContext;
        public OrderController(TongYiPay tongYiPay,
            OrderDbContext dbContext)
        {
            _tongYiPay = tongYiPay ?? throw new ArgumentNullException(nameof(tongYiPay));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("pay")]
        public async Task<IActionResult> Pay([Required] string payType, [Required] string orderNo, [Required] int orderAmt, string sn = "", string itemName = "")
        {
            if (orderAmt < 100)
            {
                return Content(JsonConvert.SerializeObject(Envelope.Error(1, "order amount can not less than 100.")));
            }

            if (_dbContext.Orders.Any(o => o.OrderNo == orderNo))
            {
                return Content(JsonConvert.SerializeObject(Envelope.Error(1, "order exists.")));
            }

            var order = new Order
            {
                OrderNo = orderNo,
                OrderAmt = orderAmt,
                PayType = payType,
                ItemName = itemName,
                SN = sn
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            string formContent = _tongYiPay.ToFormContent(orderNo, payType, orderAmt, itemName);
            return Content(formContent, "text/html");
        }

        [HttpGet("query")]
        public ActionResult<Envelope<string>> Query([Required] string orderNo)
        {
            return Envelope.Ok(orderNo);
        }

    }
}