﻿using AutoMapper;
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
    public class FavoritesController : ControllerBase //[AUTHORIZATION]
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
        public async Task<IActionResult> GetFavorites()
        {
            var Favorites = await _unitOfWork.Favorites.FindAllAsync(favorite => favorite.UserId == _UserId);
            List<FavoriteResponse> FavoritesResponse = new List<FavoriteResponse>();
            if (Favorites is not null)
            {
                foreach (var favorite in Favorites)
                {
                    FavoritesResponse.Add(_mapper.Map<FavoriteResponse>(favorite));
                }
            }
            return Ok(FavoritesResponse);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetRecord(int id)
        //{
        //    var record = await _unitOfWork.Favorites.GetById(id);

        //    if (record is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<RecordResponse>(record));
        //}

        //[HttpPost]
        //[Authorize(Roles = RoleHealper.CLIENT)]
        //public async Task<IActionResult> CreateRecord(RecordRequest recordRequest)
        //{
        //    if (recordRequest == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var geolocation = _mapper.Map<Geolocation>(recordRequest);
        //        var flat = _mapper.Map<Flat>(recordRequest);
        //        var record = _mapper.Map<Record>(recordRequest);

        //        record.PublicationDate = System.DateTime.Now;
        //        record.UserId = _UserId;
        //        flat.Geolocation = geolocation;
        //        record.Flat = flat;

        //        if (await _unitOfWork.Favorites.Add(record))
        //        {
        //            await _unitOfWork.CompleteAsync();
        //            return Ok(_mapper.Map<RecordResponse>(record));
        //        }
        //    }
        //    return BadRequest();
        //}

        //[HttpPut("{id}")]// TODO implement update of CURRENT user
        //[Authorize(Roles = RoleHealper.CLIENT)]
        //public async Task<IActionResult> UpdateRecord(int id, RecordRequest recordRequest)
        //{
        //    if (recordRequest is null)
        //    {
        //        return BadRequest();
        //    }

        //    var anyRecord = await _unitOfWork.Favorites.GetById(id);

        //    if (anyRecord is null)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        var geolocation = _mapper.Map<Geolocation>(recordRequest);
        //        var flat = _mapper.Map<Flat>(recordRequest);
        //        var record = _mapper.Map<Record>(recordRequest);
        //        flat.Geolocation = geolocation;
        //        record.Flat = flat;
        //        record.Id = id;

        //        if (await _unitOfWork.Favorites.Update(record))
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
        //    var record = await _unitOfWork.Favorites.GetById(id);

        //    if (record == null)
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