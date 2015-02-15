using System;
using System.Linq;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.ApplicationServices.Classes
{
    internal class AgentManager
    {
        private const Int32 AppNameId = 2;
        private const String AtlasXDatabaseUser = "AtlasX_User";

        public static void UpdateAgentInMrcWithParsedDetail(ChangeRequestDetailParsed parsedDetail,
            Transaction transaction,
            Int32 personNumber,
            IUnitOfWorkMrc unitOfWorkMrc)
        {
            var parsedAgent = parsedDetail as ChangeRequestDetailAgentParsed;
            if (parsedAgent != null)
            {
                // Get Transaction To Relates
                var transactionToRelate =
                    transaction.TransactionToRelates.SingleOrDefault(
                        agent =>
                            agent.TransactionId == transaction.TransactionId &&
                            agent.RelationshipNumber == parsedAgent.RelationshipNumber);

                // set all other agents to partners
                foreach (var agent in transaction.TransactionToRelates)
                {
                    agent.SortOrder = 2;
                    agent.EntityStateForGraphsUpdates = State.Modified;
                    unitOfWorkMrc.TransactionToRelateRepository.Update(agent);
                }

                if (transactionToRelate != null)
                {
                    // Update TransactionToRelate In MRC
                    transactionToRelate.Active = parsedAgent.Active;
                    transactionToRelate.SortOrder = parsedAgent.SortOrder;
                    transactionToRelate.EntityStateForGraphsUpdates = State.Modified;
                    unitOfWorkMrc.TransactionToRelateRepository.Update(transactionToRelate);
                }
                else
                {
                    // Create New TransactionToRelate In MRC
                    transactionToRelate = new TransactionToRelate
                    {
                        PersonNumber = parsedAgent.PersonNumber,
                        RelationshipNumber = parsedAgent.RelationshipNumber,
                        Active = parsedAgent.Active,
                        CrtBy = personNumber,
                        CrtDt = DateTime.Now,
                        SortOrder = parsedAgent.SortOrder,
                        PrintPerson = parsedAgent.PrintPerson,
                        TransactionId = transaction.TransactionId,
                        OfficeId = parsedAgent.OfficeId,
                        EntityStateForGraphsUpdates = State.Added
                    };
                    unitOfWorkMrc.TransactionToRelateRepository.Insert(transactionToRelate);
                }
            }
        }

        public static void UpdateAgentInMrc(Int32 transactionId,
            Int32 personNumber,
            Int32 relationshipNumber,
            Int32 officeId,
            IUnitOfWorkMrc unitOfWorkMrc)
        {
            var transaction = unitOfWorkMrc.TransactionRepository.GetById(transactionId);

            var transactionToRelate =
                    transaction.TransactionToRelates.SingleOrDefault(
                        agent =>
                            agent.TransactionId == transactionId &&
                            agent.RelationshipNumber == relationshipNumber);

            // set all other agents to partners
            foreach (var agent in transaction.TransactionToRelates)
            {
                agent.SortOrder = 2;
                agent.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionToRelateRepository.Update(agent);
            }

            // Add Agent As Primary
            if (transactionToRelate != null)
            {
                // Update TransactionToRelate In MRC
                transactionToRelate.Active = "A";
                transactionToRelate.SortOrder = 1;
                transactionToRelate.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionToRelateRepository.Update(transactionToRelate);
            }
            else
            {
                // Create New TransactionToRelate In MRC
                transactionToRelate = new TransactionToRelate
                {
                    PersonNumber = personNumber,
                    RelationshipNumber = relationshipNumber,
                    Active = "A",
                    CrtBy = personNumber,
                    CrtDt = DateTime.Now,
                    SortOrder = 1,
                    PrintPerson = true,
                    TransactionId = transactionId,
                    OfficeId = officeId,
                    EntityStateForGraphsUpdates = State.Added
                };                

                unitOfWorkMrc.TransactionToRelateRepository.Insert(transactionToRelate);
            }

            unitOfWorkMrc.Save();
        }

        public static void UpdateAgentStatusInMrc(Int32 transactionId,
            Int32 relationshipNumber,
            String status,
            IUnitOfWorkMrc unitOfWorkMrc)
        {
            var transaction = unitOfWorkMrc.TransactionRepository.GetById(transactionId);

            var transactionToRelate =
                    transaction.TransactionToRelates.SingleOrDefault(
                        agent =>
                            agent.TransactionId == transactionId &&
                            agent.RelationshipNumber == relationshipNumber);

            if (transactionToRelate != null)
            {
                // Update TransactionToRelate In MRC
                transactionToRelate.Active = status;
                transactionToRelate.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionToRelateRepository.Update(transactionToRelate);
            }

            unitOfWorkMrc.Save();
        }

        public static void UpdateAgentToPrimaryInMrc(Int32 transactionId,
            Int32 relationshipNumber,
            IUnitOfWorkMrc unitOfWorkMrc)
        {
            var transaction = unitOfWorkMrc.TransactionRepository.GetById(transactionId);

            foreach (var transactionToRelate in transaction.TransactionToRelates)
            {
                if (transactionToRelate.RelationshipNumber == relationshipNumber)
                {
                    transactionToRelate.SortOrder = 1;                    
                }
                else
                {
                    transactionToRelate.SortOrder = 2;                    
                }

                transactionToRelate.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionToRelateRepository.Update(transactionToRelate);     
            }            

            unitOfWorkMrc.Save();
        }

        public static void UpdateAgentToSecondaryInMrc(Int32 transactionId,
           Int32 relationshipNumber,
           IUnitOfWorkMrc unitOfWorkMrc)
        {
            var transaction = unitOfWorkMrc.TransactionRepository.GetById(transactionId);

            var transactionToRelate =
                    transaction.TransactionToRelates.SingleOrDefault(
                        agent =>
                            agent.TransactionId == transactionId &&
                            agent.RelationshipNumber == relationshipNumber);

            if (transactionToRelate != null)
            {
                transactionToRelate.SortOrder = 2;
                transactionToRelate.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionToRelateRepository.Update(transactionToRelate);
            }

            unitOfWorkMrc.Save();
        }

        public static void UpdateAgentInAtlasXWithParsedDetail(ChangeRequestDetailParsed parsedDetail,
            Transaction transaction,            
            IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            var parsedAgent = parsedDetail as ChangeRequestDetailAgentParsed;
            if (parsedAgent != null)
            {
                // Get AtlasX Transaction
                var atlasXToApps = unitOfWorkAtlasX.WAtlasXToAppRepository.Get(
                    filter: app => app.AppNameId == AppNameId && app.AppXId == transaction.TransactionId).ToList();
                WAtlasXToApp atlasXToApp = null;
                if (atlasXToApps.Any())
                    atlasXToApp = atlasXToApps.First();

                if (atlasXToApp != null)
                {
                    // Get AtlasX WAtlasXToAppWPerson                                                                                      
                    var atlasXToAppWPerson =
                        atlasXToApp.WAtlasXtoAppWPersons.SingleOrDefault(
                            person => person.RelationshipNumber == parsedAgent.RelationshipNumber);

                    if (atlasXToAppWPerson != null)
                    {
                        // Update WAtlasXToAppWPerson In AtlasX
                        atlasXToAppWPerson.Active = parsedAgent.Active;
                        atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;
                        unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(atlasXToAppWPerson);
                    }
                    else
                    {
                        // Create New WAtlasXtoAppWPerson In AtlasX
                        atlasXToAppWPerson = new WAtlasXtoAppWPerson
                        {
                            AtlasXtoAppId = atlasXToApp.AtlasXtoAppId,
                            PersonNumber = parsedAgent.PersonNumber,
                            RelationshipNumber = parsedAgent.RelationshipNumber,
                            AtlasXtoAppRoleId = parsedAgent.SortOrder == 1 ? Convert.ToInt16(200) : Convert.ToInt16(600),
                            Active = parsedAgent.Active,
                            CrUser = AtlasXDatabaseUser,
                            CrDate = DateTime.Now,
                            EntityStateForGraphsUpdates = State.Added
                        };
                        unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Insert(atlasXToAppWPerson);
                    }
                }

                unitOfWorkAtlasX.Save();
            }
        }

        public static void UpdateAgentInAtlasX(Int32 transactionId,
            Int32 personNumber,
            Int32 relationshipNumber,            
            IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            // Get AtlasX Transaction
            var atlasXToApps = unitOfWorkAtlasX.WAtlasXToAppRepository.Get(
                filter: app => app.AppNameId == AppNameId && app.AppXId == transactionId).ToList();

            WAtlasXToApp atlasXToApp = null;

            if (atlasXToApps.Any())
                atlasXToApp = atlasXToApps.First();

            if (atlasXToApp != null)
            {
                WAtlasXtoAppWPerson atlasXToAppWPerson = null;
                // Get AtlasX WAtlasXToAppWPerson  
                if (atlasXToApp.WAtlasXtoAppWPersons == null)
                {
                    atlasXToAppWPerson = null;
                }
                else
                {
                    atlasXToAppWPerson =  atlasXToApp.WAtlasXtoAppWPersons.SingleOrDefault(
                           person => person.RelationshipNumber == relationshipNumber);

                    // Set All To Partners
                    foreach (var agent in atlasXToApp.WAtlasXtoAppWPersons)
                    {
                        agent.AtlasXtoAppRoleId = 600;
                        unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(agent);
                    }
                }                                                                   

                // Add Agent As Primary
                if (atlasXToAppWPerson != null)
                {
                    // Update WAtlasXToAppWPerson In AtlasX
                    atlasXToAppWPerson.Active = "A";
                    atlasXToAppWPerson.AtlasXtoAppRoleId = 200;
                    atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;
                    unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(atlasXToAppWPerson);
                }
                else
                {
                    // Create New WAtlasXtoAppWPerson In AtlasX
                    atlasXToAppWPerson = new WAtlasXtoAppWPerson
                    {
                        AtlasXtoAppId = atlasXToApp.AtlasXtoAppId,
                        PersonNumber = personNumber,
                        RelationshipNumber = relationshipNumber,
                        AtlasXtoAppRoleId = 200,
                        Active = "A",
                        CrUser = AtlasXDatabaseUser,
                        CrDate = DateTime.Now,
                        EntityStateForGraphsUpdates = State.Added
                    };                    

                    unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Insert(atlasXToAppWPerson);
                }
            }

            unitOfWorkAtlasX.Save();
        }

        public static void UpdateAgentStatusInAtlasX(Int32 transactionId,
            Int32 relationshipNumber,
            String status,            
            IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            // Get AtlasX Transaction
            var atlasXToApps = unitOfWorkAtlasX.WAtlasXToAppRepository.Get(
                filter: app => app.AppNameId == AppNameId && app.AppXId == transactionId).ToList();

            WAtlasXToApp atlasXToApp = null;

            if (atlasXToApps.Any())
                atlasXToApp = atlasXToApps.First();

            if (atlasXToApp != null)
            {
                // Get AtlasX WAtlasXToAppWPerson                                                                                      
                var atlasXToAppWPerson =
                    atlasXToApp.WAtlasXtoAppWPersons.SingleOrDefault(
                        person => person.RelationshipNumber == relationshipNumber);

                if (atlasXToAppWPerson != null)
                {
                    // Update WAtlasXToAppWPerson In AtlasX
                    atlasXToAppWPerson.Active = status;
                    atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;
                    unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(atlasXToAppWPerson);
                }
            }

            unitOfWorkAtlasX.Save();
        }

        public static void UpdateAgentToPrimaryInAtlasX(Int32 transactionId,
            Int32 relationshipNumber,            
            IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            // Get AtlasX Transaction
            var atlasXToApps = unitOfWorkAtlasX.WAtlasXToAppRepository.Get(
                filter: app => app.AppNameId == AppNameId && app.AppXId == transactionId).ToList();

            WAtlasXToApp atlasXToApp = null;

            if (atlasXToApps.Any())
                atlasXToApp = atlasXToApps.First();

            if (atlasXToApp != null)
            {
                foreach (var atlasXToAppWPerson in atlasXToApp.WAtlasXtoAppWPersons)
                {
                    if (atlasXToAppWPerson.RelationshipNumber == relationshipNumber)
                    {
                        atlasXToAppWPerson.AtlasXtoAppRoleId = 200; // Primary                        
                        atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;                        
                    }
                    else
                    {
                        if (atlasXToAppWPerson.AtlasXtoAppRoleId == 200)
                        {
                            atlasXToAppWPerson.AtlasXtoAppRoleId = 600; // Partner
                            atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;                            
                        }
                    }

                    if (atlasXToAppWPerson.EntityStateForGraphsUpdates == State.Modified)
                        unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(atlasXToAppWPerson);
                    
                }                                                                                      
            }

            unitOfWorkAtlasX.Save();
        }

        public static void UpdateAgentToSecondaryInAtlasX(Int32 transactionId,
            Int32 relationshipNumber,            
            IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            // Get AtlasX Transaction
            var atlasXToApps = unitOfWorkAtlasX.WAtlasXToAppRepository.Get(
                filter: app => app.AppNameId == AppNameId && app.AppXId == transactionId).ToList();

            WAtlasXToApp atlasXToApp = null;

            if (atlasXToApps.Any())
                atlasXToApp = atlasXToApps.First();

            if (atlasXToApp != null)
            {
                foreach (var atlasXToAppWPerson in atlasXToApp.WAtlasXtoAppWPersons)
                {
                    if (atlasXToAppWPerson.RelationshipNumber == relationshipNumber)
                    {
                        atlasXToAppWPerson.AtlasXtoAppRoleId = 600; // Partner
                        atlasXToAppWPerson.EntityStateForGraphsUpdates = State.Modified;
                        unitOfWorkAtlasX.WAtlasXToAppWPersonRepository.Update(atlasXToAppWPerson);
                    }
                }
            }

            unitOfWorkAtlasX.Save();
        }
    }
}
