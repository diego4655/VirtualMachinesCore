using Amazon.DynamoDBv2.DataModel;

namespace VirtualMachinesCore.Domain.Entities
{    
    public class User
    {
        [DynamoDBHashKey]
         public string Username { get; set; }
         public string Password { get; set; }
        
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
} 