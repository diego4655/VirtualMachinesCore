namespace VirtualMachinesCore.Domain.Entities
{
    public class VirtualMachine
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public int Cores { get; set; }
        public int RamGB { get; set; }
        public int DiskGB { get; set; }
        public string OperatingSystem { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string? LastPaginationToken { get; set; }
    }
} 