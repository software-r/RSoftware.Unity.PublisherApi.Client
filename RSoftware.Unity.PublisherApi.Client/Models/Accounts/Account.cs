namespace RSoftware.Unity.PublisherApi.Client.Models.Accounts
{
    public class Account
    {
        private readonly string[] _data;

        public string Id => _data[2];

        public string Email => _data[0];

        public string FullName => _data[1];

        public Account(string[] data)
        {
            _data = data;
        }
    }
}
