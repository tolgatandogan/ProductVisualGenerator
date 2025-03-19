using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Desc2DesignAI.Business.Services.Concrete;

namespace ProductVisualGenerator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                var config = new AppConfig(
                    ApiKey: "API-KEY",
                    DefaultImageCount: 1,
                    DefaultSize: "1024x1024",
                    DelayBetweenRequests: 4000
                );

                var serviceProvider = new ServiceFactory(config);
                var imageGenerator = serviceProvider.CreateImageGenerator();

                await imageGenerator.GenerateFromCsv("createproductimage.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error: {ex.Message}");
            }
        }
    }

    public record AppConfig(
        string ApiKey,
        int DefaultImageCount,
        string DefaultSize,
        int DelayBetweenRequests
    );

    public class ServiceFactory
    {
        private readonly AppConfig _config;

        public ServiceFactory(AppConfig config) => _config = config;

        public ImageGenerator CreateImageGenerator()
        {
            return new ImageGenerator(
                csvReader: new CsvReaderService(),
                aiService: new OpenAIImageService(_config),
                downloader: new ImageDownloader(),
                config: _config
            );
        }
    }
}