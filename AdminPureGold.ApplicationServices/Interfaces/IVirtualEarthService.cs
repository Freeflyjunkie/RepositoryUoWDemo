using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO.Bing;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IVirtualEarthService
    {
        IEnumerable<Location> FindLocationByAddress(string addressLine1, string city, string state, string zip);
    }
}
