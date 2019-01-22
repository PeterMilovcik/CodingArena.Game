﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingArena.Player.Battlefield;

namespace CodingArena.Game
{
    internal class Round : IRound
    {
        private TextWriter Output { get; }

        public Round(TextWriter output)
        {
            Output = output;
        }

        public Task<RoundResult> StartAsync(ICollection<Bot> bots, Battlefield battlefield)
        {
            Output.WriteLine($"Starting round {DateTime.Now} ...");
            return bots.Any() == false
                ? NoBotsQualified()
                : bots.Count == 1
                    ? OnlyOneBotQualified(bots.First())
                    : MoreThanOneBotsQualified(bots, battlefield);
        }

        private Task<RoundResult> NoBotsQualified()
        {
            Output.WriteLine("No bots are qualified.");
            return Task.FromResult(new RoundResult());
        }

        private Task<RoundResult> OnlyOneBotQualified(Bot bot)
        {
            Output.WriteLine("Only one bot is qualified.");
            Output.WriteLine($"Winner is {bot.Name}.");
            return Task.FromResult(new RoundResult {Winner = bot.Name});
        }

        private Task<RoundResult> MoreThanOneBotsQualified(ICollection<Bot> bots, Battlefield battlefield)
        {
            PlaceBotsOnBattlefield(bots, battlefield);

            var roundResult = new RoundResult();
            DisplayQualifiedBots(bots);
            const int maxTurns = 100;
            var turn = new Turn(0, bots, battlefield);
            do
            {
                turn = turn.StartTurn();

            } while (bots.Count > 1 && turn.Number < maxTurns);

            if (bots.Count == 1)
            {
                var winner = bots.First();
                Output.WriteLine($"Winner is {winner.Name}.");
                return Task.FromResult(new RoundResult { Winner = winner.Name });
            }
            if (bots.Count > 1)
            {
                Output.WriteLine($"No winner after {maxTurns} turns.");
            }
            return Task.FromResult(roundResult);
        }

        private void PlaceBotsOnBattlefield(ICollection<Bot> bots, Battlefield battlefield)
        {
            var random = new Random((int) DateTime.Now.Ticks);

            foreach (var bot in bots)
            {
                var place = FindEmptyPlace(battlefield, random);
                battlefield[place.X, place.Y] = new BattlefieldPlace(place.X, place.Y, bot);
            }
        }

        private static IBattlefieldPlace FindEmptyPlace(Battlefield battlefield, Random random)
        {
            var emptyPlaces = new List<IBattlefieldPlace>();
            for (int y = 0; y < battlefield.Size.Height; y++)
            {
                for (int x = 0; x < battlefield.Size.Width; x++)
                {
                    if (battlefield[x, y].IsEmpty)
                    {
                        emptyPlaces.Add(battlefield[x, y]);
                    }
                }
            }

            if (emptyPlaces.Any())
            {
                return emptyPlaces[random.Next(emptyPlaces.Count - 1)];
            }

            throw new InvalidOperationException("Failed to find empty place on battlefield.");
        }

        private void DisplayQualifiedBots(ICollection<Bot> bots)
        {
            Output.WriteLine("Bots qualified:");
            foreach (var bot in bots)
                Output.WriteLine($" * {bot.Name}");
        }
    }
}
