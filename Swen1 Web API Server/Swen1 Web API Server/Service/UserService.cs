using Semester_Projekt.Data_Access;
using Semester_Projekt.Models;

namespace Swen1_Web_API_Server.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User newUser)
        {
            var existingUser = _userRepository.GetUserByUsername(newUser.Username);
            if (existingUser != null)
            {
                throw new Exception("Dieser Benutzername ist bereits vergeben.");
            }

            var age = DateTime.Today.Year - newUser.Birthdate.Year;
            if (age < 0 || age > 120)
            {
                throw new Exception("Alter ist unplausibel.");
            }

            newUser.Created_At = DateTime.UtcNow;
            newUser.Email_Certified = false;
            newUser.Ageverified = false;
            newUser.Roles_Id = 1;
            _userRepository.AddUser(newUser);
        }
        public void RegisterAdmin(User user)
        {

        }
        public void RegisterModerator(User user)
        {

        }
        
        public User LoginUserByUsername(string username, string password)
        {
            var existingUser = _userRepository.GetUserByUsername(username);
            if (existingUser != null && password == existingUser.Password)
            {
                return existingUser;
            }
            else
            {
                throw new Exception("Username oder Passwort sind falsch oder existieren nicht");
            }
        }

        public User LoginUser(User user)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null && user.Password == existingUser.Password)
            {
                return existingUser;
            }
            else
            {
                throw new Exception("Username oder Passwort sind falsch oder existieren nicht");
            }
        }

        //maybe login by email

        public User? GetUserByID(int userId)
        {
            return _userRepository.GetUserByID(userId);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void UpdateUserProfile(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUserByID(int userId)
        {

            _userRepository.DeleteUserByID(userId);
        }

        public void DeleteUserByName(string username)
        {
            var existingUser = _userRepository.GetUserByUsername(username);

            _userRepository.DeleteUserByName(username);
        }

        public void DeleteUser(User user) 
        {
            _userRepository.DeleteUser(user);
        }
               
    }
}
