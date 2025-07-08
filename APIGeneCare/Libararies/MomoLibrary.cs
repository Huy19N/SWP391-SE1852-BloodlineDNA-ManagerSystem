using APIGeneCare.Model.Payment.Momo;
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
            _requestData.Add(key, value);
        }
        public void AddResponseData(string key, string value)
        {
            _responseData.Add(key, value);

        }
        public string GetRequestData(string key)
        {
            return _requestData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
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
        private bool ValidateSignatureCreateLink(string signature, string hashKey)
        {
            var rspRaw = _responseData;
            var data = new StringBuilder();

            if (!rspRaw.ContainsKey("accessKey"))
            {
                rspRaw.Add("accessKey", this.GetRequestData("accessKey"));
            }
            if (rspRaw.ContainsKey("deeplink"))
            {
                rspRaw.Remove("deeplink");
            }
            if (rspRaw.ContainsKey("qrCodeUrl"))
            {
                rspRaw.Remove("qrCodeUrl");
            }
            if (rspRaw.ContainsKey("deeplinkMiniApp"))
            {
                rspRaw.Remove("deeplinkMiniApp");
            }
            if (rspRaw.ContainsKey("signature"))
            {
                rspRaw.Remove("signature");
            }
            if (rspRaw.ContainsKey("userFee"))
            {
                rspRaw.Remove("userFee");
            }

            foreach (var (key, value) in _responseData)
            {
                data.Append(key + "=" + value + "&");
            }

            //remove last '&'
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }

            var myChecksum = GetSignature(data.ToString(), hashKey);
            if(!myChecksum.Equals(signature, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }


            return true;
        }
        public string GenerateSignature(string momoHashSecret)
        {
            var data = new StringBuilder();
            
            foreach (var (key, value) in _requestData)
            {
                data.Append(key + "=" + value + "&");
            }

            data.Remove(data.Length - 1, 1); // Remove the last '&'

            var querystring = data.ToString();


            var Signature = GetSignature(querystring, momoHashSecret);

            return Signature;
        }
        public MomoResponseModel? GetFullResponseDataOfCreateLink(IQueryCollection collection, string hashSecret)
        {
            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    this.AddResponseData(key, value);
                }
            }

            var orderId = Convert.ToInt64(this.GetResponseData("orderId"));
            var orderInfo = this.GetResponseData("orderInfo");

            var transactionId = this.GetResponseData("transId");
            var resultCode = this.GetResponseData("resultCode");

            var signature =
                collection.FirstOrDefault(k => k.Key == "signature").Value; //hash của dữ liệu trả về
            var checkSignature = ValidateSignatureCreateLink(signature!, hashSecret); //check Signature
            
            if (!checkSignature)
                return null;
            return new MomoResponseModel
            {
                partnerCode = this.GetResponseData("partnerCode"),
                orderId = orderId.ToString(),
                requestId = this.GetResponseData("requestId"),
                amount = Convert.ToDecimal(this.GetResponseData("amount")),
                responseTime = Convert.ToInt64(this.GetResponseData("responseTime")),
                message = this.GetResponseData("message"),
                resultCode = resultCode,
                payUrl = this.GetResponseData("payUrl"),
                deeplink = this.GetResponseData("deeplink"),
                qrCodeUrl = this.GetResponseData("qrCodeUrl"),
                deeplinkMiniApp = this.GetResponseData("deeplinkMiniApp"),
                signature = signature,
                userFee = Convert.ToDecimal(this.GetResponseData("userFee"))
            };
        }

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
