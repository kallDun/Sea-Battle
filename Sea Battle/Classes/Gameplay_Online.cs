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

        private List<Player> players = new List<Player> { };
        private PlayersDraw players_drawing = new PlayersDraw();
        private Player winner = null;

        private bool everyoneIsReady = false;
        private bool endGame = false;

        private Player ourPlayer;
        private IServer server;
        private bool canDoAnything = true;

        public Gameplay_Online(IServer server, bool isTurn)
        {
            this.server = server;

            ourPlayer = new Player(
                $"Player {new Random().Next(1, 1000)}", 
                player1_TableCoordinates, 
                player1_ListCoordinates);
            ourPlayer.isTurn = isTurn;
            
            players.Add(ourPlayer);

            server.SendData(ourPlayer);
            players.Add(server.GetData());
        }
        
        public void Update(Graphics g)
        {
            UpdateStatus(g);

            if (everyoneIsReady && !endGame)
            {
                foreach (var player in players) // check if anyone won
                {
                    if (player.CheckIsLose())
                    {
                        winner = GetOtherPlayer(player);
                        winner.AddScore();
                        endGame = true;
                    }
                }
                canDoAnything = !players[1].isTurn;
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
                players_drawing.updateGame(g, players, ourPlayer);
                return;
            }
            else
            if (!ourPlayer.IsReady())
                players_drawing.updatePrepare(g, ourPlayer);
            else
            {
                server.SendData(ourPlayer);
                players[1] = server.GetData();

                if (players[1].IsReady())
                {
                    everyoneIsReady = true;
                    players[0].PrepareForGame();
                    players[1] = server.GetData();
                }
            }
        }

        public void updateServerTick()
        {
            server.SendData(ourPlayer);
            if (players.Count > 1) players[1] = server.GetData();
        }

        public Player GetOtherPlayer(Player player)
        {
            if (players.IndexOf(player) + 1 < players.Count())
            {
                return players[players.IndexOf(player) + 1];
            }
            else
            {
                return players[0];
            }
        }

        public void RestartGame()
        {
            ourPlayer = players.First();
            ourPlayer.restartPlayer();
            server.SendData(ourPlayer);
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
