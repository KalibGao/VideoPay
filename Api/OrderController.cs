using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger _logger;
        public OrderController(TongYiPay tongYiPay,
            OrderDbContext dbContext,
            ILogger<OrderController> logger)
        {
            _tongYiPay = tongYiPay ?? throw new ArgumentNullException(nameof(tongYiPay));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("pay")]
        public async Task<IActionResult> Pay([Required] string payType, [Required] string orderNo, [Required] int orderAmt, string sn = "", string itemName = "")
        {
            if (orderAmt < 100)
            {
                return BadRequest(JsonConvert.SerializeObject(Envelope.Error(1, "order amount can not less than 100.")));
            }

            if (_dbContext.Orders.Any(o => o.OrderNo == orderNo))
            {
                return BadRequest(JsonConvert.SerializeObject(Envelope.Error(1, "order exists.")));
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
        public IActionResult Query([Required] string orderNo)
        {
            if (string.IsNullOrWhiteSpace(orderNo))
            {
                return BadRequest(JsonConvert.SerializeObject(Envelope.Error(1, "order cannot empty.")));
            }
            var order = _dbContext.Orders.Where(o => o.OrderNo == orderNo).FirstOrDefault();

            if (order == null)
            {
                return BadRequest(JsonConvert.SerializeObject(Envelope.Error(1, "order not exists.")));
            }

            return Ok(JsonConvert.SerializeObject(Envelope.Ok(new { Order = order.OrderNo, HasPaid = order.HasPaid })));
        }

        [HttpGet("callback/tongyifu")]
        public void Callback_TongYiFu([Required] string orderid, [Required] string returncode, string memberid, string datetime)
        {
            _logger.LogWarning($"callback enterd, order: {orderid} callback error, returncode: {returncode}, memberid: {memberid}, datetime: {datetime}");
            var order = _dbContext.Orders.Where(o => o.OrderNo == orderid).FirstOrDefault();
            if (order == null || returncode != "00")
            {
                _logger.LogWarning($"order: {orderid} callback error, returncode: {returncode}, memberid: {memberid}, datetime: {datetime}");
            }
            else
            {
                order.HasPaid = true;
                order.LastUpdateTime = DateTime.Now;

                _dbContext.Orders.Update(order);
                _dbContext.SaveChanges();
            }
        }

    }
}