namespace Desc2DesignAI.Business.Services.Abstract
{
    // 2. Abstraction Layers
    public interface IImageDownloader
    {
        Task DownloadAsync(string productName, IEnumerable<string> urls);
    }
}