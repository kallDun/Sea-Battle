﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle.Classes.Server_Client
{
    interface IServer
    {
        Player GetData();
        void SendData(Player player);
    }
}
