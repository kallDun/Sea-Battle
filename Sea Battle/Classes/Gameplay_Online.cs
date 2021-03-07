using Sea_Battle.Classes.Control;
using Sea_Battle.Classes.Server_Client;
using Sea_Battle.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Sea_Battle.Classes.ControlParameters;

namespace Sea_Battle.Classes
{
    class Gameplay_Online : IGameplay
    {
        private ControlKeyboard controlKeyboard = new ControlKeyboard();
        private ControlMouse controlMouse = new ControlMouse();

        private PlayersDraw players_drawing = new PlayersDraw();
        private Player winner = null;

        private bool everyoneIsReady = false;
        private bool endGame = false;

        private Player ourPlayer;
        private Player otherPlayer;
        private IServer server;
        private bool canDoAnything = true;

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
                foreach (var player in new List<Player> { ourPlayer, otherPlayer }) // check if anyone won
                {
                    if (player.CheckIsLose())
                    {
                        winner = GetOtherPlayer(player);
                        winner.AddScore();
                        endGame = true;
                    }
                }
                canDoAnything = !otherPlayer.isTurn;
            }
            else if (endGame)
            {
                players_drawing.updateWinSituation(g, winner);
            }
        }

        public void UpdateStatus(Graphics g)
        {
            if (everyoneIsReady)
            {
                players_drawing.updateGame(g, new List<Player> { ourPlayer, otherPlayer }, ourPlayer);
                return;
            }
            else
            if (ourPlayer.IsReady() && otherPlayer.IsReady())
            {
                everyoneIsReady = true;
                ourPlayer.PrepareForGame();
                otherPlayer.PrepareForGame();
            }
            else 
                players_drawing.updatePrepare(g, ourPlayer);
        }

        public void updateServerTick()
        {
            server.SendData(ourPlayer);
            otherPlayer = server.GetData();
        }

        public Player GetOtherPlayer(Player player)
        {
            if (player == ourPlayer) return otherPlayer;
            else return ourPlayer;
        }

        public void RestartGame()
        {
            ourPlayer.restartPlayer();
            endGame = false;
            winner = null;
            everyoneIsReady = false;
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
                        ourPlayer.isTurn = !ourPlayer.isTurn;
                        server.SendData(ourPlayer);
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
                        ourPlayer = GetOtherPlayer(ourPlayer);
                }
                else
                {
                    controlKeyboard.keyPressedInPrepareMode(e, ourPlayer);
                }
            }      
        }
    }
}
