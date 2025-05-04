using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;

        public UserRepository(IDynamoDBContext context)
        {
            var tablename = Environment.GetEnvironmentVariable("UserTableName");
            if (!string.IsNullOrEmpty(tablename))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(User)] = new Amazon.Util.TypeMapping(typeof(User), tablename);
            }
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(User username)
        {
            try
            {
                return await _context.LoadAsync(username);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.SaveAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _context.SaveAsync(user);
        }
    }
} 