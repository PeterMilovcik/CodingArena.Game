﻿namespace CodingArena.Player.TurnActions
{
    public sealed class RechargeShield : ITurnAction
    {
        internal RechargeShield()
        {
        }

        public int EnergyCost => 10;

        public int RechargeAmount => 20;
    }
}