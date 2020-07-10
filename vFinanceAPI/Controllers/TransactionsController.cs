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
    public class TransactionsController : ControllerBase
    {

        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService service)
        {
            _transactionService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }


        [HttpGet("{fromDate}/{toDate}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllByDate(DateTime fromDate, DateTime toDate)
        {
            var transactions = await _transactionService.GetByDateAsync(fromDate, toDate);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetById(string id)
        {
            var collection = await _transactionService.GetByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _transactionService.CreateAsync(transaction);
            return Ok(transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbTransaction = await _transactionService.GetByIdAsync(id);
            if (dbTransaction == null)
            {
                return NotFound();
            }

            await _transactionService.UpdateAsync(id, transaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            await _transactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
