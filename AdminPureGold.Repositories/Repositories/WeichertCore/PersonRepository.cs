using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly WeichertCoreContext _context;
        public PersonRepository(WeichertCoreContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetPersonsByPersonNumbers(IEnumerable<Int32?> personNumbers)
        {
            return _context.Persons.Where(p => personNumbers.Contains(p.PersonNumber)).ToList();
        }
    }
}
