using Newtonsoft.Json;
using Sea_Battle.Classes;
using Sea_Battle.Classes.Server_Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class Form1 : Form
    {
        private Graphics g;

        public Form1() => InitializeComponent();

        private IGameplay gameplay;

        private void Form1_Load(object sender, EventArgs e) => timer.Start();

        private void button1_Click(object sender, EventArgs e) // for online
        {
            onlineGame_init__();
            init__();

            var isServer = checkBox1.Checked;
            IServer server;
            if (isServer)
                server = new Server();
            else
                server = new Client();
            if (server != null) gameplay = new Gameplay_Online(server, isServer);
        }

        private void button2_Click(object sender, EventArgs e) // for offline
        {
            init__();
            gameplay = new Gameplay_Offline();
        }

        private void onlineGame_init__()
        {
            ServerParameters.IP = textBox_ip.Text;
            int.TryParse(textBox_port.Text, out ServerParameters.PORT);
            server_update_timer.Start();
        }

        private void init__()
        {
            Controls.Remove(button1);
            Controls.Remove(button2);
            Controls.Remove(checkBox1);
            Controls.Remove(textBox_ip);
            Controls.Remove(textBox_port);
        }

        // listeners
        private void timer_Tick(object sender, EventArgs e)
        {
            pictureGraphics.Image = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(pictureGraphics.Image);
            gameplay?.Update(g);

            pictureGraphics.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) => gameplay?.KeyPressed(e);

        private void pictureGraphics_MouseClick(object sender, MouseEventArgs e) => gameplay?.MouseClick(e);

        private void pictureGraphics_MouseMove(object sender, MouseEventArgs e) => gameplay?.MouseMoving(e);

        private void server_update_timer_Tick(object sender, EventArgs e)
        {
            if (gameplay is Gameplay_Online) (gameplay as Gameplay_Online).updateServerTick();
        }
    }
}
