using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Infrastructure.Repositories
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly IDynamoDBContext _context;
        private const string TableName = "MACHINES";
        
        public VirtualMachineRepository(IDynamoDBContext context)
        {
                if (!string.IsNullOrEmpty(TableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(VirtualMachine)] = new Amazon.Util.TypeMapping(typeof(VirtualMachine), TableName);
            }
            _context = context;
        }

        public async Task<VirtualMachine> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<VirtualMachine>(id);
        }

       public async Task CreateAsync(VirtualMachine vm)
        {
            await _context.SaveAsync(vm);
        }

        public async Task UpdateAsync(VirtualMachine vm)
        {
            await _context.SaveAsync(vm);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<VirtualMachine>(id);
        }

        public async Task<List<VirtualMachine>> GetByOwnerAsync(VirtualMachine vm)
        {
            var VirtualMachineTable = _context.GetTargetTable<VirtualMachine>();
            string nextPaginationToken = vm.LastPaginationToken ?? "{}";
            List<Document> documents = new List<Document>();

            var query = new QueryFilter();

            do
            {
                var cmQueryDB = VirtualMachineTable.Scan(new ScanOperationConfig()
                {
                    Limit = 25,
                    PaginationToken = nextPaginationToken
                });
                documents.AddRange(await cmQueryDB.GetNextSetAsync());
                nextPaginationToken = cmQueryDB.PaginationToken;
            } while (documents.Count < 25 && !string.Equals(nextPaginationToken, "{}", StringComparison.Ordinal));
            IEnumerable<VirtualMachine> readers = _context.FromDocuments<VirtualMachine>(documents);
            List<VirtualMachine> List = readers.ToList();
            return List;
        }
    }
} 