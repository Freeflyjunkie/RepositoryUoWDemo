using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using AdminPureGold.Repositories.EF;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{
    public class WAtlasXToAppWPersonRepository : GenericRepository<WAtlasXtoAppWPerson>, IWAtlasXToAppWPersonRepository
    {
        public WAtlasXToAppWPersonRepository(AtlasXContext context)
            : base(context)
        {

        }
    }
}

