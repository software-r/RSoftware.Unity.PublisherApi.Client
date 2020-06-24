namespace RSoftware.Unity.PublisherApi.Client.Models.Login
{
    public class LoginResult
    {
        public string AccessToken { get; set; }
        public TwoFactorAuthData TfaResumeData { get; set; }
        public LoginStatus Status { get; set; }
    }
}
