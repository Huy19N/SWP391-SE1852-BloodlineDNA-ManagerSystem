using APIGeneCare.Model.Payment.VnPay;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System.Data.Linq.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace APIGeneCare.Libararies
{
    public class MomoLibrary
    {

        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new MomoCompare());
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new MomoCompare());

        public IDictionary<string, string> GetAllRequestData()
        {
            return new Dictionary<string, string>(_requestData);
        }
        public IDictionary<string, string> GetAllResponseData()
        {
            return new Dictionary<string, string>(_responseData);
        }
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }
        public string GetResponseData(string key)
        {
            return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
        }
        private String GetSignature(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private bool ValidateSignature(string signature, string text, string key)
        {
            var rspRaw = _responseData;
            rspRaw.Add("accessKey", "");
            if (_responseData.ContainsKey("signature"))
            {
                _responseData.Remove("signature");
            }
            if (_responseData.ContainsKey("partnerUserId"))
            {
                _responseData.Remove("partnerUserId");
            }
            if (_responseData.ContainsKey("storeId"))
            {
                _responseData.Remove("storeId");
            }
            if (_responseData.ContainsKey("paymentOption"))
            {
                _responseData.Remove("paymentOption");
            }
            if (_responseData.ContainsKey("userFee"))
            {
                _responseData.Remove("userFee");
            }
            if (_responseData.ContainsKey("promotionInfo"))
            {
                _responseData.Remove("promotionInfo");
            }



            var expectedSignature = GetSignature(text, key);
            return expectedSignature.Equals(signature, StringComparison.OrdinalIgnoreCase);
        }

        public string CreateRequestUrl(string baseUrl, string momoHashSecret)
        {
            var data = new StringBuilder();

            foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
            {
                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
            }

            var querystring = data.ToString();

            baseUrl += "?" + querystring;
            var signData = querystring;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }

            var Signature = GetSignature(baseUrl, momoHashSecret);

            baseUrl += "signature=" + Signature;
            this.AddRequestData("signature", Signature);

            return baseUrl;
        }
        //public PaymentResponseModel GetFullResponseData(IQueryCollection collection, string hashSecret)
        //{
        //    foreach (var (key, value) in collection)
        //    {
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            this.AddResponseData(key, value);
        //        }
        //    }

        //    var orderId = Convert.ToInt64(this.GetResponseData("orderId"));
        //    var orderInfo = this.GetResponseData("orderInfo");

        //    var transactionId = this.GetResponseData("transId");
        //    var resultCode = this.GetResponseData("resultCode");

        //    var signature =
        //        collection.FirstOrDefault(k => k.Key == "signature").Value; //hash của dữ liệu trả về
        //    var checkSignature = ValidateSignature(vnpSecureHash, hashSecret); //check Signature
        //    if (!checkSignature)
        //        return new PaymentResponseModel()
        //        {
        //            Success = false
        //        };
        //    return new PaymentResponseModel()
        //    {
                
        //    };
        //}

        internal class MomoCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                var MomoCompare = CompareInfo.GetCompareInfo("en-US");
                return MomoCompare.Compare(x, y, CompareOptions.Ordinal);
            }
        }
    }
}
