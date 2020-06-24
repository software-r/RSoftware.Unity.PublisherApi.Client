namespace RSoftware.Unity.PublisherApi.Client.Models.Downloads
{
    using RSoftware.Unity.PublisherApi.Client.Misc;
    using System;

    public partial class PackageDownloads
    {
        private readonly string[] _data;

        public string PackageName => _data[0];

        public int Quantity => int.TryParse(_data[1], out var result) ? result : 0;

        public DateTimeOffset FirstDownload => Utility.ParseDt(_data[2]);

        public DateTimeOffset LastDownload => Utility.ParseDt(_data[3]);

        public string ShortUrl { get; }

        public PackageDownloads(string[] data, string shortUrl)
        {
            _data = data;
            ShortUrl = shortUrl;
        }
    }
}
