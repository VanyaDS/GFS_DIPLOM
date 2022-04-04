using AutoMapper;
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
    public class FavoritesController : ControllerBase
    {
        private int _UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private readonly ILogger<FavoritesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FavoritesController(
            ILogger<FavoritesController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _unitOfWork.Favorites.FindAllAsync(favorite => favorite.UserId == _UserId);
            List<FavoriteResponse> favoritesResponse = new List<FavoriteResponse>();
            if (favorites is not null)
            {
                foreach (var favorite in favorites)
                {
                    favoritesResponse.Add(_mapper.Map<FavoriteResponse>(favorite));
                }
            }
            return Ok(favoritesResponse);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var favorite = await _unitOfWork.Favorites.GetById(id);

            if (favorite is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FavoriteResponse>(favorite));
        }

        [HttpPost("{recordId}")]
        [Authorize]
        public async Task<IActionResult> CreateFavorite(int recordId)
        {
            var record = await _unitOfWork.Records.GetById(recordId);
            if (record is null)
            {
                return BadRequest();
            }

            Favorite favorite = new Favorite
            {
                UserId = _UserId,
                RecordId = recordId
            };

            if (await _unitOfWork.Favorites.Add(favorite))
            {
                await _unitOfWork.CompleteAsync();
                return Ok(_mapper.Map<FavoriteResponse>(favorite));
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _unitOfWork.Favorites.GetById(id);

            if (favorite is null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Favorites.Delete(id))
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteFavorites()
        {
            var favorites = await _unitOfWork.Favorites.FindAllAsync(favorite => favorite.UserId == _UserId);

            if (favorites is not null)
            {
                foreach (var favorite in favorites)
                {
                    if (await _unitOfWork.Favorites.Delete(favorite.Id))
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                }
            }
            return NoContent();
        }
    }
}
