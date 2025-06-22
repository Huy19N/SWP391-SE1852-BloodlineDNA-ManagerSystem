using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        int GetTotalAmount(int type);
    }
}
