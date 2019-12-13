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
        private readonly List<Moon> _moons;
        private HashSet<(int, int, int, int, int, int, int, int)> _xTicks;
        private HashSet<(int, int, int, int, int, int, int, int)> _yTicks;
        private HashSet<(int, int, int, int, int, int, int, int)> _zTicks;


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

        public void Reset()
        {
            foreach (var moon in _moons)
            {
                moon.Reset();
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

        public long RunFindPeriod()
        {
            _ticks = 0;
            _xTicks = new HashSet<(int, int, int, int, int, int, int, int)>();
            _yTicks = new HashSet<(int, int, int, int, int, int, int, int)>();
            _zTicks = new HashSet<(int, int, int, int, int, int, int, int)>();
            while (!FoundAllPeriods())
            {
                SimulateTick();
                _ticks++;
            }

            return FindLowestCommonPeriod();
        }

        private long FindLowestCommonPeriod()
        {
            var periods = new long[]
            {
                _xTicks.Count,
                _yTicks.Count,
                _zTicks.Count,
            };

            return periods.Aggregate(Lcm);
        }

        private long Lcm(long a, long b)
        {
            return Math.Abs(a * b) / Gcd(a, b);
        }

        private long Gcd(long a, long b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }

        private bool FoundAllPeriods()
        {
            var hasXPeriod = HasXPeriod();
            var hasYPeriod = HasYPeriod();
            var hasZPeriod = HasZPeriod();

            return hasXPeriod && hasYPeriod && hasZPeriod;
        }

        private bool HasXPeriod()
        {
            var tick = (_moons[0].Position.x, _moons[1].Position.x, _moons[2].Position.x, _moons[3].Position.x,
                _moons[0].Velocity.x, _moons[1].Velocity.x, _moons[2].Velocity.x, _moons[3].Velocity.x);

            if (_xTicks.Contains(tick))
            {
                return true;
            }
            _xTicks.Add(tick);
            return false;
        }

        private bool HasYPeriod()
        {
            var tick = (_moons[0].Position.y, _moons[1].Position.y, _moons[2].Position.y, _moons[3].Position.y,
                _moons[0].Velocity.y, _moons[1].Velocity.y, _moons[2].Velocity.y, _moons[3].Velocity.y);

            if (_yTicks.Contains(tick))
            {
                return true;
            }
            _yTicks.Add(tick);
            return false;
        }

        private bool HasZPeriod()
        {
            var tick = (_moons[0].Position.z, _moons[1].Position.z, _moons[2].Position.z, _moons[3].Position.z,
                _moons[0].Velocity.z, _moons[1].Velocity.z, _moons[2].Velocity.z, _moons[3].Velocity.z);

            if (_zTicks.Contains(tick))
            {
                return true;
            }
            _zTicks.Add(tick);
            return false;
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
                moon.UpdatePosition(_ticks);
            }
        }

        private int GetTotalEnergy()
        {
            return _moons.Sum(moon => moon.GetTotalEnergy());
        }
    }
}
