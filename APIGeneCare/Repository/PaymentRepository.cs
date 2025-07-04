<<<<<<< Updated upstream
﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
=======
﻿using APIGeneCare.Entities;
using APIGeneCare.Libararies;
>>>>>>> Stashed changes
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public PaymentRepository(GeneCareContext context) => _context = context;
        public IEnumerable<PaymentDTO> GetAllPayments()
            => _context.Payments.Select(p => new PaymentDTO
            {
                PaymentId = p.PaymentId,
                BookingId = p.BookingId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status
            }).ToList();
        public int GetTotalAmount(int type)
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
                    return _context.Payments
                        .Where(p => p.PaymentDate.HasValue &&
                                    p.PaymentDate.Value.Year == currentYear)
                        .Sum(p => p.Amount) ?? 0;
                }
                else if (type == 2)
                {
                    return _context.Payments
                        .Where(p => p.PaymentDate.HasValue &&
                                    p.PaymentDate.Value.Year == currentYear &&
                                    p.PaymentDate.Value.Month >= startMonth &&
                                    p.PaymentDate.Value.Month <= endMonth)
                        .Sum(p => p.Amount) ?? 0;
                }
                else if (type == 3)
                {
                    return _context.Payments
                        .Where(p => p.PaymentDate.HasValue &&
                                   p.PaymentDate.Value.Month == currentMonth)
                        .Sum(p => p.Amount) ?? 0;
                }
                return _context.Payments
                    .Where(p => p.PaymentDate.HasValue &&
                                p.PaymentDate.Value.Day == DateTime.Now.Day &&
                                p.PaymentDate.Value.Month == currentMonth &&
                                p.PaymentDate.Value.Year == currentYear)
                    .Sum(p => p.Amount) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

<<<<<<< Updated upstream
=======
        #region payment with VnPay
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]??"");
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"] ?? "");
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"] ?? "");
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"] ?? "");
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"] ?? "");
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"] ?? "");
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType ?? "");
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:ReturnUrl"] ?? "");
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"] ?? "", _configuration["Vnpay:HashSecret"] ?? "");

            return paymentUrl;
        }
        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"] ?? "");
            
            return response;
        }
        #endregion
>>>>>>> Stashed changes
    }
}
