using System;
using System.Linq;
using System.Xml;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.ApplicationServices.Classes
{
    internal static class ChangeRequestDetailParser
    {
        public static ChangeRequestDetailParsed ParseChangeRequestDetail(ChangeRequest changeRequest, IUnitOfWorkCore unitOfWorkCore)
        {
            switch (changeRequest.ChangeRequestCategoryId)
            {
                case 400: // Address Change                    
                    return ParseAddress(changeRequest);

                case 401: // Agent Change                                        
                    return ParseAgent(changeRequest, unitOfWorkCore);

                case 402: // Name Change                    
                    return ParseName(changeRequest);

                case 403:
                    return new ChangeRequestDetailRemovalParsed
                    {
                        ChangeRequestId = changeRequest.ChangeRequestId,
                        ChangeRequestCategoryId = changeRequest.ChangeRequestCategoryId
                    };

                case 404:
                    return new ChangeRequestDetailOtherParsed
                    {
                        ChangeRequestId = changeRequest.ChangeRequestId,
                        ChangeRequestCategoryId = changeRequest.ChangeRequestCategoryId
                    };

                default:
                    return null;
            }
        }
        public static void UpdateAddressWithParsedDetail(ChangeRequestDetailParsed parsedDetail, Transaction transaction, IUnitOfWorkMrc unitOfWorkMrc)
        {
            var parsedAddress = parsedDetail as ChangeRequestDetailAddressParsed;
            if (parsedAddress != null)
            {                
                transaction.AtlasXPropertyId = parsedAddress.NewAtlasXPropertyId;
                transaction.AtlasXPropertyAlternateId = parsedAddress.NewAtlasXPropertyAlternateId;
                transaction.EntityStateForGraphsUpdates = State.Modified;
                unitOfWorkMrc.TransactionRepository.Update(transaction);
            }
        }
        public static void UpdateCustomerNameWithParsedDetail(ChangeRequestDetailParsed parsedDetail, 
            Transaction transaction, 
            Int32 personNumber, 
            IUnitOfWorkMrc unitOfWorkMrc)
        {
            var parsedName = parsedDetail as ChangeRequestDetailNameParsed;
            if (parsedName != null)
            {
                if (transaction.PresentationDetail != null)
                {
                    transaction.PresentationDetail.CustomerName = parsedName.NewCustomerName;
                    transaction.PresentationDetail.LeaveBehindLetterName = parsedName.NewEnvelopeName;
                    transaction.PresentationDetail.EntityStateForGraphsUpdates = State.Modified;
                    unitOfWorkMrc.TransactionRepository.Update(transaction);
                }
                else
                {
                    var presentationDetail = new PresentationDetail
                    {
                        TransactionId = transaction.TransactionId,
                        CustomerName = parsedName.NewCustomerName,
                        LeaveBehindLetterName = parsedName.NewEnvelopeName,
                        CrtBy = personNumber,
                        CrtDt = DateTime.Now,
                        EntityStateForGraphsUpdates = State.Added
                    };
                    transaction.PresentationDetail = presentationDetail;
                    unitOfWorkMrc.TransactionRepository.Insert(transaction);
                }
            }
        }

        public static void UpdateTransactionWithParsedDetail(Transaction transaction, IUnitOfWorkMrc unitOfWorkMrc)
        {
            transaction.Active = "I";
            transaction.EntityStateForGraphsUpdates = State.Modified;
            unitOfWorkMrc.TransactionRepository.Update(transaction);
        }
        private static ChangeRequestDetailParsed ParseName(ChangeRequest changeRequest)
        {
            var parsedDetail =
                new ChangeRequestDetailNameParsed(changeRequest.ChangeRequestId, changeRequest.ChangeRequestCategoryId);

            try
            {
                var detailDoc = new XmlDocument();
                detailDoc.LoadXml(changeRequest.Detail);
                string[] xpathSection = { "oldvalue", "newvalue" };

                foreach (var xpath in xpathSection)
                {
                    var rootOldValues = detailDoc.SelectSingleNode("changerequestdetail/" + xpath);
                    if (rootOldValues == null)
                        return parsedDetail;

                    var customerName = rootOldValues.SelectSingleNode("customername");
                    var envelopeName = rootOldValues.SelectSingleNode("envelopename");

                    if (customerName == null
                        || envelopeName == null)
                        return parsedDetail;

                    if (xpath == "oldvalue")
                    {
                        parsedDetail.OldCustomerName = customerName.InnerText;
                        parsedDetail.OldEnvelopeName = envelopeName.InnerText;
                    }
                    else
                    {
                        parsedDetail.NewCustomerName = customerName.InnerText;
                        parsedDetail.NewEnvelopeName = envelopeName.InnerText;
                    }
                }

                return parsedDetail;
            }
            catch (Exception)
            {
                return parsedDetail;
            }
        }
        private static ChangeRequestDetailParsed ParseAgent(ChangeRequest changeRequest, IUnitOfWorkCore unitOfWorkCore)
        {
            var parsedDetail =
                new ChangeRequestDetailAgentParsed(changeRequest.ChangeRequestId, changeRequest.ChangeRequestCategoryId);

            try
            {
                var detailDoc = new XmlDocument();
                detailDoc.LoadXml(changeRequest.Detail);

                var rootOldValues = detailDoc.SelectSingleNode("changerequestdetail/newvalue");
                if (rootOldValues == null)
                    return parsedDetail;

                var personnumber = rootOldValues.SelectSingleNode("personnumber");
                var relationshipnumber = rootOldValues.SelectSingleNode("relationshipnumber");
                var officeid = rootOldValues.SelectSingleNode("officeid");
                var payamount = rootOldValues.SelectSingleNode("payamount");
                var sortorder = rootOldValues.SelectSingleNode("sortorder");
                var printperson = rootOldValues.SelectSingleNode("printperson");
                var active = rootOldValues.SelectSingleNode("active");

                if (personnumber == null
                    || relationshipnumber == null
                    || active == null
                    || officeid == null
                    || payamount == null
                    || sortorder == null
                    || printperson == null)
                    return parsedDetail;

                parsedDetail.PersonNumber = Convert.ToInt32(personnumber.InnerText);
                parsedDetail.RelationshipNumber = Convert.ToInt32(relationshipnumber.InnerText);
                parsedDetail.Active = active.InnerText;
                parsedDetail.OfficeId = Convert.ToInt32(officeid.InnerText);
                parsedDetail.PayAmount = Convert.ToInt32(payamount.InnerText);
                parsedDetail.SortOrder = Convert.ToByte(sortorder.InnerText);
                parsedDetail.PrintPerson = printperson.InnerText == "1" ? true : false;
                
                var relateToName = unitOfWorkCore.RelateToNameRepository.Get(name => name.RelationshipNumber == parsedDetail.RelationshipNumber);
                parsedDetail.RelateToName = relateToName.FirstOrDefault();

                return parsedDetail;
            }
            catch (Exception)
            {
                return parsedDetail;
            }
        }
        private static ChangeRequestDetailAddressParsed ParseAddress(ChangeRequest changeRequest)
        {
            var parsedDetail = 
                new ChangeRequestDetailAddressParsed(changeRequest.ChangeRequestId, changeRequest.ChangeRequestCategoryId);

            try
            {
                var detailDoc = new XmlDocument();
                detailDoc.LoadXml(changeRequest.Detail);
                string[] xpathSection = { "oldvalue", "newvalue" };

                foreach (var xpath in xpathSection)
                {
                    var rootOldValues = detailDoc.SelectSingleNode("changerequestdetail/" + xpath);
                    if (rootOldValues == null)
                        return parsedDetail;

                    var atlasXPropertyId = rootOldValues.SelectSingleNode("atlasxpropertyid");
                    var atlasXPropertyAlternateId = rootOldValues.SelectSingleNode("atlasxpropertyalternateid");
                    var address1 = rootOldValues.SelectSingleNode("address1");
                    var address2 = rootOldValues.SelectSingleNode("address2");
                    var city = rootOldValues.SelectSingleNode("city");
                    var state = rootOldValues.SelectSingleNode("state");
                    var zip = rootOldValues.SelectSingleNode("zip");

                    if (atlasXPropertyId == null
                        || atlasXPropertyAlternateId == null
                        || address1 == null
                        || address2 == null
                        || city == null
                        || state == null
                        || zip == null)
                        return parsedDetail;

                    if (xpath == "oldvalue")
                    {
                        parsedDetail.OldAddress1 = address1.InnerText;
                        parsedDetail.OldAddress2 = address2.InnerText;
                        parsedDetail.OldCity = city.InnerText;
                        parsedDetail.OldState = state.InnerText;
                        parsedDetail.OldZip = zip.InnerText;
                        parsedDetail.OldAtlasXPropertyId =
                            Convert.ToInt32(atlasXPropertyId.InnerText);
                        parsedDetail.OldAtlasXPropertyAlternateId =
                            Convert.ToInt32(atlasXPropertyAlternateId.InnerText);
                    }
                    else
                    {
                        parsedDetail.NewAddress1 = address1.InnerText;
                        parsedDetail.NewAddress2 = address2.InnerText;
                        parsedDetail.NewCity = city.InnerText;
                        parsedDetail.NewState = state.InnerText;
                        parsedDetail.NewZip = zip.InnerText;
                        parsedDetail.NewAtlasXPropertyId =
                            Convert.ToInt32(atlasXPropertyId.InnerText);
                        parsedDetail.NewAtlasXPropertyAlternateId =
                            Convert.ToInt32(atlasXPropertyAlternateId.InnerText);
                    }
                }

                return parsedDetail;
            }
            catch (Exception)
            {
                return parsedDetail;
            }
        }
    }
}
