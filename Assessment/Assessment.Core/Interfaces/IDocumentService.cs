using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentService
    {
        public Task<String> ExtractText(Stream stream);
    }
}
