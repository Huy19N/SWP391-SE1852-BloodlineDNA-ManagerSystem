namespace GeneCare.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

       
        private bool _errorLoginEmailPassword = false;
        public bool ErrorLoginEmaiPassword
        {
            get => _errorLoginEmailPassword;
            set => _errorLoginEmailPassword = value;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
