﻿using AutoMapper;
using BattleshipGame.API.Models.Game;
using BattleshipGame.API.Services.Repositories;
using BattleshipGame.Logic.Services;

namespace BattleshipGame.API.Services.Controllers
{
    public class GameInfoService : IGameInfoService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayersRepository _playersRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IGeneratingService _generatingService;
        private readonly IMapper _mapper;

        public GameInfoService(IGameRepository gameRepository,
            IPlayersRepository playersRepository,
            IFieldRepository fieldRepository,
            IGeneratingService generatingService,
            IMapper mapper)
        {
            _gameRepository = gameRepository;
            _playersRepository = playersRepository;
            _fieldRepository = fieldRepository;
            _generatingService = generatingService;
            _mapper = mapper;
        }

        public async Task<bool> ValidatePlayers(string playerName)
        {
            var playersIds = await _gameRepository.GetPlayersIds();
            var player1 = await _playersRepository.GetPlayerAsync(playersIds[0]);
            var player2 = await _playersRepository.GetPlayerAsync(playersIds[1]);

            if (player1 == null || player2 == null || playerName != player1.Name && playerName != player2.Name)
            {
                return false;
            }

            return true;
        }

        public async Task<string> DisplayGameBoard(string playerName)
        {
            var playerFields = await _fieldRepository.GetPlayerFieldsAsync(playerName);

            // Mapping entities to game models

            var mappedPlayerFields = _mapper.Map<List<Field>>(playerFields);

            var gameBoard = _generatingService.DisplayGameBoard(mappedPlayerFields, 10, 10);

            return gameBoard;
        }
    }
}
