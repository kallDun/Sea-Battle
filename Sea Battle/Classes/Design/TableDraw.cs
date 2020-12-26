
using Sea_Battle.Classes;
using System.Collections.Generic;
using System.Drawing;
using static Sea_Battle.Parameters;
using static Sea_Battle.Design.DrawParameters;
using static Sea_Battle.Classes.ControlParameters;
using System.Linq;
using System;

namespace Sea_Battle.Design
{
    class TableDraw
    {
        private Point tableCoord;

        public TableDraw(Point tableCoord)
        {
            this.tableCoord = tableCoord;
        }

        public void DrawTable(Graphics g, in Cell[,] cells)
        {
            var coord = tableCoord;

            g.DrawRectangle(boldPen, coord.X + CellSize, coord.Y + CellSize, 
                TableSize * CellSize, TableSize * CellSize);

            for (int i = 0; i < TableSize; i++)
            {
                for (int j = 0; j < TableSize; j++)
                {
                    DrawCell(g, cells[i, j], new Point(
                        (coord.X + CellSize) + j * CellSize,
                        (coord.Y + CellSize) + i * CellSize)
                        );
                }
            }

            DrawMarkers(g, coord);
        }

        public void DrawInfo(Graphics g, string name, int scores)
        {
            var point = new Point(
                tableCoord.X + (TableSize / 2 - 2) * CellSize,
                tableCoord.Y + (TableSize + 1) * CellSize);

            g.DrawString($"{name} ({scores} scores)", textFont, textColor, point);
        }

        public void DrawActiveCell(Graphics g, Point point)
        {
            g.DrawRectangle(activeCellPen, 
                (tableCoord.X + CellSize) + point.X * CellSize,
                (tableCoord.Y + CellSize) + point.Y * CellSize, 
                CellSize, CellSize);
        }

        private void DrawCell(Graphics g, in Cell cell, Point coord)
        {
            g.DrawRectangle(cellPen, coord.X, coord.Y, CellSize, CellSize);

            if (cell.isBlownUp && (!cell.hasNoShip)) DrawCross(g, coord);
            else if (cell.isBlownUp) DrawFill(g, coord);
        }

        private void DrawFill(Graphics g, Point coord)
        {
            var rectangle = new Rectangle(coord.X + CellSize / 3, coord.Y + CellSize / 3, CellSize / 3, CellSize / 3);
            g.FillEllipse(Brushes.Gray, rectangle);
            g.DrawEllipse(elipsePen, rectangle);
            
        }

        private void DrawCross(Graphics g, Point coord)
        {
            g.DrawLine(crossPen, new Point(coord.X, coord.Y), new Point(coord.X + CellSize, coord.Y + CellSize));
            g.DrawLine(crossPen, new Point(coord.X + CellSize, coord.Y), new Point(coord.X, coord.Y + CellSize));
        }

        private void DrawMarkers(Graphics g, Point coord)
        {
            var firstNumber = 1;
            var firstLetter = 'a';

            for (int i = 1; i <= TableSize; i++)
            {
                g.DrawString(firstNumber.ToString(), markersFont, textColor, 
                    new Point(coord.X + markersShift, coord.Y + markersShift + i * CellSize));
                firstNumber++;
            }

            for (int i = 1; i <= TableSize; i++)
            {
                g.DrawString(firstLetter.ToString(), markersFont, textColor,
                    new Point(coord.X + markersShift + i * CellSize, coord.Y + markersShift));
                firstLetter++;
            }
        }

        public void DrawDestroyedShips(Graphics g, in List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                DrawDestroyedShip(g, ship);
            }
        }

        private void DrawDestroyedShip(Graphics g, in Ship ship)
        {
            if (ship == null) return;

            Rectangle rectangle = new Rectangle(
                tableCoord.X + CellSize * (ship.coordinates[0].X + 1),
                tableCoord.Y + CellSize * (ship.coordinates[0].Y + 1),

                CellSize * (Math.Abs(ship.coordinates.Last().X - ship.coordinates[0].X) + 1),
                CellSize * (Math.Abs(ship.coordinates.Last().Y - ship.coordinates[0].Y) + 1)
                );

            g.FillRectangle(destrShipColor, rectangle);
        }

        public void DrawShips(Graphics g, in List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                DrawShip(g, ship);
            }
        }

        public void DrawShip(Graphics g, in Ship ship, bool isActive = false)
        {
            if (ship == null) return;

            var pen = isActive ? activePen : shipPen;

            Rectangle rectangle = new Rectangle(
                tableCoord.X + CellSize * (ship.coordinates[0].X + 1),
                tableCoord.Y + CellSize * (ship.coordinates[0].Y + 1),

                CellSize * (Math.Abs(ship.coordinates.Last().X - ship.coordinates[0].X) + 1),
                CellSize * (Math.Abs(ship.coordinates.Last().Y - ship.coordinates[0].Y) + 1)
                );

            g.DrawRectangle(pen, rectangle);
        }

    }
}
