using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Models.Database.Entities;
using GeoFlat.Server.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IActionResult> Get()
        {
            var records = await _unitOfWork.Records.All();
            return Ok(records);
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
                record.UserId = 1;
                flat.Geolocation = geolocation;
                record.Flat = flat;
                await _unitOfWork.Records.Add(record);
                await _unitOfWork.CompleteAsync();

                return Ok(record);
            }

            return new JsonResult("Somethign went wrong") { StatusCode = 500 };    
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _unitOfWork.Records.GetById(id);

            if (record == null)
                return BadRequest();

            await _unitOfWork.Geolocations.Delete(id); // to delete cascade
            await _unitOfWork.CompleteAsync();

            return Ok(record);
        }
    }
}
