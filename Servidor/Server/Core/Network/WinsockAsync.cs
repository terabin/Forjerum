using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Objeto com caractéristicas que serão usadas pelo Winsock.
    // class StateObject
    //*********************************************************************************************
    public class StateObject
    {
        // Socket do cliente.
        public Socket Async {get ; set;}
        // Tamanho do receive buffer.
        public const int BufferSize = 1202048;
        // Receive buffer. !importante
        public byte[] buffer = new byte[BufferSize];
        // Para receber strings e processar.
        public StringBuilder sb = new StringBuilder();
        // index do cliente
        public int clientid { get; set; }
        public StateObject(Socket t, int i) { Async = t; clientid = i; }
        // Informações sobre status do jogador na conexão
        public bool IsConnected { get; set; }
        public int index { get; set; }
    }
    //*********************************************************************************************
    // Classe principal de conexão assíncrona.
    // WinsockAsync.cs
    //*********************************************************************************************
    class WinsockAsync : Languages.LStruct
    {
        // Conexão básica do servidor
        public static Socket listener;
        public static List<StateObject> Clients;
        // Thread 
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        // Mensagem simples
        public static string Motd = "Bem vindo a " + Globals.GAME_NAME + "!";
        // Obsoleto agora
        public static string[] connected = new string[] { };
        //public void Send(Socket sck, string message, Encoding encoding);
        static ConsoleEventDelegate event_handler; 
        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        //*********************************************************************************************
        // start / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Inicia a conexão e a interação com o loop.
        //*********************************************************************************************
        public static void start()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Lidar com fechamento
            event_handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(event_handler, true);

            //Clientes
            Clients = new List<StateObject>(100);

            // Iniciar uma nova thread, isso não vai iniciar ela ainda.
            Thread sThread = new Thread(new ThreadStart(Loops.BetaLoop));

            // Iniciar a Thread
            sThread.Start();

            //Iniciar conexão
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Estabelece o local de fuga para a socket
            // Obter o nome de DNS do servidor em seguida
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;//ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8000);

            // Cria uma socket TCP/IP.
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);
            listener.NoDelay = true;

            // Cria a Thread principal.
            Thread lThread = new Thread(new ThreadStart(Listen));

            // Iniciar a Thread principal
            lThread.Start();

            //Loop principal
            Loops.AlphaLoop();
        }
        //*********************************************************************************************
        // ConsoleEventCallBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Desconectar usuários antes de fechar o servidor.
        //*********************************************************************************************
        static bool ConsoleEventCallback(int eventType)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, eventType) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, eventType));
            }

            //CÓDIGO
            if (eventType == 2)
            {
                for (int i = 0; i <= WinsockAsync.Clients.Count; i++)
                {
                    WinsockAsync.DisconnectUser(i);
                }
            }
            return false;
        }
        //*********************************************************************************************
        // IsNumeric / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool IsNumeric(string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data));
            }

            //CÓDIGO
            int v;
            if (Int32.TryParse(data.Trim(), out v))
            {
                return true;
            }
            return false;
        }
        //*********************************************************************************************
        // Listen / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Listen()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return; 
            }

            //CÓDIGO
            try
            {
                while (true)
                {
                    // Seta o evento para o status neutro
                    allDone.Reset();

                    // Inicia um listener assíncrono para ouvir as conexões.
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Esperar uma conexão surgir para seguir.
                    allDone.WaitOne();

                    Thread.Sleep(10);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        //*********************************************************************************************
        // AcceptCallBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Conexão aceita.
        //*********************************************************************************************
        public static void AcceptCallback(IAsyncResult ar)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, ar) != null)
            {
                return;
            }

            //CÓDIGO
            //Temporizador
            Loops.Accept_Timer = Loops.TickCount.ElapsedMilliseconds;

            // Signal the main thread to continue.
            allDone.Set();

            // Socket \o/.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            //Adicionamos ele na lista
            Clients.Add(new StateObject(handler, Clients.Count - 1));
            Clients[Clients.Count - 1].Async.NoDelay = true;
            Clients[Clients.Count - 1].IsConnected = true;
            
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 0; i < 100; i++)
            {
                if (UserConnection.Checkindex(i))
                {
                    Clients[(Clients.Count() - 1)].index = i;
                    break;
                }
            }

            //Ele não está logado, pois se conectou agora
            PStruct.tempplayer[WinsockAsync.Clients[(WinsockAsync.Clients.Count() - 1)].index].Logged = false;

            //Ele não está no jogo pois se conectou agora
            PStruct.tempplayer[WinsockAsync.Clients[(WinsockAsync.Clients.Count() - 1)].index].ingame = false;

            //Zerar o Player_Highindex pra evitar problemas
            Globals.Player_Highindex = 0;


            //Vamos atualizar o Player_Highindex sem frescura
            for (int i = 0; i < WinsockAsync.Clients.Count(); i++)
            {
                if (WinsockAsync.Clients[i].index > Globals.Player_Highindex)
                {
                    Globals.Player_Highindex = WinsockAsync.Clients[i].index;
                }
            }

            //Vamos atualizar o Player_Highindex para todos os jogadores
            SendData.Send_UpdatePlayerHighindex();

            //WinsockAnsyc.Clients[Clients.Count() - 1].Listindex = Clients.Count() - 1;
            Log(String.Format("Cliente conectado: {0}", Clients.Count() - 1));

            // Cria o objeto de conexão do jogador
            Clients[Clients.Count - 1].Async.BeginReceive(Clients[Clients.Count - 1].buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), Clients[Clients.Count - 1]);
        }

        // FIONREAD é equivalente a informação disponível.
        public const int FIONREAD = 0x4004667F;

        //*********************************************************************************************
        // GetPendingByteCount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetPendingByteCount(Socket s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            try
            {
                byte[] outValue = BitConverter.GetBytes(0);

                // Checa quantos bytes foram recebidos
                s.IOControl(FIONREAD, null, outValue);

                int bytesAvailable = BitConverter.ToInt32(outValue, 0);
                //Console.WriteLine("server has {0} bytes pending. Available property says {1}.",
                //  bytesAvailable, s.Available);

                return bytesAvailable;
            }
            catch
            {
                return 0;
            }
        }
        //*********************************************************************************************
        // ReadCallBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Ler informações recebidas.
        //*********************************************************************************************
        public static void ReadCallback(IAsyncResult ar)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, ar) != null)
            {
                return;
            }

            //CÓDIGO
            String content = String.Empty;
            // state = clients[i].
            // handler = clients[i].async
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.Async;


            if (handler == null) { return; }; if (!handler.Connected) { return; }
            if (state.index < 0) { return; }

            int clientid = UserConnection.Getindex(state.index);

            //Se ultrapassar os limites, fechar conexão.
            if ((clientid < 0) || (clientid > Clients.Count - 1))
            {
                return;
            }

            int index = Clients[clientid].index;
            // Clients[Clients.Count - 1].Ansyc.EndReceive(ar)

            //Obtenção da informação
            int buffersize = GetPendingByteCount(handler);

            if (buffersize > 1000)
            {
                if (PStruct.character[index, PStruct.player[index].SelectedChar].Access < 1)
                {
                    Console.WriteLine(lang.player_buffer_limit_exceeded);
                    return;
                }
            }
            // Ler informações do cliente
            try
            {
                //Finalizar o recebimento para começar a ler
                int bytesRead = handler.EndReceive(ar);

                //Se tiver dados para ler.
                if (bytesRead > 0)
                {
                    //Tem dados, então vamos processa-los
                    state.sb.Append(Encoding.UTF8.GetString(
                        state.buffer, 0, bytesRead));

                    // Passamos tudo para uma váriavel ler.
                    content = state.sb.ToString();

                    // Extender APP / Cancelar Thread atual para executar a próxima?
                    Extensions.ExtensionApp.ExtendMyApp(index, content);

                    //Enviamos os dados para o SelectPacket
                    ReceiveData.SelectPacket(index, content);

                    //Limpar a stream de dados pois já foram processados.
                    state.sb.Clear();
                }

                //Voltamos a receber os dados.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10054)
                {
                    //A conexão será fechada logo pela outra thread, então apenas aguardar.
                    return;
                }
                Console.WriteLine(lang.connection_critical_error);
                Database.LogError(e);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(lang.player_critical_error);
                Database.LogError(e);
                return;
            }
        }
        //*********************************************************************************************
        // Send / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Enviar informações.
        //*********************************************************************************************
        public static void Send(int clientid, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data) != null)
            {
                return;
            }

            //CÓDIGO
            try
            {
                //Conectado?
                if (Clients[clientid].IsConnected)
                {
                    // Converte a string para byte
                    byte[] byteData = Encoding.UTF8.GetBytes(data);

                    // Envia a informação para o cliente
                    Clients[clientid].Async.BeginSend(byteData, 0, byteData.Length, 0,
                        new AsyncCallback(SendCallback), Clients[clientid].Async);
                }
            }
            catch
            {
                //NO!
                Log(String.Format(lang.forcibly_closed_connection));
            }
        }
        //*********************************************************************************************
        // SendCallBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void SendCallback(IAsyncResult ar)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, ar) != null)
            {
                return;
            }

            //CÓDIGO
            try
            {
                // Socket do cliente
                Socket handler = (Socket)ar.AsyncState;

                // Completa o envio
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //*********************************************************************************************
        // ShutdownUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ShutdownUser(int clientid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid) != null)
            {
                return;
            }

            //CÓDIGO
            if ((Clients.Count <= 0) || (clientid > Clients.Count - 1)) { return; }
            //Consequentemente deve ser desconectado.
            Clients[clientid].Async.Shutdown(SocketShutdown.Both);
        }
        //*********************************************************************************************
        // DisconnectUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Desconectar o jogador.
        //*********************************************************************************************
        public static void DisconnectUser(int clientid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid) != null)
            {
                return;
            }

            //CÓDIGO
            //LOL
            if ((Clients.Count <= 0) || (clientid > Clients.Count - 1)) { return; }
            //Se estiver morto, resetar posição
            //PStruct.tempplayer[Clients[clientid].index].isDead = false;

            //Sai da troca
            if (PStruct.tempplayer[Clients[clientid].index].InTrade > 0)
            {
                PStruct.GiveTrade(Clients[clientid].index);
                PStruct.GiveTrade(PStruct.tempplayer[Clients[clientid].index].InTrade);

                //Verificar se o jogador não se desconectou no processo
                if (Clients[(UserConnection.Getindex(PStruct.tempplayer[Clients[clientid].index].InTrade))].IsConnected)
                {
                    SendData.Send_PlayerG(PStruct.tempplayer[Clients[clientid].index].InTrade);
                    SendData.Send_TradeClose(PStruct.tempplayer[Clients[clientid].index].InTrade);
                    SendData.Send_InvSlots(PStruct.tempplayer[Clients[clientid].index].InTrade, PStruct.player[PStruct.tempplayer[Clients[clientid].index].InTrade].SelectedChar);
                }

                PStruct.ClearTempTrade(PStruct.tempplayer[Clients[clientid].index].InTrade);
                PStruct.ClearTempTrade(Clients[clientid].index);
            }

            //Sai do Craft
            if (PStruct.tempplayer[Clients[clientid].index].InCraft)
            {
                for (int i = 1; i < Globals.Max_Craft; i++)
                {
                    if (PStruct.craft[Clients[clientid].index, i].num > 0)
                    {
                        PStruct.GiveItem(Clients[clientid].index, PStruct.craft[Clients[clientid].index, i].type, PStruct.craft[Clients[clientid].index, i].num, PStruct.craft[Clients[clientid].index, i].value, PStruct.craft[Clients[clientid].index, i].refin, PStruct.craft[Clients[clientid].index, i].exp);
                    }
                }
            }

            //Salva o jogador SE PRECISAR
            if (PStruct.tempplayer[Clients[clientid].index].ingame)
            {
                Database.SaveCharacter(Clients[clientid].index, PStruct.player[Clients[clientid].index].Email, PStruct.player[Clients[clientid].index].SelectedChar);
                Database.SaveBank(Clients[clientid].index);
                Database.SaveFriendList(Clients[clientid].index);
            }

            //Sai do grupo
            if (PStruct.tempplayer[Clients[clientid].index].Party > 0)
            {
                PStruct.KickParty(Clients[clientid].index, Clients[clientid].index, true);
            }

            //Vamos avisar ao mapa que o jogador saiu
            SendData.Send_PlayerLeft(PStruct.character[Clients[clientid].index, PStruct.player[Clients[clientid].index].SelectedChar].Map, Clients[clientid].index);

            //Apagamos o banco
            Database.ClearBank(Clients[clientid].index);

            Console.WriteLine(lang.player_cleared + " " + clientid + Clients[clientid].index);

            //Fecha a conexão
            Clients[clientid].Async.Close();

            //Limpa dados sobre o jogador
            Clients[clientid].Async = null;

            //Limpa dados temporários sobre o jogador
            Database.ClearPlayer(Clients[clientid].index, true);
            PStruct.ClearTempPlayer(Clients[clientid].index);

            //Limpa informações gerais da conexão
            Clients[clientid].index = -1;

            //Remova da lista de clientes do servidor
            Clients.RemoveAt(clientid);

            //Zerar o Player_Highindex pra evitar problemas
            Globals.Player_Highindex = 0;

            //Vamos atualizar o Player_Highindex sem frescura
            for (int i = 0; i < WinsockAsync.Clients.Count(); i++)
            {
                if (Clients[i].index > Globals.Player_Highindex)
                {
                    Globals.Player_Highindex = Clients[i].index;
                }
            }

            //Vamos atualizar o Player_Highindex para todos os jogadores
            SendData.Send_UpdatePlayerHighindex();

            Log(lang.player_disconnected + " " + clientid);
        }
        //*********************************************************************************************
        // LoginAnswer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static string LoginAnswer(string[] data, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data, index) != null)
            {
                return Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data, index).ToString();
            }

            //CÓDIGO
            string password = data[1];
            Console.WriteLine(lang.login + " " + data[0]);
            Console.WriteLine(lang.password + " " + password);
            if(Database.TryLogin(index, data[0], password))
            {

                if (PlayerLogic.isPlayerConnected(data[0]) == true)
                {
                    return "c";
                }
                else
                {
                    PStruct.player[index].Email = data[0];
                    Log(lang.player_authenticated + ": " + data[0] + " / " + password);
                    return "a";
                }
            }
            else { return "p"; }
        }
        //*********************************************************************************************
        // Log / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Log(string data, ConsoleColor c = ConsoleColor.Gray)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, data, c) != null)
            {
                return;
            }

            //CÓDIGO
            Console.ForegroundColor = c;
            Console.WriteLine(data);
        }

    }
        
}


