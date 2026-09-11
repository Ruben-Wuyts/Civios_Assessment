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
        private readonly IFileUploadValidator _uploadValidator;
        public DocumentController(IDocumentService documentService, IFileUploadValidator uploadValidator) 
        { 
            _documentService = documentService;
            _uploadValidator = uploadValidator;
        }

        [HttpPost("analyze")]
        public async Task<ActionResult<string>> AnalyzeDocument(IFormFile file)
        {
            try
            {
                _uploadValidator.FileNotEmpty(file.Length);
                _uploadValidator.FileExtensionAllowed(file.Name);

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
