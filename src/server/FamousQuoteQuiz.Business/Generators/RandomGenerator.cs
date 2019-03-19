using System;
using FamousQuoteQuiz.Business.Extensions;
using FamousQuoteQuiz.Core.Generators;

namespace FamousQuoteQuiz.Business.Generators
{
    public class RandomGenerator : IRandomGenerator
    {
        private static readonly Random GetRandom = new Random();

        public long GetRandomNumber(long min, long max) => GetRandom.NextLong(min, max);
    }
}
