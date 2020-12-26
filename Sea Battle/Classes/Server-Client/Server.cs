using System.Net.Sockets;
using static Sea_Battle.Classes.Server_Client.ServerParameters;
using static Sea_Battle.Classes.ControlParameters;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace Sea_Battle.Classes.Server_Client
{
    class Server
    {
        private bool isEveryoneReady = false;
        private bool isGameEnd = false;

        private Player player;
        private bool isGameRestart = false;
        private bool isReady = false;
        private bool isLoose = false;
        private bool isBet = true;

        private UdpClient client;

        public Server()
        {
            client = new UdpClient(PORT);
            player = new Player("Player 1", player1_TableCoordinates, player1_ListCoordinates);
        }



        public void Update(Graphics g)
        {
            
            ReceiveBool();


            


        }

        private async void ReceiveBool()
        {
            if (!isEveryoneReady && !isGameEnd)
            {
                var data = await client.ReceiveAsync();

                using (var ms = new MemoryStream(data.Buffer))
                {
                    if (bool.TryParse(ms.ToString(), out bool isOtherPlayerReady))
                        isEveryoneReady = isReady && isOtherPlayerReady;
                }
            } 
            else
            if (isEveryoneReady && !isGameEnd)
            {
                var data = await client.ReceiveAsync();

                using (var ms = new MemoryStream(data.Buffer))
                {
                    if (bool.TryParse(ms.ToString(), out bool isOtherPlayerLoose))
                    {
                        isGameEnd = isOtherPlayerLoose;
                        // add one point
                        // send points to other player
                    }   
                }
            }
            else
            if (isGameEnd)
            {
                var data = await client.ReceiveAsync();

                using (var ms = new MemoryStream(data.Buffer))
                {
                    if (bool.TryParse(ms.ToString(), out bool isRestart))
                    {
                        if (isRestart && isGameRestart)
                        {
                            // restart
                        }
                    }
                }
            }
        }

    }
}
