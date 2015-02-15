using System.Collections.Generic;

namespace AdminPureGold.Repositories.Interfaces
{
    public interface IGenericSqlQueryRepository<T> where T : class 
    {
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);

        void ExecSqlCommand(string sql, params object[] parameters);
    }
}
