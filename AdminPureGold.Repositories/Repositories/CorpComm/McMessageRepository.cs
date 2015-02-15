using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.CorpComm;
using AdminPureGold.Repositories.Interfaces.CorpComm;
using AdminPureGold.Repositories.EF;

namespace AdminPureGold.Repositories.Repositories.CorpComm
{
    public class McMessageRepository : GenericRepository<McMessage>, IMcMessageRepository
    {
        private readonly CorpCommContext _context;

        public McMessageRepository(CorpCommContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<McMessage> GetMessagesForPerson(Int32 personNumber)
        {
            return _context.McMessage.Where(a => a.RecipientWpersno == personNumber).ToList();
        }
    }
}
