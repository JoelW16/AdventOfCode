using System;
using System.IO;

namespace DayEight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "TransmittedImage.txt");

            var imageDecoder = new ImageDecoder(dataDirectory, 25, 6);
            var checkSum = imageDecoder.RunCheckSum();
            Console.WriteLine($"Image Transmission CheckSum:{checkSum}");
        }
    }
}
