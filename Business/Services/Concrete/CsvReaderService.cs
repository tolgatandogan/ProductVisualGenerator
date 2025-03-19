using CsvHelper;
using CsvHelper.Configuration;
using Desc2DesignAI.Business.Services.Abstract;
using Desc2DesignAI.Core.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class CsvReaderService : ICsvReader
{
    public IEnumerable<Product> ReadProducts(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<Product>().ToList();
    }
}