using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace DiagnoseMe.Tools
{
    public class ChatGptService
    {
        private readonly string _apiKey = "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";
        private readonly HttpClient httpClient;
        private const string apiUrl = "https://api.openai.com/v1/chat/completions";

        public ChatGptService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = message }
                },
                max_tokens = 2000
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(apiUrl, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Błąd: {response.StatusCode} - {responseContent}";
                }

                var responseJson = JsonSerializer.Deserialize<JsonDocument>(responseContent);
                string chatResponse = responseJson.RootElement
                                .GetProperty("choices")[0]
                                .GetProperty("message")
                                .GetProperty("content")
                                .GetString();

                return chatResponse;
            }
            catch (Exception ex)
            {
                return $"Błąd: {ex.Message}";
            }
        }
    }
}
