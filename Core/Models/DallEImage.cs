﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Desc2DesignAI.Core.Models
{
    public class DallEImage
    {
        [JsonPropertyName("revised_prompt")]
        public string RevisedPrompt { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}