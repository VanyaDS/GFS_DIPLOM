using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoFlat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly ILogger<RecordsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private int +UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        private int _UserId = 1;

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
        public async Task<IActionResult> CreateRecord(RecordRequest recordRequest)
        {
            if (recordRequest == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var geolocation = _mapper.Map<Geolocation>(recordRequest);
                var flat = _mapper.Map<Flat>(recordRequest);
                var record = _mapper.Map<Record>(recordRequest);

                record.PublicationDate = System.DateTime.Now;
                record.UserId = _UserId;// change to current user id
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, RecordRequest recordRequest)
        {
            if (recordRequest is null)
            {
                return BadRequest();
            }

            var anyRecord = await _unitOfWork.Records.GetById(id);

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
                record.Id = id;

                if (await _unitOfWork.Records.Update(record))
                {
                    await _unitOfWork.CompleteAsync();
                    return NoContent();
                }

            }
            return BadRequest();
        }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRecord(int id)
            {
                var record = await _unitOfWork.Records.GetById(id);

                if (record == null)
                {
                    return NotFound();
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
