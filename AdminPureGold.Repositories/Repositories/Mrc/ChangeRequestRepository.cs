using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class ChangeRequestRepository: GenericRepository<ChangeRequest>, IChangeRequestRepository
    {
        private readonly MrcContext _context;
        public ChangeRequestRepository(MrcContext context) : base (context)
        {
            _context = context;
        }
        
        public IEnumerable<ChangeRequestCategory> GetCategories()
        {
            return _context.ChangeRequestCategories.ToList();
        }
        public IEnumerable<ChangeRequestStatus> GetRequestStatuses()
        {
            return _context.ChangeRequestStatuses.ToList();
        }
        public IEnumerable<ChangeRequest> GetChangeRequests(IEnumerable<Int32> changeRequestIds)
        {
            return _context.ChangeRequests.Where(r => changeRequestIds.Contains(r.ChangeRequestId)).ToList();
        }
    }
}
