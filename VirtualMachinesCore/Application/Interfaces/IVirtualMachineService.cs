using VirtualMachinesCore.Application.DTOs;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Interfaces
{
    public interface IVirtualMachineService
    {
        Task<IEnumerable<VirtualMachine>> GetUserVirtualMachinesAsync(VirtualMachine vm);
        Task<VirtualMachine> GetVirtualMachineByIdAsync(string id);        
        Task<VirtualMachine> CreateVirtualMachineAsync(VirtualMachineRequest request);
        Task UpdateVirtualMachineAsync(string id, VirtualMachineRequest request);
        Task DeleteVirtualMachineAsync(string id);
    }
} 