namespace AdminPureGold.ApplicationServices.DTO.Bing
{
    public class Location
    {
        public string Address { get; set; }
        public bool IsAddressMatch { get; set; }
        public string City { get; set; }
        public bool IsCityMatch { get; set; }
        public string State { get; set; }
        public bool IsStateMatch { get; set; }
        public string Zip { get; set; }
        public bool IsZipMatch { get; set; }
        public string County { get; set; }
        public string Confidence { get; set; }
        public string  EntityType { get; set; }
    }
}
