using System;
using System.Collections.Generic;

namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IWeichertCoreSqlQueryRepository<T> : IGenericSqlQueryRepository<T> where T : class 
    {
        IEnumerable<T> GetAgentByLastNameOrAssociateNumber(string search);
        IEnumerable<T> GetActiveInactiveAgentByLastNameOrAssociateNumber(string search);
        T GetActiveInactiveAgentByRelationshipNumber(Int32 relationshipNumber);
        IEnumerable<T> GetEmployeeByLastName(string search);
    }
}
