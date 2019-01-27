﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace CodingArena.Game
{
    [Export(typeof(IMatch))]
    internal class Match : IMatch
    {
        [ImportingConstructor]
        public Match(IOutput output, GameConfiguration config, ISettings settings)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Settings = settings;
            Winners = new Dictionary<string, int>();
        }

        private Dictionary<string, int> Winners { get; }
        private IOutput Output { get; }
        private GameConfiguration Config { get; }
        private ISettings Settings { get; }

        public Task<IRound> CreateRoundAsync() => 
            Task.FromResult<IRound>(new Round(Output));

        public Task WaitForNextRoundAsync()
        {
            Output.NextRoundIn(Settings.NextRoundDelay);
            return Task.Delay(Settings.NextRoundDelay);
        }

        public void Process(RoundResult roundResult)
        {
            if (roundResult.WinnerName == null)
            {
                return;
            }
            if (Winners.ContainsKey(roundResult.WinnerName))
            {
                Winners[roundResult.WinnerName]++;
            }
            else
            {
                Winners.Add(roundResult.WinnerName, 1);
            }
            Output.MatchResult(Winners);
        }
    }
}