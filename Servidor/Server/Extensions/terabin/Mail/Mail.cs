using System;
using System.IO;
using System.Net.Mail;

namespace FORJERUM.Extensions
{
    //*********************************************************************************************
    // Sistema para interação com EMAILS no Forjerum
    // Mail.cs
    //*********************************************************************************************
    class Mail : Languages.LStruct  
    {
        public static string SMTP_SERVER = GET_SMTP_SERVER();
        public static string SMTP_USER = GET_SMTP_USER();
        public static string SMTP_PASS = GET_SMTP_PASS();


        //*********************************************************************************************
        // SelectPacket 
        // Método é obrigado a checar novas packets recebidas
        //*********************************************************************************************
        public static object SelectPacket(params object[] args)
        {
            //Parâmetros originais
            int index = Convert.ToInt32(args[1]);
            string data = args[2].ToString();

            //Ação
            Loops.Last_Packet = data;
            string[] packets = data.Split('\\');
            for (int i = 0; i < packets.Length; i++)
            {
                if (packets[i] == String.Empty) { break; }
                if ((!PStruct.tempplayer[index].ingame))
                {
                    //Tratamento das packets
                    if (packets[i].StartsWith("<67>")) { ReceivedForget(index, packets[i]); }
                    else if (packets[i].StartsWith("<68>")) { ReceivedActivation(index, packets[i]); }
                    else if (packets[i].StartsWith("<69>")) { ReceivedEmailActivation(index); }
                }
            }

            //Extender, não sob escrever
            return ExtensionApp.EXTEND;
        }
        //*********************************************************************************************
        // GET_SMTP_SERVER 
        //*********************************************************************************************
        public static string GET_SMTP_SERVER()
        {
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\smtp_server.txt");

            string smtp_server = s.ReadToEnd();
            s.Close();
            return smtp_server;
        }
        //*********************************************************************************************
        // GET_SMTP_USER
        //*********************************************************************************************
        public static string GET_SMTP_USER()
        {
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\smtp_user.txt");

            string smtp_user = s.ReadToEnd();
            s.Close();
            return smtp_user;
        }
        //*********************************************************************************************
        // GET_SMTP_PASS
        //*********************************************************************************************
        public static string GET_SMTP_PASS()
        {
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\smtp_pass.txt");

            string smtp_pass = s.ReadToEnd();
            s.Close();
            return smtp_pass;
        }
        //*********************************************************************************************
        // ReceivedRegister
        // No registro é necessário enviar o código de ativação
        //*********************************************************************************************
        public static object ReceivedRegister(params object[] args)
        {
            int Index = Convert.ToInt32(args[1]);
            string data = args[2].ToString();

            string[] register = data.Replace("<5>", "").Split(';');

            if (register.Length != 3) { return false; }
            if (register[0].Length > 8) { return false; }
            if (register[1].Length > 8) { return false; }
            if (register[2].Length > 100) { return false; }

            if (Database.NameIsIllegal(register[0])) { return false; }
            if (Database.NameIsIllegal(register[1])) { return false; }
            if (Database.NameIsIllegal(register[2])) { return false; }

            register[2] = register[2].ToLower();

            if (!(Database.AccountExists(register[2])))
            {
              Mail.Send_ActivationCode(Index, register[2]);
            }
            
