﻿using System.Threading.Tasks;

namespace CodingArena.Player.TurnActions
{
    public class Move : ITurnAction
    {
        public Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}