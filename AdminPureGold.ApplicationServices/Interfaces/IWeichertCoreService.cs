using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IWeichertCoreService
    {
        Person GetPersonByPersonNumber(int personNumber);
        IEnumerable<Person> GetPersons(IEnumerable<Int32?> personNumbers);
        PersonToRelate GetSalesManagerForOfficeId(int officeId);
        PersonToRelate GetProcessingManagerForOfficeId(int officeId);
        Person GetPersonByAssociateNumber(string associateNumber);
        IEnumerable<ViewBaseAssociateActive> GetAgentByLastNameOrAssociateNumber(string search);
        IEnumerable<ViewBaseAssociateActive> GetActiveInactiveAgentByLastNameOrAssociateNumber(string search);
        ViewBaseAssociateActive GetActiveInactiveAgentByRelationshipNumber(Int32 relationshipNumber);
        IEnumerable<ViewBaseEmployeeActive> GetEmployeeByLastName(string search);
        WeichertOneUser GetWeichertOneUserByCredentials(String username, String password);
        RelateToName GetRelateToNameByPersonNumber(Int32 personNumber);
    }
}
