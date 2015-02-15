using System.Collections.Generic;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.AtlasX;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{
    public class PropertyAlternateRepository : GenericRepository<PropertyAlternate>, IPropertyAlternateRepository
    {
        public PropertyAlternateRepository(AtlasXContext context) : base (context)
        {
            
        }

        public IEnumerable<PropertyAlternate> Foo()
        {
            return new List<PropertyAlternate>();
        }
    }
}
