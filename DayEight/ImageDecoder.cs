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

        public ImageDecoder(string imagePath, int width, int height)
        {
            ReadImage(imagePath, width, height);
        }

        private void ReadImage(string imagePath, int width, int height)
        {
            var imageData = File.ReadLines(imagePath).ToArray()[0];

            var layerOffset = (width * height);
            var layers = imageData.Length / layerOffset;

            _image = new int[layers][,];

            for (var i = 0; i < layers; i++)
            {
                var layer = new int[height, width];
                for (var j = 0; j < height; j++)
                {
                    for (var k = 0; k < width; k++)
                    {
                        var pixelIndex = (layerOffset * i) + (j * width) +  k;
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
    }
}
