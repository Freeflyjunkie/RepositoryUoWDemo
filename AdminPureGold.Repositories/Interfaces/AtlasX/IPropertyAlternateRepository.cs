using System.Collections.Generic;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.Interfaces.AtlasX
{
    public interface IPropertyAlternateRepository : IGenericRepository<PropertyAlternate>
    {
        IEnumerable<PropertyAlternate> Foo();
    }
}
