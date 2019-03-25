using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Accounts
    {
        //*********************************************************************************************
        // AccountExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se determinada conta existe.
        //*********************************************************************************************
        public static bool accountExists(string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email)); }

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
        public static bool tryLogin(int s, string email, string password)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, email, password) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, email, password)); }

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
                PlayerStruct.player[s].Password = br.ReadString();

                //Compara os dados e manda um negativo caso estejam errados
                if (email2.ToLower() == email.ToLower()) { } else { br.Close(); return false; }
                if (PlayerStruct.player[s].Password == password) { } else { br.Close(); return false; }

                //Termina de ler
                PlayerStruct.player[s].Username = br.ReadString();
                PlayerStruct.player[s].Confirmed = br.ReadBoolean();
                PlayerStruct.player[s].Premmy = br.ReadString();
                PlayerStruct.player[s].WPoints = br.ReadInt32();
                PlayerStruct.player[s].Banned = br.ReadString();

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
        public static bool addWPoints(string email, int points)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, points) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, points)); }

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
        public static bool addPremmy(string email, int days)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, days) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, days)); }

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
        public static bool addBan(string email, int days)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, days) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email, days)); }

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
        // saveAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva a conta de determinado jogador online.
        //*********************************************************************************************
        public static bool saveAccount(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s)); }

            //CÓDIGO
            //representa o arquivo
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + PlayerStruct.player[s].Email + ".dat", FileMode.Open);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            bw.Write(PlayerStruct.player[s].Email);
            bw.Write(PlayerStruct.player[s].Password);
            bw.Write(PlayerStruct.player[s].Username);
            bw.Write(PlayerStruct.player[s].Confirmed);
            bw.Write(PlayerStruct.player[s].Premmy);
            bw.Write(PlayerStruct.player[s].WPoints);
            bw.Write(PlayerStruct.player[s].Banned);

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // RegisterNewAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Registra uma nova conta com os parâmetros enviados.
        //*********************************************************************************************
        public static bool registerNewAccount(int s, string username, string password, string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, username, password, email) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, username, password, email)); }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat") == false)
            {
                //representa o arquivo que vamos criar
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + email + ".dat", FileMode.Create);

                //Definimos o escrivão do arquivo. hue
                BinaryWriter bw = new BinaryWriter(file);

                PlayerStruct.player[s].Username = username;
                PlayerStruct.player[s].Email = email;
                PlayerStruct.player[s].Password = password;
                PlayerStruct.player[s].Confirmed = false;
                PlayerStruct.player[s].Premmy = DateTime.Now.ToString();
                PlayerStruct.player[s].WPoints = 0;
                PlayerStruct.player[s].Banned = DateTime.Now.ToString();


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
        // DeleteAccount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Deletar determinada conta.
        //*********************************************************************************************
        public static bool deleteAccount(string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, email) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, email));
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
    }
}
