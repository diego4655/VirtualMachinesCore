namespace VirtualMachinesCore.Application.DTOs
{
    public class VirtualMachineRequest
    {
        public string Name { get; set; }
        public int Cores { get; set; }
        public int RamGB { get; set; }
        public int DiskGB { get; set; }
        public string OperatingSystem { get; set; }
    }
} 