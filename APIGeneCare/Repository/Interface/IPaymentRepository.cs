using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.VnPay;

namespace APIGeneCare.Repository.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<PaymentMethod> GetAllPaymentMethods();
        PaymentMethod? GetPaymentMethodById(decimal id);
        IEnumerable<KeyVersionDTO> GetAllKeyVersionsByMethodId(decimal methodId);
        bool CreatePaymentMethod(PaymentMethodDTO paymentMethod);
        bool CreateKeyVersion(KeyVersionDTO keyVersion);
        bool UpdateKeyVersion(KeyVersionDTO keyVersion);
        IEnumerable<PaymentDTO> GetAllPayments();
        decimal GetTotalAmount(int type);
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        string PaymentExecute(IQueryCollection collections);
    }
}
