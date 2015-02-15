using System;
using System.Linq;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.ViewModels;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class SurveySearchViewModelBuilder
    {
        public static SurveySearchViewModel GetViewModel(
            String referenceNumber, IToolboxService toolboxService)
        {
            var list = toolboxService.WeichertSLService.GetListWithPropertyByReferenceNumber(referenceNumber);

            if (list != null)
            {
                var sale = list.Sales.SingleOrDefault(s => s.Closing != null);

                return new SurveySearchViewModel
                {
                    List = list,
                    ClosedSale = sale,
                };
            }
            return null;
        }
    }
}