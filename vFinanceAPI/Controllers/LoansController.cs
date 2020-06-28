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
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;
        private readonly CollectionService _collectionService;

        public LoansController(LoanService lService,CollectionService cService)
        {
            _loanService = lService;
            _collectionService = cService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetById(string id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            if (loan.LoanCollections.Count > 0)
            {
                var tempList = new List<LoanCollection>();
                foreach (var collectionId in loan.LoanCollections)
                {
                    var collection = await _collectionService.GetByIdAsync(collectionId);
                    if (collection != null)
                        tempList.Add(collection);
                }
                loan.LoanCollectionList = tempList;
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _loanService.CreateAsync(loan);
            return Ok(loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Loan updatedLoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            await _loanService.UpdateAsync(id, updatedLoan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            await _loanService.DeleteAsync(id);
            return NoContent();
        }
    }
}