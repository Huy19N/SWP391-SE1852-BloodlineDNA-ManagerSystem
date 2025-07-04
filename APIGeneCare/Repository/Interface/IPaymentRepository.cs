using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.VnPay;

namespace APIGeneCare.Repository.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<PaymentDTO> GetAllPayments();
        int GetTotalAmount(int type);
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
