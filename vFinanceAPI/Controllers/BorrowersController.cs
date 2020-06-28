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
    public class BorrowersController : ControllerBase
    {
        private readonly BorrowerService _borrowerService;
        private readonly LoanService _loanService;
        public BorrowersController(BorrowerService bService,LoanService lService)
        {
            _borrowerService = bService;
            _loanService = lService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrower>>> GetAll()
        {
            var borrowers = await _borrowerService.GetAllAsync();
            return Ok(borrowers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Borrower>> GetById(string id)
        {
            var borrower = await _borrowerService.GetByIdAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            if (borrower.Loans.Count > 0)
            {
                var tempList = new List<Loan>();
                foreach (var loanId in borrower.Loans)
                {
                    var course = await _loanService.GetByIdAsync(loanId);
                    if (course != null)
                        tempList.Add(course);
                }
                borrower.LoanList = tempList;
            }

            return Ok(borrower);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Borrower borrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _borrowerService.CreateAsync(borrower);
            return Ok(borrower);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Borrower updatedBorrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var borrower = await _borrowerService.GetByIdAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            await _borrowerService.UpdateAsync(id, updatedBorrower);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var borrower = await _borrowerService.GetByIdAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }
            await _borrowerService.DeleteAsync(id);
            return NoContent();
        }
    }
}