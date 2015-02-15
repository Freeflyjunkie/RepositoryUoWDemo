namespace AdminPureGold.ApplicationServices.DTO.Bing
{
    public class BLResponse
    {
        public string copyright { get; set; }
        public string brandLogoUri { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string authenticationResultCode { get; set; }
        public string[] errorDetails { get; set; }
        public string traceId { get; set; }
        public BLResourceSet[] resourceSets { get; set; }
    }
}
