using APIGeneCare.Model.Payment.Momo;
using System.Globalization;
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
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private bool ValidateSignatureCreateLink(string signature, string accessKey, string hashKey)
        {
            var rspRaw = _responseData;
            var data = new StringBuilder();

            if (!rspRaw.ContainsKey("accessKey"))
            {
                rspRaw.Add("accessKey", accessKey);
            }

            if (rspRaw.ContainsKey("storeId"))
            {
                rspRaw.Remove("storeId");
            }

            if (rspRaw.ContainsKey("partnerUserId"))
            {
                rspRaw.Remove("partnerUserId");
            }

            if (rspRaw.ContainsKey("userFee"))
            {
                rspRaw.Remove("userFee");
            }

            if (rspRaw.ContainsKey("promotionInfo"))
            {
                rspRaw.Remove("promotionInfo");
            }

            if (rspRaw.ContainsKey("paymentOption"))
            {
                rspRaw.Remove("paymentOption");
            }

            if (rspRaw.ContainsKey("signature"))
            {
                rspRaw.Remove("signature");
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
            if (!myChecksum.Equals(signature, StringComparison.InvariantCultureIgnoreCase))
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
        public MomoResponseModel? GetFullResponse(IQueryCollection collection, string accessKey, string hashSecret)
        {
            foreach (var (key, value) in collection)
            {
                this.AddResponseData(key, value);
            }

            var signature =
                collection.FirstOrDefault(k => k.Key == "signature").Value; //hash của dữ liệu trả về
            var checkSignature = ValidateSignatureCreateLink(signature, accessKey, hashSecret); //check Signature

            if (!checkSignature)
                return null;
            return new MomoResponseModel
            {
                PartnerCode = this.GetResponseData("partnerCode"),
                OrderId = this.GetResponseData("orderId"),
                Amount = Convert.ToDecimal(this.GetResponseData("amount")),
                OrderInfo = this.GetResponseData("orderInfo"),
                OrderType = this.GetResponseData("orderType"),
                TransId = this.GetResponseData("transId"),
                ResultCode = this.GetResponseData("resultCode"),
                Message = this.GetResponseData("message"),
                PayType = this.GetResponseData("payType"),
                ResponseTime = this.GetResponseData("responseTime"),
                ExtraData = this.GetResponseData("extraData"),
                Signature = this.GetResponseData("signature")
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
