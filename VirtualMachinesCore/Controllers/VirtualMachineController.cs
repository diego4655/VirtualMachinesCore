using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualMachinesCore.Application.DTOs;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Controllers
{
    /// <summary>
    /// Controller for managing virtual machines
    /// </summary>
    [Authorize]
    public class VirtualMachineController : BaseController
    {
        private readonly IVirtualMachineService _vmService;

        public VirtualMachineController(IVirtualMachineService vmService)
        {
            _vmService = vmService;
        }

        [Authorize]
        [HttpGet]
        [HttpOptions]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string lastpagination)
        {
            var vms = await _vmService.GetUserVirtualMachinesAsync(new VirtualMachine() { LastPaginationToken = lastpagination });
            return HandleResult(vms);
        }

        [Authorize]
        [HttpGet]
        [HttpOptions]
        [Route("/GetOne")]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            var vm = await _vmService.GetVirtualMachineByIdAsync(id);
            return HandleResult(vm);
        }


        [Authorize]
        [HttpPost]
        [HttpOptions]
        [Route("/Create")]
        public async Task<IActionResult> Create(VirtualMachineRequest vmDto)
        {
            var createdVm = await _vmService.CreateVirtualMachineAsync(vmDto);
            return CreatedAtAction(nameof(GetById), new { id = createdVm.Id }, createdVm);
        }

        [Authorize]
        [HttpPut]
        [HttpOptions]
        [Route("/Update")]
        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] VirtualMachineRequest vmDto)
        {
            await _vmService.UpdateVirtualMachineAsync(id, vmDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        [HttpOptions]
        [Route("/Delete")]
        public async Task<IActionResult> Delete([FromQuery]string id)
        {
            await _vmService.DeleteVirtualMachineAsync(id);
            return NoContent();
        }
    }
} 