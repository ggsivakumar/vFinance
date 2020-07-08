using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using vFinanceAPI.Services;

namespace vFinanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        
        public DocumentController(DocumentService dService)
        {
            _documentService = dService;         
        }

        [HttpPost]
        public async Task<IActionResult> Create(vFinanceAPI.Model.Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _documentService.CreateAsync(document);
            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            await _documentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            
            return Ok(document);
        }
    }
}
