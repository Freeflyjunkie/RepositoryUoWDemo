using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.EF;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class LastImportDateRepository : GenericRepository<LastImportDate>, ILastImportDateRepository
    {
        public LastImportDateRepository(MrcContext context) 
            : base (context)
        {
                
        }
    }
}
