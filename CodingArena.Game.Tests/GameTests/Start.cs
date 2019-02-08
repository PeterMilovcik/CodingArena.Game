﻿using FluentAssertions;
using NUnit.Framework;

namespace CodingArena.Game.Tests.GameTests
{
    internal class Start : TestFixture
    {
        [Test]
        public void Match_NotNull()
        {
            Game.Start();
            Game.Match.Should().NotBeNull();
        }

        [Test]
        public void MatchStarting_IsRaised()
        {
            bool isMatchStartingRaised = false;
            Game.MatchStarting += (sender, args) => isMatchStartingRaised = true;
            Game.Start();
            isMatchStartingRaised.Should().BeTrue();
        }

        [Test]
        public void MatchFinished_IsRaised()
        {
            bool isMatchFinishedRaised = false;
            Game.MatchFinished += (sender, args) => isMatchFinishedRaised = true;
            Game.Start();
            isMatchFinishedRaised.Should().BeTrue();
        }
    }
}