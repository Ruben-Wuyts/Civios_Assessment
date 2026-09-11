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

        [HttpPost("analyze")]
        public async Task<ActionResult<string>> AnalyzeDocument(IFormFile file)
        {
            try
            {
                using var stream = file.OpenReadStream();
                var text = await _documentService.ExtractText(stream);
                return Ok(text);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }
        }
    }
}
