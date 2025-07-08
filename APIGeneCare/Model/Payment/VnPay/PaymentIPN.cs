namespace APIGeneCare.Model.Payment.VnPay
{
    public class PaymentIPN
    {
        public string TmnCode { get; set; }
        public double Amount { get; set; }
        public string BankCode { get; set; }
        public string BankTranNo { get; set; }
        public string CardType { get; set; }
        public DateTime PayDate { get; set; }
        public string OrderInfo { get; set; }
        public long TransactionNo { get; set; }
        public int ResponseCode { get; set; }
        public int TransactionStatus { get; set; }
        public long TxnRef { get; set; }
        public string SecureHash { get; set; }
    }
}
