using TestDB;
UserDAO userDAO = new UserDAO();

UserDTO user = userDAO.GetUser(1);
Console.WriteLine("User retrieval completed. Check the console for any errors or results.");
Console.WriteLine(user);