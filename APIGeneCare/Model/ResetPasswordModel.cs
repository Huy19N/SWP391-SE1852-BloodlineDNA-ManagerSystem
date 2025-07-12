namespace APIGeneCare.Model
{
    public class ResetPasswordModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
