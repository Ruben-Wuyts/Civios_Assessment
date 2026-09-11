using Assessment.Core.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Interfaces
{
    public interface IFileUploadValidator
    {
        public FileValidationResult ValidateFile(string fileName, long fileSize);
    }
}
