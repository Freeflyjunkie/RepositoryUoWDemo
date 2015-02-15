using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface IPureGoldEmailRepository : IGenericRepository<PureGoldEmail>
    {
        IEnumerable<PureGoldEmail> GetPureGoldEmails();

        IEnumerable<PureGoldEmail> GetPureGoldEmails_Sent();

        IEnumerable<PureGoldEmail> GetPureGoldEmails_Pending();

        PureGoldEmail GetPureGoldEmails_Pending_Next();

    }
}
