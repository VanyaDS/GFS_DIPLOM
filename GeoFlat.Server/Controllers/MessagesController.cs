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
    public class MessagesController : ControllerBase////////////////////////////////AUTHORIZATIONS
    {
        private int _UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly ILogger<MessagesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessagesController(
            ILogger<MessagesController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _unitOfWork.Messages.FindAllAsync(mess => mess.Sender == _UserId);
            List<MessageResponse> messagesResponse = new List<MessageResponse>();
            if (messages is not null)
            {
                foreach (var message in messages)
                {
                   messagesResponse.Add(_mapper.Map<MessageResponse>(message));
                }
            }
            return Ok(messagesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _unitOfWork.Messages.GetById(id);

            if (message is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MessageResponse>(message));
        }

        //[HttpPost]
        //[Authorize(Roles = RoleHealper.CLIENT)]
        //public async Task<IActionResult> CreateRecord(RecordRequest messageRequest)
        //{
        //    if (messageRequest is null)
        //    {
        //        return BadRequest();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var geolocation = _mapper.Map<Geolocation>(messageRequest);
        //        var flat = _mapper.Map<Flat>(messageRequest);
        //        var message = _mapper.Map<Record>(messageRequest);

        //        message.PublicationDate = System.DateTime.Now;
        //        message.UserId = _UserId;
        //        flat.Geolocation = geolocation;
        //        message.Flat = flat;

        //        if (await _unitOfWork.Records.Add(message))
        //        {
        //            await _unitOfWork.CompleteAsync();
        //            return Ok(_mapper.Map<RecordResponse>(message));
        //        }
        //    }
        //    return BadRequest();
        //}

        //[HttpPut("{id}")]// TODO implement update of CURRENT user
        //[Authorize(Roles = RoleHealper.CLIENT)]
        //public async Task<IActionResult> UpdateRecord(int id, RecordRequest messageRequest)
        //{
        //    if (messageRequest is null)
        //    {
        //        return BadRequest();
        //    }

        //    var anyRecord = await _unitOfWork.Records.GetById(id);

        //    if (anyRecord is null)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        var geolocation = _mapper.Map<Geolocation>(messageRequest);
        //        var flat = _mapper.Map<Flat>(messageRequest);
        //        var message = _mapper.Map<Record>(messageRequest);
        //        flat.Geolocation = geolocation;
        //        message.Flat = flat;
        //        message.Id = id;

        //        if (await _unitOfWork.Records.Update(message))
        //        {
        //            await _unitOfWork.CompleteAsync();
        //            return NoContent();
        //        }

        //    }
        //    return BadRequest();
        //}

        //[HttpDelete("{id}")]
        //[Authorize(Roles = RoleHealper.ADMIN + "," + RoleHealper.MODER)]
        //public async Task<IActionResult> DeleteRecord(int id)
        //{
        //    var message = await _unitOfWork.Records.GetById(id);

        //    if (message is null)
        //    {
        //        return NotFound();
        //    }

        //    if (await _unitOfWork.Geolocations.Delete(id) /* to delete cascade*/ )
        //    {
        //        await _unitOfWork.CompleteAsync();
        //        return NoContent();
        //    }
        //    return BadRequest();
        //}
    }
}
