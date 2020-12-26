
using Sea_Battle.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Sea_Battle.Classes.ControlParameters;
using static Sea_Battle.Design.DrawParameters;

namespace Sea_Battle.Design
{
    class ElementsDraw
    {
        private Point startCoord;
        public ElementsDraw(Point startCoord) => this.startCoord = startCoord;


        public void updateListOfShips(Graphics g, List<int> leftShips)
        {
            var coord = startCoord;

            for (int i = 0; i < Math.Ceiling((double) leftShips.Count / shipsInRowInList); i++)
            {
                for (int j = 0; j < shipsInRowInList; j++)
                {
                    if (((i * shipsInRowInList) + j) >= leftShips.Count()) return;

                    int takenShipType = leftShips[(i * shipsInRowInList) + j];

                    var shipRectangle = new Rectangle(coord.X + j * cellsInRowInList * CellSize,
                        coord.Y + i * cellsInColInList * CellSize, takenShipType * CellSize, CellSize);

                    g.DrawRectangle(shipPen, shipRectangle);

                }
            }
        }

        public void updateActiveShipInList(Graphics g, Ship activeShip, int pos_ActShip)
        {
            if (activeShip == null) return;

            var coord = startCoord;

            var activeType = activeShip.type;
            var activeTypeIndex = pos_ActShip;
            int indexI = activeTypeIndex / shipsInRowInList;
            int indexJ = activeTypeIndex % shipsInRowInList;

            var activeShipRectangle = new Rectangle(coord.X + indexJ * cellsInRowInList * CellSize,
                        coord.Y + indexI * cellsInColInList * CellSize, activeType * CellSize, CellSize);

            g.FillRectangle(Brushes.Gray, activeShipRectangle);
        }

    }
}
