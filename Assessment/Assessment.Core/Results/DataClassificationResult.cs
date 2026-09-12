using Assessment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Results
{
    public class DataClassificationResult
    {
        public DataClassification Classification { get; init; }
        public string Reason { get; init; }

        public DataClassificationResult(DataClassification classification, string reason) 
        {
            Classification = classification;
            Reason = reason;
        }

    }
}
