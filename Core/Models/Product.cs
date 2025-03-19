using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desc2DesignAI.Core.Models
{
    public record Product(
        string BaseCode,
        string ProductName,
        string AttributeSet,
        string ImageSize,
        string VariantableColor,
        string Category,
        string BaseCategory,
        string ChildCategory,
        string Quality,
        string Size
    );
}