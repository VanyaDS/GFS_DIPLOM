using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GeoFlat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly ILogger<TestingController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TestingController(
            ILogger<TestingController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accounts = await _unitOfWork.Accounts.All();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _unitOfWork.Accounts.GetById(id);

            if (item == null)
                return NotFound();

      
            return Ok(item);
        }

        public class testModel
        {
            public string Email { get; set; }
            public string Password { get; set; }         
            public int RoleId { get; set; }
        }
      
        [HttpPost]
        public async Task<IActionResult> CreateAccount(testModel testModel)
        {
            if (testModel == null)
            {
                return BadRequest();
            }

            Account account = new Account
            {
                Email = testModel.Email,
                Password = testModel.Password,
                RoleId = testModel.RoleId
            };

            if (ModelState.IsValid)
            {
                await _unitOfWork.Accounts.Add(account);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { account.Id }, account);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Account account)
        {
            if (id != account.Id)
                return BadRequest();

            await _unitOfWork.Accounts.Update(account);
            await _unitOfWork.CompleteAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _unitOfWork.Accounts.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Accounts.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
