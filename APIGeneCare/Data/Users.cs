using System.Data;

namespace APIGeneCare.Data
{
    public class Users
    {
        public int UserId { get; set; }
        public int RoleID { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

    }
}
