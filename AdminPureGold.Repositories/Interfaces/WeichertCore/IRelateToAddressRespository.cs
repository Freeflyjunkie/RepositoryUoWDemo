using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IRelateToAddressRespository : IGenericRepository<RelateToAddress>
    {
        IEnumerable<RelateToAddress> Foo();
    }
}
