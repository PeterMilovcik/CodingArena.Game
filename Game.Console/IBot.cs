﻿namespace Game.Console
{
    internal interface IBot
    {
        int MaxHealthPoints { get; }
        int HealthPoints { get; }
        double HealthPercentage { get; }

        int MaxShieldPoints { get; }
        int ShieldPoints { get; }
        double ShieldPercentage { get; }

        BattlefieldPosition Position { get; }
    }
}