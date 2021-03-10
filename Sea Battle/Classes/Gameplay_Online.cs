using Sea_Battle.Classes.Control;
using Sea_Battle.Classes.Server_Client;
using Sea_Battle.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sea_Battle.Classes.ControlParameters;

namespace Sea_Battle.Classes
{
    class Gameplay_Online : Form1, IGameplay
    {
        private ControlKeyboard controlKeyboard = new ControlKeyboard();
        private ControlMouse controlMouse = new ControlMouse();

        private PlayersDraw players_drawing = new PlayersDraw();
        private Player winner = null;

        // checkers:
        private const int max_isWaiting_times = 12;
        private const int waiting_time_in_sec = 10;
        private const int timesToCheckLosers_Max = 100;

        private bool everyoneIsReady = false;
        private bool endGame = false;
        private bool canDoAnything = true;
        private int isWaiting = 0;
        private int timesToCheckLosers_Now = timesToCheckLosers_Max;

        private Player ourPlayer, otherPlayer;
        private IServer server;

        private Field otherPlayer_field;

        public Gameplay_Online(IServer server, bool isTurn)
        {
            this.server = server;

            ourPlayer = new Player(
                $"Player {(isTurn ? 1 : 2)}", 
                isTurn? player1_TableCoordinates : player2_TableCoordinates,
                isTurn? player1_ListCoordinates : player2_ListCoordinates);
            ourPlayer.isTurn = isTurn;

            server.SendData(ourPlayer);
            otherPlayer = (server.GetData());
        }
        
        public void Update(Graphics g)
        {
            UpdateStatus(g);

            if (everyoneIsReady && !endGame)
            {
                if (timesToCheckLosers_Now == 0)
                {
                    int count1 = ourPlayer.GetDestroyedShips().Count();
                    int count2 = otherPlayer.GetDestroyedShips().Count();
                    int max_count = Parameters.ships.Count();

                    if (count1 == max_count) winner = otherPlayer;
                    else if (count2 == max_count) winner = ourPlayer;

                    if (winner != null)
                    {
                        winner.AddScore();
                        endGame = true;
                        canDoAnything = true;
                    }
                }
                else if (timesToCheckLosers_Now > 0) timesToCheckLosers_Now--;
            }
            else if (endGame)
            {
                players_drawing.updateWinSituation(g, winner);
                new TableDraw(ourPlayer.tableCoordinates).DrawShips(g, ourPlayer.GetAllShips());
                if (!otherPlayer.IsReady()) RestartGame();
            }
        }

        public void UpdateStatus(Graphics g)
        {
            if (everyoneIsReady)
            {
                players_drawing.updateGame(g,
                new List<Player> { ourPlayer, otherPlayer },
                ourPlayer.isTurn ? ourPlayer : otherPlayer);

                var other = (!ourPlayer.isTurn ? ourPlayer : otherPlayer);
                new TableDraw(other.tableCoordinates).DrawActiveCell(g, other.GetActiveCell(), true);
                new TableDraw(otherPlayer.tableCoordinates).DrawShips(g, otherPlayer.GetAllShips());

                return;
            }
            else
            if (ourPlayer.IsReady() && otherPlayer.IsReady())
            {
                if (!ourPlayer.isLoad && !otherPlayer.isLoad)
                {
                    otherPlayer_field = otherPlayer.field;
                    ourPlayer.isLoad = true;
                }
                else if (ourPlayer.isLoad && otherPlayer.isLoad)
                {
                    ourPlayer.ChangeField(otherPlayer_field);
                    ourPlayer.PrepareForGame();
                    everyoneIsReady = true;
                }
            }
            else
                players_drawing.updatePrepare(g, ourPlayer);
        }

        // ping data between players
        public void updateServerTick()
        {
            server.SendData(ourPlayer);
            var other = server.GetData();
            if (other != null) otherPlayer = other;

            if (everyoneIsReady && !endGame)
            {
                if (!otherPlayer.isTurn && !ourPlayer.isTurn && isWaiting == 0) ourPlayer.isTurn = true;
                else if (isWaiting > 0) isWaiting--;

                canDoAnything = ourPlayer.isTurn;
                ourPlayer.field.updateDestroyedShips();
            }
        }

        public void RestartGame()
        {
            ourPlayer.restartPlayer();
            ourPlayer.isLoad = false;
            endGame = false;
            winner = null;
            everyoneIsReady = false;
            isWaiting = max_isWaiting_times;
            timesToCheckLosers_Now = timesToCheckLosers_Max;
        }

        public void MouseMoving(MouseEventArgs e)
        {
            if (everyoneIsReady) controlMouse.MouseMovingToChangePlaceToShoot(e, ourPlayer);
            else controlMouse.MouseMoveToChoosePosition(e, ourPlayer);
        }

        public void MouseClick(MouseEventArgs e)
        {
            if (canDoAnything)
            {
                if (everyoneIsReady && !endGame)
                {
                    if (controlMouse.MouseClickToShoot(e, ourPlayer))
                    {
                        ourPlayer.isTurn = false;
                        isWaiting = max_isWaiting_times;
                    }
                }
                else if (!everyoneIsReady)
                {
                    controlMouse.MouseClickToChooseShipFromList(e, ourPlayer);
                    controlMouse.MouseClickToSetUpTheShip(e, ourPlayer);
                }
            }
        }

        public void KeyPressed(KeyEventArgs e) 
        {
            if (canDoAnything)
            {
                if (endGame)
                {
                    if (controlKeyboard.isRestart(e)) RestartGame();
                }
                else
                if (everyoneIsReady)
                {
                    if (controlKeyboard.keyPressedInGameMode(e, ourPlayer))
                    {
                        ourPlayer.isTurn = false;
                        isWaiting = max_isWaiting_times;
                    }
                }
                else
                {
                    controlKeyboard.keyPressedInPrepareMode(e, ourPlayer);
                }
            }      
        }
    }
}
