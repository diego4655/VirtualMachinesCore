using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(User username);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
} 