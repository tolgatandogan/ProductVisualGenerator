public interface IImageGenerator
{
    Task GenerateFromCsv(string csvPath);
}