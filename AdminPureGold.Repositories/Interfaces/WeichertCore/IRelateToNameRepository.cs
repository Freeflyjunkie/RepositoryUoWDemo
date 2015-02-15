using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IRelateToNameRepository : IGenericRepository<RelateToName>
    {
        RelateToName GetRelateToNameByPersonNumber(Int32 personNumber);
    }
}
