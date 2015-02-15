using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;
using System.Data.SqlClient;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    class WeichertOneUserRepository : GenericRepository<WeichertOneUser>, IWeichertOneUserRepository
    {
        private readonly WeichertCoreContext _weichertCoreContext;
        public WeichertOneUserRepository(WeichertCoreContext context)
            : base(context)
        {
            _weichertCoreContext = context;
        }
        
        public IEnumerable<WeichertOneUser> Foo()
        {
            throw new NotImplementedException();
        }

        public WeichertOneUser GetWeichertOneUserByCredentials(string username, string password)
        {
            var usernameParam = new SqlParameter
            {                
                ParameterName = "UserName",
                Value = username
            };

            var passwordParam = new SqlParameter
            {
                ParameterName = "Password",
                Value = password
            };

           return _weichertCoreContext
                .Database
                .SqlQuery<WeichertOneUser>("exec Wone_Login_GetByCredentials @UserName, @Password ", usernameParam, passwordParam).FirstOrDefault();            
        }

    }
}
