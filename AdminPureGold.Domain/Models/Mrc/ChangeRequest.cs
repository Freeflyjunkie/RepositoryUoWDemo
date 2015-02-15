using AdminPureGold.Domain.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class ChangeRequest : IModelWithState
    {
        public Int32 ChangeRequestId { get; set; }
        public Int32 TransactionId { get; set; }
        public Int16 ChangeRequestCategoryId { get; set; }
        public Int16 ChangeRequestStatusId { get; set; }
        public Int32 PersonNumber { get; set; }
        public String Detail { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public virtual ChangeRequestCategory ChangeRequestCategory { get; set; }
        public virtual ChangeRequestStatus ChangeRequestStatus { get; set; }
        public virtual Collection<ChangeRequestComment> ChangeRequestComments { get; set; }        

        public State EntityStateForGraphsUpdates { get; set; } 
    }
}

/***************************
 * For Address Changes
 **************************/
//<changerequestdetail>
//    <oldvalue>
//        <atlasxpropertyid></atlasxpropertyid>
//        <atlasxpropertyalternateid></atlasxpropertyalternateid>
//        <address1></address1>
//        <address2></address2>
//        <city></city>
//        <state></state>
//        <zip></zip>
//    </oldvalue>
//    <newvalue>
//        <atlasxpropertyid></atlasxpropertyid>
//        <atlasxpropertyalternateid></atlasxpropertyalternateid>
//        <address1></address1>
//        <address2></address2>
//        <city></city>
//        <state></state>
//        <zip></zip>
//    </newvalue>
//</changerequestdetail>

/***************************
 * For Name Changes
 **************************/
//<changerequestdetail>
//    <oldvalue>
//        <customername></customername>
//        <envelopename></envelopename>
//    </oldvalue>
//    <newvalue>
//        <customername></customername>
//        <envelopename></envelopename>
//    </newvalue>
//</changerequestdetail>

/***************************
 * For Agent Changes
 **************************/
//<changerequestdetail>
//    <newvalue>
//        <personnumber></personnumber>
//        <relationshipnumber></relationshipnumber>
//        <active></active>
//    </newvalue>
//</changerequestdetail>

/***************************
 * For Removal 
 **************************/
//<changerequestdetail>
//    <newvalue>
//      <transactionid></transactionid>
//      <active></active>
//    </newvalue>
//</changerequestdetail>

/***************************
 * For Other 
 **************************/
//<changerequestdetail>
//    <newvalue>
//      <detail>See Comments</detail>
//    </newvalue>
//</changerequestdetail>