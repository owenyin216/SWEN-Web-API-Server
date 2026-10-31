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
            if (age > 0 && age <120)
            {
                throw new Exception("Alter ist unplausibel.");
            }

            _userRepository.AddUser(newUser);
        }

        public User? GetUser(int userId)
        {
            return _userRepository.GetUserByID(userId);
        }
    }
}
