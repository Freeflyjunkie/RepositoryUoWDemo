using System.Globalization;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using System;
using ServiceManager.AtlasX;
using Property = AdminPureGold.Domain.Models.AtlasX.Property;

namespace AdminPureGold.ApplicationServices.Services
{    
    public class AtlasXService : IAtlasXService
    {
        private const int NegativeOne = -1;
        private const int PositiveOne = 1;
        private const int OriginationId = 2;
        private const int WeichertWayPropertyId = 440000000;
        private readonly IUnitOfWorkAtlasX _unitOfWorkAtlasX;

        public AtlasXService(IUnitOfWorkAtlasX unitOfWorkAtlasX)
        {
            _unitOfWorkAtlasX = unitOfWorkAtlasX;
        }
        public Property GetPropertyById(Int32 atlasXPropertyId)
        {
            return _unitOfWorkAtlasX.PropertyRepository.GetById(atlasXPropertyId);
        }
        public PropertyAlternate GetPropertyAlternateById(Int32 atlasXPropertyAlternateId)
        {
            return _unitOfWorkAtlasX.PropertyAlternateRepository.GetById(atlasXPropertyAlternateId);
        }
        public UspsValidatedProperty ValidatePropertyUsingUsps(String address1, String address2, String city, String state, String zip)
        {
            var atlasXPropertyManager = new ServiceManager.AtlasX.Property();                        
            var validatedProperty = atlasXPropertyManager.ValidatePropertyDPV(address1, address2, city, state, zip, 
                NegativeOne.ToString(CultureInfo.InvariantCulture),
                PositiveOne.ToString(CultureInfo.InvariantCulture), 
                OriginationId.ToString(CultureInfo.InvariantCulture));

            return new UspsValidatedProperty
                    {
                        Address1 = validatedProperty.Address1,
                        Address2 = validatedProperty.Address2,
                        City = validatedProperty.City,
                        State = validatedProperty.State,
                        Zip = validatedProperty.Zip,
                        CorrectionDescription = validatedProperty.CorrectionsDesc,
                        DpvDescription = validatedProperty.DPVDesc,
                        DpvNotesDescription = validatedProperty.DPVNotesDesc,
                        ErrorCode = validatedProperty.ErrorCode,
                        GeocodeLevelDescription = validatedProperty.GeocodeLevelDescription,
                        Validated = validatedProperty.Validated
                    };            
        }
        public void GetValidatedPropertyIds(Int32 personNumber, String address1, String address2, String city, String state, String zip, 
            out Int32 propertyId, out Int32 propertyAlternateId)
        {
            propertyAlternateId = NegativeOne;
            propertyId = WeichertWayPropertyId;

            var atlasXPropertyManager = new ServiceManager.AtlasX.Property();
            var validatedProperty = atlasXPropertyManager.ValidatePropertyDPV(address1, address2, city, state, zip,
                NegativeOne.ToString(CultureInfo.InvariantCulture),
                PositiveOne.ToString(CultureInfo.InvariantCulture),
                OriginationId.ToString(CultureInfo.InvariantCulture));

            var prevalidatedProperty = atlasXPropertyManager.CreateAlternateProperty(propertyId.ToString(CultureInfo.InvariantCulture), 
                address1, address2, "", "", city, state, zip);
            
            switch (validatedProperty.Validated)
            {
                case "0":
                    atlasXPropertyManager.UpdatePropertyAlternate(prevalidatedProperty, personNumber, out propertyAlternateId);                    
                    break; 

                case "1":
                    atlasXPropertyManager.UpdateValidatedAddress(validatedProperty,
                                prevalidatedProperty, personNumber, out propertyId, out propertyAlternateId);

                    if (propertyAlternateId == -1)
                    {
                        prevalidatedProperty.PropertyID = propertyId.ToString(CultureInfo.InvariantCulture);
                        atlasXPropertyManager.UpdatePropertyAlternate(prevalidatedProperty, personNumber,
                            out propertyAlternateId);
                    }
                    break;                    

                case "2":
                    atlasXPropertyManager.UpdateQuaziValidatedAddress(validatedProperty,
                                prevalidatedProperty, personNumber, out propertyId, out propertyAlternateId);

                    if (propertyAlternateId == -1)
                    {
                        var alternateProperty = atlasXPropertyManager.CreateAlternateProperty(propertyId.ToString(CultureInfo.InvariantCulture),
                            prevalidatedProperty.Address1, prevalidatedProperty.Address2,
                            "", "",
                            prevalidatedProperty.City,
                            prevalidatedProperty.State,
                            prevalidatedProperty.Zip);                        
                    }
                    break;                    
            }            
        }
        public int GetMrcTransactiondIdBySaleId(int saleId)
        {
            long? atlasXTransactionId = TransactionManager.
               GetAtlasXTransactionIDByApplicationTransactionID(TransactionManager.ApplicationName.OSSII_Sale, saleId);

            int? mrcId = TransactionManager.
                GetApplicationTransactionIDByAtlasXTransactionID(TransactionManager.ApplicationName.MRC_Direct, atlasXTransactionId);
         
            return mrcId ?? 0;
        }
    }
}
