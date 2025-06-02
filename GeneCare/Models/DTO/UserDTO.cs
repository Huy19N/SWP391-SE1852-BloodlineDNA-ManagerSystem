namespace GeneCare.Models.DTO
{
    public class UserDTO
    {
        private int userid;
        private RoleDTO role;
        private string fullname;
        private string address;
        private string email;
        private string phone;
        private string password;

        public UserDTO() { }

        public UserDTO(int userid, RoleDTO role, string fullname, string address, string email, string phone, string password)
        {
            this.userid = userid;
            this.role = role;
            this.fullname = fullname;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.password = password;
        }
        public int UserId
        {
            get { return userid; }
            set { userid = value; }
        }
        public RoleDTO Role
        {
            get { return role; }
            set { role = value; }
        }
        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
