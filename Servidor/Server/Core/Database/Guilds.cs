using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Guilds
    {
        public static bool loadGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, guildnum));
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
                GuildStruct.guild[intguildnum].name = br.ReadString();
                GuildStruct.guild[intguildnum].level = br.ReadInt32();
                GuildStruct.guild[intguildnum].exp = br.ReadInt32();
                GuildStruct.guild[intguildnum].shield = br.ReadInt32();
                GuildStruct.guild[intguildnum].hue = br.ReadInt32();
                GuildStruct.guild[intguildnum].leader = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 1; i < Globals.Max_Guild_Members; i++)
                {
                    GuildStruct.guild[intguildnum].memberlist[i] = br.ReadString();
                    GuildStruct.guild[intguildnum].membersprite[i] = br.ReadString();
                    GuildStruct.guild[intguildnum].memberhue[i] = br.ReadInt32();
                    GuildStruct.guild[intguildnum].membersprite_s[i] = br.ReadInt32();
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
        public static bool saveGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Guilds/" + guildnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intguildnum = Convert.ToInt32(guildnum);

            //Salvamos os dados básicos do item
            bw.Write(GuildStruct.guild[intguildnum].name);
            bw.Write(GuildStruct.guild[intguildnum].level);
            bw.Write(GuildStruct.guild[intguildnum].exp);
            bw.Write(GuildStruct.guild[intguildnum].shield);
            bw.Write(GuildStruct.guild[intguildnum].hue);
            bw.Write(GuildStruct.guild[intguildnum].leader);

            //Salvamos os efeitos dos itens
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                bw.Write(GuildStruct.guild[intguildnum].memberlist[i]);
                bw.Write(GuildStruct.guild[intguildnum].membersprite[i]);
                bw.Write(GuildStruct.guild[intguildnum].memberhue[i]);
                bw.Write(GuildStruct.guild[intguildnum].membersprite_s[i]);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        public static void clearGuild(string guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intguildnum = Convert.ToInt32(guildnum);

            //Limpamos o tamanho do mapa
            GuildStruct.guild[intguildnum].name = "";
            GuildStruct.guild[intguildnum].level = 0;
            GuildStruct.guild[intguildnum].exp = 0;
            GuildStruct.guild[intguildnum].shield = 0;
            GuildStruct.guild[intguildnum].hue = 0;
            GuildStruct.guild[intguildnum].leader = 0;

            //Carregamos os efeitos em seguida
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                GuildStruct.guild[intguildnum].memberlist[i] = "";
                GuildStruct.guild[intguildnum].membersprite[i] = "";
                GuildStruct.guild[intguildnum].memberhue[i] = 0;
                GuildStruct.guild[intguildnum].membersprite_s[i] = 0;
            }
        }
        public static void loadGuilds()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                if (loadGuild(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearGuild(Convert.ToString(i));
                    saveGuild(Convert.ToString(i));
                }
            }

        }
    }
}
