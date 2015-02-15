using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.ViewModels;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class CreateCustomerViewModelBuilder
    {
        public static CreateCustomerViewModel GetViewModel(
            String referenceNumber, IToolboxService toolboxService)
        {
            var list = toolboxService.WeichertSLService.GetListWithClosedSaleByReferenceNumber(referenceNumber);
            if (list != null)
            {
                var sale = list.Sales.SingleOrDefault(s => s.Closing != null);
                var canCreate = false;
                var agentViewModels = Enumerable.Empty<AgentViewModel>();
                if (sale != null)
                {
                    if (sale.SaleToAssociates.Any())
                    {
                        agentViewModels =
                            AgentViewModelBuilder.GetViewModels(
                                sale.SaleToAssociates.Select(t => t.RelationshipNumber), toolboxService);
                    }

                    var mrcId = toolboxService.AtlasXService.GetMrcTransactiondIdBySaleId(sale.SaleId);
                    if (mrcId == 0)
                    {
                        canCreate = true;
                    }
                }

                return new CreateCustomerViewModel
                {
                    List = list,
                    ClosedSale = sale,
                    AgentViewModels = agentViewModels,
                    AllowCreate = canCreate
                };
            }     
            return null;    
        }
    }
}