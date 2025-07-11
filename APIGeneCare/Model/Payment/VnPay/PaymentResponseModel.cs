﻿namespace APIGeneCare.Model.Payment.VnPay
{
    public class PaymentResponseModel
    {
        public bool Success { get; set; }
        public string? TransactionNo { get; set; }
        public string? BankCode { get; set; }
        public string? BankTranNo { get; set; }
        public string? OrderInfo { get; set; }
        public string? TransactionId { get; set; }
        public string? ResponseCode { get; set; }
        public string? TransactionStatus { get; set; }
        public string? SecureHash { get; set; }

    }
}
