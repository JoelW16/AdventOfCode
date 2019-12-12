using System;
using System.Collections.Generic;
using System.Text;

namespace DayTwelve
{
    public class Moon
    {
        public (int x, int y, int z) Position;
        public (int x, int y, int z) Velocity;

        public Moon(int[] position)
        {
           Position = (position[0], position[1], position[2]);
        }

        public void AdjustVelocity((int x, int y, int z) velocity)
        {
            Velocity.x += velocity.x;
            Velocity.y += velocity.y;
            Velocity.z += velocity.z;
        }

        public void UpdatePosition()
        {
            Position.x += Velocity.x;
            Position.y += Velocity.y;
            Position.z += Velocity.z;
        }

        public int GetTotalEnergy()
        {
            var pos = Math.Abs(Position.x) + Math.Abs(Position.y) + Math.Abs(Position.z);
            var kin = Math.Abs(Velocity.x) + Math.Abs(Velocity.y) + Math.Abs(Velocity.z);

            return pos * kin;
        }
    }
}
