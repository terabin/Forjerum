using System;
using System.Text;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Métodos e tratamentos de todas as informações que serão enviadas pelo servidor.
    // sendData.cs
    //*********************************************************************************************
    class SendData
    {
        //*********************************************************************************************
        // sendToAll / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todas as conexões.
        //*********************************************************************************************
        public static void sendToAll(string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                WinsockAsync.send(i, packet);
            }
        }
        //*********************************************************************************************
        // sendToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todos os jogadores em determinado mapa.
        //*********************************************************************************************
        public static void sendToMap(int map, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == map) && (PlayerStruct.tempplayer[i].ingame == true))
                {
                    sendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // sendToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todos os membros de determinada guilda.
        //*********************************************************************************************
        public static void sendToGuild(int guild, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guild, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Guild == guild) && (PlayerStruct.tempplayer[i].ingame == true))
                {
                    sendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // sendToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para membros de determinado grupo.
        //*********************************************************************************************
        public static void sendToParty(int partynum, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if ((PlayerStruct.tempplayer[i].Party == partynum) && (PlayerStruct.tempplayer[i].ingame == true))
                {
                    sendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // sendToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para jogadores em um determinado mapa com exceção de um determinado
        // jogador.
        //*********************************************************************************************
        public static void sendToMapBut(int s, int map, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, map, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (i != s)
                {
                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == map)  && (PlayerStruct.tempplayer[i].ingame == true))
                    {
                        sendToUser(i, packet);
                    }
                }
            }
        }
        //*********************************************************************************************
        // sendToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet apenas para um determinado usuário.
        //*********************************************************************************************
        public static void sendToUser(int clientid, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                if (WinsockAsync.Clients[i].s == clientid)
                {
                    WinsockAsync.send(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // sendPlayerDataToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de um jogador para todos em determinado mapa com exceção de um jogador.
        //*********************************************************************************************
        public static void sendPlayerDataToMapBut(int s, string username, int charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, username, charId) != null)
            {
                return;
            }


            //CÓDIGO
            string charName = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName);
            int charSprites = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites);
            int charClass = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId);
            string charSprite = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite);
            int charLevel = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level);
            int charExp = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp);
            int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
            int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
            int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
            int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
            int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
            int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
            int charPoints = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points;
            int charMap = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            byte charX = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
            byte charY = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
            byte charDir = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir);
            int charVitality = PlayerStruct.tempplayer[s].Vitality;
            int charSpirit = PlayerStruct.tempplayer[s].Spirit;
            int charAccess = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access);
            int SkillPoints = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints;
            int charHue = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue;
            int charGender = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gender;
            string Equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;
            int extra_vitality = PlayerRelations.getExtraVitality(s);
            int extra_spirit = PlayerRelations.getExtraVitality(s);

            string packet = "";
            packet = packet + s + ";";
            packet = packet + charName + ";"; packet = packet + charSprites + ";"; packet = packet + charClass + ";"; packet = packet + charSprite + ";"; packet = packet + charLevel + ";";
            packet = packet + charExp + ";"; packet = packet + charFire + ";"; packet = packet + charEarth + ";"; packet = packet + charWater + ";"; packet = packet + charWind + ";";
            packet = packet + charDark + ";"; packet = packet + charLight + ";"; packet = packet + charPoints + ";"; packet = packet + charMap + ";"; packet = packet + charX + ";";
            packet = packet + charY + ";"; packet = packet + charDir + ";"; packet = packet + charVitality + ";"; packet = packet + charSpirit + ";"; packet = packet + charAccess + ";";
            packet = packet + charHue + ";"; packet = packet + charGender + ";"; packet = packet + SkillPoints + ";"; packet = packet + Equipment + ";"; packet = packet + extra_vitality + ";";
            packet = packet + extra_spirit + ";";

            sendToMapBut(s, charMap, String.Format("<8>{0}></8>\n", packet));

        }
        //*********************************************************************************************
        // sendPlayerDataTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de um determinado jogador para outro determinado jogador.
        //*********************************************************************************************
        public static void sendPlayerDataTo(int Receiver, int s, string username, int charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Receiver, s, username, charId) != null)
            {
                return;
            }

            //CÓDIGO
            string charName = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName;
            int charSprites = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites;
            int charClass = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId;
            string charSprite = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite;
            int charLevel = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level;
            int charExp = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp;
            int charFire = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire;
            int charEarth = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth;
            int charWater = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water;
            int charWind = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind;
            int charDark = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark;
            int charLight = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light;
            int charPoints = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points;
            int charMap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            byte charX = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            byte charY = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;
            byte charDir = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir;
            int charVitality = PlayerStruct.tempplayer[s].Vitality;
            int charSpirit = PlayerStruct.tempplayer[s].Spirit;
            int charAccess = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access;
            int charHue = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue;
            int charGender = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gender;
            int SkillPoints = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints;
            string Equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;

            string packet = "";
            packet = packet + s + ";";
            packet = packet + charName + ";"; packet = packet + charSprites + ";"; packet = packet + charClass + ";"; packet = packet + charSprite + ";"; packet = packet + charLevel + ";";
            packet = packet + charExp + ";"; packet = packet + charFire + ";"; packet = packet + charEarth + ";"; packet = packet + charWater + ";"; packet = packet + charWind + ";";
            packet = packet + charDark + ";"; packet = packet + charLight + ";"; packet = packet + charPoints + ";"; packet = packet + charMap + ";"; packet = packet + charX + ";";
            packet = packet + charY + ";"; packet = packet + charDir + ";"; packet = packet + charVitality + ";"; packet = packet + charSpirit + ";"; packet = packet + charAccess + ";";
            packet = packet + charHue + ";"; packet = packet + charGender + ";"; packet = packet + SkillPoints + ";"; packet = packet + Equipment + ";";

            sendToUser(Receiver, String.Format("<8>{0}</8>\n", packet));

        }
        //*********************************************************************************************
        // sendUpdatePlayerHighs / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendUpdatePlayerHighs()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            sendToAll(String.Format("<9>{0}</9>\n", Globals.Player_Highs));
        }
        //*********************************************************************************************
        // sendPlayerXY / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a posição de um jogador para um mapa.
        //*********************************************************************************************
        public static void sendPlayerXY(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int charX = (PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].X);
            int charY = (PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Y);
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<10>{0}</10>\n", s + ";" + charX + ";" + charY));
        }
        //*********************************************************************************************
        // sendMsgToAll / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat de todos.
        //*********************************************************************************************
        public static void sendMsgToAll(string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            sendToAll(String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // sendMsgToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat para jogadores em um determinado mapa.
        //*********************************************************************************************
        public static void sendMsgToMap(int s, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // sendMsgToPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat para um determinado jogador.
        //*********************************************************************************************
        public static void sendMsgToPlayer(int s, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<11 {0};{1}>{2}</11>\n", msg, color, type));
        }
        //*********************************************************************************************
        // sendMsgToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat de um determinado grupo.
        //*********************************************************************************************
        public static void sendMsgToParty(int party, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, party, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            sendToParty(party, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // sendMsgToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada mensagem no chat para uma guilda determinada.
        //*********************************************************************************************
        public static void sendMsgToGuild(int guild, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guild, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            sendToGuild(guild, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // sendInvSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o inventário de um determinado jogador.
        //*********************************************************************************************
        public static void sendInvSlots(int s, int characterslot)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, characterslot) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            packet = packet + (Globals.MaxInvSlot - 1) + ";";

            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                packet = packet + i + ";";
                packet = packet + s + ";";
                packet = packet + PlayerStruct.invslot[s, i].item + ";";
            }

            sendToUser(s, String.Format("<12>{0}</12>\n", packet));

        }
        //*********************************************************************************************
        // sendNpcTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNpcTo(int s, int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            string name = NpcStruct.npc[map, id].Name;
            string sprite = NpcStruct.npc[map, id].Sprite;
            int npcs = NpcStruct.npc[map, id].s;
            int x = NpcStruct.tempnpc[map, id].X;
            int y = NpcStruct.tempnpc[map, id].Y;
            int vitality = NpcStruct.npc[map, id].Vitality;
            int dir = NpcStruct.tempnpc[map, id].Dir;

            sendToUser(s, String.Format("<14 {0};{1};{2};{3};{4};{5};{6}>{7}</14>\n", id, name, sprite, npcs, x, y, vitality, dir));
        }
        //*********************************************************************************************
        // sendNpcToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNpcToMap(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            if (NpcStruct.tempnpc[map, id].Dead == true) { return; }

            string packet = "";
            packet += id + ";";
            packet += NpcStruct.npc[map, id].Name + ";";
            packet += NpcStruct.npc[map, id].Sprite + ";";
            packet += NpcStruct.npc[map, id].s + ";";
            packet += NpcStruct.tempnpc[map, id].X + ";";
            packet += NpcStruct.tempnpc[map, id].Y + ";";
            packet += NpcStruct.npc[map, id].Vitality + ";";
            packet += NpcStruct.tempnpc[map, id].Dir + ";";

            packet = "1" + ";" + packet;

            sendToMap(map, String.Format("<14>{0}</14>\n", packet));
        }
        //*********************************************************************************************
        // sendMapNpcsTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendMapNpcsTo(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int map = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);

            if (MapStruct.tempmap[map].NpcCount == 0) { return; }

            string packet = "";
            int count = 0;

            for (int i = 1; i <= MapStruct.tempmap[map].NpcCount; i++)
            {
                if (!NpcStruct.tempnpc[map, i].Dead)
                {
                    packet += i + ";";
                    packet += NpcStruct.npc[map, i].Name + ";";
                    packet += NpcStruct.npc[map, i].Sprite + ";";
                    packet += NpcStruct.npc[map, i].s + ";";
                    packet += NpcStruct.tempnpc[map, i].X + ";";
                    packet += NpcStruct.tempnpc[map, i].Y + ";";
                    packet += NpcStruct.npc[map, i].Vitality + ";";
                    packet += NpcStruct.tempnpc[map, i].Dir + ";";
                    count += 1;
                }
            }

            packet = count + ";" + packet;

            sendToUser(s, String.Format("<14>{0}</14>\n", packet));
        }
        //*********************************************************************************************
        // sendNpcMove / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNpcMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            packet += id + ";"; packet += NpcStruct.tempnpc[map, id].X + ";"; packet += NpcStruct.tempnpc[map, id].Y + ";";
            sendToMap(map, String.Format("<15>{0}</15>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerLeft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa ao mapa que determinado jogador saiu.
        //*********************************************************************************************
        public static void sendPlayerLeft(int map, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMapBut(s, map, String.Format("<16>{0}</16>\n", s));
        }
        //*********************************************************************************************
        // sendPlayerEquipmentToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações dos itens equipados de um determinado jogador ao mapa.
        // O visual style está praticamente feito, apenas falta o client side. ;)
        //*********************************************************************************************
        public static void sendPlayerEquipmentToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<17 {0}>{1}</17>\n", s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment));

        }
        //*********************************************************************************************
        // sendPlayerEquipmentTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de itens equipados de um jogador para outro.
        //*********************************************************************************************
        public static void sendPlayerEquipmentTo(int s, int players)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, players) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<17 {0}>{1}</17>\n", players, PlayerStruct.character[players, PlayerStruct.player[players].SelectedChar].Equipment));

        }
        //*********************************************************************************************
        // sendPlayerSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSkills(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            packet = packet + (Globals.MaxPlayer_Skills - 1) + ";";

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                packet = packet + i + ";";
                packet = packet + PlayerStruct.skill[s, i].num + ";";
                packet = packet + PlayerStruct.skill[s, i].level + ";";
            }

            sendToUser(s, String.Format("<18>{0}</18>\n", packet));

        }
        //*********************************************************************************************
        // sendPlayerFriends / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a lista de amigos.
        //*********************************************************************************************
        public static void sendPlayerFriends(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            int friendscount = FriendRelations.getPlayerFriendsCount(s);

            packet = packet + friendscount + ";";

            //PlayerStruct.addFriend(s, s);

            for (int i = 1; i <= friendscount; i++)
            {
                packet = packet + i + ";";
                packet = packet + PlayerStruct.player[s].friend[i].name + ";";
                packet = packet + PlayerStruct.player[s].friend[i].sprite + ";";
                packet = packet + PlayerStruct.player[s].friend[i].sprite_s + ";";
                packet = packet + PlayerStruct.player[s].friend[i].classid + ";";
                packet = packet + PlayerStruct.player[s].friend[i].level + ";";
                packet = packet + PlayerStruct.player[s].friend[i].guildname + ";";
                if (FriendRelations.friendIsOnline(s, i)) { packet = packet + "1" + ";"; } else { packet = packet + "0" + ";"; }
            }

            sendToUser(s, String.Format("<87>{0}</87>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerWarp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa sobre o teleporte do mapa ao jogador.
        //*********************************************************************************************
        public static void sendPlayerWarp(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<19>{0}</19>\n", PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map + ";" + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X + ";" + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y + ";" + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir));
        }
        //*********************************************************************************************
        // sendPlayerDir / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a direção de um jogador.
        //*********************************************************************************************
        public static void sendPlayerDir(int s, int ToMap = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, ToMap) != null)
            {
                return;
            }

            //CÓDIGO
            if (ToMap == 0)
            {
                sendToUser(s, String.Format("<20 {0}>{1}</20>\n", s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir));
            }
            else
            {
                sendToMapBut(s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<20 {0}>{1}</20>\n", s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir));
            }
        }
        //*********************************************************************************************
        // sendActionMsg / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendActionMsg(int s, string msg, int color, int x, int y, int type, int dir, int ToMap = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, msg, color, x, y, type, dir, ToMap) != null)
            {
                return;
            }

            //CÓDIGO
            if (ToMap == 0)
            {
                sendToUser(s, String.Format("<21>{0}</21>\n", msg + ";" + color + ";" + x + ";" + y + ";" + type + ";" + dir));
            }
            else
            {
                sendToMap(ToMap, String.Format("<21>{0}</21>\n", msg + ";" + color + ";" + x + ";" + y + ";" + type + ";" + dir));
            }
        }
        //*********************************************************************************************
        // sendAnimation / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma animação em um determinado alvo e mapa.
        //*********************************************************************************************
        public static void sendAnimation(int map, int targettype, int target, int animid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, targettype, target, animid) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<22 {0};{1}>{2}</22>\n", targettype, target, animid));
        }
        //*********************************************************************************************
        // sendNpcLeft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o npc precisa sumir.
        //*********************************************************************************************
        public static void sendNpcLeft(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<23>{0}</23>\n", id)); 
        }
        //*********************************************************************************************
        // sendNpcVitality / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNpcVitality(int map, int id, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<24 {0}>{1}</24>\n", id, vitality));
        }
        //*********************************************************************************************
        // sendPlayerVitalityToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerVitalityToMap(int map, int s, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, s, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<25 {0}>{1}</25>\n", s, vitality));
        }
        //*********************************************************************************************
        // sendPlayerVitalityToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerVitalityToParty(int partynum, int s, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            sendToParty(partynum, String.Format("<25 {0}>{1}</25>\n", s, vitality));
        }
        //*********************************************************************************************
        // sendPlayerSpiritToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSpiritToMap(int map, int s, int spirit)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, s, spirit) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<30 {0}>{1}</30>\n", s, spirit));
        }
        //*********************************************************************************************
        // sendPlayerSpiritToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSpiritToParty(int partynum, int s, int spirit)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s, spirit) != null)
            {
                return;
            }

            //CÓDIGO
            sendToParty(partynum, String.Format("<30 {0}>{1}</30>\n", s, spirit));
        }
        //*********************************************************************************************
        // sendNpcDir / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a direção de um NPC.
        //*********************************************************************************************
        public static void sendNpcDir(int map, int id, int dir)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<26 {0}>{1}</26>\n", id, dir));
        }
        //*********************************************************************************************
        // sendMapItems / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendMapItems(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int ItemNum;
            int ItemType;
            int Value;
            int X;
            int Y;
            int count = 0;

            string packet = "";

            for (int i = 1; i <= MapStruct.getMapItemMaxs(Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)); i++)
            {
                if (MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].ItemNum > 0)
                {
                    ItemNum = MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].ItemNum;
                    ItemType = MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].ItemType;
                    Value = MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].Value;
                    X = MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].X;
                    Y = MapStruct.mapitem[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].Y;

                    packet = packet + i + ";";
                    packet = packet + ItemNum + ";"; packet = packet + ItemType + ";"; packet = packet + Value + ";";
                    packet = packet + X + ";"; packet = packet + Y + ";";
                    count += 1;
                }
            }

            packet = packet + count;

            sendToUser(s, String.Format("<28>{0}</28>\n", packet));
        }
        //*********************************************************************************************
        // sendMapItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendMapItem(int map, int mapitemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, mapitemnum) != null)
            {
                return;
            }

            //CÓDIGO
            int ItemNum = MapStruct.mapitem[map, mapitemnum].ItemNum;
            int ItemType = MapStruct.mapitem[map, mapitemnum].ItemType;
            int Value = MapStruct.mapitem[map, mapitemnum].Value;
            int X = MapStruct.mapitem[map, mapitemnum].X;
            int Y = MapStruct.mapitem[map, mapitemnum].Y;

            string packet = "";
            packet = packet + mapitemnum + ";";
            packet = packet + ItemNum + ";"; packet = packet + ItemType + ";"; packet = packet + Value + ";";
            packet = packet + X + ";"; packet = packet + Y + ";";
            packet = packet + "1" + ";";

            sendToMap(map, String.Format("<28>{0}</28>\n", packet));
        }
        //*********************************************************************************************
        // sendClearMapItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendClearMapItem(int map, int mapitemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, mapitemnum) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<29>{0}</29>\n", mapitemnum));
        }
        //*********************************************************************************************
        // sendPlayerHotkeys / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza os atalhos de um jogador.
        //*********************************************************************************************
        public static void sendPlayerHotkeys(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            for (int i = 1; i < Globals.MaxHotkeys; i++)
            {
                packet = packet + PlayerStruct.hotkey[s, i].type + "," + PlayerStruct.hotkey[s, i].num + ",";
            }
            sendToUser(s, String.Format("<31>{0}</31>\n", packet));
        }
        //*********************************************************************************************
        // sendplayerAttack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa ao mapa que determinado jogador está atacando.
        //*********************************************************************************************
        public static void sendplayerAttack(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<32>{0}</32>\n", s));
        }
        //*********************************************************************************************
        // sendFrozen / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa que determinado jogador está congelado.
        //*********************************************************************************************
        public static void sendFrozen(int type, int s, int map = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, type, s, map) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (type == Globals.Target_Player)
            {
                if (PlayerStruct.tempplayer[s].isFrozen) { value = 1; }
                sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<84>{0}</84>\n", type + ";" + s + ";" + value));
            }
            else
            {
                if (NpcStruct.tempnpc[map, s].isFrozen) { value = 1; }
                sendToMap(map, String.Format("<84>{0}</84>\n", type + ";" + s + ";" + value));
            }
        }
        //*********************************************************************************************
        // sendMoveSpeed / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a velocidade de movimento de um jogador para o mapa.
        //*********************************************************************************************
        public static void sendMoveSpeed(int type, int s, int map = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, type, s, map) != null)
            {
                return;
            }

            //CÓDIGO
            if (type == 1)
            {
                sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<33>{0}</33>\n", type + ";" + s + ";" + PlayerStruct.tempplayer[s].movespeed));
            }
            else
            {
                sendToMap(map, String.Format("<33>{0}</33>\n", type + ";" + s + ";" + NpcStruct.tempnpc[map, s].movespeed));
            }
        }
        //*********************************************************************************************
        // sendPlayerAtrToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerAtrToMapBut(int s, string username, string charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, username, charId) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
            int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
            int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
            int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
            int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
            int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
            int charPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points);

            string packet = "";
            packet += s + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            sendToMapBut(s, charMap, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // sendMapGuildTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendMapGuildTo(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int MapGuild = MapStruct.map[charMap].guildnum;

            string packet = "";
            packet += GuildStruct.guild[MapGuild].name + ";"; packet += GuildStruct.guild[MapGuild].shield + ";"; packet += GuildStruct.guild[MapGuild].hue + ";";

            sendToUser(s, String.Format("<83>{0}</83>\n", packet));
        }
        //*********************************************************************************************
        // sendMapGuildToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendMapGuildToMap(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return;
            }

            //CÓDIGO
            int MapGuild = MapStruct.map[map].guildnum;

            string packet = "";
            packet += GuildStruct.guild[MapGuild].name + ";"; packet += GuildStruct.guild[MapGuild].shield + ";"; packet += GuildStruct.guild[MapGuild].hue + ";";

            sendToMap(map, String.Format("<83>{0}</83>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerAtrTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerAtrTo(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
            int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
            int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
            int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
            int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
            int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
            int charPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points);

            string packet = "";
            packet += s + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            sendToUser(s, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExtraVitalityTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraVitalityTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PlayerRelations.getExtraVitality(s);

            string packet = "";
            packet += s + ";"; packet += extra_vitality + ";";

            sendToUser(user, String.Format("<79>{0}</79>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExtraVitalityToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraVitalityToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            {
                int map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
                int extra_vitality = PlayerRelations.getExtraVitality(s);

                string packet = "";
                packet += s + ";"; packet += extra_vitality + ";";

                sendToMap(map, String.Format("<79>{0}</79>\n", packet));
            }
        }
        //*********************************************************************************************
        // sendPlayerExtraVitalityToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraVitalityToParty(int partynum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PlayerRelations.getExtraVitality(s);

            string packet = "";
            packet += s + ";"; packet += extra_vitality + ";";

            sendToParty(partynum, String.Format("<79>{0}</79>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExtraSpiritTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraSpiritTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PlayerRelations.getExtraSpirit(s);

            string packet = "";
            packet += s + ";"; packet += extra_vitality + ";";

            sendToUser(user, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExtraSpiritToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraSpiritToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int extra_vitality = PlayerRelations.getExtraSpirit(s);

            string packet = "";
            packet += s + ";"; packet += extra_vitality + ";";

            sendToMap(map, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExtraSpiritToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerExtraSpiritToParty(int partynum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PlayerRelations.getExtraSpirit(s);

            string packet = "";
            packet += s + ";"; packet += extra_vitality + ";";

            sendToParty(partynum, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerAtrToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerAtrToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
            int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
            int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
            int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
            int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
            int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
            int charPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points);

            string packet = "";
            packet += s + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            sendToMap(charMap, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza experiência do jogador.
        //*********************************************************************************************
        public static void sendPlayerExp(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<35>{0}</35>\n", PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp));
        }
        //*********************************************************************************************
        // sendPlayerSkillPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza os pontos de habilidade de um jogador.
        //*********************************************************************************************
        public static void sendPlayerSkillPoints(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<36>{0}</36>\n", PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints));
        }
        //*********************************************************************************************
        // sendPlayerLevel / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o nível de um jogador.
        //*********************************************************************************************
        public static void sendPlayerLevel(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<37 {0}>{1}</37>\n", s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level));
        }
        //*********************************************************************************************
        // sendPartyRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPartyRequest(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<38>{0}</38>\n", s));
        }
        //*********************************************************************************************
        // sendPartyDataTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPartyDataTo(int partynum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PartyRelations.getPartyMembersCount(partynum);
            packet = packet + (partymemberscount) + ",";
            packet = packet + PlayerStruct.party[partynum].leader + ",";
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PlayerStruct.partymembers[partynum, i].s + ",";
            }
            sendToUser(s, String.Format("<39>{0}</39>\n", packet));
        }
        //*********************************************************************************************
        // sendPartyDataToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPartyDataToParty(int partynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PartyRelations.getPartyMembersCount(partynum);
            packet = packet + (partymemberscount) + ",";
            packet = packet + PlayerStruct.party[partynum].leader + ",";
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PlayerStruct.partymembers[partynum, i].s + ",";
            }
            sendToParty(partynum, String.Format("<39>{0}</39>\n", packet));
        }
        //*********************************************************************************************
        // sendPartyKick / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPartyKick(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<40></40>\n"));
        }
        //*********************************************************************************************
        // sendPartyVital / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a vida de todos no grupo.
        //*********************************************************************************************
        public static void sendPartyVital(int partynum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PartyRelations.getPartyMembersCount(partynum);
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PlayerStruct.character[PlayerStruct.partymembers[partynum, i].s, PlayerStruct.player[PlayerStruct.partymembers[partynum, i].s].SelectedChar].Vitality + ",";
                packet = packet + PlayerStruct.character[PlayerStruct.partymembers[partynum, i].s, PlayerStruct.player[PlayerStruct.partymembers[partynum, i].s].SelectedChar].Spirit + ",";
            }

            sendToUser(s, String.Format("<40>{0}</40>\n", packet));
        }
        //*********************************************************************************************
        // sendPartyChange / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPartyChange(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<41>{0}</41>\n", s));
        }
        //*********************************************************************************************
        // sendTradeRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendTradeRequest(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<42>{0}</42>\n", s));
        }
        //*********************************************************************************************
        // sendGuildRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendGuildRequest(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<68>{0}</68>\n", s));
        }
        //*********************************************************************************************
        // sendFriendRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendFriendRequest(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<86>{0}</86>\n", s));
        }
        //*********************************************************************************************
        // sendPetAttack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa que o pet está atacando.
        //*********************************************************************************************
        public static void sendPetAttack(int s, int target, int targettype)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, target, targettype) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            packet = packet + s + ";";
            packet = packet + target + ";";
            packet = packet + targettype + ";";
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<85>{0}</85>\n", packet));
        }
        //*********************************************************************************************
        // sendTradeOffers / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza itens em oferta em uma troca.
        //*********************************************************************************************
        public static void sendTradeOffers(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, user) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int tradeofferscount = TradeRelations.getPlayerTradeOffersCount(s);
            packet = packet + (s) + ";";
            packet = packet + tradeofferscount + ";";
            packet = packet + PlayerStruct.tempplayer[s].TradeG + ";";
            for (int i = 1; i <= tradeofferscount; i++)
            {
                packet = packet + PlayerStruct.tradeslot[s, i].item + ";";
            }
            sendToUser(user, String.Format("<43>{0}</43>\n", packet));
        }
        //*********************************************************************************************
        // sendTradeAccept / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendTradeAccept(int user, int code)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, code) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<44>{0}</44>\n", code));
        }
        //*********************************************************************************************
        // sendTradeRefuse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendTradeRefuse(int user, int code)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, code) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<45>{0}</45>\n", code)); 
        }
        //*********************************************************************************************
        // sendTradeClose / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendTradeClose(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<46></46>\n"));
        }
        //*********************************************************************************************
        // sendPlayerG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualizar ouro do jogador.
        //*********************************************************************************************
        public static void sendPlayerG(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<47>{0}</47>\n", PlayerStruct.character[user, PlayerStruct.player[user].SelectedChar].Gold));
        }
        //*********************************************************************************************
        // sendPlayerC / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Cash :D
        //*********************************************************************************************
        public static void sendPlayerC(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<48>{0}</48>\n", PlayerStruct.character[user, PlayerStruct.player[user].SelectedChar].Cash));
        }
        //*********************************************************************************************
        // sendTradeG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza ouro na troca.
        //*********************************************************************************************
        public static void sendTradeG(int s, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<49 {0}>{1}</49>\n", s, PlayerStruct.tempplayer[s].TradeG));
        }
        //*********************************************************************************************
        // sendAllQuests / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualizar missões para um jogador.
        //*********************************************************************************************
        public static void sendAllQuests(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int questcount = QuestRelations.getPlayerQuestsCount(s);
            packet = packet + questcount + ";";
            for (int g = 1; g < Globals.MaxQuestGivers; g++)
            {
                for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                {
                    if (PlayerStruct.queststatus[s, g, q].status != 0)
                    {
                        packet = packet + g + ";";
                        packet = packet + q + ";";
                        packet = packet + PlayerStruct.queststatus[s, g, q].status + ";";
                        for (int k = 1; k < Globals.MaxQuestKills; k++)
                        {
                            packet = packet + PlayerStruct.questkills[s, g, q, k].kills + ";";
                        }
                        for (int a = 1; a < Globals.MaxQuestActions; a++)
                        {
                            string actioninfo = "0";

                            if (PlayerStruct.questactions[s, g, q, a].actiondone == true) { actioninfo = "1"; }
                            if (PlayerStruct.questactions[s, g, q, a].actiondone == false) { actioninfo = "0"; }

                            packet = packet + actioninfo + ";";
                        }
                    }
                }
            }
            sendToUser(s, String.Format("<50>{0}</50>\n", packet));
        }
        //*********************************************************************************************
        // sendQuestKill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendQuestKill(int s, int g, int q, int k)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, g, q, k) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<51>{0}</51>\n", g + ";" + q + ";" + k + ";" + PlayerStruct.questkills[s, g, q, k].kills));
        }
        //*********************************************************************************************
        // sendQuestAction / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendQuestAction(int s, int g, int q, int a)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, q, q, a) != null)
            {
                return;
            }

            //CÓDIGO
            string actioninfo = "0";

            if (PlayerStruct.questactions[s, g, q, a].actiondone == true) { actioninfo = "1"; }
            if (PlayerStruct.questactions[s, g, q, a].actiondone == false) { actioninfo = "0"; }

            sendToUser(s, String.Format("<62>{0}</62>\n", g + ";" + q + ";" + a + ";" + actioninfo));
        }
        //*********************************************************************************************
        // sendKnockBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia um empurrão para o jogador ou npc.
        //*********************************************************************************************
        public static void sendKnockBack(int map, int type, int s, int dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, s, dir, range) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<52>{0}</52>\n", type + ";" + s + ";" + dir + ";" + range));
        }
        //*********************************************************************************************
        // sendSleep / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendSleep(int map, int type, int s, int is_sleeping)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, s, is_sleeping) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<53>{0}</53>\n", type + ";" + s + ";" + is_sleeping));

        }
        //*********************************************************************************************
        // sendStun / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendStun(int map, int type, int s, int is_stunned)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, s, is_stunned) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<54>{0}</54>\n", type + ";" + s + ";" + is_stunned));
        }
        //*********************************************************************************************
        // sendSleepToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendSleepToUser(int user, int type, int s, int is_sleeping)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, type, s, is_sleeping) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(user, String.Format("<53>{0}</53>\n", type + ";" + s + ";" + is_sleeping));
        }
        //*********************************************************************************************
        // sendStunToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendStunToUser(int user, int type, int s, int is_stunned)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, type, s, is_stunned) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(user, String.Format("<54>{0}</54>\n", type + ";" + s + ";" + is_stunned));
        }
        //*********************************************************************************************
        // sendShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendShop(int s, int shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, shopnum) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int item_count = ShopStruct.shop[shopnum].item_count;
            packet = packet + item_count + ";";
            for (int slot = 1; s <= item_count; s++)
            {
                packet = packet + s + ";";
                packet = packet + ShopStruct.shopitem[shopnum, slot].type + ";";
                packet = packet + ShopStruct.shopitem[shopnum, slot].num + ";";
                packet = packet + ShopStruct.shopitem[shopnum, slot].value + ";";
                packet = packet + ShopStruct.shopitem[shopnum, slot].refin + ";";
                packet = packet + ShopStruct.shopitem[shopnum, slot].price + ";";
            }
            sendToUser(s, String.Format("<55>{0}</55>\n", packet));
        }
        //*********************************************************************************************
        // sendCraft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCraft(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            for (int c = 1; c < Globals.Max_Craft; c++)
            {
                packet = packet + c + ";";
                packet = packet + PlayerStruct.craft[s, c].type + ";";
                packet = packet + PlayerStruct.craft[s, c].num + ";";
                packet = packet + PlayerStruct.craft[s, c].value + ";";
                packet = packet + PlayerStruct.craft[s, c].refin + ";";
            }
            sendToUser(s, String.Format("<56>{0}</56>\n", packet));
        }
        //*********************************************************************************************
        // sendProfs / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendProfs(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[1] = 2;
            //PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[1] = 3;
            //PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[1] = 2;
            string packet = "";
            int prof_count = Globals.Max_Profs_Per_Char - 1;
            packet = packet + prof_count + ";";
            for (int j = 1; j < Globals.Max_Profs_Per_Char; j++)
            {
                packet = packet + j + ";";
                packet = packet + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[j] + ";";
                packet = packet + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[j] + ";";
                packet = packet + PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[j] + ";";
            }
            sendToUser(s, String.Format("<60>{0}</60>\n", packet));
        }
        //*********************************************************************************************
        // sendProfExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendProfEXP(int s, int profnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, profnum) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<57>{0};{1}</57>\n", profnum, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum]));
        }
        //*********************************************************************************************
        // sendProfLEVEL / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendProfLEVEL(int s, int profnum)
        {            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, profnum) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<58>{0};{1}</58>\n", profnum, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum]));
        }
        //*********************************************************************************************
        // sendEventGraphic / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um evento no jogo.
        //*********************************************************************************************
        public static void sendEventGraphic(int s, int event_id, string graphic, int graphic_s, byte is_tile = 0, byte dir = 2)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, event_id, graphic, graphic_s, is_tile, dir) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<59>{0}</59>\n", event_id + ";" + graphic + ";" + graphic_s + ";" + is_tile + ";" + dir));
        }
        //*********************************************************************************************
        // sendEventGraphicToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um evento no jogo.
        //*********************************************************************************************
        public static void sendEventGraphicToMap(int map, int event_id, string graphic, int graphic_s, byte is_tile = 0, byte dir = 2)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, event_id, graphic, graphic_s, is_tile, dir) != null)
            {
                return;
            }

            //CÓDIGO
            sendToMap(map, String.Format("<59>{0}</59>\n", event_id + ";" + graphic + ";" + graphic_s + ";" + is_tile + ";" + dir));
        }
        //*********************************************************************************************
        // sendBankSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendBankSlots(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int bank_count = Globals.Max_BankSlots - 1;
            packet = packet + bank_count + ";";
            for (int b = 1; b < Globals.Max_BankSlots; b++)
            {
                packet = packet + b + ";";
                packet = packet + PlayerStruct.player[s].bankslot[b].type + ";";
                packet = packet + PlayerStruct.player[s].bankslot[b].num + ";";
                packet = packet + PlayerStruct.player[s].bankslot[b].value + ";";
                packet = packet + PlayerStruct.player[s].bankslot[b].refin + ";";
                packet = packet + PlayerStruct.player[s].bankslot[b].exp + ";";
            }
            sendToUser(s, String.Format("<61>{0}</61>\n", packet));
        }
        //*********************************************************************************************
        // sendPShopSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPShopSlots(int shops, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, shops, s) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int pshop_count = Globals.Max_PShops - 1;
            packet = packet + shops + ";";
            packet = packet + pshop_count + ";";
            for (int b = 1; b < Globals.Max_PShops; b++)
            {
                packet = packet + b + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].type + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].num + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].value + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].refin + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].exp + ";";
                packet = packet + PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[b].price + ";";
            }
            sendToUser(s, String.Format("<81>{0}</81>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerShoppingTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerShoppingTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (PlayerStruct.tempplayer[s].Shopping) { value = 1; }

            sendToUser(user, String.Format("<82>{0};{1}</82>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerShoppingToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerShoppingToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (PlayerStruct.tempplayer[s].Shopping) { value = 1; }

            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<82>{0};{1}</82>\n", s, value));

        }
        //*********************************************************************************************
        // sendPremmy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPremmy(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int result = 0;

            if (PlayerRelations.isPlayerPremmy(s)) { result = 1; }

            sendToUser(s, String.Format("<63>{0};{1}</63>\n", result, PlayerStruct.player[s].Premmy));
        }
        //*********************************************************************************************
        // sendWPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendWPoints(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<64>{0}</64>\n", PlayerStruct.player[s].WPoints));
        }
        //*********************************************************************************************
        // sendRecipe / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendRecipe(int s, int recipetype, int recipenum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, recipetype, recipenum) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int recipe_count = Globals.Max_Craft - 1;
            packet = packet + recipe_count + ";";

            for (int r = 1; r < Globals.Max_Craft; r++)
            {
                packet = packet + r + ";";
                packet = packet + MapStruct.craftrecipe[recipetype, recipenum, r].type + ";";
                packet = packet + MapStruct.craftrecipe[recipetype, recipenum, r].num + ";";
                packet = packet + MapStruct.craftrecipe[recipetype, recipenum, r].value + ";";
                packet = packet + MapStruct.craftrecipe[recipetype, recipenum, r].refin + ";";
            }
            sendToUser(s, String.Format("<65>{0}</65>\n", packet));
        }
        //*********************************************************************************************
        // sendGuildTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendGuildTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) { return; }
            sendToUser(user, String.Format("<66>{0}</66>\n", s + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].name + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].shield + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // sendGuildToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendGuildToMap(int mapnum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) { return; }
            sendToMap(mapnum, String.Format("<66>{0}</66>\n", s + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].name + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].shield + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // sendGuildToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendGuildToMapBut(int mapnum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) { return; }
            sendToMapBut(s, mapnum, String.Format("<66>{0}</66>\n", s + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].name + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].shield + ";" + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // sendCompleteGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCompleteGuild(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO  
            // PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = 1;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) { return; }

            string packet = "";
            int member_count = GuildStruct.getMember_Count(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].name + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].level + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].exp + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].shield + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].hue + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].memberlist[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].membersprite[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].memberhue[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].membersprite_s[i] + ";";
            }

            sendToUser(s, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // sendCompleteClearGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCompleteClearGuild(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            // PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = 1;

            string packet = "";
            int member_count = 0;

            packet = packet + member_count + ";";
            packet = packet + "" + ";";
            packet = packet + 0 + ";";
            packet = packet + 0 + ";";
            packet = packet + 0 + ";";
            packet = packet + 0 + ";";
            packet = packet + 0 + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + "" + ";";
                packet = packet + "" + ";";
                packet = packet + 0 + ";";
                packet = packet + 0 + ";";
            }

            sendToUser(s, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // sendCompleteGuildToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCompleteGuildToGuild(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) { return; }

            string packet = "";
            int member_count = GuildStruct.getMember_Count(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].name + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].level + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].exp + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].shield + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].hue + ";";
            packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].memberlist[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].membersprite[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].memberhue[i] + ";";
                packet = packet + GuildStruct.guild[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild].membersprite_s[i] + ";";
            }

            sendToGuild(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // sendCompleteGuildToGuildG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendCompleteGuildToGuildG(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return;
            }

            //CÓDIGO
            if (guildnum <= 0) { return; }

            string packet = "";
            int member_count = GuildStruct.getMember_Count(guildnum);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GuildStruct.guild[guildnum].name + ";";
            packet = packet + GuildStruct.guild[guildnum].level + ";";
            packet = packet + GuildStruct.guild[guildnum].exp + ";";
            packet = packet + GuildStruct.guild[guildnum].shield + ";";
            packet = packet + GuildStruct.guild[guildnum].hue + ";";
            packet = packet + GuildStruct.guild[guildnum].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GuildStruct.guild[guildnum].memberlist[i] + ";";
                packet = packet + GuildStruct.guild[guildnum].membersprite[i] + ";";
                packet = packet + GuildStruct.guild[guildnum].memberhue[i] + ";";
                packet = packet + GuildStruct.guild[guildnum].membersprite_s[i] + ";";
            }

            sendToGuild(guildnum, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // sendPlayerDeathToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerDeathToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;

            if (PlayerStruct.tempplayer[s].isDead) { value = 1; }

            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<69>{0};{1}</69>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerDeathTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerDeathTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;

            if (PlayerStruct.tempplayer[s].isDead) { value = 1; }

            sendToUser(user, String.Format("<69>{0};{1}</69>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerPvpToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerPvpToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP) { value = 1; } else { value = 0; }
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<70>{0};{1}</70>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerPvpTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerPvpTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP) { value = 1; } else { value = 0; }
            sendToUser(user, String.Format("<70>{0};{1}</70>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerPvpChangeTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerPvpChangeTimer(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            sendToUser(s, String.Format("<71>{0}</71>\n", result));
        }
        //*********************************************************************************************
        // sendPlayerPvpBanTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerPvpBanTimer(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            sendToUser(s, String.Format("<72>{0}</72>\n", result));
        }
        //*********************************************************************************************
        // sendPlayerSoreToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSoreToMap(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PlayerRelations.playerIsSore(s)) { value = 1; } else { value = 0; }
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<73>{0};{1}</73>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerSoreTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSoreTo(int user, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, s) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PlayerRelations.playerIsSore(s)) { value = 1; } else { value = 0; }
            sendToUser(user, String.Format("<73>{0};{1}</73>\n", s, value));
        }
        //*********************************************************************************************
        // sendPlayerPvpSoreTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerPvpSoreTimer(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            sendToUser(s, String.Format("<74>{0}</74>\n", result));
        }
        //*********************************************************************************************
        // sendPlayerSkillCoolDown / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendPlayerSkillCooldown(int s, int slot, int cooldown)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, slot, cooldown) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<75>{0};{1}</75>\n", slot, cooldown));
        }
        //*********************************************************************************************
        // sendNStatus / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNStatus(int s, string msg)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, msg) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<76>{0}</76>\n", msg));
        }
        //*********************************************************************************************
        // sendNotice / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendNotice(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<77>{0}</77>\n", Globals.NOTICE));
        }
        //*********************************************************************************************
        // sendBrokeSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void sendBrokeSkill(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            sendToUser(s, String.Format("<78></78>\n"));
        }
        //*********************************************************************************************
        // sendPlayerSprite / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um jogador.
        //*********************************************************************************************
        public static void sendPlayerSprite(int s, string sprite_name, int sprite_s)
        {
            string packet = s + ";" + sprite_name + ";" + sprite_s;
            sendToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, String.Format("<91>{0}</91>\n", packet));
        }
    }
}
