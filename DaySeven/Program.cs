﻿using System;
using System.IO;

namespace DaySeven
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var computer = new IntCodeComputer();
            var intCodeProgram = computer.ReadInIntCodeProgram(dataDirectory);
            var thrustBot = new ThrustBot();
            var result = thrustBot.Run(computer, intCodeProgram);

            Console.WriteLine(result);


            //zConsole.WriteLine(value);
        }
    }
}
