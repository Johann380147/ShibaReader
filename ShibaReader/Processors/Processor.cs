using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Processors
{
    class Processor
    {

        protected string ExtractParameterValue(string parameter, string ignoreText)
        {
            return parameter.Replace(ignoreText + ":", "")
                            .Replace("\"", "")
                            .Trim();
        }
    }
}
