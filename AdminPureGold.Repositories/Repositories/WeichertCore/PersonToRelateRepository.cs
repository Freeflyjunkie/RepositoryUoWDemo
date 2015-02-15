using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class PersonToRelateRepository : GenericRepository<PersonToRelate>, IPersonToRelateRepository
    {
        public PersonToRelateRepository(WeichertCoreContext context) : base(context)
        {
                
        }

        public IEnumerable<PersonToRelate> Foo()
        {
            return new List<PersonToRelate>();
        }
    }
}
