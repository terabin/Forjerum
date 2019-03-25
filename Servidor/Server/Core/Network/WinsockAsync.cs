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

namespace __Forjerum
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
        // s do cliente
        public int clientid { get; set; }
        public StateObject(Socket t, int i) { Async = t; clientid = i; }
        // Informações sobre status do jogador na conexão
        public bool IsConnected { get; set; }
        public int s { get; set; }
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
        //public void send(Socket sck, string message, Encoding encoding);
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
            if (Extensions.ExtensionApp.extendMyApp
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
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, eventType) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, eventType));
            }

            //CÓDIGO
            if (eventType == 2)
            {
                for (int i = 0; i <= WinsockAsync.Clients.Count; i++)
                {
                    WinsockAsync.disconnectUser(i);
                }
            }
            return false;
        }
        //*********************************************************************************************
        // isNumeric / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool isNumeric(string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, data) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
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
            if (Extensions.ExtensionApp.extendMyApp
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
            if (Extensions.ExtensionApp.extendMyApp
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
            
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 0; i < 100; i++)
            {
                if (UserConnection.checkS(i))
                {
                    Clients[(Clients.Count() - 1)].s = i;
                    break;
                }
            }

            //Ele não está logado, pois se conectou agora
            PlayerStruct.tempplayer[WinsockAsync.Clients[(WinsockAsync.Clients.Count() - 1)].s].Logged = false;

            //Ele não está no jogo pois se conectou agora
            PlayerStruct.tempplayer[WinsockAsync.Clients[(WinsockAsync.Clients.Count() - 1)].s].ingame = false;

            //Zerar o Player_Highs pra evitar problemas
            Globals.Player_Highs = 0;


            //Vamos atualizar o Player_Highs sem frescura
            for (int i = 0; i < WinsockAsync.Clients.Count(); i++)
            {
                if (WinsockAsync.Clients[i].s > Globals.Player_Highs)
                {
                    Globals.Player_Highs = WinsockAsync.Clients[i].s;
                }
            }

            //Vamos atualizar o Player_Highs para todos os jogadores
            SendData.sendUpdatePlayerHighs();

            //WinsockAnsyc.Clients[Clients.Count() - 1].Lists = Clients.Count() - 1;
            Log(String.Format("Cliente conectado: {0}", Clients.Count() - 1));

            // Cria o objeto de conexão do jogador
            Clients[Clients.Count - 1].Async.BeginReceive(Clients[Clients.Count - 1].buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), Clients[Clients.Count - 1]);
        }

        // FIONREAD é equivalente a informação disponível.
        public const int FIONREAD = 0x4004667F;

        //*********************************************************************************************
        // getPendingByteCount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getPendingByteCount(Socket s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
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
            if (Extensions.ExtensionApp.extendMyApp
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
            if (state.s < 0) { return; }

            int clientid = UserConnection.getS(state.s);

            //Se ultrapassar os limites, fechar conexão.
            if ((clientid < 0) || (clientid > Clients.Count - 1))
            {
                return;
            }

            int s = Clients[clientid].s;
            // Clients[Clients.Count - 1].Ansyc.EndReceive(ar)

            //Obtenção da informação
            int buffersize = getPendingByteCount(handler);

            if (buffersize > 1000)
            {
                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 1)
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
                    Extensions.ExtensionApp.extendMyApp(s, content);

                    //Enviamos os dados para o SelectPacket
                    ReceiveData.selectPacket(s, content);

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
                Database.Handler.logError(e);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(lang.player_critical_error);
                Database.Handler.logError(e);
                return;
            }
        }
        //*********************************************************************************************
        // Send / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Enviar informações.
        //*********************************************************************************************
        public static void send(int clientid, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
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
                        new AsyncCallback(sendCallback), Clients[clientid].Async);
                }
            }
            catch
            {
                //É possível que o jogador perca a conexão durante o processo da Thread original,
                //nesse caso, ele será desconectado em breve, não temos que nos preocupar com isso.
                return;
            }
        }
        //*********************************************************************************************
        // SendCallBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCallback(IAsyncResult ar)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
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
            catch
            {
                //O sendCallback pode criar exceções quando uma informação é enviada e não consegue
                //alcançar o jogador, ou caso ele tenha sido desconectado no processo, de toda forma não
                //podemos identificar todas as Threads que essa conexão criou no intervalo em que foi
                //fechada, então simplesmente deixamos as exceções serem ignoradas até que o jogador
                //seja desconectado pelo AlphaLoop.
                return;
            }
        }
        //*********************************************************************************************
        // ShutdownUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void shutdownUser(int clientid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
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
        public static void disconnectUser(int clientid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid) != null)
            {
                return;
            }

            //CÓDIGO
            //LOL
            if ((Clients.Count <= 0) || (clientid > Clients.Count - 1)) { return; }
            //Se estiver morto, resetar posição
            //PlayerStruct.tempplayer[Clients[clientid].s].isDead = false;

            //Sai da troca
            if (PlayerStruct.tempplayer[Clients[clientid].s].InTrade > 0)
            {
                TradeRelations.giveTrade(Clients[clientid].s);
                TradeRelations.giveTrade(PlayerStruct.tempplayer[Clients[clientid].s].InTrade);

                //Verificar se o jogador não se desconectou no processo
                if (Clients[(UserConnection.getS(PlayerStruct.tempplayer[Clients[clientid].s].InTrade))].IsConnected)
                {
                    SendData.sendPlayerG(PlayerStruct.tempplayer[Clients[clientid].s].InTrade);
                    SendData.sendTradeClose(PlayerStruct.tempplayer[Clients[clientid].s].InTrade);
                    SendData.sendInvSlots(PlayerStruct.tempplayer[Clients[clientid].s].InTrade, PlayerStruct.player[PlayerStruct.tempplayer[Clients[clientid].s].InTrade].SelectedChar);
                }

                TradeRelations.clearTempTrade(PlayerStruct.tempplayer[Clients[clientid].s].InTrade);
                TradeRelations.clearTempTrade(Clients[clientid].s);
            }

            //Sai do Craft
            if (PlayerStruct.tempplayer[Clients[clientid].s].InCraft)
            {
                for (int i = 1; i < Globals.Max_Craft; i++)
                {
                    if (PlayerStruct.craft[Clients[clientid].s, i].num > 0)
                    {
                        InventoryRelations.giveItem(Clients[clientid].s, PlayerStruct.craft[Clients[clientid].s, i].type, PlayerStruct.craft[Clients[clientid].s, i].num, PlayerStruct.craft[Clients[clientid].s, i].value, PlayerStruct.craft[Clients[clientid].s, i].refin, PlayerStruct.craft[Clients[clientid].s, i].exp);
                    }
                }
            }

            //Salva o jogador SE PRECISAR
            if (PlayerStruct.tempplayer[Clients[clientid].s].ingame)
            {
                Database.Characters.saveCharacter(Clients[clientid].s, PlayerStruct.player[Clients[clientid].s].Email, PlayerStruct.player[Clients[clientid].s].SelectedChar);
                Database.Banks.saveBank(Clients[clientid].s);
                Database.FriendLists.saveFriendList(Clients[clientid].s);
            }

            //Sai do grupo
            if (PlayerStruct.tempplayer[Clients[clientid].s].Party > 0)
            {
                PartyRelations.kickParty(Clients[clientid].s, Clients[clientid].s, true);
            }

            //Vamos avisar ao mapa que o jogador saiu
            SendData.sendPlayerLeft(PlayerStruct.character[Clients[clientid].s, PlayerStruct.player[Clients[clientid].s].SelectedChar].Map, Clients[clientid].s);

            //Apagamos o banco
            Database.Banks.clearBank(Clients[clientid].s);

            Console.WriteLine(lang.player_cleared + " " + clientid + Clients[clientid].s);

            //Fecha a conexão
            Clients[clientid].Async.Close();

            //Limpa dados sobre o jogador
            Clients[clientid].Async = null;

            //Limpa dados temporários sobre o jogador
            PlayerStruct.clearPlayer(Clients[clientid].s, true);
            PlayerStruct.clearTempPlayer(Clients[clientid].s);

            //Limpa informações gerais da conexão
            Clients[clientid].s = -1;

            //Remova da lista de clientes do servidor
            Clients.RemoveAt(clientid);

            //Zerar o Player_Highs pra evitar problemas
            Globals.Player_Highs = 0;

            //Vamos atualizar o Player_Highs sem frescura
            for (int i = 0; i < WinsockAsync.Clients.Count(); i++)
            {
                if (Clients[i].s > Globals.Player_Highs)
                {
                    Globals.Player_Highs = Clients[i].s;
                }
            }

            //Vamos atualizar o Player_Highs para todos os jogadores
            SendData.sendUpdatePlayerHighs();

            Log(lang.player_disconnected + " " + clientid);
        }
        //*********************************************************************************************
        // LoginAnswer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static string LoginAnswer(string[] data, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, data, s) != null)
            {
                return Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, data, s).ToString();
            }

            //CÓDIGO
            string password = data[1];
            Console.WriteLine(lang.login + " " + data[0]);
            Console.WriteLine(lang.password + " " + password);
            if(Database.Accounts.tryLogin(s, data[0], password))
            {

                if (PlayerLogic.isPlayerConnected(data[0]) == true)
                {
                    return "c";
                }
                else
                {
                    PlayerStruct.player[s].Email = data[0];
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
            if (Extensions.ExtensionApp.extendMyApp
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


