using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.CorpComm;

namespace AdminPureGold.Repositories.Interfaces.CorpComm
{
    public interface IMcMessageRepository : IGenericRepository<McMessage>
    {
        IEnumerable<McMessage> GetMessagesForPerson(Int32 personNumber);
    }
}
