using System.Runtime.CompilerServices;

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

        private String _errorRegister;
        public String ErrorRegister
        {
            get => _errorRegister;
            set => _errorRegister = value;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
