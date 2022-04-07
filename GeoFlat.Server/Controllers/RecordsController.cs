using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Helpers;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public RecordsController(
            ILogger<RecordsController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecords()
        {
            var records = await _unitOfWork.Records.All();
            List<RecordResponse> recordsResponse = new List<RecordResponse>();
            if (records is not null)
            {
                foreach (var record in records)
                {
                    recordsResponse.Add(_mapper.Map<RecordResponse>(record));
                }
            }
            return Ok(recordsResponse);
        }
        
        [HttpGet("me")]
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> GetCurrentUserRecords()
        {
            var records = await _unitOfWork.Records.FindAllAsync(rec => rec.UserId == _UserId);
            List<RecordResponse> recordsResponse = new List<RecordResponse>();
            if (records is not null)
            {
                foreach (var record in records)
                {
                    recordsResponse.Add(_mapper.Map<RecordResponse>(record));
                }
            }
            return Ok(recordsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var record = await _unitOfWork.Records.GetById(id);

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

        [HttpPut("{id}")]// TODO implement update of CURRENT user
        [Authorize(Roles = RoleHealper.CLIENT)]
        public async Task<IActionResult> UpdateRecord(int recordId, RecordRequest recordRequest)
        {
            if (recordRequest is null)
            {
                return BadRequest();
            }

            var anyRecord = await _unitOfWork.Records.GetById(recordId);

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

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleHealper.ADMIN + "," + RoleHealper.MODER)]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _unitOfWork.Records.GetById(id);

            if (record is null)
            {
                return NotFound();
            }
          
            var favoritesOfUser = await _unitOfWork.Favorites.FindAllAsync(fav => fav.RecordId == id);
            var comparisonsOfUser = await _unitOfWork.Comparisons.FindAllAsync(comp => comp.RecordId == id);

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

            if (await _unitOfWork.Geolocations.Delete(id) /* to delete cascade*/ )
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
