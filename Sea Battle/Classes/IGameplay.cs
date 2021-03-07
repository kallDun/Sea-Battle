using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle.Classes
{
    interface IGameplay
    {
        void Update(Graphics g);

        void UpdateStatus(Graphics g);

        Player GetOtherPlayer(Player player);

        void RestartGame();

        void MouseMoving(MouseEventArgs e);

        void MouseClick(MouseEventArgs e);

        void KeyPressed(KeyEventArgs e);
    }
}
