using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class RelateToEmailRepository :GenericRepository<RelateToEmail>, IRelateToEmailRepository
    {
        private readonly WeichertCoreContext _context;
        public RelateToEmailRepository(WeichertCoreContext context)
            : base(context)
        {
            _context = context;
        }

        public RelateToEmail GetRelateToEmailByPersonNumber(Int32 personNumber)
        {
            // Take Agent RoleTaskNumber First
            // Emp RoleTaskNo = 100
            // Asc RoleTaskNo > 100                      
            return _context.PersonToRelates
                .Where(t => t.RoleTaskNumber >= 100 && t.RoleTaskNumber <= 106 && t.PersonNumber == personNumber)
                .OrderByDescending(d => d.RoleTaskNumber)
                .Join(_context.RelateToEmails,
                    r => r.RelationshipNumber,
                    n => n.RelationshipNumber,
                    (r, n) => n)
                .ToList()
                .FirstOrDefault();
        }
    }
}