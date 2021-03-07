using System.Net.Sockets;
using static Sea_Battle.Classes.Server_Client.ServerParameters;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System;

namespace Sea_Battle.Classes.Server_Client
{
    class Client : IServer
    {
        Socket socket;

        public Client()
        {
            try
            {
                // create socket
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // connect to host
                socket.Connect(ipPoint);
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
                bytes = socket.Receive(data);
                str.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            // converting to json and return
            return JsonConvert.DeserializeObject<Player>(str.ToString());
        }

        public void SendData(Player player)
        {
            // give response
            string message = JsonConvert.SerializeObject(player);
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
        }
    }
}
