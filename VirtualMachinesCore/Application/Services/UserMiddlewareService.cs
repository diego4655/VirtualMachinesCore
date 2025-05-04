using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Services
{
    public class UserMiddlewareService : IUserMiddlewareService
    {
        private readonly IUserRepository _userRepository;
        public UserMiddlewareService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUser(User user)
        {
            return await _userRepository.GetUserByUsernameAsync(user);
        }
    }
}
