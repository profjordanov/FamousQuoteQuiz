using System;
using static System.Math;

namespace FamousQuoteQuiz.Business.Extensions
{
    public static class RandomExtensions
    {
        public static long NextLong(this Random random, long min, long max)
        {
            var buf = new byte[8];
            random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);
            return Abs(longRand % (max - min)) + min;
        }
    }
}