using Sea_Battle.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private Gameplay gameplay = new Gameplay();

        private void Form1_Load(object sender, EventArgs e) => timer.Start();

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureGraphics.Image = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(pictureGraphics.Image);

            gameplay.update(g);

            pictureGraphics.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) => gameplay.keyPressed(e);

        private void pictureGraphics_MouseClick(object sender, MouseEventArgs e) => gameplay.MouseClick(e);

        private void pictureGraphics_MouseMove(object sender, MouseEventArgs e) => gameplay.MouseMoving(e);
    }
}
