using System.ComponentModel;
using Microsoft.Data.SqlClient;
using GeneCare.Models.Utils;
using System.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace GeneCare.Models
{
    public class Users : DbContext
    {
        private int _userId;
        private Role _role;
        private String _fullName;
        private String _address;
        private String _email;
        private String _phone;
        private String _password;

        #region Properties
        public Users() {
            _role = new Role();
            _fullName = string.Empty;
            _address = string.Empty;
            _email = string.Empty;
            _phone = string.Empty;
            _password = string.Empty;
        }
        public Users(int userId, Role role, String fullName, String address, String email, String phone, String password)
        {
            _userId = userId;
            _role = role;
            _fullName = fullName;
            _address = address;
            _email = email;
            _phone = phone;
            _password = password;
        }
        #endregion

        #region Getters and Setters
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public Role Role
        {
            get { return _role; }
            set { _role = value; }
        }
        public String FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        public String Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion

        #region DAO
        public Users? getUser(int? userId, String? email, String? password)
        {
            var query = "SELECT * FROM Users WHERE UserID = @uerId OR ( Email = @Email AND Password = @Password )";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId.HasValue ? userId : 0;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _userId = reader.GetInt32("UserID");
                                _role.getRole(reader.GetInt32("RoleID"), null);
                                _fullName = reader.GetString("FullName");
                                _address = reader.GetString("Address");
                                _email = reader.GetString("Email");
                                _phone = reader.GetString("Phone");
                                _password = reader.GetString("Password");
                            }
                            return this;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                return null;
            }
        }
        public Boolean addUser()
        {
            var query = "INSERT INTO Users (RoleID, FullName, Address, Email, Phone, Password) VALUES (@RoleId, @FullName, @Address, @Email, @Phone, @Password)";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = _role.RoleId;
                    cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = _fullName;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _address;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _email;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _phone;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = _password;
                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public Boolean updateUser (Users user)
        {
            var query = "UPDATE Users SET FullName = @FullName, Address = @Address, Email = @Email, Phone = @Phone, Password = @Password WHERE UserID = @UserId";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = user.Address;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = user.Phone;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public Dictionary<int,Users>? getUserList()
        {
            var query = "SELECT * FROM Users";
            using(SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Dictionary<int, Users> userList = new Dictionary<int, Users>();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Users user = new Users()
                                    {
                                        UserId = reader.GetInt32("UserID"),
                                        Role = new Role().getRole(reader.GetInt32("RoleID"), null),
                                        FullName = reader.GetString("FullName"),
                                        Address = reader.GetString("Address"),
                                        Email = reader.GetString("Email"),
                                        Phone = reader.GetString("Phone"),
                                        Password = reader.GetString("Password")
                                    };
                                    userList.Add(user.UserId, user);
                                }
                                return userList;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return null;
        }
        public Boolean deleteUser(int userId)
        {
            var query = "DELETE FROM Users WHERE UserID = @UserId";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        #endregion
    }
}
