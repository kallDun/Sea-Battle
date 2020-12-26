using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle.Classes.Control
{
    class ControlKeyboard
    {

        public void keyPressedInPrepareMode(KeyEventArgs e, Player activePlayer)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    activePlayer.ChangePlaceToActiveShip(-1, 0);
                    break;

                case Keys.Right:
                    activePlayer.ChangePlaceToActiveShip(1, 0);
                    break;

                case Keys.Up:
                    activePlayer.ChangePlaceToActiveShip(0, -1);
                    break;

                case Keys.Down:
                    activePlayer.ChangePlaceToActiveShip(0, 1);
                    break;

                case Keys.A:
                    activePlayer.ChangePlaceToActiveShip(-1, 0);
                    break;

                case Keys.D:
                    activePlayer.ChangePlaceToActiveShip(1, 0);
                    break;

                case Keys.W:
                    activePlayer.ChangePlaceToActiveShip(0, -1);
                    break;

                case Keys.S:
                    activePlayer.ChangePlaceToActiveShip(0, 1);
                    break;

                case Keys.NumPad0:
                    activePlayer.RotateActiveShip();
                    break;

                case Keys.R:
                    activePlayer.RotateActiveShip();
                    break;

                case Keys.Space:
                    activePlayer.TryToSetTheShip();
                    break;

                case Keys.E:
                    activePlayer.TryToSetTheShip();
                    break;

                case Keys.Q:
                    activePlayer.restartPlayer();
                    break;
            }
        }

        public bool keyPressedInGameMode(KeyEventArgs e, Player activePlayer)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    activePlayer.MoveActiveCell(-1, 0);
                    break;

                case Keys.Right:
                    activePlayer.MoveActiveCell(1, 0);
                    break;

                case Keys.Up:
                    activePlayer.MoveActiveCell(0, -1);
                    break;

                case Keys.Down:
                    activePlayer.MoveActiveCell(0, 1);
                    break;

                case Keys.A:
                    activePlayer.MoveActiveCell(-1, 0);
                    break;

                case Keys.D:
                    activePlayer.MoveActiveCell(1, 0);
                    break;

                case Keys.W:
                    activePlayer.MoveActiveCell(0, -1);
                    break;

                case Keys.S:
                    activePlayer.MoveActiveCell(0, 1);
                    break;

                case Keys.Space:
                    return activePlayer.TryToShoot(); ;

                case Keys.E:
                    return activePlayer.TryToShoot();
            }
            return false;
        }

        public bool isRestart(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Space);
        }


    }
}
