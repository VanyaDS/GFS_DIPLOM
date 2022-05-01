using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Automapper.ResponseModels;
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
    public class MessagesController : ControllerBase
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
        [Authorize]
        public async Task<IActionResult> GetUsersWithLastMessages() // on the top of result list there are the latest messages
        {
            var messagesSendedByMe = await _unitOfWork.Messages.FindAllAsync(mess => mess.Sender == _UserId && mess.Recipient != null);
            var messagesRecivedByMe = await _unitOfWork.Messages.FindAllAsync(mess => mess.Recipient == _UserId && mess.Sender != null);

            List<int?> idsOfUsersThatHasContactWithMe = new List<int?>();

            if (messagesSendedByMe.Any())
            {
                foreach (var item in messagesSendedByMe)
                {
                    idsOfUsersThatHasContactWithMe.Add(item.Recipient);
                }
            }

            if (messagesRecivedByMe.Any())
            {
                foreach (var item in messagesRecivedByMe)
                {
                    idsOfUsersThatHasContactWithMe.Add(item.Sender);
                }
            }
            if (idsOfUsersThatHasContactWithMe.Any())
            {
                idsOfUsersThatHasContactWithMe = idsOfUsersThatHasContactWithMe.Distinct().ToList();
            }

            List<Message> resultMessageList = new List<Message>();

            foreach (var idUser in idsOfUsersThatHasContactWithMe)
            {
                var allMessageswithUser = await _unitOfWork.Messages
                .FindAllAsync(mess => mess.Sender == _UserId && mess.Recipient == idUser
                              || mess.Sender == idUser && mess.Recipient == _UserId);

                var latestMessageWithUser = allMessageswithUser.OrderByDescending(mes => mes.SendingDate).First();
                resultMessageList.Add(latestMessageWithUser);
            }

            resultMessageList = resultMessageList.OrderByDescending(mes => mes.SendingDate).ToList();

            List<MessageResponse> messagesResponse = new List<MessageResponse>();
            if (resultMessageList.Any())
            {
                foreach (var message in resultMessageList)
                {
                    var resultMessage = _mapper.Map<MessageResponse>(message);
                    if(resultMessage.SenderId == _UserId)
                    {
                        resultMessage.IsRead = true;
                    }
                    messagesResponse.Add(resultMessage);
                }
            }
            return Ok(messagesResponse);
        }

        [Authorize]
        [HttpGet("{userId}")] // on the top of result list there are the earliest messages
        public async Task<IActionResult> GetMessages(int userId)
        {
            var messagesWithUser = await _unitOfWork.Messages.FindAllAsync(mess => mess.Sender == _UserId && mess.Recipient == userId
                              || mess.Sender == userId && mess.Recipient == _UserId);

            List<MessageResponse> messagesResponse = new List<MessageResponse>();
            if (messagesWithUser.Any())
            {
                messagesWithUser.OrderBy(date => date.SendingDate).ToList();
                foreach (var message in messagesWithUser)
                {
                    if (message.Sender == userId)
                    {
                        message.IsRead = true;
                    }
                    if (await _unitOfWork.Messages.Update(message))
                    {
                        await _unitOfWork.CompleteAsync();
                        messagesResponse.Add(_mapper.Map<MessageResponse>(message));
                    }
                }
            }
            return Ok(messagesResponse);
        }
        
        [Authorize]
        [HttpGet("unread")] 
        public async Task<IActionResult> GetInfoAboutUnreadMessages()
        {
            var unreadMessagesToCurrentUser = await _unitOfWork.Messages.FindAllAsync(mess => mess.Recipient == _UserId && !mess.IsRead);
      
            var infoMessagesResponse = new InfoMessagesResponse();
          
            if (unreadMessagesToCurrentUser.Any())
            {
                int messageNumber = unreadMessagesToCurrentUser.GroupBy(g => g.Sender).Count();

                infoMessagesResponse.NumberOfUnreadMessagesWithUsers = messageNumber;
                infoMessagesResponse.HasUnreadMessages = true;              
            }
            else
            {
                infoMessagesResponse.NumberOfUnreadMessagesWithUsers = 0;
                infoMessagesResponse.HasUnreadMessages = false;
            }
            return Ok(infoMessagesResponse);
        }

        [HttpPost("{userId}")]
        [Authorize]
        public async Task<IActionResult> CreateMessage(int userId, [FromBody] MessageRequest messageRequest)
        {
            if (messageRequest is null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(messageRequest.Message))
            {
                return BadRequest();
            }

            var user = await _unitOfWork.Users.GetById(userId);

            if (user is null)
            {
                return BadRequest();
            }

            Message newMessage = new Message
            {
                SendingDate = System.DateTime.Now,
                MessageText = messageRequest.Message,
                Sender = _UserId,
                Recipient = userId,
                IsRead = false
            };

            if (await _unitOfWork.Messages.Add(newMessage))
            {
                await _unitOfWork.CompleteAsync();
                
                var sender = await _unitOfWork.Users.GetById(_UserId);
                var result = _mapper.Map<MessageResponse>(newMessage);

                if (sender is not null)
                {
                    result.SenderName = sender.Name;
                    result.SenderSurname = sender.Surname;
                    result.SenderPhoneNumber = sender.PhoneNumber;
                    result.SenderEmail = sender.Account.Email;
                }
               
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _unitOfWork.Messages.GetById(id);

            if (message is null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Messages.Delete(id))
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
