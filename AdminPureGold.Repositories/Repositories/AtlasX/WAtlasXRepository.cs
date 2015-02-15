using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.AtlasX;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{
    public class WAtlasXRepository : GenericRepository<WAtlasX>, IWAtlasXRepository
    {
        public WAtlasXRepository(AtlasXContext context)
            : base(context)
        {

        }       
    }
}
