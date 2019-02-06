﻿using System.ComponentModel.Composition;
using CodingArena.Game.Factories;

namespace CodingArena.Game.Internal
{
    [Export(typeof(IGame))]
    internal class Game : IGame
    {
        private IMatchFactory MatchFactory { get; }
        private IOutput Output { get; }

        [ImportingConstructor]
        public Game(IMatchFactory matchFactory, IOutput output)
        {
            MatchFactory = matchFactory;
            Output = output;
        }

        public void Start()
        {
            Output.DisplayGameTitle();
            var match = MatchFactory.Create();
            match.Start();
        }
    }
}