namespace AdminPureGold.ApplicationServices.DTO.Bing
{
    public class BLGeocodePoint : BLPoint
    {
        public string calculationMethod { get; set; }
        public string[] usageTypes { get; set; }
    }
}
