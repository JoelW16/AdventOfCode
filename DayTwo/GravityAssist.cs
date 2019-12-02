using System;
using System.IO;
using System.Linq;

namespace DayTwo
{
    public class GravityAssist
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var computer = new IntCodeComputer();
            var intCodeProgram = computer.ReadInIntCodeProgram(dataDirectory);

            var returnValue = BruteForceGravityAssist(computer, intCodeProgram); 
            Console.WriteLine(returnValue);
        }

        
        private static int BruteForceGravityAssist(IntCodeComputer computer, int[] intCodeProgram) {
            for(var i = 0 ; i < 100; i++){
                for(var j = 0; j < 100; j++){
                    var intCodeProgramCopy = (int[])intCodeProgram.Clone();
                    intCodeProgramCopy[1] = i;
                    intCodeProgramCopy[2] = j;

                    var program = (intCodeProgramCopy, 0);
                    var returnValue = computer.Run(program);

                    if(returnValue == 19690720) return 100 * i + j;
                }
            }
            return 0;
        }
    }
}