﻿using AutoMapper;
using BattleshipGame.API.Models.Player;
using BattleshipGame.API.Services;
using BattleshipGame.API.Services.Repositories;
using BattleshipGame.Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BattleshipGame.API.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerDto> _logger;

        private readonly IPlayersRepository _playersRepository;

        private readonly IMapper _mapper;

        private readonly IMessageService _message;

        public PlayerController(ILogger<PlayerDto> logger, 
            IPlayersRepository playersRepository, 
            IMapper mapper,
            IMessageService message)
        {
            _logger = logger;
            _playersRepository = playersRepository;
            _mapper = mapper;
            _message = message;
        }

        [SwaggerOperation(Summary = "Get all players.")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var dbPlayers = await _playersRepository.GetPlayersAsync();

            List<PlayerDto> players = new List<PlayerDto>();

            return Ok(_mapper.Map<IEnumerable<PlayerDto>>(dbPlayers));
        }

        [HttpGet("{playerid}", Name = "GetPlayer")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(int playerId)
        {
            var selectedPlayer = await _playersRepository.GetPlayerAsync(playerId);

            if (selectedPlayer == null)
            {
                _logger.LogWarning($"Player with id {playerId} doesn't found");
                return NotFound(_message.PlayerNotFoundMessage());
            }

            return Ok(_mapper.Map<PlayerDto>(selectedPlayer));
        }

        [SwaggerOperation(Summary = "Create a new player.")]
        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(PlayerForCreationDto playerForCreation)
        {
            var newPlayer = _mapper.Map<PlayerEntity>(playerForCreation);

            var result = await _playersRepository.CreatePlayerAsync(newPlayer);

            if (result == false) return BadRequest(_message.UserCreatingError());

            await _playersRepository.SaveChangesAsync();

            return CreatedAtRoute("GetPlayer",
                new
                {
                    //playerId - refers to route template in GetPlayer method
                    playerId = newPlayer.Id
                },
                newPlayer
                );
        }

        [HttpPut("{playerid}")]
        public async Task<ActionResult> UpdatePlayer(int playerId, PlayerForUpdateDto playerForUpdate)
        {
            var dbPlayer = await _playersRepository.GetPlayerAsync(playerId);

            if (dbPlayer == null) return NotFound(_message.PlayerNotFoundMessage());

            _mapper.Map(playerForUpdate, dbPlayer);

            await _playersRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{playerId}")]
        public async Task<ActionResult> PartiallyUpdatePlayer(int playerId, JsonPatchDocument<PlayerForUpdateDto> patchDocument)
        {
            var selectedPlayerDb = await _playersRepository.GetPlayerAsync(playerId);

            if (selectedPlayerDb == null) return NotFound(_message.PlayerNotFoundMessage());

            var playerToPatch = _mapper.Map<PlayerForUpdateDto>(selectedPlayerDb);

            // This method applies provided changes and properties with no changes stays as their were.
            // Without this patching, properties with no changes would be null.
            patchDocument.ApplyTo(playerToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!TryValidateModel(playerToPatch)) return BadRequest();

            _mapper.Map(playerToPatch, selectedPlayerDb);

            await _playersRepository.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{playerid}")]
        public async Task<ActionResult> DeletePlayer(int playerId)
        {
            var selectedPlayer = await _playersRepository.GetPlayerAsync(playerId);

            if (selectedPlayer == null) return NotFound(_message.PlayerNotFoundMessage());

            _playersRepository.DeletePlayer(selectedPlayer);
            await _playersRepository.SaveChangesAsync();

            return Ok(_message.Delete());
        }
    }
}
