
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sea_Battle.Classes
{
    class ShipControl
    {
        public Ship activeShip { get; private set; } = null;
        public int positionOfContrlShip { get; private set; } = 0;

        private List<int> leftShips;
        public ShipControl(List<int>  leftShips) => this.leftShips = leftShips;


        public void createNewShip(int position)
        {
            if (position < 0 || position >= leftShips.Count) return;

            List<Point> coordinates = new List<Point> { };

            for (int i = 0; i < leftShips[position]; i++)
            {
                coordinates.Add(new Point( 0, i ));
            }

            positionOfContrlShip = position;
            activeShip = new Ship(coordinates);
        }

        public void rotateActiveShip()
        {
            if (activeShip == null) return;

            List<Point> newCoord = new List<Point> { };

            Point fulcrum = activeShip.coordinates.First(); 
            Point endPoint = activeShip.coordinates.Last();

            var gapX = endPoint.X - fulcrum.X;
            var gapY = endPoint.Y - fulcrum.Y;

            if (gapX > gapY)
            {
                for (int i = 0; i <= gapX; i++)
                {
                    Point coord = new Point(fulcrum.X, fulcrum.Y + i);

                    if ((coord.X < 0 || coord.X >= Parameters.TableSize)
                    || coord.Y < 0 || coord.Y >= Parameters.TableSize) return;

                    newCoord.Add(coord);
                }
            } 
            else
            {
                for (int i = 0; i <= gapY; i++)
                {
                    Point coord = new Point(fulcrum.X + i, fulcrum.Y);

                    if ((coord.X < 0 || coord.X >= Parameters.TableSize)
                    || coord.Y < 0 || coord.Y >= Parameters.TableSize) return;

                    newCoord.Add(coord);
                }
            }

            activeShip = new Ship(newCoord);
        }

        public void changePosition(Point point)
        {
            if (activeShip == null) return;

            List<Point> newCoord = new List<Point>(activeShip.coordinates);
            Point fulcrum = activeShip.coordinates.First();

            for (int i = 0; i < newCoord.Count; i++)
            {
                Point coord = newCoord[i];

                coord.X += point.X - fulcrum.X;
                coord.Y += point.Y - fulcrum.Y;

                if ((coord.X < 0 || coord.X >= Parameters.TableSize)
                    || coord.Y < 0 || coord.Y >= Parameters.TableSize) return;

                newCoord[i] = coord;
            }

            activeShip = new Ship(newCoord);
        }

        public void moveActiveShip(int offsetX, int offsetY)
        {
            if (activeShip == null) return;

            List<Point> newCoord = new List<Point>(activeShip.coordinates);

            for (int i = 0; i < newCoord.Count; i++)
            {
                Point coord = newCoord[i];
                coord.X += offsetX;
                coord.Y += offsetY;

                if ((coord.X < 0 || coord.X >= Parameters.TableSize)
                    || coord.Y < 0 || coord.Y >= Parameters.TableSize) return;

                newCoord[i] = coord;
            }

            activeShip = new Ship(newCoord);
        }

        public void deleteActiveShip() => activeShip = null;

    }
}
