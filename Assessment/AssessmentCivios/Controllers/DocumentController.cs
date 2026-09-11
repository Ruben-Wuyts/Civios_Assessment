using Assessment.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService) 
        { 
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<String> GetDocumentClassification(FileStream stream)
        {
            try
            {
                return await _documentService.ExtractText(stream);
            }
            catch (Exception ex) 
            { 
                return ex.Message;
            }
        }
    }
}
