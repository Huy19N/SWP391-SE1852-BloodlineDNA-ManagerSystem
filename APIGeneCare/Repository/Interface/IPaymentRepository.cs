using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<PaymentDTO> GetAllPayments();
        int GetTotalAmount(int type);
    }
}
