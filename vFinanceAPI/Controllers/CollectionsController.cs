using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vFinanceAPI.Model;
using vFinanceAPI.Services;

namespace vFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly CollectionService _collectionService;

        public CollectionsController(CollectionService service)
        {
            _collectionService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanCollection>>> GetAll()
        {
            var loans = await _collectionService.GetAllAsync();
            return Ok(loans);
        }


        [HttpGet("{fromDate}/{toDate}/{status}")]
        public async Task<ActionResult<IEnumerable<LoanCollection>>> GetAllByDate(DateTime fromDate, DateTime toDate, string status)
        {
            var loans = await _collectionService.GetByDateAsync(fromDate, toDate, status);
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanCollection>> GetById(string id)
        {
            var collection = await _collectionService.GetByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanCollection loanCollection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _collectionService.CreateAsync(loanCollection);
            return Ok(loanCollection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, LoanCollection updatedLoanCollection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collection = await _collectionService.GetByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            await _collectionService.UpdateAsync(id, updatedLoanCollection);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var loan = await _collectionService.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            await _collectionService.DeleteAsync(id);
            return NoContent();
        }
    }
}