﻿using BattleshipGame.Data.Entities;
using static BattleshipGame.API.Services.Controllers.GamePlayService;

namespace BattleshipGame.API.Services.Controllers
{
    public interface IGamePlayService
    {
        Task<List<PlayerEntity>> GetPlayers();

        Task<string> PlayersAndCoordsNullCheck(string playerName, string coordinates);

        Task<CombinedResponseData> UpdatePlayerFields(string playerName, string coordinates);

        Task<string> FlagCheck(int value);

        Task<List<string>> SetRandomShootAndUpdateFields();

        Task<string> RefreshGameBoard();

        Task<List<string>> CoordinatesCheck(string coordinates);

        Task<List<string>> GetAllHitFields(string playerName);
    }
}