using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IWeichertOneUserRepository : IGenericRepository<WeichertOneUser>
    {
        IEnumerable<WeichertOneUser> Foo();
        WeichertOneUser GetWeichertOneUserByCredentials(string username, string password);
    }
}
