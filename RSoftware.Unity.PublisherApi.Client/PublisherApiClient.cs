using RSoftware.Unity.PublisherApi.Client.Models.Accounts;
using RSoftware.Unity.PublisherApi.Client.Models.User;

namespace RSoftware.Unity.PublisherApi.Client
{
    using Newtonsoft.Json;
    using RSoftware.Unity.PublisherApi.Client.Exceptions;
    using RSoftware.Unity.PublisherApi.Client.Misc;
    using RSoftware.Unity.PublisherApi.Client.Models.Downloads;
    using RSoftware.Unity.PublisherApi.Client.Models.Invoices;
    using RSoftware.Unity.PublisherApi.Client.Models.Login;
    using RSoftware.Unity.PublisherApi.Client.Models.Packages;
    using RSoftware.Unity.PublisherApi.Client.Models.Publisher;
    using RSoftware.Unity.PublisherApi.Client.Models.Revenues;
    using RSoftware.Unity.PublisherApi.Client.Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    public class PublisherApiClient : IPublisherApiClient
    {
        private string _accessToken;

        private CookieContainer _cookieContainer;

        private HttpClient _httpClient;

        /// <summary>
        /// Client state.
        /// </summary>
        public bool IsLoggedIn { get; private set; }

        public PublisherApiClient()
        {
            _cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Login to publisher portal with previously obtained token.
        /// </summary>
        /// <param name="token">Previously saved token in format KharmaToken.KharmaSession</param>
        public void LoginWithTokenAsync(string token)
        {
            if (IsLoggedIn)
            {
                return;
            }

            IsLoggedIn = true;
            _accessToken = token;
        }

        /// <summary>
        /// Login to Unity Publisher Portal.
        /// </summary>
        /// <param name="username">Email or username</param>
        /// <param name="password">Password</param>
        /// <param name="tfaResumeData">Two-factor auth data previously obtained</param>
        /// <param name="tfaCode">Two-factor auth code</param>
        /// <returns>Returns LoginResult with current status of login and Two-factor data if Unity Publisher Portal need this.</returns>
        public async Task<LoginResult> LoginAsync(string username, string password, TwoFactorAuthData tfaResumeData = null, string tfaCode = null)
        {
            if (IsLoggedIn)
            {
                return new LoginResult()
                {
                    AccessToken = _accessToken,
                    Status = LoginStatus.Success,
                };
            }

            var result = await GetLoginTokenAsync(username, password, tfaResumeData, tfaCode);

            if (result.Status == LoginStatus.Success)
            {
                LoginWithTokenAsync(result.AccessToken);
            }

            return result;
        }

        /// <summary>
        /// Login to Unity Publisher Portal with TFA data specified.
        /// </summary>
        /// <param name="tfaResumeData">Two-factor auth data previously obtained</param>
        /// <param name="tfaCode">Two-factor auth code</param>
        /// <returns>Returns LoginResult with current status of login.</returns>
        public async Task<LoginResult> LoginAsync(TwoFactorAuthData tfaResumeData, string tfaCode)
        {
            return await LoginAsync(null, null, tfaResumeData, tfaCode);
        }

        /// <summary>
        /// Logout from Unity Publisher Portal and disposing access token.
        /// </summary>
        public async Task LogoutAsync()
        {
            if (!IsLoggedIn)
            {
                return;
            }

            IsLoggedIn = false;

            var response = await GetHttpClient().GetAsync("/logout");

            if (!response.IsSuccessStatusCode)
            {
                throw new UnityPublisherApiException($"Logout request failed, error code {response.StatusCode}", response.StatusCode);
            }

            _accessToken = null;
            _httpClient.Dispose();
            _cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Get account information.
        /// </summary>
        /// <returns>Information about user.</returns>
        public async Task<UserInfo> GetUserInfoAsync()
        {
            return await FetchDataAsync<UserInfo>("/api/user/overview.json"); ;
        }

        /// <summary>
        /// Get publisher information.
        /// </summary>
        /// <returns>Information about publisher</returns>
        public async Task<PublisherInfo> GetPublisherInfoAsync()
        {
            AssertIsLoggedIn();

            var data = await FetchDataAsync<PublisherInfoResponse>("/api/publisher/overview.json");

            return data.Overview;
        }

        /// <summary>
        /// Get verified invoices by id.
        /// </summary>
        /// <param name="invoices">Invoices ids</param>
        /// <returns>Information about requested invoices.</returns>
        public async Task<Invoice[]> VerifyInvoicesAsync(string[] invoices)
        {
            AssertIsLoggedIn();

            var invoice = string.Join(",", invoices);

            var publisherInfo = await GetPublisherInfoAsync();

            var data = await FetchDataAsync<InvoiceResponse>($"/api/publisher-info/verify-invoice/{publisherInfo.Id}/{invoice}.json");

            return data.Payload.Select(payload => new Invoice(payload)).ToArray();
        }

        /// <summary>
        /// Get verified invoices by id. Don't require login to publisher portal.
        /// </summary>
        /// <param name="apiKey">API key from publisher portal</param>
        /// <param name="invoices">Invoices ids</param>
        /// <returns>Information about requested invoices.</returns>
        public async Task<Models.Api.Invoice[]> VerifyInvoicesByApiKeyAsync(string apiKey, string[] invoices)
        {
            var invoice = string.Join(",", invoices);

            var httpClient = CreateHttpClient();

            var parameters = new Dictionary<string, string>()
            {
                { "key", apiKey },
                { "invoice", invoice }
            };

            var json = await httpClient.GetStringAsync(ClientConsts.API_VERIFY_INVOICE_URL + ToQueryString(parameters));
            var response = JsonConvert.DeserializeObject<Models.Api.InvoiceResponse>(json);

            return response.Invoices;
        }

        /// <summary>
        /// Get revenue information.
        /// </summary>
        /// <returns>Information about revenue.</returns>
        public async Task<Revenue[]> GetRevenueAsync()
        {
            AssertIsLoggedIn();

            var publisherInfo = await GetPublisherInfoAsync();

            var response = await FetchDataAsync<RevenueResponse>($"/api/publisher-info/revenue/{publisherInfo.Id}.json");

            return response.Payload.Select(data => new Revenue(data)).ToArray();
        }

        /// <summary>
        /// Get packages information presented in portal.
        /// </summary>
        /// <returns>Packages information and it's statuses.</returns>
        public async Task<PackagesInfo> GetPackagesInfoAsync()
        {
            AssertIsLoggedIn();

            return await FetchDataAsync<PackagesInfo>("/api/management/packages.json");
        }

        /// <summary>
        /// Get downloads information about packages for selected period.
        /// </summary>
        /// <param name="period">Requested period - year, month</param>
        /// <param name="filter">Specify packages type</param>
        /// <returns>Information about package downloads.</returns>
        public async Task<PackageDownloads[]> GetDownloadsAsync(DateTimeOffset period, PackageFilter filter = PackageFilter.All)
        {
            AssertIsLoggedIn();

            CheckYear(period);

            var publisherInfo = await GetPublisherInfoAsync();
            var date = period.ToString(ClientConsts.PERIOD_DT_FORMAT);
            var parameters = new Dictionary<string, string>()
            {
                { "package_filter", filter.ToString().ToLowerInvariant() }
            };

            var response = await FetchDataAsync<DownloadsResponse>($"/api/publisher-info/downloads/{publisherInfo.Id}/{date}.json" + ToQueryString(parameters));
            var downloads = new PackageDownloads[response.Payload.Length];

            for (int i = 0; i < response.Payload.Length; i++)
            {
                downloads[i] = new PackageDownloads(response.Payload[i], response.Links[i].ShortUrl);
            }

            return downloads;
        }

        /// <summary>
        /// Get available sales periods.
        /// </summary>
        /// <returns>Sales periods.</returns>
        public async Task<SalesPeriod[]> GetSalesPeriodsAsync()
        {
            AssertIsLoggedIn();

            var publisherInfo = await GetPublisherInfoAsync();

            var response = await FetchDataAsync<SalesPeriodsResponse>($"/api/publisher-info/months/{publisherInfo.Id}.json");

            return response.Periods;
        }

        /// <summary>
        /// Get sales for requested period.
        /// </summary>
        /// <param name="period">Period - year, month</param>
        /// <returns>Sales in period.</returns>
        public async Task<SalesPeriodInfo> GetSalesAsync(SalesPeriod period)
        {
            return await GetSalesAsync(period.Value);
        }

        /// <summary>
        /// Get sales for requested period.
        /// </summary>
        /// <param name="period">Period - year, month</param>
        /// <returns>Sales in period.</returns>
        public async Task<SalesPeriodInfo> GetSalesAsync(DateTimeOffset period)
        {
            AssertIsLoggedIn();

            CheckYear(period);

            var publisherInfo = await GetPublisherInfoAsync();
            var date = period.ToString(ClientConsts.PERIOD_DT_FORMAT);

            var response = await FetchDataAsync<SalesResponse>($"/api/publisher-info/sales/{publisherInfo.Id}/{date}.json");
            var packages = new SalesPackageInfo[response.Payload.Length];

            for (int i = 0; i < response.Payload.Length; i++)
            {
                packages[i] = new SalesPackageInfo(response.Payload[i], response.PackageIncomes[i].ShortUrl, response.PackageIncomes[i].RawNet);
            }

            return new SalesPeriodInfo(packages, Utility.ParseFloat(publisherInfo.PayoutCut));
        }

        /// <summary>
        /// Get user accounts for publisher.
        /// </summary>
        /// <returns>All accounts for publisher.</returns>
        public async Task<Account[]> GetUserAccountsAsync()
        {
            AssertIsLoggedIn();

            var publisherInfo = await GetPublisherInfoAsync();

            var response = await FetchDataAsync<AccountsResponse>($"/api/publisher-info/users/{publisherInfo.Id}.json");

            return response.Payload.Select(data => new Account(data)).ToArray();
        }

        private async Task<TDataType> FetchDataAsync<TDataType>(string url)
        {
            var json = await FetchJsonAsync(url);
            return JsonConvert.DeserializeObject<TDataType>(json);
        }

        private async Task<string> FetchJsonAsync(string url)
        {
            AssertIsLoggedIn();

            var response = await GetHttpClient().GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnityPublisherApiException($"Fetching data from \"{url}\" failed, error code {response.StatusCode}", response.StatusCode);
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<LoginResult> GetLoginTokenAsync(string username, string password, TwoFactorAuthData tfaResumeData, string tfaCode)
        {
            var cookieContainer = new CookieContainer();

            using (var httpClient = CreateHttpClient(null, cookieContainer))
            {
                string pageData;

                if (tfaResumeData == null)
                {
                    var response = await httpClient.GetAsync(ClientConsts.SALES_URL);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new UnityPublisherApiException($"Login failed, error code {response.StatusCode}", response.StatusCode);
                    }

                    var redirectUrl = response.RequestMessage.RequestUri;
                    var genesisToken = GetCookie(cookieContainer, redirectUrl, ClientConsts.GENESIS_COOKIE_NAME);

                    if (string.IsNullOrWhiteSpace(genesisToken))
                    {
                        throw new UnityPublisherApiException($"{ClientConsts.GENESIS_COOKIE_NAME} cookie not found", response.StatusCode);
                    }

                    pageData = await response.Content.ReadAsStringAsync();

                    var authTokenRegex = new Regex("<input type=\"hidden\" name=\"authenticity_token\" value=\"(.+)\" />");
                    var authTokenMatches = authTokenRegex.Matches(pageData);
                    var authToken = authTokenMatches.Count > 0 ? authTokenMatches[0].Groups[1].Value : null;

                    if (string.IsNullOrWhiteSpace(authToken))
                    {
                        throw new UnityPublisherApiException($"Page authenticity token not found");
                    }

                    response = await httpClient.PostAsync(redirectUrl, new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        {"utf8", "✓"},
                        {"_method", "put"},
                        {"authenticity_token", authToken},
                        {"conversations_create_session_form[email]", username},
                        {"conversations_create_session_form[password]", password},
                        {"conversations_create_session_form[remember_me]", true.ToString()},
                        {"commit", "Log in"},
                    }));

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new UnityPublisherApiException($"Conversation failed, error code {response.StatusCode}", response.StatusCode);
                    }

                    pageData = await response.Content.ReadAsStringAsync();

                    if (pageData.Contains("conversations_tfa_required_form[verify_code]"))
                    {
                        var tfaActionRegex = new Regex("id=\"new_conversations_tfa_required_form\" action=\"(.+?)\"");
                        var tfaActionMatches = tfaActionRegex.Matches(pageData);
                        var tfaActionUrl = tfaActionMatches.Count > 0 ? tfaActionMatches[0].Groups[1].Value : null;

                        return new LoginResult()
                        {
                            TfaResumeData = new TwoFactorAuthData()
                            {
                                ActionUrl = ClientConsts.ID_UNITY_URL + tfaActionUrl,
                                GenesisToken = genesisToken,
                                AuthenticityToken = authToken
                            },
                            Status = LoginStatus.TFA
                        };
                    }
                }
                else
                {
                    SetCookie(cookieContainer, new Uri(ClientConsts.ID_UNITY_URL), ClientConsts.GENESIS_COOKIE_NAME, tfaResumeData.GenesisToken);

                    var response = await httpClient.PostAsync(tfaResumeData.ActionUrl, new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        {"utf8", "✓"},
                        {"_method", "put"},
                        {"authenticity_token", tfaResumeData.AuthenticityToken},
                        {"conversations_tfa_required_form[verify_code]", tfaCode},
                        {"conversations_tfa_required_form[submit_verify_code]", "Verify"},
                    }));

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new UnityPublisherApiException($"TFA conversation failed, error code {response.StatusCode}", response.StatusCode);
                    }

