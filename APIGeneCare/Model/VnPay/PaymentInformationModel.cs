namespace APIGeneCare.Model.VnPay
{
    public class PaymentInformationModel
    {
        public int PaymentMethodId { get; set; } // payment method 
        public string? OrderType { get; set; } // random
        public decimal Amount { get; set; } //currence
        public int bookingId { get; set; } //booking id
        public string? Name { get; set; } // user name or email

    }
}
