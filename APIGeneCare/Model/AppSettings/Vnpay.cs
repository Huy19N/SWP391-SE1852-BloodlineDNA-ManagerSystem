namespace APIGeneCare.Model.AppSettings
{
    public class Vnpay
    {
        public string TmnCode { get; set; } = string.Empty;
        public string Command { get; set; } = string.Empty;
        public string CurrCode { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string HashSecret { get; set; } = string.Empty;
        public string EndpointURL { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
