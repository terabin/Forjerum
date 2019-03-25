using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class FriendLists
    {
        //*********************************************************************************************
        // saveFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva a lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool saveFriendList(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s)); }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PlayerStruct.player[s].Username + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int friendscount = FriendRelations.getPlayerFriendsCount(s);

            bw.Write(friendscount);

            for (int i = 1; i <= friendscount; i++)
            {
                bw.Write(PlayerStruct.player[s].friend[i].name);
                bw.Write(PlayerStruct.player[s].friend[i].sprite);
                bw.Write(PlayerStruct.player[s].friend[i].sprite_s);
                bw.Write(PlayerStruct.player[s].friend[i].classid);
                bw.Write(PlayerStruct.player[s].friend[i].level);
                bw.Write(PlayerStruct.player[s].friend[i].guildname);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // loadFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega a lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool loadFriendList(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PlayerStruct.player[s].Username + ".dat"))
                {

                    //representa o arquivo
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/FriendLists/" + PlayerStruct.player[s].Username + ".dat", FileMode.Open);

                    //cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    int friendcount = br.ReadInt32();

                    for (int i = 1; i <= friendcount; i++)
                    {
                        PlayerStruct.player[s].friend[i].name = br.ReadString();
                        PlayerStruct.player[s].friend[i].sprite = br.ReadString();
                        PlayerStruct.player[s].friend[i].sprite_s = br.ReadInt32();
                        PlayerStruct.player[s].friend[i].classid = br.ReadInt32();
                        PlayerStruct.player[s].friend[i].level = br.ReadInt32();
                        PlayerStruct.player[s].friend[i].guildname = br.ReadString();
                    }

                    //Fecha o leitor
                    br.Close();

                    //if (String.IsNullOrEmpty(MapStruct.map[Convert.ToInt32(mapnum)].max_width)) { clearMap(mapnum); saveMap(mapnum); }

                    //Responde que o item foi carregado
                    return true;
                }
                else
                //Responde que o mapa não existe
                { return false; }
            }
        }
        //*********************************************************************************************
        // clearFriendList / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Apenas descarrega a estrutura da lista de amigos de determinado jogador.
        //*********************************************************************************************
        public static bool clearFriendList(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s)); }

            //CÓDIGO
            try
            {

                for (int i = 1; i < Globals.Max_Friends; i++)
                {
                    PlayerStruct.player[s].friend[i].name = "";
                    PlayerStruct.player[s].friend[i].sprite = "";
                    PlayerStruct.player[s].friend[i].sprite_s = 0;
                    PlayerStruct.player[s].friend[i].classid = 0;
                    PlayerStruct.player[s].friend[i].level = 0;
                    PlayerStruct.player[s].friend[i].guildname = "";
                }
                return true;
            }
            catch { return false; }
        }

    }
}
