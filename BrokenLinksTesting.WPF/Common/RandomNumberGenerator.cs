using System;

namespace BrokenLinksTesting.WPF.Common
{
    public static class RandomNumberGenerator
    {
        readonly static Random _random = new Random();
        readonly static int _min = 1;
        readonly static int _max = 10000000;

        // Generates a random number within a range.      
        public static int GenerateRandomNumber()
        {
            return _random.Next(_min, _max);
        }
    }
}
