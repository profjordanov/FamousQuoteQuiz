namespace FamousQuoteQuiz.Core.Generators
{
    public interface IRandomGenerator
    {
        long GetRandomNumber(long min, long max);
    }
}
