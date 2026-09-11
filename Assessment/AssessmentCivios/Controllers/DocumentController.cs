using Assessment.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
                
                var validationResult = _uploadValidator.ValidateFile(file.FileName, file.Length);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

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
