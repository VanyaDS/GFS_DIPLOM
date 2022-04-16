using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
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
    public class RecordsController : ControllerBase
    {
        private int _UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly ILogger<RecordsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public RecordsController(
            ILogger<RecordsController> logger,
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
        public async Task<IActionResult> GetRecords()
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverter model))
            {
                return BadRequest();
            }

            var records = await _unitOfWork.Records.All();
            List<RecordResponse> recordsResponse = new List<RecordResponse>();
            
            if (records is not null)
            {
                foreach (var record in records)
                {
                    recordsResponse.Add(_mapper.Map<RecordResponse>(record));                   
                }

                foreach (var record in recordsResponse)
                {
                    record.PriceBYN = model.ConvertFromUSDToBYN(record.Price);
                }
            }
            return Ok(recordsResponse);
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> GetCurrentUserRecords()
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverter model))
            {
                return BadRequest();
            }

            var records = await _unitOfWork.Records.FindAllAsync(rec => rec.UserId == _UserId);
            List<RecordResponse> recordsResponse = new List<RecordResponse>();
            if (records is not null)
            {
                foreach (var record in records)
                {
                    recordsResponse.Add(_mapper.Map<RecordResponse>(record));
                }
                foreach (var record in recordsResponse)
                {
                    record.PriceBYN = model.ConvertFromUSDToBYN(record.Price);
                }
            }
            return Ok(recordsResponse);
        }

        [HttpGet("{recordId}")]
        public async Task<IActionResult> GetRecord(int recordId)
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverter model))
            {
                return BadRequest();
            }
            var record = await _unitOfWork.Records.GetById(recordId);

            if (record is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordResponse>(record));
        }

        [HttpPost]
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> CreateRecord(RecordRequest recordRequest)
        {
            if (recordRequest is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var geolocation = _mapper.Map<Geolocation>(recordRequest);
                var flat = _mapper.Map<Flat>(recordRequest);
                var record = _mapper.Map<Record>(recordRequest);

                record.PublicationDate = System.DateTime.Now;
                record.UserId = _UserId;
                flat.Geolocation = geolocation;
                record.Flat = flat;

                if (await _unitOfWork.Records.Add(record))
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok(_mapper.Map<RecordResponse>(record));
                }
            }
            return BadRequest();
        }

        [HttpPut("{recordId}")]
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> UpdateRecord(int recordId, RecordRequest recordRequest)
        {
            if (recordRequest is null)
            {
                return BadRequest();
            }

            var anyRecord = await _unitOfWork.Records.GetById(recordId);

            if (anyRecord.UserId != _UserId)
            {
                return BadRequest();
            }

            if (anyRecord is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var geolocation = _mapper.Map<Geolocation>(recordRequest);
                var flat = _mapper.Map<Flat>(recordRequest);
                var record = _mapper.Map<Record>(recordRequest);
                flat.Geolocation = geolocation;
                record.Flat = flat;
                record.Id = recordId;

                if (await _unitOfWork.Records.Update(record))
                {
                    await _unitOfWork.CompleteAsync();
                    return NoContent();
                }

            }
            return BadRequest();
        }
     
        [HttpPut("renewstatus/{recordId}")]
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> UpdateRecord(int recordId)
        {

            var anyRecord = await _unitOfWork.Records.GetById(recordId);

            if (anyRecord.UserId != _UserId)
            {
                return BadRequest();
            }

            if (anyRecord is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
            anyRecord.RentStatus = !anyRecord.RentStatus;

                if (await _unitOfWork.Records.Update(anyRecord))
                {
                    await _unitOfWork.CompleteAsync();
                    return NoContent();
                }

            }
            return BadRequest();
        }
        [HttpDelete("{recordId}")]
        [Authorize(Roles = RoleHealper.ADMIN + "," + RoleHealper.MODER)]
        public async Task<IActionResult> DeleteRecord(int recordId)
        {
            var record = await _unitOfWork.Records.GetById(recordId);

            if (record is null)
            {
                return NotFound();
            }

            var favoritesOfUser = await _unitOfWork.Favorites.FindAllAsync(fav => fav.RecordId == recordId);
            var comparisonsOfUser = await _unitOfWork.Comparisons.FindAllAsync(comp => comp.RecordId == recordId);

            if (favoritesOfUser.Any())
            {
                _unitOfWork.Favorites.DeleteAll(favoritesOfUser);
                await _unitOfWork.CompleteAsync();
            }
            if (comparisonsOfUser.Any())
            {
                _unitOfWork.Comparisons.DeleteAll(comparisonsOfUser);
                await _unitOfWork.CompleteAsync();
            }

            if (await _unitOfWork.Geolocations.Delete(recordId) /* to delete cascade*/ )
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
