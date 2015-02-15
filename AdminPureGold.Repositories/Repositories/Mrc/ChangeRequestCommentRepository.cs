using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class ChangeRequestCommentRepository : GenericRepository<ChangeRequestComment>, IChangeRequestCommentRepository
    {
        private readonly MrcContext _context;
        public ChangeRequestCommentRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
