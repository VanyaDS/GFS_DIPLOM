using AutoMapper;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Helpers;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeoFlat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonsController : ControllerBase
    {
        private int _UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly ILogger<ComparisonsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public ComparisonsController(
            ILogger<ComparisonsController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetComparisons()
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverter model))
            {
                return StatusCode(500, "Internal server error with currency service");
            }

            var comparisons = await _unitOfWork.Comparisons.FindAllAsync(comparison => comparison.UserId == _UserId && comparison.RecordId != null);
            List<ComparisonResponse> comparisonsResponse = new List<ComparisonResponse>();
            if (comparisons is not null)
            {
                foreach (var comparison in comparisons)
                {
                    comparisonsResponse.Add(_mapper.Map<ComparisonResponse>(comparison));
                }
                foreach (var record in comparisonsResponse)
                {
                    record.PriceBYN = model.ConvertFromUSDToBYN(record.Price);
                }
            }
            return Ok(comparisonsResponse);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverter model))
            {
                return StatusCode(500, "Internal server error with currency service");
            }

            var comparison = await _unitOfWork.Comparisons.GetById(id);

            if (comparison is null)
            {
                return NotFound();
            }
           
            var resultRecord = _mapper.Map<ComparisonResponse>(comparison);
            resultRecord.PriceBYN = model.ConvertFromUSDToBYN(resultRecord.Price);

            return Ok(resultRecord);
        }

        [HttpPost("{recordId}")]
        [Authorize]
        public async Task<IActionResult> CreateComparison(int recordId)
        {
            var record = await _unitOfWork.Records.GetById(recordId);
            if (record is null)
            {
                return BadRequest();
            }

            Comparison comparison = new Comparison
            {
                UserId = _UserId,
                RecordId = recordId
            };

            if (await _unitOfWork.Comparisons.Add(comparison))
            {
                await _unitOfWork.CompleteAsync();
                return Ok(_mapper.Map<ComparisonResponse>(comparison));
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComparison(int id)
        {
            var comparison = await _unitOfWork.Comparisons.GetById(id);

            if (comparison is null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Comparisons.Delete(id))
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteComparisons()
        {
            var comparisons = await _unitOfWork.Comparisons.FindAllAsync(comparison => comparison.UserId == _UserId);

            if (comparisons is not null)
            {
                foreach (var comparison in comparisons)
                {
                    if (await _unitOfWork.Comparisons.Delete(comparison.Id))
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                }
            }
            return NoContent();
        }
    }
}
