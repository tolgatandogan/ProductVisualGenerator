using System.Text.Json;
using Desc2DesignAI.Business.Services.Abstract;
using ProductVisualGenerator;
using Desc2DesignAI.Core.Models;

namespace Desc2DesignAI.Business.Services.Concrete
{
    public class OpenAIImageService : IAiImageService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly AppConfig _config;

        public OpenAIImageService(AppConfig config)
        {
            _config = config;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.ApiKey}");
        }

        public async Task<IEnumerable<string>> GenerateImagesAsync(string prompt)
        {
            var response = await SendRequestToDallE(prompt);
            return response;
        }

        private async Task<IEnumerable<string>> SendRequestToDallE(string prompt)
        {
            var requestBody = new
            {
                model = "dall-e-3",
                prompt,
                n = _config.DefaultImageCount,
                size = _config.DefaultSize
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(
                "https://api.openai.com/v1/images/generations",
                new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var test = JsonSerializer.Deserialize<DallEResponse>(content);
            return test.Data.Select(img => img.Url);
        }

        public void Dispose() => _httpClient?.Dispose();
    }
}