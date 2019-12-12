using System;
using System.Collections.Generic;
using System.Text;

namespace DayTwelve
{
    public class Moon
    {
        public (int x, int y, int z) Position;
        public (int x, int y, int z) Velocity;
        public int XPeriod { get; set; }
        public int YPeriod { get; set; }
        public int ZPeriod { get; set; }

        private (int x, int y, int z) _startPosition;



        public Moon(int[] position)
        {
            _startPosition = (position[0], position[1], position[2]);
            Position = (position[0], position[1], position[2]);
        }

        public void AdjustVelocity((int x, int y, int z) velocity)
        {
            Velocity.x += velocity.x;
            Velocity.y += velocity.y;
            Velocity.z += velocity.z;
        }

        public void UpdatePosition(int tick)
        {
            Position.x += Velocity.x;
            Position.y += Velocity.y;
            Position.z += Velocity.z;

            if(!HasAllPeriods()) UpdateAxisPeriod(tick+1);
        }

        public int GetTotalEnergy()
        {
            var pos = Math.Abs(Position.x) + Math.Abs(Position.y) + Math.Abs(Position.z);
            var kin = Math.Abs(Velocity.x) + Math.Abs(Velocity.y) + Math.Abs(Velocity.z);

            return pos * kin;
        }

        public bool HasAllPeriods()
        {
            var a = XPeriod > 0 && YPeriod > 0 && ZPeriod > 0 ;
            return a;
        }

        private void UpdateAxisPeriod(int tick)
        {
            if (Position.x == _startPosition.x && Velocity.x == 0 && XPeriod == 0)
            {
                XPeriod = tick;
            }
            if (Position.y == _startPosition.y && Velocity.y == 0 && YPeriod == 0)
            {
                YPeriod = tick;
            }
            if (Position.z == _startPosition.z && Velocity.z == 0 && ZPeriod == 0)
            {
                ZPeriod = tick;
            }
        }
    }
}
