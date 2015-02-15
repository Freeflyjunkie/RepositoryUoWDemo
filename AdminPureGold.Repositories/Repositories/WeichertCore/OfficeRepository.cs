using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(WeichertCoreContext context): base(context)
        {
            
        }

        public IEnumerable<Office> Foo()
        {
            return new List<Office>();
        }
    }
}
