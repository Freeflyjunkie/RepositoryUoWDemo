using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IPersonToRelateRepository : IGenericRepository<PersonToRelate>
    {
        IEnumerable<PersonToRelate> Foo();
    }
}
