using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Relacionado a todo o gerenciamento da Database do jogo.
    // Database.cs
    //*********************************************************************************************
    class Database : Languages.LStruct
    {
        //*********************************************************************************************
        // Construtores que vão armazenas estatíticas do servidor, tais como erros e estado baseado
        // no tempo.
        //*********************************************************************************************
        static StringBuilder sbLogger = new StringBuilder ();
        static StringBuilder sbStatus = new StringBuilder ();
        //*********************************************************************************************
        // DEFINE_CLASSES_DATA / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void DEFINE_CLASSES_DATA()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return; }
 
            //CÓDIGO
            string[] temp;
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\classes_info.txt", Encoding.GetEncoding("iso-8859-1"));
            s.ReadLine();

            for (int i = 1; i <= Globals.Max_Classes; i++)
            {
                PStruct.classes[i].fire = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PStruct.classes[i].earth = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PStruct.classes[i].water = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PStruct.classes[i].wind = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PStruct.classes[i].dark = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PStruct.classes[i].light = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                temp = s.ReadLine().Split(':')[1].Split(';');
                PStruct.classes[i].sprite_name = new string[2];
                PStruct.classes[i].sprite_index = new int[2];
                PStruct.classes[i].sprite_name[0] = temp[0];
                PStruct.classes[i].sprite_index[0] = Convert.ToInt32(temp[1]);
                temp = s.ReadLine().Split(':')[1].Split(';');
                PStruct.classes[i].sprite_name[1] = temp[0];
                PStruct.classes[i].sprite_index[1] = Convert.ToInt32(temp[1]);
                s.ReadLine();
            }

            s.Close();
        }
        //*********************************************************************************************
        // GET_GAME_NAME / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm o nome do jogo
        //*********************************************************************************************
        public static string GET_GAME_NAME()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\game_name.txt", Encoding.GetEncoding("iso-8859-1"));

            string game_name = s.ReadToEnd();
            s.Close();
            return game_name;
        }
        //*********************************************************************************************
        // GET_MOTD / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm a mensagem do dia
        //*********************************************************************************************
        public static string GET_MOTD()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\motd.txt", Encoding.GetEncoding("iso-8859-1"));

            string motd = s.ReadToEnd();
            s.Close();
            return motd;
        }
        //*********************************************************************************************
        // GET_NOTICE / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm a notícia atual
        //*********************************************************************************************
        public static string GET_NOTICE()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\notice.txt", Encoding.GetEncoding("iso-8859-1"));

            string notice = s.ReadToEnd();
            s.Close();
            return notice;
        }
        //*********************************************************************************************
        // DefineAdmin / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm do admin_mail.txt o admin
        //*********************************************************************************************
        public static void DefineAdmin()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return; }
            
            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\admin_mail.txt", Encoding.GetEncoding("iso-8859-1"));

            string admin_mail = s.ReadToEnd();
            Globals.MASTER_EMAIL = admin_mail;
            Console.WriteLine(lang.admin_status_attr_to_account + ": " + admin_mail);
            s.Close();
        }
        //*********************************************************************************************
        // LogError / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LogError(Exception e)
        {
            sbLogger.AppendLine(lang.error_log + ": " + e.Message + " [" + lang.origin + ": " + e.Source + "]" + "  [" + lang.extra + ": " + e.StackTrace + "|" + e.InnerException);
        }
        //*********************************************************************************************
        // LogAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void LogAdd(string message)
        {
            sbLogger.AppendLine(lang.info_log + ": " + message);
        }
        //*********************************************************************************************
        // StatusAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void StatusAdd(string message)
        {
            sbStatus.AppendLine(message);
        }
        //*********************************************************************************************
        // DischargeWrite / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void DischargeWriter()
        {
            File.WriteAllText("./Log.txt", sbLogger.ToString());
            sbLogger.Clear();
            File.WriteAllText("./Status.txt", sbStatus.ToString());
            sbStatus.Clear();
        }
        #region Account
        //*********************************************************************************************
        // AccountExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se determinada conta existe.
        //*********************************************************************************************
        public static bool AccountExists(string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
            //A conta existe, pode deixar o cara passar poax.
            { return true; }
            else
            //Não existe, pega ele ! Pega ele !
            { return false; }
        }
        //*********************************************************************************************
        // TryLogin / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se o jogador pode se conectar no contexto atual
        //*********************************************************************************************
        public static bool TryLogin(int index, string email, string password)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, email, password) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, email, password)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
            {
           
                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Open);
            
                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê primeiros dados
                string email2 = br.ReadString();
                PStruct.player[index].Password = br.ReadString();

                //Compara os dados e manda um negativo caso estejam errados
                if (email2.ToLower() == email.ToLower()) { } else { br.Close(); return false; }
                if (PStruct.player[index].Password == password) { } else { br.Close(); return false; }

                //Termina de ler
                PStruct.player[index].Username = br.ReadString();
                PStruct.player[index].Confirmed = br.ReadBoolean();
                PStruct.player[index].Premmy = br.ReadString();
                PStruct.player[index].WPoints = br.ReadInt32();
                PStruct.player[index].Banned = br.ReadString();

                //Fecha o leitor
                br.Close();

                //Responde que está tudo certo se a comparação foi sem problemas
                return true;

            }
            else
            //Responde que o usuário não existe
            { return false; }
        }
        //*********************************************************************************************
        // AddWPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adiciona pontos(cash) diretamente na DB.
        //*********************************************************************************************
        public static bool AddWPoints(string email, int points)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, points) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, points)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê primeiros dados
                string email2 = br.ReadString();
                string password = br.ReadString();
                string username = br.ReadString();
                bool confirmed = br.ReadBoolean();
                string premmy = br.ReadString();
                int wpoints = br.ReadInt32();
                string banned = br.ReadString();

                //Data adicionada
                wpoints += points;

                //Definimos o escrivão do arquivo. hue
                BinaryWriter bw = new BinaryWriter(file);

                //Começamos do zero
                bw.Seek(0, 0);

                //Escrevemos os novos dados
                bw.Write(email2);
                bw.Write(password);
                bw.Write(username);
                bw.Write(confirmed);
                bw.Write(premmy);
                bw.Write(wpoints);
                bw.Write(banned);

                //Fechamos os tratamentos
                bw.Close();
                br.Close();

                //Responde que está tudo certo se a comparação foi sem problemas
                return true;

            }
            return false;
        }
        //*********************************************************************************************
        // AddPremmy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adiciona tempo de assinatura determinado.
        //*********************************************************************************************
        public static bool AddPremmy(string email, int days)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, days) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, days)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê primeiros dados
                string email2 = br.ReadString();
                string password = br.ReadString();
                string username = br.ReadString();
                bool confirmed = br.ReadBoolean();
                string premmy = br.ReadString();
                int wpoints = br.ReadInt32();
                string banned = br.ReadString();

                //Data adicionada
                DateTime myDate = DateTime.Parse(premmy);
                myDate = myDate.AddDays(days);
                premmy = myDate.ToString();

                //Definimos o escrivão do arquivo. hue
                BinaryWriter bw = new BinaryWriter(file);

                //Começamos do zero
                bw.Seek(0, 0);

                //Escrevemos os novos dados
                bw.Write(email2);
                bw.Write(password);
                bw.Write(username);
                bw.Write(confirmed);
                bw.Write(premmy);
                bw.Write(wpoints);
                bw.Write(banned);

                //Fechamos os tratamentos
                bw.Close();
                br.Close();

                //Responde que está tudo certo se a comparação foi sem problemas
                return true;

            }
            return false;
        }
        //*********************************************************************************************
        // AddBan / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adiciona tempo em que o jogador deve ficar afastado.
        //*********************************************************************************************
        public static bool AddBan(string email, int days)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, days) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email, days)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê primeiros dados
                string email2 = br.ReadString();
                string password = br.ReadString();
                string username = br.ReadString();
                bool confirmed = br.ReadBoolean();
                string premmy = br.ReadString();
                int wpoints = br.ReadInt32();
                string banned = br.ReadString();

                //Nova data
                DateTime myDate = DateTime.Parse(banned);
                myDate = myDate.AddDays(days);
                banned = myDate.ToString();

                //Definimos o escrivão do arquivo. hue
                BinaryWriter bw = new BinaryWriter(file);

                //Começamos do zero
                bw.Seek(0, 0);

                //Escrevemos
                bw.Write(email2);
                bw.Write(password);
                bw.Write(username);
                bw.Write(confirmed);
                bw.Write(premmy);
                bw.Write(wpoints);
                bw.Write(banned);

                //Fechamos os tratamentos
                br.Close();
                bw.Close();

                //Responde que está tudo certo se a comparação foi sem problemas
                return true;

            }
            return false;
        }
        //*********************************************************************************************
        // SaveAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva a conta de determinado jogador online.
        //*********************************************************************************************
        public static bool SaveAccount(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); }

            //CÓDIGO
            //representa o arquivo
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + PStruct.player[index].Email + ".dat", FileMode.Open);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            bw.Write(PStruct.player[index].Email);
            bw.Write(PStruct.player[index].Password);
            bw.Write(PStruct.player[index].Username);
            bw.Write(PStruct.player[index].Confirmed);
            bw.Write(PStruct.player[index].Premmy);
            bw.Write(PStruct.player[index].WPoints);
            bw.Write(PStruct.player[index].Banned);

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // RegisterNewAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Registra uma nova conta com os parâmetros enviados.
        //*********************************************************************************************
        public static bool RegisterNewAccount(int index, string username, string password, string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, username, password, email) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, username, password, email)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat") == false)
            {
                //representa o arquivo que vamos criar
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Create);

                //Definimos o escrivão do arquivo. hue
                BinaryWriter bw = new BinaryWriter(file);

                PStruct.player[index].Username = username;
                PStruct.player[index].Email = email;
                PStruct.player[index].Password = password;
                PStruct.player[index].Confirmed = false;
                PStruct.player[index].Premmy = DateTime.Now.ToString();
                PStruct.player[index].WPoints = 0;
                PStruct.player[index].Banned = DateTime.Now.ToString();


                //grava os dados no arquivo
                bw.Write(email);
                bw.Write(password);
                bw.Write(username);
                bw.Write(false);
                bw.Write(DateTime.Now.ToString());
                bw.Write(0);
                bw.Write(DateTime.Now.ToString());
                bw.Close();

                //Retorna que deu tudo certo
                return true;
            }
            else
            { return false; }
        }
        //*********************************************************************************************
        // SaveBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva o banco de determinado jogador online.
        //*********************************************************************************************
        public static bool SaveBank(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PStruct.player[index].Username + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            for (int i = 1; i < Globals.Max_BankSlots; i++)
            {
                bw.Write(PStruct.player[index].bankslot[i].type);
                bw.Write(PStruct.player[index].bankslot[i].num);
                bw.Write(PStruct.player[index].bankslot[i].value);
                bw.Write(PStruct.player[index].bankslot[i].refin);
                bw.Write(PStruct.player[index].bankslot[i].exp);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // SaveFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva a lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool SaveFriendList(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PStruct.player[index].Username + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int friendscount = PStruct.GetPlayerFriendsCount(index);

            bw.Write(friendscount);

            for (int i = 1; i <= friendscount; i++)
            {
                bw.Write(PStruct.player[index].friend[i].name);
                bw.Write(PStruct.player[index].friend[i].sprite);
                bw.Write(PStruct.player[index].friend[i].sprite_index);
                bw.Write(PStruct.player[index].friend[i].classid);
                bw.Write(PStruct.player[index].friend[i].level);
                bw.Write(PStruct.player[index].friend[i].guildname);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Apenas descarrega a estrutura do banco de determinado jogador.
        //*********************************************************************************************
        public static bool ClearBank(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); }

            //CÓDIGO
            try
            {

                for (int i = 1; i < Globals.Max_BankSlots; i++)
                {
                    PStruct.player[index].bankslot[i].type = 0;
                    PStruct.player[index].bankslot[i].num = 0;
                    PStruct.player[index].bankslot[i].value = 0;
                    PStruct.player[index].bankslot[i].refin = 0;
                }
                return true;
            }
            catch { return false; }
        }
        //*********************************************************************************************
        // ClearFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Apenas descarrega a estrutura da lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool ClearFriendList(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); }

            //CÓDIGO
            try
            {

                for (int i = 1; i < Globals.Max_Friends; i++)
                {
                    PStruct.player[index].friend[i].name = "";
                    PStruct.player[index].friend[i].sprite = "";
                    PStruct.player[index].friend[i].sprite_index = 0;
                    PStruct.player[index].friend[i].classid = 0;
                    PStruct.player[index].friend[i].level = 0;
                    PStruct.player[index].friend[i].guildname = "";
                }
                return true;
            }
            catch { return false; }
        }
        //*********************************************************************************************
        // ClearSkin / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Faz parte de um sistema incompleto.
        //*********************************************************************************************
        public static bool ClearSkin(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null) 
            { 
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index)); 
            }

            //CÓDIGO
            try
            {

                for (int i = 1; i < Globals.Max_Skins; i++)
                {
                    PStruct.player[index].skin[i] = false;
                }
                return true;
            }
            catch { return false; }
        }
        //*********************************************************************************************
        // LoadBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega os itens no banco de determinado jogador.
        //*********************************************************************************************
        public static bool LoadBank(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index));
            }

            //CÓDIGO
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PStruct.player[index].Username + ".dat"))
                {

                    //representa o arquivo
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PStruct.player[index].Username + ".dat", FileMode.Open);

                    //cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    for (int i = 1; i < Globals.Max_BankSlots; i++)
                    {
                        PStruct.player[index].bankslot[i].type = br.ReadInt32();
                        PStruct.player[index].bankslot[i].num = br.ReadInt32(); 
                        PStruct.player[index].bankslot[i].value = br.ReadInt32(); 
                        PStruct.player[index].bankslot[i].refin = br.ReadInt32(); 
                        PStruct.player[index].bankslot[i].exp = br.ReadInt32(); 
                    }

                    //Fecha o leitor
                    br.Close();

                    //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                    //Responde que o item foi carregado
                    return true;
                }
                else
                //Responde que o mapa não existe
                { return false; }
            }
        }
        //*********************************************************************************************
        // LoadFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega a lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool LoadFriendList(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index));
            }

            //CÓDIGO
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PStruct.player[index].Username + ".dat"))
                {

                    //representa o arquivo
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PStruct.player[index].Username + ".dat", FileMode.Open);

                    //cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    int friendcount = br.ReadInt32();

                    for (int i = 1; i <= friendcount; i++)
                    {
                        PStruct.player[index].friend[i].name = br.ReadString();
                        PStruct.player[index].friend[i].sprite = br.ReadString(); 
                        PStruct.player[index].friend[i].sprite_index = br.ReadInt32(); 
                        PStruct.player[index].friend[i].classid = br.ReadInt32(); 
                        PStruct.player[index].friend[i].level = br.ReadInt32(); 
                        PStruct.player[index].friend[i].guildname = br.ReadString(); 
                    }

                    //Fecha o leitor
                    br.Close();

                    //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                    //Responde que o item foi carregado
                    return true;
                }
                else
                //Responde que o mapa não existe
                { return false; }
            }
        }
        //*********************************************************************************************
        // ClearPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Limpa os dados de determinado jogador.
        //*********************************************************************************************
        public static void ClearPlayer(int index, bool complete = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, complete) != null)
            {
                return;
            }

            //CÓDIGO
            //Define dados básicos sobre o personagem a ser apagado
            string username = PStruct.player[Convert.ToInt32(index)].Username;
            string ID = PStruct.player[Convert.ToInt32(index)].SelectedChar.ToString();
            for (int i = 0; i <= 2; i++)
            {
                PStruct.character[index, i].CharacterName = null;
                PStruct.character[index, i].Spriteindex = 0;
                PStruct.character[index, i].ClassId = 0;
                PStruct.character[index, i].Sprite = null;
                PStruct.character[index, i].Level = 0;
                PStruct.character[index, i].Exp = 0;
                PStruct.character[index, i].Fire = 0;
                PStruct.character[index, i].Earth = 0;
                PStruct.character[index, i].Water = 0;
                PStruct.character[index, i].Wind = 0;
                PStruct.character[index, i].Dark = 0;
                PStruct.character[index, i].Light = 0;
                PStruct.character[index, i].Map = 0;
                PStruct.character[index, i].X = 0;
                PStruct.character[index, i].Y = 0;
                PStruct.character[index, i].Dir = 0;
                PStruct.character[index, i].Equipment = "0;0,0;0,0;0,0;0,0;0";
                PStruct.character[index, i].Vitality = 0;
                PStruct.character[index, i].Spirit = 0;
                PStruct.character[index, i].Access = 0;
                PStruct.character[index, i].SkillPoints = 0;
                PStruct.character[index, i].Gold = 0;
                PStruct.character[index, i].Cash = 0;
                PStruct.character[index, i].Hue = 0;
                PStruct.character[index, i].Gender = 0;
                PStruct.character[index, i].Guild = 0;
                PStruct.character[index, i].PVPChangeTimer = 0;
                PStruct.character[index, i].PVPBanTimer = 0;
                PStruct.character[index, i].PVPPenalty = 0;
                PStruct.character[index, i].PVP = false;

                for (int z = 1; z < Globals.Max_Chests; z++)
                {
                    PStruct.character[index, i].Chest[z] = false;
                }

                for (int z = 1; z < Globals.Max_Profs_Per_Char; z++)
                {
                    PStruct.character[index, i].Prof_Type[z] = 0;
                    PStruct.character[index, i].Prof_Level[z] = 0;
                    PStruct.character[index, i].Prof_Exp[z] = 0;
                }

                for (int z = 1; z < Globals.Max_PShops; z++)
                {
                    PStruct.character[index, i].pshopslot[z].type = 0;
                    PStruct.character[index, i].pshopslot[z].num = 0;
                    PStruct.character[index, i].pshopslot[z].value = 0;
                    PStruct.character[index, i].pshopslot[z].refin = 0;
                    PStruct.character[index, i].pshopslot[z].price = 0;
                }

            }

            //QUEST GIANT CODO
            for (int g = 1; g < Globals.MaxQuestGivers; g++)
            {
                for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                {
                    if (PStruct.queststatus[index, g, q].status != 0)
                    {
                        PStruct.queststatus[index, g, q].status = 0;
                        for (int k = 1; k < Globals.MaxQuestKills; k++)
                        {
                            PStruct.questkills[index, g, q, k].kills = 0;
                        }
                        for (int a = 1; a < Globals.MaxQuestActions; a++)
                        {
                            PStruct.questactions[index, g, q, a].actiondone = false;
                        }
                    }
                }
            }

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                PStruct.skill[index, i].num = 0;
                PStruct.skill[index, i].level = 0;
            }

            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                PStruct.invslot[index, i].item = Globals.NullItem;
            }

            for (int i = 1; i < Globals.MaxHotkeys; i++)
            {
                PStruct.hotkey[index, i].type = 0;
                PStruct.hotkey[index, i].num = 0;
            }

            if (complete)
            {
                PStruct.player[index].Username = "";
                PStruct.player[index].Password = "";
                PStruct.player[index].Premmy = "";
                PStruct.player[index].WPoints = 0;
                PStruct.player[index].Confirmed = false;
                PStruct.player[index].Email = "";
                PStruct.player[index].SelectedChar = 0;
            }
        }

        #endregion

        #region Char
        //*********************************************************************************************
        // SlotExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se o Slot para criação de personagem está livre
        //*********************************************************************************************
        public static bool SlotExists(string username, string ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, username, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, username, ID));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                if (fileinfo.Name.ToLower().Contains(username + "slot" + ID)) { return true; }
            }

            return false;
        }
        //*********************************************************************************************
        // ResetAndGiveExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Método não é chamado em nenhum momento no servidor, serve para executar o reset do jogador
        // e devolver o nível, este método está em testes, então o risco é seu.
        //*********************************************************************************************
        public static bool ResetAndGiveExp()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                //if (fileinfo.Name.Contains(username + "slot" + ID)) { return true; }
                Console.WriteLine(fileinfo.Name);
                LoadUnCompleteChar(fileinfo.Name);
                int level = PStruct.character[1, 0].Level;
                int totalexp = 0;

                for (int i = 1; i < level; i++)
                {
                    double total = (i * 500) * 1.2;
                    int exp = Convert.ToInt32(total);
                    totalexp += exp;
                }

                Console.WriteLine(totalexp);
                PStruct.character[1, 0].Level = 1;
                PStruct.character[1, 0].Exp = totalexp;
                PStruct.player[1].SelectedChar = 0;

                int index = 1;
                int classid = PStruct.character[1, 0].ClassId;
                PStruct.character[1, 0].SkillPoints = 0;

                if (classid == 1)
                {
                    //Prece ao vento
                    PStruct.skill[index, 1].num = 2;
                    PStruct.skill[index, 1].level = 0;

                    //Onos Aroga
                    PStruct.skill[index, 2].num = 6;
                    PStruct.skill[index, 2].level = 0;

                    //Etrof Otnev
                    PStruct.skill[index, 3].num = 7;
                    PStruct.skill[index, 3].level = 0;

                    //Ogral Ossap
                    PStruct.skill[index, 4].num = 8;
                    PStruct.skill[index, 4].level = 0;
                }

                if (classid == 2)
                {
                    //Tempo ruim
                    PStruct.skill[index, 1].num = 14;
                    PStruct.skill[index, 1].level = 0;

                    //Ponto de corrupção
                    PStruct.skill[index, 2].num = 16;
                    PStruct.skill[index, 2].level = 0;

                    //Ambição Arut Neva
                    PStruct.skill[index, 3].num = 15;
                    PStruct.skill[index, 3].level = 0;

                    //Antes que você possa notar!
                    PStruct.skill[index, 4].num = 9;
                    PStruct.skill[index, 4].level = 0;
                }

                if (classid == 3)
                {
                    //Motivação Aiprah
                    PStruct.skill[index, 1].num = 4;
                    PStruct.skill[index, 1].level = 0;

                    //Julgamento Aiprah
                    PStruct.skill[index, 2].num = 3;
                    PStruct.skill[index, 2].level = 0;

                    //Maldição Aiprah
                    PStruct.skill[index, 3].num = 1;
                    PStruct.skill[index, 3].level = 0;

                    //Controle Aiprah
                    PStruct.skill[index, 4].num = 5;
                    PStruct.skill[index, 4].level = 0;
                }

                if (classid == 4)
                {
                    //Primeiro Corte
                    PStruct.skill[index, 1].num = 10;
                    PStruct.skill[index, 1].level = 0;

                    //Segundo Corte
                    PStruct.skill[index, 2].num = 11;
                    PStruct.skill[index, 2].level = 0;

                    //Terceiro Corte
                    PStruct.skill[index, 3].num = 12;
                    PStruct.skill[index, 3].level = 0;

                    //Daishi ni Katto
                    PStruct.skill[index, 4].num = 13;
                    PStruct.skill[index, 4].level = 0;
                }

                if (classid == 5)
                {
                    //Coração Ritelf
                    PStruct.skill[index, 1].num = 35;
                    PStruct.skill[index, 1].level = 0;

                    //Esmagamento
                    PStruct.skill[index, 2].num = 36;
                    PStruct.skill[index, 2].level = 0;

                    //Afugentar
                    PStruct.skill[index, 3].num = 38;
                    PStruct.skill[index, 3].level = 0;

                    //Contra Ataque
                    PStruct.skill[index, 4].num = 37;
                    PStruct.skill[index, 4].level = 0;
                }

                if (classid == 6)
                {
                    //Benção Cani
                    PStruct.skill[index, 1].num = 39;
                    PStruct.skill[index, 1].level = 0;

                    //Dança da Folha
                    PStruct.skill[index, 2].num = 40;
                    PStruct.skill[index, 2].level = 0;

                    //Empolgação
                    PStruct.skill[index, 3].num = 41;
                    PStruct.skill[index, 3].level = 0;

                    //Masterização
                    PStruct.skill[index, 4].num = 42;
                    PStruct.skill[index, 4].level = 0;
                }

                int extrafire = 0;
                int extraearth = 0;
                int extrawater = 0;
                int extrawind = 0;
                int extradark = 0;
                int extralight = 0;

                if (classid == 1)
                {
                    extrafire = 3;
                    extraearth = 4;
                    extrawater = 1;
                    extrawind = 5;
                    extradark = 16;
                    extralight = 11;
                }

                if (classid == 2)
                {
                    extrafire = 7;
                    extraearth = 12;
                    extrawater = 2;
                    extrawind = 8;
                    extradark = 8;
                    extralight = 3;
                }

                if (classid == 3)
                {
                    extrafire = 5;
                    extraearth = 8;
                    extrawater = 2;
                    extrawind = 5;
                    extradark = 10;
                    extralight = 10;
                }

                if (classid == 4)
                {
                    extrafire = 5;
                    extraearth = 5;
                    extrawater = 12;
                    extrawind = 10;
                    extradark = 6;
                    extralight = 2;
                }


                if (classid == 5)
                {
                    extrafire = 4;
                    extraearth = 15;
                    extrawater = 1;
                    extrawind = 1;
                    extradark = 4;
                    extralight = 15;
                }

                if (classid == 6)
                {
                    extrafire = 4;
                    extraearth = 3;
                    extrawater = 10;
                    extrawind = 20;
                    extradark = 1;
                    extralight = 2;
                }

                PStruct.character[1, 0].Fire = extrafire;
                PStruct.character[1, 0].Earth = extraearth;
                PStruct.character[1, 0].Water = extrawater;
                PStruct.character[1, 0].Wind = extrawind;
                PStruct.character[1, 0].Light = extralight;
                PStruct.character[1, 0].Dark = extradark;

                PStruct.character[1, 0].Points = 16;

                UnSaveCharacter(1, fileinfo.Name); 
            }

            return false;
        }
        //*********************************************************************************************
        // UnSaveCharacter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Complemento do sistema de resetar e devolver exp.
        //*********************************************************************************************
        public static void UnSaveCharacter(int index, string filename, bool isnewchar = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, filename, isnewchar) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].CharacterName != null)
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/convert/" + filename) == false)
                {
                    //representa o arquivo que vamos criar
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/convert/" + filename, FileMode.Create);

                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //Define as váriaveis a serem salvas
                    string charName = (PStruct.character[index, PStruct.player[index].SelectedChar].CharacterName);
                    int charSpriteindex = (PStruct.character[index, PStruct.player[index].SelectedChar].Spriteindex);
                    int charClass = (PStruct.character[index, PStruct.player[index].SelectedChar].ClassId);
                    string charSprite = (PStruct.character[index, PStruct.player[index].SelectedChar].Sprite);
                    int charLevel = (PStruct.character[index, PStruct.player[index].SelectedChar].Level);
                    int charExp = (PStruct.character[index, PStruct.player[index].SelectedChar].Exp);
                    int charFire = (PStruct.character[index, PStruct.player[index].SelectedChar].Fire);
                    int charEarth = (PStruct.character[index, PStruct.player[index].SelectedChar].Earth);
                    int charWater = (PStruct.character[index, PStruct.player[index].SelectedChar].Water);
                    int charWind = (PStruct.character[index, PStruct.player[index].SelectedChar].Wind);
                    int charDark = (PStruct.character[index, PStruct.player[index].SelectedChar].Dark);
                    int charLight = (PStruct.character[index, PStruct.player[index].SelectedChar].Light);
                    int charPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].Points);
                    int charMap = (PStruct.character[index, PStruct.player[index].SelectedChar].Map);
                    byte charX = (PStruct.character[index, PStruct.player[index].SelectedChar].X);
                    byte charY = (PStruct.character[index, PStruct.player[index].SelectedChar].Y);
                    byte charDir = (PStruct.character[index, PStruct.player[index].SelectedChar].Dir);
                    string Equipment = (PStruct.character[index, PStruct.player[index].SelectedChar].Equipment);
                    int Vitality = (PStruct.tempplayer[Convert.ToInt32(index)].Vitality);
                    int Spirit = (PStruct.tempplayer[Convert.ToInt32(index)].Spirit);
                    int Access = (PStruct.character[index, PStruct.player[index].SelectedChar].Access);
                    int SkillPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].SkillPoints);
                    int Gold = (PStruct.character[index, PStruct.player[index].SelectedChar].Gold);
                    int Cash = (PStruct.character[index, PStruct.player[index].SelectedChar].Cash);
                    int Hue = (PStruct.character[index, PStruct.player[index].SelectedChar].Hue);
                    int Gender = (PStruct.character[index, PStruct.player[index].SelectedChar].Gender);
                    int Guild = (PStruct.character[index, PStruct.player[index].SelectedChar].Guild);

                    //PVP
                    long PVPChangeTimer;
                    long PVPBanTimer;
                    long PVPPenalty;
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPChangeTimer > 0)
                    {
                        PVPChangeTimer = PStruct.character[index, PStruct.player[index].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPChangeTimer < 0) { PVPChangeTimer = 0; }
                    }
                    else { PVPChangeTimer = 0; }
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPBanTimer > 0)
                    {
                        PVPBanTimer = PStruct.character[index, PStruct.player[index].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPBanTimer < 0) { PVPBanTimer = 0; }
                    }
                    else { PVPBanTimer = 0; }
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPPenalty > 0)
                    {
                        PVPPenalty = PStruct.character[index, PStruct.player[index].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPPenalty < 0) { PVPPenalty = 0; }
                    }
                    else { PVPPenalty = 0; }

                    bool PVP = PStruct.character[index, PStruct.player[index].SelectedChar].PVP;

                    //QUEST
                    int questcount = PStruct.GetPlayerQuestsCount(index);

                    if (isnewchar)
                    {
                        int totalpoints = Convert.ToInt32(charFire) + Convert.ToInt32(charEarth) + Convert.ToInt32(charWater) + Convert.ToInt32(charWind) + Convert.ToInt32(charDark) + Convert.ToInt32(charLight);
                        if (totalpoints != 16) { return; }
                    }

                    //grava os dados no arquivo
                    bw.Write(charName);
                    bw.Write(Guild);
                    bw.Write(charSpriteindex);
                    bw.Write(charClass);
                    bw.Write(charSprite);
                    bw.Write(charLevel);
                    bw.Write(charExp);
                    bw.Write(charFire);
                    bw.Write(charEarth);
                    bw.Write(charWater);
                    bw.Write(charWind);
                    bw.Write(charDark);
                    bw.Write(charLight);
                    bw.Write(charPoints);
                    bw.Write(charMap);
                    bw.Write(charX);
                    bw.Write(charY);
                    bw.Write(charDir);
                    bw.Write(Equipment);
                    bw.Write(Vitality);
                    bw.Write(Spirit);
                    bw.Write(Access);
                    bw.Write(SkillPoints);
                    bw.Write(Gold);
                    bw.Write(Cash);
                    bw.Write(Hue);
                    bw.Write(Gender);
                    bw.Write(PVPChangeTimer);
                    bw.Write(PVPBanTimer);
                    bw.Write(PVPPenalty);
                    bw.Write(PVP);

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    bw.Write(questcount);

                    for (int g = 1; g < Globals.MaxQuestGivers; g++)
                    {
                        for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                        {
                            if (PStruct.queststatus[index, g, q].status != 0)
                            {
                                bw.Write(g);
                                bw.Write(q);
                                bw.Write(PStruct.queststatus[index, g, q].status);
                                for (int k = 1; k < Globals.MaxQuestKills; k++)
                                {
                                    bw.Write(PStruct.questkills[index, g, q, k].kills);
                                }
                                for (int a = 1; a < Globals.MaxQuestActions; a++)
                                {
                                    bw.Write(PStruct.questactions[index, g, q, a].actiondone);
                                }
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Type[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PStruct.skill[index, i].num);
                        bw.Write(PStruct.skill[index, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PStruct.invslot[index, i].item);
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].type);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].num);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].refin);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].value);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PStruct.hotkey[index, i].type);
                        bw.Write(PStruct.hotkey[index, i].num);
                    }

                    bw.Close();
                }
            }
        }
        //*********************************************************************************************
        // LoadUnCompleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Complemento do sistema de resetar e devolver exp.
        //*********************************************************************************************
        public static bool LoadUnCompleteChar(string filename)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, filename) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, filename));
            }

            //CÓDIGO
            int index = 1;
            int ID = 0;
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + filename) == true)
            {
                //Representa o arquivo que vamos abrir

                FileStream file;

                try
                {
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + filename, FileMode.Open);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PStruct.character[index, ID].CharacterName = br.ReadString();
                    PStruct.character[index, ID].Guild = br.ReadInt32();
                    PStruct.character[index, ID].Spriteindex = br.ReadInt32();
                    PStruct.character[index, ID].ClassId = br.ReadInt32();
                    PStruct.character[index, ID].Sprite = br.ReadString();
                    PStruct.character[index, ID].Level = br.ReadInt32();
                    PStruct.character[index, ID].Exp = br.ReadInt32();
                    PStruct.character[index, ID].Fire = br.ReadInt32();
                    PStruct.character[index, ID].Earth = br.ReadInt32();
                    PStruct.character[index, ID].Water = br.ReadInt32();
                    PStruct.character[index, ID].Wind = br.ReadInt32();
                    PStruct.character[index, ID].Dark = br.ReadInt32();
                    PStruct.character[index, ID].Light = br.ReadInt32();
                    PStruct.character[index, ID].Points = br.ReadInt32();
                    PStruct.character[index, ID].Map = br.ReadInt32();
                    PStruct.character[index, ID].X = br.ReadByte();
                    PStruct.character[index, ID].Y = br.ReadByte();
                    PStruct.character[index, ID].Dir = br.ReadByte();
                    PStruct.character[index, ID].Equipment = br.ReadString();
                    PStruct.character[index, ID].Vitality = br.ReadInt32();
                    PStruct.character[index, ID].Spirit = br.ReadInt32();
                    PStruct.character[index, ID].Access = br.ReadInt32();
                    PStruct.character[index, ID].SkillPoints = br.ReadInt32();
                    PStruct.character[index, ID].Gold = br.ReadInt32();
                    PStruct.character[index, ID].Cash = br.ReadInt32();
                    PStruct.character[index, ID].Hue = br.ReadInt32();
                    PStruct.character[index, ID].Gender = br.ReadInt32();
                    PStruct.character[index, ID].PVPChangeTimer = br.ReadInt64();
                    PStruct.character[index, ID].PVPBanTimer = br.ReadInt64();
                    PStruct.character[index, ID].PVPPenalty = br.ReadInt64();
                    PStruct.character[index, ID].PVP = br.ReadBoolean();

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    int questcount = br.ReadInt32();
                    int g = 0;
                    int z = 0;

                    for (int q = 1; q <= questcount; q++)
                    {
                        {
                            g = br.ReadInt32();
                            z = br.ReadInt32();
                            PStruct.queststatus[index, g, z].status = br.ReadInt32();
                            for (int k = 1; k < Globals.MaxQuestKills; k++)
                            {
                                PStruct.questkills[index, g, z, k].kills = br.ReadInt32();
                            }
                            for (int a = 1; a < Globals.MaxQuestActions; a++)
                            {
                                PStruct.questactions[index, g, z, a].actiondone = br.ReadBoolean();
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        PStruct.character[index, ID].Chest[i] = br.ReadBoolean();
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        PStruct.character[index, ID].Prof_Type[i] = br.ReadInt32();
                        PStruct.character[index, ID].Prof_Level[i] = br.ReadInt32();
                        PStruct.character[index, ID].Prof_Exp[i] = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        PStruct.skill[index, i].num = br.ReadInt32();
                        PStruct.skill[index, i].level = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PStruct.invslot[index, i].item = br.ReadString();
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].type = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].num = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].refin = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].value = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].price = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PStruct.hotkey[index, i].type = br.ReadByte();
                        PStruct.hotkey[index, i].num = br.ReadInt32();
                    }

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed); return false; }
        }
        //*********************************************************************************************
        // CharExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se determinado personagem já existe.
        //*********************************************************************************************
        public static bool CharExists(string name)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, name));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                for (int i = 0; i <= Globals.MaxChars; i++){
                    if (fileinfo.Name.ToLower().Contains("slot" + i + " - " + name.ToLower() + ".dat")) { return true; }
                }

            }

            return false;
        }
        //*********************************************************************************************
        // GetCharBySlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static string GetCharBySlot(string username, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, username, ID) != null)
            {
                return Convert.ToString
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, username, ID));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                    if (fileinfo.Name.Contains(username + "slot" + ID)) { return fileinfo.Name; }
            }
            Console.WriteLine(username);
            return null;
        }
        //*********************************************************************************************
        // NameIsIllegal / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool NameIsIllegal(string name)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, name));
            }

            //CÓDIGO
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            if (r.IsMatch(name))
            {
                return true;
            }
            return false;
        }
        //*********************************************************************************************
        // CreateNewChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Cria um novo personagem com os parâmetros determinados.
        //*********************************************************************************************
        public static bool CreateNewChar(int index, string email, int ID, string name, int classid, int fire, int earth, int water, int wind, int dark, int light, int hue, int gender)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, email, ID, name, classid, fire, earth, water, wind, dark, light, hue, gender) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, email, ID, name, classid, fire, earth, water, wind, dark, light, hue, gender));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat") == false)
            {
                FileStream file;

                try
                {
                    //representa o arquivo que vamos criar
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat", FileMode.Create);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    Console.WriteLine(name);
                    return false;
                }

                try
                {
                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //incorrect class
                    if ((classid > Globals.MaxClasses) || (classid < 1)) { return false; }

                    if ((hue < 0) || (hue > 500)) { return false; }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PStruct.invslot[index, i].item = Globals.NullItem;
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PStruct.character[index, ID].pshopslot[i].num = 0;
                        PStruct.character[index, ID].pshopslot[i].type = 0;
                        PStruct.character[index, ID].pshopslot[i].value = 0;
                        PStruct.character[index, ID].pshopslot[i].refin = 0;
                        PStruct.character[index, ID].pshopslot[i].price = 0;
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PStruct.hotkey[index, i].type = 0;
                        PStruct.hotkey[index, i].num = 0;
                    }

                    int totalpoints = fire + earth + water + wind + dark + light;
                    if (totalpoints != 16) 
                    { 
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat");
                        }
                        SendData.Send_NStatus(index, "Falha ao criar personagem, por favor, recomece.");
                        return false;
                    }

                    string sprite = PStruct.classes[classid].sprite_name[gender];
                    int spriteindex = PStruct.classes[classid].sprite_index[gender];

                    int extrafire = PStruct.classes[classid].fire;
                    int extraearth = PStruct.classes[classid].earth;
                    int extrawater = PStruct.classes[classid].water;
                    int extrawind = PStruct.classes[classid].wind;
                    int extradark = PStruct.classes[classid].dark;
                    int extralight = PStruct.classes[classid].light;

                    //grava os dados no arquivo
                    bw.Write(name);
                    bw.Write(0);
                    bw.Write(spriteindex);
                    bw.Write(classid);
                    bw.Write(sprite);
                    bw.Write(1);
                    bw.Write(0);
                    bw.Write(fire + extrafire);
                    bw.Write(earth + extraearth);
                    bw.Write(water + extrawater);
                    bw.Write(wind + extrawind);
                    bw.Write(dark + extradark);
                    bw.Write(light + extralight);
                    bw.Write(0);
                    bw.Write(Globals.InitialMap);
                    bw.Write(Globals.InitialX);
                    bw.Write(Globals.InitialY);
                    bw.Write(Globals.InitialMap);
                    bw.Write(Globals.InitialX);
                    bw.Write(Globals.InitialY);
                    bw.Write(Globals.DirDown);
                    bw.Write(Globals.InitialHelmet + "," + Globals.InitialArmor + "," + Globals.InitialWeapon + "," + Globals.InitialShield + "," + Globals.InitialPet);
                    bw.Write(200);
                    bw.Write(200);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(hue);
                    bw.Write(gender);
                    bw.Write(0);
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(false);

                    if (classid == 1)
                    {
                        //Incendiar
                        PStruct.skill[index, 1].num = 44;
                        PStruct.skill[index, 1].level = 1;

                        //Onos Aroga
                        PStruct.skill[index, 2].num = 6;
                        PStruct.skill[index, 2].level = 1;

                        //Etrof Otnev
                        PStruct.skill[index, 3].num = 8;
                        PStruct.skill[index, 3].level = 1;

                        //Ogral Ossap
                        PStruct.skill[index, 4].num = 7;
                        PStruct.skill[index, 4].level = 1;

                        //Aprimoramento Mágico
                        PStruct.skill[index, 5].num = 46;
                        PStruct.skill[index, 5].level = 0;

                        //Estrela congelante
                        PStruct.skill[index, 6].num = 45;
                        PStruct.skill[index, 6].level = 1;


                    }

                    if (classid == 2)
                    {
                        //Tempo ruim
                        PStruct.skill[index, 1].num = 14;
                        PStruct.skill[index, 1].level = 1;

                        //Aprimoramento Vital
                        PStruct.skill[index, 2].num = 52;
                        PStruct.skill[index, 2].level = 1;

                        //Ponto de corrupção
                        PStruct.skill[index, 3].num = 16;
                        PStruct.skill[index, 3].level = 1;

                        //Ambição Arut Neva
                        PStruct.skill[index, 4].num = 15;
                        PStruct.skill[index, 4].level = 1;

                        //Manipulação Vital
                        PStruct.skill[index, 5].num = 53;
                        PStruct.skill[index, 5].level = 1;

                        //Antes que você possa notar!
                        PStruct.skill[index, 6].num = 9;
                        PStruct.skill[index, 6].level = 1;
                    }

                    if (classid == 3)
                    {
                        //Lembrança do Deserto
                        PStruct.skill[index, 1].num = 55;
                        PStruct.skill[index, 1].level = 1;

                        //Motivação Aiprah
                        PStruct.skill[index, 2].num = 4;
                        PStruct.skill[index, 2].level = 1;

                        //Julgamento Aiprah
                        PStruct.skill[index, 3].num = 3;
                        PStruct.skill[index, 3].level = 1;

                        //Maldição Aiprah
                        PStruct.skill[index, 4].num = 1;
                        PStruct.skill[index, 4].level = 1;

                        //Filhos da Areia
                        PStruct.skill[index, 5].num = 56;
                        PStruct.skill[index, 5].level = 1;

                        //Controle Aiprah
                        PStruct.skill[index, 6].num = 5;
                        PStruct.skill[index, 6].level = 1;
                    }

                    if (classid == 4)
                    {
                        //Primeiro Corte
                        PStruct.skill[index, 1].num = 10;
                        PStruct.skill[index, 1].level = 1;

                        //Segundo Corte
                        PStruct.skill[index, 2].num = 11;
                        PStruct.skill[index, 2].level = 1;

                        //Embainhar
                        PStruct.skill[index, 3].num = 47;
                        PStruct.skill[index, 3].level = 1;

                        //Terceiro Corte
                        PStruct.skill[index, 4].num = 12;
                        PStruct.skill[index, 4].level = 1;

                        //Laço de vida
                        PStruct.skill[index, 5].num = 48;
                        PStruct.skill[index, 5].level = 1;

                        //Daishi ni Katto
                        PStruct.skill[index, 6].num = 13;
                        PStruct.skill[index, 6].level = 1;
                    }

                    if (classid == 5)
                    {
                        //Lâmina Ritelf
                        PStruct.skill[index, 1].num = 54;
                        PStruct.skill[index, 1].level = 1;

                        //Coração Ritelf
                        PStruct.skill[index, 2].num = 35;
                        PStruct.skill[index, 2].level = 1;

                        //Esmagamento
                        PStruct.skill[index, 3].num = 36;
                        PStruct.skill[index, 3].level = 1;

                        //Afugentar
                        PStruct.skill[index, 4].num = 38;
                        PStruct.skill[index, 4].level = 1;

                        //Cortes Gêmeos
                        PStruct.skill[index, 5].num = 51;
                        PStruct.skill[index, 5].level = 1;

                        //Contra Ataque
                        PStruct.skill[index, 6].num = 37;
                        PStruct.skill[index, 6].level = 1;
                    }

                    if (classid == 6)
                    {
                        //Benção Cani
                        PStruct.skill[index, 1].num = 39;
                        PStruct.skill[index, 1].level = 1;

                        //Dança da Folha
                        PStruct.skill[index, 2].num = 40;
                        PStruct.skill[index, 2].level = 1;

                        //Fluxo da Alma
                        PStruct.skill[index, 3].num = 50;
                        PStruct.skill[index, 3].level = 1;

                        //Lua Nova
                        PStruct.skill[index, 4].num = 49;
                        PStruct.skill[index, 4].level = 1;

                        //Empolgação
                        PStruct.skill[index, 5].num = 41;
                        PStruct.skill[index, 5].level = 1;

                        //Masterização
                        PStruct.skill[index, 6].num = 42;
                        PStruct.skill[index, 6].level = 1;
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Type[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PStruct.skill[index, i].num);
                        bw.Write(PStruct.skill[index, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PStruct.invslot[index, i].item.ToString());
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PStruct.character[index, ID].pshopslot[i].type);
                        bw.Write(PStruct.character[index, ID].pshopslot[i].num);
                        bw.Write(PStruct.character[index, ID].pshopslot[i].refin);
                        bw.Write(PStruct.character[index, ID].pshopslot[i].value);
                        bw.Write(PStruct.character[index, ID].pshopslot[i].exp);
                        bw.Write(PStruct.character[index, ID].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PStruct.hotkey[index, i].type);
                        bw.Write(PStruct.hotkey[index, i].num);
                    }

                    bw.Close();

                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { return false; }
        }
        //*********************************************************************************************
        // LoadShowChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega o personagem apenas para ser mostrado na seleção.
        //*********************************************************************************************
        public static bool LoadShowChar(int index, string email, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, email, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, email, ID));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID)) == true)
            {
                FileStream file;

                try
                {
                    //Representa o arquivo que vamos abrir
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID), FileMode.Open);

                }
                catch
                {
                    Console.WriteLine(lang.load_show_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PStruct.character[index, ID].CharacterName = br.ReadString();
                    PStruct.character[index, ID].Guild = br.ReadInt32();
                    PStruct.character[index, ID].Spriteindex = br.ReadInt32();
                    PStruct.character[index, ID].ClassId = br.ReadInt32();
                    PStruct.character[index, ID].Sprite = br.ReadString();
                    PStruct.character[index, ID].Level = br.ReadInt32();
                    PStruct.character[index, ID].Exp = br.ReadInt32();
                    PStruct.character[index, ID].Fire = br.ReadInt32();
                    PStruct.character[index, ID].Earth = br.ReadInt32();
                    PStruct.character[index, ID].Water = br.ReadInt32();
                    PStruct.character[index, ID].Wind = br.ReadInt32();
                    PStruct.character[index, ID].Dark = br.ReadInt32();
                    PStruct.character[index, ID].Light = br.ReadInt32();
                    PStruct.character[index, ID].Points = br.ReadInt32();
                    PStruct.character[index, ID].Map = br.ReadInt32();
                    PStruct.character[index, ID].X = br.ReadByte();
                    PStruct.character[index, ID].Y = br.ReadByte();
                    PStruct.character[index, ID].BootMap = br.ReadInt32();
                    PStruct.character[index, ID].BootX = br.ReadByte();
                    PStruct.character[index, ID].BootY = br.ReadByte();
                    PStruct.character[index, ID].Dir = br.ReadByte();
                    PStruct.character[index, ID].Equipment = br.ReadString();
                    PStruct.character[index, ID].Vitality = br.ReadInt32();
                    PStruct.character[index, ID].Spirit = br.ReadInt32();
                    PStruct.character[index, ID].Access = br.ReadInt32();
                    PStruct.character[index, ID].SkillPoints = br.ReadInt32();
                    PStruct.character[index, ID].Gold = br.ReadInt32();
                    PStruct.character[index, ID].Cash = br.ReadInt32();
                    PStruct.character[index, ID].Hue = br.ReadInt32();
                    PStruct.character[index, ID].Gender = br.ReadInt32();

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed); return false; }
        }
        //*********************************************************************************************
        // DeleteAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Deletar determinada conta.
        //*********************************************************************************************
        public static bool DeleteAccount(string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, email) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, email));
            }

            //CÓDIGO
            try
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat"))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + ".dat");
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }

        }
        //*********************************************************************************************
        // DeleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Deletar determinado personagem.
        //*********************************************************************************************
        public static bool DeleteChar(int index, int slot)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, slot) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, slot));
            }

            //CÓDIGO
            try
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(PStruct.player[index].Email, slot)) == true)
                {
                    FileStream file;

                    try
                    {
                        //Representa o arquivo que vamos abrir
                        file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(PStruct.player[index].Email, slot), FileMode.Open);

                    }
                    catch
                    {
                        return false;
                    }

                    try
                    {
                        //Cria o leitor do arquivo
                        BinaryReader br = new BinaryReader(file);

                        //Lê os dados no arquivo
                        string name = br.ReadString();
                        int guild = br.ReadInt32();

                        if (guild <= 0)
                        {
                            br.Close();
                        }
                        else
                        {
                            //Retira o idiota da guilda
                            for (int i = 1; i < Globals.Max_Guild_Members; i++)
                            {
                                if (GStruct.guild[guild].memberlist[i] == name)
                                {
                                    GStruct.guild[guild].memberlist[i] = "";
                                    GStruct.guild[guild].membersprite[i] = "";
                                    GStruct.guild[guild].membersprite_index[i] = 0;
                                }
                            }

                            SaveGuild(guild.ToString());

                            SendData.Send_CompleteGuildToGuildG(guild);
                            SendData.Send_MsgToGuild(guild, lang.the_player + " " + name + " " + lang.has_left_the_guild, Globals.ColorWhite, Globals.Msg_Type_Server);

                            br.Close();
                        }
                    }
                    catch
                    { return false; }
                }
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(PStruct.player[index].Email, slot));
                return true;
            }
            catch
            {
                Console.WriteLine(lang.error);
                return false;
            }
        }
        //*********************************************************************************************
        // LoadCompleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadCompleteChar(int index, string email, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, email, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, index, email, ID));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID)) == true)
            {
                //Representa o arquivo que vamos abrir

                FileStream file;

                try
                {
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID), FileMode.Open);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    Console.WriteLine(lang.character_deleted);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PStruct.character[index, ID].CharacterName = br.ReadString();
                    PStruct.character[index, ID].Guild = br.ReadInt32();
                    PStruct.character[index, ID].Spriteindex = br.ReadInt32();
                    PStruct.character[index, ID].ClassId = br.ReadInt32();
                    PStruct.character[index, ID].Sprite = br.ReadString();
                    PStruct.character[index, ID].Level = br.ReadInt32();
                    PStruct.character[index, ID].Exp = br.ReadInt32();
                    PStruct.character[index, ID].Fire = br.ReadInt32();
                    PStruct.character[index, ID].Earth = br.ReadInt32();
                    PStruct.character[index, ID].Water = br.ReadInt32();
                    PStruct.character[index, ID].Wind = br.ReadInt32();
                    PStruct.character[index, ID].Dark = br.ReadInt32();
                    PStruct.character[index, ID].Light = br.ReadInt32();
                    PStruct.character[index, ID].Points = br.ReadInt32();
                    PStruct.character[index, ID].Map = br.ReadInt32();
                    PStruct.character[index, ID].X = br.ReadByte();
                    PStruct.character[index, ID].Y = br.ReadByte();
                    PStruct.character[index, ID].BootMap = br.ReadInt32();
                    PStruct.character[index, ID].BootX = br.ReadByte();
                    PStruct.character[index, ID].BootY = br.ReadByte();
                    PStruct.character[index, ID].Dir = br.ReadByte();
                    PStruct.character[index, ID].Equipment = br.ReadString();
                    PStruct.character[index, ID].Vitality = br.ReadInt32();
                    PStruct.character[index, ID].Spirit = br.ReadInt32();
                    PStruct.character[index, ID].Access = br.ReadInt32();
                    PStruct.character[index, ID].SkillPoints = br.ReadInt32();
                    PStruct.character[index, ID].Gold = br.ReadInt32();
                    PStruct.character[index, ID].Cash = br.ReadInt32();
                    PStruct.character[index, ID].Hue = br.ReadInt32();
                    PStruct.character[index, ID].Gender = br.ReadInt32();
                    PStruct.character[index, ID].PVPChangeTimer = br.ReadInt64();
                    PStruct.character[index, ID].PVPBanTimer = br.ReadInt64();
                    PStruct.character[index, ID].PVPPenalty = br.ReadInt64();
                    PStruct.character[index, ID].PVP = br.ReadBoolean();

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    int questcount = br.ReadInt32();
                    int g = 0;
                    int z = 0;

                    for (int q = 1; q <= questcount; q++)
                    {
                        {
                            g = br.ReadInt32();
                            z = br.ReadInt32();
                            PStruct.queststatus[index, g, z].status = br.ReadInt32();
                            for (int k = 1; k < Globals.MaxQuestKills; k++)
                            {
                                PStruct.questkills[index, g, z, k].kills = br.ReadInt32();
                            }
                            for (int a = 1; a < Globals.MaxQuestActions; a++)
                            {
                                PStruct.questactions[index, g, z, a].actiondone = br.ReadBoolean();
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        PStruct.character[index, ID].Chest[i] = br.ReadBoolean();
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        PStruct.character[index, ID].Prof_Type[i] = br.ReadInt32();
                        PStruct.character[index, ID].Prof_Level[i] = br.ReadInt32();
                        PStruct.character[index, ID].Prof_Exp[i] = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        PStruct.skill[index, i].num = br.ReadInt32();
                        PStruct.skill[index, i].level = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PStruct.invslot[index, i].item = br.ReadString();
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].type = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].num = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].refin = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].value = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].exp = br.ReadInt32();
                        PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].price = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PStruct.hotkey[index, i].type = br.ReadByte();
                        PStruct.hotkey[index, i].num = br.ReadInt32();
                    }

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed);return false; }
        }
        //*********************************************************************************************
        // UpdateCharData / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool UpdateCharData(string user, string name, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, name, data) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, user, name, data));
            }

            //CÓDIGO
            try
            {
                //DeleteChar(name, data[0]);
                //CreateNewChar(user, name, data);
                return true;
            }
            catch { return false; }
        }
        //*********************************************************************************************
        // SaveCharacter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void SaveCharacter(int index, string email, int ID, bool isnewchar = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, email, ID, isnewchar) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].CharacterName != null)
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID)) == true)
                {
                    //representa o arquivo que vamos criar
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + GetCharBySlot(email, ID), FileMode.Create);

                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //Define as váriaveis a serem salvas
                    string charName = (PStruct.character[index, PStruct.player[index].SelectedChar].CharacterName);
                    int charSpriteindex = (PStruct.character[index, PStruct.player[index].SelectedChar].Spriteindex);
                    int charClass = (PStruct.character[index, PStruct.player[index].SelectedChar].ClassId);
                    string charSprite = (PStruct.character[index, PStruct.player[index].SelectedChar].Sprite);
                    int charLevel = (PStruct.character[index, PStruct.player[index].SelectedChar].Level);
                    int charExp = (PStruct.character[index, PStruct.player[index].SelectedChar].Exp);
                    int charFire = (PStruct.character[index, PStruct.player[index].SelectedChar].Fire);
                    int charEarth = (PStruct.character[index, PStruct.player[index].SelectedChar].Earth);
                    int charWater = (PStruct.character[index, PStruct.player[index].SelectedChar].Water);
                    int charWind = (PStruct.character[index, PStruct.player[index].SelectedChar].Wind);
                    int charDark = (PStruct.character[index, PStruct.player[index].SelectedChar].Dark);
                    int charLight = (PStruct.character[index, PStruct.player[index].SelectedChar].Light);
                    int charPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].Points);
                    int charMap = (PStruct.character[index, PStruct.player[index].SelectedChar].Map);
                    byte charX = (PStruct.character[index, PStruct.player[index].SelectedChar].X);
                    byte charY = (PStruct.character[index, PStruct.player[index].SelectedChar].Y);
                    int charBootMap = (PStruct.character[index, PStruct.player[index].SelectedChar].BootMap);
                    byte charBootX = (PStruct.character[index, PStruct.player[index].SelectedChar].BootX);
                    byte charBootY = (PStruct.character[index, PStruct.player[index].SelectedChar].BootY);
                    byte charDir = (PStruct.character[index, PStruct.player[index].SelectedChar].Dir);
                    string Equipment = (PStruct.character[index, PStruct.player[index].SelectedChar].Equipment);
                    int Vitality = (PStruct.tempplayer[Convert.ToInt32(index)].Vitality);
                    int Spirit = (PStruct.tempplayer[Convert.ToInt32(index)].Spirit);
                    int Access = (PStruct.character[index, PStruct.player[index].SelectedChar].Access);
                    int SkillPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].SkillPoints);
                    int Gold = (PStruct.character[index, PStruct.player[index].SelectedChar].Gold);
                    int Cash = (PStruct.character[index, PStruct.player[index].SelectedChar].Cash);
                    int Hue = (PStruct.character[index, PStruct.player[index].SelectedChar].Hue);
                    int Gender = (PStruct.character[index, PStruct.player[index].SelectedChar].Gender);
                    int Guild = (PStruct.character[index, PStruct.player[index].SelectedChar].Guild);
                    
                    //PVP
                    long PVPChangeTimer;
                    long PVPBanTimer;
                    long PVPPenalty;
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPChangeTimer > 0)
                    {
                        PVPChangeTimer = PStruct.character[index, PStruct.player[index].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPChangeTimer < 0) { PVPChangeTimer = 0; }
                    }
                    else {  PVPChangeTimer = 0; }
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPBanTimer > 0)
                    {
                        PVPBanTimer = PStruct.character[index, PStruct.player[index].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPBanTimer < 0) { PVPBanTimer = 0; }
                    }
                    else { PVPBanTimer = 0; }
                    if (PStruct.character[index, PStruct.player[index].SelectedChar].PVPPenalty > 0)
                    {
                        PVPPenalty = PStruct.character[index, PStruct.player[index].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPPenalty < 0) { PVPPenalty = 0; }
                    }
                    else { PVPPenalty = 0; }

                    bool PVP = PStruct.character[index, PStruct.player[index].SelectedChar].PVP;

                    //QUEST
                    int questcount = PStruct.GetPlayerQuestsCount(index);
                    
                    if (isnewchar)
                    {
                        int totalpoints = Convert.ToInt32(charFire) + Convert.ToInt32(charEarth) + Convert.ToInt32(charWater) + Convert.ToInt32(charWind) + Convert.ToInt32(charDark) + Convert.ToInt32(charLight);
                        if (totalpoints != 16) { return; }
                    }

                    //grava os dados no arquivo
                    bw.Write(charName);
                    bw.Write(Guild);
                    bw.Write(charSpriteindex);
                    bw.Write(charClass);
                    bw.Write(charSprite);
                    bw.Write(charLevel);
                    bw.Write(charExp);
                    bw.Write(charFire);
                    bw.Write(charEarth);
                    bw.Write(charWater);
                    bw.Write(charWind);
                    bw.Write(charDark);
                    bw.Write(charLight);
                    bw.Write(charPoints);
                    bw.Write(charMap);
                    bw.Write(charX);
                    bw.Write(charY);
                    bw.Write(charBootMap);
                    bw.Write(charBootX);
                    bw.Write(charBootY);
                    bw.Write(charDir);
                    bw.Write(Equipment);
                    bw.Write(Vitality);
                    bw.Write(Spirit);
                    bw.Write(Access);
                    bw.Write(SkillPoints);
                    bw.Write(Gold);
                    bw.Write(Cash);
                    bw.Write(Hue);
                    bw.Write(Gender);
                    bw.Write(PVPChangeTimer);
                    bw.Write(PVPBanTimer);
                    bw.Write(PVPPenalty);
                    bw.Write(PVP);

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    bw.Write(questcount);

                    for (int g = 1; g < Globals.MaxQuestGivers; g++)
                    {
                        for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                        {
                            if (PStruct.queststatus[index, g, q].status != 0)
                            {
                                bw.Write(g);
                                bw.Write(q);
                                bw.Write(PStruct.queststatus[index, g, q].status);
                                for (int k = 1; k < Globals.MaxQuestKills; k++)
                                {
                                    bw.Write(PStruct.questkills[index, g, q, k].kills);
                                }
                                for (int a = 1; a < Globals.MaxQuestActions; a++)
                                {
                                    bw.Write(PStruct.questactions[index, g, q, a].actiondone);
                                }
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Type[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[i]);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PStruct.skill[index, i].num);
                        bw.Write(PStruct.skill[index, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PStruct.invslot[index, i].item);
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].type);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].num);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].refin);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].value);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].exp);
                        bw.Write(PStruct.character[index, PStruct.player[index].SelectedChar].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PStruct.hotkey[index, i].type);
                        bw.Write(PStruct.hotkey[index, i].num);
                    }

                    bw.Close();
                }
            }
        }

        #endregion

        #region Map
        //*********************************************************************************************
        // SaveMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveMap(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, mapnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            //Salvamos o tamanho do mapa primeiro
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].name);
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].max_width);
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].max_height);
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].guildnum);
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].guildgold);
            bw.Write(MStruct.map[Convert.ToInt32(mapnum)].guildmember);

            //Salvamos os tiles em seguida
            for (int x = 0; x <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                for (int y = 0; y <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                {

                    {
                        bw.Write(MStruct.tile[mapnum, x, y].Event_Id);
                        bw.Write(MStruct.tile[mapnum, x, y].Data1);
                        bw.Write(MStruct.tile[mapnum, x, y].Data2);
                        bw.Write(MStruct.tile[mapnum, x, y].Data3);
                        bw.Write(MStruct.tile[mapnum, x, y].Data4);
                        bw.Write(MStruct.tile[mapnum, x, y].DownBlock);
                        bw.Write(MStruct.tile[mapnum, x, y].LeftBlock);
                        bw.Write(MStruct.tile[mapnum, x, y].RightBlock);
                        bw.Write(MStruct.tile[mapnum, x, y].UpBlock);
                    }

                }

            bw.Close();
            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // LoadMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadMap(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, mapnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê o tamanho do mapa
                MStruct.map[Convert.ToInt32(mapnum)].name = br.ReadString();
                MStruct.map[Convert.ToInt32(mapnum)].max_width = br.ReadString();
                MStruct.map[Convert.ToInt32(mapnum)].max_height = br.ReadString();
                MStruct.map[Convert.ToInt32(mapnum)].guildnum = br.ReadInt32();
                MStruct.map[Convert.ToInt32(mapnum)].guildgold = br.ReadInt32();
                MStruct.map[Convert.ToInt32(mapnum)].guildmember = br.ReadString();

                //Carregamos os tiles em seguida
                for (int x = 0; x <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                    for (int y = 0; y <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                    {
                        {
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].Event_Id = br.ReadInt32();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2 = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].Data3 = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].Data4 = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].DownBlock = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].LeftBlock = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].RightBlock = br.ReadString();
                            MStruct.tile[Convert.ToInt32(mapnum), x, y].UpBlock = br.ReadString();
                            
                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "30")
                            {
                                int e = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);
                                string[] notedata = EStruct.enemy[e].note.Split(',');
                                for (int i2 = 1; i2 <= Globals.MaxMapNpcs; i2++)
                                {
                                    if (String.IsNullOrEmpty(NStruct.npc[mapnum, i2].Name))
                                    {
                                        NStruct.npc[mapnum, i2].Name = EStruct.enemy[e].battler_name;
                                        NStruct.npc[mapnum, i2].Map = mapnum;
                                        NStruct.npc[mapnum, i2].X = x;
                                        NStruct.npc[mapnum, i2].Y = y;
                                        NStruct.npc[mapnum, i2].Vitality = Convert.ToInt32(EStruct.enemyparams[e, 0].value);
                                        NStruct.npc[mapnum, i2].Spirit = Convert.ToInt32(EStruct.enemyparams[e, 1].value);
                                        NStruct.tempnpc[mapnum, i2].Target = 0;
                                        NStruct.tempnpc[mapnum, i2].X = x;
                                        NStruct.tempnpc[mapnum, i2].Y = y;
                                        NStruct.tempnpc[mapnum, i2].curTargetX = NStruct.npc[mapnum, i2].X;
                                        NStruct.tempnpc[mapnum, i2].curTargetY = NStruct.npc[mapnum, i2].Y;
                                        NStruct.tempnpc[mapnum, i2].Vitality = NStruct.npc[mapnum, i2].Vitality;
                                        NStruct.npc[mapnum, i2].Attack = Convert.ToInt32(EStruct.enemyparams[e, 2].value);
                                        NStruct.npc[mapnum, i2].Defense = Convert.ToInt32(EStruct.enemyparams[e, 6].value); ;
                                        NStruct.npc[mapnum, i2].Agility = Convert.ToInt32(EStruct.enemyparams[e, 7].value); ;
                                        NStruct.npc[mapnum, i2].MagicDefense = Convert.ToInt32(EStruct.enemyparams[e, 5].value);
                                        NStruct.npc[mapnum, i2].MagicAttack = Convert.ToInt32(EStruct.enemyparams[e, 4].value);
                                        NStruct.npc[mapnum, i2].Luck = Convert.ToInt32(EStruct.enemyparams[e, 3].value);
                                        NStruct.npc[mapnum, i2].Sprite = notedata[0];
                                        NStruct.npc[mapnum, i2].index = Convert.ToInt32(notedata[1]);
                                        NStruct.npc[mapnum, i2].Type = Convert.ToInt32(notedata[6]);
                                        NStruct.npc[mapnum, i2].Range = 1;

                                        if (notedata.Length - 1 > 11)
                                        {
                                            NStruct.npc[mapnum, i2].KnockAttack = Convert.ToBoolean(notedata[12]);
                                            NStruct.npc[mapnum, i2].KnockRange = Convert.ToInt32(notedata[13]);
                                            NStruct.nspell[mapnum, i2, 1].spellnum = Convert.ToInt32(notedata[14]);
                                            NStruct.nspell[mapnum, i2, 2].spellnum = Convert.ToInt32(notedata[15]);
                                            NStruct.nspell[mapnum, i2, 3].spellnum = Convert.ToInt32(notedata[16]);
                                            NStruct.nspell[mapnum, i2, 4].spellnum = Convert.ToInt32(notedata[17]);
                                        }

                                        NStruct.npc[mapnum, i2].Animation = Convert.ToInt32(notedata[8]);
                                        NStruct.npc[mapnum, i2].SpeedMove = Convert.ToInt32(notedata[5]);
                                        NStruct.tempnpc[mapnum, i2].movespeed = NStruct.npc[mapnum, i2].SpeedMove / 64;
                                        NStruct.npc[mapnum, i2].Respawn = Convert.ToInt32(notedata[7]) * 10;
                                        NStruct.npc[mapnum, i2].Exp = EStruct.enemy[e].exp;
                                        NStruct.npc[mapnum, i2].Gold = EStruct.enemy[e].gold;
                                        NStruct.npcdrop[mapnum, i2, 0].ItemNum = EStruct.enemydrops[e, 0].data_id;
                                        NStruct.npcdrop[mapnum, i2, 0].ItemType = EStruct.enemydrops[e, 0].kind;
                                        NStruct.npcdrop[mapnum, i2, 0].Chance = Convert.ToInt32(EStruct.enemydrops[e, 0].denominator);
                                        NStruct.npcdrop[mapnum, i2, 0].Value = 1;
                                        NStruct.npcdrop[mapnum, i2, 1].ItemNum = EStruct.enemydrops[e, 1].data_id;
                                        NStruct.npcdrop[mapnum, i2, 1].ItemType = EStruct.enemydrops[e, 1].kind;
                                        NStruct.npcdrop[mapnum, i2, 1].Chance = Convert.ToInt32(EStruct.enemydrops[e, 1].denominator);
                                        NStruct.npcdrop[mapnum, i2, 1].Value = 1;
                                        NStruct.npcdrop[mapnum, i2, 2].ItemNum = EStruct.enemydrops[e, 2].data_id;
                                        NStruct.npcdrop[mapnum, i2, 2].ItemType = EStruct.enemydrops[e, 2].kind;
                                        NStruct.npcdrop[mapnum, i2, 2].Chance = Convert.ToInt32(EStruct.enemydrops[e, 2].denominator);
                                        NStruct.npcdrop[mapnum, i2, 2].Value = 1;
                                        NStruct.tempnpc[mapnum, i2].prev_move = new NStruct.Point[7];
                                        break;
                                    }
                                }
                            }



                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "22")
                            {
                                int tppoint = MStruct.GetOpenTpPoint();
                                MStruct.tppoint[tppoint].map = mapnum;
                                MStruct.tppoint[tppoint].cost = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data4);

                                int tp_count = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);

                                MStruct.tppoint[tppoint].tp_map = new int[tp_count];
                                MStruct.tppoint[tppoint].tp_x = new byte[tp_count];
                                MStruct.tppoint[tppoint].tp_y = new byte[tp_count];

                                string[] tp_data = MStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(':');

                                MStruct.tppoint[tppoint].count = tp_data.Length;

                                for (int i = 0; i < tp_data.Length; i++)
                                {
                                    MStruct.tppoint[tppoint].tp_map[i] = Convert.ToInt32(tp_data[i].Split(',')[0]);
                                    MStruct.tppoint[tppoint].tp_x[i] = Convert.ToByte(tp_data[i].Split(',')[1]);
                                    MStruct.tppoint[tppoint].tp_y[i] = Convert.ToByte(tp_data[i].Split(',')[2]);

                                }
                            }

                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "23")
                            {
                                int savepoint = MStruct.GetOpenSavePoint();
                                MStruct.savepoint[savepoint].map = mapnum;

                                string[] save_data = MStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(',');

                                MStruct.savepoint[savepoint].save_map = Convert.ToInt32(save_data[0]);
                                MStruct.savepoint[savepoint].save_x = Convert.ToByte(save_data[1]);
                                MStruct.savepoint[savepoint].save_y = Convert.ToByte(save_data[2]);
                            }

  

                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "14")
                            {
                                int bankpoint = MStruct.GetOpenBankPoint();
                                MStruct.bankpoint[bankpoint].map = mapnum;
                            }

                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "15")
                            {
                                int craftpoint = MStruct.GetOpenCraftPoint();
                                MStruct.craftpoint[craftpoint].map = mapnum;
                                MStruct.craftpoint[craftpoint].type = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);
                            }

                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "17")
                            {
                                int workpoint = MStruct.GetOpenWorkPoint();
                                MStruct.workpoint[workpoint].map = mapnum;
                                MStruct.workpoint[workpoint].x = x;
                                MStruct.workpoint[workpoint].y = y;
                                MStruct.workpoint[workpoint].req_tool = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data2.Split(',')[0]);
                                MStruct.workpoint[workpoint].type = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data2.Split(',')[1]);
                                MStruct.workpoint[workpoint].vitality = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data3.Split(',')[0]);
                                MStruct.workpoint[workpoint].exp = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data3.Split(',')[1]);
                                MStruct.workpoint[workpoint].level_req = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data3.Split(',')[2]);
                                MStruct.workpoint[workpoint].reward = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data4.Split(',')[0]);
                                MStruct.workpoint[workpoint].respawn_timer = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data4.Split(',')[1]);
                                MStruct.workpoint[workpoint].active_sprite = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data4.Split(',')[2]);
                                MStruct.workpoint[workpoint].inactive_sprite = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data4.Split(',')[3]);
                                MStruct.tempworkpoint[workpoint].vitality = MStruct.workpoint[workpoint].vitality;
                                MStruct.tempworkpoint[workpoint].respawn = 0;
                            }

                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "18")
                            {
                                int chestpoint = MStruct.GetOpenChestPoint();
                                MStruct.chestpoint[chestpoint].map = mapnum;
                                MStruct.chestpoint[chestpoint].x = x;
                                MStruct.chestpoint[chestpoint].y = y;
                                MStruct.chestpoint[chestpoint].active_sprite = MStruct.tile[mapnum, x, y].Data2.Split(',')[0];
                                MStruct.chestpoint[chestpoint].active_sprite_index = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data2.Split(',')[1]);
                                MStruct.chestpoint[chestpoint].inactive_sprite = MStruct.tile[mapnum, x, y].Data2.Split(',')[2];
                                MStruct.chestpoint[chestpoint].inactive_sprite_index = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data2.Split(',')[3]);
                                if (MStruct.tile[mapnum, x, y].Data3.Contains('?'))
                                {
                                    //MStruct.chestpoint[chestpoint].is_random = true;
                                    //MStruct.chestpoint[chestpoint].reward = new string[0];
                                    //MStruct.chestpoint[chestpoint].reward[0] = MStruct.tile[mapnum, x, y].Data3.Split('?')[0];
                                }
                                else
                                {
                                    MStruct.chestpoint[chestpoint].is_random = false;
                                    MStruct.chestpoint[chestpoint].reward_count = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data3.Split(',')[0]);
                                    MStruct.chestpoint[chestpoint].reward = new string[MStruct.chestpoint[chestpoint].reward_count + 1];

                                    int reader = 1;
                                    for (int i = 1; i <= MStruct.chestpoint[chestpoint].reward_count; i++)
                                    {
                                        MStruct.chestpoint[chestpoint].reward[i] += MStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MStruct.chestpoint[chestpoint].reward[i] += MStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MStruct.chestpoint[chestpoint].reward[i] += MStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MStruct.chestpoint[chestpoint].reward[i] += MStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                    }
                                }
                                MStruct.chestpoint[chestpoint].key = Convert.ToInt32(MStruct.tile[mapnum, x, y].Data4);
                            }

                            //QUESTS
                            if (MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "10")
                            {
                                string[] questdata = MStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(':');
                                int questgiver = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data4);

                                //Informações de onde fica nosso "quest giver"
                                MStruct.questgiver[questgiver].map = mapnum;
                                MStruct.questgiver[questgiver].x = x;
                                MStruct.questgiver[questgiver].y = y;
                                MStruct.questgiver[questgiver].quest_count = Convert.ToInt32(MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);

                                int reader = 0;

                                for (int q = 1; q <= MStruct.questgiver[questgiver].quest_count; q++)                 
                                {

                                    MStruct.quest[questgiver, q].type = questdata[reader];

                                    int typekill = Convert.ToInt32(questdata[reader].Split('|')[0]);
                                    int typeaction = Convert.ToInt32(questdata[reader].Split('|')[1]);
                                    int typeitem = Convert.ToInt32(questdata[reader].Split('|')[2]);

                                    reader += 1;

                                    int killvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    int actionvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    int itemvalue = Convert.ToInt32(questdata[reader]); reader += 1;


                                    if (typekill > 0)
                                    {
                                        MStruct.quest[questgiver, q].killvalue = killvalue;
                                        for (int k = 1; k <= Convert.ToInt32(MStruct.quest[questgiver, q].killvalue); k++)
                                        {
                                            MStruct.questkills[questgiver, q, k].monstername = questdata[reader]; reader += 1;
                                            MStruct.questkills[questgiver, q, k].value = Convert.ToInt32(questdata[reader]); reader += 1;
                                        }
                                    }
                                    if (typeaction > 0)
                                    {
                                         MStruct.quest[questgiver, q].actionvalue = actionvalue;
                                       
                                        for (int a = 1; a <= Convert.ToInt32( MStruct.quest[questgiver, q].actionvalue); a++)
                                        {
                                            MStruct.questactions[questgiver, q, a].type = Convert.ToInt32(questdata[reader]); reader += 1;
                                            MStruct.questactions[questgiver, q, a].data = questdata[reader]; reader += 1;
                                        }
                                    }
                                    if (typeitem > 0)
                                    {
                                        MStruct.quest[questgiver, q].itemvalue = itemvalue;
                                        for (int i = 1; i <= Convert.ToInt32(MStruct.quest[questgiver, q].itemvalue); i++)
                                        {
                                            MStruct.questitems[questgiver, q, i].item = questdata[reader]; reader += 1;
                                        }
                                    }

                                    MStruct.quest[questgiver, q].rewardvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    for (int i = 1; i <= Convert.ToInt32(MStruct.quest[questgiver, q].rewardvalue); i++)
                                    {
                                        MStruct.questrewards[questgiver, q, i].item = questdata[reader]; reader += 1;
                                    }

                                    MStruct.quest[questgiver, q].exp = Convert.ToInt32(questdata[reader]); reader += 1;
                                    MStruct.quest[questgiver, q].gold = Convert.ToInt32(questdata[reader]); reader += 1;

                                }
                            }
                        }

                    }

                //Fecha o leitor
                br.Close();

                if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o mapa foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }
        }
        //*********************************************************************************************
        // ClearMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearMap(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return;
            }

            //CÓDIGO
            //Limpamos o tamanho do mapa
            MStruct.map[Convert.ToInt32(mapnum)].name = "";
            MStruct.map[Convert.ToInt32(mapnum)].max_width = "19";
            MStruct.map[Convert.ToInt32(mapnum)].max_height = "14";
            MStruct.map[Convert.ToInt32(mapnum)].guildnum = 0;
            MStruct.map[Convert.ToInt32(mapnum)].guildgold = 0;
            MStruct.map[Convert.ToInt32(mapnum)].guildmember = "";

            //Limpamos os tiles

            for (int x = 0; x <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                for (int y = 0; y <= Convert.ToInt32(MStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                {
                    {
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].Event_Id = 0;
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 = "0";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].Data2 = "0";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].Data3 = "0";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].Data4 = "0";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].DownBlock = "true";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].LeftBlock = "true";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].RightBlock = "true";
                        MStruct.tile[Convert.ToInt32(mapnum), x, y].UpBlock = "true";
                    }

                }
        }
        //*********************************************************************************************
        // LoadMaps / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadMaps()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxMaps; i++)
            {
                if (LoadMap(i))
                {
                    // okay
                }
                else
                {
                    ClearMap(i);
                    SaveMap(i);
                }
                MStruct.tempmap[i].NpcCount = MStruct.GetMapNpcCount(i);
            }

        }
    #endregion
    
        #region Item
        //*********************************************************************************************
        // LoadItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, itemnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intitemnum = Convert.ToInt32(itemnum);

                //Lê-mos os dados básicos do item
                IStruct.item[intitemnum].name = br.ReadString();
                IStruct.item[intitemnum].price = br.ReadInt32();
                IStruct.item[intitemnum].consumable = br.ReadString();
                IStruct.item[intitemnum].success_rate = br.ReadInt32();
                IStruct.item[intitemnum].animation_id = br.ReadInt32();
                IStruct.item[intitemnum].note = br.ReadString();

                if (IStruct.item[intitemnum].note.Length > 1)
                {
                    string[] data = IStruct.item[intitemnum].note.Split(',');
                    IStruct.itemextra[intitemnum].type = Convert.ToInt32(data[0]);

                    if (IStruct.itemextra[intitemnum].type == 1)
                    {
                        IStruct.itemextra[intitemnum].sprite = data[1];
                        IStruct.itemextra[intitemnum].sprite_index = Convert.ToInt32(data[2]);
                    }
                }

                IStruct.item[intitemnum].speed = br.ReadInt32();
                IStruct.item[intitemnum].repeats = br.ReadInt32();
                IStruct.item[intitemnum].tp_gain = br.ReadInt32();
                IStruct.item[intitemnum].hit_type = br.ReadInt32();
                IStruct.item[intitemnum].effects_count = br.ReadInt32();
                IStruct.item[intitemnum].damage_type = br.ReadInt32();
                IStruct.item[intitemnum].damage_formula = br.ReadString();
                IStruct.item[intitemnum].damage_element = br.ReadInt32();
                IStruct.item[intitemnum].damage_variance = br.ReadInt32();
                IStruct.item[intitemnum].damage_critical = br.ReadString();


                //Carregamos os efeitos em seguida
                for (int i = 0; i <= IStruct.item[intitemnum].effects_count; i++)
                {
                    IStruct.itemeffect[intitemnum, i].code = br.ReadInt32();
                    IStruct.itemeffect[intitemnum, i].data_id = br.ReadInt32();
                    IStruct.itemeffect[intitemnum, i].value1 = br.ReadDouble();
                    IStruct.itemeffect[intitemnum, i].value2 = br.ReadDouble();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }
        }
        //*********************************************************************************************
        // SaveItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, itemnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intitemnum = Convert.ToInt32(itemnum);

            //Salvamos os dados básicos do item
            bw.Write(IStruct.item[intitemnum].name);
            bw.Write(IStruct.item[intitemnum].price);
            bw.Write(IStruct.item[intitemnum].consumable);
            bw.Write(IStruct.item[intitemnum].success_rate);
            bw.Write(IStruct.item[intitemnum].animation_id);
            bw.Write(IStruct.item[intitemnum].note);
            bw.Write(IStruct.item[intitemnum].speed);
            bw.Write(IStruct.item[intitemnum].repeats);
            bw.Write(IStruct.item[intitemnum].tp_gain);
            bw.Write(IStruct.item[intitemnum].hit_type);
            bw.Write(IStruct.item[intitemnum].effects_count);
            bw.Write(IStruct.item[intitemnum].damage_type);
            bw.Write(IStruct.item[intitemnum].damage_formula);
            bw.Write(IStruct.item[intitemnum].damage_element);
            bw.Write(IStruct.item[intitemnum].damage_variance);
            bw.Write(IStruct.item[intitemnum].damage_critical);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= IStruct.item[intitemnum].effects_count; i++)
            {
                bw.Write(IStruct.itemeffect[intitemnum, i].code);
                bw.Write(IStruct.itemeffect[intitemnum, i].data_id);
                bw.Write(IStruct.itemeffect[intitemnum, i].value1);
                bw.Write(IStruct.itemeffect[intitemnum, i].value2);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intitemnum = Convert.ToInt32(itemnum);

            //Limpamos o tamanho do mapa
            IStruct.item[intitemnum].name = "";
            IStruct.item[intitemnum].price = 0;
            IStruct.item[intitemnum].consumable = "";
            IStruct.item[intitemnum].success_rate = 0;
            IStruct.item[intitemnum].animation_id = 0;
            IStruct.item[intitemnum].note = "";
            IStruct.item[intitemnum].speed = 0;
            IStruct.item[intitemnum].repeats = 0;
            IStruct.item[intitemnum].tp_gain = 0;
            IStruct.item[intitemnum].hit_type = 0;
            IStruct.item[intitemnum].damage_type = 0;
            IStruct.item[intitemnum].damage_formula = "";
            IStruct.item[intitemnum].damage_element = 0;
            IStruct.item[intitemnum].damage_variance = 0;
            IStruct.item[intitemnum].damage_critical = "";

            int effects_count = IStruct.item[intitemnum].effects_count;

            IStruct.item[intitemnum].effects_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                IStruct.itemeffect[intitemnum, i].code = 0;
                IStruct.itemeffect[intitemnum, i].data_id = 0;
                IStruct.itemeffect[intitemnum, i].value1 = 0.0;
                IStruct.itemeffect[intitemnum, i].value2 = 0.0;
            }
        }
        //*********************************************************************************************
        // LoadItems / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadItems()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxItems; i++)
            {
                if (LoadItem(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearItem(Convert.ToString(i));
                    SaveItem(Convert.ToString(i));
                }
            }

        }

        #endregion

        #region Weapon
        //*********************************************************************************************
        // LoadWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, weaponnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intweaponnum = Convert.ToInt32(weaponnum);

                //Lê-mos os dados básicos da arma
                WStruct.weapon[intweaponnum].name = br.ReadString();
                WStruct.weapon[intweaponnum].price = br.ReadInt32();
                WStruct.weapon[intweaponnum].etype_id = br.ReadInt32();
                WStruct.weapon[intweaponnum].wtype_id = br.ReadInt32();
                WStruct.weapon[intweaponnum].animation_id = br.ReadInt32();
                WStruct.weapon[intweaponnum].params_size = br.ReadInt32();
                WStruct.weapon[intweaponnum].features_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= WStruct.weapon[intweaponnum].params_size; i++)
                {
                    WStruct.weaponparams[intweaponnum, i].value = br.ReadDouble();
                }

                //Carregamos os features em seguida
                for (int i = 0; i <= WStruct.weapon[intweaponnum].features_size; i++)
                {
                    WStruct.weaponfeatures[intweaponnum, i].code = br.ReadInt32();
                    WStruct.weaponfeatures[intweaponnum, i].data_id = br.ReadInt32();
                    WStruct.weaponfeatures[intweaponnum, i].value = br.ReadInt32();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que a arma não existe
            { return false; }

        }
        //*********************************************************************************************
        // SaveWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, weaponnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intweaponnum = Convert.ToInt32(weaponnum);

            //Salvamos os dados básicos da arma
            bw.Write(WStruct.weapon[intweaponnum].name);
            bw.Write(WStruct.weapon[intweaponnum].price);
            bw.Write(WStruct.weapon[intweaponnum].etype_id);
            bw.Write(WStruct.weapon[intweaponnum].wtype_id);
            bw.Write(WStruct.weapon[intweaponnum].animation_id);
            bw.Write(WStruct.weapon[intweaponnum].params_size);
            bw.Write(WStruct.weapon[intweaponnum].features_size);

            //Salvamos os params das armas
            for (int i = 0; i <= WStruct.weapon[intweaponnum].params_size; i++)
            {
                bw.Write(WStruct.weaponparams[intweaponnum, i].value);
            }

            //Salvamos as features das armas
            for (int i = 0; i <= WStruct.weapon[intweaponnum].features_size; i++)
            {
                bw.Write(WStruct.weaponfeatures[intweaponnum, i].code);
                bw.Write(WStruct.weaponfeatures[intweaponnum, i].data_id);
                bw.Write(WStruct.weaponfeatures[intweaponnum, i].value);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intweaponnum = Convert.ToInt32(weaponnum);

            //Limpamos os dados básicos da arma
            WStruct.weapon[intweaponnum].name = "";
            WStruct.weapon[intweaponnum].price = 0;
            WStruct.weapon[intweaponnum].etype_id = 0;
            WStruct.weapon[intweaponnum].wtype_id = 0;
            WStruct.weapon[intweaponnum].animation_id = 0;

            int params_size = WStruct.weapon[intweaponnum].params_size;
            int features_size = WStruct.weapon[intweaponnum].features_size;

            WStruct.weapon[intweaponnum].params_size = 0;
            WStruct.weapon[intweaponnum].features_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                WStruct.weaponparams[intweaponnum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                WStruct.weaponfeatures[intweaponnum, i].code = 0;
                WStruct.weaponfeatures[intweaponnum, i].data_id = 0;
                WStruct.weaponfeatures[intweaponnum, i].value = 0.0;
            }
        }
        //*********************************************************************************************
        // LoadWeapons / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadWeapons()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxWeapons; i++)
            {
                if (LoadWeapon(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearWeapon(Convert.ToString(i));
                    SaveWeapon(Convert.ToString(i));
                }
            }

        }
        #endregion

        #region Armors
        //*********************************************************************************************
        // LoadArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, armornum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intarmornum = Convert.ToInt32(armornum);

                //Lê-mos os dados básicos da arma
                AStruct.armor[intarmornum].name = br.ReadString();
                AStruct.armor[intarmornum].price = br.ReadInt32();
                AStruct.armor[intarmornum].etype_id = br.ReadInt32();
                AStruct.armor[intarmornum].atype_id = br.ReadInt32();
                AStruct.armor[intarmornum].params_size = br.ReadInt32();
                AStruct.armor[intarmornum].features_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= AStruct.armor[intarmornum].params_size; i++)
                {
                    AStruct.armorparams[intarmornum, i].value = br.ReadDouble();
                }

                //Carregamos os features em seguida
                for (int i = 0; i <= AStruct.armor[intarmornum].features_size; i++)
                {
                    AStruct.armorfeatures[intarmornum, i].code = br.ReadInt32();
                    AStruct.armorfeatures[intarmornum, i].data_id = br.ReadInt32();
                    AStruct.armorfeatures[intarmornum, i].value = br.ReadInt32();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que a arma não existe
            { return false; }

        }
        //*********************************************************************************************
        // SaveArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, armornum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intarmornum = Convert.ToInt32(armornum);

            //Salvamos os dados básicos da arma
            bw.Write(AStruct.armor[intarmornum].name);
            bw.Write(AStruct.armor[intarmornum].price);
            bw.Write(AStruct.armor[intarmornum].etype_id);
            bw.Write(AStruct.armor[intarmornum].atype_id);
            bw.Write(AStruct.armor[intarmornum].params_size);
            bw.Write(AStruct.armor[intarmornum].features_size);

            //Salvamos os params das armas
            for (int i = 0; i <= AStruct.armor[intarmornum].params_size; i++)
            {
                bw.Write(AStruct.armorparams[intarmornum, i].value);
            }

            //Salvamos as features das armas
            for (int i = 0; i <= AStruct.armor[intarmornum].features_size; i++)
            {
                bw.Write(AStruct.armorfeatures[intarmornum, i].code);
                bw.Write(AStruct.armorfeatures[intarmornum, i].data_id);
                bw.Write(AStruct.armorfeatures[intarmornum, i].value);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return;
            }

            //CÓDIGO
            int intarmornum = Convert.ToInt32(armornum);

            //Limpamos os dados básicos da arma
            AStruct.armor[intarmornum].name = "";
            AStruct.armor[intarmornum].price = 0;
            AStruct.armor[intarmornum].etype_id = 0;
            AStruct.armor[intarmornum].atype_id = 0;

            int params_size = AStruct.armor[intarmornum].params_size;
            int features_size = AStruct.armor[intarmornum].features_size;

            AStruct.armor[intarmornum].params_size = 0;
            AStruct.armor[intarmornum].features_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                AStruct.armorparams[intarmornum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                AStruct.armorfeatures[intarmornum, i].code = 0;
                AStruct.armorfeatures[intarmornum, i].data_id = 0;
                AStruct.armorfeatures[intarmornum, i].value = 0.0;
            }
        }
        //*********************************************************************************************
        // LoadArmors / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadArmors()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxArmors; i++)
            {
                if (LoadArmor(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearArmor(Convert.ToString(i));
                    SaveArmor(Convert.ToString(i));
                }
            }

        }
        #endregion

        #region Skill
        //*********************************************************************************************
        // LoadSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, skillnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intskillnum = Convert.ToInt32(skillnum);

                //Lê-mos os dados básicos do item
                SStruct.skill[intskillnum].scope = br.ReadInt32();
                SStruct.skill[intskillnum].stype_id = br.ReadInt32();
                SStruct.skill[intskillnum].mp_cost = br.ReadInt32();
                SStruct.skill[intskillnum].tp_cost = br.ReadInt32();
                SStruct.skill[intskillnum].message1 = br.ReadString();
                SStruct.skill[intskillnum].message2 = br.ReadString();
                SStruct.skill[intskillnum].required_wtype_id1 = br.ReadInt32();
                SStruct.skill[intskillnum].required_wtype_id2 = br.ReadInt32();
                SStruct.skill[intskillnum].occasion = br.ReadInt32();
                SStruct.skill[intskillnum].success_rate = br.ReadInt32();
                SStruct.skill[intskillnum].repeats = br.ReadInt32();
                SStruct.skill[intskillnum].tp_gain = br.ReadInt32();
                SStruct.skill[intskillnum].hit_type = br.ReadInt32();
                SStruct.skill[intskillnum].animation_id = br.ReadInt32();
                SStruct.skill[intskillnum].speed = br.ReadInt32();
                SStruct.skill[intskillnum].note = br.ReadString();
                SStruct.skill[intskillnum].damage_type = br.ReadInt32();
                SStruct.skill[intskillnum].damage_formula = br.ReadString();
                SStruct.skill[intskillnum].damage_element = br.ReadInt32();
                SStruct.skill[intskillnum].damage_variance = br.ReadInt32();
                SStruct.skill[intskillnum].damage_critical = br.ReadString();
                SStruct.skill[intskillnum].effects_count = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 0; i <= SStruct.skill[intskillnum].effects_count; i++)
                {
                    SStruct.skilleffect[intskillnum, i].code = br.ReadInt32();
                    SStruct.skilleffect[intskillnum, i].data_id = br.ReadInt32();
                    SStruct.skilleffect[intskillnum, i].value1 = br.ReadDouble();
                    SStruct.skilleffect[intskillnum, i].value2 = br.ReadDouble();
                }

                //Fecha o leitor
                br.Close();

                if (String.IsNullOrEmpty(SStruct.skill[intskillnum].note)) { return true; }

                if ((SStruct.skill[intskillnum].repeats <= 1))
                {
                    SStruct.skill[intskillnum].type = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[0]);
                    SStruct.skill[intskillnum].range_effect = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[1]);
                    if (SStruct.skill[intskillnum].type == 10)
                    {
                        SStruct.skill[intskillnum].passive_type = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[2]);
                        SStruct.skill[intskillnum].passive_chance = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[3]);
                        SStruct.skill[intskillnum].passive_multiplier = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[4]);
                        SStruct.skill[intskillnum].passive_interval = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[5]);
                    }
                    if ((SStruct.skill[intskillnum].type == 7) || (SStruct.skill[intskillnum].type == 8) || (SStruct.skill[intskillnum].type == 13))
                    {
                        SStruct.skill[intskillnum].interval = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[2]);
                    }
                    if (SStruct.skill[intskillnum].type == 9)
                    {
                        SStruct.skill[intskillnum].drain = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[2]);
                    }
                }
                else
                {
                    SStruct.skill[intskillnum].type = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[0]);
                    SStruct.skill[intskillnum].second_anim = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[1]);
                    SStruct.skill[intskillnum].interval = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[2]);
                    SStruct.skill[intskillnum].range_effect = Convert.ToInt32(SStruct.skill[intskillnum].note.Split(',')[3]);
                    SStruct.skill[intskillnum].is_line = Convert.ToBoolean(SStruct.skill[intskillnum].note.Split(',')[4]);
                    SStruct.skill[intskillnum].slow = Convert.ToBoolean(SStruct.skill[intskillnum].note.Split(',')[5]);
                }


                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }

        }
        //*********************************************************************************************
        // SaveSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, skillnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intskillnum = Convert.ToInt32(skillnum);

            //Salvamos os dados básicos do item
            bw.Write(SStruct.skill[intskillnum].scope);
            bw.Write(SStruct.skill[intskillnum].stype_id);
            bw.Write(SStruct.skill[intskillnum].mp_cost);
            bw.Write(SStruct.skill[intskillnum].tp_cost);
            bw.Write(SStruct.skill[intskillnum].message1);
            bw.Write(SStruct.skill[intskillnum].message2);
            bw.Write(SStruct.skill[intskillnum].required_wtype_id1);
            bw.Write(SStruct.skill[intskillnum].required_wtype_id2);
            bw.Write(SStruct.skill[intskillnum].occasion);
            bw.Write(SStruct.skill[intskillnum].success_rate);
            bw.Write(SStruct.skill[intskillnum].repeats);
            bw.Write(SStruct.skill[intskillnum].tp_gain);
            bw.Write(SStruct.skill[intskillnum].hit_type);
            bw.Write(SStruct.skill[intskillnum].animation_id);
            bw.Write(SStruct.skill[intskillnum].speed);
            bw.Write(SStruct.skill[intskillnum].note);
            bw.Write(SStruct.skill[intskillnum].damage_type);
            bw.Write(SStruct.skill[intskillnum].damage_formula);
            bw.Write(SStruct.skill[intskillnum].damage_element);
            bw.Write(SStruct.skill[intskillnum].damage_variance);
            bw.Write(SStruct.skill[intskillnum].damage_critical);
            bw.Write(SStruct.skill[intskillnum].effects_count);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= SStruct.skill[intskillnum].effects_count; i++)
            {
                bw.Write(SStruct.skilleffect[intskillnum, i].code);
                bw.Write(SStruct.skilleffect[intskillnum, i].data_id);
                bw.Write(SStruct.skilleffect[intskillnum, i].value1);
                bw.Write(SStruct.skilleffect[intskillnum, i].value2);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intskillnum = Convert.ToInt32(skillnum);

            //Limpamos o tamanho do mapa
            SStruct.skill[intskillnum].scope = 0;
            SStruct.skill[intskillnum].stype_id = 0;
            SStruct.skill[intskillnum].mp_cost = 0;
            SStruct.skill[intskillnum].tp_cost = 0;
            SStruct.skill[intskillnum].message1 = "";
            SStruct.skill[intskillnum].message2 = "";
            SStruct.skill[intskillnum].required_wtype_id1 = 0;
            SStruct.skill[intskillnum].required_wtype_id2 = 0;
            SStruct.skill[intskillnum].occasion = 0;
            SStruct.skill[intskillnum].success_rate = 0;
            SStruct.skill[intskillnum].repeats = 0;
            SStruct.skill[intskillnum].tp_gain = 0;
            SStruct.skill[intskillnum].hit_type = 0;
            SStruct.skill[intskillnum].animation_id = 0;
            SStruct.skill[intskillnum].speed = 0;
            SStruct.skill[intskillnum].note = "";
            SStruct.skill[intskillnum].damage_type = 0;
            SStruct.skill[intskillnum].damage_formula = "";
            SStruct.skill[intskillnum].damage_element = 0;
            SStruct.skill[intskillnum].damage_variance = 0;
            SStruct.skill[intskillnum].damage_critical = "false";

            int effects_count = SStruct.skill[intskillnum].effects_count;

            SStruct.skill[intskillnum].effects_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                SStruct.skilleffect[intskillnum, i].code = 0;
                SStruct.skilleffect[intskillnum, i].data_id = 0;
                SStruct.skilleffect[intskillnum, i].value1 = 0.0;
                SStruct.skilleffect[intskillnum, i].value2 = 0.0;
            }
        }
        //*********************************************************************************************
        // LoadSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadSkills()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxSkills; i++)
            {
                if (LoadSkill(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearSkill(Convert.ToString(i));
                    SaveSkill(Convert.ToString(i));
                }
            }

        }

        #endregion

        #region Enemies
        //*********************************************************************************************
        // LoadEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, enemynum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intenemynum = Convert.ToInt32(enemynum);

                //Lê-mos os dados básicos da arma
                EStruct.enemy[intenemynum].battler_name = br.ReadString();
                EStruct.enemy[intenemynum].battler_hue = br.ReadInt32();
                EStruct.enemy[intenemynum].exp = br.ReadInt32();
                EStruct.enemy[intenemynum].gold = br.ReadInt32();
                EStruct.enemy[intenemynum].note = br.ReadString();
                EStruct.enemy[intenemynum].params_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= EStruct.enemy[intenemynum].params_size; i++)
                {
                    EStruct.enemyparams[intenemynum, i].value = br.ReadDouble();
                }

                EStruct.enemy[intenemynum].drops_size = br.ReadInt32();

                //Carregamos os features em seguida
                for (int i = 0; i <= EStruct.enemy[intenemynum].drops_size; i++)
                {
                    EStruct.enemydrops[intenemynum, i].kind = br.ReadInt32();
                    EStruct.enemydrops[intenemynum, i].data_id = br.ReadInt32();
                    EStruct.enemydrops[intenemynum, i].denominator = br.ReadDouble();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que a arma não existe
            { return false; }

        }
        //*********************************************************************************************
        // SaveEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, enemynum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intenemynum = Convert.ToInt32(enemynum);

            //Salvamos os dados básicos do inimigo
            bw.Write(EStruct.enemy[intenemynum].battler_name);
            bw.Write(EStruct.enemy[intenemynum].battler_hue);
            bw.Write(EStruct.enemy[intenemynum].exp);
            bw.Write(EStruct.enemy[intenemynum].gold);
            bw.Write(EStruct.enemy[intenemynum].note);
            bw.Write(EStruct.enemy[intenemynum].params_size);

            //Salvamos os params dos inimigos
            for (int i = 0; i <= EStruct.enemy[intenemynum].params_size; i++)
            {
                bw.Write(EStruct.enemyparams[intenemynum, i].value);
            }

            bw.Write(EStruct.enemy[intenemynum].drops_size);

            //Salvamos as features das armas
            for (int i = 0; i <= EStruct.enemy[intenemynum].drops_size; i++)
            {
                bw.Write(EStruct.enemydrops[intenemynum, i].kind);
                bw.Write(EStruct.enemydrops[intenemynum, i].data_id);
                bw.Write(EStruct.enemydrops[intenemynum, i].denominator);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return;
            }

            //CÓDIGO
            int intenemynum = Convert.ToInt32(enemynum);

            //Limpamos os dados básicos do inimigo
            EStruct.enemy[intenemynum].battler_name = "";
            EStruct.enemy[intenemynum].battler_hue = 0;
            EStruct.enemy[intenemynum].exp = 0;
            EStruct.enemy[intenemynum].gold = 0;
            EStruct.enemy[intenemynum].note = "";

            int params_size = EStruct.enemy[intenemynum].params_size;
            int features_size = EStruct.enemy[intenemynum].drops_size;

            EStruct.enemy[intenemynum].params_size = 0;
            EStruct.enemy[intenemynum].drops_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                EStruct.enemyparams[intenemynum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                EStruct.enemydrops[intenemynum, i].kind = 0;
                EStruct.enemydrops[intenemynum, i].data_id = 0;
                EStruct.enemydrops[intenemynum, i].denominator = 0.0;
            }
        }
        //*********************************************************************************************
        // LoadEnemies / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadEnemies()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.MaxEnemies; i++)
            {
                if (LoadEnemy(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearEnemy(Convert.ToString(i));
                    SaveEnemy(Convert.ToString(i));
                }
            }

        }
        #endregion

        #region Shop
        //*********************************************************************************************
        // LoadShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool LoadShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, shopnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intshopnum = Convert.ToInt32(shopnum);

                //Lê-mos os dados básicos da loja
                ShopStruct.shop[intshopnum].map = br.ReadInt32();
                ShopStruct.shop[intshopnum].x = br.ReadInt32();
                ShopStruct.shop[intshopnum].y = br.ReadInt32();
                ShopStruct.shop[intshopnum].item_count = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 0; i <= ShopStruct.shop[intshopnum].item_count; i++)
                {
                    ShopStruct.shopitem[intshopnum, i].type = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].num = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].value = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].refin = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].price = br.ReadInt32();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MStruct.map[Convert.ToInt32(mapnum)].max_width)) { ClearMap(mapnum); SaveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }
        }
        //*********************************************************************************************
        // SaveShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool SaveShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, shopnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intshopnum = Convert.ToInt32(shopnum);

            //Salvamos os dados básicos do item
            bw.Write(ShopStruct.shop[intshopnum].map);
            bw.Write(ShopStruct.shop[intshopnum].x);
            bw.Write(ShopStruct.shop[intshopnum].y);
            bw.Write(ShopStruct.shop[intshopnum].item_count);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= ShopStruct.shop[intshopnum].item_count; i++)
            {
                bw.Write(ShopStruct.shopitem[intshopnum, i].type);
                bw.Write(ShopStruct.shopitem[intshopnum, i].num);
                bw.Write(ShopStruct.shopitem[intshopnum, i].value);
                bw.Write(ShopStruct.shopitem[intshopnum, i].refin);
                bw.Write(ShopStruct.shopitem[intshopnum, i].price);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // ClearShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ClearShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intshopnum = Convert.ToInt32(shopnum);

            //Limpamos o tamanho do mapa
            ShopStruct.shop[intshopnum].map = 0;
            ShopStruct.shop[intshopnum].x = 0;
            ShopStruct.shop[intshopnum].y = 0;

            int effects_count = ShopStruct.shop[intshopnum].item_count;

            ShopStruct.shop[intshopnum].item_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                ShopStruct.shopitem[intshopnum, i].type = 0;
                ShopStruct.shopitem[intshopnum, i].num = 0;
                ShopStruct.shopitem[intshopnum, i].value = 0;
                ShopStruct.shopitem[intshopnum, i].refin = 0;
                ShopStruct.shopitem[intshopnum, i].price = 0;
            }
        }
        //*********************************************************************************************
        // LoadShops / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadShops()
        {   
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.Max_Shops; i++)
            {
                if (LoadShop(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearShop(Convert.ToString(i));
                    SaveShop(Convert.ToString(i));
                }
            }

        }

        #endregion
        //*********************************************************************************************
        // LoadShopsRud / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadShopsRud()
        {
            //Manual, no editor, sorry :/
            ShopStruct.shop[1].map = 3;
            ShopStruct.shop[1].x = 9;
            ShopStruct.shop[1].y = 7;
            ShopStruct.shop[1].item_count = 2;

            ShopStruct.shopitem[1, 1].type = 1;
            ShopStruct.shopitem[1, 1].num = 1;
            ShopStruct.shopitem[1, 1].value = 1;
            ShopStruct.shopitem[1, 1].refin = 0;
            ShopStruct.shopitem[1, 1].price = 75;

            ShopStruct.shopitem[1, 2].type = 1;
            ShopStruct.shopitem[1, 2].num = 2;
            ShopStruct.shopitem[1, 2].value = 1;
            ShopStruct.shopitem[1, 2].refin = 0;
            ShopStruct.shopitem[1, 2].price = 75;

            //Manual, no editor, sorry :/
            ShopStruct.shop[2].map = 132;
            ShopStruct.shop[2].x = 13;
            ShopStruct.shop[2].y = 7;
            ShopStruct.shop[2].item_count = 1;

            ShopStruct.shopitem[2, 1].type = 1;
            ShopStruct.shopitem[2, 1].num = 50;
            ShopStruct.shopitem[2, 1].value = 1;
            ShopStruct.shopitem[2, 1].refin = 0;
            ShopStruct.shopitem[2, 1].price = 25;

            //Manual, no editor, sorry :/
            ShopStruct.shop[3].map = 136;
            ShopStruct.shop[3].x = 12;
            ShopStruct.shop[3].y = 10;
            ShopStruct.shop[3].item_count = 1;

            ShopStruct.shopitem[3, 1].type = 2;
            ShopStruct.shopitem[3, 1].num = 28;
            ShopStruct.shopitem[3, 1].value = 1;
            ShopStruct.shopitem[3, 1].refin = 0;
            ShopStruct.shopitem[3, 1].price = 1;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[4].map = 77;
            ShopStruct.shop[4].x = 13;
            ShopStruct.shop[4].y = 3;
            ShopStruct.shop[4].item_count = 1;

            ShopStruct.shopitem[4, 1].type = 1;
            ShopStruct.shopitem[4, 1].num = 5;
            ShopStruct.shopitem[4, 1].value = 1;
            ShopStruct.shopitem[4, 1].refin = 0;
            ShopStruct.shopitem[4, 1].price = 100;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[5].map = 148;
            ShopStruct.shop[5].x = 9;
            ShopStruct.shop[5].y = 7;
            ShopStruct.shop[5].item_count = 1;

            ShopStruct.shopitem[5, 1].type = 2;
            ShopStruct.shopitem[5, 1].num = 31;
            ShopStruct.shopitem[5, 1].value = 1;
            ShopStruct.shopitem[5, 1].refin = 0;
            ShopStruct.shopitem[5, 1].price = 1;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[6].map = 149;
            ShopStruct.shop[6].x = 10;
            ShopStruct.shop[6].y = 6;
            ShopStruct.shop[6].item_count = 1;

            ShopStruct.shopitem[6, 1].type = 1;
            ShopStruct.shopitem[6, 1].num = 47;
            ShopStruct.shopitem[6, 1].value = 1;
            ShopStruct.shopitem[6, 1].refin = 0;
            ShopStruct.shopitem[6, 1].price = 50;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[7].map = 0;
            ShopStruct.shop[7].x = 0;
            ShopStruct.shop[7].y = 0;
            ShopStruct.shop[7].item_count = 3;

            ShopStruct.shopitem[7, 1].type = 1;
            ShopStruct.shopitem[7, 1].num = 68;
            ShopStruct.shopitem[7, 1].value = 1;
            ShopStruct.shopitem[7, 1].refin = 0;
            ShopStruct.shopitem[7, 1].price = 5;// 225;

            ShopStruct.shopitem[7, 2].type = 1;
            ShopStruct.shopitem[7, 2].num = 70;
            ShopStruct.shopitem[7, 2].value = 1;
            ShopStruct.shopitem[7, 2].refin = 0;
            ShopStruct.shopitem[7, 2].price = 10;// 225;

            ShopStruct.shopitem[7, 3].type = 1;
            ShopStruct.shopitem[7, 3].num = 71;
            ShopStruct.shopitem[7, 3].value = 1;
            ShopStruct.shopitem[7, 3].refin = 0;
            ShopStruct.shopitem[7, 3].price = 10;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[8].map = 13;
            ShopStruct.shop[8].x = 5;
            ShopStruct.shop[8].y = 7;
            ShopStruct.shop[8].item_count = 1;

            ShopStruct.shopitem[8, 1].type = 1;
            ShopStruct.shopitem[8, 1].num = 69;
            ShopStruct.shopitem[8, 1].value = 1;
            ShopStruct.shopitem[8, 1].refin = 0;
            ShopStruct.shopitem[8, 1].price = 500;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[9].map = 167;
            ShopStruct.shop[9].x = 11;
            ShopStruct.shop[9].y = 6;
            ShopStruct.shop[9].item_count = 11;

            ShopStruct.shopitem[9, 1].type = 2;
            ShopStruct.shopitem[9, 1].num = 2;
            ShopStruct.shopitem[9, 1].value = 1;
            ShopStruct.shopitem[9, 1].refin = 0;
            ShopStruct.shopitem[9, 1].price = 209;// 225;

            ShopStruct.shopitem[9, 2].type = 2;
            ShopStruct.shopitem[9, 2].num = 3;
            ShopStruct.shopitem[9, 2].value = 1;
            ShopStruct.shopitem[9, 2].refin = 0;
            ShopStruct.shopitem[9, 2].price = 477;// 225;

            ShopStruct.shopitem[9, 3].type = 2;
            ShopStruct.shopitem[9, 3].num = 4;
            ShopStruct.shopitem[9, 3].value = 1;
            ShopStruct.shopitem[9, 3].refin = 0;
            ShopStruct.shopitem[9, 3].price = 990;// 225;

            ShopStruct.shopitem[9, 4].type = 2;
            ShopStruct.shopitem[9, 4].num = 5;
            ShopStruct.shopitem[9, 4].value = 1;
            ShopStruct.shopitem[9, 4].refin = 0;
            ShopStruct.shopitem[9, 4].price = 1650;// 225;

            ShopStruct.shopitem[9, 5].type = 2;
            ShopStruct.shopitem[9, 5].num = 6;
            ShopStruct.shopitem[9, 5].value = 1;
            ShopStruct.shopitem[9, 5].refin = 0;
            ShopStruct.shopitem[9, 5].price = 3350;// 225;

            ShopStruct.shopitem[9, 6].type = 2;
            ShopStruct.shopitem[9, 6].num = 7;
            ShopStruct.shopitem[9, 6].value = 1;
            ShopStruct.shopitem[9, 6].refin = 0;
            ShopStruct.shopitem[9, 6].price = 5450;// 225;

            ShopStruct.shopitem[9, 7].type = 2;
            ShopStruct.shopitem[9, 7].num = 8;
            ShopStruct.shopitem[9, 7].value = 1;
            ShopStruct.shopitem[9, 7].refin = 0;
            ShopStruct.shopitem[9, 7].price = 8220;// 225;

            ShopStruct.shopitem[9, 8].type = 2;
            ShopStruct.shopitem[9, 8].num = 8;
            ShopStruct.shopitem[9, 8].value = 1;
            ShopStruct.shopitem[9, 8].refin = 0;
            ShopStruct.shopitem[9, 8].price = 10130;// 225;

            ShopStruct.shopitem[9, 9].type = 2;
            ShopStruct.shopitem[9, 9].num = 9;
            ShopStruct.shopitem[9, 9].value = 1;
            ShopStruct.shopitem[9, 9].refin = 0;
            ShopStruct.shopitem[9, 9].price = 13630;// 225;

            ShopStruct.shopitem[9, 10].type = 2;
            ShopStruct.shopitem[9, 10].num = 10;
            ShopStruct.shopitem[9, 10].value = 1;
            ShopStruct.shopitem[9, 10].refin = 0;
            ShopStruct.shopitem[9, 10].price = 17780;// 225;

            ShopStruct.shopitem[9, 11].type = 2;
            ShopStruct.shopitem[9, 11].num = 11;
            ShopStruct.shopitem[9, 11].value = 1;
            ShopStruct.shopitem[9, 11].refin = 0;
            ShopStruct.shopitem[9, 11].price = 23180;// 225;


            //Manual, no editor, sorry :/
            ShopStruct.shop[10].map = 168;
            ShopStruct.shop[10].x = 11;
            ShopStruct.shop[10].y = 6;
            ShopStruct.shop[10].item_count = 12;

            ShopStruct.shopitem[10, 1].type = 3;
            ShopStruct.shopitem[10, 1].num = 2;
            ShopStruct.shopitem[10, 1].value = 1;
            ShopStruct.shopitem[10, 1].refin = 0;
            ShopStruct.shopitem[10, 1].price = 189;// 225;

            ShopStruct.shopitem[10, 2].type = 3;
            ShopStruct.shopitem[10, 2].num = 3;
            ShopStruct.shopitem[10, 2].value = 1;
            ShopStruct.shopitem[10, 2].refin = 0;
            ShopStruct.shopitem[10, 2].price = 389;// 225;

            ShopStruct.shopitem[10, 3].type = 3;
            ShopStruct.shopitem[10, 3].num = 4;
            ShopStruct.shopitem[10, 3].value = 1;
            ShopStruct.shopitem[10, 3].refin = 0;
            ShopStruct.shopitem[10, 3].price = 756;// 225;

            ShopStruct.shopitem[10, 4].type = 3;
            ShopStruct.shopitem[10, 4].num = 5;
            ShopStruct.shopitem[10, 4].value = 1;
            ShopStruct.shopitem[10, 4].refin = 0;
            ShopStruct.shopitem[10, 4].price = 1229;// 225;

            ShopStruct.shopitem[10, 5].type = 3;
            ShopStruct.shopitem[10, 5].num = 14;
            ShopStruct.shopitem[10, 5].value = 1;
            ShopStruct.shopitem[10, 5].refin = 0;
            ShopStruct.shopitem[10, 5].price = 4663;// 225;

            ShopStruct.shopitem[10, 6].type = 3;
            ShopStruct.shopitem[10, 6].num = 15;
            ShopStruct.shopitem[10, 6].value = 1;
            ShopStruct.shopitem[10, 6].refin = 0;
            ShopStruct.shopitem[10, 6].price = 7224;// 225;

            ShopStruct.shopitem[10, 7].type = 3;
            ShopStruct.shopitem[10, 7].num = 16;
            ShopStruct.shopitem[10, 7].value = 1;
            ShopStruct.shopitem[10, 7].refin = 0;
            ShopStruct.shopitem[10, 7].price = 10101;// 225;

            ShopStruct.shopitem[10, 8].type = 3;
            ShopStruct.shopitem[10, 8].num = 17;
            ShopStruct.shopitem[10, 8].value = 1;
            ShopStruct.shopitem[10, 8].refin = 0;
            ShopStruct.shopitem[10, 8].price = 14101;// 225;

            ShopStruct.shopitem[10, 9].type = 3;
            ShopStruct.shopitem[10, 9].num = 18;
            ShopStruct.shopitem[10, 9].value = 1;
            ShopStruct.shopitem[10, 9].refin = 0;
            ShopStruct.shopitem[10, 9].price = 19243;// 225;

            ShopStruct.shopitem[10, 10].type = 3;
            ShopStruct.shopitem[10, 10].num = 22;
            ShopStruct.shopitem[10, 10].value = 1;
            ShopStruct.shopitem[10, 10].refin = 0;
            ShopStruct.shopitem[10, 10].price = 22342;// 225;

            ShopStruct.shopitem[10, 11].type = 3;
            ShopStruct.shopitem[10, 11].num = 23;
            ShopStruct.shopitem[10, 11].value = 1;
            ShopStruct.shopitem[10, 11].refin = 0;
            ShopStruct.shopitem[10, 11].price = 26347;// 225;

            ShopStruct.shopitem[10, 12].type = 3;
            ShopStruct.shopitem[10, 12].num = 24;
            ShopStruct.shopitem[10, 12].value = 1;
            ShopStruct.shopitem[10, 12].refin = 0;
            ShopStruct.shopitem[10, 12].price = 32345;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[11].map = 174;
            ShopStruct.shop[11].x = 11;
            ShopStruct.shop[11].y = 6;
            ShopStruct.shop[11].item_count = 6;

            ShopStruct.shopitem[11, 1].type = 2;
            ShopStruct.shopitem[11, 1].num = 34;
            ShopStruct.shopitem[11, 1].value = 1;
            ShopStruct.shopitem[11, 1].refin = 0;
            ShopStruct.shopitem[11, 1].price = 500;// 225;

            ShopStruct.shopitem[11, 2].type = 2;
            ShopStruct.shopitem[11, 2].num = 32;
            ShopStruct.shopitem[11, 2].value = 1;
            ShopStruct.shopitem[11, 2].refin = 0;
            ShopStruct.shopitem[11, 2].price = 1200;// 225;

            ShopStruct.shopitem[11, 3].type = 2;
            ShopStruct.shopitem[11, 3].num = 33;
            ShopStruct.shopitem[11, 3].value = 1;
            ShopStruct.shopitem[11, 3].refin = 0;
            ShopStruct.shopitem[11, 3].price = 2100;// 225;

            ShopStruct.shopitem[11, 4].type = 2;
            ShopStruct.shopitem[11, 4].num = 35;
            ShopStruct.shopitem[11, 4].value = 1;
            ShopStruct.shopitem[11, 4].refin = 0;
            ShopStruct.shopitem[11, 4].price = 5000;// 225;

            ShopStruct.shopitem[11, 5].type = 2;
            ShopStruct.shopitem[11, 5].num = 36;
            ShopStruct.shopitem[11, 5].value = 1;
            ShopStruct.shopitem[11, 5].refin = 0;
            ShopStruct.shopitem[11, 5].price = 8000;// 225;

            ShopStruct.shopitem[11, 6].type = 2;
            ShopStruct.shopitem[11, 6].num = 37;
            ShopStruct.shopitem[11, 6].value = 1;
            ShopStruct.shopitem[11, 6].refin = 0;
            ShopStruct.shopitem[11, 6].price = 14000; //225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[12].map = 175;
            ShopStruct.shop[12].x = 11;
            ShopStruct.shop[12].y = 6;
            ShopStruct.shop[12].item_count = 11;

            ShopStruct.shopitem[12, 1].type = 3;
            ShopStruct.shopitem[12, 1].num = 7;
            ShopStruct.shopitem[12, 1].value = 1;
            ShopStruct.shopitem[12, 1].refin = 0;
            ShopStruct.shopitem[12, 1].price = 300;// 225;

            ShopStruct.shopitem[12, 2].type = 3;
            ShopStruct.shopitem[12, 2].num = 8;
            ShopStruct.shopitem[12, 2].value = 1;
            ShopStruct.shopitem[12, 2].refin = 0;
            ShopStruct.shopitem[12, 2].price = 600;// 225;

            ShopStruct.shopitem[12, 3].type = 3;
            ShopStruct.shopitem[12, 3].num = 9;
            ShopStruct.shopitem[12, 3].value = 1;
            ShopStruct.shopitem[12, 3].refin = 0;
            ShopStruct.shopitem[12, 3].price = 1100;// 225;

            ShopStruct.shopitem[12, 4].type = 3;
            ShopStruct.shopitem[12, 4].num = 10;
            ShopStruct.shopitem[12, 4].value = 1;
            ShopStruct.shopitem[12, 4].refin = 0;
            ShopStruct.shopitem[12, 4].price = 1800;// 225;

            ShopStruct.shopitem[12, 5].type = 3;
            ShopStruct.shopitem[12, 5].num = 11;
            ShopStruct.shopitem[12, 5].value = 1;
            ShopStruct.shopitem[12, 5].refin = 0;
            ShopStruct.shopitem[12, 5].price = 2500;// 225;

            ShopStruct.shopitem[12, 6].type = 3;
            ShopStruct.shopitem[12, 6].num = 12;
            ShopStruct.shopitem[12, 6].value = 1;
            ShopStruct.shopitem[12, 6].refin = 0;
            ShopStruct.shopitem[12, 6].price = 4000;// 225;

            ShopStruct.shopitem[12, 7].type = 3;
            ShopStruct.shopitem[12, 7].num = 13;
            ShopStruct.shopitem[12, 7].value = 1;
            ShopStruct.shopitem[12, 7].refin = 0;
            ShopStruct.shopitem[12, 7].price = 6200;// 225;

            ShopStruct.shopitem[12, 8].type = 3;
            ShopStruct.shopitem[12, 8].num = 25;
            ShopStruct.shopitem[12, 8].value = 1;
            ShopStruct.shopitem[12, 8].refin = 0;
            ShopStruct.shopitem[12, 8].price = 9300;// 225;

            ShopStruct.shopitem[12, 9].type = 3;
            ShopStruct.shopitem[12, 9].num = 26;
            ShopStruct.shopitem[12, 9].value = 1;
            ShopStruct.shopitem[12, 9].refin = 0;
            ShopStruct.shopitem[12, 9].price = 13800;// 225;

            ShopStruct.shopitem[12, 10].type = 3;
            ShopStruct.shopitem[12, 10].num = 27;
            ShopStruct.shopitem[12, 10].value = 1;
            ShopStruct.shopitem[12, 10].refin = 0;
            ShopStruct.shopitem[12, 10].price = 17100;// 225;

            ShopStruct.shopitem[12, 11].type = 3;
            ShopStruct.shopitem[12, 11].num = 30;
            ShopStruct.shopitem[12, 11].value = 1;
            ShopStruct.shopitem[12, 11].refin = 0;
            ShopStruct.shopitem[12, 11].price = 22400;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[13].map = 170;
            ShopStruct.shop[13].x = 11;
            ShopStruct.shop[13].y = 6;
            ShopStruct.shop[13].item_count = 7;

            ShopStruct.shopitem[13, 1].type = 1;
            ShopStruct.shopitem[13, 1].num = 1;
            ShopStruct.shopitem[13, 1].value = 1;
            ShopStruct.shopitem[13, 1].refin = 0;
            ShopStruct.shopitem[13, 1].price = 75;// 225;

            ShopStruct.shopitem[13, 2].type = 1;
            ShopStruct.shopitem[13, 2].num = 2;
            ShopStruct.shopitem[13, 2].value = 1;
            ShopStruct.shopitem[13, 2].refin = 0;
            ShopStruct.shopitem[13, 2].price = 75;// 225;

            ShopStruct.shopitem[13, 3].type = 1;
            ShopStruct.shopitem[13, 3].num = 69;
            ShopStruct.shopitem[13, 3].value = 1;
            ShopStruct.shopitem[13, 3].refin = 0;
            ShopStruct.shopitem[13, 3].price = 500;// 225;

            ShopStruct.shopitem[13, 4].type = 1;
            ShopStruct.shopitem[13, 4].num = 73;
            ShopStruct.shopitem[13, 4].value = 1;
            ShopStruct.shopitem[13, 4].refin = 0;
            ShopStruct.shopitem[13, 4].price = 200;// 225;

            ShopStruct.shopitem[13, 5].type = 1;
            ShopStruct.shopitem[13, 5].num = 72;
            ShopStruct.shopitem[13, 5].value = 1;
            ShopStruct.shopitem[13, 5].refin = 0;
            ShopStruct.shopitem[13, 5].price = 700;// 225;

            ShopStruct.shopitem[13, 6].type = 1;
            ShopStruct.shopitem[13, 6].num = 74;
            ShopStruct.shopitem[13, 6].value = 1;
            ShopStruct.shopitem[13, 6].refin = 0;
            ShopStruct.shopitem[13, 6].price = 500;// 225;

            ShopStruct.shopitem[13, 7].type = 1;
            ShopStruct.shopitem[13, 7].num = 75;
            ShopStruct.shopitem[13, 7].value = 1;
            ShopStruct.shopitem[13, 7].refin = 0;
            ShopStruct.shopitem[13, 7].price = 1000;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[14].map = 169;
            ShopStruct.shop[14].x = 11;
            ShopStruct.shop[14].y = 6;
            ShopStruct.shop[14].item_count = 1;

            ShopStruct.shopitem[14, 1].type = 1;
            ShopStruct.shopitem[14, 1].num = 68;
            ShopStruct.shopitem[14, 1].value = 1;
            ShopStruct.shopitem[14, 1].refin = 0;
            ShopStruct.shopitem[14, 1].price = 100000;// 225;


            //Manual, no editor, sorry :/
            ShopStruct.shop[15].map = 219;
            ShopStruct.shop[15].x = 11;
            ShopStruct.shop[15].y = 6;
            ShopStruct.shop[15].item_count = 11;

            ShopStruct.shopitem[15, 1].type = 3;
            ShopStruct.shopitem[15, 1].num = 7;
            ShopStruct.shopitem[15, 1].value = 1;
            ShopStruct.shopitem[15, 1].refin = 0;
            ShopStruct.shopitem[15, 1].price = 300;// 225;

            ShopStruct.shopitem[15, 2].type = 3;
            ShopStruct.shopitem[15, 2].num = 8;
            ShopStruct.shopitem[15, 2].value = 1;
            ShopStruct.shopitem[15, 2].refin = 0;
            ShopStruct.shopitem[15, 2].price = 600;// 225;

            ShopStruct.shopitem[15, 3].type = 3;
            ShopStruct.shopitem[15, 3].num = 9;
            ShopStruct.shopitem[15, 3].value = 1;
            ShopStruct.shopitem[15, 3].refin = 0;
            ShopStruct.shopitem[15, 3].price = 1100;// 225;

            ShopStruct.shopitem[15, 4].type = 3;
            ShopStruct.shopitem[15, 4].num = 10;
            ShopStruct.shopitem[15, 4].value = 1;
            ShopStruct.shopitem[15, 4].refin = 0;
            ShopStruct.shopitem[15, 4].price = 1800;// 225;

            ShopStruct.shopitem[15, 5].type = 3;
            ShopStruct.shopitem[15, 5].num = 11;
            ShopStruct.shopitem[15, 5].value = 1;
            ShopStruct.shopitem[15, 5].refin = 0;
            ShopStruct.shopitem[15, 5].price = 2500;// 225;

            ShopStruct.shopitem[15, 6].type = 3;
            ShopStruct.shopitem[15, 6].num = 12;
            ShopStruct.shopitem[15, 6].value = 1;
            ShopStruct.shopitem[15, 6].refin = 0;
            ShopStruct.shopitem[15, 6].price = 4000;// 225;

            ShopStruct.shopitem[15, 7].type = 3;
            ShopStruct.shopitem[15, 7].num = 13;
            ShopStruct.shopitem[15, 7].value = 1;
            ShopStruct.shopitem[15, 7].refin = 0;
            ShopStruct.shopitem[15, 7].price = 6200;// 225;

            ShopStruct.shopitem[15, 8].type = 3;
            ShopStruct.shopitem[15, 8].num = 25;
            ShopStruct.shopitem[15, 8].value = 1;
            ShopStruct.shopitem[15, 8].refin = 0;
            ShopStruct.shopitem[15, 8].price = 9300;// 225;

            ShopStruct.shopitem[15, 9].type = 3;
            ShopStruct.shopitem[15, 9].num = 26;
            ShopStruct.shopitem[15, 9].value = 1;
            ShopStruct.shopitem[15, 9].refin = 0;
            ShopStruct.shopitem[15, 9].price = 13800;// 225;

            ShopStruct.shopitem[15, 10].type = 3;
            ShopStruct.shopitem[15, 10].num = 27;
            ShopStruct.shopitem[15, 10].value = 1;
            ShopStruct.shopitem[15, 10].refin = 0;
            ShopStruct.shopitem[15, 10].price = 17100;// 225;

            ShopStruct.shopitem[15, 11].type = 3;
            ShopStruct.shopitem[15, 11].num = 30;
            ShopStruct.shopitem[15, 11].value = 1;
            ShopStruct.shopitem[15, 11].refin = 0;
            ShopStruct.shopitem[15, 11].price = 22400;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[16].map = 220;
            ShopStruct.shop[16].x = 11;
            ShopStruct.shop[16].y = 6;
            ShopStruct.shop[16].item_count = 7;

            ShopStruct.shopitem[16, 1].type = 1;
            ShopStruct.shopitem[16, 1].num = 1;
            ShopStruct.shopitem[16, 1].value = 1;
            ShopStruct.shopitem[16, 1].refin = 0;
            ShopStruct.shopitem[16, 1].price = 75;// 225;

            ShopStruct.shopitem[16, 2].type = 1;
            ShopStruct.shopitem[16, 2].num = 2;
            ShopStruct.shopitem[16, 2].value = 1;
            ShopStruct.shopitem[16, 2].refin = 0;
            ShopStruct.shopitem[16, 2].price = 75;// 225;

            ShopStruct.shopitem[16, 3].type = 1;
            ShopStruct.shopitem[16, 3].num = 69;
            ShopStruct.shopitem[16, 3].value = 1;
            ShopStruct.shopitem[16, 3].refin = 0;
            ShopStruct.shopitem[16, 3].price = 500;// 225;

            ShopStruct.shopitem[16, 4].type = 1;
            ShopStruct.shopitem[16, 4].num = 73;
            ShopStruct.shopitem[16, 4].value = 1;
            ShopStruct.shopitem[16, 4].refin = 0;
            ShopStruct.shopitem[16, 4].price = 200;// 225;

            ShopStruct.shopitem[16, 5].type = 1;
            ShopStruct.shopitem[16, 5].num = 72;
            ShopStruct.shopitem[16, 5].value = 1;
            ShopStruct.shopitem[16, 5].refin = 0;
            ShopStruct.shopitem[16, 5].price = 700;// 225;

            ShopStruct.shopitem[16, 6].type = 1;
            ShopStruct.shopitem[16, 6].num = 74;
            ShopStruct.shopitem[16, 6].value = 1;
            ShopStruct.shopitem[16, 6].refin = 0;
            ShopStruct.shopitem[16, 6].price = 500;// 225;

            ShopStruct.shopitem[16, 7].type = 1;
            ShopStruct.shopitem[16, 7].num = 75;
            ShopStruct.shopitem[16, 7].value = 1;
            ShopStruct.shopitem[16, 7].refin = 0;
            ShopStruct.shopitem[16, 7].price = 1000;// 225;
        }
        //*********************************************************************************************
        // LoadRecipes / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void LoadRecipes()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Manual, no editor, sorry :/
            
            //Espada pequena
            MStruct.craftrecipe[2, 1, 1].type = 1;
            MStruct.craftrecipe[2, 1, 1].num = 46;
            MStruct.craftrecipe[2, 1, 1].value = 2;
            MStruct.craftrecipe[2, 1, 1].refin = 0;

            MStruct.craftrecipe[2, 1, 2].type = 1;
            MStruct.craftrecipe[2, 1, 2].num = 47;
            MStruct.craftrecipe[2, 1, 2].value = 1;
            MStruct.craftrecipe[2, 1, 2].refin = 0;

            //Espada Comum
            MStruct.craftrecipe[2, 2, 1].type = 1;
            MStruct.craftrecipe[2, 2, 1].num = 46;
            MStruct.craftrecipe[2, 2, 1].value = 5;
            MStruct.craftrecipe[2, 2, 1].refin = 0;

            MStruct.craftrecipe[2, 2, 2].type = 1;
            MStruct.craftrecipe[2, 2, 2].num = 47;
            MStruct.craftrecipe[2, 2, 2].value = 3;
            MStruct.craftrecipe[2, 2, 2].refin = 0;

            //Espada de Invartam
            MStruct.craftrecipe[2, 3, 1].type = 1;
            MStruct.craftrecipe[2, 3, 1].num = 46;
            MStruct.craftrecipe[2, 3, 1].value = 6;
            MStruct.craftrecipe[2, 3, 1].refin = 0;

            MStruct.craftrecipe[2, 3, 2].type = 1;
            MStruct.craftrecipe[2, 3, 2].num = 47;
            MStruct.craftrecipe[2, 3, 2].value = 4;
            MStruct.craftrecipe[2, 3, 2].refin = 0;

            MStruct.craftrecipe[2, 3, 3].type = 1;
            MStruct.craftrecipe[2, 3, 3].num = 12;
            MStruct.craftrecipe[2, 3, 3].value = 2;
            MStruct.craftrecipe[2, 3, 3].refin = 0;

            //Espada do Velho Espadachim
            MStruct.craftrecipe[2, 4, 1].type = 1;
            MStruct.craftrecipe[2, 4, 1].num = 46;
            MStruct.craftrecipe[2, 4, 1].value = 10;
            MStruct.craftrecipe[2, 4, 1].refin = 0;

            MStruct.craftrecipe[2, 4, 2].type = 1;
            MStruct.craftrecipe[2, 4, 2].num = 47;
            MStruct.craftrecipe[2, 4, 2].value = 8;
            MStruct.craftrecipe[2, 4, 2].refin = 0;

            MStruct.craftrecipe[2, 4, 3].type = 1;
            MStruct.craftrecipe[2, 4, 3].num = 12;
            MStruct.craftrecipe[2, 4, 3].value = 6;
            MStruct.craftrecipe[2, 4, 3].refin = 0;

            //Florete
            MStruct.craftrecipe[2, 5, 1].type = 1;
            MStruct.craftrecipe[2, 5, 1].num = 46;
            MStruct.craftrecipe[2, 5, 1].value = 15;
            MStruct.craftrecipe[2, 5, 1].refin = 0;

            MStruct.craftrecipe[2, 5, 2].type = 1;
            MStruct.craftrecipe[2, 5, 2].num = 47;
            MStruct.craftrecipe[2, 5, 2].value = 9;
            MStruct.craftrecipe[2, 5, 2].refin = 0;

            MStruct.craftrecipe[2, 5, 3].type = 1;
            MStruct.craftrecipe[2, 5, 3].num = 14;
            MStruct.craftrecipe[2, 5, 3].value = 9;
            MStruct.craftrecipe[2, 5, 3].refin = 0;

            //Florete do Capitão
            MStruct.craftrecipe[2, 6, 1].type = 1;
            MStruct.craftrecipe[2, 6, 1].num = 46;
            MStruct.craftrecipe[2, 6, 1].value = 25;
            MStruct.craftrecipe[2, 6, 1].refin = 0;

            MStruct.craftrecipe[2, 6, 2].type = 1;
            MStruct.craftrecipe[2, 6, 2].num = 47;
            MStruct.craftrecipe[2, 6, 2].value = 9;
            MStruct.craftrecipe[2, 6, 2].refin = 0;

            MStruct.craftrecipe[2, 6, 3].type = 1;
            MStruct.craftrecipe[2, 6, 3].num = 14;
            MStruct.craftrecipe[2, 6, 3].value = 9;
            MStruct.craftrecipe[2, 6, 3].refin = 0;

            MStruct.craftrecipe[2, 6, 4].type = 1;
            MStruct.craftrecipe[2, 6, 4].num = 9;
            MStruct.craftrecipe[2, 6, 4].value = 1;
            MStruct.craftrecipe[2, 6, 4].refin = 0;

            //Espada Longa
            MStruct.craftrecipe[2, 7, 1].type = 1;
            MStruct.craftrecipe[2, 7, 1].num = 46;
            MStruct.craftrecipe[2, 7, 1].value = 30;
            MStruct.craftrecipe[2, 7, 1].refin = 0;

            MStruct.craftrecipe[2, 7, 2].type = 1;
            MStruct.craftrecipe[2, 7, 2].num = 47;
            MStruct.craftrecipe[2, 7, 2].value = 15;
            MStruct.craftrecipe[2, 7, 2].refin = 0;

            MStruct.craftrecipe[2, 7, 3].type = 1;
            MStruct.craftrecipe[2, 7, 3].num = 10;
            MStruct.craftrecipe[2, 7, 3].value = 12;
            MStruct.craftrecipe[2, 7, 3].refin = 0;

            //Espada Longa do Andarilho
            MStruct.craftrecipe[2, 8, 1].type = 1;
            MStruct.craftrecipe[2, 8, 1].num = 46;
            MStruct.craftrecipe[2, 8, 1].value = 35;
            MStruct.craftrecipe[2, 8, 1].refin = 0;

            MStruct.craftrecipe[2, 8, 2].type = 1;
            MStruct.craftrecipe[2, 8, 2].num = 47;
            MStruct.craftrecipe[2, 8, 2].value = 20;
            MStruct.craftrecipe[2, 8, 2].refin = 0;

            MStruct.craftrecipe[2, 8, 3].type = 1;
            MStruct.craftrecipe[2, 8, 3].num = 10;
            MStruct.craftrecipe[2, 8, 3].value = 12;
            MStruct.craftrecipe[2, 8, 3].refin = 0;

            MStruct.craftrecipe[2, 8, 4].type = 1;
            MStruct.craftrecipe[2, 8, 4].num = 8;
            MStruct.craftrecipe[2, 8, 4].value = 10;
            MStruct.craftrecipe[2, 8, 4].refin = 0;

            //Espada Pesada
            MStruct.craftrecipe[2, 9, 1].type = 1;
            MStruct.craftrecipe[2, 9, 1].num = 46;
            MStruct.craftrecipe[2, 9, 1].value = 45;
            MStruct.craftrecipe[2, 9, 1].refin = 0;

            MStruct.craftrecipe[2, 9, 2].type = 1;
            MStruct.craftrecipe[2, 9, 2].num = 47;
            MStruct.craftrecipe[2, 9, 2].value = 40;
            MStruct.craftrecipe[2, 9, 2].refin = 0;

            MStruct.craftrecipe[2, 9, 3].type = 1;
            MStruct.craftrecipe[2, 9, 3].num = 18;
            MStruct.craftrecipe[2, 9, 3].value = 5;
            MStruct.craftrecipe[2, 9, 3].refin = 0;

            //Espada Pesada do Guardião
            MStruct.craftrecipe[2, 10, 1].type = 1;
            MStruct.craftrecipe[2, 10, 1].num = 46;
            MStruct.craftrecipe[2, 10, 1].value = 55;
            MStruct.craftrecipe[2, 10, 1].refin = 0;

            MStruct.craftrecipe[2, 10, 2].type = 1;
            MStruct.craftrecipe[2, 10, 2].num = 47;
            MStruct.craftrecipe[2, 10, 2].value = 50;
            MStruct.craftrecipe[2, 10, 2].refin = 0;

            MStruct.craftrecipe[2, 10, 3].type = 1;
            MStruct.craftrecipe[2, 10, 3].num = 18;
            MStruct.craftrecipe[2, 10, 3].value = 5;
            MStruct.craftrecipe[2, 10, 3].refin = 0;

            MStruct.craftrecipe[2, 10, 4].type = 1;
            MStruct.craftrecipe[2, 10, 4].num = 19;
            MStruct.craftrecipe[2, 10, 4].value = 5;
            MStruct.craftrecipe[2, 10, 4].refin = 0;

            //Espada do Antigo Rei
            MStruct.craftrecipe[2, 11, 1].type = 1;
            MStruct.craftrecipe[2, 11, 1].num = 46;
            MStruct.craftrecipe[2, 11, 1].value = 55;
            MStruct.craftrecipe[2, 11, 1].refin = 0;

            MStruct.craftrecipe[2, 11, 2].type = 1;
            MStruct.craftrecipe[2, 11, 2].num = 47;
            MStruct.craftrecipe[2, 11, 2].value = 50;
            MStruct.craftrecipe[2, 11, 2].refin = 0;

            MStruct.craftrecipe[2, 11, 3].type = 1;
            MStruct.craftrecipe[2, 11, 3].num = 18;
            MStruct.craftrecipe[2, 11, 3].value = 5;
            MStruct.craftrecipe[2, 11, 3].refin = 0;

            MStruct.craftrecipe[2, 11, 4].type = 1;
            MStruct.craftrecipe[2, 11, 4].num = 19;
            MStruct.craftrecipe[2, 11, 4].value = 5;
            MStruct.craftrecipe[2, 11, 4].refin = 0;
          
            MStruct.craftrecipe[2, 11, 5].type = 1;
            MStruct.craftrecipe[2, 11, 5].num = 34;
            MStruct.craftrecipe[2, 11, 5].value = 50;
            MStruct.craftrecipe[2, 11, 5].refin = 0;

            //Aiguillon
            MStruct.craftrecipe[2, 12, 1].type = 1;
            MStruct.craftrecipe[2, 12, 1].num = 46;
            MStruct.craftrecipe[2, 12, 1].value = 100;
            MStruct.craftrecipe[2, 12, 1].refin = 0;

            MStruct.craftrecipe[2, 12, 2].type = 1;
            MStruct.craftrecipe[2, 12, 2].num = 47;
            MStruct.craftrecipe[2, 12, 2].value = 75;
            MStruct.craftrecipe[2, 12, 2].refin = 0;

            MStruct.craftrecipe[2, 12, 3].type = 1;
            MStruct.craftrecipe[2, 12, 3].num = 38;
            MStruct.craftrecipe[2, 12, 3].value = 100;
            MStruct.craftrecipe[2, 12, 3].refin = 0;

            MStruct.craftrecipe[2, 12, 4].type = 1;
            MStruct.craftrecipe[2, 12, 4].num = 39;
            MStruct.craftrecipe[2, 12, 4].value = 5;
            MStruct.craftrecipe[2, 12, 4].refin = 0;

            MStruct.craftrecipe[2, 12, 5].type = 1;
            MStruct.craftrecipe[2, 12, 5].num = 40;
            MStruct.craftrecipe[2, 12, 5].value = 1;
            MStruct.craftrecipe[2, 12, 5].refin = 0;

            //Aiguille
            MStruct.craftrecipe[2, 13, 1].type = 1;
            MStruct.craftrecipe[2, 13, 1].num = 46;
            MStruct.craftrecipe[2, 13, 1].value = 100;
            MStruct.craftrecipe[2, 13, 1].refin = 0;

            MStruct.craftrecipe[2, 13, 2].type = 1;
            MStruct.craftrecipe[2, 13, 2].num = 47;
            MStruct.craftrecipe[2, 13, 2].value = 86;
            MStruct.craftrecipe[2, 13, 2].refin = 0;

            MStruct.craftrecipe[2, 13, 3].type = 1;
            MStruct.craftrecipe[2, 13, 3].num = 41;
            MStruct.craftrecipe[2, 13, 3].value = 100;
            MStruct.craftrecipe[2, 13, 3].refin = 0;

            MStruct.craftrecipe[2, 13, 4].type = 1;
            MStruct.craftrecipe[2, 13, 4].num = 39;
            MStruct.craftrecipe[2, 13, 4].value = 5;
            MStruct.craftrecipe[2, 13, 4].refin = 0;

            MStruct.craftrecipe[2, 13, 5].type = 1;
            MStruct.craftrecipe[2, 13, 5].num = 40;
            MStruct.craftrecipe[2, 13, 5].value = 1;
            MStruct.craftrecipe[2, 13, 5].refin = 0;

            //Radiata
            MStruct.craftrecipe[2, 14, 1].type = 1;
            MStruct.craftrecipe[2, 14, 1].num = 46;
            MStruct.craftrecipe[2, 14, 1].value = 100;
            MStruct.craftrecipe[2, 14, 1].refin = 0;

            MStruct.craftrecipe[2, 14, 2].type = 1;
            MStruct.craftrecipe[2, 14, 2].num = 47;
            MStruct.craftrecipe[2, 14, 2].value = 86;
            MStruct.craftrecipe[2, 14, 2].refin = 0;

            MStruct.craftrecipe[2, 14, 3].type = 1;
            MStruct.craftrecipe[2, 14, 3].num = 43;
            MStruct.craftrecipe[2, 14, 3].value = 100;
            MStruct.craftrecipe[2, 14, 3].refin = 0;

            MStruct.craftrecipe[2, 14, 4].type = 1;
            MStruct.craftrecipe[2, 14, 4].num = 37;
            MStruct.craftrecipe[2, 14, 4].value = 5;
            MStruct.craftrecipe[2, 14, 4].refin = 0;

            MStruct.craftrecipe[2, 14, 5].type = 1;
            MStruct.craftrecipe[2, 14, 5].num = 42;
            MStruct.craftrecipe[2, 14, 5].value = 1;
            MStruct.craftrecipe[2, 14, 5].refin = 0;

            //Fantome
            MStruct.craftrecipe[2, 15, 1].type = 1;
            MStruct.craftrecipe[2, 15, 1].num = 46;
            MStruct.craftrecipe[2, 15, 1].value = 100;
            MStruct.craftrecipe[2, 15, 1].refin = 0;

            MStruct.craftrecipe[2, 15, 2].type = 1;
            MStruct.craftrecipe[2, 15, 2].num = 47;
            MStruct.craftrecipe[2, 15, 2].value = 86;
            MStruct.craftrecipe[2, 15, 2].refin = 0;

            MStruct.craftrecipe[2, 15, 3].type = 1;
            MStruct.craftrecipe[2, 15, 3].num = 36;
            MStruct.craftrecipe[2, 15, 3].value = 100;
            MStruct.craftrecipe[2, 15, 3].refin = 0;

            MStruct.craftrecipe[2, 15, 4].type = 1;
            MStruct.craftrecipe[2, 15, 4].num = 40;
            MStruct.craftrecipe[2, 15, 4].value = 10;
            MStruct.craftrecipe[2, 15, 4].refin = 0;

            MStruct.craftrecipe[2, 15, 5].type = 1;
            MStruct.craftrecipe[2, 15, 5].num = 35;
            MStruct.craftrecipe[2, 15, 5].value = 10;
            MStruct.craftrecipe[2, 15, 5].refin = 0;

            //Volonte
            MStruct.craftrecipe[2, 16, 1].type = 1;
            MStruct.craftrecipe[2, 16, 1].num = 46;
            MStruct.craftrecipe[2, 16, 1].value = 100;
            MStruct.craftrecipe[2, 16, 1].refin = 0;

            MStruct.craftrecipe[2, 16, 2].type = 1;
            MStruct.craftrecipe[2, 16, 2].num = 47;
            MStruct.craftrecipe[2, 16, 2].value = 86;
            MStruct.craftrecipe[2, 16, 2].refin = 0;

            MStruct.craftrecipe[2, 16, 3].type = 1;
            MStruct.craftrecipe[2, 16, 3].num = 26;
            MStruct.craftrecipe[2, 16, 3].value = 100;
            MStruct.craftrecipe[2, 16, 3].refin = 0;

            MStruct.craftrecipe[2, 16, 4].type = 1;
            MStruct.craftrecipe[2, 16, 4].num = 39;
            MStruct.craftrecipe[2, 16, 4].value = 1;
            MStruct.craftrecipe[2, 16, 4].refin = 0;

            MStruct.craftrecipe[2, 16, 5].type = 1;
            MStruct.craftrecipe[2, 16, 5].num = 40;
            MStruct.craftrecipe[2, 16, 5].value = 10;
            MStruct.craftrecipe[2, 16, 5].refin = 0;

            //Rocher
            MStruct.craftrecipe[2, 17, 1].type = 1;
            MStruct.craftrecipe[2, 17, 1].num = 46;
            MStruct.craftrecipe[2, 17, 1].value = 100;
            MStruct.craftrecipe[2, 17, 1].refin = 0;

            MStruct.craftrecipe[2, 17, 2].type = 1;
            MStruct.craftrecipe[2, 17, 2].num = 47;
            MStruct.craftrecipe[2, 17, 2].value = 90;
            MStruct.craftrecipe[2, 17, 2].refin = 0;

            MStruct.craftrecipe[2, 17, 3].type = 1;
            MStruct.craftrecipe[2, 17, 3].num = 32;
            MStruct.craftrecipe[2, 17, 3].value = 100;
            MStruct.craftrecipe[2, 17, 3].refin = 0;

            MStruct.craftrecipe[2, 17, 4].type = 1;
            MStruct.craftrecipe[2, 17, 4].num = 22;
            MStruct.craftrecipe[2, 17, 4].value = 1;
            MStruct.craftrecipe[2, 17, 4].refin = 0;

            MStruct.craftrecipe[2, 17, 5].type = 1;
            MStruct.craftrecipe[2, 17, 5].num = 40;
            MStruct.craftrecipe[2, 17, 5].value = 10;
            MStruct.craftrecipe[2, 17, 5].refin = 0;

            //Brule
            MStruct.craftrecipe[2, 18, 1].type = 1;
            MStruct.craftrecipe[2, 18, 1].num = 46;
            MStruct.craftrecipe[2, 18, 1].value = 100;
            MStruct.craftrecipe[2, 18, 1].refin = 0;

            MStruct.craftrecipe[2, 18, 2].type = 1;
            MStruct.craftrecipe[2, 18, 2].num = 47;
            MStruct.craftrecipe[2, 18, 2].value = 90;
            MStruct.craftrecipe[2, 18, 2].refin = 0;

            MStruct.craftrecipe[2, 18, 3].type = 1;
            MStruct.craftrecipe[2, 18, 3].num = 42;
            MStruct.craftrecipe[2, 18, 3].value = 56;
            MStruct.craftrecipe[2, 18, 3].refin = 0;

            MStruct.craftrecipe[2, 18, 4].type = 1;
            MStruct.craftrecipe[2, 18, 4].num = 17;
            MStruct.craftrecipe[2, 18, 4].value = 100;
            MStruct.craftrecipe[2, 18, 4].refin = 0;

            MStruct.craftrecipe[2, 18, 5].type = 1;
            MStruct.craftrecipe[2, 18, 5].num = 40;
            MStruct.craftrecipe[2, 18, 5].value = 10;
            MStruct.craftrecipe[2, 18, 5].refin = 0;

            //Grand Cri
            MStruct.craftrecipe[2, 19, 1].type = 1;
            MStruct.craftrecipe[2, 19, 1].num = 46;
            MStruct.craftrecipe[2, 19, 1].value = 100;
            MStruct.craftrecipe[2, 19, 1].refin = 0;

            MStruct.craftrecipe[2, 19, 2].type = 1;
            MStruct.craftrecipe[2, 19, 2].num = 47;
            MStruct.craftrecipe[2, 19, 2].value = 100;
            MStruct.craftrecipe[2, 19, 2].refin = 0;

            MStruct.craftrecipe[2, 19, 3].type = 1;
            MStruct.craftrecipe[2, 19, 3].num = 22;
            MStruct.craftrecipe[2, 19, 3].value = 2;
            MStruct.craftrecipe[2, 19, 3].refin = 0;

            MStruct.craftrecipe[2, 19, 4].type = 1;
            MStruct.craftrecipe[2, 19, 4].num = 45;
            MStruct.craftrecipe[2, 19, 4].value = 2;
            MStruct.craftrecipe[2, 19, 4].refin = 0;

            MStruct.craftrecipe[2, 19, 5].type = 1;
            MStruct.craftrecipe[2, 19, 5].num = 40;
            MStruct.craftrecipe[2, 19, 5].value = 10;
            MStruct.craftrecipe[2, 19, 5].refin = 0;

            //Tapegeur
            MStruct.craftrecipe[2, 20, 1].type = 1;
            MStruct.craftrecipe[2, 20, 1].num = 46;
            MStruct.craftrecipe[2, 20, 1].value = 100;
            MStruct.craftrecipe[2, 20, 1].refin = 0;

            MStruct.craftrecipe[2, 20, 2].type = 1;
            MStruct.craftrecipe[2, 20, 2].num = 47;
            MStruct.craftrecipe[2, 20, 2].value = 100;
            MStruct.craftrecipe[2, 20, 2].refin = 0;

            MStruct.craftrecipe[2, 20, 3].type = 1;
            MStruct.craftrecipe[2, 20, 3].num = 22;
            MStruct.craftrecipe[2, 20, 3].value = 2;
            MStruct.craftrecipe[2, 20, 3].refin = 0;
        
            MStruct.craftrecipe[2, 20, 4].type = 1;
            MStruct.craftrecipe[2, 20, 4].num = 28;
            MStruct.craftrecipe[2, 20, 4].value = 1;
            MStruct.craftrecipe[2, 20, 4].refin = 0;

            MStruct.craftrecipe[2, 20, 5].type = 1;
            MStruct.craftrecipe[2, 20, 5].num = 40;
            MStruct.craftrecipe[2, 20, 5].value = 10;
            MStruct.craftrecipe[2, 20, 5].refin = 0;

            //Muramasa
            MStruct.craftrecipe[2, 21, 1].type = 1;
            MStruct.craftrecipe[2, 21, 1].num = 46;
            MStruct.craftrecipe[2, 21, 1].value = 100;
            MStruct.craftrecipe[2, 21, 1].refin = 0;

            MStruct.craftrecipe[2, 21, 2].type = 1;
            MStruct.craftrecipe[2, 21, 2].num = 47;
            MStruct.craftrecipe[2, 21, 2].value = 100;
            MStruct.craftrecipe[2, 21, 2].refin = 0;
        
            MStruct.craftrecipe[2, 21, 3].type = 1;
            MStruct.craftrecipe[2, 21, 3].num = 28;
            MStruct.craftrecipe[2, 21, 3].value = 1;
            MStruct.craftrecipe[2, 21, 3].refin = 0;

            MStruct.craftrecipe[2, 21, 4].type = 1;
            MStruct.craftrecipe[2, 21, 4].num = 48;
            MStruct.craftrecipe[2, 21, 4].value = 1;
            MStruct.craftrecipe[2, 21, 4].refin = 0;

            MStruct.craftrecipe[2, 21, 5].type = 1;
            MStruct.craftrecipe[2, 21, 5].num = 40;
            MStruct.craftrecipe[2, 21, 5].value = 10;
            MStruct.craftrecipe[2, 21, 5].refin = 0;

            //Espada Secto
            MStruct.craftrecipe[2, 22, 1].type = 1;
            MStruct.craftrecipe[2, 22, 1].num = 46;
            MStruct.craftrecipe[2, 22, 1].value = 50;
            MStruct.craftrecipe[2, 22, 1].refin = 0;

            MStruct.craftrecipe[2, 22, 2].type = 1;
            MStruct.craftrecipe[2, 22, 2].num = 47;
            MStruct.craftrecipe[2, 22, 2].value = 50;
            MStruct.craftrecipe[2, 22, 2].refin = 0;

            MStruct.craftrecipe[2, 22, 3].type = 1;
            MStruct.craftrecipe[2, 22, 3].num = 26;
            MStruct.craftrecipe[2, 22, 3].value = 100;
            MStruct.craftrecipe[2, 22, 3].refin = 0;

            //Voospada
            MStruct.craftrecipe[2, 23, 1].type = 1;
            MStruct.craftrecipe[2, 23, 1].num = 46;
            MStruct.craftrecipe[2, 23, 1].value = 100;
            MStruct.craftrecipe[2, 23, 1].refin = 0;

            MStruct.craftrecipe[2, 23, 2].type = 1;
            MStruct.craftrecipe[2, 23, 2].num = 47;
            MStruct.craftrecipe[2, 23, 2].value = 100;
            MStruct.craftrecipe[2, 23, 2].refin = 0;

            MStruct.craftrecipe[2, 23, 3].type = 1;
            MStruct.craftrecipe[2, 23, 3].num = 23;
            MStruct.craftrecipe[2, 23, 3].value = 100;
            MStruct.craftrecipe[2, 23, 3].refin = 0;

            MStruct.craftrecipe[2, 23, 4].type = 1;
            MStruct.craftrecipe[2, 23, 4].num = 28;
            MStruct.craftrecipe[2, 23, 4].value = 1;
            MStruct.craftrecipe[2, 23, 4].refin = 0;

            MStruct.craftrecipe[2, 23, 5].type = 1;
            MStruct.craftrecipe[2, 23, 5].num = 25;
            MStruct.craftrecipe[2, 23, 5].value = 40;
            MStruct.craftrecipe[2, 23, 5].refin = 0;

            //Lança Feitiços
            MStruct.craftrecipe[2, 24, 1].type = 1;
            MStruct.craftrecipe[2, 24, 1].num = 47;
            MStruct.craftrecipe[2, 24, 1].value = 100;
            MStruct.craftrecipe[2, 24, 1].refin = 0;

            MStruct.craftrecipe[2, 24, 2].type = 1;
            MStruct.craftrecipe[2, 24, 2].num = 38;
            MStruct.craftrecipe[2, 24, 2].value = 100;
            MStruct.craftrecipe[2, 24, 2].refin = 0;

            MStruct.craftrecipe[2, 24, 3].type = 1;
            MStruct.craftrecipe[2, 24, 3].num = 34;
            MStruct.craftrecipe[2, 24, 3].value = 30;
            MStruct.craftrecipe[2, 24, 3].refin = 0;

            MStruct.craftrecipe[2, 24, 4].type = 1;
            MStruct.craftrecipe[2, 24, 4].num = 40;
            MStruct.craftrecipe[2, 24, 4].value = 1;
            MStruct.craftrecipe[2, 24, 4].refin = 0;

            MStruct.craftrecipe[2, 24, 5].type = 1;
            MStruct.craftrecipe[2, 24, 5].num = 41;
            MStruct.craftrecipe[2, 24, 5].value = 2;
            MStruct.craftrecipe[2, 24, 5].refin = 0;

            //Cajado do Antigo Carvalho
            MStruct.craftrecipe[2, 25, 1].type = 1;
            MStruct.craftrecipe[2, 25, 1].num = 47;
            MStruct.craftrecipe[2, 25, 1].value = 100;
            MStruct.craftrecipe[2, 25, 1].refin = 0;

            MStruct.craftrecipe[2, 25, 2].type = 1;
            MStruct.craftrecipe[2, 25, 2].num = 43;
            MStruct.craftrecipe[2, 25, 2].value = 200;
            MStruct.craftrecipe[2, 25, 2].refin = 0;

            MStruct.craftrecipe[2, 25, 3].type = 1;
            MStruct.craftrecipe[2, 25, 3].num = 40;
            MStruct.craftrecipe[2, 25, 3].value = 1;
            MStruct.craftrecipe[2, 25, 3].refin = 0;

            //Fim Mif
            MStruct.craftrecipe[2, 26, 1].type = 1;
            MStruct.craftrecipe[2, 26, 1].num = 46;
            MStruct.craftrecipe[2, 26, 1].value = 100;
            MStruct.craftrecipe[2, 26, 1].refin = 0;

            MStruct.craftrecipe[2, 26, 2].type = 1;
            MStruct.craftrecipe[2, 26, 2].num = 47;
            MStruct.craftrecipe[2, 26, 2].value = 100;
            MStruct.craftrecipe[2, 26, 2].refin = 0;

            MStruct.craftrecipe[2, 26, 3].type = 1;
            MStruct.craftrecipe[2, 26, 3].num = 37;
            MStruct.craftrecipe[2, 26, 3].value = 200;
            MStruct.craftrecipe[2, 26, 3].refin = 0;

            //Oacidlam
            MStruct.craftrecipe[2, 27, 1].type = 1;
            MStruct.craftrecipe[2, 27, 1].num = 46;
            MStruct.craftrecipe[2, 27, 1].value = 100;
            MStruct.craftrecipe[2, 27, 1].refin = 0;

            MStruct.craftrecipe[2, 27, 2].type = 1;
            MStruct.craftrecipe[2, 27, 2].num = 47;
            MStruct.craftrecipe[2, 27, 2].value = 100;
            MStruct.craftrecipe[2, 27, 2].refin = 0;

            MStruct.craftrecipe[2, 27, 3].type = 1;
            MStruct.craftrecipe[2, 27, 3].num = 34;
            MStruct.craftrecipe[2, 27, 3].value = 100;
            MStruct.craftrecipe[2, 27, 3].refin = 0;

            MStruct.craftrecipe[2, 27, 4].type = 1;
            MStruct.craftrecipe[2, 27, 4].num = 28;
            MStruct.craftrecipe[2, 27, 4].value = 1;
            MStruct.craftrecipe[2, 27, 4].refin = 0;

            MStruct.craftrecipe[2, 27, 5].type = 1;
            MStruct.craftrecipe[2, 27, 5].num = 40;
            MStruct.craftrecipe[2, 27, 5].value = 130;
            MStruct.craftrecipe[2, 27, 5].refin = 0;

            //Picareta
            MStruct.craftrecipe[2, 28, 1].type = 1;
            MStruct.craftrecipe[2, 28, 1].num = 46;
            MStruct.craftrecipe[2, 28, 1].value = 2;
            MStruct.craftrecipe[2, 28, 1].refin = 0;

            MStruct.craftrecipe[2, 28, 2].type = 1;
            MStruct.craftrecipe[2, 28, 2].num = 47;
            MStruct.craftrecipe[2, 28, 2].value = 1;
            MStruct.craftrecipe[2, 28, 2].refin = 0;

            //Clava Kiorkle
            MStruct.craftrecipe[2, 29, 1].type = 1;
            MStruct.craftrecipe[2, 29, 1].num = 46;
            MStruct.craftrecipe[2, 29, 1].value = 50;
            MStruct.craftrecipe[2, 29, 1].refin = 0;

            MStruct.craftrecipe[2, 29, 2].type = 1;
            MStruct.craftrecipe[2, 29, 2].num = 47;
            MStruct.craftrecipe[2, 29, 2].value = 50;
            MStruct.craftrecipe[2, 29, 2].refin = 0;

            //Machado do Golem
            MStruct.craftrecipe[2, 30, 1].type = 1;
            MStruct.craftrecipe[2, 30, 1].num = 46;
            MStruct.craftrecipe[2, 30, 1].value = 1000;
            MStruct.craftrecipe[2, 30, 1].refin = 0;

            MStruct.craftrecipe[2, 30, 2].type = 1;
            MStruct.craftrecipe[2, 30, 2].num = 47;
            MStruct.craftrecipe[2, 30, 2].value = 100;
            MStruct.craftrecipe[2, 30, 2].refin = 0;

            //Martelo
            MStruct.craftrecipe[2, 31, 1].type = 1;
            MStruct.craftrecipe[2, 31, 1].num = 46;
            MStruct.craftrecipe[2, 31, 1].value = 2;
            MStruct.craftrecipe[2, 31, 1].refin = 0;

            MStruct.craftrecipe[2, 31, 2].type = 1;
            MStruct.craftrecipe[2, 31, 2].num = 47;
            MStruct.craftrecipe[2, 31, 2].value = 1;
            MStruct.craftrecipe[2, 31, 2].refin = 0;
        }

        #region Guild

        public static bool LoadGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Guilds/" + guildnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Guilds/" + guildnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intguildnum = Convert.ToInt32(guildnum);

                //Lê-mos os dados básicos do item
                GStruct.guild[intguildnum].name = br.ReadString();
                GStruct.guild[intguildnum].level = br.ReadInt32();
                GStruct.guild[intguildnum].exp = br.ReadInt32();
                GStruct.guild[intguildnum].shield = br.ReadInt32();
                GStruct.guild[intguildnum].hue = br.ReadInt32();
                GStruct.guild[intguildnum].leader = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 1; i < Globals.Max_Guild_Members; i++)
                {
                    GStruct.guild[intguildnum].memberlist[i] = br.ReadString();
                    GStruct.guild[intguildnum].membersprite[i] = br.ReadString();
                    GStruct.guild[intguildnum].memberhue[i] = br.ReadInt32();
                    GStruct.guild[intguildnum].membersprite_index[i] = br.ReadInt32();
                }

                //Fecha o leitor
                br.Close();

                //Responde que a guilda foi carregada
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }
        }
        public static bool SaveGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.ExtendMyApp(MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Guilds/" + guildnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intguildnum = Convert.ToInt32(guildnum);

            //Salvamos os dados básicos do item
            bw.Write(GStruct.guild[intguildnum].name);
            bw.Write(GStruct.guild[intguildnum].level);
            bw.Write(GStruct.guild[intguildnum].exp);
            bw.Write(GStruct.guild[intguildnum].shield);
            bw.Write(GStruct.guild[intguildnum].hue);
            bw.Write(GStruct.guild[intguildnum].leader);

            //Salvamos os efeitos dos itens
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                bw.Write(GStruct.guild[intguildnum].memberlist[i]);
                bw.Write(GStruct.guild[intguildnum].membersprite[i]);
                bw.Write(GStruct.guild[intguildnum].memberhue[i]);
                bw.Write(GStruct.guild[intguildnum].membersprite_index[i]);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        public static void ClearGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intguildnum = Convert.ToInt32(guildnum);

            //Limpamos o tamanho do mapa
            GStruct.guild[intguildnum].name = "";
            GStruct.guild[intguildnum].level = 0;
            GStruct.guild[intguildnum].exp = 0;
            GStruct.guild[intguildnum].shield = 0;
            GStruct.guild[intguildnum].hue = 0;
            GStruct.guild[intguildnum].leader = 0;

            //Carregamos os efeitos em seguida
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                GStruct.guild[intguildnum].memberlist[i] = "";
                GStruct.guild[intguildnum].membersprite[i] = "";
                GStruct.guild[intguildnum].memberhue[i] = 0;
                GStruct.guild[intguildnum].membersprite_index[i] = 0;
            }
        }
        public static void LoadGuilds()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual index está disponível para o jogador
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                if (LoadGuild(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    ClearGuild(Convert.ToString(i));
                    SaveGuild(Convert.ToString(i));
                }
            }

        }

        #endregion
    }
}