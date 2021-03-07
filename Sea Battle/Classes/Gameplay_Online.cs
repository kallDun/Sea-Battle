using Sea_Battle.Classes.Control;
using Sea_Battle.Classes.Server_Client;
using Sea_Battle.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
        private bool everyoneIsReady = false;
        private bool endGame = false;
        private bool canDoAnything = true;
        private bool isWaiting = false;
        private int timesToCheckLosers = 3;

        private Player ourPlayer;
        private Player otherPlayer;
        private IServer server;

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
                if (timesToCheckLosers == 0)
                {
                    if (ourPlayer.CheckIsLose()) winner = otherPlayer;
                    else if (otherPlayer.CheckIsLose()) winner = ourPlayer;

                    if (winner != null)
                    {
                        winner.AddScore();
                        endGame = true;
                    }
                }
                else timesToCheckLosers--;
            }
            else if (endGame)
            {
                players_drawing.updateWinSituation(g, winner);
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
                new TableDraw(other.tableCoordinates).DrawActiveCell(g, other.GetActiveCell());
                return;
            }
            else
            if (ourPlayer.IsReady() && otherPlayer.IsReady())
            {
                server_update_timer.Stop();
                server.SendData(ourPlayer);
                ourPlayer.ChangeField(otherPlayer.field);
                ourPlayer.ChangeName(otherPlayer.name);
                Thread.Sleep(1000);
                server_update_timer.Start();

                ourPlayer.PrepareForGame();
                everyoneIsReady = true;
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
                if (!otherPlayer.isTurn && !ourPlayer.isTurn && !isWaiting) ourPlayer.isTurn = true;
                else isWaiting = false;

                canDoAnything = ourPlayer.isTurn;
                ourPlayer.field.updateDestroyedShips();
            }
        }

        public void RestartGame()
        {
            ourPlayer.restartPlayer();
            endGame = false;
            winner = null;
            everyoneIsReady = false;
            timesToCheckLosers = 3;
        }

        public void MouseMoving(MouseEventArgs e)
        {
            if (everyoneIsReady && !endGame) controlMouse.MouseMovingToChangePlaceToShoot(e, ourPlayer);
            else if (!everyoneIsReady) controlMouse.MouseMoveToChoosePosition(e, ourPlayer);
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
                        isWaiting = true;
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
                        isWaiting = true;
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
