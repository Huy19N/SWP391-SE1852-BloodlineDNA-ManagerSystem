using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GeneCareContext _context;
        public PaymentRepository(GeneCareContext context) => _context = context;
        public IEnumerable<Payment> GetAllPayments()
            => _context.Payments.ToList();

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
            
    }
}
