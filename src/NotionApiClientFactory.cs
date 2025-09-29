using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Flow.Launcher.Plugin.Notion
{
    internal static class NotionApiClientFactory
    {
        internal const string ApiVersion = "2025-09-03";

        internal static HttpClient Create(Settings settings)
        {
            var client = new HttpClient();
            ApplyDefaults(client, settings);
            return client;
        }

        internal static void ApplyDefaults(HttpClient client, Settings settings)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            var token = settings?.InernalInegrationToken?.Trim();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (!client.DefaultRequestHeaders.Contains("Notion-Version"))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Notion-Version", ApiVersion);
            }

            if (!client.DefaultRequestHeaders.Accept.Any(static h => h.MediaType == "application/json"))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
    }
}
