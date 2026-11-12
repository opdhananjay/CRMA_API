using APPC.Models.Common;
using APPC.Repositories.Interfaces;
using APPC.Services;

namespace APPC.Repositories
{
    public class PlatformRepository: IPlatformRepository
    {
        private readonly string _connectionString;
        public PlatformRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //public async Task<ServiceResult<object>> CreateOrganization()
        //{
                
        //}

    }
}
