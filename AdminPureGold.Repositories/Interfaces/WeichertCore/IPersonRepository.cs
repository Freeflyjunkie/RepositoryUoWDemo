using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        IEnumerable<Person> GetPersonsByPersonNumbers(IEnumerable<Int32?> personNumbers);
    }
}
