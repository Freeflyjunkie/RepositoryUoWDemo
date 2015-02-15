using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class AgentViewModelBuilder
    {
        public static AgentViewModel GetViewModel(Person person, IToolboxService toolboxService)
        {
            var agentViewModel = new AgentViewModel
            {
                PersonToImage = new PersonToImage(),
                PersonNumber = person.PersonNumber,
                RelateToName = new RelateToName(),
                Office = new Office(),
                OfficeSalesManager = new PersonToRelate
                {
                    Office = new Office(),
                    RelateToAddresses = new Collection<RelateToAddress>(),
                    RelateToEmails = new Collection<RelateToEmail>(),
                    RelateToNames = new Collection<RelateToName>(),
                    RelateToPhones = new Collection<RelateToPhone>()
                },
                RelateToAddress = new RelateToAddress(),
                RelateToEmail = new RelateToEmail(),
                RelateToPhone = new RelateToPhone()
            };

            var personToImages = person.PersonToImage.Where(a => a.DisplayStatus == "A").OrderByDescending(b => b.CrDate);
            if (personToImages.Any())
            {
                agentViewModel.PersonToImage = personToImages.First();
            }

            // Take Agent RoleTaskNumber First
            // Emp RoleTaskNo = 100
            // Asc RoleTaskNo > 100                      
            PersonToRelate personToRelate =
                person.PersonToRelates
                .Where(t => t.RoleTaskNumber >= 100 && t.RoleTaskNumber <= 106)
                .OrderByDescending(ptr => ptr.RoleTaskNumber)
                .ToList()
                .FirstOrDefault();

            if (personToRelate != null)
            {
                agentViewModel.Office = personToRelate.Office;

                agentViewModel.OfficeSalesManager =
                    toolboxService.WeichertCoreService.GetSalesManagerForOfficeId(agentViewModel.Office.OfficeId);

                agentViewModel.ProcessingManager =
                    toolboxService.WeichertCoreService.GetProcessingManagerForOfficeId(agentViewModel.Office.OfficeId);

                if (personToRelate.RelateToNames.Any())
                {
                    agentViewModel.RelateToName = personToRelate.RelateToNames
                        .FirstOrDefault(rtn => !rtn.NameRoleCode.Contains("Common"));
                }

                if (personToRelate.RelateToPhones.Any())
                {
                    agentViewModel.RelateToPhone = personToRelate.RelateToPhones
                       .FirstOrDefault();
                }

                if (personToRelate.RelateToEmails.Any())
                {
                    agentViewModel.RelateToEmail = personToRelate.RelateToEmails
                       .FirstOrDefault();
                }

                if (personToRelate.RelateToAddresses.Any())
                {
                    agentViewModel.RelateToAddress = personToRelate.RelateToAddresses
                       .FirstOrDefault();
                }
            }

            return agentViewModel;
        }        
        public static AgentViewModel GetViewModel(Int32 relationshipNumber, IToolboxService toolboxService)
        {
            var weichertCoreRecord = toolboxService.WeichertCoreService.GetActiveInactiveAgentByRelationshipNumber(relationshipNumber);
            return new AgentViewModel
             {
                 PersonNumber = weichertCoreRecord.Wpersno,
                 RelateToName = new RelateToName
                 {
                     FirstName = weichertCoreRecord.LicenseFname,
                     LastName = weichertCoreRecord.LicenseLname,
                     MiddleName = weichertCoreRecord.LicenseMname,
                     RelationshipNumber = weichertCoreRecord.Wrelateno
                 },
                 Office = new Office
                 {
                     OfficeId = weichertCoreRecord.OfficeId,
                     OfficeNumber = weichertCoreRecord.OfficeNumber,
                     Address = weichertCoreRecord.OfficeAddress,
                     City = weichertCoreRecord.OfficeCity,
                     State = weichertCoreRecord.OfficeState,
                     ZipCode = weichertCoreRecord.OfficeZip,
                     OfficeName = weichertCoreRecord.Office,
                     FriendlyOfficeName = weichertCoreRecord.Office,
                     Division = weichertCoreRecord.Div,
                     DivisionName = weichertCoreRecord.Division,
                     FriendlyDivisionName = weichertCoreRecord.Division,
                     Region = weichertCoreRecord.Region,
                     Rvp = weichertCoreRecord.Rvp,
                     Phone = weichertCoreRecord.OfficePhone
                 },
                 OfficeSalesManager = new PersonToRelate
                 {
                     Office = new Office(),
                     RelateToAddresses = new Collection<RelateToAddress>(),
                     RelateToEmails = new Collection<RelateToEmail>(),
                     RelateToNames = new Collection<RelateToName>(),
                     RelateToPhones = new Collection<RelateToPhone>()
                 },
                 RelateToAddress = new RelateToAddress(),
                 RelateToEmail = new RelateToEmail
                 {
                     EmailAddress = weichertCoreRecord.WorkEmail
                 },
                 RelateToPhone = new RelateToPhone
                 {
                     PhoneNumber = weichertCoreRecord.CellPhone
                 }
             };
        }
        public static AgentViewModel GetViewModelRelateToName(Int32 personNumber, IToolboxService toolboxService)
        {
            return new AgentViewModel
            {
                PersonToImage = new PersonToImage(),
                PersonNumber = personNumber,
                RelateToName = toolboxService.WeichertCoreService.GetRelateToNameByPersonNumber(personNumber),
                Office = new Office(),
                OfficeSalesManager = new PersonToRelate
                {
                    Office = new Office(),
                    RelateToAddresses = new Collection<RelateToAddress>(),
                    RelateToEmails = new Collection<RelateToEmail>(),
                    RelateToNames = new Collection<RelateToName>(),
                    RelateToPhones = new Collection<RelateToPhone>()
                },
                RelateToAddress = new RelateToAddress(),
                RelateToEmail = new RelateToEmail(),
                RelateToPhone = new RelateToPhone()
            };
        }
        public static IEnumerable<AgentViewModel> GetViewModels(IEnumerable<Person> persons, IToolboxService toolboxService)
        {
            var agentViewModels = new List<AgentViewModel>();
            foreach (var person in persons)
            {
                agentViewModels.Add(GetViewModel(person, toolboxService));
            }
            return agentViewModels;
        }
        public static IEnumerable<AgentViewModel> GetViewModels(IEnumerable<Int32?> relationshipNumbers, IToolboxService toolboxService)
        {
            var agentViewModels = new List<AgentViewModel>();
            foreach (var relationshipNumber in relationshipNumbers)
            {
                int irelationshipNumber = relationshipNumber ?? 0;
                agentViewModels.Add(GetViewModel(irelationshipNumber, toolboxService));
            }
            return agentViewModels;
        }
        public static IEnumerable<AgentViewModel> GetViewModels(IEnumerable<Transaction> transactions, IToolboxService toolboxService)
        {
            var agentViewModels = new List<AgentViewModel>();
            foreach (var transaction in transactions)
            {
                foreach (var transactionToRelate in transaction.TransactionToRelates)
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!agentViewModels.Any(avm => avm.PersonNumber == transactionToRelate.PersonNumber))
                    {                        
                        var agentViewModel = GetViewModelRelateToName(transactionToRelate.PersonNumber, toolboxService);
                        agentViewModels.Add(agentViewModel);
                    }
                }
            }
            return agentViewModels;
        }
        public static IEnumerable<AgentViewModel> GetViewModelsRelateToNameFromChangeRequests(IEnumerable<ChangeRequest> changeRequests, IToolboxService toolboxService)
        {
            var agentViewModels = new List<AgentViewModel>();
            foreach (var changeRequest in changeRequests)
            {
                // Get AgentViewModel for Request Owners
                if (!agentViewModels.Any(peps => peps.PersonNumber == changeRequest.PersonNumber))
                {
                    AgentViewModel requestor = GetViewModelRelateToName(changeRequest.PersonNumber, toolboxService);
                    agentViewModels.Add(requestor);
                }

                // Get AgentViewModel for Comment Owners
                foreach (var comment in changeRequest.ChangeRequestComments)
                {
                    if (!agentViewModels.Any(co => co.PersonNumber == comment.PersonNumber))
                    {
                        AgentViewModel commentOwner = GetViewModelRelateToName(comment.PersonNumber, toolboxService);
                        agentViewModels.Add(commentOwner);
                    }
                }
            }
            return agentViewModels;
        }
        public static IEnumerable<AgentViewModel> GetViewModelsFromChangeRequest(ChangeRequest changeRequest, IToolboxService toolboxService)
        {
            var agentViewModels = new List<AgentViewModel>();

            // Get AgentViewModel for Request Owners
            if (!agentViewModels.Any(peps => peps.PersonNumber == changeRequest.PersonNumber))
            {
                Person person = toolboxService.WeichertCoreService.GetPersonByPersonNumber(changeRequest.PersonNumber);
                AgentViewModel requestor = GetViewModel(person, toolboxService);
                agentViewModels.Add(requestor);
            }

            // Get AgentViewModel for Comment Owners
            foreach (var comment in changeRequest.ChangeRequestComments)
            {
                if (!agentViewModels.Any(co => co.PersonNumber == comment.PersonNumber))
                {
                    Person person = toolboxService.WeichertCoreService.GetPersonByPersonNumber(comment.PersonNumber);
                    AgentViewModel commentOwner = AgentViewModelBuilder.GetViewModel(person, toolboxService);
                    agentViewModels.Add(commentOwner);
                }
            }

            return agentViewModels;
        }
        public static List<Int32?> GetAgentPersonNumbers(Transaction transaction)
        {
            var agentPersonNumbers = new List<int?> { transaction.PersonNumber };

            // Mrc Agents
            foreach (var transactionToRelate in transaction.TransactionToRelates)
            {
                if (!agentPersonNumbers.Contains(transactionToRelate.PersonNumber))
                    agentPersonNumbers.Add(transactionToRelate.PersonNumber);
            }

            return agentPersonNumbers;
        }
        public static List<Int32?> GetAgentPersonNumbers(List list)
        {
            var agentPersonNumbers = new List<Int32?>();

            // Get List Agents
            foreach (var listToAssociate in list.ListToAssociates)
            {
                if (listToAssociate.PersonNumber != null)
                {
                    if (!agentPersonNumbers.Contains(listToAssociate.PersonNumber))
                        agentPersonNumbers.Add(listToAssociate.PersonNumber);
                }
            }

            // Get Sale Agents
            foreach (var sale in list.Sales)
            {
                foreach (var saleToAssociate in sale.SaleToAssociates)
                {
                    if (saleToAssociate.PersonNumber != null)
                    {
                        if (!agentPersonNumbers.Contains(saleToAssociate.PersonNumber))
                            agentPersonNumbers.Add(saleToAssociate.PersonNumber);
                    }
                }
            }

            return agentPersonNumbers;
        }
        public static List<Int32?> GetAgentPersonNumbers(Transaction transaction, List list)
        {
            var agentPersonNumbers = new List<int?> { transaction.PersonNumber };

            // Mrc Agents
            foreach (var transactionToRelate in transaction.TransactionToRelates)
            {
                if (!agentPersonNumbers.Contains(transactionToRelate.PersonNumber))
                    agentPersonNumbers.Add(transactionToRelate.PersonNumber);
            }

            if (list != null)
            {
                // Get List Agents
                foreach (var listToAssociate in list.ListToAssociates)
                {
                    if (listToAssociate.PersonNumber != null)
                    {
                        if (!agentPersonNumbers.Contains(listToAssociate.PersonNumber))
                            agentPersonNumbers.Add(listToAssociate.PersonNumber);
                    }
                }

                // Get Sale Agents
                foreach (var sale in list.Sales)
                {
                    foreach (var saleToAssociate in sale.SaleToAssociates)
                    {
                        if (saleToAssociate.PersonNumber != null)
                        {
                            if (!agentPersonNumbers.Contains(saleToAssociate.PersonNumber))
                                agentPersonNumbers.Add(saleToAssociate.PersonNumber);
                        }
                    }
                }
            }

            return agentPersonNumbers;
        }
        public static List<Int32?> GetAgentPersonNumbers(List<ChangeRequest> changeRequests)
        {
            var agentPersonNumbers = new List<int?>();

            // Change Request Owner
            foreach (var changeRequest in changeRequests)
            {
                if (!agentPersonNumbers.Contains(changeRequest.PersonNumber))
                    agentPersonNumbers.Add(changeRequest.PersonNumber);
            }

            // Change Request Comment Owners
            foreach (var changeRequest in changeRequests)
            {
                foreach (var comment in changeRequest.ChangeRequestComments)
                {
                    if (!agentPersonNumbers.Contains(comment.PersonNumber))
                        agentPersonNumbers.Add(comment.PersonNumber);
                }
            }

            return agentPersonNumbers;
        }
        public static List<Int32?> GetAllAgentPersonNumbers(Transaction transaction, List list)
        {
            var agentPersonNumbers = new List<int?> { transaction.PersonNumber };

            // Mrc Agents
            foreach (var transactionToRelate in transaction.TransactionToRelates)
            {
                if (!agentPersonNumbers.Contains(transactionToRelate.PersonNumber))
                    agentPersonNumbers.Add(transactionToRelate.PersonNumber);
            }

            // Change Request Owner
            foreach (var changeRequest in transaction.ChangeRequests)
            {
                if (!agentPersonNumbers.Contains(changeRequest.PersonNumber))
                    agentPersonNumbers.Add(changeRequest.PersonNumber);
            }

            // Change Request Comment Owners
            foreach (var changeRequest in transaction.ChangeRequests)
            {
                foreach (var comment in changeRequest.ChangeRequestComments)
                {
                    if (!agentPersonNumbers.Contains(comment.PersonNumber))
                        agentPersonNumbers.Add(comment.PersonNumber);
                }
            }

            if (list != null)
            {
                // Get List Agents
                foreach (var listToAssociate in list.ListToAssociates)
                {
                    if (listToAssociate.PersonNumber != null)
                    {
                        if (!agentPersonNumbers.Contains(listToAssociate.PersonNumber))
                            agentPersonNumbers.Add(listToAssociate.PersonNumber);
                    }
                }

                // Get Sale Agents
                foreach (var sale in list.Sales)
                {
                    foreach (var saleToAssociate in sale.SaleToAssociates)
                    {
                        if (saleToAssociate.PersonNumber != null)
                        {
                            if (!agentPersonNumbers.Contains(saleToAssociate.PersonNumber))
                                agentPersonNumbers.Add(saleToAssociate.PersonNumber);
                        }
                    }
                }
            }

            return agentPersonNumbers;
        }
    }
}