using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using static Sea_Battle.Parameters;

namespace Sea_Battle.Classes
{
    class Field
    {
        [JsonProperty]
        public Cell[,] cells { get; private set; }
        [JsonProperty]
        public List<Ship> ships { get; private set; } = new List<Ship> { };
        [JsonProperty]
        public List<Ship> destroyedShips { get; private set; } = new List<Ship> { };

        public Field() => initialize();

        private void initialize()
        {
            cells = new Cell[TableSize, TableSize];

            for (int i = 0; i < TableSize; i++)
            {
                for (int j = 0; j < TableSize; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public void updateFieldForGame()
        {
            foreach (var cell in cells)
            {
                cell.prepareForGame();
            }
        }

        public void updateDestroyedShips()
        {
            foreach (var ship in ships)
            {
                if (!destroyedShips.Contains(ship) && ship.CheckIsShipBroken(cells))
                {
                    destroyedShips.Add(ship);
                    DestroyAroundShip(ship);
                }
            }
        }

        public void restartField()
        {
            ships.Clear();
            destroyedShips.Clear();
            initialize();
        }

        public bool isAllShipsDestroyed() => destroyedShips.Count() == ships.Count();

        public bool IsAvailablePlaceForShip(in Ship activeShip)
        {
            if (activeShip == null) return false;

            var whiteSpaceCount =
                activeShip.coordinates.Where(coord => !cells[coord.Y, coord.X].isBlownUp).Count();
            return whiteSpaceCount == activeShip.coordinates.Count();
        }

        public bool Shoot(Point coords) 
        { 
            cells[coords.Y, coords.X].blowUp();
            return cells[coords.Y, coords.X].hasNoShip;
        }

        public bool CanAnyoneShoot(Point coord) => !cells[coord.Y, coord.X].isBlownUp;

        public void AddNewShip(in Ship activeShip) 
        { 
            ships.Add(activeShip);
            DestroyAroundShip(activeShip);

            foreach (var coord in activeShip.coordinates)
            {
                cells[coord.Y, coord.X].takePlace();
            }
        }

        private void DestroyAroundShip(Ship ship)
        {
            foreach (var coord in ship.coordinates)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        var y = coord.Y + i;
                        var x = coord.X + j;

                        if (x >= 0 && x < TableSize && y >= 0 && y < TableSize)
                            cells[y, x].blowUp();
                    }
                }
            }
        }
    }
}
