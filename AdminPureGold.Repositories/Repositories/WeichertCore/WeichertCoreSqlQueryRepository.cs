using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class WeichertCoreSqlQueryRepository<T> : GenericSqlQueryRepository<T>, IWeichertCoreSqlQueryRepository<T> where T : class, new()
    {
        public WeichertCoreSqlQueryRepository()
            : base(new WeichertCoreContext())
        {

        }

        public IEnumerable<T> GetAgentByLastNameOrAssociateNumber(string search)
        {
            string sqlStatement;

            if (search.Contains(","))
            {
                sqlStatement = "SELECT * FROM vwBaseAscActive " 
                    + "WHERE AssocType NOT IN ('REF') "
                    + "AND Office IS NOT NULL "
                    + "AND RTrim(LicenseLName) + ', ' + RTrim(LicenseFName) like '" + search + "%'";
            }
            else
            {
                sqlStatement = "SELECT * FROM vwBaseAscActive "
                + "WHERE AssocType NOT IN ('REF') "
                + "AND Office IS NOT NULL "
                + "AND (AssocNo = '" + search + "' OR LicenseLName LIKE '" + search + "%')";
            }

            return base.ExecWithStoreProcedure(sqlStatement).ToList();
        }

        public IEnumerable<T> GetActiveInactiveAgentByLastNameOrAssociateNumber(string search)
        {
            string sqlStatement;            

            if (search.Contains(","))
            {
                sqlStatement = "SELECT * FROM vwAscAllActiveInactive "
                    + "WHERE AssocType NOT IN ('REF') "
                    + "AND Office IS NOT NULL "
                    + "AND RTrim(LicenseLName) + ', ' + RTrim(LicenseFName) LIKE '" + search + "%'";
            }
            else
            {
                sqlStatement = "SELECT * FROM vwAscAllActiveInactive "
               + "WHERE AssocType NOT IN ('REF') "
               + "AND Office IS NOT NULL "
               + "AND (AssocNo = '" + search + "' OR LicenseLName LIKE '" + search + "%')";
            }

            return base.ExecWithStoreProcedure(sqlStatement).ToList();
        }

        public T GetActiveInactiveAgentByRelationshipNumber(int relationshipNumber)
        {
            var sqlStatement = "SELECT * FROM vwAscAllActiveInactive WHERE Wrelateno = " + relationshipNumber;
            var records = base.ExecWithStoreProcedure(sqlStatement).ToList();
            if (records.Count == 0)
            {
                return new T();
            }
            return records.First();
        }

        public IEnumerable<T> GetEmployeeByLastName(string search)
        {
            string sqlStatement;

            if (search.Contains(","))
            {
                sqlStatement = "SELECT * FROM vwBaseEmpActive WHERE RTrim(SSNlname) + ', ' + RTrim(SSNfname) LIKE '" + search + "%'";
            }
            else
            {
                sqlStatement = "SELECT * FROM vwBaseEmpActive WHERE SSNlname like '" + search + "%')";
            }

            return base.ExecWithStoreProcedure(sqlStatement).ToList();
        }

        public IEnumerable<T> GetProcessingManagerByOfficeId(Int32 officeId)
        {
            var sqlStatement = "SELECT * FROM vwBaseEmpActive WHERE OfficeID = " + officeId + " AND JobTitle = 'Processing Manager'";
            return base.ExecWithStoreProcedure(sqlStatement).ToList();
        }
    }
}
