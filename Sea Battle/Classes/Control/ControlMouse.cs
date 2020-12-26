

using System.Drawing;
using System.Windows.Forms;
using static Sea_Battle.Parameters;
using static Sea_Battle.Classes.ControlParameters;
using System;

namespace Sea_Battle.Classes.Control
{
    class ControlMouse
    {
        public void MouseMovingToChangePlaceToShoot(MouseEventArgs e, Player activePlayer)
        {
            var coord = getCoord(e, activePlayer);
            if (IsMouseOnTable(coord))
            {
                activePlayer.ChangePlaceToActiveCell(coord);
            }
        }

        private Point getCoord(MouseEventArgs e, Player activePlayer)
        {
            int X = (e.X - activePlayer.tableCoordinates.X) / CellSize - 1;
            int Y = (e.Y - activePlayer.tableCoordinates.Y) / CellSize - 1;
            return new Point(X, Y);
        }
        private bool IsMouseOnTable(Point coord) =>
            coord.X >= 0 && coord.X < TableSize && coord.Y >= 0 && coord.Y < TableSize;

        public bool MouseClickToShoot(MouseEventArgs e, Player activePlayer)
        {
            MouseMovingToChangePlaceToShoot(e, activePlayer);
            if (IsMouseOnTable(getCoord(e, activePlayer)))
                return activePlayer.TryToShoot();
            else 
                return false;
        }

        public void MouseMoveToChoosePosition(MouseEventArgs e, Player activePlayer)
        {
            var coord = getCoord(e, activePlayer);
            if (IsMouseOnTable(coord))
            {
                activePlayer.ChangePositionToActiveShip(coord);
            }
        }

        public void MouseClickToChooseShipFromList(MouseEventArgs e, Player activePlayer)
        {
            var list = activePlayer.leftShips;

            int X = (e.X - activePlayer.listCoordinates.X) / CellSize / cellsInRowInList;
            int Y = (e.Y - activePlayer.listCoordinates.Y) / CellSize / cellsInColInList;

            int X_left = (e.X - activePlayer.listCoordinates.X) / CellSize % cellsInRowInList;
            int Y_left = (e.Y - activePlayer.listCoordinates.Y) / CellSize % cellsInColInList;

            if (X >= 0 && X < shipsInRowInList && Y >= 0 && Y < Math.Ceiling((double)list.Count / shipsInRowInList))
            {
                if (Y_left == 0)
                {
                    int ship_type = (Y * cellsInColInList) + X;

                    if (list[ship_type] > X_left)
                    {
                        activePlayer.CreateActiveShip(ship_type);
                    }
                }
            }
        }

        public void MouseClickToSetUpTheShip(MouseEventArgs e, Player activePlayer)
        {
            MouseMoveToChoosePosition(e, activePlayer);
            if (IsMouseOnTable(getCoord(e, activePlayer))) activePlayer.TryToSetTheShip();
        }

    }
}
