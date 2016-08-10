using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Métodos de gerenciamento e obtênção da conexão relacionada ao Winsock e ao servidor.
    // UserConnection.cs
    //*********************************************************************************************
    class UserConnection
    {
        //*********************************************************************************************
        // isConnected / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna se determinada conexão ainda se mantêm ativa.
        //*********************************************************************************************
        public static bool isConnected(int clientid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, clientid));
            }

            //CÓDIGO
            try
            {
                bool Connected;
                Connected = true;

                // Detectar se o cliente está desconectado
                if (WinsockAsync.Clients[clientid].Async.Poll(1000, SelectMode.SelectRead))
                {
                    byte[] buff = new byte[1];
                    if (WinsockAsync.Clients[clientid].Async.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // Cliente desconectado
                        Connected = false;
                    }
                }

                return Connected;
            }
            catch { return false; }

        }
        //*********************************************************************************************
        // CheckIndex / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado valor de index está livre.
        //*********************************************************************************************
        public static bool Checkindex(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index));
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                if (WinsockAsync.Clients[i].index == index)
                {
                    return false;
                }
            }
            return true;
        }
        //*********************************************************************************************
        // GetIndex / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna o index do jogador baseado no servidor, e não na conexão.
        //*********************************************************************************************
        public static int Getindex(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return Convert.ToInt32
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index));
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                if (WinsockAsync.Clients[i].index == index)
                {
                    if ((i < 0) || (i >= WinsockAsync.Clients.Count())) { return -1; }
                    return i;
                }
            }
            return -1;
        }
    }
}
  