            //Extender, não sob escrever
            return ExtensionApp.EXTEND;
        }
        //*********************************************************************************************
        // ReceivedActivation
        // Recebe a tentativa para ativar determinada conta.
        //*********************************************************************************************
        public static void ReceivedActivation(int index, string data)
        {
            string[] packet = data.Replace("<68>", "").Split(';');

            if (packet.Length != 1) { return; }
            if (!ReceiveData.IsNumeric(packet[0])) { return; }
            if (packet[0].Length < 4) { return; }
            if (packet[0].Length > 5) { return; }

            if (Convert.ToInt32(packet[0]) == PStruct.tempplayer[index].ActivationCode)
            {
                PStruct.player[index].Confirmed = true;
                Database.SaveAccount(index);
                if (!PStruct.tempplayer[index].Logged)
                {
                    SendData.SendToUser(index, String.Format("<5 {0};{1}>a</5>\n", PStruct.player[index].Email, PStruct.player[index].Password));
                }
                else
                {
                    SendData.SendToUser(index, String.Format("<5 {0};{1}>v</5>\n", "", ""));
                }
            }
            else
            {
                SendData.SendToUser(index, String.Format("<5 {0};{1}>f</5>\n", PStruct.player[index].Email, ""));
            }
        }
        //*********************************************************************************************
        // ReceivedForget
        // Recebe o pedido para que o servidor envie os dados perdidos do jogados
        //*********************************************************************************************
        public static void ReceivedForget(int index, string data)
        {
            string[] packet = data.Replace("<67>", "").Split(';');
            if (packet.Length != 2) { return; }
            if (!ReceiveData.IsNumeric(packet[0])) { return; }
            if (Convert.ToInt32(packet[0]) > 1) { return; }
            if (Convert.ToInt32(packet[0]) < 0) { return; }
            if (packet[1].Length > 100) { return; }
            if (packet[1].Length < 5) { return; }

            Mail.Send_Account(index, 1, packet[1]);
        }
        //*********************************************************************************************
        // ReceivedEmailActivation
        // Envia novamente o código para confirmação
        //*********************************************************************************************
        public static void ReceivedEmailActivation(int index)
        {
            if (PStruct.tempplayer[index].ActivationCode > 0) { return; }
            Mail.Send_ActivationCode(index, PStruct.player[index].Email);
        }
        //*********************************************************************************************
        // Send_ActivationCode
        //*********************************************************************************************
        public static void Send_ActivationCode(int index, string email)
        {
            //Define os dados do e-mail
            string nomeRemetente = Globals.GAME_NAME;
            string emailRemetente = SMTP_USER;

            Console.WriteLine(lang.sent_to + email);

            string emailDestinatario = email;
            string emailComCopia = email;
            string emailComCopiaOculta = email;

            string assuntoMensagem = Globals.GAME_NAME + " - " + lang.confirmation_code;

            int code = Globals.Rand(1000, 9999);

            PStruct.tempplayer[index].ActivationCode = code;

            Console.WriteLine(email);

            string conteudoMensagem = lang.your_confirmation_code_is + code;

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            try
            {
                objEmail.To.Add(emailDestinatario);
            }
            catch
            {
                SendData.SendToUser(index, String.Format("<5 {0};{1}>h</5>\n", "", ""));
                Database.DeleteAccount(PStruct.player[index].Email);
            }

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            SmtpClient SmtpServer = new SmtpClient(SMTP_SERVER);
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASS);
            SmtpServer.EnableSsl = true;

            //Enviamos o e-mail através do método .send()
            try
            {
                SmtpServer.SendAsync(objEmail, null);
            }
            catch (Exception ex)
            {
                SendData.SendToUser(index, String.Format("<5 {0};{1}>h</5>\n", "", ""));
                Database.DeleteAccount(PStruct.player[index].Email);
                Console.Write(lang.problems_in_email_sent + lang.error + " = " + ex.Message);
            }

        }
        //*********************************************************************************************
        // Send_Account
        //*********************************************************************************************
        public static void Send_Account(int index, int type, string data)
        {
            string user;
            string password;
            string email;

            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + data + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Accounts/" + data + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê primeiros dados
                email = br.ReadString();
                password = br.ReadString();
                user = br.ReadString();

                //Fecha o leitor
                br.Close();

            }
            else
            {
                SendData.SendToUser(index, String.Format("<5 {0};{1}>o</5>\n", "", ""));
                return;
            }

            //Define os dados do e-mail
            string nomeRemetente = Globals.GAME_NAME;
            string emailRemetente = SMTP_USER;

            string emailDestinatario = email;
            string emailComCopia = email;
            string emailComCopiaOculta = email;

            string assuntoMensagem = Globals.GAME_NAME + " - " + lang.account_recover;
            string conteudoMensagem = lang.email + ": " + email + lang.login + ": " + user + lang.password + ": " + password ;

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagemn
            //objEmail.Attachments.Add(anexo);


            SmtpClient SmtpServer = new SmtpClient(SMTP_SERVER);
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASS);
            SmtpServer.EnableSsl = true;


            //Enviamos o e-mail através do método .send()
            try
            {
                SmtpServer.SendAsync(objEmail, null);
                SendData.SendToUser(index, String.Format("<5 {0};{1}>k</5>\n", "", ""));
                //Response.Write("E-mail enviado com sucesso !");
            }
            catch (Exception ex)
            {
                SendData.SendToUser(index, String.Format("<5 {0};{1}>o</5>\n", "", ""));
                Console.WriteLine(lang.problems_in_email_sent + lang.error + " = " + ex.Message);
            }

        }
    }
}
