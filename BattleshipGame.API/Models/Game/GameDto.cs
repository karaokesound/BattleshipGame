﻿namespace BattleshipGame.API.Models.Game
{
    public class GameDto
    {
        public string Player1 { get; set; } = null!;

        public string Player2 { get; set; } = null!;

        public List<Field>? Player1Board { get; set; }

        public List<Field>? Player2Board { get; set; }

        public GameDto()
        {
            Player1Board = new List<Field>();
            Player2Board = new List<Field>();
        }
    }
}
