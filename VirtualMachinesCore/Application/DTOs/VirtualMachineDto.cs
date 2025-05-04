namespace VirtualMachinesCore.Application.DTOs
{
    public class VirtualMachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int MemoryGB { get; set; }
        public int CpuCores { get; set; }
        public string OperatingSystem { get; set; }
    }
} 