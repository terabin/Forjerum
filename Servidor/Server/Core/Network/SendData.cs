using System;
using System.Text;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Métodos e tratamentos de todas as informações que serão enviadas pelo servidor.
    // SendData.cs
    //*********************************************************************************************
    class SendData
    {
        //*********************************************************************************************
        // SendToAll / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todas as conexões.
        //*********************************************************************************************
        public static void SendToAll(string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                WinsockAsync.Send(i, packet);
            }
        }
        //*********************************************************************************************
        // SendToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todos os jogadores em determinado mapa.
        //*********************************************************************************************
        public static void SendToMap(int map, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == map) && (PStruct.tempplayer[i].ingame == true))
                {
                    SendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // SendToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para todos os membros de determinada guilda.
        //*********************************************************************************************
        public static void SendToGuild(int guild, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guild, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if ((PStruct.character[i, PStruct.player[i].SelectedChar].Guild == guild) && (PStruct.tempplayer[i].ingame == true))
                {
                    SendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // SendToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para membros de determinado grupo.
        //*********************************************************************************************
        public static void SendToParty(int partynum, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if ((PStruct.tempplayer[i].Party == partynum) && (PStruct.tempplayer[i].ingame == true))
                {
                    SendToUser(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // SendToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet para jogadores em um determinado mapa com exceção de um determinado
        // jogador.
        //*********************************************************************************************
        public static void SendToMapBut(int index, int map, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, map, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if (i != index)
                {
                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == map)  && (PStruct.tempplayer[i].ingame == true))
                    {
                        SendToUser(i, packet);
                    }
                }
            }
        }
        //*********************************************************************************************
        // SendToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada packet apenas para um determinado usuário.
        //*********************************************************************************************
        public static void SendToUser(int clientid, string packet)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, clientid, packet) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 0; i < WinsockAsync.Clients.Count; i++)
            {
                if (WinsockAsync.Clients[i].index == clientid)
                {
                    WinsockAsync.Send(i, packet);
                }
            }
        }
        //*********************************************************************************************
        // Send_PlayerDataToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de um jogador para todos em determinado mapa com exceção de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerDataToMapBut(int index, string username, int charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, username, charId) != null)
            {
                return;
            }


            //CÓDIGO
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
            int charPoints = PStruct.character[index, PStruct.player[index].SelectedChar].Points;
            int charMap = (PStruct.character[index, PStruct.player[index].SelectedChar].Map);
            byte charX = (PStruct.character[index, PStruct.player[index].SelectedChar].X);
            byte charY = (PStruct.character[index, PStruct.player[index].SelectedChar].Y);
            byte charDir = (PStruct.character[index, PStruct.player[index].SelectedChar].Dir);
            int charVitality = PStruct.tempplayer[index].Vitality;
            int charSpirit = PStruct.tempplayer[index].Spirit;
            int charAccess = (PStruct.character[index, PStruct.player[index].SelectedChar].Access);
            int SkillPoints = PStruct.character[index, PStruct.player[index].SelectedChar].SkillPoints;
            int charHue = PStruct.character[index, PStruct.player[index].SelectedChar].Hue;
            int charGender = PStruct.character[index, PStruct.player[index].SelectedChar].Gender;
            string Equipment = PStruct.character[index, PStruct.player[index].SelectedChar].Equipment;
            int extra_vitality = PStruct.GetExtraVitality(index);
            int extra_spirit = PStruct.GetExtraVitality(index);

            string packet = "";
            packet = packet + index + ";";
            packet = packet + charName + ";"; packet = packet + charSpriteindex + ";"; packet = packet + charClass + ";"; packet = packet + charSprite + ";"; packet = packet + charLevel + ";";
            packet = packet + charExp + ";"; packet = packet + charFire + ";"; packet = packet + charEarth + ";"; packet = packet + charWater + ";"; packet = packet + charWind + ";";
            packet = packet + charDark + ";"; packet = packet + charLight + ";"; packet = packet + charPoints + ";"; packet = packet + charMap + ";"; packet = packet + charX + ";";
            packet = packet + charY + ";"; packet = packet + charDir + ";"; packet = packet + charVitality + ";"; packet = packet + charSpirit + ";"; packet = packet + charAccess + ";";
            packet = packet + charHue + ";"; packet = packet + charGender + ";"; packet = packet + SkillPoints + ";"; packet = packet + Equipment + ";"; packet = packet + extra_vitality + ";";
            packet = packet + extra_spirit + ";";

            SendToMapBut(index, charMap, String.Format("<8>{0}></8>\n", packet));

        }
        //*********************************************************************************************
        // Send_PlayerDataTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de um determinado jogador para outro determinado jogador.
        //*********************************************************************************************
        public static void Send_PlayerDataTo(int Receiver, int index, string username, int charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, Receiver, index, username, charId) != null)
            {
                return;
            }

            //CÓDIGO
            string charName = PStruct.character[index, PStruct.player[index].SelectedChar].CharacterName;
            int charSpriteindex = PStruct.character[index, PStruct.player[index].SelectedChar].Spriteindex;
            int charClass = PStruct.character[index, PStruct.player[index].SelectedChar].ClassId;
            string charSprite = PStruct.character[index, PStruct.player[index].SelectedChar].Sprite;
            int charLevel = PStruct.character[index, PStruct.player[index].SelectedChar].Level;
            int charExp = PStruct.character[index, PStruct.player[index].SelectedChar].Exp;
            int charFire = PStruct.character[index, PStruct.player[index].SelectedChar].Fire;
            int charEarth = PStruct.character[index, PStruct.player[index].SelectedChar].Earth;
            int charWater = PStruct.character[index, PStruct.player[index].SelectedChar].Water;
            int charWind = PStruct.character[index, PStruct.player[index].SelectedChar].Wind;
            int charDark = PStruct.character[index, PStruct.player[index].SelectedChar].Dark;
            int charLight = PStruct.character[index, PStruct.player[index].SelectedChar].Light;
            int charPoints = PStruct.character[index, PStruct.player[index].SelectedChar].Points;
            int charMap = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            byte charX = PStruct.character[index, PStruct.player[index].SelectedChar].X;
            byte charY = PStruct.character[index, PStruct.player[index].SelectedChar].Y;
            byte charDir = PStruct.character[index, PStruct.player[index].SelectedChar].Dir;
            int charVitality = PStruct.tempplayer[index].Vitality;
            int charSpirit = PStruct.tempplayer[index].Spirit;
            int charAccess = PStruct.character[index, PStruct.player[index].SelectedChar].Access;
            int charHue = PStruct.character[index, PStruct.player[index].SelectedChar].Hue;
            int charGender = PStruct.character[index, PStruct.player[index].SelectedChar].Gender;
            int SkillPoints = PStruct.character[index, PStruct.player[index].SelectedChar].SkillPoints;
            string Equipment = PStruct.character[index, PStruct.player[index].SelectedChar].Equipment;

            string packet = "";
            packet = packet + index + ";";
            packet = packet + charName + ";"; packet = packet + charSpriteindex + ";"; packet = packet + charClass + ";"; packet = packet + charSprite + ";"; packet = packet + charLevel + ";";
            packet = packet + charExp + ";"; packet = packet + charFire + ";"; packet = packet + charEarth + ";"; packet = packet + charWater + ";"; packet = packet + charWind + ";";
            packet = packet + charDark + ";"; packet = packet + charLight + ";"; packet = packet + charPoints + ";"; packet = packet + charMap + ";"; packet = packet + charX + ";";
            packet = packet + charY + ";"; packet = packet + charDir + ";"; packet = packet + charVitality + ";"; packet = packet + charSpirit + ";"; packet = packet + charAccess + ";";
            packet = packet + charHue + ";"; packet = packet + charGender + ";"; packet = packet + SkillPoints + ";"; packet = packet + Equipment + ";";

            SendToUser(Receiver, String.Format("<8>{0}</8>\n", packet));

        }
        //*********************************************************************************************
        // Send_UpdatePlayerHighIndex / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_UpdatePlayerHighindex()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            SendToAll(String.Format("<9>{0}</9>\n", Globals.Player_Highindex));
        }
        //*********************************************************************************************
        // Send_PlayerXY / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a posição de um jogador para um mapa.
        //*********************************************************************************************
        public static void Send_PlayerXY(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int charX = (PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].X);
            int charY = (PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Y);
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<10>{0}</10>\n", index + ";" + charX + ";" + charY));
        }
        //*********************************************************************************************
        // Send_MsgToAll / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat de todos.
        //*********************************************************************************************
        public static void Send_MsgToAll(string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            SendToAll(String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // Send_MsgToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat para jogadores em um determinado mapa.
        //*********************************************************************************************
        public static void Send_MsgToMap(int index, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // Send_MsgToPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat para um determinado jogador.
        //*********************************************************************************************
        public static void Send_MsgToPlayer(int index, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<11 {0};{1}>{2}</11>\n", msg, color, type));
        }
        //*********************************************************************************************
        // Send_MsgToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma mensagem no chat de um determinado grupo.
        //*********************************************************************************************
        public static void Send_MsgToParty(int party, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, party, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            SendToParty(party, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // Send_MsgToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia determinada mensagem no chat para uma guilda determinada.
        //*********************************************************************************************
        public static void Send_MsgToGuild(int guild, string msg, int color, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guild, msg, color, type) != null)
            {
                return;
            }

            //CÓDIGO
            byte[] databyte = Encoding.Unicode.GetBytes(msg);
            string output = Encoding.Unicode.GetString(databyte);

            SendToGuild(guild, String.Format("<11 {0};{1}>{2}</11>\n", output, color, type));
        }
        //*********************************************************************************************
        // Send_InvSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o inventário de um determinado jogador.
        //*********************************************************************************************
        public static void Send_InvSlots(int index, int characterslot)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, characterslot) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            packet = packet + (Globals.MaxInvSlot - 1) + ";";

            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                packet = packet + i + ";";
                packet = packet + index + ";";
                packet = packet + PStruct.invslot[index, i].item + ";";
            }

            SendToUser(index, String.Format("<12>{0}</12>\n", packet));

        }
        //*********************************************************************************************
        // Send_NpcTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_NpcTo(int index, int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            string name = NStruct.npc[map, id].Name;
            string sprite = NStruct.npc[map, id].Sprite;
            int npcindex = NStruct.npc[map, id].index;
            int x = NStruct.tempnpc[map, id].X;
            int y = NStruct.tempnpc[map, id].Y;
            int vitality = NStruct.npc[map, id].Vitality;
            int dir = NStruct.tempnpc[map, id].Dir;

            SendToUser(index, String.Format("<14 {0};{1};{2};{3};{4};{5};{6}>{7}</14>\n", id, name, sprite, npcindex, x, y, vitality, dir));
        }
        //*********************************************************************************************
        // Send_NpcToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_NpcToMap(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            if (NStruct.tempnpc[map, id].Dead == true) { return; }

            string packet = "";
            packet += id + ";";
            packet += NStruct.npc[map, id].Name + ";";
            packet += NStruct.npc[map, id].Sprite + ";";
            packet += NStruct.npc[map, id].index + ";";
            packet += NStruct.tempnpc[map, id].X + ";";
            packet += NStruct.tempnpc[map, id].Y + ";";
            packet += NStruct.npc[map, id].Vitality + ";";
            packet += NStruct.tempnpc[map, id].Dir + ";";

            packet = "1" + ";" + packet;

            SendToMap(map, String.Format("<14>{0}</14>\n", packet));
        }
        //*********************************************************************************************
        // Send_MapNpcsTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_MapNpcsTo(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int map = Convert.ToInt32(PStruct.character[index, PStruct.player[index].SelectedChar].Map);

            if (MStruct.tempmap[map].NpcCount == 0) { return; }

            string packet = "";
            int count = 0;

            for (int i = 1; i <= MStruct.tempmap[map].NpcCount; i++)
            {
                if (!NStruct.tempnpc[map, i].Dead)
                {
                    packet += i + ";";
                    packet += NStruct.npc[map, i].Name + ";";
                    packet += NStruct.npc[map, i].Sprite + ";";
                    packet += NStruct.npc[map, i].index + ";";
                    packet += NStruct.tempnpc[map, i].X + ";";
                    packet += NStruct.tempnpc[map, i].Y + ";";
                    packet += NStruct.npc[map, i].Vitality + ";";
                    packet += NStruct.tempnpc[map, i].Dir + ";";
                    count += 1;
                }
            }

            packet = count + ";" + packet;

            SendToMap(map, String.Format("<14>{0}</14>\n", packet));
        }
        //*********************************************************************************************
        // Send_NpcMove / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_NpcMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            packet += id + ";"; packet += NStruct.tempnpc[map, id].X + ";"; packet += NStruct.tempnpc[map, id].Y + ";";
            SendToMap(map, String.Format("<15>{0}</15>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerLeft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa ao mapa que determinado jogador saiu.
        //*********************************************************************************************
        public static void Send_PlayerLeft(int map, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMapBut(index, map, String.Format("<16>{0}</16>\n", index));
        }
        //*********************************************************************************************
        // Send_PlayerEquipmentToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações dos itens equipados de um determinado jogador ao mapa.
        // O visual style está praticamente feito, apenas falta o client side. ;)
        //*********************************************************************************************
        public static void Send_PlayerEquipmentToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<17 {0}>{1}</17>\n", index, PStruct.character[index, PStruct.player[index].SelectedChar].Equipment));

        }
        //*********************************************************************************************
        // Send_PlayerEquipmentTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia informações de itens equipados de um jogador para outro.
        //*********************************************************************************************
        public static void Send_PlayerEquipmentTo(int index, int playerindex)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, playerindex) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<17 {0}>{1}</17>\n", playerindex, PStruct.character[playerindex, PStruct.player[playerindex].SelectedChar].Equipment));

        }
        //*********************************************************************************************
        // Send_PlayerSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSkills(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            packet = packet + (Globals.MaxPlayer_Skills - 1) + ";";

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                packet = packet + i + ";";
                packet = packet + PStruct.skill[index, i].num + ";";
                packet = packet + PStruct.skill[index, i].level + ";";
            }

            SendToUser(index, String.Format("<18>{0}</18>\n", packet));

        }
        //*********************************************************************************************
        // Send_PlayerFriends / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a lista de amigos.
        //*********************************************************************************************
        public static void Send_PlayerFriends(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";

            int friendscount = PStruct.GetPlayerFriendsCount(index);

            packet = packet + friendscount + ";";

            //PStruct.AddFriend(index, index);

            for (int i = 1; i <= friendscount; i++)
            {
                packet = packet + i + ";";
                packet = packet + PStruct.player[index].friend[i].name + ";";
                packet = packet + PStruct.player[index].friend[i].sprite + ";";
                packet = packet + PStruct.player[index].friend[i].sprite_index + ";";
                packet = packet + PStruct.player[index].friend[i].classid + ";";
                packet = packet + PStruct.player[index].friend[i].level + ";";
                packet = packet + PStruct.player[index].friend[i].guildname + ";";
                if (PStruct.FriendIsOnline(index, i)) { packet = packet + "1" + ";"; } else { packet = packet + "0" + ";"; }
            }

            SendToUser(index, String.Format("<87>{0}</87>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerWarp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa sobre o teleporte do mapa ao jogador.
        //*********************************************************************************************
        public static void Send_PlayerWarp(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<19>{0}</19>\n", PStruct.character[index, PStruct.player[index].SelectedChar].Map + ";" + PStruct.character[index, PStruct.player[index].SelectedChar].X + ";" + PStruct.character[index, PStruct.player[index].SelectedChar].Y + ";" + PStruct.character[index, PStruct.player[index].SelectedChar].Dir));
        }
        //*********************************************************************************************
        // Send_PlayerDir / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a direção de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerDir(int index, int ToMap = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, ToMap) != null)
            {
                return;
            }

            //CÓDIGO
            if (ToMap == 0)
            {
                SendToUser(index, String.Format("<20 {0}>{1}</20>\n", index, PStruct.character[index, PStruct.player[index].SelectedChar].Dir));
            }
            else
            {
                SendToMapBut(index, PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<20 {0}>{1}</20>\n", index, PStruct.character[index, PStruct.player[index].SelectedChar].Dir));
            }
        }
        //*********************************************************************************************
        // Send_ActionMsg / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_ActionMsg(int index, string msg, int color, int x, int y, int type, int dir, int ToMap = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, msg, color, x, y, type, dir, ToMap) != null)
            {
                return;
            }

            //CÓDIGO
            if (ToMap == 0)
            {
                SendToUser(index, String.Format("<21>{0}</21>\n", msg + ";" + color + ";" + x + ";" + y + ";" + type + ";" + dir));
            }
            else
            {
                SendToMap(ToMap, String.Format("<21>{0}</21>\n", msg + ";" + color + ";" + x + ";" + y + ";" + type + ";" + dir));
            }
        }
        //*********************************************************************************************
        // Send_Animation / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia uma animação em um determinado alvo e mapa.
        //*********************************************************************************************
        public static void Send_Animation(int map, int targettype, int target, int animid)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, targettype, target, animid) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<22 {0};{1}>{2}</22>\n", targettype, target, animid));
        }
        //*********************************************************************************************
        // Send_NpcLeft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o npc precisa sumir.
        //*********************************************************************************************
        public static void Send_NpcLeft(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<23>{0}</23>\n", id)); 
        }
        //*********************************************************************************************
        // Send_NpcVitality / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_NpcVitality(int map, int id, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<24 {0}>{1}</24>\n", id, vitality));
        }
        //*********************************************************************************************
        // Send_PlayerVitalityToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerVitalityToMap(int map, int index, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, index, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<25 {0}>{1}</25>\n", index, vitality));
        }
        //*********************************************************************************************
        // Send_PlayerVitalityToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerVitalityToParty(int partynum, int index, int vitality)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index, vitality) != null)
            {
                return;
            }

            //CÓDIGO
            SendToParty(partynum, String.Format("<25 {0}>{1}</25>\n", index, vitality));
        }
        //*********************************************************************************************
        // Send_PlayerSpiritToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSpiritToMap(int map, int index, int spirit)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, index, spirit) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<30 {0}>{1}</30>\n", index, spirit));
        }
        //*********************************************************************************************
        // Send_PlayerSpiritToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSpiritToParty(int partynum, int index, int spirit)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index, spirit) != null)
            {
                return;
            }

            //CÓDIGO
            SendToParty(partynum, String.Format("<30 {0}>{1}</30>\n", index, spirit));
        }
        //*********************************************************************************************
        // Send_NpcDir / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a direção de um NPC.
        //*********************************************************************************************
        public static void Send_NpcDir(int map, int id, int dir)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<26 {0}>{1}</26>\n", id, dir));
        }
        //*********************************************************************************************
        // Send_MapItems / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_MapItems(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
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

            for (int i = 1; i <= MStruct.GetMapItemMaxindex(Convert.ToInt32(PStruct.character[index, PStruct.player[index].SelectedChar].Map)); i++)
            {
                if (MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].ItemNum > 0)
                {
                    ItemNum = MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].ItemNum;
                    ItemType = MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].ItemType;
                    Value = MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].Value;
                    X = MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].X;
                    Y = MStruct.mapitem[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].Y;

                    packet = packet + i + ";";
                    packet = packet + ItemNum + ";"; packet = packet + ItemType + ";"; packet = packet + Value + ";";
                    packet = packet + X + ";"; packet = packet + Y + ";";
                    count += 1;
                }
            }

            packet = packet + count;

            SendToUser(index, String.Format("<28>{0}</28>\n", packet));
        }
        //*********************************************************************************************
        // Send_MapItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_MapItem(int map, int mapitemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, mapitemnum) != null)
            {
                return;
            }

            //CÓDIGO
            int ItemNum = MStruct.mapitem[map, mapitemnum].ItemNum;
            int ItemType = MStruct.mapitem[map, mapitemnum].ItemType;
            int Value = MStruct.mapitem[map, mapitemnum].Value;
            int X = MStruct.mapitem[map, mapitemnum].X;
            int Y = MStruct.mapitem[map, mapitemnum].Y;

            string packet = "";
            packet = packet + mapitemnum + ";";
            packet = packet + ItemNum + ";"; packet = packet + ItemType + ";"; packet = packet + Value + ";";
            packet = packet + X + ";"; packet = packet + Y + ";";
            packet = packet + "1" + ";";

            SendToMap(map, String.Format("<28>{0}</28>\n", packet));
        }
        //*********************************************************************************************
        // Send_ClearMapItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_ClearMapItem(int map, int mapitemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, mapitemnum) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<29>{0}</29>\n", mapitemnum));
        }
        //*********************************************************************************************
        // Send_PlayerHotkeys / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza os atalhos de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerHotkeys(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            for (int i = 1; i < Globals.MaxHotkeys; i++)
            {
                packet = packet + PStruct.hotkey[index, i].type + "," + PStruct.hotkey[index, i].num + ",";
            }
            SendToUser(index, String.Format("<31>{0}</31>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerAttack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa ao mapa que determinado jogador está atacando.
        //*********************************************************************************************
        public static void Send_PlayerAttack(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<32>{0}</32>\n", index));
        }
        //*********************************************************************************************
        // SendFrozen / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa que determinado jogador está congelado.
        //*********************************************************************************************
        public static void Send_Frozen(int type, int index, int map = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, type, index, map) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (type == Globals.Target_Player)
            {
                if (PStruct.tempplayer[index].isFrozen) { value = 1; }
                SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<84>{0}</84>\n", type + ";" + index + ";" + value));
            }
            else
            {
                if (NStruct.tempnpc[map, index].isFrozen) { value = 1; }
                SendToMap(map, String.Format("<84>{0}</84>\n", type + ";" + index + ";" + value));
            }
        }
        //*********************************************************************************************
        // Send_MoveSpeed / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a velocidade de movimento de um jogador para o mapa.
        //*********************************************************************************************
        public static void Send_MoveSpeed(int type, int index, int map = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, type, index, map) != null)
            {
                return;
            }

            //CÓDIGO
            if (type == 1)
            {
                SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<33>{0}</33>\n", type + ";" + index + ";" + PStruct.tempplayer[index].movespeed));
            }
            else
            {
                SendToMap(map, String.Format("<33>{0}</33>\n", type + ";" + index + ";" + NStruct.tempnpc[map, index].movespeed));
            }
        }
        //*********************************************************************************************
        // Send_PlayerAtrToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerAtrToMapBut(int index, string username, string charId)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, username, charId) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int charFire = (PStruct.character[index, PStruct.player[index].SelectedChar].Fire);
            int charEarth = (PStruct.character[index, PStruct.player[index].SelectedChar].Earth);
            int charWater = (PStruct.character[index, PStruct.player[index].SelectedChar].Water);
            int charWind = (PStruct.character[index, PStruct.player[index].SelectedChar].Wind);
            int charDark = (PStruct.character[index, PStruct.player[index].SelectedChar].Dark);
            int charLight = (PStruct.character[index, PStruct.player[index].SelectedChar].Light);
            int charPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].Points);

            string packet = "";
            packet += index + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            SendToMapBut(index, charMap, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // Send_MapGuildTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_MapGuildTo(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int MapGuild = MStruct.map[charMap].guildnum;

            string packet = "";
            packet += GStruct.guild[MapGuild].name + ";"; packet += GStruct.guild[MapGuild].shield + ";"; packet += GStruct.guild[MapGuild].hue + ";";

            SendToUser(index, String.Format("<83>{0}</83>\n", packet));
        }
        //*********************************************************************************************
        // Send_MapGuildToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_MapGuildToMap(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return;
            }

            //CÓDIGO
            int MapGuild = MStruct.map[map].guildnum;

            string packet = "";
            packet += GStruct.guild[MapGuild].name + ";"; packet += GStruct.guild[MapGuild].shield + ";"; packet += GStruct.guild[MapGuild].hue + ";";

            SendToMap(map, String.Format("<83>{0}</83>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerAtrTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerAtrTo(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int charFire = (PStruct.character[index, PStruct.player[index].SelectedChar].Fire);
            int charEarth = (PStruct.character[index, PStruct.player[index].SelectedChar].Earth);
            int charWater = (PStruct.character[index, PStruct.player[index].SelectedChar].Water);
            int charWind = (PStruct.character[index, PStruct.player[index].SelectedChar].Wind);
            int charDark = (PStruct.character[index, PStruct.player[index].SelectedChar].Dark);
            int charLight = (PStruct.character[index, PStruct.player[index].SelectedChar].Light);
            int charPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].Points);

            string packet = "";
            packet += index + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            SendToUser(index, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExtraVitalityTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraVitalityTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PStruct.GetExtraVitality(index);

            string packet = "";
            packet += index + ";"; packet += extra_vitality + ";";

            SendToUser(user, String.Format("<79>{0}</79>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExtraVitalityToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraVitalityToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            {
                int map = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
                int extra_vitality = PStruct.GetExtraVitality(index);

                string packet = "";
                packet += index + ";"; packet += extra_vitality + ";";

                SendToMap(map, String.Format("<79>{0}</79>\n", packet));
            }
        }
        //*********************************************************************************************
        // Send_PlayerExtraVitalityToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraVitalityToParty(int partynum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PStruct.GetExtraVitality(index);

            string packet = "";
            packet += index + ";"; packet += extra_vitality + ";";

            SendToParty(partynum, String.Format("<79>{0}</79>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExtraSpiritTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraSpiritTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PStruct.GetExtraSpirit(index);

            string packet = "";
            packet += index + ";"; packet += extra_vitality + ";";

            SendToUser(user, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExtraSpiritToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraSpiritToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int map = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int extra_vitality = PStruct.GetExtraSpirit(index);

            string packet = "";
            packet += index + ";"; packet += extra_vitality + ";";

            SendToMap(map, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExtraSpiritToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerExtraSpiritToParty(int partynum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index) != null)
            {
                return;
            }

            //CÓDIGO
            int extra_vitality = PStruct.GetExtraSpirit(index);

            string packet = "";
            packet += index + ";"; packet += extra_vitality + ";";

            SendToParty(partynum, String.Format("<80>{0}</80>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerAtrToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerAtrToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int charMap = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int charFire = (PStruct.character[index, PStruct.player[index].SelectedChar].Fire);
            int charEarth = (PStruct.character[index, PStruct.player[index].SelectedChar].Earth);
            int charWater = (PStruct.character[index, PStruct.player[index].SelectedChar].Water);
            int charWind = (PStruct.character[index, PStruct.player[index].SelectedChar].Wind);
            int charDark = (PStruct.character[index, PStruct.player[index].SelectedChar].Dark);
            int charLight = (PStruct.character[index, PStruct.player[index].SelectedChar].Light);
            int charPoints = (PStruct.character[index, PStruct.player[index].SelectedChar].Points);

            string packet = "";
            packet += index + ";"; packet += charFire + ";"; packet += charEarth + ";";
            packet += charWater + ";"; packet += charWind + ";";
            packet += charDark + ";"; packet += charLight + ";"; packet += charPoints + ";";

            SendToMap(charMap, String.Format("<34>{0}</34>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza experiência do jogador.
        //*********************************************************************************************
        public static void Send_PlayerExp(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<35>{0}</35>\n", PStruct.character[index, PStruct.player[index].SelectedChar].Exp));
        }
        //*********************************************************************************************
        // Send_PlayerSkillPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza os pontos de habilidade de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerSkillPoints(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<36>{0}</36>\n", PStruct.character[index, PStruct.player[index].SelectedChar].SkillPoints));
        }
        //*********************************************************************************************
        // Send_PlayerLevel / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o nível de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerLevel(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<37 {0}>{1}</37>\n", index, PStruct.character[index, PStruct.player[index].SelectedChar].Level));
        }
        //*********************************************************************************************
        // Send_PartyRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PartyRequest(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<38>{0}</38>\n", index));
        }
        //*********************************************************************************************
        // Send_PartyDataTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PartyDataTo(int partynum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PStruct.GetPartyMembersCount(partynum);
            packet = packet + (partymemberscount) + ",";
            packet = packet + PStruct.party[partynum].leader + ",";
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PStruct.partymembers[partynum, i].index + ",";
            }
            SendToUser(index, String.Format("<39>{0}</39>\n", packet));
        }
        //*********************************************************************************************
        // Send_PartyDataToParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PartyDataToParty(int partynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PStruct.GetPartyMembersCount(partynum);
            packet = packet + (partymemberscount) + ",";
            packet = packet + PStruct.party[partynum].leader + ",";
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PStruct.partymembers[partynum, i].index + ",";
            }
            SendToParty(partynum, String.Format("<39>{0}</39>\n", packet));
        }
        //*********************************************************************************************
        // Send_PartyKick / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PartyKick(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<40></40>\n"));
        }
        //*********************************************************************************************
        // Send_PartyVital / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza a vida de todos no grupo.
        //*********************************************************************************************
        public static void Send_PartyVital(int partynum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int partymemberscount = PStruct.GetPartyMembersCount(partynum);
            for (int i = 1; i <= partymemberscount; i++)
            {
                packet = packet + PStruct.character[PStruct.partymembers[partynum, i].index, PStruct.player[PStruct.partymembers[partynum, i].index].SelectedChar].Vitality + ",";
                packet = packet + PStruct.character[PStruct.partymembers[partynum, i].index, PStruct.player[PStruct.partymembers[partynum, i].index].SelectedChar].Spirit + ",";
            }

            SendToUser(index, String.Format("<40>{0}</40>\n", packet));
        }
        //*********************************************************************************************
        // Send_PartyChange / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PartyChange(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<41>{0}</41>\n", index));
        }
        //*********************************************************************************************
        // Send_TradeRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_TradeRequest(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<42>{0}</42>\n", index));
        }
        //*********************************************************************************************
        // Send_GuildRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_GuildRequest(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<68>{0}</68>\n", index));
        }
        //*********************************************************************************************
        // Send_FriendRequest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_FriendRequest(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<86>{0}</86>\n", index));
        }
        //*********************************************************************************************
        // Send_PetAttack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Avisa que o pet está atacando.
        //*********************************************************************************************
        public static void Send_PetAttack(int index, int target, int targettype)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, target, targettype) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            packet = packet + index + ";";
            packet = packet + target + ";";
            packet = packet + targettype + ";";
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<85>{0}</85>\n", packet));
        }
        //*********************************************************************************************
        // Send_TradeOffers / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza itens em oferta em uma troca.
        //*********************************************************************************************
        public static void Send_TradeOffers(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, user) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int tradeofferscount = PStruct.GetPlayerTradeOffersCount(index);
            packet = packet + (index) + ";";
            packet = packet + tradeofferscount + ";";
            packet = packet + PStruct.tempplayer[index].TradeG + ";";
            for (int i = 1; i <= tradeofferscount; i++)
            {
                packet = packet + PStruct.tradeslot[index, i].item + ";";
            }
            SendToUser(user, String.Format("<43>{0}</43>\n", packet));
        }
        //*********************************************************************************************
        // Send_TradeAccept / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_TradeAccept(int user, int code)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, code) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<44>{0}</44>\n", code));
        }
        //*********************************************************************************************
        // Send_TradeRefuse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_TradeRefuse(int user, int code)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, code) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<45>{0}</45>\n", code)); 
        }
        //*********************************************************************************************
        // Send_TradeClose / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_TradeClose(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<46></46>\n"));
        }
        //*********************************************************************************************
        // Send_PlayerG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualizar ouro do jogador.
        //*********************************************************************************************
        public static void Send_PlayerG(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<47>{0}</47>\n", PStruct.character[user, PStruct.player[user].SelectedChar].Gold));
        }
        //*********************************************************************************************
        // Send_PlayerC / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Cash :D
        //*********************************************************************************************
        public static void Send_PlayerC(int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<48>{0}</48>\n", PStruct.character[user, PStruct.player[user].SelectedChar].Cash));
        }
        //*********************************************************************************************
        // Send_TradeG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza ouro na troca.
        //*********************************************************************************************
        public static void Send_TradeG(int index, int user)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<49 {0}>{1}</49>\n", index, PStruct.tempplayer[index].TradeG));
        }
        //*********************************************************************************************
        // Send_AllQuests / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualizar missões para um jogador.
        //*********************************************************************************************
        public static void Send_AllQuests(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int questcount = PStruct.GetPlayerQuestsCount(index);
            packet = packet + questcount + ";";
            for (int g = 1; g < Globals.MaxQuestGivers; g++)
            {
                for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                {
                    if (PStruct.queststatus[index, g, q].status != 0)
                    {
                        packet = packet + g + ";";
                        packet = packet + q + ";";
                        packet = packet + PStruct.queststatus[index, g, q].status + ";";
                        for (int k = 1; k < Globals.MaxQuestKills; k++)
                        {
                            packet = packet + PStruct.questkills[index, g, q, k].kills + ";";
                        }
                        for (int a = 1; a < Globals.MaxQuestActions; a++)
                        {
                            string actioninfo = "0";

                            if (PStruct.questactions[index, g, q, a].actiondone == true) { actioninfo = "1"; }
                            if (PStruct.questactions[index, g, q, a].actiondone == false) { actioninfo = "0"; }

                            packet = packet + actioninfo + ";";
                        }
                    }
                }
            }
            SendToUser(index, String.Format("<50>{0}</50>\n", packet));
        }
        //*********************************************************************************************
        // Send_QuestKill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_QuestKill(int index, int g, int q, int k)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, g, q, k) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<51>{0}</51>\n", g + ";" + q + ";" + k + ";" + PStruct.questkills[index, g, q, k].kills));
        }
        //*********************************************************************************************
        // Send_QuestAction / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_QuestAction(int index, int g, int q, int a)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, q, q, a) != null)
            {
                return;
            }

            //CÓDIGO
            string actioninfo = "0";

            if (PStruct.questactions[index, g, q, a].actiondone == true) { actioninfo = "1"; }
            if (PStruct.questactions[index, g, q, a].actiondone == false) { actioninfo = "0"; }

            SendToUser(index, String.Format("<62>{0}</62>\n", g + ";" + q + ";" + a + ";" + actioninfo));
        }
        //*********************************************************************************************
        // Send_KnockBack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Envia um empurrão para o jogador ou npc.
        //*********************************************************************************************
        public static void Send_KnockBack(int map, int type, int index, int dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, index, dir, range) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<52>{0}</52>\n", type + ";" + index + ";" + dir + ";" + range));
        }
        //*********************************************************************************************
        // Send_Sleep / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Sleep(int map, int type, int index, int is_sleeping)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, index, is_sleeping) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<53>{0}</53>\n", type + ";" + index + ";" + is_sleeping));

        }
        //*********************************************************************************************
        // Send_Stun / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Stun(int map, int type, int index, int is_stunned)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, type, index, is_stunned) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<54>{0}</54>\n", type + ";" + index + ";" + is_stunned));
        }
        //*********************************************************************************************
        // Send_SleepToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_SleepToUser(int user, int type, int index, int is_sleeping)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, type, index, is_sleeping) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(user, String.Format("<53>{0}</53>\n", type + ";" + index + ";" + is_sleeping));
        }
        //*********************************************************************************************
        // Send_StunToUser / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_StunToUser(int user, int type, int index, int is_stunned)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, type, index, is_stunned) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(user, String.Format("<54>{0}</54>\n", type + ";" + index + ";" + is_stunned));
        }
        //*********************************************************************************************
        // Send_Shop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Shop(int index, int shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, shopnum) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int item_count = ShopStruct.shop[shopnum].item_count;
            packet = packet + item_count + ";";
            for (int s = 1; s <= item_count; s++)
            {
                packet = packet + s + ";";
                packet = packet + ShopStruct.shopitem[shopnum, s].type + ";";
                packet = packet + ShopStruct.shopitem[shopnum, s].num + ";";
                packet = packet + ShopStruct.shopitem[shopnum, s].value + ";";
                packet = packet + ShopStruct.shopitem[shopnum, s].refin + ";";
                packet = packet + ShopStruct.shopitem[shopnum, s].price + ";";
            }
            SendToUser(index, String.Format("<55>{0}</55>\n", packet));
        }
        //*********************************************************************************************
        // Send_Craft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Craft(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            for (int c = 1; c < Globals.Max_Craft; c++)
            {
                packet = packet + c + ";";
                packet = packet + PStruct.craft[index, c].type + ";";
                packet = packet + PStruct.craft[index, c].num + ";";
                packet = packet + PStruct.craft[index, c].value + ";";
                packet = packet + PStruct.craft[index, c].refin + ";";
            }
            SendToUser(index, String.Format("<56>{0}</56>\n", packet));
        }
        //*********************************************************************************************
        // Send_Profs / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Profs(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            //PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Type[1] = 2;
            //PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[1] = 3;
            //PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[1] = 2;
            string packet = "";
            int prof_count = Globals.Max_Profs_Per_Char - 1;
            packet = packet + prof_count + ";";
            for (int j = 1; j < Globals.Max_Profs_Per_Char; j++)
            {
                packet = packet + j + ";";
                packet = packet + PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Type[j] + ";";
                packet = packet + PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[j] + ";";
                packet = packet + PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[j] + ";";
            }
            SendToUser(index, String.Format("<60>{0}</60>\n", packet));
        }
        //*********************************************************************************************
        // Send_ProfExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_ProfEXP(int index, int profnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, profnum) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<57>{0};{1}</57>\n", profnum, PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Exp[profnum]));
        }
        //*********************************************************************************************
        // Send_ProfLEVEL / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_ProfLEVEL(int index, int profnum)
        {            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, profnum) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<58>{0};{1}</58>\n", profnum, PStruct.character[index, PStruct.player[index].SelectedChar].Prof_Level[profnum]));
        }
        //*********************************************************************************************
        // Send_EventGraphic / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um evento no jogo.
        //*********************************************************************************************
        public static void Send_EventGraphic(int index, int event_id, string graphic, int graphic_index, byte is_tile = 0, byte dir = 2)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, event_id, graphic, graphic_index, is_tile, dir) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<59>{0}</59>\n", event_id + ";" + graphic + ";" + graphic_index + ";" + is_tile + ";" + dir));
        }
        //*********************************************************************************************
        // Send_EventGraphicToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um evento no jogo.
        //*********************************************************************************************
        public static void Send_EventGraphicToMap(int map, int event_id, string graphic, int graphic_index, byte is_tile = 0, byte dir = 2)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, event_id, graphic, graphic_index, is_tile, dir) != null)
            {
                return;
            }

            //CÓDIGO
            SendToMap(map, String.Format("<59>{0}</59>\n", event_id + ";" + graphic + ";" + graphic_index + ";" + is_tile + ";" + dir));
        }
        //*********************************************************************************************
        // Send_BankSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_BankSlots(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
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
                packet = packet + PStruct.player[index].bankslot[b].type + ";";
                packet = packet + PStruct.player[index].bankslot[b].num + ";";
                packet = packet + PStruct.player[index].bankslot[b].value + ";";
                packet = packet + PStruct.player[index].bankslot[b].refin + ";";
                packet = packet + PStruct.player[index].bankslot[b].exp + ";";
            }
            SendToUser(index, String.Format("<61>{0}</61>\n", packet));
        }
        //*********************************************************************************************
        // Send_PShopSlots / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PShopSlots(int shopindex, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, shopindex, index) != null)
            {
                return;
            }

            //CÓDIGO
            string packet = "";
            int pshop_count = Globals.Max_PShops - 1;
            packet = packet + shopindex + ";";
            packet = packet + pshop_count + ";";
            for (int b = 1; b < Globals.Max_PShops; b++)
            {
                packet = packet + b + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].type + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].num + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].value + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].refin + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].exp + ";";
                packet = packet + PStruct.character[shopindex, PStruct.player[shopindex].SelectedChar].pshopslot[b].price + ";";
            }
            SendToUser(index, String.Format("<81>{0}</81>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerShoppingTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerShoppingTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (PStruct.tempplayer[index].Shopping) { value = 1; }

            SendToUser(user, String.Format("<82>{0};{1}</82>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerShoppingToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerShoppingToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;
            if (PStruct.tempplayer[index].Shopping) { value = 1; }

            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<82>{0};{1}</82>\n", index, value));

        }
        //*********************************************************************************************
        // Send_Premmy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Premmy(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int result = 0;

            if (PStruct.IsPlayerPremmy(index)) { result = 1; }

            SendToUser(index, String.Format("<63>{0};{1}</63>\n", result, PStruct.player[index].Premmy));
        }
        //*********************************************************************************************
        // Send_WPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_WPoints(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<64>{0}</64>\n", PStruct.player[index].WPoints));
        }
        //*********************************************************************************************
        // Send_Recipe / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Recipe(int index, int recipetype, int recipenum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, recipetype, recipenum) != null)
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
                packet = packet + MStruct.craftrecipe[recipetype, recipenum, r].type + ";";
                packet = packet + MStruct.craftrecipe[recipetype, recipenum, r].num + ";";
                packet = packet + MStruct.craftrecipe[recipetype, recipenum, r].value + ";";
                packet = packet + MStruct.craftrecipe[recipetype, recipenum, r].refin + ";";
            }
            SendToUser(index, String.Format("<65>{0}</65>\n", packet));
        }
        //*********************************************************************************************
        // Send_GuildTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_GuildTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].Guild <= 0) { return; }
            SendToUser(user, String.Format("<66>{0}</66>\n", index + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].name + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].shield + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // Send_GuildToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_GuildToMap(int mapnum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum, index) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].Guild <= 0) { return; }
            SendToMap(mapnum, String.Format("<66>{0}</66>\n", index + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].name + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].shield + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // Send_GuildToMapBut / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_GuildToMapBut(int mapnum, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum, index) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].Guild <= 0) { return; }
            SendToMapBut(index, mapnum, String.Format("<66>{0}</66>\n", index + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].name + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].shield + ";" + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].hue));
        }
        //*********************************************************************************************
        // Send_CompleteGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_CompleteGuild(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO  
            // PStruct.character[index, PStruct.player[index].SelectedChar].Guild = 1;
            if (PStruct.character[index, PStruct.player[index].SelectedChar].Guild <= 0) { return; }

            string packet = "";
            int member_count = GStruct.GetMember_Count(PStruct.character[index, PStruct.player[index].SelectedChar].Guild);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].name + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].level + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].exp + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].shield + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].hue + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].memberlist[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].membersprite[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].memberhue[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].membersprite_index[i] + ";";
            }

            SendToUser(index, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // Send_CompleteClearGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_CompleteClearGuild(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            // PStruct.character[index, PStruct.player[index].SelectedChar].Guild = 1;

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

            SendToUser(index, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // Send_CompleteGuildToGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_CompleteGuildToGuild(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            if (PStruct.character[index, PStruct.player[index].SelectedChar].Guild <= 0) { return; }

            string packet = "";
            int member_count = GStruct.GetMember_Count(PStruct.character[index, PStruct.player[index].SelectedChar].Guild);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].name + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].level + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].exp + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].shield + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].hue + ";";
            packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].memberlist[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].membersprite[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].memberhue[i] + ";";
                packet = packet + GStruct.guild[PStruct.character[index, PStruct.player[index].SelectedChar].Guild].membersprite_index[i] + ";";
            }

            SendToGuild(PStruct.character[index, PStruct.player[index].SelectedChar].Guild, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // Send_CompleteGuildToGuildG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_CompleteGuildToGuildG(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return;
            }

            //CÓDIGO
            if (guildnum <= 0) { return; }

            string packet = "";
            int member_count = GStruct.GetMember_Count(guildnum);

            if (member_count <= 0) { return; }

            packet = packet + member_count + ";";
            packet = packet + GStruct.guild[guildnum].name + ";";
            packet = packet + GStruct.guild[guildnum].level + ";";
            packet = packet + GStruct.guild[guildnum].exp + ";";
            packet = packet + GStruct.guild[guildnum].shield + ";";
            packet = packet + GStruct.guild[guildnum].hue + ";";
            packet = packet + GStruct.guild[guildnum].leader + ";";

            for (int i = 1; i <= member_count; i++)
            {
                packet = packet + i + ";";
                packet = packet + GStruct.guild[guildnum].memberlist[i] + ";";
                packet = packet + GStruct.guild[guildnum].membersprite[i] + ";";
                packet = packet + GStruct.guild[guildnum].memberhue[i] + ";";
                packet = packet + GStruct.guild[guildnum].membersprite_index[i] + ";";
            }

            SendToGuild(guildnum, String.Format("<67>{0}</67>\n", packet));
        }
        //*********************************************************************************************
        // Send_PlayerDeathToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerDeathToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;

            if (PStruct.tempplayer[index].isDead) { value = 1; }

            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<69>{0};{1}</69>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerDeathTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerDeathTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value = 0;

            if (PStruct.tempplayer[index].isDead) { value = 1; }

            SendToUser(user, String.Format("<69>{0};{1}</69>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerPvpToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerPvpToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PStruct.character[index, PStruct.player[index].SelectedChar].PVP) { value = 1; } else { value = 0; }
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<70>{0};{1}</70>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerPvpTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerPvpTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PStruct.character[index, PStruct.player[index].SelectedChar].PVP) { value = 1; } else { value = 0; }
            SendToUser(user, String.Format("<70>{0};{1}</70>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerPvpChangeTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerPvpChangeTimer(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PStruct.character[index, PStruct.player[index].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            SendToUser(index, String.Format("<71>{0}</71>\n", result));
        }
        //*********************************************************************************************
        // Send_PlayerPvpBanTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerPvpBanTimer(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PStruct.character[index, PStruct.player[index].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            SendToUser(index, String.Format("<72>{0}</72>\n", result));
        }
        //*********************************************************************************************
        // Send_PlayerSoreToMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSoreToMap(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PStruct.PlayerIsSore(index)) { value = 1; } else { value = 0; }
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<73>{0};{1}</73>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerSoreTo / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSoreTo(int user, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, user, index) != null)
            {
                return;
            }

            //CÓDIGO
            int value;
            if (PStruct.PlayerIsSore(index)) { value = 1; } else { value = 0; }
            SendToUser(user, String.Format("<73>{0};{1}</73>\n", index, value));
        }
        //*********************************************************************************************
        // Send_PlayerPvpSoreTimer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerPvpSoreTimer(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            long result = PStruct.character[index, PStruct.player[index].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
            if (result < 0) { result = 0; }
            SendToUser(index, String.Format("<74>{0}</74>\n", result));
        }
        //*********************************************************************************************
        // Send_PlayerSkillCoolDown / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_PlayerSkillCooldown(int index, int slot, int cooldown)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, slot, cooldown) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<75>{0};{1}</75>\n", slot, cooldown));
        }
        //*********************************************************************************************
        // Send_NStatus / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_NStatus(int index, string msg)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, msg) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<76>{0}</76>\n", msg));
        }
        //*********************************************************************************************
        // Send_Notice / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_Notice(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<77>{0}</77>\n", Globals.NOTICE));
        }
        //*********************************************************************************************
        // Send_BrokeSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Send_BrokeSkill(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return;
            }

            //CÓDIGO
            SendToUser(index, String.Format("<78></78>\n"));
        }
        //*********************************************************************************************
        // Send_PlayerSprite / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Atualiza o visual de um jogador.
        //*********************************************************************************************
        public static void Send_PlayerSprite(int index, string sprite_name, int sprite_index)
        {
            string packet = index + ";" + sprite_name + ";" + sprite_index;
            SendToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, String.Format("<91>{0}</91>\n", packet));
        }
    }
}
