﻿using BattleshipGame.API.Services.Repositories;
using BattleshipGame.Data.Entities;
using BattleshipGame.Logic.Logic;
using BattleshipGame.Logic.Services;

namespace BattleshipGame.API.Services.Controllers
{
    public class GameStartingService : IGameStartingService
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IGameRepository _gameRepository;
        private readonly iFieldRepository _fieldRepository;
        private readonly IValidationService _validation;
        private readonly IGeneratingService _generatingService;

        public GameStartingService(IPlayersRepository playersRepository,
            IGameRepository gameRepository,
            iFieldRepository fieldRepository,
            IValidationService validation,
            IGeneratingService generatingService)
        {
            _playersRepository = playersRepository;
            _gameRepository = gameRepository;
            _fieldRepository = fieldRepository;
            _validation = validation;
            _generatingService = generatingService;
        }

        public async Task<List<PlayerEntity>> GetPlayers(string playerName)
        {
            List<PlayerEntity> players = new List<PlayerEntity>();

            var playersIds = await _gameRepository.GetPlayersIds();
            var player1 = await _playersRepository.GetPlayerAsync(playersIds[0]);
            var player2 = await _playersRepository.GetPlayerAsync(playersIds[1]);

            players.Add(player1);
            players.Add(player2);

            return players;
        }

        public async Task<List<PlayerEntity>> ValidateAndGetPlayers(int key, string playerName)
        {
            // Player2 (opponent) is being taken randomly from the database

            List<PlayerEntity> players = new List<PlayerEntity>();

            PlayerEntity player1 = await _playersRepository.GetPlayerByNameAsync(playerName);
            PlayerEntity player2 = await _playersRepository.GetRandomPlayerAsync(playerName);

            if (key != 1 || player1 == null || player2 == null) return players; // Empty list

            players.Add(player1);
            players.Add(player2);

            return players;
        }

        public async Task StartNewGame(List<PlayerEntity> players)
        {
            // Deleting previous game data

            _fieldRepository.DeleteAllFields();
            await _fieldRepository.SaveChangesAsync();
            _gameRepository.DeleteAllGames();
            await _gameRepository.SaveChangesAsync();

            // Setting flag that Player1[our] takes first shoot

            players[0].CanShoot = true;
            players[1].CanShoot = false;

            await _playersRepository.SaveChangesAsync();
            await _gameRepository.AddNewGameAsync(players[0], players[1]);
            await _gameRepository.SaveChangesAsync();

            // Creating game boards and adding them to the database

            var board1 = new GameCore(10, 10, 12, players[0].Name, _validation, _generatingService);
            var board2 = new GameCore(10, 10, 12, players[1].Name, _validation, _generatingService);

            foreach (var field in board1.Fields)
            {
                await _fieldRepository.AddFieldAsync(field, players[0]);
            }

            foreach (var field in board2.Fields)
            {
                await _fieldRepository.AddFieldAsync(field, players[1]);
            }

            await _fieldRepository.SaveChangesAsync();
        }
    }
}
