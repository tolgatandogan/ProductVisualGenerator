namespace Desc2DesignAI.Business.Services.Abstract
{
    public interface IAiImageService
    {
        Task<IEnumerable<string>> GenerateImagesAsync(string prompt);
    }
}