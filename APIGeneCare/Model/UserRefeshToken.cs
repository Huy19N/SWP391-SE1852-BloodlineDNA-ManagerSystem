using APIGeneCare.Model.DTO;

namespace APIGeneCare.Model
{
    public class UserRefeshToken : UserDTO
    {
        public string? IPAddress { get; set; }

        public string? UserAgent { get; set; }
    }
}
