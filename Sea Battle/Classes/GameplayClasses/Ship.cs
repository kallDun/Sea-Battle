using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sea_Battle.Classes
{
    class Ship
    {
        public List<Point> coordinates { get; private set; }
        public int type { get; private set; }

        public Ship(List<Point> coordinates)
        {
            this.coordinates = coordinates;
            type = coordinates.Count;
        }

        public bool CheckIsShipBroken(in Cell[,] cells)
        {
            foreach (var coord in coordinates)
            {
                if (!cells[coord.Y, coord.X].isBlownUp) return false;
            }

            return true;
        }
    }
}
