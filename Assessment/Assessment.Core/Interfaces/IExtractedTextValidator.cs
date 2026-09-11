using Assessment.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Interfaces
{
    public interface IExtractedTextValidator
    {
        public FileValidationResult IsTextValid(string text);
    }
}
