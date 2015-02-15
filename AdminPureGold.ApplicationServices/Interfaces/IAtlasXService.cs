using System;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IAtlasXService
    {
        Property GetPropertyById(Int32 atlasXPropertyId);        
        PropertyAlternate GetPropertyAlternateById(Int32 atlasXPropertyAlternateId);
        UspsValidatedProperty ValidatePropertyUsingUsps(String address1, String address2, String city, String state, String zip);
        void GetValidatedPropertyIds(Int32 personNumber, String address1, String address2, String city,
            String state, String zip, out Int32 propertyId, out Int32 propertyAlternateId);
        Int32 GetMrcTransactiondIdBySaleId(Int32 saleId);
    }
}
