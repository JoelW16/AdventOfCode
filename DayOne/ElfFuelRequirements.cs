using System;
using System.IO;
using System.Linq;

namespace DayOne{
    public class ElfFuelRequierments
    {
        public static void Main(string [] args){
            var masses = ReadMasses();
            var fuelRequirement = GetFuelRequirementPartOne(masses);
            var totalFuelRequierment = GetFuelRequirementPartTwo(masses);
            //The elves fuel requirement is: 3432671, however the total fuel needed to carry fuel is: 5146132
            Console.WriteLine($"The elves fuel requirement is: {fuelRequirement}, however the total fuel needed to carry fuel is: {totalFuelRequierment}");
        }

        private static int GetFuelRequirementPartOne(int[] masses)
        {
            return masses.Sum(mass => (mass / 3) - 2);
        }

        private static int GetFuelRequirementPartTwo(int[] masses)
        {
            return masses.Sum(mass => GetTotalFuelRequirement(mass));
        }


        private static int GetTotalFuelRequirement(int mass)
        {
            var requiredFuel = (mass / 3) - 2;
            if(requiredFuel > 0) return requiredFuel + GetTotalFuelRequirement(requiredFuel);
            return 0;

        }

        private static int[] ReadMasses(){
            var masses = File.ReadLines("ElvesShipMasses.txt").Select(mass => Convert.ToInt32(mass)).ToArray();
            return masses;
        }
    }
}

