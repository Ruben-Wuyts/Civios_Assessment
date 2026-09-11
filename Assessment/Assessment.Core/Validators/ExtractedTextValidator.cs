using Assessment.Core.Interfaces;
using Assessment.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Validators
{
    public class ExtractedTextValidator : IExtractedTextValidator
    {
        public FileValidationResult IsTextValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return FileValidationResult.Fail("File does not contain readable text.");
            }

            return FileValidationResult.Success();
        }
    }
}
