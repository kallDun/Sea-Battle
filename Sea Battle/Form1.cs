using Newtonsoft.Json;
using Sea_Battle.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class Form1 : Form
    {
        private Graphics g;

        public Form1() => InitializeComponent();

        private Gameplay gameplay = new Gameplay();

        private void Form1_Load(object sender, EventArgs e) => timer.Start();

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureGraphics.Image = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(pictureGraphics.Image);
            gameplay.Update(g);

            pictureGraphics.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) => gameplay.KeyPressed(e);

        private void pictureGraphics_MouseClick(object sender, MouseEventArgs e) => gameplay.MouseClick(e);

        private void pictureGraphics_MouseMove(object sender, MouseEventArgs e) => gameplay.MouseMoving(e);
    }
}
