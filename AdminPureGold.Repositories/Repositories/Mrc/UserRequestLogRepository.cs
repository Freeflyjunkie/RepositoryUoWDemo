using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class UserRequestLogRepository : GenericRepository<UserRequestLog>, IUserRequestLogRepository
    {
        public UserRequestLogRepository(MrcContext context): base (context)
        {
            
        }
    }
}
