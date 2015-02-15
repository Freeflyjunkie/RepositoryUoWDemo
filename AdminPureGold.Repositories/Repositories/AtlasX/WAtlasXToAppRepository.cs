using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using AdminPureGold.Repositories.EF;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{
    public class WAtlasXToAppRepository : GenericRepository<WAtlasXToApp>, IWAtlasXToAppRepository
    {
        public WAtlasXToAppRepository(AtlasXContext context)
            : base(context)
        {

        }
    }
}
