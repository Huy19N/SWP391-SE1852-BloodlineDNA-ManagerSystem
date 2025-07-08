namespace APIGeneCare.Model.Payment
{
    public class PaymentInformationModel
    {
        public int PaymentMethodId { get; set; } // payment method 
        public string? OrderType { get; set; } // random
        public decimal Amount { get; set; } //currence
        public int BookingId { get; set; } //booking id
        public string Email { get; set; } // user email

    }
}
