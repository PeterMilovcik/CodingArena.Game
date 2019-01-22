﻿using CodingArena.Player;
using CodingArena.Player.Battlefield;

namespace CodingArena.Game
{
    internal class Enemy : IEnemy
    {
        public Enemy(Bot bot)
        {
            Bot = bot;
        }

        public string Name => Bot.Name;
        public IBattlefieldPlace Position => Bot.Position;
        public float Damage => Bot.Damage;
        public float Shield => Bot.Shield;
        private Bot Bot { get; }
    }
}