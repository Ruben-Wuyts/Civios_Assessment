using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Results
{
    public class FileValidationResult
    {
        public bool IsValid { get; init; }
        public string? ErrorMessage { get; init; }

        public static FileValidationResult Success()
            => new() { IsValid = true };

        public static FileValidationResult Fail(string message)
            => new()
            {
                IsValid = false,
                ErrorMessage = message
            };
    }
}
