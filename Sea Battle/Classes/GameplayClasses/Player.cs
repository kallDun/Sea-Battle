
using Newtonsoft.Json;
using Sea_Battle.Classes.GameplayClasses;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sea_Battle.Classes
{
    class Player
    {
        [JsonProperty]
        public MoveControl moveControl { get; private set; }
        [JsonProperty]
        public ShipControl shipControl { get; private set; }
        [JsonProperty]
        public Field field { get; private set; }
        [JsonProperty]
        public string name { get; private set; }
        [JsonProperty]
        public int scores { get; private set; } = 0;
        [JsonProperty]
        public List<int> leftShips { get; private set; }
        [JsonProperty]
        public Point tableCoordinates { get; private set; }
        [JsonProperty]
        public Point listCoordinates { get; private set; }

        // for online game
        [JsonProperty]
        public bool isTurn;
        [JsonProperty]
        public bool isLoad = false;


        public Player(string name, Point tableCoordinates, Point listCoordinates)
        {
            this.name = name;
            this.tableCoordinates = tableCoordinates;
            this.listCoordinates = listCoordinates;
            initialize();
        }

        private void initialize()
        {
            leftShips = new List<int>(Parameters.ships);
            moveControl = new MoveControl(new Point(0, 0));
            shipControl = new ShipControl(leftShips);
            field = new Field();
            shipControl.createNewShip(0);
        }

        public void TryToSetTheShip()
        {
            if (field.IsAvailablePlaceForShip(shipControl.activeShip))
            {
                leftShips.Remove(shipControl.activeShip.type);
                field.AddNewShip(shipControl.activeShip);
                DeleteTheActiveShip();
                shipControl.createNewShip(0);
            }
        }

        public bool TryToShoot()
        {
            if (CanAnyoneShoot(GetActiveCell()))
            {
                return field.Shoot(GetActiveCell());
            }
            return false;
        }

        public bool CheckIsLose()
        {
            field.updateDestroyedShips();
            return field.isAllShipsDestroyed();
        }

        public void restartPlayer()
        {
            field.restartField();
            initialize();
        }

        public void PrepareForGame() => field.updateFieldForGame();
        public bool IsReady() => leftShips.Count() == 0;

        public Cell[,] getField() => field.cells;
        public List<Ship> GetAllShips() => field.ships;
        public List<Ship> GetDestroyedShips() => field.destroyedShips;
        public Ship GetActiveShip() => shipControl.activeShip;
        public Point GetActiveCell() => moveControl.activeCell;
        public int GetActiveShipPosition() => shipControl.positionOfContrlShip;
        public void AddScore() => scores++;

        public bool CanAnyoneShoot(Point coord) => field.CanAnyoneShoot(coord);
        public void CreateActiveShip(int position = 0) => shipControl.createNewShip(position);
        public void RotateActiveShip() => shipControl.rotateActiveShip();
        public void DeleteTheActiveShip() => shipControl.deleteActiveShip();
        public void ChangePlaceToActiveShip(int offsetX, int offsetY) => shipControl.moveActiveShip(offsetX, offsetY);
        public void ChangePositionToActiveShip(Point point) => shipControl.changePosition(point);

        public void MoveActiveCell(int offsetX, int offsetY) => moveControl.MoveActiveCell(offsetX, offsetY);
        public void ChangePlaceToActiveCell(Point point) => moveControl.SetCoordForActiveCell(point);

        public void ChangeField(Field field) => this.field = field;
        public void ChangeName(string name) => this.name = name;
        public void ChangeScores(int scores) => this.scores = scores;
    }
}
