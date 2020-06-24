namespace RSoftware.Unity.PublisherApi.Client
{
    internal static class ClientConsts
    {
        public const string BASE_ADDRESS = "https://publisher.assetstore.unity3d.com";
        
        public const string SALES_URL = "https://publisher.assetstore.unity3d.com/sales.html";

        public const string ID_UNITY_URL = "https://id.unity.com";

        public const string API_VERIFY_INVOICE_URL = "https://api.assetstore.unity3d.com/publisher/v1/invoice/verify.json";

        public const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Safari/537.36";
        public const string TOKEN_COOKIE_NAME = "kharma_token";
        public const string SESSION_COOKIE_NAME = "kharma_session";
        public const string GENESIS_COOKIE_NAME = "_genesis_auth_frontend_session";
     
        public const string TFA_CODE_REQUESTED = "TFA_CODE_REQUESTED";

        public const string PERIOD_DT_FORMAT = "yyyyMM";
    }
}
