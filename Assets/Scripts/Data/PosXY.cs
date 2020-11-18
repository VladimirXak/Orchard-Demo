using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard
{
    [Serializable]
    public struct PosXY : IEquatable<PosXY>
    {
        public int x;
        public int y;

        public PosXY(int newX = 0, int newY = 0)
        {
            x = newX;
            y = newY;
        }

        public bool Equals(PosXY obj)
        {
            return (x == obj.x && y == obj.y);
        }

        public override string ToString()
        {
            return "x: " + x + ", y: " + y;
        }

        public static PosXY operator +(PosXY a, PosXY b)
        {
            return new PosXY(a.x + b.x, a.y + b.y);
        }

        public static PosXY operator -(PosXY a, PosXY b)
        {
            return new PosXY(a.x - b.x, a.y - b.y);
        }
    }
}
