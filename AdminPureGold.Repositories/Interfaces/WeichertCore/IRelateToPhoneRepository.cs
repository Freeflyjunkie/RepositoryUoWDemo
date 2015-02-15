using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IRelateToPhoneRepository : IGenericRepository<RelateToPhone>
    {
        IEnumerable<RelateToPhone> Foo();
    }
}
