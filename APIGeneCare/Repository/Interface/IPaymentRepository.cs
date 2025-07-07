using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.VnPay;

namespace APIGeneCare.Repository.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<PaymentMethod> GetAllPaymentMethods();
        PaymentMethod? GetPaymentMethodById(decimal id);
        bool CreatePaymentMethod(PaymentMethodDTO paymentMethod);
        IEnumerable<PaymentDTO> GetAllPayments();
        decimal GetTotalAmount(int type);
        string CreateVNPayPaymentUrl(PaymentInformationModel model, HttpContext context);
        string VNPayPaymentResponse(IQueryCollection collections);
        PaymentResponseModel VNpayPaymentIPN(IQueryCollection collections);
    }
}
