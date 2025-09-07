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
            var user = new User
            {
                Name = dto.Name,
                Login = dto.Login
            };

            return await _repository.CreateUserAsync(user);
        }

    }
}