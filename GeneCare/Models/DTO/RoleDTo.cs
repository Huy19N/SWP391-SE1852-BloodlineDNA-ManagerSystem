namespace GeneCare.Models.DTO
{
    public class RoleDTo
    {
        private int roleId;
        private string roleName;
        public RoleDTo() { }
        public RoleDTo(int roleId, string roleName)
        {
            this.roleId = roleId;
            this.roleName = roleName;
        }
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
    }
}
