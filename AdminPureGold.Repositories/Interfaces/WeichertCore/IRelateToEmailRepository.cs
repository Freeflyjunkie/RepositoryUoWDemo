using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IRelateToEmailRepository : IGenericRepository<RelateToEmail>
    {
        RelateToEmail GetRelateToEmailByPersonNumber(Int32 personNumber);
    }
}
