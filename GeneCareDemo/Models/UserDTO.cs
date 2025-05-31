namespace GeneCare.Models.DTO
{
    public class UserDTO
    {
        private int userid;
        private int roleid;
        private string fullname;
        private string address;
        private string email;
        private string phone;
        private string password;

        public UserDTO() { }

        public UserDTO(int userid, int roleid, string fullname, string address, string email, string phone, string password)
        {
            this.userid = userid;
            this.roleid = roleid;
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
        public int RoleId
        {
            get { return roleid; }
            set { roleid = value; }
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
