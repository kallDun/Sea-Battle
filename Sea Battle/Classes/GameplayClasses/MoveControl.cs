
using System.Drawing;
using static Sea_Battle.Parameters;

namespace Sea_Battle.Classes.GameplayClasses
{
    class MoveControl
    {
        public Point activeCell { get; private set; }

        public MoveControl(Point activeCell) => this.activeCell = activeCell;

        public void SetCoordForActiveCell(Point point)
        {
            activeCell = point;
        }

        public void MoveActiveCell(int offsetX, int offsetY)
        {
            var newX = activeCell.X + offsetX;
            var newY = activeCell.Y + offsetY;

            if (newX < 0 || newX >= TableSize || newY < 0 || newY >= TableSize) return;

            activeCell = new Point(newX, newY);
        }
    }
}
