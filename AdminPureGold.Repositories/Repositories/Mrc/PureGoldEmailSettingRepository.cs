using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class PureGoldEmailSettingRepository : GenericRepository<PureGoldEmailSetting>, IPureGoldEmailSettingRepository
    {
        private readonly MrcContext _context;
        public PureGoldEmailSettingRepository(MrcContext context) 
            : base (context)
        {
            _context = context;
        }

        public IEnumerable<PureGoldEmailSetting> GetPureGoldEmailSettings()
        {
            return _context.PureGoldEmailSettings.ToList();
        }

    }
}
