using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IOfficeRepository : IGenericRepository<Office>
    {
        IEnumerable<Office> Foo();
    }
}
