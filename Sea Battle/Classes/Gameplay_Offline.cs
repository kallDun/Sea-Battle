using Sea_Battle.Classes.Control;
using Sea_Battle.Design;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Sea_Battle.Classes.ControlParameters;

namespace Sea_Battle.Classes
{
    class Gameplay_Offline : IGameplay
    {
        private ControlKeyboard controlKeyboard = new ControlKeyboard();
        private ControlMouse controlMouse = new ControlMouse();

        private List<Player> players = new List<Player> { };
        private PlayersDraw players_drawing = new PlayersDraw();
        private Player winner = null;

        private bool everyoneIsReady = false;
        private bool endGame = false;
        private Player activePlayer;

        public Gameplay_Offline()
        {
            players.Add(new Player("Player 1", player1_TableCoordinates, player1_ListCoordinates));
            players.Add(new Player("Player 2", player2_TableCoordinates, player2_ListCoordinates));
            activePlayer = players.First();
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
                players_drawing.updateGame(g, players, activePlayer);
                return;
            }

            if (!activePlayer.IsReady())
                players_drawing.updatePrepare(g, activePlayer);
            else
            {
                if (players.IndexOf(activePlayer) + 1 < players.Count())
                {
                    activePlayer = players[players.IndexOf(activePlayer) + 1];
                }
                else 
                {
                    everyoneIsReady = true;

                    foreach (var player in players)
                    {
                        player.PrepareForGame();
                    }
                }
            }
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
            activePlayer = players.First();
            foreach (var player in players)
            {
                player.restartPlayer();
            }
            endGame = false;
            winner = null;
            everyoneIsReady = false;
        }

        public void MouseMoving(MouseEventArgs e)
        {
            if (everyoneIsReady && !endGame) controlMouse.MouseMovingToChangePlaceToShoot(e, activePlayer);
            else if (!everyoneIsReady) controlMouse.MouseMoveToChoosePosition(e, activePlayer);
        }

        public void MouseClick(MouseEventArgs e)
        {
            if (everyoneIsReady && !endGame)
            {
                if (controlMouse.MouseClickToShoot(e, activePlayer))
                {
                    activePlayer = GetOtherPlayer(activePlayer);
                }
            }  
            else if (!everyoneIsReady)
            {
                controlMouse.MouseClickToChooseShipFromList(e, activePlayer);
                controlMouse.MouseClickToSetUpTheShip(e, activePlayer);
            }
        }

        public void KeyPressed(KeyEventArgs e) 
        {
            if (endGame)
            {
                if (controlKeyboard.isRestart(e)) RestartGame();
            }
            else
            if (everyoneIsReady)
            {
                if (controlKeyboard.keyPressedInGameMode(e, activePlayer))
                    activePlayer = GetOtherPlayer(activePlayer);
            }
            else
            {
                controlKeyboard.keyPressedInPrepareMode(e, activePlayer);
            }                
        }
    }
}
