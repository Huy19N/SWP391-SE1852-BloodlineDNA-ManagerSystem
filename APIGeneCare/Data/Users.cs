using System.Data;

namespace APIGeneCare.Data
{
    public class Users
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public String FullName { get; set; } = string.Empty;
        public String Address { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String Phone { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
    }
}
