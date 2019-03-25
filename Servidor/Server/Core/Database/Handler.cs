using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

namespace __Forjerum.Database
{
    //*********************************************************************************************
    // Relacionado a todo o gerenciamento da Database do jogo.
    // Database.cs
    //*********************************************************************************************
    class Handler : Languages.LStruct
    {
        //*********************************************************************************************
        // Construtores que vão armazenas estatíticas do servidor, tais como erros e estado baseado
        // no tempo.
        //*********************************************************************************************
        static StringBuilder sbLogger = new StringBuilder ();
        static StringBuilder sbStatus = new StringBuilder ();
        //*********************************************************************************************
        // nameIsIllegal / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool nameIsIllegal(string name)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, name));
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
        // defineClassesData / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void defineClassesData()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return; }
 
            //CÓDIGO
            string[] temp;
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\classes_info.txt", Encoding.GetEncoding("iso-8859-1"));
            s.ReadLine();

            for (int i = 1; i <= Globals.Max_Classes; i++)
            {
                PlayerStruct.classes[i].fire = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PlayerStruct.classes[i].earth = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PlayerStruct.classes[i].water = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PlayerStruct.classes[i].wind = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PlayerStruct.classes[i].dark = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                PlayerStruct.classes[i].light = Convert.ToInt32(s.ReadLine().Split(':')[1]);
                temp = s.ReadLine().Split(':')[1].Split(';');
                PlayerStruct.classes[i].sprite_name = new string[2];
                PlayerStruct.classes[i].sprite_s = new int[2];
                PlayerStruct.classes[i].sprite_name[0] = temp[0];
                PlayerStruct.classes[i].sprite_s[0] = Convert.ToInt32(temp[1]);
                temp = s.ReadLine().Split(':')[1].Split(';');
                PlayerStruct.classes[i].sprite_name[1] = temp[0];
                PlayerStruct.classes[i].sprite_s[1] = Convert.ToInt32(temp[1]);
                s.ReadLine();
            }

            s.Close();
        }
        //*********************************************************************************************
        // getGameName / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm o nome do jogo
        //*********************************************************************************************
        public static string getGameName()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\game_name.txt", Encoding.GetEncoding("iso-8859-1"));

            string game_name = s.ReadToEnd();
            s.Close();
            return game_name;
        }
        //*********************************************************************************************
        // getMOTD / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm a mensagem do dia
        //*********************************************************************************************
        public static string getMOTD()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\motd.txt", Encoding.GetEncoding("iso-8859-1"));

            string motd = s.ReadToEnd();
            s.Close();
            return motd;
        }
        //*********************************************************************************************
        // getNotice / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm a notícia atual
        //*********************************************************************************************
        public static string getNotice()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name).ToString(); }

            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\notice.txt", Encoding.GetEncoding("iso-8859-1"));

            string notice = s.ReadToEnd();
            s.Close();
            return notice;
        }
        //*********************************************************************************************
        // defineAdmin / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm do admin_mail.txt o admin
        //*********************************************************************************************
        public static void defineAdmin()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name) != null) { return; }
            
            //CÓDIGO
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\admin_mail.txt", Encoding.GetEncoding("iso-8859-1"));

            string admin_mail = s.ReadToEnd();
            Globals.MASTER_EMAIL = admin_mail;
            Console.WriteLine(lang.admin_status_attr_to_account + ": " + admin_mail);
            s.Close();
        }
        //*********************************************************************************************
        // logError / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void logError(Exception e)
        {
            sbLogger.AppendLine(lang.error_log + ": " + e.Message + " [" + lang.origin + ": " + e.Source + "]" + "  [" + lang.extra + ": " + e.StackTrace + "|" + e.InnerException);
        }
        //*********************************************************************************************
        // logAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void logAdd(string message)
        {
            sbLogger.AppendLine(lang.info_log + ": " + message);
        }
        //*********************************************************************************************
        // statusAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void statusAdd(string message)
        {
            sbStatus.AppendLine(message);
        }
        //*********************************************************************************************
        // DischargeWrite / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obtêm informações das classes
        //*********************************************************************************************
        public static void dischargeWriter()
        {
            File.WriteAllText("./Log.txt", sbLogger.ToString());
            sbLogger.Clear();
            File.WriteAllText("./Status.txt", sbStatus.ToString());
            sbStatus.Clear();
        }
    }
}