using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DayFour
{
    public class SecureContainer
    {
        
        public static void Main(string[] args)
        {
            var numberOfPasswordsPart1 = GetNumberOfPasswordsMatchesInRange(193651, 649729, HasValidAdjacentDigits);
            Console.WriteLine($"Passwords which match criteria part 1: {numberOfPasswordsPart1}");

            var numberOfPasswordsPart2 = GetNumberOfPasswordsMatchesInRange(193651, 649729, HasTwoAdjacentDigits);
            Console.WriteLine($"Passwords which match criteria part 2: {numberOfPasswordsPart2}");
        }

        private static int GetNumberOfPasswordsMatchesInRange(int lower, int upper, Func<string, bool> isValid)
        {
            var possiblePasswords = new List<int>();
            for (var i = lower; i < upper; i++)
            {
                var password = i.ToString();
                if (IsSorted(password) && isValid(password))
                {
                    possiblePasswords.Add(i);
                }
            }
            return possiblePasswords.Count;
        }

        private static bool IsSorted(string password)
        {
            return Regex.IsMatch(password, @"^(0*1*2*3*4*5*6*7*8*9*)$");
        }

        private static bool HasValidAdjacentDigits(string sortedPassword)
        {
            return sortedPassword.GroupBy(digit => digit).Count(c => c.Count() > 1) > 0;
        }

        private static bool HasTwoAdjacentDigits(string sortedPassword)
        {
            return sortedPassword.GroupBy(digit => digit).Count(c => c.Count() == 2) > 0;
        }

    }
}
