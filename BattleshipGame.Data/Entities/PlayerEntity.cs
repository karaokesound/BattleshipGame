﻿namespace BattleshipGame.Data.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }

        public PlayerEntity(string name, string city)
        {
            Name = name;
            City = city;
        }
    }
}