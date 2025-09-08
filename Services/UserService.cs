using API.DTO;
using API.Model;
using API.Repositories;

namespace API.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetByUserIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User?> CreateUserAsync(CreateUserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Login) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return null;
            }

            var allUsers = _repository.GetAllAsync().Result;
            if (allUsers.Any(u => u.Login.Equals(dto.Login)))
            {
                return null; // Já existe
            }
            var user = new User
            {
                Name = dto.Name,
                Login = dto.Login,
                IsActive = true,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            return await _repository.CreateUserAsync(user);
        }

        public async Task<User?> InactivateUserAsync(int id, bool status)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            user.IsActive = status;
            return await _repository.UpdateUserAsync(user);
        }

        public async Task<User?> UpdateUserAsync(int id, UserUpdateDTO user)
        {
            var FindedUser = await _repository.GetByIdAsync(id);
            if (FindedUser == null) return null;
            var Newuser = new User
            {
                Id = FindedUser.Id,
                Name = user.Name,
                Login = user.Login,
                IsActive = user.IsActive,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(FindedUser.PasswordHash)
            };
            return await _repository.UpdateUserAsync(Newuser);
        }

        public async Task<User?> AuthenticateAsync(LoginDTO dto)
        {
            var user = await _repository.GetByLoginAsync(dto.Login);
            if (user == null || !user.IsActive) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValid)
                return null;
            if (isValid && !user.IsActive)
                return null;

            return user;
        }

    }
}