                    pageData = await response.Content.ReadAsStringAsync();
                }

                var bounceRegex = new Regex("window\\.location\\.href \\= \"(.+)\"");
                var bounceMatches = bounceRegex.Matches(pageData);
                var bounceUrl = bounceMatches.Count > 0 ? bounceMatches[0].Groups[1].Value : null;

                var bounceResponse = await httpClient.GetAsync(bounceUrl);

                if (!bounceResponse.IsSuccessStatusCode || string.IsNullOrWhiteSpace(bounceUrl))
                {
                    throw new UnityPublisherApiException($"Bounce failed, error code {bounceResponse.StatusCode}", bounceResponse.StatusCode);
                }

                var bounceUri = new Uri(bounceUrl);

                var kharmaToken = GetCookie(cookieContainer, bounceUri, ClientConsts.TOKEN_COOKIE_NAME);

                if (string.IsNullOrWhiteSpace(kharmaToken))
                {
                    throw new UnityPublisherApiException($"{ClientConsts.TOKEN_COOKIE_NAME} cookie not found");
                }

                var kharmaSession = GetCookie(cookieContainer, bounceUri, ClientConsts.SESSION_COOKIE_NAME);

                if (string.IsNullOrWhiteSpace(kharmaSession))
                {
                    throw new UnityPublisherApiException($"{ClientConsts.SESSION_COOKIE_NAME} cookie not found");
                }

                return new LoginResult()
                {
                    AccessToken = $"{kharmaToken}.{kharmaSession}",
                    Status = LoginStatus.Success
                };
            }
        }

        private HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                if (!string.IsNullOrWhiteSpace(_accessToken))
                {
                    var token = _accessToken.Split('.');

                    _cookieContainer.Add(new Uri(ClientConsts.BASE_ADDRESS), new Cookie(ClientConsts.TOKEN_COOKIE_NAME, token[0]));
                    _cookieContainer.Add(new Uri(ClientConsts.BASE_ADDRESS), new Cookie(ClientConsts.SESSION_COOKIE_NAME, token[1]));
                }

                _httpClient = CreateHttpClient(new Uri(ClientConsts.BASE_ADDRESS), _cookieContainer);
            }

            return _httpClient;
        }

        private HttpClient CreateHttpClient(Uri baseAddress = null, CookieContainer cookieContainer = null)
        {
            var handler = new HttpClientHandler();

            if (cookieContainer != null)
            {
                handler.CookieContainer = cookieContainer;
                handler.UseCookies = true;
            }

            return new HttpClient(handler)
            {
                BaseAddress = baseAddress != null ? baseAddress : null,
                DefaultRequestHeaders =
                {
                    { "User-Agent", ClientConsts.USER_AGENT }
                },
            };
        }

        private void AssertIsLoggedIn()
        {
            if (!IsLoggedIn)
            {
                throw new UnityPublisherApiException("Can't execute operation when not logged in");
            }
        }

        private void CheckYear(DateTimeOffset dt)
        {
            if (dt.Year < 2010)
            {
                throw new UnityPublisherApiException("Year must be after 2009");
            }
        }

        private string ToQueryString(Dictionary<string, string> nvc)
        {
            var @params = nvc.Select(s => string.Format("{0}={1}", HttpUtility.UrlEncode(s.Key), HttpUtility.UrlEncode(s.Value)));
            return "?" + string.Join("&", @params);
        }

        private string GetCookie(CookieContainer container, Uri uri, string name)
        {
            var cookies = container.GetCookies(uri).Cast<Cookie>();

            foreach (var cookie in cookies)
            {
                if (cookie.Name == name)
                {
                    return cookie.Value;
                }
            }

            return null;
        }

        private void SetCookie(CookieContainer container, Uri uri, string name, string value)
        {
            container.Add(uri, new Cookie(name, value));
        }
    }
}
