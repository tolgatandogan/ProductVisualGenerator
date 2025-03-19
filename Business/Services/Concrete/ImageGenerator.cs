using Desc2DesignAI.Business.Services.Abstract;
using Desc2DesignAI.Core.Models;
using ProductVisualGenerator;

namespace Desc2DesignAI.Business.Services.Concrete
{
    // 4. Coordinator Class
    public class ImageGenerator
    {
        private readonly ICsvReader _csvReader;
        private readonly IAiImageService _aiService;
        private readonly IImageDownloader _downloader;
        private readonly AppConfig _config;

        public ImageGenerator(
            ICsvReader csvReader,
            IAiImageService aiService,
            IImageDownloader downloader,
            AppConfig config)
        {
            _csvReader = csvReader;
            _aiService = aiService;
            _downloader = downloader;
            _config = config;
        }

        public async Task GenerateFromCsv(string csvPath)
        {
            var products = _csvReader.ReadProducts(csvPath);

            foreach (var product in products)
            {
                try
                {
                    var prompt = CreatePrompt(product);
                    var imageUrls = await _aiService.GenerateImagesAsync(prompt);
                    await _downloader.DownloadAsync(product.ProductName, imageUrls);
                    await Task.Delay(_config.DelayBetweenRequests);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {product.BaseCode}: {ex.Message}");
                }
            }
        }

        private static string CreatePrompt(Product product) =>
            $"{product.BaseCode}, {product.ProductName}, {product.AttributeSet}, {product.ImageSize}, {product.VariantableColor}, {product.Category}, {product.BaseCategory}, {product.ChildCategory}, {product.Quality}, {product.Size}";
    }
}