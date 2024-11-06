using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiagnoseMe.Tools
{
    public class ChatGptService
    {
        private readonly string _apiKey = "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";
        private readonly int _retriesCount = 3;
        private readonly int _delay = 3000;
        public ChatGptService() { }
        public async Task<string> GetChatGPTResponse(string message)
        {
            string errorMsg = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = message }
                    }
                };

                string jsonRequest = JsonSerializer.Serialize(requestBody);
                HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                for (int i = 0; i < _retriesCount; i++)
                {
                    try
                    {
                        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                        if (response.IsSuccessStatusCode)
                        {
                            response.EnsureSuccessStatusCode();
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var responseObject = JsonSerializer.Deserialize<JsonDocument>(jsonResponse);

                            string chatResponse = responseObject.RootElement
                                .GetProperty("choices")[0]
                                .GetProperty("message")
                                .GetProperty("content")
                                .GetString();

                            return chatResponse;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                        {
                            errorMsg = $"Błąd: {response.StatusCode}";
                            await Task.Delay(_delay);
                            continue;
                        }
                        else
                        {
                            return $"Błąd: {response.StatusCode}";
                        }
                    }
                    catch (Exception ex)
                    {
                        return $"Wystąpił błąd: {ex.Message}";
                    }
                }
                return errorMsg;
            }
        }
    }
}
