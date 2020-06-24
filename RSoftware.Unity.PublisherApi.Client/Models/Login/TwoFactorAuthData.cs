namespace RSoftware.Unity.PublisherApi.Client.Models.Login
{
    public class TwoFactorAuthData
    {
        public string ActionUrl { get; set; }
        public string GenesisToken { get; set; }
        public string AuthenticityToken { get; set; }
    }
}
