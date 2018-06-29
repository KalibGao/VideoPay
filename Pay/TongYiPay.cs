using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using VideoPay.Utils;

namespace VideoPay.Pay
{
    /// <summary>
    /// web: http://www.linkillybb.cn
    /// </summary>
    public class TongYiPay
    {
        private readonly IConfiguration _configuration;

        public TongYiPay(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string AppId => _configuration["Payment:TongYiFu:AppId"];

        private string Key => _configuration["Payment:TongYiFu:Key"];

        public string BaseUri => _configuration["Payment:TongYiFu:BaseUri"];

        private string NotifyUri => _configuration["Payment:TongYiFu:NotifyUri"];

        private SortedDictionary<string, string> QueryParameters { get; set; } = new SortedDictionary<string, string>();

        private Random Random { get; set; } = new Random(DateTime.Now.Millisecond);


        public string ToUri(string orderNo, string payType, int amount, string itemName)
        {
            PrepareParameters(orderNo, payType, amount, itemName);

            StringBuilder queryBuilder = new StringBuilder();

            foreach (var keyValuePair in QueryParameters)
            {
                queryBuilder.Append($"&{keyValuePair.Key}={keyValuePair.Value}");
            }
            if (queryBuilder.ToString().StartsWith('&'))
            {
                queryBuilder.Replace('&', '?', 0, 1);
            }

            return BaseUri + queryBuilder.ToString();
        }

        public string ToFormContent(string orderNo, string payType, int amount, string itemName)
        {
            PrepareParameters(orderNo, payType, amount, itemName);

            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append($"<form id=\"Form1\" name=\"Form1\" method=\"post\" action=\"{BaseUri}\">");
            foreach (var keyValuePair in QueryParameters)
            {
                contentBuilder.Append($"<input type=\"hidden\" name=\"{keyValuePair.Key}\" value=\"{keyValuePair.Value}\"/>");
            }
            contentBuilder.Append("</form>");
            contentBuilder.Append("<script>");
            contentBuilder.Append("document.Form1.submit();");
            contentBuilder.Append("</script>");

            return contentBuilder.ToString();
        }

        private void PrepareParameters(string orderNo, string payType, int amount, string itemName)
        {
            if (QueryParameters.Count > 0)
            {
                QueryParameters.Clear();
            }

            QueryParameters["pay_memberid"] = AppId;
            QueryParameters["pay_orderid"] = orderNo;
            QueryParameters["pay_amount"] = (amount / 100.0f).ToString("F2");
            QueryParameters["pay_applydate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QueryParameters["pay_bankcode"] = ToBankCode(payType);
            QueryParameters["pay_notifyurl"] = NotifyUri;
            QueryParameters["pay_callbackurl"] = "http://www.baidu.com";
            QueryParameters["pay_md5sign"] = CalculateSign();
            QueryParameters["tongdao"] = ToTongDaoCode(payType);
        }

        private static string ToBankCode(string payType)
        {
            string bankCode = string.Empty;
            switch (payType.ToLower())
            {
                case "weixin":
                    bankCode = "Wxzf";
                    break;
                case "alipay":
                    bankCode = "ALIPAY";
                    break;
                default:
                    bankCode = "Wxzf";
                    break;
            }
            return bankCode;
        }

        private static string ToTongDaoCode(string payType)
        {
            string tongDaoCode = string.Empty;
            switch (payType.ToLower())
            {
                case "weixin":
                    tongDaoCode = "WxWap";
                    break;
                case "alipay":
                    tongDaoCode = "ZfbWap1";
                    break;
                default:
                    tongDaoCode = "WxWap";
                    break;
            }
            return tongDaoCode;
        }

        private string CalculateSign()
        {
            // var sortedDictionary = new SortedDictionary<string, string>(QueryParameters);
            StringBuilder signValue = new StringBuilder();

            foreach (var keyValuePair in QueryParameters)
            {
                signValue.Append($"{keyValuePair.Key}={keyValuePair.Value}&");
            }
            signValue.Append($"key={Key}");
            Console.WriteLine(signValue.ToString());
            return EncryptHelper.MD5Encypt(signValue.ToString(), "X2");
        }

    }
}