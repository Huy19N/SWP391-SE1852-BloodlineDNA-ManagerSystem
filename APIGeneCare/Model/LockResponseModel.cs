namespace APIGeneCare.Model
{
    public class LockResponseModel
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public DateTime? LockoutEnd { get; set; } = null;
    }
}
