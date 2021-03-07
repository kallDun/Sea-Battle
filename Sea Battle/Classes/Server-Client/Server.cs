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
            try
            {
                Player obj = JsonConvert.DeserializeObject<Player>(str.ToString());
                return obj;
            }
            catch (Exception) 
            {
                return null;
            }

            // save in file to check
            //File.WriteAllText(Environment.CurrentDirectory + @"\File.json", JsonConvert.SerializeObject(obj));
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
