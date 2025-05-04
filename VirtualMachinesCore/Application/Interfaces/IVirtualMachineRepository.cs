using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Interfaces
{
    public interface IVirtualMachineRepository
    {
        Task<VirtualMachine> GetByIdAsync(string id);
        Task<List<VirtualMachine>> GetByOwnerAsync(VirtualMachine vm);
        Task CreateAsync(VirtualMachine vm);
        Task UpdateAsync(VirtualMachine vm);
        Task DeleteAsync(string id);
    }
} 