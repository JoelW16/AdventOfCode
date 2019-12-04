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
            var numberOfPasswords = GetNumberOfPasswordsMatchesInRange(193651, 649729);
            Console.WriteLine($"Passwords which match criteria: {numberOfPasswords}");
        }

        private static int GetNumberOfPasswordsMatchesInRange(int lower, int upper)
        {
            var possiblePasswords = new List<int>();
            for (var i = lower; i < upper; i++)
            {
                var password = i.ToString();
                if (IsSorted(password) && HasValidAdjacentDigits(password))
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

        private static bool HasValidAdjacentDigits(string password)
        {
            var adjacentDigits = new List<(char digit, int count)>();

            foreach (var unit in password)
            {
                if (adjacentDigits.Count > 0 && adjacentDigits[^1].digit == unit)
                {
                    var lastAdjacent = adjacentDigits[^1];
                    lastAdjacent.count++;
                    adjacentDigits[^1] = lastAdjacent;
                }
                else
                {
                    adjacentDigits.Add((unit, 1));
                }
            }
            //Part 1
            //return adjacentDigits.Count(c => c.count > 1) > 0;

            //Part 2
            return adjacentDigits.Count(c => c.count == 2) > 0;
        }

    }
}
