using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface IChangeRequestRepository : IGenericRepository<ChangeRequest>
    {        
        IEnumerable<ChangeRequestCategory> GetCategories();
        IEnumerable<ChangeRequestStatus> GetRequestStatuses();
        IEnumerable<ChangeRequest> GetChangeRequests(IEnumerable<Int32> changeRequestIds);
    }
}
