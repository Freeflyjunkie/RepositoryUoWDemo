using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class RelateToPhoneRepository : GenericRepository<RelateToPhone>, IRelateToPhoneRepository
    {
        public RelateToPhoneRepository(WeichertCoreContext context) : base(context)
        {
            
        }

        public IEnumerable<RelateToPhone> Foo()
        {
            return new List<RelateToPhone>();
        }
    }
}
