using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DayTwelve
{
    public class OrbitSimulation
    {
        private int _ticks;
        private List<Moon> _moons;

        public OrbitSimulation(string path)
        {
            _moons = new List<Moon>();
            ReadMoons(path);
        }

        public void ReadMoons(string path)
        {
            var moonPositions = File.ReadLines(path).ToList();

            foreach (var moonPosition in moonPositions)
            {
                var position = moonPosition.Trim('<').Trim('>').Split(',').Select(i => int.Parse(i.Split('=')[1]))
                    .ToArray();
                var moon = new Moon(position);
                _moons.Add(moon);
            }
        }

        public int Run(int ticks)
        {
            _ticks = ticks;
            while (_ticks > 0)
            { 
                SimulateTick();
                _ticks--;
            }

            return GetTotalEnergy();
        }

        private void SimulateTick()
        {
            for (var i = 0; i < _moons.Count - 1; i++)
            {
                for (var m = i + 1; m < _moons.Count; m++)
                {
                    var moonA = _moons[i];
                    var moonB = _moons[m];

                    var x = moonA.Position.x > moonB.Position.x ? -1 : moonA.Position.x < moonB.Position.x ? 1 : 0;
                    var y = moonA.Position.y > moonB.Position.y ? -1 : moonA.Position.y < moonB.Position.y ? 1 : 0;
                    var z = moonA.Position.z > moonB.Position.z ? -1 : moonA.Position.z < moonB.Position.z ? 1 : 0;

                    moonA.AdjustVelocity((x, y, z));
                    moonB.AdjustVelocity((x * -1, y * -1, z * -1));
                }
            }

            foreach (var moon in _moons)
            {
                moon.UpdatePosition();
            }
        }

        private int GetTotalEnergy()
        {
            return _moons.Sum(moon => moon.GetTotalEnergy());
        }
    }
}
