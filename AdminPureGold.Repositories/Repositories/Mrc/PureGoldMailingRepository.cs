using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class PureGoldMailingRepository : GenericRepository<PureGoldMailing>, IPureGoldMailingRepository
    {
        public PureGoldMailingRepository(MrcContext context)
            : base(context)
        {

        }
    }
}
