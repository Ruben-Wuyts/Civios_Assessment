using Assessment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentStorage
    {
        Task<string> SaveTemporaryAsync(Stream stream, string fileName);
        Task<string> MoveToFinalStorageAsync(string currentPath, DataClassification classification);
    }
}
