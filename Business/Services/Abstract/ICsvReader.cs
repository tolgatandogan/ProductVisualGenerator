using Desc2DesignAI.Core.Models;
using ProductVisualGenerator;

namespace Desc2DesignAI.Business.Services.Abstract
{
    public interface ICsvReader
    {
        IEnumerable<Product> ReadProducts(string filePath);
    }
}