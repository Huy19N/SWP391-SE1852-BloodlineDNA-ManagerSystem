namespace APIGeneCare.Model.Payment.Momo
{
    public class MomoResponseModel
    {
        public string? partnerCode { get; set; }
        public string? requestId { get; set; }
        public string? orderId { get; set; }
        public decimal amount { get; set; }
        public long responseTime { get; set; }
        public string? message { get; set; }
        public string? resultCode { get; set; }
        public string? payUrl { get; set; }
        public string? deeplink { get; set; }
        public string? qrCodeUrl { get; set; }
        public string? deeplinkMiniApp { get; set; }
        public string? signature { get; set; }
        public decimal userFee { get; set; }
    }
}
