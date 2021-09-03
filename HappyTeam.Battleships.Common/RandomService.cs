using System;
using HappyTeam.Battleships.Common.Interfaces;

namespace HappyTeam.Battleships.Common
{
    public class RandomService : IRandomService
    {
        protected static Random Rnd = new Random();

        public bool GenerateBool()
        {
            return Rnd.NextDouble() >= 0.5;
        }

        public int GenerateInt(int max, int min = 0)
        {
            return Rnd.Next(min, max);
        }
    }
}
