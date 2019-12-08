using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DayEight
{
    public class ImageDecoder
    {
        private int[][,] _image;
        private int _width;
        private int _height;
        private int[,] _decodedImage;

        public ImageDecoder(string imagePath, int width, int height)
        {
            _width = width;
            _height = height;
            ReadImage(imagePath);
        }

        private void ReadImage(string imagePath)
        {
            var imageData = File.ReadLines(imagePath).ToArray()[0];

            var layerOffset = (_width * _height);
            var layers = imageData.Length / layerOffset;

            _image = new int[layers][,];

            for (var i = 0; i < layers; i++)
            {
                var layer = new int[_height, _width];
                for (var j = 0; j < _height; j++)
                {
                    for (var k = 0; k < _width; k++)
                    {
                        var pixelIndex = (layerOffset * i) + (j * _width) +  k;
                        layer[j, k] = int.Parse(imageData[pixelIndex].ToString());
                    }
                }
                _image[i] = layer;
            }
        }

        public int? RunCheckSum()
        {
            (int? count, int? sum) check = (null, null);
            foreach (var layer in _image)
            {
                var zeroCount = layer.Cast<int>().Count(i => i == 0);
                if (check.count != null && zeroCount > check.count) continue;
                {
                    var ones = layer.Cast<int>().Count(i => i == 1);
                    var twos = layer.Cast<int>().Count(i => i == 2);
                    check.count = zeroCount;
                    check.sum = ones * twos;
                }
            }
            return check.sum;
        }

        public void DecodeImage()
        {
            _decodedImage = new int[_height, _width];
            for (var i = _image.Length -1 ; i >= 0; i--)
            {
                for (var j = 0; j < _height; j++)
                {
                    for (var k = 0; k < _width; k++)
                    {
                        if(_image[i][j, k] != 2)
                            _decodedImage[j,k] = _image[i][j, k];
                    }
                }
            }
        }


        public void RenderImage()
        {
            Console.WriteLine($"Image Resolution:{_width} x {_height}");
            Console.WriteLine("--Start--");
            for (var j = 0; j < _height; j++)
            {
                for (var k = 0; k < _width; k++)
                {
                    if (_decodedImage[j, k] == 0)
                        PrintPrimary();
                    else if (_decodedImage[j, k] == 1) 
                        PrintSecondary();
                }
                Console.WriteLine();
            }

            ResetConsoleColours();
            Console.WriteLine("---End---");
        }

        public void PrintPrimary()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("#");
        }

        public void PrintSecondary()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
        }

        public void ResetConsoleColours()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
