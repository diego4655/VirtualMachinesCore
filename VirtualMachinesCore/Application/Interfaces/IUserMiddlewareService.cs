using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Interfaces
{
    public interface IUserMiddlewareService
    {
        Task<User> GetUser(User user);
    }
}
