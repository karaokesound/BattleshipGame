﻿using BattleshipGame.Data.Entities;

namespace BattleshipGame.API.Services.Controllers
{
    public interface IGameStartingService
    {
        Task<List<PlayerEntity>> GetPlayers(string playerName);

        Task<List<PlayerEntity>> ValidateAndReturnPlayers(int key, string playerName);

        Task StartNewGame(List<PlayerEntity> players);
    }
}
