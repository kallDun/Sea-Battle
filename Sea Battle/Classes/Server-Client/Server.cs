using System.Net.Sockets;
using static Sea_Battle.Classes.Server_Client.ServerParameters;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System;
using System.IO;

namespace Sea_Battle.Classes.Server_Client
{
    class Server : IServer
    {
        private Socket handler;

        public Server()
        {
            // create socket
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // bind socket with local point
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                //Console.WriteLine("Waiting for connection...");

                handler = listenSocket.Accept();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Player GetData()
        {
            // get message
            StringBuilder str = new StringBuilder();

            int bytes = 0;
            byte[] data = new byte[256];

            do
            {
                bytes = handler.Receive(data);
                str.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (handler.Available > 0);

            // converting to json and return
            using (FileStream fs = File.Create(Environment.CurrentDirectory + @"\File.json"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(str.ToString());
                fs.Write(info, 0, info.Length);
            }
            return JsonConvert.DeserializeObject<Player>(str.ToString());
        }

        public void SendData(Player player)
        {
            // give response
            string message = JsonConvert.SerializeObject(player);
            byte[] data = Encoding.Unicode.GetBytes(message);
            handler.Send(data);
        }
    }
}
