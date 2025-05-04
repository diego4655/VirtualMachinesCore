using VirtualMachinesCore.Application.DTOs;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;
using System.Security.Claims;

namespace VirtualMachinesCore.Application.Services
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly IVirtualMachineRepository _vmRepository;        

        public VirtualMachineService(IVirtualMachineRepository vmRepository)
        {
            _vmRepository = vmRepository;        
        }

        public async Task<VirtualMachine> CreateVirtualMachineAsync(VirtualMachineRequest request)
        {                         
            if (request.Cores <= 0)
            {
                throw new ArgumentException("Debe seleccionar un numero de Cores");
            }
                
            if (request.RamGB <= 0)
            {
                throw new ArgumentException("Debe agregar un valor a la RAM");
            }
                
            if (request.DiskGB <= 0)
            {
                throw new ArgumentException("Debe agregar un valor al disco duro");
            }
                
            if (string.IsNullOrEmpty(request.OperatingSystem))
            {
                throw new ArgumentException("El sistema operativo es requerido");
            }
                

            var vm = new VirtualMachine
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Cores = request.Cores,
                RamGB = request.RamGB,
                DiskGB = request.DiskGB,
                OperatingSystem = request.OperatingSystem,                                
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow
            };

            await _vmRepository.CreateAsync(vm);
            return vm;
        }

        public async Task<IEnumerable<VirtualMachine>> GetUserVirtualMachinesAsync(VirtualMachine vm)
        {
            
            return await _vmRepository.GetByOwnerAsync(vm);
        }

     
        public async Task<VirtualMachine> GetVirtualMachineByIdAsync(string id)
        {
            var vm = await _vmRepository.GetByIdAsync(id);
            if (vm == null)
            {
                throw new KeyNotFoundException("Maquina virtual no encontrada");
            }
                
            return vm;
        }

        public async Task UpdateVirtualMachineAsync(string id,VirtualMachineRequest request)
        {
            var vm = await _vmRepository.GetByIdAsync(id);
            if (vm == null)
                throw new KeyNotFoundException("Maquina Virtual no encontrada");


            if (request.Cores <= 0)
                throw new ArgumentException("El numero de procesadores debe ser mayor a 0");
            if (request.RamGB <= 0)
                throw new ArgumentException("La memoria RAM debe ser  mayor a 0");
            if (request.DiskGB <= 0)
                throw new ArgumentException("El tamaño del disco debe ser mayor a 0");
            if (string.IsNullOrEmpty(request.OperatingSystem))
                throw new ArgumentException("El sistema operativo es requerido");

            vm.Name = request.Name;
            vm.Cores = request.Cores;
            vm.RamGB = request.RamGB;
            vm.DiskGB = request.DiskGB;
            vm.OperatingSystem = request.OperatingSystem;
            vm.LastModifiedAt = DateTime.UtcNow;

            await _vmRepository.UpdateAsync(vm);            
        }

        public async Task DeleteVirtualMachineAsync(string id)
        {
            var vm = await _vmRepository.GetByIdAsync(id);
            if (vm == null)
                throw new KeyNotFoundException("Virtual machine not found");

            await _vmRepository.DeleteAsync(id);
        }
    }
} 