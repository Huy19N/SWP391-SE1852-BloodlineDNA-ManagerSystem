namespace APIGeneCare.Model.AppSettings
{
    public class GoogleLoginSettings
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string ReturnAfterLogin { get; set; } = string.Empty;
    }
}
