using VirtualMachinesCore.Application.DTOs;

namespace VirtualMachinesCore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);        
    }
} 