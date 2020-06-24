using RSoftware.Unity.PublisherApi.Client.Models.Login;
using System;
using System.Threading.Tasks;

namespace RSoftware.Unity.PublisherApi.Client.ConsoleTestApp
{
    class Program
    {
        private const string USERNAME = "";
        private const string PASSWORD = "";

        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var client = new PublisherApiClient();

            var result = await client.LoginAsync(USERNAME, PASSWORD);

            if (result.Status == LoginStatus.TFA)
            {
                Console.WriteLine($"Please, write TFA code: ");
                var tfaCode = Console.ReadLine();
                Console.WriteLine($"TFA Code: {tfaCode}");
                result = await client.LoginAsync(result.TfaResumeData, tfaCode);
            }

            var info = await client.GetUserInfoAsync();

            var invoices = await client.VerifyInvoicesAsync(new[] { "10170689441353" });

            var salesPeriods = await client.GetSalesPeriodsAsync();

            var packages = await client.GetPackagesInfoAsync();

            Console.WriteLine($"Result: {result}; Info: {info.Name}");

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Invoice: {invoice.Id}; Refunded: {invoice.IsRefunded}; Status: {invoice.Status}");
            }

            foreach (var period in salesPeriods)
            {
                Console.WriteLine($"Period: {period.Name}; RawValue: {period.RawValue}");
                var sales = await client.GetSalesAsync(period);
                var downloads = await client.GetDownloadsAsync(period.Value);
            }
        }
    }
}
