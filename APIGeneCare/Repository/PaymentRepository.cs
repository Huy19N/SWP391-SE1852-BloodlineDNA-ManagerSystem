// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Libararies;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.Payment;
using APIGeneCare.Model.Payment.VnPay;
using APIGeneCare.Repository.Interface;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Json;

namespace APIGeneCare.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GeneCareContext _context;
        private readonly AppSettings _appSettings;
        private readonly VnpaySettings _vnpay;
        private readonly MomoSettings _momo;
        private readonly FontEndSettings _fontEnd;
        public PaymentRepository(GeneCareContext context,
            IOptionsMonitor<AppSettings> appSettings,
            IOptionsMonitor<VnpaySettings> vnpayOptions,
            IOptionsMonitor<MomoSettings> momoOptions,
            IOptionsMonitor<FontEndSettings> fontEnd,
            IConfiguration configuration)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
            _vnpay = vnpayOptions.CurrentValue;
            _momo = momoOptions.CurrentValue;
            _fontEnd = fontEnd.CurrentValue;
        }

        public IEnumerable<PaymentMethod> GetAllPaymentMethods()
            => _context.PaymentMethods.Select(pm => new PaymentMethod
            {
                PaymentMethodId = pm.PaymentMethodId,
                MethodName = pm.MethodName,
                IconUrl = pm.IconUrl,
            });

        public PaymentMethod? GetPaymentMethodById(decimal id)
            => _context.PaymentMethods.FirstOrDefault(pm => pm.PaymentMethodId == id);

        public bool CreatePaymentMethod(PaymentMethodDTO paymentMethod)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (paymentMethod == null)
                {
                    return false;
                }
                _context.PaymentMethods.Add(new PaymentMethod
                {
                    MethodName = paymentMethod.MethodName,
                    IconUrl = paymentMethod.IconUrl
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public IEnumerable<PaymentDTO> GetAllPayments()
            => _context.Payments.Select(p => new PaymentDTO
            {
                PaymentId = p.PaymentId,
                BookingId = p.BookingId,
                PaymentMethodId = p.PaymentMethodId,
                TransactionStatus = p.TransactionStatus,
                ResponseCode = p.ResponseCode,
                Amount = p.Amount,
                Currency = p.Currency,
                PaymentDate = p.PaymentDate,
                BankTranNo = p.BankTranNo,
                OrderInfo = p.OrderInfo,
                TransactionNo = p.TransactionNo,
                SecureHash = p.SecureHash,
                RawData = p.RawData,
                HavePaid = p.HavePaid
            });
        public decimal GetTotalAmount(int type)
        {
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                int currentMonth = timeNow.Month;
                int currentYear = timeNow.Year;
                int quarter = (currentMonth - 1) / 3 + 1;
                int startMonth = (quarter - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                if (type == 1)
                {
                    return _context.Payments.Where(p => p.PaymentDate.Year == currentYear && p.HavePaid).Sum(p => p.Amount);
                }
                else if (type == 2)
                {
                    return _context.Payments
                        .Where(p => p.PaymentDate.Year == currentYear &&
                                    p.PaymentDate.Month >= startMonth &&
                                    p.PaymentDate.Month <= endMonth &&
                                    p.HavePaid)
                        .Sum(p => p.Amount);
                }
                else if (type == 3)
                {
                    return _context.Payments
                        .Where(p => p.PaymentDate.Month == currentMonth &&
                        p.HavePaid)
                        .Sum(p => p.Amount);
                }
                return _context.Payments
                    .Where(p => p.PaymentDate.Day == DateTime.Now.Day &&
                                p.PaymentDate.Month == currentMonth &&
                                p.PaymentDate.Year == currentYear &&
                                p.HavePaid)
                    .Sum(p => p.Amount);
            }
            catch
            {
                return 0;
            }
        }

        #region payment with VnPay
        public string CreateVNPayPaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                var tick = DateTime.Now.Ticks.ToString();
                var paymentId = tick + Guid.NewGuid().ToString();

                var pay = new VnPayLibrary();

                var payment = new Payment
                {
                    PaymentId = paymentId,
                    BookingId = model.BookingId,
                    PaymentMethodId = model.PaymentMethodId,
                    TransactionStatus = null,
                    ResponseCode = null,
                    TransactionNo = null,
                    BankTranNo = null,
                    Amount = model.Amount,
                    Currency = _vnpay.CurrCode,
                    PaymentDate = timeNow,
                    OrderInfo = null,
                    SecureHash = null,
                    RawData = null,
                    HavePaid = false,
                };
                _context.Payments.Add(payment);
                _context.SaveChanges();

                pay.AddRequestData("vnp_Version", _vnpay.Version);
                pay.AddRequestData("vnp_Command", _vnpay.Command);
                pay.AddRequestData("vnp_TmnCode", _vnpay.TmnCode);
                pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _vnpay.CurrCode);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _vnpay.Locale);
                pay.AddRequestData("vnp_OrderInfo", $"{paymentId}|{model.PaymentMethodId}|{model.Email}|{model.Amount}|{model.BookingId}");
                pay.AddRequestData("vnp_OrderType", model.OrderType ?? string.Empty);
                pay.AddRequestData("vnp_ReturnUrl", _vnpay.ReturnUrl);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl =
                    pay.CreateRequestUrl(_vnpay.EndpointURL, _vnpay.HashSecret);

                var requestData = pay.GetAllRequestData();
                requestData.TryGetValue("vnp_OrderInfo", out var orderInfo);
                requestData.TryGetValue("vnp_SecureHash", out var secureHash);

                payment = _context.Payments.FirstOrDefault(x => x.PaymentId == payment.PaymentId);

                if (payment == null)
                {
                    throw new Exception("Payment not found after creation.");
                }

                payment.OrderInfo = orderInfo?.ToString();
                payment.SecureHash = secureHash?.ToString();
                payment.RawData = JsonSerializer.Serialize(requestData);

                _context.SaveChanges();
                transaction.Commit();
                return paymentUrl;
            }
            catch
            {

                transaction.Rollback();
                throw;
            }
        }
        public string VNPayPaymentResponse(IQueryCollection collections)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                StringBuilder url = new StringBuilder(_fontEnd.ReturnAfterPay);
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                collections.TryGetValue("vnp_OrderInfo", out var orderInfo);
                var pay = new VnPayLibrary();

                var response = pay.GetFullResponseData(collections, _vnpay.HashSecret);

                if (response.Success)
                {
                    var orderInfoSplit = response.OrderInfo?.Split("|");
                    if (orderInfoSplit == null || orderInfoSplit.Length < 1)
                    {
                        throw new Exception("Invalid order information format.");
                    }
                    string paymentId = orderInfoSplit[0];

                    var paymentReturnLog = new PaymentReturnLog
                    {
                        PaymentId = paymentId,
                        RawData = JsonSerializer.Serialize(pay.GetAllResponseData()),
                        ReturnedAt = timeNow,
                        ResponseCode = response.ResponseCode ?? string.Empty,
                        TransactionStatus = response.TransactionStatus ?? string.Empty,
                    };
                    _context.PaymentReturnLogs.Add(paymentReturnLog);

                    var existingPayment = _context.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
                    if (existingPayment != null)
                    {
                        existingPayment.ResponseCode = response.ResponseCode;
                        existingPayment.TransactionStatus = response.TransactionStatus;
                        existingPayment.BankTranNo = response.BankTranNo;
                        existingPayment.TransactionNo = response.TransactionNo;

                        if (response.TransactionStatus == "00")
                        {
                            existingPayment.HavePaid = true;
                        }
                    }
                    url.Append($"?paymentId={paymentId}");
                    _context.SaveChanges();
                }

                transaction.Commit();
                return url.ToString();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

        }
        public PaymentResponseModel VNpayPaymentIPN(IQueryCollection collections)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                collections.TryGetValue("vnp_OrderInfo", out var orderInfo);
                var pay = new VnPayLibrary();

                var response = pay.GetFullResponseData(collections, _vnpay.HashSecret);

                if (response.Success)
                {
                    var orderInfoSplit = response.OrderInfo?.Split("|");
                    if (orderInfoSplit == null || orderInfoSplit.Length < 1)
                    {
                        throw new Exception("Invalid order information format.");
                    }
                    string paymentId = orderInfoSplit[0];

                    var paymentIpnlog = new PaymentIpnlog
                    {
                        PaymentId = paymentId,
                        RawData = JsonSerializer.Serialize(pay.GetAllResponseData()),
                        ReceivedAt = timeNow,
                        ResponseCode = response.ResponseCode ?? string.Empty,
                        TransactionStatus = response.TransactionStatus ?? string.Empty,
                    };
                    _context.PaymentIpnlogs.Add(paymentIpnlog);

                    var existingPayment = _context.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
                    if (existingPayment != null)
                    {
                        existingPayment.ResponseCode = response.ResponseCode;
                        existingPayment.TransactionStatus = response.TransactionStatus;
                        existingPayment.BankTranNo = response.BankTranNo;
                        existingPayment.TransactionNo = response.TransactionNo;

                        if (response.TransactionStatus == "00")
                        {
                            existingPayment.HavePaid = true;
                        }
                        else
                        {
                            existingPayment.HavePaid = false;
                        }
                    }
                    _context.SaveChanges();
                }

                transaction.Commit();
                return response!;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion
        #region payment with MoMo
        public async Task<string> CreateMomoPaymentUrlAsync(PaymentInformationModel model, HttpContext context)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                var tick = DateTime.Now.Ticks.ToString();
                var paymentId = tick + Guid.NewGuid().ToString();

                var requestId = Guid.NewGuid().ToString() + tick;
                using var httpClient = new HttpClient();

                var pay = new MomoLibrary();

                var payment = new Payment
                {
                    PaymentId = paymentId,
                    BookingId = model.BookingId,
                    PaymentMethodId = model.PaymentMethodId,
                    TransactionStatus = null,
                    ResponseCode = null,
                    TransactionNo = null,
                    BankTranNo = null,
                    Amount = model.Amount,
                    Currency = _momo.Currency,
                    PaymentDate = timeNow,
                    OrderInfo = null,
                    SecureHash = null,
                    RawData = null,
                    HavePaid = false,
                };
                _context.Payments.Add(payment);
                _context.SaveChanges();

                payment = _context.Payments.FirstOrDefault(x => x.PaymentId == payment.PaymentId);

                if (payment == null)
                {
                    throw new Exception("Payment not found after creation.");
                }

                pay.AddRequestData("accessKey", _momo.AccessKey);
                pay.AddRequestData("amount", ((long)model.Amount).ToString());
                pay.AddRequestData("extraData", "");
                pay.AddRequestData("ipnUrl", _momo.IpnUrl);
                pay.AddRequestData("orderId", payment.PaymentId.ToString());
                pay.AddRequestData("orderInfo", $"{paymentId}|{model.PaymentMethodId}|{model.Email}|{model.Amount}|{model.BookingId}");
                pay.AddRequestData("partnerCode", _momo.PartnerCode);
                pay.AddRequestData("redirectUrl", _momo.RedirectUrl);
                pay.AddRequestData("requestId", requestId);
                pay.AddRequestData("requestType", _momo.RequestType);

                var signature = pay.GenerateSignature(_momo.HashSecret);
                pay.AddRequestData("signature", signature);
                pay.AddRequestData("lang", _momo.Lang);

                var requestData = pay.GetAllRequestData();
                var JSONRequestData = JsonSerializer.Serialize(requestData);
                var content = new StringContent(JSONRequestData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
                // convert Dictionary<string, string> to IQueryCollection

                if (dict == null || !dict.ContainsKey("payUrl") || !dict.ContainsKey("resultCode"))
                {
                    throw new Exception("Invalid response from MoMo API.");
                }

                var queryDict = dict.ToDictionary(
                    kv => kv.Key,
                    kv => new StringValues(kv.Value?.ToString())
                );
                IQueryCollection queryCollection = new QueryCollection(queryDict);

                //payment.ResponseCode = momoResponseModel.resultCode;
                payment.RawData = JSONRequestData;
                payment.SecureHash = signature;

                string payUrl = queryCollection["payUrl"].ToString();
                if (string.IsNullOrEmpty(payUrl) || queryCollection["resultCode"].ToString() != "0")
                {
                    throw new Exception($"Failed to create payment URL. resultCode:{queryCollection["resultCode"].ToString()} -");
                }

                _context.SaveChanges();
                transaction.Commit();
                return payUrl;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public string MomoPaymentResponse(IQueryCollection collections)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                StringBuilder url = new StringBuilder(_fontEnd.ReturnAfterPay);
                collections.TryGetValue("orderInfo", out var orderInfo);
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);


                var pay = new MomoLibrary();

                var response = pay.GetFullResponse(collections, _momo.AccessKey, _momo.HashSecret);

                if (response != null)
                {
                    if (string.IsNullOrEmpty(response.OrderId))
                    {
                        throw new Exception("OrderId is missing in the response.");
                    }
                    string paymentId = response.OrderId;

                    var paymentReturnLog = new PaymentReturnLog
                    {
                        PaymentId = paymentId,
                        RawData = JsonSerializer.Serialize(pay.GetAllResponseData()),
                        ReturnedAt = timeNow,
                        ResponseCode = response.ResultCode ?? string.Empty,
                        TransactionStatus = response.ResultCode ?? string.Empty,
                    };
                    _context.PaymentReturnLogs.Add(paymentReturnLog);

                    var existingPayment = _context.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
                    if (existingPayment != null)
                    {
                        existingPayment.ResponseCode = response.ResultCode;
                        existingPayment.TransactionStatus = response.ResultCode;

                        existingPayment.TransactionNo = response.TransId;

                        if (response.ResultCode == "0")
                        {
                            existingPayment.HavePaid = true;
                        }
                    }
                    url.Append($"?paymentId={paymentId}");
                    _context.SaveChanges();
                }

                transaction.Commit();
                return url.ToString();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> MomoPaymentIPN(Stream _body)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                using var reader = new StreamReader(_body);
                var body = await reader.ReadToEndAsync();
                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(body);

                if (dict == null || !dict.ContainsKey("orderInfo"))
                {
                    throw new Exception("Invalid IPN data received from MoMo.");
                }

                var queryDict = dict.ToDictionary(
                    kv => kv.Key,
                    kv => new StringValues(kv.Value?.ToString())
                );

                IQueryCollection collections = new QueryCollection(queryDict);

                collections.TryGetValue("orderInfo", out var orderInfo);
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);


                var pay = new MomoLibrary();

                var response = pay.GetFullResponse(collections, _momo.AccessKey, _momo.HashSecret);

                if (response != null)
                {
                    string paymentId = response.OrderId ?? string.Empty;

                    var paymentIpnlog = new PaymentIpnlog
                    {
                        PaymentId = paymentId,
                        RawData = JsonSerializer.Serialize(pay.GetAllResponseData()),
                        ReceivedAt = timeNow,
                        ResponseCode = response.ResultCode ?? string.Empty,
                        TransactionStatus = response.ResultCode ?? string.Empty,
                    };
                    _context.PaymentIpnlogs.Add(paymentIpnlog);

                    var existingPayment = _context.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
                    if (existingPayment != null)
                    {
                        existingPayment.ResponseCode = response.ResultCode;
                        existingPayment.TransactionStatus = response.ResultCode;

                        existingPayment.TransactionNo = response.TransId;

                        if (response.ResultCode == "0")
                        {
                            existingPayment.HavePaid = true;
                        }
                    }
                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion
    }
}
