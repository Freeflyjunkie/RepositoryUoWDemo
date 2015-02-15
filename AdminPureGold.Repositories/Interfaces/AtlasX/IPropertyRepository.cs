using System.Collections.Generic;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.Interfaces.AtlasX
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        IEnumerable<Property> Foo();
    }
}
