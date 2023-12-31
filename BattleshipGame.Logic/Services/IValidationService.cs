﻿using BattleshipGame.API.Models.Game;
using BattleshipGame.Data.Entities;

namespace BattleshipGame.Logic.Services
{
    public interface IValidationService
    {
        bool OneFieldShipValidation(int startX, int startY, int endX, int endY, List<Field> allFields, string username, int shipId);

        bool TwoFieldShipValidation(int startX, int startY, int endX, int endY, List<Field> allFields, string username, int shipId);

        bool ThreeFieldShipValidation(int startX, int startY, int endX, int endY, List<Field> allFields, string username, int shipId);

        bool FourFieldShipValidation(int startX, int startY, int endX, int endY, List<Field> allFields, string username, int shipId);

        List<int> ValidateCoordsFormatAndReturnId(string coordinates);

        List<string> CheckIfFieldsWereHit(List<FieldEntity> fields);
    }
}
