namespace APIGeneCare.Model.AppSettings
{
    public class EmailSettings
    {
        public  string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 0;
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public double ExpiredConfirmAt { get; set; } = 5; // thời gian hết hạn xác nhận email (phút)
    }
}
