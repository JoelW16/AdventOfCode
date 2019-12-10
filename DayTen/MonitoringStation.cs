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
                var (key, _) = _asteroids.ElementAt(i);
                _asteroids[key] = NumberInLineOfSight(AsteroidsLocations(key));
            }

            return _asteroids.Aggregate((l, r) => l.Value > r.Value ? l : r);
        }

        private static int NumberInLineOfSight(List<Dictionary<(int, int), decimal>> asteroidLocations)
        {
            return asteroidLocations.Sum(asteroidLocation => asteroidLocation.Values.Distinct().Count());
        }

        private List<Dictionary<(int, int), decimal>> AsteroidsLocations((int x, int y) a)
        {
            var viewPointPositiveGradients = new Dictionary<(int, int), decimal>();
            var viewPointNegativeGradients = new Dictionary<(int, int), decimal>();
            var viewPointHorizontal = new Dictionary<(int, int), decimal>();
            var viewPointVertical = new Dictionary<(int, int), decimal>();

            foreach (var (b, _) in _asteroids)
            {
                if (a == b) continue;
                //Check if Vertical or Horizontal
                var dy = (b.y - a.y);
                var sy = (a.y > b.y) ? 1 : -1;
                var dx = (b.x - a.x);
                var sx = (a.x > b.x) ? 1 : -1;

                if (dy == 0)
                {
                    viewPointHorizontal.Add(b, sx);
                    continue;
                }
                if(dx == 0)
                {
                    viewPointVertical.Add(b, sy);
                    continue;
                }

                //Get Gradients
                var gradient = (decimal)dy / dx;

                if (sy == 1)
                {
                    viewPointPositiveGradients.Add(b, gradient);
                }
                else
                {
                    viewPointNegativeGradients.Add(b, gradient);
                }
            }
            var asteroids = new List<Dictionary<(int, int), decimal>>
            {
                viewPointVertical, viewPointPositiveGradients, viewPointHorizontal, viewPointNegativeGradients
            };
            return asteroids;
        }



        public List<(int, int)> GetVaporiseAsteroidManifest((int, int) asteroidBase)
        {
            var isNorth = true;
            var isEast = true;

            var manifest = new List<(int, int)>();
            List<Dictionary<(int, int), decimal>> asteroidLocations = AsteroidsLocations(asteroidBase);

            var vertical = asteroidLocations[0];
            var positiveGradients = asteroidLocations[1];
            var horizontal = asteroidLocations[2];
            var negativeGradients = asteroidLocations[3];

            var count = positiveGradients.Count() + vertical.Count() + horizontal.Count() + negativeGradients.Count();

            while (count > 0)
            {
                if (isNorth)
                {
                    //Get North
                    var asteroid = vertical.OrderByDescending(i => i.Key.Item2).FirstOrDefault(i => i.Value > 0);
                    DestroyAsteroid(asteroid.Key, ref manifest, ref vertical, ref count);

                    Dictionary<(int, int), decimal> asteroids = null;
                    if (isEast)
                    {
                        //North East
                        asteroids = positiveGradients.OrderBy(i => i.Value).ThenBy().Where(i => i.Value > 0).ToDictionary(i => i.Key, i => i.Value);
                        DestroyAsteroids(asteroids, ref manifest, ref positiveGradients, ref count);


                        //Get East
                        asteroid = horizontal.OrderByDescending(i => i.Key.Item2).FirstOrDefault(i => i.Value < 0);
                        DestroyAsteroid(asteroid.Key, ref manifest, ref vertical, ref count);
                        isNorth = false;
                    }
                    else
                    {
                        //North West
                        asteroids = positiveGradients.OrderByDescending(i => i.Value).Where(i => i.Value < 0).ToDictionary(i => i.Key, i => i.Value);
                        DestroyAsteroids(asteroids, ref manifest, ref positiveGradients, ref count);
                        isEast = true;
                    }
                }
                else
                {
                    KeyValuePair<(int, int), decimal> asteroid;
                    Dictionary<(int, int), decimal> asteroids = null;
                    if (isEast)
                    {
                        //South East
                        asteroids = negativeGradients.OrderBy(i => i.Value).Where(i => i.Value < 0).ToDictionary(i => i.Key, i => i.Value);
                        DestroyAsteroids(asteroids, ref manifest, ref negativeGradients, ref count);

                        //Get South
                        asteroid = vertical.OrderBy(i => i.Key.Item2).FirstOrDefault(i => i.Value < 0);
                        DestroyAsteroid(asteroid.Key, ref manifest, ref vertical, ref count);
                        isEast = false;
                        
                    }
                    else
                    {
                        //South West
                        asteroids = negativeGradients.OrderByDescending(i => i.Value).Where(i => i.Value > 0).ToDictionary(i => i.Key, i => i.Value);
                        DestroyAsteroids(asteroids, ref manifest, ref negativeGradients, ref count);

                        //Get West
                        asteroid = horizontal.OrderByDescending(i => i.Key.Item1).FirstOrDefault(i => i.Value > 0);
                        DestroyAsteroid(asteroid.Key, ref manifest, ref horizontal, ref count);
                        isNorth = true;
                    }
                }
            }
            return manifest;
        }

        private void DestroyAsteroids(Dictionary<(int, int), decimal> asteroids, ref List<(int, int)> manifest, ref Dictionary<(int, int), decimal> dictionary, ref int count)
        {
            decimal previous = 0;
            for (var i = 0; i < asteroids.Count; i++)
            {
                var (key, value) = asteroids.ElementAt(i);
                if (value == previous) continue;

                previous = value;
                DestroyAsteroid(key, ref manifest, ref dictionary, ref count);
            }
        }

        private void DestroyAsteroid((int, int) asteroid, ref List<(int, int)> manifest, ref Dictionary<(int, int), decimal> list, ref int count)
        {
            manifest.Add(asteroid);
            list.Remove(asteroid);
            count--;
        }
    }
}
