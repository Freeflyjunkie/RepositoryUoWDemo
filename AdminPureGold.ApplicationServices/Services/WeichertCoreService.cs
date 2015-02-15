using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.Interfaces.WeichertCore;
using AdminPureGold.Repositories.Repositories.WeichertCore;

namespace AdminPureGold.ApplicationServices.Services
{
    public class WeichertCoreService : IWeichertCoreService
    {
        private readonly IUnitOfWorkCore _unitOfWorkCore;

        public WeichertCoreService(IUnitOfWorkCore unitOfWorkCore)
        {
            _unitOfWorkCore = unitOfWorkCore;
        }

        public Person GetPersonByPersonNumber(int personNumber)
        {
            return _unitOfWorkCore.PersonRepository.Get(p => p.PersonNumber == personNumber,
                includeProperties: "PersonToImage," +
                                   "PersonToRelates," +
                                   "PersonToRelates.Office," +
                                   "PersonToRelates.RelateToNames," +
                                   "PersonToRelates.RelateToPhones," +
                                   "PersonToRelates.RelateToEmails," +
                                   "PersonToRelates.RelateToAddresses")
                .ToList().FirstOrDefault();
        }
        public RelateToName GetRelateToName(int relationshipNumber)
        {
            IEnumerable<RelateToName> names = _unitOfWorkCore.RelateToNameRepository.Get(name => name.RelationshipNumber == relationshipNumber);
            return names.FirstOrDefault();
        }
        public PersonToRelate GetSalesManagerForOfficeId(int officeId)
        {
            var personToRelates = _unitOfWorkCore.PersonToRelateRepository.Get(ptr =>
                ptr.OfficeId == officeId
                && ptr.RoleTaskNumber == 104
                && ptr.Active == "A").ToList();

            if (personToRelates.Any())
            {
                return personToRelates.First();
            }
            else
            {
                return new PersonToRelate
                {
                    Office = new Office(),
                    RelateToAddresses = new Collection<RelateToAddress>(),
                    RelateToEmails = new Collection<RelateToEmail>(),
                    RelateToNames = new Collection<RelateToName>(),
                    RelateToPhones = new Collection<RelateToPhone>()
                };
            }
        }
        public PersonToRelate GetProcessingManagerForOfficeId(int officeId)
        {
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseEmployeeActive>();
            var viewResults = weichertCoreSqlQueryRepository.GetProcessingManagerByOfficeId(officeId).ToList();

            var personToRelate = new PersonToRelate
                {
                    Office = new Office(),
                    RelateToAddresses = new Collection<RelateToAddress>(),
                    RelateToEmails = new Collection<RelateToEmail>(),
                    RelateToNames = new Collection<RelateToName>(),
                    RelateToPhones = new Collection<RelateToPhone>()
                };


            if (viewResults.Any())
            {
                var firstResult = viewResults.First();
                personToRelate = _unitOfWorkCore.PersonToRelateRepository.GetById(firstResult.Wrelateno);
            }

            return personToRelate;
        }
        public Person GetPersonByAssociateNumber(string associateNumber)
        {
            return _unitOfWorkCore.PersonRepository.GetById(0);
        }
        public IEnumerable<ViewBaseAssociateActive> GetAgentByLastNameOrAssociateNumber(string search)
        {
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseAssociateActive>();
            var result = weichertCoreSqlQueryRepository.GetAgentByLastNameOrAssociateNumber(search);
            return result;
        }
        public IEnumerable<ViewBaseAssociateActive> GetActiveInactiveAgentByLastNameOrAssociateNumber(string search)
        {
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseAssociateActive>();
            var result = weichertCoreSqlQueryRepository.GetActiveInactiveAgentByLastNameOrAssociateNumber(search);
            return result;
        }
        public ViewBaseAssociateActive GetActiveInactiveAgentByRelationshipNumber(Int32 relationshipNumber)
        {
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseAssociateActive>();
            var result = weichertCoreSqlQueryRepository.GetActiveInactiveAgentByRelationshipNumber(relationshipNumber);
            return result;
        }
        public IEnumerable<ViewBaseEmployeeActive> GetEmployeeByLastName(string search)
        {
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseEmployeeActive>();
            var result = weichertCoreSqlQueryRepository.GetEmployeeByLastName(search);
            return result;
        }
        public WeichertOneUser GetWeichertOneUserByCredentials(string username, string password)
        {
            return _unitOfWorkCore.WeichertOneUserRepository.GetWeichertOneUserByCredentials(username, password);
        }
        public IEnumerable<Person> GetPersons(IEnumerable<int?> personNumbers)
        {
            var persons = _unitOfWorkCore.PersonRepository.GetPersonsByPersonNumbers(personNumbers).ToList();
            return persons;
        }
        public RelateToName GetRelateToNameByPersonNumber(int personNumber)
        {                      
            return _unitOfWorkCore.RelateToNameRepository.GetRelateToNameByPersonNumber(personNumber);
        }
    }
}
