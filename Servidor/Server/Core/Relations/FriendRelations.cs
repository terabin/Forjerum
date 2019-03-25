using System;
using System.Reflection;

namespace __Forjerum
{
    class FriendRelations
    {
        //*********************************************************************************************
        // getFriendOpenSlot
        //*********************************************************************************************
        public static int getFriendOpenSlot(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Friends; i++)
            {
                if (String.IsNullOrEmpty(PlayerStruct.player[s].friend[i].name))
                {
                    return i;
                }
            }
            return 0;
        }
        //*********************************************************************************************
        // friendNameExist
        //*********************************************************************************************
        public static bool friendNameExist(int s, string name)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, name) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, name));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Friends; i++)
            {
                if (PlayerStruct.player[s].friend[i].name == name)
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // friendIsOnline
        //*********************************************************************************************
        public static bool friendIsOnline(int s, int friendnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum));
            }

            //CÓDIGO
            for (int i = 1; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.player[s].friend[friendnum].name == PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName)
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // getPlayerFriendsCount
        //*********************************************************************************************
        public static int getPlayerFriendsCount(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int count = 0;
            for (int i = 1; i < Globals.Max_Friends; i++)
            {
                if (!String.IsNullOrEmpty(PlayerStruct.player[s].friend[i].name))
                {
                    count += 1;
                }
                else
                {
                    break;
                }
            }
            return count;
        }
        //*********************************************************************************************
        // refreshFriends
        // Atualiza a lista de amigos de determinado jogador
        //*********************************************************************************************
        public static void refreshFriends(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int friendscount = getPlayerFriendsCount(s);
            for (int i = 1; i <= friendscount; i++)
            {
                //Analisar todos os jogadores online
                for (int y = 0; y <= Globals.Player_Highs; y++)
                {
                    if (PlayerStruct.player[s].friend[i].name == PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].CharacterName)
                    {
                        PlayerStruct.player[s].friend[i].sprite = PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].Sprite;
                        PlayerStruct.player[s].friend[i].sprite_s = PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].Sprites;
                        PlayerStruct.player[s].friend[i].classid = PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].ClassId;
                        PlayerStruct.player[s].friend[i].level = PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].Level;
                        PlayerStruct.player[s].friend[i].guildname = GuildStruct.guild[PlayerStruct.character[y, PlayerStruct.player[y].SelectedChar].Guild].name;
                    }
                }
            }
            SendData.sendPlayerFriends(s);
        }
        //*********************************************************************************************
        // addFriend
        // Adiciona um jogador na lista de amigos de outro
        //*********************************************************************************************
        public static bool addFriend(int s, int friendnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum));
            }

            //CÓDIGO
            //Valor principal
            int friendslot = getFriendOpenSlot(s);
            if (friendslot <= 0) { return false; }

            //Verificar se já não está na lista
            if (friendNameExist(s, PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].CharacterName))
            {
                return false;
            }

            //Tentar adicionar
            try
            {
                PlayerStruct.player[s].friend[friendslot].name = PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].CharacterName;
                PlayerStruct.player[s].friend[friendslot].sprite = PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].Sprite;
                PlayerStruct.player[s].friend[friendslot].sprite_s = PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].Sprites;
                PlayerStruct.player[s].friend[friendslot].classid = PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].ClassId;
                PlayerStruct.player[s].friend[friendslot].level = PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].Level;
                PlayerStruct.player[s].friend[friendslot].guildname = GuildStruct.guild[PlayerStruct.character[friendnum, PlayerStruct.player[friendnum].SelectedChar].Guild].name;
                if (String.IsNullOrEmpty(PlayerStruct.player[s].friend[friendslot].guildname)) { PlayerStruct.player[s].friend[friendslot].guildname = ""; }
                SendData.sendPlayerFriends(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //*********************************************************************************************
        // deleteFriend
        // Retira determinado jogador da lista de amigos de outro
        //*********************************************************************************************
        public static bool deleteFriend(int s, int friendnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, friendnum));
            }

            //CÓDIGO
            if (friendnum == 0) { return false; }
            try
            {
                int friendscount = getPlayerFriendsCount(s) + 1;
                PlayerStruct.player[s].friend[friendnum].name = "";
                PlayerStruct.player[s].friend[friendnum].sprite = "";
                PlayerStruct.player[s].friend[friendnum].sprite_s = 0;
                PlayerStruct.player[s].friend[friendnum].classid = 0;
                PlayerStruct.player[s].friend[friendnum].level = 0;
                PlayerStruct.player[s].friend[friendnum].guildname = "";
                if (friendnum < friendscount)
                {
                    for (int i = friendnum + 1; i <= friendscount; i++)
                    {
                        PlayerStruct.player[s].friend[i - 1].name = PlayerStruct.player[s].friend[i].name;
                        PlayerStruct.player[s].friend[i - 1].sprite = PlayerStruct.player[s].friend[i].sprite;
                        PlayerStruct.player[s].friend[i - 1].sprite_s = PlayerStruct.player[s].friend[i].sprite_s;
                        PlayerStruct.player[s].friend[i - 1].classid = PlayerStruct.player[s].friend[i].classid;
                        PlayerStruct.player[s].friend[i - 1].level = PlayerStruct.player[s].friend[i].level;
                        PlayerStruct.player[s].friend[i - 1].guildname = PlayerStruct.player[s].friend[i].guildname;
                    }
                }
                SendData.sendPlayerFriends(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
