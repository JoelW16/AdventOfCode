using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DayTen
{
    public class MonitoringStation
    {
        private Dictionary<(int x, int y), int> _asteroids;

        public MonitoringStation(string path)
        {
            ReadAsteroidMap(path);
        }

        private void ReadAsteroidMap(string path)
        {
            _asteroids = new Dictionary<(int x, int y), int>();
            var lines = File.ReadLines(path).ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        _asteroids.Add((j, i), 0);
                    }
                }
            }
        }

        public KeyValuePair<(int,int), int> FindBestViewPoint()
        {
            for (var i = 0; i < _asteroids.Count; i ++)
            {
                var (a, _) = _asteroids.ElementAt(i);
                var ast = MapAsteroidsFrom(a);
                _asteroids[a] = ast.Select(i => i.Angle).Distinct().Count();
            }

            return _asteroids.Aggregate((l, r) => l.Value > r.Value ? l : r);
        }


        private List<Asteroid> MapAsteroidsFrom((int x, int y) a)
        {
            List<Asteroid> ast = new List<Asteroid>();
            foreach (var (b, _) in _asteroids)
            {

                if (a == b) continue;
                float xDiff = b.x - a.x;
                float yDiff = a.y - b.y;
                var rads = Math.Atan2(xDiff, yDiff);
                
                var compassDeg = (180 / Math.PI) * rads;
                double normaliseDeg(double a) => ((a % 360) + 360) % 360;

                var angle = normaliseDeg(compassDeg);

                ast.Add(new Asteroid {Coordinates = b, Angle = angle});
            }

            return ast;
        }

        
        public List<Asteroid> GetVaporiseAsteroidManifest((int x, int y) asteroidBase)
        {
            var manifest = new List<Asteroid>();
            var asteroids = MapAsteroidsFrom(asteroidBase).OrderBy(i => i.Angle)
                .ThenBy(i => Math.Abs(asteroidBase.x - i.Coordinates.x) + Math.Abs(asteroidBase.y - i.Coordinates.y))
                .ToList();

            while (asteroids.Any())
            {
                var previousHeading = 360d;
                for (var i = 0; i < asteroids.Count(); i++)
                {
                    var asteroid = asteroids[i];
                    if(Math.Abs(asteroid.Angle - previousHeading) < double.Epsilon) continue;
                    previousHeading = asteroid.Angle;
                    manifest.Add(asteroid);
                    asteroids.Remove(asteroid);
                    i--;
                }
            }
            return manifest;
        }
    }

    public class Asteroid
    {
        public double Angle { get; set; }
        public (int x, int y) Coordinates { get; set; }

        public override string ToString()
        {
            return $"{Coordinates.x},{Coordinates.y}: {Angle}";
        }
    }

}
