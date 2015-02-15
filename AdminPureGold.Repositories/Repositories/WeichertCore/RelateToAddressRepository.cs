using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class RelateToAddressRepository : GenericRepository<RelateToAddress>, IRelateToAddressRespository
    {
        public RelateToAddressRepository(WeichertCoreContext context) : base(context)
        {
            
        }

        public IEnumerable<RelateToAddress> Foo()
        {
            throw new NotImplementedException();
        }
    }
}
