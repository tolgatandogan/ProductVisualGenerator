using Desc2DesignAI.Business.Services.Abstract;

namespace Desc2DesignAI.Business.Services.Concrete
{
    public class ImageDownloader : IImageDownloader
    {
        public async Task DownloadAsync(string productName, IEnumerable<string> urls)
        {
            Directory.CreateDirectory("generated_images");
            using var httpClient = new HttpClient();

            int index = 1;
            foreach (var url in urls)
            {
                var imageBytes = await httpClient.GetByteArrayAsync(url);
                var safeName = SanitizeFileName(productName);
                var path = Path.Combine("generated_images", $"{safeName}_{index++}.png");
                await File.WriteAllBytesAsync(path, imageBytes);
            }
        }

        private static string SanitizeFileName(string name) =>
            Path.GetInvalidFileNameChars().Aggregate(name, (current, c) => current.Replace(c, '_'));
    }
}