using Sea_Battle.Classes;
using System.Collections.Generic;
using System.Drawing;

using static Sea_Battle.Design.DrawParameters;

namespace Sea_Battle.Design
{
    class PlayersDraw
    {

        public void updatePrepare(Graphics g, Player player)
        {
            var tableDraw = new TableDraw(player.tableCoordinates, prepareMode: true);
            var elementsDraw = new ElementsDraw(player.listCoordinates);

            var field = player.getField();

            tableDraw.DrawTable(g, field);
            tableDraw.DrawShips(g, player.GetAllShips());
            tableDraw.DrawShip(g, player.GetActiveShip(), true);
            tableDraw.DrawInfo(g, player.name, player.scores);

            elementsDraw.updateListOfShips(g, player.leftShips);
            elementsDraw.updateActiveShipInList(g, player.GetActiveShip(), player.GetActiveShipPosition());

        }

        public void updateGame(Graphics g, List<Player> players, Player activePlayer)
        {
            foreach (Player player in players)
            {
                updateGameForPlayer(g, player);
            }

            var tableDraw = new TableDraw(activePlayer.tableCoordinates);
            tableDraw.DrawActiveCell(g, activePlayer.GetActiveCell());
        }

        public void updateGameForPlayer(Graphics g, Player player)
        {
            var tableDraw = new TableDraw(player.tableCoordinates);

            tableDraw.DrawTable(g, player.getField());
            tableDraw.DrawDestroyedShips(g, player.GetDestroyedShips());
            tableDraw.DrawInfo(g, player.name, player.scores);
        }

        public void updateWinSituation(Graphics g, Player winner)
        {
            var text = $"Winner is {winner.name}\n('space' to restart)";

            g.DrawString(text, textFont, textColor, ControlParameters.winnerTextLocation);
        }

    }
}
