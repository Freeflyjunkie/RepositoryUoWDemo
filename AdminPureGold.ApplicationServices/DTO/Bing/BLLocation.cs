namespace AdminPureGold.ApplicationServices.DTO.Bing
{
    public class BLLocation
    {
        public BLBoundingBox boundingBox { get; set; }
        public string name { get; set; }
        public BLPoint point { get; set; }
        public string entityType { get; set; }
        public BLAddress address { get; set; }
        public string confidence { get; set; }
        public BLGeocodePoint[] geocodePoints { get; set; }
        public string[] matchCodes { get; set; }
    }
}
