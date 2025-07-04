// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Libararies;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.VnPay;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Mono.TextTemplating.CodeCompilation;
using Org.BouncyCastle.Asn1.X9;
using System.Data.Common;
using System.Text.Json;

namespace APIGeneCare.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GeneCareContext _context;
        private readonly IConfiguration _configuration;
        public PaymentRepository(GeneCareContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        public IEnumerable<PaymentMethod> GetAllPaymentMethods()
            => _context.PaymentMethods.Select(pm => new PaymentMethod
            {
                PaymentMethodId = pm.PaymentMethodId,
                MethodName = pm.MethodName,
                Description = pm.Description,
                EndpointUrl = pm.EndpointUrl,
                IconUrl = pm.IconUrl,
            });
        
        public PaymentMethod? GetPaymentMethodById(decimal id)
            => _context.PaymentMethods.FirstOrDefault(pm => pm.PaymentMethodId == id);

        public IEnumerable<KeyVersionDTO> GetAllKeyVersionsByMethodId(decimal methodId)
            => _context.KeyVersions.Where(kv => kv.PaymentMethodId == methodId).Select(kv => new KeyVersionDTO
            {
                KeyVersionId = kv.KeyVersionId,
                PaymentMethodId = kv.PaymentMethodId,
                Version = kv.Version,
                HashSecret = kv.HashSecret,
                TmnCode = kv.TmnCode,
                CreatedAt = kv.CreatedAt,
                ExpiredAt = kv.ExpiredAt,
                IsActive = kv.IsActive
            });

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
                    Description = paymentMethod.Description,
                    EndpointUrl = paymentMethod.EndpointUrl,
                    IconUrl = paymentMethod.IconUrl
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool CreateKeyVersion(KeyVersionDTO keyVersion)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (keyVersion == null)
                {
                    return false;
                }
                foreach (var x in _context.KeyVersions.Where(x => x.PaymentMethodId == keyVersion.PaymentMethodId))
                {
                    if (x.ExpiredAt == null ) x.ExpiredAt = DateTime.Now;
                    x.IsActive = false;
                }
                _context.SaveChanges();

                _context.KeyVersions.Add(new KeyVersion
                {
                    PaymentMethodId = keyVersion.PaymentMethodId,
                    Version = keyVersion.Version,
                    HashSecret = keyVersion.HashSecret,
                    TmnCode = keyVersion.TmnCode,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool UpdateKeyVersion(KeyVersionDTO keyVersion)
        {
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    if (keyVersion == null || keyVersion.IsActive)
                    {
                        return false;
                    }
                    var existingkeyVersion = _context.KeyVersions.Find(keyVersion.KeyVersionId);
                    if (existingkeyVersion == null)
                    {
                        return false;
                    }
                    existingkeyVersion.Version = keyVersion.Version;
                    if(!keyVersion.IsActive && existingkeyVersion.ExpiredAt == null)
                    {
                        existingkeyVersion.ExpiredAt = DateTime.Now;
                    }
                    existingkeyVersion.IsActive = keyVersion.IsActive;
                    _context.KeyVersions.Update(existingkeyVersion);

                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<PaymentDTO> GetAllPayments()
            => _context.Payments.Select(p => new PaymentDTO
            {
                PaymentId = p.PaymentId,
                BookingId = p.BookingId,
                KeyVersionId = p.KeyVersionId,
                TransactionId = p.TransactionId,
                Amount = p.Amount,
                Currency = p.Currency,
                PaymentDate = p.PaymentDate,
                BankCode = p.BankCode,
                OrderInfo = p.OrderInfo,
                ResponseCode = p.ResponseCode,
                SecureHash = p.SecureHash,
                RawData = p.RawData,
                HavePaid = p.HavePaid
            });

        public decimal GetTotalAmount(int type)
        {
            try
            {
                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;
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
                                    p.PaymentDate.Month <= endMonth)
                        .Sum(p => p.Amount);
                }
                else if (type == 3)
                {
                    return _context.Payments
                        .Where(p => p.PaymentDate.Month == currentMonth)
                        .Sum(p => p.Amount);
                }
                return _context.Payments
                    .Where(p => p.PaymentDate.Day == DateTime.Now.Day &&
                                p.PaymentDate.Month == currentMonth &&
                                p.PaymentDate.Year == currentYear)
                    .Sum(p => p.Amount);
            }
            catch
            {
                return 0;
            }
        }

        public bool TestCreatePayment(PaymentDTO payment)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Payments.Add(new Payment
                {
                    BookingId = 1,
                    KeyVersionId = 1,
                    TransactionId = DateTime.Now.ToString(),
                    Amount = 1000m,
                    Currency = "VND",
                    PaymentDate = DateTime.Now,
                    BankCode = "00",
                    OrderInfo = "muuu",
                    ResponseCode = "01",
                    SecureHash = "aa",
                    RawData = "saa",
                    HavePaid = true
                });
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        #region payment with VnPay
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                var tick = DateTime.Now.Ticks.ToString();
                var pay = new VnPayLibrary();

                pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
                pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
                pay.AddRequestData("vnp_TmnCode", _context.KeyVersions.FirstOrDefault(x => x.PaymentMethodId == model.PaymentMethodId && x.IsActive).TmnCode);
                pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{model.Name} paid {model.Amount} for booking id {model.bookingId}");
                pay.AddRequestData("vnp_OrderType", model.OrderType);
                pay.AddRequestData("vnp_ReturnUrl", _context.PaymentMethods.FirstOrDefault(x => x.PaymentMethodId == model.PaymentMethodId).EndpointUrl);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl =
                    pay.CreateRequestUrl(_context.PaymentMethods.FirstOrDefault(x => x.PaymentMethodId == model.PaymentMethodId).EndpointUrl
                                        ,_context.KeyVersions.FirstOrDefault(x => x.PaymentMethodId == model.PaymentMethodId && x.IsActive).HashSecret);

                var requestData = pay.GetRequestData();
                var keyVersion = _context.KeyVersions.FirstOrDefault(x => x.PaymentMethodId == model.PaymentMethodId && x.IsActive);
                if (keyVersion == null)
                    throw new Exception("Active KeyVersion not found for PaymentMethodId: " + model.PaymentMethodId);

                requestData.TryGetValue("vnp_OrderInfo", out var orderInfo);
                requestData.TryGetValue("vnp_SecureHash", out var secureHash);

                var payment = new Payment
                {
                    BookingId = model.bookingId,
                    KeyVersionId = keyVersion.KeyVersionId,
                    TransactionId = tick,
                    Amount = model.Amount,
                    Currency = _configuration["Vnpay:CurrCode"],
                    PaymentDate = timeNow,
                    BankCode = null,
                    OrderInfo = orderInfo,
                    ResponseCode = null,
                    SecureHash = secureHash,
                    RawData = JsonSerializer.Serialize(requestData),
                    HavePaid = false
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();
                transaction.Commit();
                return paymentUrl;
            }
            catch (Exception ex)
            {

                transaction.Rollback();
                return null!;
            }

            
        }
        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            
            return response;
        }
        #endregion
    }
}
