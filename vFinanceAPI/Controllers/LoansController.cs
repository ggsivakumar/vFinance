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

            var collections = await _collectionService.GetByLoanIdAsync(id);
            if(collections.Any())
            {
                loan.LoanCollections = new List<LoanCollection>();
                foreach(var collection in collections)
                {
                    loan.LoanCollections.Add(collection);
                }

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

            var newLoan = await _loanService.CreateAsync(loan);
            if(newLoan != null)
            {
                if(loan.LoanCollections.Any())
                {
                    foreach(var collection in loan.LoanCollections)
                    {
                        collection.LoanId = newLoan.Id;
                        await _collectionService.CreateAsync(collection);
                    }
                }
            }
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

            var collections = await _collectionService.GetByLoanIdAsync(loan.Id);
            if(collections.Any())
            {
                await _collectionService.DeleteAllAsync(loan.Id);
            }

            await _loanService.DeleteAsync(id);
            return NoContent();
        }
    }
}