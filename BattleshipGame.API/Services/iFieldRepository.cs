﻿using BattleshipGame.Data.Entities;

namespace BattleshipGame.API.Services
{
    public interface iFieldRepository
    {
        Task<List<FieldEntity>> GetPlayerFieldsAsync(string player);

        Task<List<string>> GetCurrentPlayersByFieldsAsync();

        Task<bool> AddFieldAsync(int x, int y, int shipSize, bool isEmpty, bool isHitted, bool isValid, string player);

        void DeleteAllFields();

        Task<bool> SaveChangesAsync();
    }
}
