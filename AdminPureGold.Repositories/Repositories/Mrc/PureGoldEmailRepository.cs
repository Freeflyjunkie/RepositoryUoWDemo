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
    public class PureGoldEmailRepository : GenericRepository<PureGoldEmail>, IPureGoldEmailRepository
    {
        private readonly MrcContext _context;
        public PureGoldEmailRepository(MrcContext context) 
            : base (context)
        {
            _context = context;
        }

        public IEnumerable<PureGoldEmail> GetPureGoldEmails()
        {
            return _context.PureGoldEmails.ToList();
        }
        
        public IEnumerable<PureGoldEmail> GetPureGoldEmails_Sent()
        {
            return _context.PureGoldEmails
                .Where(a => a.EmailSentDate != null)
                .ToList();
        }
        public IEnumerable<PureGoldEmail> GetPureGoldEmails_Pending()
        {
            return _context.PureGoldEmails
                .Where(a => a.EmailSentDate == null)
                .ToList();
        }

        public PureGoldEmail GetPureGoldEmails_Pending_Next()
        {
            return _context.PureGoldEmails
                .Where(a => a.EmailSentDate == null)
                .Take(1).SingleOrDefault();
        }


    }
}
