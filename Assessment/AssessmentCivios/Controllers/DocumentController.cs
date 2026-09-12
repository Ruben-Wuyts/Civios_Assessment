using Assessment.Core.Interfaces;
using Assessment.Core.Results;
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
        public async Task<ActionResult<DataClassificationResult>> AnalyzeDocument(IFormFile file)
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

                if (!text.IsValid)
                {
                    return BadRequest(text.ErrorMessage);
                }

                var result = await _documentService.Classify(text.ExtractedText);

                return Ok(result);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }
        }
    }
}
