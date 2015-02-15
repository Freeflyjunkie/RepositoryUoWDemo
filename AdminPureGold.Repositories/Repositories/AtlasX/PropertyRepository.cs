using System.Collections.Generic;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.AtlasX;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(AtlasXContext context)
            : base(context)
        {
            
        }

        public IEnumerable<Property> Foo()
        {
            return new List<Property>();
        }
    }
}
