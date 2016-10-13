using Microsoft.IdentityModel.Clients.ActiveDirectory;
using RestSharp;
using System.Collections.Generic;

namespace AzureADWrapper
{
    class GraphApiClient
    {
        private const string GraphApiUrl = "https://graph.windows.net";

        public static void CreateUser(string tenant, string clientId, string clientSecret, string name)
        {
            var authenticationContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenant}", false);
            var clientCred = new ClientCredential(clientId, clientSecret);
            var authenticationResult = authenticationContext.AcquireTokenAsync(GraphApiUrl, clientCred).Result; // Blocking
            var header = authenticationResult.AccessTokenType + " " + authenticationResult.AccessToken;
            var restClient = new RestClient(GraphApiUrl + "/" + tenant);
            var request = new RestRequest(Method.POST);
            request.Resource = "users?api-version=1.6";
            request.AddHeader("Authorization", authenticationResult.AccessTokenType + " " + authenticationResult.AccessToken);
            request.AddJsonBody(new Dictionary<string, object>
            {
                {"accountEnabled",true },
                {"displayName", name },
                { "mailNickname", name },
                { "passwordProfile", new Dictionary<string, object> {
                    { "password", "Test1234" },
                    { "forceChangePasswordNextLogin", false}
                } },
                { "userPrincipalName", name + "@" + tenant }
            });
            var response = restClient.ExecuteAsPost(request, "POST");
        }
    }
}
