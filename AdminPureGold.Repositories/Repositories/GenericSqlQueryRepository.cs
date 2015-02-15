using System.Data.Entity;
using AdminPureGold.Repositories.Interfaces;
using System.Collections.Generic;

namespace AdminPureGold.Repositories.Repositories
{
    public class GenericSqlQueryRepository<T> : IGenericSqlQueryRepository<T> where T : class
    {
        internal DbContext Context;

        public GenericSqlQueryRepository(DbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(query, parameters);
        }
        
        public void ExecSqlCommand(string sql, params object[] parameters)
        {
            Context.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
