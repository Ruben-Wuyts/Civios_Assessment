using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;
using Assessment.Presentation.Requests;
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<DocumentAnalysisResult>> AnalyzeDocument([FromForm] AnalyzeDocumentRequest request)
        {
            try
            {

                var metadata = new DocumentMetadata(request.Author, request.Title, request.CreationDate, request.Description, request.Visibility);
                var validationResult = _uploadValidator.ValidateFile(request.File.FileName, request.File.Length);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

                using var stream = request.File.OpenReadStream();
                var text = await _documentService.ExtractText(stream);

                if (!text.IsValid)
                {
                    return BadRequest(text.ErrorMessage);
                }

                var result = await _documentService.Classify(text.ExtractedText, metadata);

                var analysisResult = await _documentService.SaveAnalyzedDocumentAsync(stream, request.File.FileName, metadata, result);

                return Ok(analysisResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id}/store")]
        public async Task<ActionResult<DocumentStoreResult>> StoreDocument(int id)
        {
            var result = await _documentService.StoreDocumentAsync(id);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpGet("{id}/audit")]
        public async Task<ActionResult<List<AuditLog>>> GetAllAuditLogsForDocument(int id)
        {
            return await _documentService.GetAllAuditLogsByDocumentIdAsync(id);
        }
          
    }
}
