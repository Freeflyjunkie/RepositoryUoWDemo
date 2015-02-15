using System;
using System.Collections.Generic;
using System.Net;
using AdminPureGold.ApplicationServices.DTO.Bing;
using AdminPureGold.ApplicationServices.Interfaces;
using Newtonsoft.Json;

namespace AdminPureGold.ApplicationServices.Services
{
    public class VirtualEarthService : IVirtualEarthService
    {
        private const string BingMapsKey = "AhwAt-KYqO7Eg-c5gSJUtfMcDD8ByR1yAqQrYol2n5WzAYflKkoDwye4bEXvTrbO";

        public IEnumerable<Location> FindLocationByAddress(string addressLine1, string city, string state, string zip)
        {
            var locations = new List<Location>();
            var adminDistrict = String.IsNullOrEmpty(state) ? "-" : state;
            var postalCode = String.IsNullOrEmpty(zip) ? "-" : zip;
            var locality = String.IsNullOrEmpty(city) ? "-" : city;
            var addressLine = String.IsNullOrEmpty(addressLine1) ? "-" : addressLine1;

            string url = String.Format(
                "http://dev.virtualearth.net/REST/v1/Locations/US/{0}/{1}/{2}/{3}?key={4}",
                adminDistrict.Trim(),
                postalCode.Trim(),
                locality.Trim(),
                addressLine.Trim(),
                BingMapsKey.Trim());

            var webClient = new WebClient();
            var response = webClient.DownloadString(new Uri(url));
            var responseObj = JsonConvert.DeserializeObject<BLResponse>(response);

            foreach (var resources in responseObj.resourceSets[0].resources)
            {
                var isAddressMatch = false;
                if (resources.address.addressLine != null
                    && addressLine1 != null)
                {
                    isAddressMatch = resources.address.addressLine.ToUpper().Equals(addressLine1.ToUpper());
                }

                var isCityMatch = false;
                if (resources.address.locality != null
                    && city != null)
                {
                    isCityMatch = resources.address.locality.ToUpper().Equals(city.ToUpper());
                }

                var isStateMatch = false;
                if (resources.address.adminDistrict != null
                    && state != null)
                {
                    isStateMatch = resources.address.adminDistrict.ToUpper().Equals(state.ToUpper());
                }

                var isZipMatch = false;
                if (resources.address.postalCode != null
                    && zip != null)
                {
                    isZipMatch = resources.address.postalCode.ToUpper().Equals(zip.ToUpper());
                }

                var location = new Location
                {
                    Address = resources.address.addressLine,
                    IsAddressMatch = isAddressMatch,
                    City = resources.address.locality,
                    IsCityMatch = isCityMatch,
                    Confidence = resources.confidence.ToUpper(),
                    County = resources.address.adminDistrict2,
                    State = resources.address.adminDistrict,
                    IsStateMatch = isStateMatch,
                    Zip = resources.address.postalCode,
                    IsZipMatch = isZipMatch,
                    EntityType = resources.entityType
                };

                locations.Add(location);
            }

            return locations;
        }
    }
}
