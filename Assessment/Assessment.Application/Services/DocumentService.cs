using Assessment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly IDocumentTextExtractor _textExtractor;
        public DocumentService(IDocumentTextExtractor textExtractor) 
        { 
            _textExtractor = textExtractor;
        }

        public Task<String> ExtractText(FileStream stream)
        {
            string text = _textExtractor.ExtractText(stream);

            return Task.FromResult(text);
        }

    }
}
