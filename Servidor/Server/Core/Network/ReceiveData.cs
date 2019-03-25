using System;
using System.Linq;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Métodos e tratamento de todas as informações que o servidor recebe.
    // ReceiveData.cs
    //*********************************************************************************************
    class ReceiveData : Languages.LStruct
    {
        //*********************************************************************************************
        // SelectPacket / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Tratamento das packets enviadas pelos jogadores.
        //*********************************************************************************************
        public static void selectPacket(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, data) != null) { return; }

            //CÓDIGO
            Loops.Last_Packet = data;
            string[] packets = data.Split('\\');
            for (int i = 0; i < packets.Length; i++)
            {
                if (packets[i] == String.Empty) { break; }
                if (PlayerStruct.tempplayer[s].ingame)
                {
                    //Dados ingame
                    if (packets[i].StartsWith("<11>")) { receivedMove(s, packets[i]); }
                    else if (packets[i].StartsWith("<12>")) { receivedMessage(s, packets[i]); }
                    else if (packets[i].StartsWith("<15>")) { receivedMapData(s, packets[i]); }
                    else if (packets[i].StartsWith("<16>")) { receivedUseItem(s, packets[i]); }
                    else if (packets[i].StartsWith("<17>")) { receivedEquipItem(s, packets[i]); }
                    else if (packets[i].StartsWith("<18>")) { receivedAttack(s, packets[i]); }
                    else if (packets[i].StartsWith("<19>")) { receivedDir(s, packets[i]); }
                    else if (packets[i].StartsWith("<20>")) { receivedPickItem(s); }
                    else if (packets[i].StartsWith("<21>")) { receivedDropItem(s, packets[i]); }
                    else if (packets[i].StartsWith("<22>")) { receivedItemData(s, packets[i]); }
                    else if (packets[i].StartsWith("<23>")) { receivedWeaponData(s, packets[i]); }
                    else if (packets[i].StartsWith("<24>")) { receivedArmorData(s, packets[i]); }
                    else if (packets[i].StartsWith("<25>")) { receivedSkillData(s, packets[i]); }
                    else if (packets[i].StartsWith("<26>")) { receivedHotkey(s, packets[i]); }
                    else if (packets[i].StartsWith("<27>")) { receivedTarget(s, packets[i]); }
                    else if (packets[i].StartsWith("<28>")) { receivedEnemyData(s, packets[i]); }
                    else if (packets[i].StartsWith("<29>")) { receivedSkill(s, packets[i]); }
                    else if (packets[i].StartsWith("<30>")) { receivedAtr(s, packets[i]); }
                    else if (packets[i].StartsWith("<31>")) { receivedUseSkillPoints(s, packets[i]); }
                    else if (packets[i].StartsWith("<32>")) { receivedParty(s); }
                    else if (packets[i].StartsWith("<33>")) { receivedPartyResponse(s, packets[i]); }
                    else if (packets[i].StartsWith("<34>")) { receivedPartyKick(s, packets[i]); }
                    else if (packets[i].StartsWith("<35>")) { receivedPartyChange(s, packets[i]); }
                    else if (packets[i].StartsWith("<36>")) { receivedTrade(s); }
                    else if (packets[i].StartsWith("<37>")) { receivedAddTradeOffer(s, packets[i]); }
                    else if (packets[i].StartsWith("<38>")) { receivedAddTradeG(s, packets[i]); }
                    else if (packets[i].StartsWith("<39>")) { receivedTradeResponse(s, packets[i]); }
                    else if (packets[i].StartsWith("<40>")) { receivedTradeAccept(s); }
                    else if (packets[i].StartsWith("<41>")) { receivedTradeRefuse(s); }
                    else if (packets[i].StartsWith("<42>")) { receivedTradeClose(s); }
                    else if (packets[i].StartsWith("<43>")) { receivedQuestAction(s, packets[i]); }
                    else if (packets[i].StartsWith("<44>")) { receivedShop(s); }
                    else if (packets[i].StartsWith("<45>")) { receivedShopBuy(s, packets[i]); }
                    else if (packets[i].StartsWith("<46>")) { receivedShopSell(s, packets[i]); }
                    else if (packets[i].StartsWith("<47>")) { receivedShopClose(s); }
                    else if (packets[i].StartsWith("<48>")) { receivedCraftAdd(s, packets[i]); }
                    else if (packets[i].StartsWith("<49>")) { receivedCraftWith(s, packets[i]); }
                    else if (packets[i].StartsWith("<50>")) { receivedOpenCraft(s); }
                    else if (packets[i].StartsWith("<51>")) { receivedCloseCraft(s); }
                    else if (packets[i].StartsWith("<52>")) { receivedCraftCreate(s); }
                    else if (packets[i].StartsWith("<53>")) { receivedItemCraft(s, packets[i]); }
                    else if (packets[i].StartsWith("<54>")) { receivedOpenChest(s); }
                    else if (packets[i].StartsWith("<55>")) { receivedOpenBank(s); }
                    else if (packets[i].StartsWith("<56>")) { receivedBankPickItem(s, packets[i]); }
                    else if (packets[i].StartsWith("<57>")) { receivedBankGiveItem(s, packets[i]); }
                    else if (packets[i].StartsWith("<58>")) { receivedCloseBank(s); }
                    else if (packets[i].StartsWith("<59>")) { receivedCompleteAction(s, packets[i]); }
                    else if (packets[i].StartsWith("<61>")) { receivedCharacterSelection(s); }
                    else if (packets[i].StartsWith("<62>")) { receivedGuild(s); }
                    else if (packets[i].StartsWith("<63>")) { receivedGuildResponse(s, packets[i]); }
                    else if (packets[i].StartsWith("<64>")) { receivedGuildCreate(s, packets[i]); }
                    else if (packets[i].StartsWith("<65>")) { receivedGuildKick(s, packets[i]); }
                    else if (packets[i].StartsWith("<66>")) { receivedRespawn(s); }
                    else if (packets[i].StartsWith("<70>")) { receivedWarp(s, packets[i]); }
                    else if (packets[i].StartsWith("<71>")) { receivedPVP(s); }
                    else if (packets[i].StartsWith("<72>")) { receivedImprovement(s, packets[i]); }
                    else if (packets[i].StartsWith("<74>")) { receivedPremmy(s, packets[i]); } //mod
                    else if (packets[i].StartsWith("<75>")) { receivedBan(s, packets[i]); } //mod
                    else if (packets[i].StartsWith("<76>")) { receivedKick(s, packets[i]); } //mod
                    else if (packets[i].StartsWith("<77>")) { receivedWPoints(s, packets[i]); } //mod
                    else if (packets[i].StartsWith("<78>")) { receivedSetPos(s, packets[i]); } //mod
                    else if (packets[i].StartsWith("<79>")) { receivedTP(s, packets[i]); }
                    else if (packets[i].StartsWith("<80>")) { receivedAddPShop(s, packets[i]); }
                    else if (packets[i].StartsWith("<81>")) { receivedWithPShop(s, packets[i]); }
                    else if (packets[i].StartsWith("<82>")) { receivedStartPShop(s); }
                    else if (packets[i].StartsWith("<83>")) { receivedBuyPShop(s, packets[i]); }
                    else if (packets[i].StartsWith("<84>")) { receivedOpenPShop(s, packets[i]); }
                    else if (packets[i].StartsWith("<85>")) { receivedClosePShop(s); }
                    else if (packets[i].StartsWith("<86>")) { receivedCollector(s); }
                    else if (packets[i].StartsWith("<87>")) { receivedOutCollector(s); }
                    else if (packets[i].StartsWith("<89>")) { receivedsavePoint(s); }
                    else if (packets[i].StartsWith("<90>")) { receivedFriendAdd(s); }
                    else if (packets[i].StartsWith("<91>")) { receivedFriendResponse(s, packets[i]); }
                    else if (packets[i].StartsWith("<92>")) { receivedFriendDelete(s, packets[i]); }
                }
                else
                {
                    //Dados fora do jogo
                    if (packets[i].StartsWith("<0>")) { receivedAuth(s, packets[i]); }
                    else if (packets[i].StartsWith("<3>")) { receivedMotd(s); }
                    else if (packets[i].StartsWith("<4>")) { receivedLogin(s, packets[i]); }
                    else if (packets[i].StartsWith("<5>")) { receivedRegister(s, packets[i]); }
                    else if (packets[i].StartsWith("<88>")) { receivedError(packets[i]); }
                    //Dados depois do login
                    if (PlayerStruct.tempplayer[s].Logged)
                    {
                        if (packets[i].StartsWith("<6>")) { receivedNewChar(s, packets[i]); }
                        else if (packets[i].StartsWith("<8>")) { receivedIngame(s, packets[i]); }
                        else if (packets[i].StartsWith("<60>")) { receivedDeleteChar(s, packets[i]); }
                        else if (packets[i].StartsWith("<73>")) { receivedWBuy(s, packets[i]); }
                    }
                }
                
                //Dados globais
                if (packets[i].StartsWith("<14>")) { receivedLatency(s); }
            }
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
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, data));
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
        // receivedPVP / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedPVP(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer > Loops.TickCount.ElapsedMilliseconds) { return; }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP = false;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer = 60000 + Loops.TickCount.ElapsedMilliseconds;
            }
            else
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP = true;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer = 60000 + Loops.TickCount.ElapsedMilliseconds;
            }
            SendData.sendPlayerPvpToMap(s);
            SendData.sendPlayerPvpChangeTimer(s);
        }
        //*********************************************************************************************
        // receivedAuth / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedAuth(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] packet = data.Replace("<0>", "").Split(';');
            if (packet[0] != Globals.Client_Version) { SendData.sendNStatus(s, "Versão inválida, por favor atualize o jogo."); WinsockAsync.shutdownUser(UserConnection.getS(s)) ;return; }
            SendData.sendToUser(s, String.Format("<0 {0}>1</0>\n", s.ToString()));
        }
        //*********************************************************************************************
        // receivedMotd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedMotd(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].ingame) {return;}
            SendData.sendToUser(s, String.Format("<3>{0}</3>\n", WinsockAsync.Motd));
        }
        //*********************************************************************************************
        // receivedSetPos / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedSetPos(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 10) { return; }
            string[] packet = data.Replace("<78>", "").Split(';');
            if (packet.Length != 2) { return; }
            if (!isNumeric(packet[1])) { return; }
            if (!isNumeric(packet[0])) { return; }

            byte x = Convert.ToByte(packet[0]);
            byte  y = Convert.ToByte(packet[1]);

            if ((x < 0) || (x > Globals.MaxMapsX)) { return; }
            if ((y < 0) || (y > Globals.MaxMapsY)) { return; }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = x;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = y;

            SendData.sendPlayerXY(s);
        }
        //*********************************************************************************************
        // receivedFriendAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedFriendAdd(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Possíveis tentativas de hacker
            if ((PlayerStruct.tempplayer[s].targettype != 1) || (PlayerStruct.tempplayer[s].target <= 0) || (PlayerStruct.tempplayer[s].InviteTimer > Loops.TickCount.ElapsedMilliseconds)) { SendData.sendMsgToPlayer(s, lang.player_can_not_receive_invite_at_this_moment, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            //Não pode convidar ele mesmo
            if (PlayerStruct.tempplayer[s].target == s)
            {
                SendData.sendMsgToPlayer(s, lang.you_can_not_invite_yourself, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //O alvo é o s
            int target = PlayerStruct.tempplayer[s].target;
            int clients = UserConnection.getS(target);


            //Verifica se ele não se desconectou no processo
            if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[clients].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verificar se já não está na lista
            if (FriendRelations.friendNameExist(s, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].CharacterName))
            {
                SendData.sendMsgToPlayer(s, lang.player_already_is_your_friend, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Criar a váriaveis gerais
            PlayerStruct.tempplayer[s].Inviting = PlayerStruct.tempplayer[s].target;
            PlayerStruct.tempplayer[target].Invited = s;
            PlayerStruct.tempplayer[s].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;
            PlayerStruct.tempplayer[target].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;

            //Envia o convite
            SendData.sendFriendRequest(s, target);
        }
        //*********************************************************************************************
        // receivedFriendResponse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedFriendResponse(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<91>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 2)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int response = Convert.ToInt32(splited[0]);
            int clients = UserConnection.getS(PlayerStruct.tempplayer[s].Invited);

            //Verificar se o jogador não se desconectou no processo
            if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[clients].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int target = PlayerStruct.tempplayer[s].Invited;

            if (response == 0)
            {
                SendData.sendMsgToPlayer(s, lang.you_have_a_new_friend, Globals.ColorGreen, Globals.Msg_Type_Server);
                SendData.sendMsgToPlayer(target, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + " " + lang.accepted_your_friend_request, Globals.ColorGreen, Globals.Msg_Type_Server);
                FriendRelations.addFriend(s, target);
                FriendRelations.addFriend(target, s);
            }
            else if (response == 1)
            {
                //Limpa os valores gerais
                PlayerStruct.tempplayer[s].InviteTimer = 0;
                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                PlayerStruct.tempplayer[s].Invited = 0;
                SendData.sendMsgToPlayer(target, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + " " + lang.refused_your_friend_request, Globals.ColorRed, Globals.Msg_Type_Server);
            }
            
        }
        //*********************************************************************************************
        // receivedFriendDelete / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedFriendDelete(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<92>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 200)) { return; }
            int friendnum = Convert.ToInt32(splited[0]);

            FriendRelations.deleteFriend(s, friendnum);

        }
        //*********************************************************************************************
        // receivedError / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Log de erros que os jogadores recebem no cliente.
        //*********************************************************************************************
        public static void receivedError(string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] packet = data.Replace("<88>", "").Split(';');
            Database.Handler.logAdd(packet[0]);
            Console.WriteLine("Um novo erro foi registrado, verifique-o em Log.txt.");
        }
        //*********************************************************************************************
        // ReceiveWPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedWPoints(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 10) { return; }
            string[] packet = data.Replace("<77>", "").Split(';');
            if (packet.Length != 2) { return; }
            if (!isNumeric(packet[1])) { return; }

            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.player[i].Email == packet[0])
                {
                    WinsockAsync.shutdownUser(UserConnection.getS(i));
                }
            }

            if (Database.Accounts.addWPoints(packet[0], Convert.ToInt32(packet[1])))
            { SendData.sendMsgToPlayer(s, lang.account_received_wpoints_success, Globals.ColorGreen, Globals.Msg_Type_Server); }
            else { SendData.sendMsgToPlayer(s, lang.wpoints_deliver_fail, Globals.ColorGreen, Globals.Msg_Type_Server); }
        }
        //*********************************************************************************************
        // receivedKick / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retirar determinado jogador do jogo.
        //*********************************************************************************************
        public static void receivedKick(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 10) { return; }
            string[] packet = data.Replace("<76>", "").Split(';');
            if (packet.Length != 1) { return; }

            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == packet[0])
                {
                    SendData.sendMsgToPlayer(s, lang.player_has_been_kicked_success, Globals.ColorGreen, Globals.Msg_Type_Server);
                    WinsockAsync.shutdownUser(UserConnection.getS(i));
                    return;
                }
            }
            SendData.sendMsgToPlayer(s, lang.player_not_found, Globals.ColorGreen, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // receivedTP / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Jogador tentando se teleportar através de um TP Point.
        //*********************************************************************************************
        public static void receivedTP(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] packet = data.Replace("<79>", "").Split(';');
            if (packet.Length != 1) { return; }
            if (!isNumeric(packet[0])) { return; }
            if (Convert.ToInt32(packet[0]) < 0) { return; }
            if (Convert.ToInt32(packet[0]) > 5) { return; }

            int id = Convert.ToInt32(packet[0]);

            for (int i = 1; i < Globals.Max_TpPoints; i++)
            {
                if (MapStruct.tppoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold >= MapStruct.tppoint[i].cost)
                    {
                        if (id <= MapStruct.tppoint[i].count)
                        {
                            MovementRelations.playerWarp(s, MapStruct.tppoint[i].tp_map[id], MapStruct.tppoint[i].tp_x[id], MapStruct.tppoint[i].tp_y[id]);
                            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold -= MapStruct.tppoint[i].cost;
                            SendData.sendPlayerG(s);
                        }
                    }
                    else
                    {
                        return; 
                    }
                }
            }
        }
        //*********************************************************************************************
        // receivedsavePoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salvar nova posição de respawn do jogador.
        //*********************************************************************************************
        public static void receivedsavePoint(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_savePoints; i++)
            {
                if (MapStruct.savepoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootMap = MapStruct.savepoint[i].save_map;
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootX = MapStruct.savepoint[i].save_x;
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootY = MapStruct.savepoint[i].save_y;
                    return;
                }
            }
        }
        //*********************************************************************************************
        // receivedPremmy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adicionar dias de Premmy através de um comando.
        //*********************************************************************************************
        public static void receivedPremmy(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 10) { return; }
            string[] packet = data.Replace("<74>", "").Split(';');
            if (packet.Length != 2) { return; }
            if (!isNumeric(packet[1])) { return; }

            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.player[i].Email == packet[0])
                {
                    WinsockAsync.shutdownUser(UserConnection.getS(i));
                }
            }

            if (Database.Accounts.addPremmy(packet[0], Convert.ToInt32(packet[1])))
            { SendData.sendMsgToPlayer(s, lang.account_received_premmy_success, Globals.ColorGreen, Globals.Msg_Type_Server); }
            else { SendData.sendMsgToPlayer(s, lang.account_received_premmy_fail, Globals.ColorGreen, Globals.Msg_Type_Server); }
        }
        //*********************************************************************************************
        // receivedBan / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para banir determinado jogador.
        //*********************************************************************************************
        public static void receivedBan(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 10) { return; }
            string[] packet = data.Replace("<75>", "").Split(';');
            if (packet.Length != 2) { return; }
            if (!isNumeric(packet[1])) { return; }

            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.player[i].Email == packet[0])
                {
                    WinsockAsync.shutdownUser(UserConnection.getS(i));
                }
            }

            if (Database.Accounts.addBan(packet[0], Convert.ToInt32(packet[1])))
            { SendData.sendMsgToPlayer(s, lang.account_banned_success, Globals.ColorGreen, Globals.Msg_Type_Server); }
            else { SendData.sendMsgToPlayer(s, lang.account_banned_fail, Globals.ColorGreen, Globals.Msg_Type_Server); }
        }
        //*********************************************************************************************
        // receivedLogin / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedLogin(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            WinsockAsync.Log(String.Format("Tentativa de login..."));
            string[] login = data.Replace("<4>", "").Split(';');

            if (login.Length != 2) { return; }
            if (login[0].Length > 100) { return; }
            if (login[1].Length > 8) { return; }

            if (Database.Handler.nameIsIllegal(login[0])) { return; }
            if (Database.Handler.nameIsIllegal(login[1])) { return; }
            if ((!PlayerStruct.tempplayer[s].Logged) && (!String.IsNullOrEmpty(PlayerStruct.player[s].Email))) { PlayerStruct.clearPlayer(s, true); }

            login[0] = login[0].ToLower();

            string response = WinsockAsync.LoginAnswer(login, s);
            //SendData.sendNStatus(s, "Acesso bloqueado, o servidor está em manutenção."); return;

            SendData.sendToUser(s, String.Format("<4 {0}>{1}</4>\n", login[0], response));
            PlayerStruct.tempplayer[s].ingame = false;
            if (response == "a")
            {
                if (PlayerRelations.isPlayerBanned(s)) { SendData.sendNStatus(s, "Essa conta está banida até " + PlayerStruct.player[s].Banned.ToString()); return; }
                if (!PlayerStruct.player[s].Confirmed)
                {
                    SendData.sendToUser(s, String.Format("<4 {0}>{1}</4>\n", "", "x"));
                }
                else
                {
                    SendData.sendToUser(s, String.Format("<4 {0}>{1}</4>\n", "", "r"));
                }
                receivedLoadChar(s, 0);
                receivedLoadChar(s, 1);
                receivedLoadChar(s, 2);
                SendData.sendPremmy(s);
                SendData.sendWPoints(s);
                SendData.sendNotice(s);
                //Carrega os itens no banco
                Database.Banks.loadBank(s);
                Database.FriendLists.loadFriendList(s);
                PlayerStruct.tempplayer[s].Logged = true;
            }
        }
        //*********************************************************************************************
        // receivedWarp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para sair do mapa.
        //*********************************************************************************************
        public static void receivedWarp(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] packet = data.Replace("<70>", "").Split(';');
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access < 3) { return; }
            MovementRelations.playerWarp(s, Convert.ToInt32(packet[0]), PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
        }
        //*********************************************************************************************
        // receivedCharacterSelection / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedCharacterSelection(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].ingame)
            {
                //Sai da troca
                if (PlayerStruct.tempplayer[s].InTrade > 0)
                {
                    TradeRelations.giveTrade(s);
                    TradeRelations.giveTrade(PlayerStruct.tempplayer[s].InTrade);


                    if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
                    {
                        TradeRelations.clearTempTrade(PlayerStruct.tempplayer[s].InTrade);
                        TradeRelations.clearTempTrade(s);
                        return;
                    }

                    if (WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
                    {
                        SendData.sendPlayerG(PlayerStruct.tempplayer[s].InTrade);
                        SendData.sendTradeClose(PlayerStruct.tempplayer[s].InTrade);
                        SendData.sendInvSlots(PlayerStruct.tempplayer[s].InTrade, PlayerStruct.player[PlayerStruct.tempplayer[s].InTrade].SelectedChar);
                    }

                    TradeRelations.clearTempTrade(PlayerStruct.tempplayer[s].InTrade);
                    TradeRelations.clearTempTrade(s);
                }

                //Sai do Craft
                if (PlayerStruct.tempplayer[s].InCraft)
                {
                    for (int i = 1; i < Globals.Max_Craft; i++)
                    {
                        if (PlayerStruct.craft[s, i].num > 0)
                        {
                            InventoryRelations.giveItem(s, PlayerStruct.craft[s, i].type, PlayerStruct.craft[s, i].num, PlayerStruct.craft[s, i].value, PlayerStruct.craft[s, i].refin, PlayerStruct.craft[s, i].exp);
                        }
                    }
                }

                //Salva o jogador SE PRECISAR
                if (PlayerStruct.tempplayer[s].ingame)
                {
                    Database.Characters.saveCharacter(s, PlayerStruct.player[s].Email, PlayerStruct.player[s].SelectedChar);
                    Database.Banks.saveBank(s);
                    Database.FriendLists.saveFriendList(s);
                }

                //Sai do grupo
                if (PlayerStruct.tempplayer[s].Party > 0)
                {
                    PartyRelations.kickParty(s, s, true);
                }

                //Vamos avisar ao mapa que o jogador saiu
                SendData.sendPlayerLeft(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s);

                //Apagamos array do jogador
                PlayerStruct.clearPlayer(s);
                PlayerStruct.clearTempPlayer(s);

                //Seleção de personagem
                receivedLoadChar(s, 0);
                receivedLoadChar(s, 1);
                receivedLoadChar(s, 2);

                //Continua conectado
                //Ingame no!
                PlayerStruct.tempplayer[s].ingame = false;
                PlayerStruct.tempplayer[s].Logged = true;
            }
            else
            {
                return;
            }
        }
        //*********************************************************************************************
        // receivedRegister / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedRegister(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] register = data.Replace("<5>", "").Split(';');

            if (register.Length != 3) { return; }
            if (register[0].Length > 8) { return; }
            if (register[1].Length > 8) { return; }
            if (register[2].Length > 100) { return; }

            if (Database.Handler.nameIsIllegal(register[0])) { return; }
            if (Database.Handler.nameIsIllegal(register[1])) { return; }
            if (Database.Handler.nameIsIllegal(register[2])) { return; }

            register[2] = register[2].ToLower();

            if (Database.Accounts.accountExists(register[2]))
            {
                SendData.sendToUser(s, String.Format("<5 {0};{1}>e</5>\n", register[2], register[1]));
            }
            else
            {
                Database.Accounts.registerNewAccount(s, register[0], register[1], register[2]);
                Database.Banks.saveBank(s);
                Database.FriendLists.saveFriendList(s);
                SendData.sendToUser(s, String.Format("<5 {0};{1}>c</5>\n", register[2], register[1]));
                WinsockAsync.Log(String.Format("Usuário {0} registrado!", register[0]));
            }
        }
        //*********************************************************************************************
        // receivedNewChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Criação de um novo personagem.
        //*********************************************************************************************
        public static void receivedNewChar(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] newChar = data.Replace("<6>", "").Split(';');

            if (newChar.Length != 11) { return; }
            if (!isNumeric(newChar[1])) { return;}
            if (!isNumeric(newChar[2])) { return; }
            if (!isNumeric(newChar[3])) { return; }
            if (!isNumeric(newChar[4])) { return; }
            if (!isNumeric(newChar[5])) { return; }
            if (!isNumeric(newChar[6])) { return; }
            if (!isNumeric(newChar[7])) { return; }
            if (!isNumeric(newChar[8])) { return; }
            if (!isNumeric(newChar[9])) { return; }
            if (!isNumeric(newChar[10])) { return; }
            if (Database.Handler.nameIsIllegal(newChar[0])) { return; }
            if (newChar[0].Length < 3) { return; }
            if (newChar[0].Length > 15) { return; }
            if (Convert.ToInt32(newChar[1]) > Globals.MaxChars - 1) { return; }
            if (Convert.ToInt32(newChar[2]) > Globals.MaxClasses) { return; }
            if (Convert.ToInt32(newChar[3]) > 12) { return; }
            if (Convert.ToInt32(newChar[4]) > 12) { return; }
            if (Convert.ToInt32(newChar[5]) > 12) { return; }
            if (Convert.ToInt32(newChar[6]) > 12) { return; }
            if (Convert.ToInt32(newChar[7]) > 12) { return; }
            if (Convert.ToInt32(newChar[8]) > 12) { return; }
            if (Convert.ToInt32(newChar[9]) > 500) { return; }
            if (Convert.ToInt32(newChar[10]) > 1) { return; }
            if (Convert.ToInt32(newChar[1]) < 0) { return; }
            if (Convert.ToInt32(newChar[2]) < 0) { return; }
            if (Convert.ToInt32(newChar[3]) < 0) { return; }
            if (Convert.ToInt32(newChar[4]) < 0) { return; }
            if (Convert.ToInt32(newChar[5]) < 0) { return; }
            if (Convert.ToInt32(newChar[6]) < 0) { return; }
            if (Convert.ToInt32(newChar[7]) < 0) { return; }
            if (Convert.ToInt32(newChar[8]) < 0) { return; }
            if (Convert.ToInt32(newChar[9]) < 0) { return; }
            if (Convert.ToInt32(newChar[10]) < 0) { return; }
            if (newChar[0].Contains(' ')) { return; }
            string name = newChar[0].Trim();
            int id = Convert.ToInt32(newChar[1]);
            int classid = Convert.ToInt32(newChar[2]);
            int fire = Convert.ToInt32(newChar[3]);
            int earth = Convert.ToInt32(newChar[4]);
            int water = Convert.ToInt32(newChar[5]);
            int wind = Convert.ToInt32(newChar[6]);
            int dark = Convert.ToInt32(newChar[7]);
            int light = Convert.ToInt32(newChar[8]);
            int hue = Convert.ToInt32(newChar[9]);
            int gender = Convert.ToInt32(newChar[10]);

            if ((!PlayerRelations.isPlayerPremmy(s)) && (classid == 6)) { SendData.sendNStatus(s, "Raça disponível apenas para assinantes."); return; }

            try { if ((Convert.ToInt32(newChar[1]) < 0) || (Convert.ToInt32(newChar[1]) > Globals.MaxChars)) { return; } } catch { return; }
            if (Database.Characters.charExists(newChar[0]))
            {
                SendData.sendToUser(s, String.Format("<6 {0}>e</6>\n", newChar[0]));
            }
            else
            {
                Database.Characters.createNewChar(s, PlayerStruct.player[s].Email, id, name, classid, fire, earth, water, wind, dark, light, hue, gender);
                SendData.sendToUser(s, String.Format("<6 {0}>c</6>\n", newChar[0]));
                //receivedLoadChar(s, 0);
                //receivedLoadChar(s, 1);
                //receivedLoadChar(s, 2);
                //string[] splited = data.Replace("<8>", "").Split(';');
                string data2 = "<8>" + newChar[1];
                receivedIngame(s, data2);
                WinsockAsync.Log(String.Format("Novo personagem {0}" + " Slot" + "{1}", newChar[0], newChar[1]));
            }
        }
        //*********************************************************************************************
        // receivedLoadChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedLoadChar(int s, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, ID) != null)
            {
                return;
            }

            //CÓDIGO
            int intcharId = ID;
            if (Database.Characters.slotExists(PlayerStruct.player[s].Email, ID.ToString()))
            {
                Database.Characters.loadShowChar(s, PlayerStruct.player[s].Email, intcharId);
                string charName = (PlayerStruct.character[s, intcharId].CharacterName);
                int charSprites = (PlayerStruct.character[s, intcharId].Sprites);
                int charClass = (PlayerStruct.character[s, intcharId].ClassId);
                string charSprite = (PlayerStruct.character[s, intcharId].Sprite);
                int charLevel = (PlayerStruct.character[s, intcharId].Level);
                int charExp = (PlayerStruct.character[s, intcharId].Exp);
                int charFire = (PlayerStruct.character[s, intcharId].Fire);
                int charEarth = (PlayerStruct.character[s, intcharId].Earth);
                int charWater = (PlayerStruct.character[s, intcharId].Water);
                int charWind = (PlayerStruct.character[s, intcharId].Wind);
                int charDark = (PlayerStruct.character[s, intcharId].Dark);
                int charLight = (PlayerStruct.character[s, intcharId].Light);
                int charMap = (PlayerStruct.character[s, intcharId].Map);
                byte charX = (PlayerStruct.character[s, intcharId].X);
                byte charY = (PlayerStruct.character[s, intcharId].Y);
                int charHue = (PlayerStruct.character[s, intcharId].Hue);
                int charGender = (PlayerStruct.character[s, intcharId].Gender);

                int characterslot = ID;

                //WinsockAsync.Log(String.Format(charName + charGender + charClass + charLevel));
                SendData.sendToUser(s, String.Format("<7 {0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}>{17}</7>\n", ID, charName, charSprites, charClass, charSprite, charLevel, charExp, charFire, charEarth, charWater, charWind, charDark, charLight, charMap, charX, charY, charHue, charGender));
            }
            else
            {
                WinsockAsync.Log(String.Format("Enviando personagem nulo..."));
                SendData.sendToUser(s, String.Format("<7 {0};e;e;e;e;e;e;e;e;e;e;e;e;e;e;e;e>e</7>\n", ID));
            }
        }
        //*********************************************************************************************
        // receivedDeleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para deletar determinado personagem.
        //*********************************************************************************************
        public static void receivedDeleteChar(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            //Dividimos os dados para análise
            string[] splited = data.Replace("<60>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }

            int charId = Convert.ToInt32(splited[0]);

            if (charId > Globals.MaxChars - 1) { return; }
            if (charId < 0) { return; }

            Database.Characters.deleteChar(s, charId);
            receivedLoadChar(s, charId);
        }
        //*********************************************************************************************
        // receivedIngame / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para entrar no jogo.
        //*********************************************************************************************
        public static void receivedIngame(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            //Verifica se o jogador já não está no jogo
            if (PlayerStruct.tempplayer[s].ingame) { return; }
            
            //Dividimos os dados para análise
            string[] splited = data.Replace("<8>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }

            int charId = Convert.ToInt32(splited[0]);

            if (charId > Globals.MaxChars - 1) { return; }
            if (charId < 0) { return; }

            //Carrega TODOS os dados do personagem
            if (!Database.Characters.loadCompleteChar(s, PlayerStruct.player[s].Email, charId)) { return; }

            //Personagem selecionado(não usamos muito isso agora)
            PlayerStruct.player[s].SelectedChar = Convert.ToInt32(charId);

            //Relacionado a definição de vida para novos e velhos jogadores
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Vitality > PlayerRelations.getPlayerMaxVitality(s))
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Vitality = PlayerRelations.getPlayerMaxVitality(s);
                PlayerStruct.tempplayer[s].Vitality = PlayerRelations.getPlayerMaxVitality(s);
            }
            //else
            //{
                PlayerStruct.tempplayer[s].Vitality = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Vitality;
            //}

            //Relacionado a definição de mana para novos e velhos jogadores
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Spirit > PlayerRelations.getPlayerMaxSpirit(s))
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Spirit = PlayerRelations.getPlayerMaxSpirit(s);
                PlayerStruct.tempplayer[s].Spirit = PlayerRelations.getPlayerMaxSpirit(s);
            }
            //else
           // {
                PlayerStruct.tempplayer[s].Spirit = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Spirit;
           // }

            //Se o dono entrou, dar acesso(mesmo que já tenha, tanto faz)
            if (PlayerStruct.player[s].Email == Globals.MASTER_EMAIL) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access = 10; }

            //Valores gerais de guilda e sua limpeza e auto correção
            int guildnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild;
            

            if (guildnum > 0)
            {
                if (String.IsNullOrEmpty(GuildStruct.guild[guildnum].name)) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = 0; }
                bool find = true;

                for (int i = 1; i < Globals.Max_Guild_Members; i++)
                {
                    if (GuildStruct.guild[guildnum].memberlist[i] == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName)
                    {
                        find = false;
                        break;
                    }
                }

                if (find) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = 0; }
            }

            //O servidor agora sabe que o jogador está dentro do jogo.
            PlayerStruct.tempplayer[s].ingame = true;
            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;

            //Avisamos todos no mapa sobre o novo jogador.
            SendData.sendPlayerDataToMapBut(s, PlayerStruct.player[s].Username, charId);
            
            //Reset pvp values
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer > 0) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer += Loops.TickCount.ElapsedMilliseconds; }
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer > 0) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer += Loops.TickCount.ElapsedMilliseconds; }
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty > 0) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty += Loops.TickCount.ElapsedMilliseconds; PlayerStruct.tempplayer[s].SORE = true; }

            //Send PVP values
            SendData.sendPlayerPvpChangeTimer(s);
            SendData.sendPlayerPvpBanTimer(s);
            SendData.sendPlayerPvpSoreTimer(s);

            //PShop Slots
            SendData.sendPShopSlots(s, s);

            //Death
            if (PlayerStruct.tempplayer[s].Vitality <= 0)
            {
                PlayerStruct.tempplayer[s].isDead = true;
                SendData.sendPlayerDeathToMap(s);
            }

            //Enviamos os dados do jogador e dos que estão no mapa para que ele os veja.
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    SendData.sendPlayerDataTo(s, i, PlayerStruct.player[i].Username, charId);
                    SendData.sendGuildTo(s, i);
                    SendData.sendPlayerShoppingTo(s, i);
                    if (PlayerStruct.tempplayer[i].Stunned) { SendData.sendStun(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 1, i, 1); }
                    if (PlayerStruct.tempplayer[i].Sleeping) { SendData.sendSleep(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 1, i, 1); }
                    if (PlayerStruct.tempplayer[i].isDead) { SendData.sendPlayerDeathTo(s, i); }
                    //SendData.sendPlayerMoveSpeedTo(s, i);
                }
            }

            for (int i = 0; i <= Globals.Max_Chests - 1; i++)
            {
                if (MapStruct.chestpoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                    {
                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                    }
                }
            }

            //Ele já recebeu seus dados, agora ele pode processar as coisas (lol)
            SendData.sendPlayerFriends(s);
            SendData.sendMapGuildTo(s);
            SendData.sendPlayerSkills(s);
            SendData.sendPlayerHotkeys(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendMapNpcsTo(s);
            SendData.sendMapItems(s);
            SendData.sendPlayerVitalityToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Vitality);
            SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);
            SendData.sendGuildToMapBut(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s);
            SendData.sendCompleteGuild(s);
            SendData.sendPlayerG(s);
            SendData.sendPlayerC(s);
            SendData.sendAllQuests(s);
            SendData.sendProfs(s);
            SendData.sendPlayerSkillPoints(s);
            SendData.sendPlayerExtraSpiritToMap(s);
            SendData.sendPlayerExtraVitalityToMap(s);
            SendData.sendPlayerPvpToMap(s);
            SendData.sendPlayerSoreToMap(s);
            SendData.sendMsgToPlayer(s, Globals.MOTD, Globals.ColorGreen, Globals.Msg_Type_Server);
            //SendData.sendPlayerMoveSpeedToMapBut(s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s);
        }
        //*********************************************************************************************
        // receivedMove / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de movimento.
        //*********************************************************************************************
        public static void receivedMove(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if ((PlayerStruct.tempplayer[s].InBank) || (PlayerStruct.tempplayer[s].InCraft) || (PlayerStruct.tempplayer[s].InTrade > 0) || (PlayerStruct.tempplayer[s].InShop > 0) || (PlayerStruct.tempplayer[s].Stunned) || (PlayerStruct.tempplayer[s].Sleeping)) { return; }
            string[] splited = data.Replace("<11>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 10) || (Convert.ToInt32(splited[0]) < 0)) { return; }

            byte Dir = Convert.ToByte(splited[0]);

            if ((MovementRelations.canPlayerMove(s, Dir) == true) && (PlayerStruct.tempplayer[s].MoveTimer < Loops.TickCount.ElapsedMilliseconds))
            {
                MovementRelations.playerMove(s, Dir);
                PlayerStruct.tempplayer[s].MoveTimer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt64((((8 + (4 - PlayerStruct.tempplayer[s].movespeed) - PlayerStruct.tempplayer[s].movespeed) * 64) - 25)); //25ms de tolerância
            }
            else
            {
                SendData.sendPlayerXY(s);
            }
        }
        //*********************************************************************************************
        // receivedMessage / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de interação com o chat.
        //*********************************************************************************************
        public static void receivedMessage(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<12>", "").Split(';');
            if (splited.Length != 1) { return; }

            string charMsg = splited[0].Trim();

            if (charMsg.Length > 150) { return; }

            string msg = "";

            try
            {
                if (charMsg.StartsWith("/w "))
                {
                    charMsg = charMsg.Remove(0, 3);
                    string[] playername = charMsg.Split(' ');
                    for (int i = 0; i <= Globals.Player_Highs; i++)
                    {
                        if (i != s)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == playername[0]))
                            {
                                charMsg = charMsg.Remove(0, playername[0].Length);
                                msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
                                SendData.sendMsgToPlayer(i, "[" + lang.private_msg + "] " + msg, Globals.ColorYellow, Globals.Msg_Type_Community);
                                SendData.sendMsgToPlayer(s, "[" + lang.private_msg + "] " + msg, Globals.ColorYellow, Globals.Msg_Type_Community);
                                break;
                            }
                        }
                    }

                    return;
                }

                if (charMsg.StartsWith("/g "))
                {
                    charMsg = charMsg.Remove(0, 3);
                    msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
                    SendData.sendMsgToGuild(s, "[" + lang.guild_msg + "] " + msg, Globals.ColorPink, Globals.Msg_Type_Guild);
                    return;
                }

                if (charMsg.StartsWith("/p "))
                {
                    if (PlayerStruct.tempplayer[s].Party > 0)
                    {
                        charMsg = charMsg.Remove(0, 3);
                        msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
                        SendData.sendMsgToParty(PlayerStruct.tempplayer[s].Party, "[" + lang.party_msg + "] " + msg, Globals.ColorGreen, Globals.Msg_Type_Community);
                        return;
                    }
                }

                if (charMsg.StartsWith("/t "))
                {
                    charMsg = charMsg.Remove(0, 3);
                    msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
                    SendData.sendMsgToAll("[" + lang.global_msg + "] " + msg, Globals.ColorBrown, Globals.Msg_Type_Global);
                    return;
                }

                if (charMsg.StartsWith("/a "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 3);
                        msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
                        SendData.sendMsgToAll("[" + lang.admin_msg + "] " + msg, Globals.ColorGreen, Globals.Msg_Type_Global);
                        return;
                    }
                }


                if (charMsg.StartsWith("/item "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 6);
                        string[] itemdata = charMsg.Split(',');
                        int itemtype = Convert.ToInt32(itemdata[0]);
                        int itemnum = Convert.ToInt32(itemdata[1]);
                        int itemvalue = Convert.ToInt32(itemdata[2]);
                        int itemrefin = Convert.ToInt32(itemdata[3]);
                        int itemexp = Convert.ToInt32(itemdata[4]);

                        InventoryRelations.giveItem(s, itemtype, itemnum, itemvalue, itemrefin, itemexp);
                        return;
                    }
                }

                if (charMsg.StartsWith("/come "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 6);
                        for (int i = 0; i <= Globals.Player_Highs; i++)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == charMsg))
                            {
                                MovementRelations.playerWarp(i, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
                                break;
                            }
                        }
                        return;
                    }
                }

                if (charMsg.StartsWith("/goto "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 6);
                        for (int i = 0; i <= Globals.Player_Highs; i++)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == charMsg))
                            {
                                MovementRelations.playerWarp(s, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y);
                                break;
                            }
                        }
                        return;
                    }
                }

                if (charMsg.StartsWith("/resetplayer "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 13);
                        for (int i = 0; i <= Globals.Player_Highs; i++)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == charMsg))
                            {
                                PlayerRelations.resetPlayerStatus(i);
                                break;
                            }
                        }
                        return;
                    }
                }

                if (charMsg.StartsWith("/saveplayers"))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        for (int i = 0; i <= Globals.Player_Highs; i++)
                        {
                            Database.Characters.saveCharacter(i, PlayerStruct.player[i].Email, PlayerStruct.player[i].SelectedChar);
                            SendData.sendMsgToPlayer(s, lang.players_has_been_saved_with_success, Globals.ColorYellow, Globals.Msg_Type_Community);
                        }
                        return;
                    }
                }

                if (charMsg.StartsWith("/giveexp "))
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access > 2)
                    {
                        charMsg = charMsg.Remove(0, 9);
                        string playername = charMsg.Split(' ')[0];
                        int exp = Convert.ToInt32(charMsg.Split(' ')[1]);
                        for (int i = 0; i <= Globals.Player_Highs; i++)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == playername))
                            {
                                PlayerRelations.givePlayerExp(i, exp);
                                break;
                            }
                        }
                        return;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Erro ao processar comando de chat.");
                return;
            }
            msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
            SendData.sendMsgToMap(s, "[" + lang.map_msg + "] " +  msg, Globals.ColorWhite, Globals.Msg_Type_Map);
        }
        //*********************************************************************************************
        // LatencyCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Ping
        //*********************************************************************************************
        static void receivedLatency(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            SendData.sendToUser(s, "<13>'e'</13>\n");
        }
        //*********************************************************************************************
        // MapCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedMapData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newMap = data.Replace("<15>", "").Split(';');
            
            //variáveis simples
            int map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;

            int part = Convert.ToInt32(newMap[0]);
            int startx = Convert.ToInt32(newMap[1]);
            int startxend = Convert.ToInt32(newMap[2]);
            int starty = Convert.ToInt32(newMap[3]);
            int startyend = Convert.ToInt32(newMap[4]);
            MapStruct.map[Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)].name = newMap[5];
            
            //nosso leitor
            int reader = 6;

            //dados do mapa em geral
            MapStruct.map[Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)].max_height = "14";
            MapStruct.map[Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)].max_width = "19";

            //organizamos todos os dados dos tiles
            for (int x = startx; x <= startxend; x++)
                for (int y = starty; y <= startyend; y++)
                {
                    {
                        MapStruct.tile[Convert.ToInt32(map), x, y].Event_Id = Convert.ToInt32(newMap[reader]);
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].Data1 = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].Data2 = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].Data3 = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].Data4 = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].DownBlock = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].LeftBlock = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].RightBlock = newMap[reader];
                        reader += 1;
                        MapStruct.tile[Convert.ToInt32(map), x, y].UpBlock = newMap[reader];
                        reader += 1;
                    }

                }

            //salvamos o mapa depois de juntar e organizar seus dados
            if (part == 4)
            {
                //Se já enviou todas as partes do mapa, salvar.
                Database.Maps.save(map);

                //Recarregar o mapa
                Database.Maps.load(map);

                //tudo certo
                Console.WriteLine("O mapa " + map + " foi atualizado com sucesso.");
            }

        }
        //*********************************************************************************************
        // UseItemCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de interação com um item do inventário.
        //*********************************************************************************************
        static void receivedUseItem(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<16>", "").Split(';');
            
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return;}

            int itemUse = Convert.ToInt32(splited[0]);

            if (itemUse > Globals.MaxInvSlot - 1) { return; }
            if (itemUse <= 0) { return; }

            string item = PlayerStruct.invslot[s, itemUse].item;
            string[] splititem = item.Split(',');

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if ((itemType == Globals.ArmorType) || (itemType == Globals.WeaponType))
            {
                if (isNumeric(ItemStruct.item[itemNum].note))
                {
                    if (Convert.ToInt32(ItemStruct.item[itemNum].note) > PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level)
                    {
                        SendData.sendMsgToPlayer(s, lang.no_level_to_use_this_item, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }
                }
            }

            string equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;
            string[] equipdata = equipment.Split(',');

            string Helmet = equipdata[0].Split(';')[0];
            string Armor = equipdata[1].Split(';')[0];
            string Weapon = equipdata[2].Split(';')[0];
            string Shield = equipdata[3].Split(';')[0];
            string Pet = equipdata[4].Split(';')[0];

            string HelmetRefin = equipdata[0].Split(';')[1];
            string ArmorRefin = equipdata[1].Split(';')[1];
            string WeaponRefin = equipdata[2].Split(';')[1];
            string ShieldRefin = equipdata[3].Split(';')[1];
            string PetRefin = equipdata[4].Split(';')[1];
            string PetExp = equipdata[4].Split(';')[2];

            int map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;

            bool equipped = false;

            if ((itemType == 0) || (itemType == 1))
            {
                if (ItemStruct.itemextra[itemNum].type == 1)
                {
                    if (Convert.ToInt32(Pet) > 0)
                    {
                        if ((Convert.ToInt32(Pet) == itemNum) && (Convert.ToInt32(PetRefin) == itemRefin) && (Convert.ToInt32(PetExp) == itemExp))
                        {
                            itemValue += 1;
                        }
                        if (!InventoryRelations.giveItem(s, itemType, Convert.ToInt32(Pet), 1, Convert.ToInt32(PetRefin), Convert.ToInt32(PetExp)))
                        {
                            return;
                        }
                    }
                    Pet = itemNum.ToString();
                    PetRefin = itemRefin.ToString();
                    PetExp = itemExp.ToString();
                    equipped = true;
                }
                if (itemNum == 58)
                {
                   if (SkillRelations.giveSpell(s, 28))
                   {
                       SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                       InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                       SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                       SendData.sendPlayerSkills(s);
                       return;
                   }
                }
                if (itemNum == 59)
                {
                    if (SkillRelations.giveSpell(s, 29))
                    {
                        SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        SendData.sendPlayerSkills(s);
                        return;
                    }
                }
                if (itemNum == 60)
                {
                    if (SkillRelations.giveSpell(s, 30))
                    {
                        SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        SendData.sendPlayerSkills(s);
                        return;
                    }
                }
                if (itemNum == 61)
                {
                    if (SkillRelations.giveSpell(s, 31))
                    {
                        SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        SendData.sendPlayerSkills(s);
                        return;
                    }
                }
                if (itemNum == 66)
                {
                    if (SkillRelations.giveSpell(s, 32))
                    {
                        SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        SendData.sendPlayerSkills(s);
                        return;
                    }
                }
                if (itemNum == 67)
                {
                    if (SkillRelations.giveSpell(s, 34))
                    {
                        SendData.sendMsgToPlayer(s, lang.you_have_learned_a_new_spell, Globals.ColorYellow, Globals.Msg_Type_Server);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        SendData.sendPlayerSkills(s);
                        return;
                    }
                }
                if (itemNum == 69)
                {
                    MovementRelations.playerWarp(s, Globals.InitialMap, Globals.InitialX, Globals.InitialY);
                    InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                    SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                }
                if (itemNum == 73)
                {
                    int target = PlayerStruct.tempplayer[s].target;
                    int targettype = PlayerStruct.tempplayer[s].targettype;
                    if (targettype == Globals.Target_Npc) { return; }
                    //Check in
                    if (!(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map == map) || !(target != s))
                    {
                        PlayerStruct.tempplayer[s].preparingskill = 0;
                        PlayerStruct.tempplayer[s].preparingskillslot = 0;
                        PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(1, s);
                        SendData.sendMsgToPlayer(s, lang.use_item_fail, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }
                    if (PlayerStruct.tempplayer[target].Vitality <= 0)
                    {
                        PlayerStruct.tempplayer[target].isDead = false;
                        SendData.sendPlayerDeathToMap(target);
                        PlayerStruct.tempplayer[target].Vitality = 10;
                        SendData.sendPlayerVitalityToMap(map, target, 10);
                        SendData.sendAnimation(map, Globals.Target_Player, target, 38);
                    }
                    InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                    SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                }
                if (itemNum == 74)
                {
                    if (PlayerStruct.tempplayer[s].SORE)
                    {
                        PlayerStruct.tempplayer[s].SORE = false;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty = 0;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP = false;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer = 0;
                        SendData.sendPlayerPvpToMap(s);
                        SendData.sendPlayerSoreToMap(s);
                        SendData.sendPlayerPvpBanTimer(s);
                        SendData.sendAnimation(map, Globals.Target_Player, s, 108);
                        InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                    }
                }
                if (ItemStruct.item[itemNum].damage_type == 3)
                {
                    if (PlayerStruct.tempplayer[s].Vitality >= PlayerRelations.getPlayerMaxVitality(s)) { return; }
                    if (PlayerStruct.tempplayer[s].isDead) { return; }
                    PlayerLogic.HealPlayer(s, (PlayerRelations.getPlayerMaxVitality(s) / 100) * Convert.ToInt32(ItemStruct.item[itemNum].damage_formula));
                    InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                    SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                    return;
                }
                if (ItemStruct.item[itemNum].damage_type == 4)
                {
                    if (PlayerStruct.tempplayer[s].Spirit >= PlayerRelations.getPlayerMaxSpirit(s)) { return; }
                    if (PlayerStruct.tempplayer[s].isDead) { return; }
                    PlayerLogic.SpiritPlayer(s, (PlayerRelations.getPlayerMaxSpirit(s) / 100) * Convert.ToInt32(ItemStruct.item[itemNum].damage_formula));
                    InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
                    SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                    return;
                }
            }
            if (itemType == 2)
            {
                if (Convert.ToInt32(Weapon) > 0)
                {
                    if ((Convert.ToInt32(Weapon) == itemNum) && (Convert.ToInt32(WeaponRefin) == itemRefin))
                    {
                        itemValue += 1;
                    }
                    if (!InventoryRelations.giveItem(s, itemType, Convert.ToInt32(Weapon), 1, Convert.ToInt32(WeaponRefin), Globals.NullExp))
                    {
                        return;
                    }
                }
                Weapon = itemNum.ToString();
                WeaponRefin = itemRefin.ToString();
                equipped = true;
            }
            if (itemType == 3)
            {
                if (ArmorStruct.armor[itemNum].etype_id == 1)
                {
                    if (Convert.ToInt32(Shield) > 0)
                    {
                        if ((Convert.ToInt32(Shield) == itemNum) && (Convert.ToInt32(ShieldRefin) == itemRefin))
                        {
                            itemValue += 1;
                        }
                        if (!InventoryRelations.giveItem(s, itemType, Convert.ToInt32(Shield), 1, Convert.ToInt32(ShieldRefin), Globals.NullExp))
                        {
                            return;
                        }
                    }
                    Shield = itemNum.ToString();
                    ShieldRefin = itemRefin.ToString();
                    equipped = true;
                }
                if (ArmorStruct.armor[itemNum].etype_id == 2)
                {
                    if (Convert.ToInt32(Helmet) > 0)
                    {
                        if ((Convert.ToInt32(Helmet) == itemNum) && (Convert.ToInt32(HelmetRefin) == itemRefin))
                        {
                            itemValue += 1;
                        }
                        if (!InventoryRelations.giveItem(s, itemType, Convert.ToInt32(Helmet), 1, Convert.ToInt32(HelmetRefin), Globals.NullExp))
                        {
                            return;
                        }
                    }
                    Helmet = itemNum.ToString();
                    HelmetRefin = itemRefin.ToString();
                    equipped = true;
                }
                if (ArmorStruct.armor[itemNum].etype_id == 3)
                {
                    if (Convert.ToInt32(Armor) > 0)
                    {
                        if ((Convert.ToInt32(Armor) == itemNum) && (Convert.ToInt32(ArmorRefin) == itemRefin))
                        {
                            itemValue += 1;
                        }
                        if (!InventoryRelations.giveItem(s, itemType, Convert.ToInt32(Armor), 1, Convert.ToInt32(ArmorRefin), Globals.NullExp))
                        {
                            return;
                        }
                    }
                    Armor = itemNum.ToString();
                    ArmorRefin = itemRefin.ToString();
                    equipped = true;
                }
            }

            if (equipped == true)
            {
                if (itemValue > 1)
                {
                    PlayerStruct.invslot[s, itemUse].item = itemType + "," + itemNum + "," + (itemValue - 1) + "," + itemRefin + "," + itemExp;
                }
                else
                {
                    PlayerStruct.invslot[s, itemUse].item = Globals.NullItem;
                }
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment = Helmet + ";" + HelmetRefin +  "," + Armor + ";" + ArmorRefin + "," + Weapon + ";" + WeaponRefin + "," + Shield + ";" + ShieldRefin + "," + Pet + ";" + PetRefin + ";" + PetExp;
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                SendData.sendPlayerEquipmentToMap(s);
                return;
            }
            
        }
        //*********************************************************************************************
        // EquipItemCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de interação com um item equipado.
        //*********************************************************************************************
        static void receivedEquipItem(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<17>", "").Split(';');
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            int itemUse = Convert.ToInt32(splited[0]);
            if (itemUse > 4) { return; }
            if (itemUse < 0) { return; }
            string equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;
            string[] equipdata = equipment.Split(',');
            string[] equip0 = equipdata[0].Split(';');
            string[] equip1 = equipdata[1].Split(';');
            string[] equip2 = equipdata[2].Split(';');
            string[] equip3 = equipdata[3].Split(';');
            string[] equip4 = equipdata[4].Split(';');

            switch (itemUse)
            {
                case 0:
                    if (!InventoryRelations.giveItem(s, 3, Convert.ToInt32(equip0[0]), 1, Convert.ToInt32(equip0[1]), Globals.NullExp)) { return; }
                    equipdata[0] = "0;0";
                    break;
                case 1:
                    if (!InventoryRelations.giveItem(s, 3, Convert.ToInt32(equip1[0]), 1, Convert.ToInt32(equip1[1]), Globals.NullExp)) { return; }
                    equipdata[1] = "0;0";
                    break;
                case 2:
                    if (!InventoryRelations.giveItem(s, 2, Convert.ToInt32(equip2[0]), 1, Convert.ToInt32(equip2[1]), Globals.NullExp)) { return; }
                    equipdata[2] = "0;0";
                    break;
                case 3:
                    if (!InventoryRelations.giveItem(s, 3, Convert.ToInt32(equip3[0]), 1, Convert.ToInt32(equip3[1]), Globals.NullExp)) { return; }
                    equipdata[3] = "0;0";
                    break;
                case 4:
                    if (!InventoryRelations.giveItem(s, 1, Convert.ToInt32(equip4[0]), 1, Convert.ToInt32(equip4[1]), Convert.ToInt32(equip4[2]))) { return; }
                    equipdata[4] = "0;0;0";
                    break;
            }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment = equipdata[0] + "," + equipdata[1] + "," + equipdata[2] + "," + equipdata[3] + "," + equipdata[4];
            SendData.sendPlayerEquipmentToMap(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            
            //string msg = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + ": " + charMsg;
            //SendData.sendMsg(s, msg);
        }
        //*********************************************************************************************
        // AttackCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para efetuar um ataque.
        //*********************************************************************************************
        static void receivedAttack(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].AttackTimer > Loops.TickCount.ElapsedMilliseconds ) { return; }
            if (PlayerStruct.tempplayer[s].preparingskill > 0) { return; }

            string[] splited = data.Replace("<18>", "").Split(';');
            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }
            int Target = Convert.ToInt32(splited[0]);
            int TargetType = Convert.ToInt32(splited[1]);

            if (Target > 255) { return; }
            if (TargetType > 2) { return; }
            if (Target < 0) { return; }
            if (TargetType < 0) { return; }

            SendData.sendplayerAttack(s);
            PlayerStruct.tempplayer[s].AttackTimer = Loops.TickCount.ElapsedMilliseconds + 1000;

            if (TargetType == 1)
            {
                //atacar jogador
                if (CombatRelations.canPlayerAttackPlayer(s, Target) )
                {
                    CombatRelations.playerAttackPlayer(s, Target);
                    return;
                }
            }

            if (TargetType == 2)
            {
                //atacar npc
                if (CombatRelations.canPlayerAttackNpc(s, Target))
                {
                    CombatRelations.playerAttackNpc(s, Target);
                    return;
                }
            }

            for (int i = 1; i < Globals.Max_WorkPoints; i++)
            {
                if (MapStruct.workpoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    switch (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir)
                    {
                        case 8:
                            if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y - 1 == MapStruct.workpoint[i].y) &&(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == MapStruct.workpoint[i].x))
                            {
                                MapRelations.playerAttackWorkPoint(s, i);
                                return;
                            }
                            break;
                        case 2:
                            if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y + 1 == MapStruct.workpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == MapStruct.workpoint[i].x))
                            {
                                MapRelations.playerAttackWorkPoint(s, i);
                                return;
                            }
                            break;
                        case 4:
                            if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == MapStruct.workpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X - 1 == MapStruct.workpoint[i].x))
                            {
                                MapRelations.playerAttackWorkPoint(s, i);
                                return;
                            }
                            break;
                        case 6:
                            if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == MapStruct.workpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X + 1 == MapStruct.workpoint[i].x))
                            {
                                MapRelations.playerAttackWorkPoint(s, i);
                                return;
                            }
                            break;
                        default:
                            WinsockAsync.Log(String.Format("Direção nula"));
                            break;
                    }
                }

            }
        }
        //*********************************************************************************************
        // receivedOpenChest / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para tentar abrir um baú.
        //*********************************************************************************************
        static void receivedOpenChest(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Chests; i++)
            {
                if (MapStruct.chestpoint[i].map > 0)
                {
                    if (MapStruct.chestpoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                    {
                        switch (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir)
                        {
                            case 8:
                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y - 1 == MapStruct.chestpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == MapStruct.chestpoint[i].x))
                                {
                                    if (!PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                                    {
                                        if (MapStruct.chestpoint[i].is_random)
                                        {
                                        }
                                        else
                                        {
                                            if (MapStruct.chestpoint[i].reward_count > 0)
                                            {
                                                if (InventoryRelations.getNumOfInvFreeSlots(s) < MapStruct.chestpoint[i].reward_count) { SendData.sendMsgToPlayer(s, lang.you_dont_have_inventory_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }
                                                for (int r = 1; r <= MapStruct.chestpoint[i].reward_count; r++)
                                                {
                                                    string[] dataitem = MapStruct.chestpoint[i].reward[r].Split(',');
                                                    InventoryRelations.giveItem(s, Convert.ToInt32(dataitem[0]), Convert.ToInt32(dataitem[1]), Convert.ToInt32(dataitem[2]), Convert.ToInt32(dataitem[3]), Globals.NullExp);
                                                }
                                            }
                                        }
                                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i] = true;
                                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                                        SendData.sendActionMsg(s, lang.finded_a_item, Globals.ColorYellow, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                                        SendData.sendMsgToPlayer(s, lang.you_have_obtained_new_items, Globals.ColorYellow, Globals.Msg_Type_Server);
                                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                                        return;
                                    }
                                }
                                break;
                            case 2:
                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y + 1 == MapStruct.chestpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == MapStruct.chestpoint[i].x))
                                {
                                    if (!PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                                    {
                                        if (MapStruct.chestpoint[i].reward_count > 0)
                                        {
                                            if (InventoryRelations.getNumOfInvFreeSlots(s) < MapStruct.chestpoint[i].reward_count) { SendData.sendMsgToPlayer(s, lang.you_dont_have_inventory_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }
                                            for (int r = 1; r <= MapStruct.chestpoint[i].reward_count; r++)
                                            {
                                                string[] dataitem = MapStruct.chestpoint[i].reward[r].Split(',');
                                                InventoryRelations.giveItem(s, Convert.ToInt32(dataitem[0]), Convert.ToInt32(dataitem[1]), Convert.ToInt32(dataitem[2]), Convert.ToInt32(dataitem[3]), Globals.NullExp);
                                            }
                                        }
                                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i] = true;
                                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                                        SendData.sendActionMsg(s, lang.finded_a_item, Globals.ColorYellow, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                                        SendData.sendMsgToPlayer(s, lang.you_have_obtained_new_items, Globals.ColorYellow, Globals.Msg_Type_Server);
                                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                                        return;
                                    }
                                }
                                break;
                            case 4:
                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == MapStruct.chestpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X - 1 == MapStruct.chestpoint[i].x))
                                {
                                    if (!PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                                    {
                                        if (MapStruct.chestpoint[i].reward_count > 0)
                                        {
                                            if (InventoryRelations.getNumOfInvFreeSlots(s) < MapStruct.chestpoint[i].reward_count) { SendData.sendMsgToPlayer(s, lang.you_dont_have_inventory_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }
                                            for (int r = 1; r <= MapStruct.chestpoint[i].reward_count; r++)
                                            {
                                                string[] dataitem = MapStruct.chestpoint[i].reward[r].Split(',');
                                                InventoryRelations.giveItem(s, Convert.ToInt32(dataitem[0]), Convert.ToInt32(dataitem[1]), Convert.ToInt32(dataitem[2]), Convert.ToInt32(dataitem[3]), Globals.NullExp);
                                            }
                                        }
                                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i] = true;
                                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                                        SendData.sendActionMsg(s, lang.finded_a_item, Globals.ColorYellow, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                                        SendData.sendMsgToPlayer(s, lang.you_have_obtained_new_items, Globals.ColorYellow, Globals.Msg_Type_Server);
                                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                                        return;
                                    }
                                }
                                break;
                            case 6:
                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == MapStruct.chestpoint[i].y) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X + 1 == MapStruct.chestpoint[i].x))
                                {
                                    if (!PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                                    {
                                        if (MapStruct.chestpoint[i].reward_count > 0)
                                        {
                                            if (InventoryRelations.getNumOfInvFreeSlots(s) < MapStruct.chestpoint[i].reward_count) { SendData.sendMsgToPlayer(s, lang.you_dont_have_inventory_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }
                                            for (int r = 1; r <= MapStruct.chestpoint[i].reward_count; r++)
                                            {
                                                string[] dataitem = MapStruct.chestpoint[i].reward[r].Split(',');
                                                InventoryRelations.giveItem(s, Convert.ToInt32(dataitem[0]), Convert.ToInt32(dataitem[1]), Convert.ToInt32(dataitem[2]), Convert.ToInt32(dataitem[3]), Globals.NullExp);
                                            }
                                        }
                                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i] = true;
                                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                                        SendData.sendActionMsg(s, lang.finded_a_item, Globals.ColorYellow, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                                        SendData.sendMsgToPlayer(s, lang.you_have_obtained_new_items, Globals.ColorYellow, Globals.Msg_Type_Server);
                                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                                        return;
                                    }
                                }
                                break;
                            default:
                                WinsockAsync.Log(String.Format(lang.null_direction));
                                break;
                        }
                    }
                }
            }
        }
        //*********************************************************************************************
        // DirCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para mudar a direção em que o personagem está olhando.
        //*********************************************************************************************
        static void receivedDir(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<19>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 10) || (Convert.ToInt32(splited[0]) < 0)) { return; }
            
            byte Dir = Convert.ToByte(splited[0]);

            switch (Dir)
            {
                case 8:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirUp;
                    break;
                case 2:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirDown;
                    break;
                case 4:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirLeft;
                    break;
                case 6:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirRight;
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }

            SendData.sendPlayerDir(s);
            SendData.sendPlayerDir(s, 1);
        }
        //*********************************************************************************************
        // receivedOutCollector / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedOutCollector(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int mapnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int guildnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild;

            if (guildnum <= 0) { return; }

            if (MapStruct.map[mapnum].guildnum <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.this_map_dont_have_collector_to_collect, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if ((MapStruct.map[mapnum].guildmember != "") && (MapStruct.map[mapnum].guildmember != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName))
            {
                SendData.sendMsgToPlayer(s, lang.you_is_not_the_collector_master, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }


            //Entregar o dinheiro ao jogador
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold += MapStruct.map[mapnum].guildgold;

            //Retirar guilda do mapa
            MapStruct.map[mapnum].guildnum = 0;
            MapStruct.map[mapnum].guildmember = "";
            MapStruct.map[mapnum].guildgold = 0;

            //Retirar o coletor
            for (int i2 = 0; i2 <= MapStruct.tempmap[mapnum].NpcCount; i2++)
            {
                if (NpcStruct.npc[mapnum, i2].Name == "Coletor de Guilda")
                {
                    //Avisar os clientes para apagarem o coletor do mapa
                    SendData.sendNpcLeft(mapnum, i2);

                    //Limpar dados sobre o coletor
                    NpcStruct.clearTempNpc(mapnum, i2);
                    break;
                }
            }

            //Atualizaro número de npc's no mapa
            MapStruct.tempmap[mapnum].NpcCount = MapStruct.getMapNpcCount(mapnum);

            //Enviar dados para todos.
            SendData.sendMapGuildToMap(mapnum);
            SendData.sendMsgToGuild(guildnum, lang.the_collector_of + " " + MapStruct.map[mapnum].name + " " + lang.has_been_collected, Globals.ColorYellow, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // receivedCollector / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedCollector(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int guildnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild;

            if (guildnum <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.only_for_guild_members, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int guildlevel = GuildStruct.guild[guildnum].level;
            int price = guildlevel * 1000;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold < price)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_gold_to_create_collector, Globals.ColorRed, Globals.Msg_Type_Server);
               // return;
            }

            int mapnum =  PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;

            if (MapStruct.map[mapnum].guildnum > 0)
            {
                SendData.sendMsgToPlayer(s, lang.a_collector_already_exist_in_this_map, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            
            int i2 = MapStruct.getMapNpcSlot(mapnum);
            if (i2 <= 1)
            {
                SendData.sendMsgToPlayer(s, lang.this_map_cannot_have_collectors, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Definir a nova guilda do mapa
            MapStruct.map[mapnum].guildnum = guildnum;
            MapStruct.map[mapnum].guildmember = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName;

            //Criar o coletor
            NpcStruct.npc[mapnum, i2].Name = lang.guild_collector;
            NpcStruct.npc[mapnum, i2].Map = mapnum;
            NpcStruct.npc[mapnum, i2].X = NpcStruct.npc[mapnum, 1].X;
            NpcStruct.npc[mapnum, i2].Y = NpcStruct.npc[mapnum, 1].Y;
            NpcStruct.npc[mapnum, i2].Vitality = (GuildStruct.guild[guildnum].level * 1000) + 1000;
            NpcStruct.npc[mapnum, i2].Spirit = (GuildStruct.guild[guildnum].level * 200) + 200;
            NpcStruct.tempnpc[mapnum, i2].Target = 0;
            NpcStruct.tempnpc[mapnum, i2].X = NpcStruct.npc[mapnum, i2].X;
            NpcStruct.tempnpc[mapnum, i2].Y = NpcStruct.npc[mapnum, i2].Y;
            NpcStruct.tempnpc[mapnum, i2].curTargetX = NpcStruct.npc[mapnum, i2].X;
            NpcStruct.tempnpc[mapnum, i2].curTargetY = NpcStruct.npc[mapnum, i2].Y;
            NpcStruct.tempnpc[mapnum, i2].Vitality = NpcStruct.npc[mapnum, i2].Vitality;
            NpcStruct.npc[mapnum, i2].Attack =  (GuildStruct.guild[guildnum].level * 34) + 34;
            NpcStruct.npc[mapnum, i2].Defense = (GuildStruct.guild[guildnum].level * 22) + 22; ;
            NpcStruct.npc[mapnum, i2].Agility = (GuildStruct.guild[guildnum].level * 4) + 4; ;
            NpcStruct.npc[mapnum, i2].MagicDefense = (GuildStruct.guild[guildnum].level * 26) + 26;
            NpcStruct.npc[mapnum, i2].MagicAttack = (GuildStruct.guild[guildnum].level * 48) + 48;
            NpcStruct.npc[mapnum, i2].Luck = (GuildStruct.guild[guildnum].level * 4) + 4;
            NpcStruct.npc[mapnum, i2].Sprite = "$sprite (84)";
            NpcStruct.npc[mapnum, i2].s = 0;
            NpcStruct.npc[mapnum, i2].Type = 1;
            NpcStruct.npc[mapnum, i2].Range = 1;
            NpcStruct.npc[mapnum, i2].Animation = 2;
            NpcStruct.npc[mapnum, i2].SpeedMove = 256;
            NpcStruct.tempnpc[mapnum, i2].movespeed = NpcStruct.npc[mapnum, i2].SpeedMove / 64;
            NpcStruct.npc[mapnum, i2].Respawn = 0;
            NpcStruct.npc[mapnum, i2].Exp = 0;
            NpcStruct.npc[mapnum, i2].Gold = 0;
            NpcStruct.tempnpc[mapnum, i2].guildnum = guildnum;
            NpcStruct.tempnpc[mapnum, i2].prev_move = new NpcStruct.Point[7];

            //Atualizaro número de npc's no mapa
            MapStruct.tempmap[mapnum].NpcCount = MapStruct.getMapNpcCount(mapnum);
            
            //Enviar dados para todos.
            SendData.sendMapGuildToMap(mapnum);
            SendData.sendNpcToMap(mapnum, i2);
            SendData.sendMsgToGuild(guildnum, lang.a_collector_has_been_created_in + " " + MapStruct.map[mapnum].name + ".", Globals.ColorYellow, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // PickItemCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para obter um item no mapa.
        //*********************************************************************************************
        static void receivedPickItem(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int map = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            int playerx = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
            int playery = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);

            for (int i = 0; i <= MapStruct.getMapItemMaxs(map); i++)
            {
                if ((MapStruct.mapitem[map, i].X == playerx) && (MapStruct.mapitem[map, i].Y == playery))
                {
                    if (InventoryRelations.giveItem(s, MapStruct.mapitem[map, i].ItemType, MapStruct.mapitem[map, i].ItemNum, MapStruct.mapitem[map, i].Value, MapStruct.mapitem[map, i].Refin, MapStruct.mapitem[map, i].Exp) == true)
                    {
                        if (MapStruct.mapitem[map, i].ItemType == 1)
                        {
                            SendData.sendActionMsg(s, ItemStruct.item[MapStruct.mapitem[map, i].ItemNum].name + " x" + MapStruct.mapitem[map, i].Value, Globals.ColorWhite, playerx, playery, 1, 0, map);
                        }
                        if (MapStruct.mapitem[map, i].ItemType == 2)
                        {
                            SendData.sendActionMsg(s, WeaponStruct.weapon[MapStruct.mapitem[map, i].ItemNum].name + " x" + MapStruct.mapitem[map, i].Value, Globals.ColorWhite, playerx, playery, 1, 0, map);
                        }
                        if (MapStruct.mapitem[map, i].ItemType == 3)
                        {
                            SendData.sendActionMsg(s, ArmorStruct.armor[MapStruct.mapitem[map, i].ItemNum].name + " x" + MapStruct.mapitem[map, i].Value, Globals.ColorWhite, playerx, playery, 1, 0, map);
                        }
                        SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                        MapStruct.mapitem[map, i].X = 0;
                        MapStruct.mapitem[map, i].Y = 0;
                        MapStruct.mapitem[map, i].ItemNum = 0;
                        MapStruct.mapitem[map, i].Value = 0;
                        MapStruct.mapitem[map, i].Refin = 0;
                        MapStruct.mapitem[map, i].Exp = 0;
                        MapStruct.mapitem[map, i].Timer = 0;
                        SendData.sendClearMapItem(map, i);
                        break;
                    }
                }
            }
        }
        //*********************************************************************************************
        // DropItemCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para derrubar um item no mapa.
        //*********************************************************************************************
        static void receivedDropItem(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<21>", "").Split(';');

            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1) || (Convert.ToInt32(splited[1]) > 999)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0) || (Convert.ToInt32(splited[1]) < 0)) { return; }

            int Slot = Convert.ToInt32(splited[0]);
            int Value = Convert.ToInt32(splited[1]);

            int Map = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            int playerx = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
            int playery = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
           
            string item = PlayerStruct.invslot[s, Convert.ToInt32(Slot)].item;
            string[] splititem = item.Split(',');

            int itemexp = Convert.ToInt32(splititem[4]);
            int itemrefin = Convert.ToInt32(splititem[3]);
            int itemvalue = Convert.ToInt32(splititem[2]);
            int itemnum = Convert.ToInt32(splititem[1]);
            int itemtype = Convert.ToInt32(splititem[0]);

            if (Value > itemvalue) { SendData.sendMsgToPlayer(s, lang.you_dont_have_this_item_amount, Globals.ColorRed, Globals.Msg_Type_Server); return; }
            if (MapStruct.getNullMapItem(Map) == 0) { SendData.sendMsgToPlayer(s, lang.map_item_is_busy, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            if (itemvalue - Value > 0)
            {
                //Há item sobrando
                PlayerStruct.invslot[s, Convert.ToInt32(Slot)].item = itemtype.ToString() + "," + itemnum.ToString() + "," + (itemvalue - Value).ToString() + "," + itemrefin.ToString() + "," + itemexp.ToString();
            }
            else
            {
                PlayerStruct.invslot[s, Convert.ToInt32(Slot)].item = Globals.NullItem; //Não há item sobrando
            }
            //Dropar item no mapa
            int NullMapItem = MapStruct.getNullMapItem(Map);
            MapStruct.mapitem[Map, NullMapItem].Value = Value;
            MapStruct.mapitem[Map, NullMapItem].ItemType = itemtype;
            MapStruct.mapitem[Map, NullMapItem].X = playerx;
            MapStruct.mapitem[Map, NullMapItem].Y = playery;
            MapStruct.mapitem[Map, NullMapItem].ItemNum = itemnum;
            MapStruct.mapitem[Map, NullMapItem].Refin = itemrefin;
            MapStruct.mapitem[Map, NullMapItem].Exp = itemexp;
            MapStruct.mapitem[Map, NullMapItem].Timer = Loops.TickCount.ElapsedMilliseconds + 600000;
            SendData.sendMapItem(Map, NullMapItem);

            //Mandar inventário do jogador
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            
        }
        //*********************************************************************************************
        // ItemCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedItemData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newItems = data.Replace("<22>", "").Split(';');

            int items_count = Convert.ToInt32(newItems[0]);

            int reader = 1;

            for (int i = 1; i <= items_count ; i++)
            {
                ItemStruct.item[i].name= newItems[reader];
                reader += 1;
                ItemStruct.item[i].price = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].consumable = newItems[reader];
                reader += 1;
                ItemStruct.item[i].success_rate = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].animation_id = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].note = newItems[reader];
                reader += 1;
                ItemStruct.item[i].speed = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].repeats = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].tp_gain = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].hit_type = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].damage_type = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].damage_formula = newItems[reader];
                reader += 1;
                ItemStruct.item[i].damage_element = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].damage_variance = Convert.ToInt32(newItems[reader]);
                reader += 1;
                ItemStruct.item[i].damage_critical = newItems[reader];
                reader += 1;

                ItemStruct.item[i].effects_count = Convert.ToInt32(newItems[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= ItemStruct.item[i].effects_count; i2++)
                {
                    ItemStruct.itemeffect[i, i2].code = Convert.ToInt32(newItems[reader]);
                    reader += 1;
                    ItemStruct.itemeffect[i, i2].data_id = Convert.ToInt32(newItems[reader]);
                    reader += 1;
                    ItemStruct.itemeffect[i, i2].value1 = Convert.ToDouble(newItems[reader]);
                    reader += 1;
                    ItemStruct.itemeffect[i, i2].value2 = Convert.ToDouble(newItems[reader]);
                    reader += 1;
                }
                Database.Items.saveItem(i.ToString());
                Database.Items.loadItem(i.ToString());
            }
            Console.WriteLine("Items foram salvos com sucesso.");
        }
        //*********************************************************************************************
        // WeaponCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedWeaponData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newWeapons = data.Replace("<23>", "").Split(';');

            int weapons_count = Convert.ToInt32(newWeapons[0]);

            int reader = 1;

            for (int i = 1; i <= weapons_count; i++)
            {
                WeaponStruct.weapon[i].name = newWeapons[reader];
                reader += 1;
                WeaponStruct.weapon[i].price = Convert.ToInt32(newWeapons[reader]);
                reader += 1;
                WeaponStruct.weapon[i].etype_id = Convert.ToInt32(newWeapons[reader]);
                reader += 1;
                WeaponStruct.weapon[i].wtype_id = Convert.ToInt32(newWeapons[reader]);
                reader += 1;
                WeaponStruct.weapon[i].animation_id = Convert.ToInt32(newWeapons[reader]);
                reader += 1;
                WeaponStruct.weapon[i].params_size = Convert.ToInt32(newWeapons[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= WeaponStruct.weapon[i].params_size; i2++)
                {
                    WeaponStruct.weaponparams[i, i2].value = Convert.ToInt32(newWeapons[reader]);
                    reader += 1;
                }

                WeaponStruct.weapon[i].features_size = Convert.ToInt32(newWeapons[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= WeaponStruct.weapon[i].features_size; i2++)
                {
                    WeaponStruct.weaponfeatures[i, i2].code = Convert.ToInt32(newWeapons[reader]);
                    reader += 1;
                    WeaponStruct.weaponfeatures[i, i2].data_id = Convert.ToInt32(newWeapons[reader]);
                    reader += 1;
                    WeaponStruct.weaponfeatures[i, i2].value = Convert.ToDouble(newWeapons[reader]);
                    reader += 1;
                }
                Database.Weapons.saveWeapon(i.ToString());
                Database.Weapons.loadWeapon(i.ToString());
            }
            Console.WriteLine("Armas foram salvas com sucesso.");
        }
        //*********************************************************************************************
        // ArmorCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedArmorData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newArmors = data.Replace("<24>", "").Split(';');

            int armors_count = Convert.ToInt32(newArmors[0]);

            int reader = 1;

            for (int i = 1; i <= armors_count; i++)
            {
                ArmorStruct.armor[i].name = newArmors[reader];
                reader += 1;
                ArmorStruct.armor[i].price = Convert.ToInt32(newArmors[reader]);
                reader += 1;
                ArmorStruct.armor[i].etype_id = Convert.ToInt32(newArmors[reader]);
                reader += 1;
                ArmorStruct.armor[i].atype_id = Convert.ToInt32(newArmors[reader]);
                reader += 1;
                ArmorStruct.armor[i].params_size = Convert.ToInt32(newArmors[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= ArmorStruct.armor[i].params_size; i2++)
                {
                    ArmorStruct.armorparams[i, i2].value = Convert.ToInt32(newArmors[reader]);
                    reader += 1;
                }

                ArmorStruct.armor[i].features_size = Convert.ToInt32(newArmors[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= ArmorStruct.armor[i].features_size; i2++)
                {
                    ArmorStruct.armorfeatures[i, i2].code = Convert.ToInt32(newArmors[reader]);
                    reader += 1;
                    ArmorStruct.armorfeatures[i, i2].data_id = Convert.ToInt32(newArmors[reader]);
                    reader += 1;
                    ArmorStruct.armorfeatures[i, i2].value = Convert.ToDouble(newArmors[reader]);
                    reader += 1;
                }
                Database.Armors.saveArmor(i.ToString());
                Database.Armors.saveArmor(i.ToString());
            }
            Console.WriteLine("Armaduras foram salvas com sucesso.");
        }
        //*********************************************************************************************
        // SkillCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedSkillData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newSkills = data.Replace("<25>", "").Split(';');
            int skills_count = Convert.ToInt32(newSkills[0]);

            int reader = 1;

            for (int i = 1; i <= skills_count; i++)
            {
                SkillStruct.skill[i].scope = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].stype_id = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].mp_cost = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].tp_cost = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].message1 = newSkills[reader];
                reader += 1;
                SkillStruct.skill[i].message2 = newSkills[reader];
                reader += 1;
                SkillStruct.skill[i].required_wtype_id1 = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].required_wtype_id2 = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].occasion = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].success_rate = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].repeats = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].tp_gain = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].hit_type = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].animation_id = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].speed = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].note = newSkills[reader];
                reader += 1;
                SkillStruct.skill[i].damage_type = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].damage_element = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].damage_formula = newSkills[reader];
                reader += 1;
                SkillStruct.skill[i].damage_variance = Convert.ToInt32(newSkills[reader]);
                reader += 1;
                SkillStruct.skill[i].damage_critical = newSkills[reader];
                reader += 1;

                SkillStruct.skill[i].effects_count = Convert.ToInt32(newSkills[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= SkillStruct.skill[i].effects_count; i2++)
                {
                    SkillStruct.skilleffect[i, i2].code = Convert.ToInt32(newSkills[reader]);
                    reader += 1;
                    SkillStruct.skilleffect[i, i2].data_id = Convert.ToInt32(newSkills[reader]);
                    reader += 1;
                    SkillStruct.skilleffect[i, i2].value1 = Convert.ToDouble(newSkills[reader]);
                    reader += 1;
                    SkillStruct.skilleffect[i, i2].value2 = Convert.ToDouble(newSkills[reader]);
                    reader += 1;
                }
                Database.Skills.saveSkill(i.ToString());
                Database.Skills.loadSkill(i.ToString());
        }
            Console.WriteLine("Skills foram salvas com sucesso.");
        }
        //*********************************************************************************************
        // EnemyCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedEnemyData(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access <= 0) { return; }
            string[] newEnemies = data.Replace("<28>", "").Split(';');

            int start = Convert.ToInt32(newEnemies[0]);
            int startend = Convert.ToInt32(newEnemies[1]);

            int reader = 2;

            for (int i = start; i <= startend; i++)
            {
                EnemyStruct.enemy[i].battler_name = newEnemies[reader];
                reader += 1;
                EnemyStruct.enemy[i].battler_hue = Convert.ToInt32(newEnemies[reader]);
                reader += 1;
                EnemyStruct.enemy[i].exp = Convert.ToInt32(newEnemies[reader]);
                reader += 1;
                EnemyStruct.enemy[i].gold = Convert.ToInt32(newEnemies[reader]);
                reader += 1;
                EnemyStruct.enemy[i].note = newEnemies[reader];
                reader += 1;

                EnemyStruct.enemy[i].params_size = Convert.ToInt32(newEnemies[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= EnemyStruct.enemy[i].params_size; i2++)
                {
                    EnemyStruct.enemyparams[i, i2].value = Convert.ToInt32(newEnemies[reader]);
                    reader += 1;
                }

                EnemyStruct.enemy[i].drops_size = Convert.ToInt32(newEnemies[reader]);
                reader += 1;

                for (int i2 = 0; i2 <= EnemyStruct.enemy[i].drops_size; i2++)
                {
                    EnemyStruct.enemydrops[i, i2].kind = Convert.ToInt32(newEnemies[reader]);
                    reader += 1;
                    EnemyStruct.enemydrops[i, i2].data_id = Convert.ToInt32(newEnemies[reader]);
                    reader += 1;
                    EnemyStruct.enemydrops[i, i2].denominator = Convert.ToInt32(newEnemies[reader]);
                    reader += 1;
                }

                Database.Enemies.saveEnemy(i.ToString());
                Database.Enemies.loadEnemy(i.ToString());
            }

            if (start == 951)
            {
                Console.WriteLine("Inimigos foram salvos com sucesso.");
            }
        }
        //*********************************************************************************************
        // HotkeyCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedHotkey(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<26>", "").Split(';');

            if (splited.Length != 3) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }
            if (!isNumeric(splited[2])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1) || (Convert.ToInt32(splited[1]) > Globals.MaxHotkeys - 1) || (Convert.ToInt32(splited[2]) > 2)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0) || (Convert.ToInt32(splited[1]) < 0) || (Convert.ToInt32(splited[2]) < 0)) { return; }

            int slot = Convert.ToInt32(splited[0]);
            int hotkeyslot = Convert.ToInt32(splited[1]);
            byte type = Convert.ToByte(splited[2]);

            if (type == 1)
            {
                if (slot > Globals.MaxPlayer_Skills - 1) { return; }
                if (PlayerStruct.skill[s, slot].num > 0)
                {
                    PlayerStruct.hotkey[s, hotkeyslot].num = slot;
                    PlayerStruct.hotkey[s, hotkeyslot].type = type;
                    SendData.sendPlayerHotkeys(s);
                }
            }

            else if (type == 2)
            {
                if ((Convert.ToInt32(PlayerStruct.invslot[s, slot].item.Split(',')[0]) <= 1) && (Convert.ToInt32(PlayerStruct.invslot[s, slot].item.Split(',')[1]) > 0))
                {
                    PlayerStruct.hotkey[s, hotkeyslot].num = Convert.ToInt32(PlayerStruct.invslot[s, slot].item.Split(',')[1]);
                    PlayerStruct.hotkey[s, hotkeyslot].type = type;
                    SendData.sendPlayerHotkeys(s);
                }  
            }

            else if (type == 0)
            {
                PlayerStruct.hotkey[s, hotkeyslot].num = 0;
                PlayerStruct.hotkey[s, hotkeyslot].type = type;
                SendData.sendPlayerHotkeys(s);
            }
        }
        //*********************************************************************************************
        // TargetCheck / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de alteração para o alvo atual.
        //*********************************************************************************************
        static void receivedTarget(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<27>", "").Split(';');

            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }
            if ((Convert.ToInt32(splited[0]) > 2) || (Convert.ToInt32(splited[1]) > 255)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0) || (Convert.ToInt32(splited[1]) < 0)) { return; }

            byte targettype = Convert.ToByte(splited[0]);
            int target = Convert.ToInt32(splited[1]);

            if ((targettype > 2) || (targettype < 0)) { return; }

            if (targettype == 1)
            {
                //Verificar se o jogador não se desconectou no processo
                int clients = UserConnection.getS(target);
                if ((clients < 0) || (clients >= WinsockAsync.Clients.Count())) { return; }
                if ((clients < 0) || (clients >= WinsockAsync.Clients.Count())) { return; }
                if (!WinsockAsync.Clients[clients].IsConnected) { return; }
                if ((target > Globals.Player_Highs) || (target < 0)) { return; }
            }

            if (targettype == 2)
            {
                if ((target > MapStruct.tempmap[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map].NpcCount) || (target < 0)) { return; }
            }

            if ((PlayerStruct.tempplayer[s].targettype == targettype) && (PlayerStruct.tempplayer[s].target == target)) { return; }

            PlayerStruct.tempplayer[s].targettype = targettype;
            PlayerStruct.tempplayer[s].target = target;

        }
        //*********************************************************************************************
        // receivedSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido de execução de determinada magia.
        //*********************************************************************************************
        static void receivedSkill(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<29>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxPlayer_Skills - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int intslot = Convert.ToInt32(splited[0]);

            if ((intslot <= 0) && (intslot >= 6)) { return; }
            if ((PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].num <= 0)) { return; }

            if (PlayerStruct.hotkey[s, intslot].type != Globals.Hotkey_Type_Skill) { return; }
            if ((PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].cooldown > Loops.TickCount.ElapsedMilliseconds)) { return; }
            if (PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].level <= 0) { return; }
            if (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].num].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].level > PlayerStruct.tempplayer[s].Spirit)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_mana_to_spell, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //SendData.sendActionMsg(s, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].
            PlayerStruct.tempplayer[s].preparingskillslot = intslot;
            PlayerStruct.tempplayer[s].preparingskill = PlayerStruct.skill[s, PlayerStruct.hotkey[s, intslot].num].num;
            PlayerStruct.tempplayer[s].skilltimer = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].speed * 100);
            PlayerStruct.tempplayer[s].movespeed -= 0.2;
            //CORRECT SKILL INCOMPT
            SendData.sendMoveSpeed(1, s);
            SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 1, s, 81);
            //PlayerStruct.tempplayer[s].targettype;
            //PlayerStruct.tempplayer[s].target;
           // Console.WriteLine("Novo alvo");

        }
        //*********************************************************************************************
        // receivedAtr / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para usar os pontos de atributo.
        //*********************************************************************************************
        static void receivedAtr(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<30>", "").Split(';');
            
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxPlayer_Skills - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int atr = Convert.ToInt32(splited[0]);

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points <= 0) { return; }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points -= 1;

            switch (splited[0])
            {
                case "1":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire += 1;
                    break;
                case "2":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth += 1;
                    break;
                case "3":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water += 1;
                    break;
                case "4":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind += 1;
                    break;
                case "5":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark += 1;
                    break;
                case "6":
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light += 1;
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }
            SendData.sendPlayerAtrTo(s);
        }
        //*********************************************************************************************
        // receivedUseSkillPoints / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para usar os pontos de habilidade.
        //*********************************************************************************************
        static void receivedUseSkillPoints(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<31>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxPlayer_Skills - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int skill = Convert.ToInt32(splited[0]);

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints <= 0) { return; }
            
            int lv_ok = 0;
            if (skill == 2) { lv_ok = 5; } 
            if (skill == 3) { lv_ok = 10; } 
            if (skill == 4) { lv_ok = 20; } 
            if (skill == 5) { lv_ok = 40; } 
            if (skill == 6) { lv_ok = 80; } 
            if (skill == 7) { lv_ok = 80; } 
            if (skill == 8) { lv_ok = 80; } 


            if (skill <= 4)
            {
                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level < lv_ok)
                {
                    return;
                }
            }

            int max_level = SkillStruct.skill[PlayerStruct.skill[s, skill].num].success_rate;

            if (PlayerStruct.skill[s, skill].level >= max_level)
            {
                SendData.sendMsgToPlayer(s, lang.this_spell_cannot_be_evolved, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints -= 1;
            PlayerStruct.skill[s, skill].level += 1;

            SendData.sendPlayerSkills(s);
            SendData.sendPlayerSkillPoints(s);
        }
        //*********************************************************************************************
        // receivedParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedParty(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Possíveis tentativas de hacker
            if ((PlayerStruct.tempplayer[s].targettype != 1) || (PlayerStruct.tempplayer[s].target <= 0) || (PlayerStruct.tempplayer[s].InviteTimer > Loops.TickCount.ElapsedMilliseconds)) { SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            //Não pode convidar ele mesmo
            if (PlayerStruct.tempplayer[s].target == s) 
            {
                SendData.sendMsgToPlayer(s, lang.you_cannot_invite_yourself, Globals.ColorRed, Globals.Msg_Type_Server);
                return; 
            }

            //O alvo é o s
            int target = PlayerStruct.tempplayer[s].target;
            int clients = UserConnection.getS(target);


            //Verifica se ele não se desconectou no processo
            if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[clients].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Veririficar se há não tem um grupo
            if (PlayerStruct.tempplayer[target].Party > 0) 
            {
                SendData.sendMsgToPlayer(s, lang.player_is_in_another_party, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verifica se ele pode ser convidado
            if (InviteRelations.isBusy(target)) {
                SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Criar a váriaveis gerais
            PlayerStruct.tempplayer[s].Inviting = PlayerStruct.tempplayer[s].target;
            PlayerStruct.tempplayer[target].Invited = s;
            PlayerStruct.tempplayer[s].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;
            PlayerStruct.tempplayer[target].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;

            //Envia o convite
            SendData.sendPartyRequest(s, target);
        }
        //*********************************************************************************************
        // receivedPartyResponse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedPartyResponse(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<33>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 2)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int response = Convert.ToInt32(splited[0]);

            int partynum = 0;

            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].Invited) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].Invited) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].Invited))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (response == 0)
            {
                //Criação do grupo
                if (PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party == 0)
                {
                    //Checa um grupo livre
                    partynum = PartyRelations.getPartyFree();

                    //Definindo posições
                    PlayerStruct.party[partynum].leader = PlayerStruct.tempplayer[s].Invited;
                    PlayerStruct.partymembers[partynum, 1].s = PlayerStruct.tempplayer[s].Invited;
                    PlayerStruct.partymembers[partynum, 2].s = s;
                    PlayerStruct.party[partynum].active = true;
                    PlayerStruct.tempplayer[s].Party = partynum;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party = partynum;
                    
                    //Envia o grupo atualizado
                    SendData.sendPartyDataToParty(partynum);
                    SendData.sendPlayerExtraSpiritToParty(partynum, s);
                    SendData.sendPlayerExtraVitalityToParty(partynum, s);
                    SendData.sendPlayerExtraSpiritToParty(partynum, PlayerStruct.tempplayer[s].Invited);
                    SendData.sendPlayerExtraVitalityToParty(partynum, PlayerStruct.tempplayer[s].Invited);
                   
                    //Limpa os valores gerais
                    PlayerStruct.tempplayer[s].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                    PlayerStruct.tempplayer[s].Invited = 0;
                    
                    //Mensagem de que o grupo foi criado
                    SendData.sendMsgToParty(partynum, lang.party_created, Globals.ColorGreen, Globals.Msg_Type_Server);
                }
                else
                {
                    //Apenas adiciona o jogador no grupo
                    for (int i = 1; i < Globals.MaxPartyMembers; i++)
                    {
                        if (PlayerStruct.partymembers[PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party, i].s == 0)
                        {
                            //Posição no grupo
                            PlayerStruct.partymembers[PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party, i].s = s;
                            PlayerStruct.tempplayer[s].Party = PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party;
                            int p_s;
                            int partymemberscount = PartyRelations.getPartyMembersCount(PlayerStruct.tempplayer[s].Party);
                            for (int p = 1; p <= partymemberscount; p++)
                            {
                                p_s = PlayerStruct.partymembers[PlayerStruct.tempplayer[s].Party, p].s;
                                if (PlayerStruct.character[p_s, PlayerStruct.player[p_s].SelectedChar].Map != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                                {
                                    SendData.sendPlayerDataTo(p_s, s, PlayerStruct.player[s].Username, PlayerStruct.player[s].SelectedChar);
                                    SendData.sendPlayerDataTo(s, p_s, PlayerStruct.player[p_s].Username, PlayerStruct.player[p_s].SelectedChar);
                                    SendData.sendPlayerExtraVitalityTo(p_s, s);
                                    SendData.sendPlayerExtraSpiritTo(p_s, s);
                                    SendData.sendPlayerExtraVitalityTo(s, p_s);
                                    SendData.sendPlayerExtraSpiritTo(s, p_s);
                                }
                                
                            }

                            //Envia o grupo atualizado
                            SendData.sendPartyDataToParty(PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Party);
                            
                            //Limpa os valores gerais
                            PlayerStruct.tempplayer[s].InviteTimer = 0;
                            PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                            PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                            PlayerStruct.tempplayer[s].Invited = 0;

                            //Envia mensagem ao grupo de que um novo jogador entrou
                            SendData.sendMsgToParty(PlayerStruct.tempplayer[s].Party, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + " " + lang.joined_in_party, Globals.ColorGreen, Globals.Msg_Type_Server);
                            break;
                        }
                    }
                }
            }
            else
            {
                SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.player_refused_party_invite, Globals.ColorRed, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // receivedPartyKick / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para retirar determinado membro do grupo.
        //*********************************************************************************************
        static void receivedPartyKick(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<34>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.Player_Highs)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int kicktarget = Convert.ToInt32(splited[0]);
            
            //Será o melhor?
            PartyRelations.kickParty(s, kicktarget);
        }
        //*********************************************************************************************
        // receivedPartChange / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Obsoleto
        //*********************************************************************************************
        static void receivedPartyChange(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            //string[] splited = data.Replace("<35>", "").Split(';');
        }
        //*********************************************************************************************
        // receivedTrade / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedTrade(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Possíveis tentativas de hacker
            if ((PlayerStruct.tempplayer[s].targettype != 1) || (PlayerStruct.tempplayer[s].target <= 0) || (PlayerStruct.tempplayer[s].InviteTimer > Loops.TickCount.ElapsedMilliseconds)) { SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            //Não pode convidar ele mesmo
            if (PlayerStruct.tempplayer[s].target == s)
            {
                SendData.sendMsgToPlayer(s, lang.you_cannot_invite_yourself, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //O alvo é o s
            int target = PlayerStruct.tempplayer[s].target;

            //Verifica se ele não se desconectou no processo
            if ((UserConnection.getS(target) < 0) || (UserConnection.getS(target) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(target))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Veririficar se há não tem uma troca
            if (PlayerStruct.tempplayer[target].InTrade > 0)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_already_trading, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verifica se ele pode ser convidado
            if (InviteRelations.isBusy(target))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Criar a váriaveis gerais
            PlayerStruct.tempplayer[s].Inviting = PlayerStruct.tempplayer[s].target;
            PlayerStruct.tempplayer[target].Invited = s;
            PlayerStruct.tempplayer[s].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;
            PlayerStruct.tempplayer[target].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;

            //Envia o convite
            SendData.sendTradeRequest(s, target);
        }
        //*********************************************************************************************
        // receivedGuild / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedGuild(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Possíveis tentativas de hacker
            if ((PlayerStruct.tempplayer[s].targettype != 1) || (PlayerStruct.tempplayer[s].target <= 0) || (PlayerStruct.tempplayer[s].InviteTimer > Loops.TickCount.ElapsedMilliseconds)) { SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            //Não pode convidar ele mesmo
            if (PlayerStruct.tempplayer[s].target == s)
            {
                SendData.sendMsgToPlayer(s, lang.you_cannot_invite_yourself, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //O alvo é o s
            int target = PlayerStruct.tempplayer[s].target;
            int clients = UserConnection.getS(target);

            //Verifica se ele não se desconectou no processo
            if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[clients].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Veririficar se há não tem uma troca
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.player_already_have_guild, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verifica se ele pode ser convidado
            if (InviteRelations.isBusy(target))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_busy, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Criar a váriaveis gerais
            PlayerStruct.tempplayer[s].Inviting = PlayerStruct.tempplayer[s].target;
            PlayerStruct.tempplayer[target].Invited = s;
            PlayerStruct.tempplayer[s].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;
            PlayerStruct.tempplayer[target].InviteTimer = Loops.TickCount.ElapsedMilliseconds + 10000;

            //Envia o convite
            SendData.sendGuildRequest(s, target);
        }
        //*********************************************************************************************
        // receivedAddTradeOffer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedAddTradeOffer(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            //Verifica se ele não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            string[] splited = data.Replace("<37>", "").Split(';');

            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[1]) > 999)) { return; }
            if ((Convert.ToInt32(splited[1]) < 0)) { return; }

            int slot = Convert.ToInt32(splited[0]);

            if (PlayerStruct.invslot[s, slot].item == Globals.NullItem) { SendData.sendMsgToPlayer(s, lang.item_null, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            string[] splititem = PlayerStruct.invslot[s, slot].item.Split(',');

            int value = Convert.ToInt32(splited[1]);

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if (itemValue < value) 
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_this_amount, Globals.ColorRed, Globals.Msg_Type_Server);
                return; 
            }

            int tradeslot = TradeRelations.getFreeTradeOffer(s);

            if (tradeslot == 0)
            {
                SendData.sendMsgToPlayer(s, lang.dont_have_more_slots, Globals.ColorRed, Globals.Msg_Type_Server);
                return; 
            }

            if (value != itemValue)
            {
                PlayerStruct.invslot[s, slot].item = itemType + "," + itemNum + "," + (itemValue - value) + "," + itemRefin + "," + itemExp;
                PlayerStruct.tradeslot[s, tradeslot].item = itemType + "," + itemNum + "," + value + "," + itemRefin + "," + itemExp;
            }
            else
            {
                PlayerStruct.invslot[s, slot].item = Globals.NullItem;
                PlayerStruct.tradeslot[s, tradeslot].item = itemType + "," + itemNum + "," + value + "," + itemRefin + "," + itemExp;
            }

            //Envia o grupo atualizado
            SendData.sendTradeOffers(PlayerStruct.tempplayer[s].InTrade, PlayerStruct.tempplayer[s].InTrade);
            SendData.sendTradeOffers(s, PlayerStruct.tempplayer[s].InTrade);
            SendData.sendTradeOffers(s, s);
            SendData.sendTradeOffers(PlayerStruct.tempplayer[s].InTrade, s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
        }
        //*********************************************************************************************
        // receivedGuildCreate / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedGuildCreate(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<64>", "").Split(';');

            if (splited.Length != 3) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }
            if ((Convert.ToInt32(splited[0]) > 999)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[1]) > 360)) { return; }
            if ((Convert.ToInt32(splited[1]) < 0)) { return; }
            if ((splited[2].Length > 15)) { return; }
            if ((splited[2].Length < 3)) { return; }
            if (Database.Handler.nameIsIllegal(splited[2])) { return; }

            int shield = Convert.ToInt32(splited[0]);
            int hue = Convert.ToInt32(splited[1]);
            string name = splited[2];

            if ((shield < 17) || (shield > 31)) { return; }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild > 0)
            {
                SendData.sendMsgToPlayer(s, lang.you_need_to_exit_your_guild_to_create_a_new, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold < 1000000)
            {
                SendData.sendMsgToPlayer(s, lang.not_enough_gold_to_guild, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                if (GuildStruct.guild[i].name == name)
                {
                    SendData.sendMsgToPlayer(s, lang.guild_name_already_exist, Globals.ColorRed, Globals.Msg_Type_Server);
                    return;
                }
            }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold -= 1000000;

            SendData.sendPlayerG(s);

            int slot = GuildStruct.getOpenGuildSlot();

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = slot;
            GuildStruct.guild[slot].name = name;
            GuildStruct.guild[slot].shield = shield;
            GuildStruct.guild[slot].hue = hue;
            GuildStruct.guild[slot].memberlist[1] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName;
            GuildStruct.guild[slot].membersprite[1] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite;
            GuildStruct.guild[slot].memberhue[1] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue;
            GuildStruct.guild[slot].membersprite_s[1] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites;
            GuildStruct.guild[slot].leader = 1;
            GuildStruct.guild[slot].level = 1;
            GuildStruct.guild[slot].exp = 0;

            Database.Guilds.saveGuild(slot.ToString());
            SendData.sendCompleteGuild(s);  
        }
        //*********************************************************************************************
        // receivedGuildResponse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedGuildResponse(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<63>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int response = Convert.ToInt32(splited[0]);

            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].Invited) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].Invited) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[UserConnection.getS(PlayerStruct.tempplayer[s].Invited)].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (response == 0)
            {
                int guildnum = PlayerStruct.character[PlayerStruct.tempplayer[s].Invited, PlayerStruct.player[PlayerStruct.tempplayer[s].Invited].SelectedChar].Guild;
                //guildnum = 1;
                //Criação da troca
                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild <= 0) && (guildnum > 0))
                {

                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = PlayerStruct.character[PlayerStruct.tempplayer[s].Invited, PlayerStruct.player[PlayerStruct.tempplayer[s].Invited].SelectedChar].Guild;

                    int slot = GuildStruct.getOpenMemberSlot(guildnum);

                    if (slot <= 0)
                    {
                        SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.dont_have_guild_slots, Globals.ColorRed, Globals.Msg_Type_Server);
                        SendData.sendMsgToPlayer(s, lang.dont_have_guild_slots, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }

                    GuildStruct.guild[guildnum].memberlist[slot] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName;
                    GuildStruct.guild[guildnum].membersprite[slot] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite;
                    GuildStruct.guild[guildnum].memberhue[slot] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue;
                    GuildStruct.guild[guildnum].membersprite_s[slot] = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites;

                    //Envia a guilda atualizada para todos online da guilda
                    SendData.sendCompleteGuildToGuild(s);

                    //Limpa os valores gerais
                    PlayerStruct.tempplayer[s].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                    PlayerStruct.tempplayer[s].Invited = 0;

                    //Envia mensagem de congratulações
                    SendData.sendMsgToGuild(guildnum, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName + " " + lang.has_joined_in_the_guild, Globals.ColorYellow, Globals.Msg_Type_Server);
                    Database.Guilds.saveGuild(guildnum.ToString());
                }
                else
                {
                    //Limpa os valores gerais
                    PlayerStruct.tempplayer[s].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                    PlayerStruct.tempplayer[s].Invited = 0;

                    //Envia msg
                    SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.player_cannot_enter_in_guild_at_this_moment, Globals.ColorRed, Globals.Msg_Type_Server);
                    SendData.sendMsgToPlayer(s, lang.player_cannot_enter_in_guild_at_this_moment, Globals.ColorRed, Globals.Msg_Type_Server);
                }
            }
            else
            {
                //Limpa os valores gerais
                PlayerStruct.tempplayer[s].InviteTimer = 0;
                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                PlayerStruct.tempplayer[s].Invited = 0;

                SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.player_refused_guild_invite, Globals.ColorRed, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // receivedGuildKick / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para retirar um membro da guilda.
        //*********************************************************************************************
        static void receivedGuildKick(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<65>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 20)) { return; }
            if ((Convert.ToInt32(splited[0]) < 1)) { return; }

            int slot = Convert.ToInt32(splited[0]);
           
            int guildnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild;

            if (guildnum <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.dont_have_guild_to_kick_a_member, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            string member_name = GuildStruct.guild[guildnum].memberlist[slot];

            if (member_name != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName)
            {
                if (GuildStruct.guild[guildnum].memberlist[GuildStruct.guild[guildnum].leader] != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName)
                {
                    SendData.sendMsgToPlayer(s, lang.only_guild_leader_can_kick, Globals.ColorRed, Globals.Msg_Type_Server);
                    return;
                }
            }
 

            int count = GuildStruct.getMember_Count(guildnum);

            for (int i = 1; i < Globals.MaxMaps; i++)
            {
                if (MapStruct.map[i].guildmember == member_name)
                {
                    MapStruct.map[i].guildmember = "";
                }
            }

            if (GuildStruct.guild[guildnum].memberlist[GuildStruct.guild[guildnum].leader] == member_name)
            {
                if (count <= 1)
                {
                    GuildStruct.guild[guildnum].memberlist[slot] = "";
                    GuildStruct.guild[guildnum].leader = 0;
                    GuildStruct.guild[guildnum].exp = 0;
                    GuildStruct.guild[guildnum].level = 0;
                    GuildStruct.guild[guildnum].name = "";
                    GuildStruct.guild[guildnum].hue = 0;
                    GuildStruct.guild[guildnum].membersprite[slot] = "";
                    GuildStruct.guild[guildnum].memberhue[slot] = 0;
                    GuildStruct.guild[guildnum].membersprite_s[slot] = 0;
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild = 0;
                    SendData.sendMsgToPlayer(s, lang.guild_desbanded, Globals.ColorRed, Globals.Msg_Type_Server);
                }
            }

            for (int i = 1; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == member_name)
                {
                    PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Guild = 0;
                    SendData.sendCompleteClearGuild(i);
                    break;
                }
            }

            for (int i = slot + 1; i < Globals.Max_Guild_Members; i++)
            {
                GuildStruct.guild[guildnum].memberlist[i - 1] = GuildStruct.guild[guildnum].memberlist[i];
                GuildStruct.guild[guildnum].membersprite[i - 1] = GuildStruct.guild[guildnum].membersprite[i];
                GuildStruct.guild[guildnum].membersprite_s[i - 1] = GuildStruct.guild[guildnum].membersprite_s[i];
            }


            SendData.sendCompleteGuildToGuild(s);
            SendData.sendMsgToGuild(guildnum, lang.the_player + " " + member_name + " " + lang.has_been_kicked, Globals.ColorRed, Globals.Msg_Type_Server);
            Database.Guilds.saveGuild(guildnum.ToString());
        }
        //*********************************************************************************************
        // receivedTradeResponse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedTradeResponse(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<39>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 2)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int response = Convert.ToInt32(splited[0]);

            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].Invited) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].Invited) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].Invited))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (response == 0)
            {
                //Criação da troca
                if (PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InTrade == 0)
                {
                    //Definindo posições
                    PlayerStruct.tempplayer[s].InTrade = PlayerStruct.tempplayer[s].Invited;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InTrade = s;

                    //Envia o grupo atualizado
                    SendData.sendTradeOffers(PlayerStruct.tempplayer[s].Invited, PlayerStruct.tempplayer[s].Invited);
                    SendData.sendTradeOffers(s, PlayerStruct.tempplayer[s].Invited);
                    SendData.sendTradeOffers(s, s);
                    SendData.sendTradeOffers(PlayerStruct.tempplayer[s].Invited, s);

                    //Limpa os valores gerais
                    PlayerStruct.tempplayer[s].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].InviteTimer = 0;
                    PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].Invited].Inviting = 0;
                    PlayerStruct.tempplayer[s].Invited = 0;
                }
                else
                {
                    SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.player_cannot_trade_now, Globals.ColorRed, Globals.Msg_Type_Server);
                }
            }
            else
            {
                TradeRelations.clearTempTrade(s);
                TradeRelations.clearTempTrade(PlayerStruct.tempplayer[s].Invited);
                SendData.sendMsgToPlayer(PlayerStruct.tempplayer[s].Invited, lang.player_refused_trade_invite, Globals.ColorRed, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // receivedTradeAccept / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedTradeAccept(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int tradetarget = PlayerStruct.tempplayer[s].InTrade;

            PlayerStruct.tempplayer[s].TradeStatus = 1;

            if ((PlayerStruct.tempplayer[s].TradeStatus == 1) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 1))
            {
                TradeRelations.giveTradeTo(s, tradetarget);
                TradeRelations.giveTradeTo(tradetarget, s);
                SendData.sendInvSlots(tradetarget, PlayerStruct.player[tradetarget].SelectedChar);
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                SendData.sendPlayerG(s);
                SendData.sendPlayerG(tradetarget);
                SendData.sendTradeAccept(s, 3);
                SendData.sendTradeAccept(tradetarget, 3);
                TradeRelations.clearTempTrade(s);
                TradeRelations.clearTempTrade(tradetarget);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 1) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 0))
            {
                SendData.sendTradeAccept(s, 1);
                SendData.sendTradeAccept(tradetarget, 2);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 0) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 1))
            {
                SendData.sendTradeAccept(tradetarget, 1);
                SendData.sendTradeAccept(s, 2);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 0) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 0))
            {
                SendData.sendTradeAccept(tradetarget, 0);
                SendData.sendTradeAccept(s, 0);
                return;
            }
        }
        //*********************************************************************************************
        // receivedTradeRefuse / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedTradeRefuse(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int tradetarget = PlayerStruct.tempplayer[s].InTrade;

            PlayerStruct.tempplayer[s].TradeStatus = 0;

            if ((PlayerStruct.tempplayer[s].TradeStatus == 1) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 1))
            {
                SendData.sendTradeRefuse(s, 3);
                SendData.sendTradeRefuse(tradetarget, 3);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 1) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 0))
            {
                SendData.sendTradeRefuse(s, 1);
                SendData.sendTradeRefuse(tradetarget, 2);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 0) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 1))
            {
                SendData.sendTradeRefuse(tradetarget, 1);
                SendData.sendTradeRefuse(s, 2);
                return;
            }

            if ((PlayerStruct.tempplayer[s].TradeStatus == 0) && (PlayerStruct.tempplayer[tradetarget].TradeStatus == 0))
            {
                SendData.sendTradeRefuse(tradetarget, 0);
                SendData.sendTradeRefuse(s, 0);
                return;
            }
        }
        //*********************************************************************************************
        // receivedTradeClose / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o jogador fechou a troca.
        //*********************************************************************************************
        public static void receivedTradeClose(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendTradeClose(s);
                TradeRelations.giveTrade(s);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
            {
                SendData.sendTradeClose(s);
                TradeRelations.giveTrade(s);
                return;
            }

            int tradetarget = PlayerStruct.tempplayer[s].InTrade;

            if (PlayerStruct.tempplayer[s].TradeG > 0)
            {
                PlayerRelations.givePlayerGold(s, PlayerStruct.tempplayer[s].TradeG); 
                PlayerStruct.tempplayer[s].TradeG = 0;
            }

            if (PlayerStruct.tempplayer[tradetarget].TradeG > 0)
            {
                PlayerRelations.givePlayerGold(tradetarget, PlayerStruct.tempplayer[tradetarget].TradeG); 
                PlayerStruct.tempplayer[tradetarget].TradeG = 0;
            }

            SendData.sendTradeClose(s);
            SendData.sendTradeClose(tradetarget);
            TradeRelations.giveTrade(s);
            TradeRelations.giveTrade(tradetarget);
            TradeRelations.clearTempTrade(s);
            TradeRelations.clearTempTrade(tradetarget);
        }
        //*********************************************************************************************
        // ReceiveAddTradeG / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para adicionar ouro a troca atual.
        //*********************************************************************************************
        public static void receivedAddTradeG(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            //Verificar se o jogador não se desconectou no processo
            if ((UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) < 0) || (UserConnection.getS(PlayerStruct.tempplayer[s].InTrade) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(PlayerStruct.tempplayer[s].InTrade))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.player_who_invited_is_not_connected, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            string[] splited = data.Replace("<38>", "").Split(';');
            
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > 99999999)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int gold = Convert.ToInt32(splited[0]);

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold < gold)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_the_amount, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (PlayerStruct.tempplayer[s].TradeG > 0)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold += PlayerStruct.tempplayer[s].TradeG;
            }

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold -= gold;
            PlayerStruct.tempplayer[s].TradeG = gold;
            
            int tradetarget = PlayerStruct.tempplayer[s].InTrade;
            
            SendData.sendPlayerG(s);
            SendData.sendTradeG(s, s);
            SendData.sendTradeG(s, tradetarget);
        }
        //*********************************************************************************************
        // receivedQuestAction / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para executar ação de missão.
        //*********************************************************************************************
        static void receivedQuestAction(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<43>", "").Split(';');

            if (splited.Length != 1) { return; }

            if (splited[0].Split(',').Length != 2) { return; }
            if (!isNumeric(splited[0].Split(',')[0])) { return; }
            if (!isNumeric(splited[0].Split(',')[1])) { return; }
            if ((Convert.ToInt32(splited[0].Split(',')[0]) > Globals.MaxQuestGivers - 1)) { return; }
            if ((Convert.ToInt32(splited[0].Split(',')[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[0].Split(',')[1]) > Globals.MaxQuestActions - 1)) { return; }
            if ((Convert.ToInt32(splited[0].Split(',')[1]) < 0)) { return; }

            int questgiver = Convert.ToInt32(splited[0].Split(',')[0]);
            int action = Convert.ToInt32(splited[0].Split(',')[1]);

            //hacking attempt
            if ((action > 2) || (action < 0))
            {
                return;
            }

            if (MapStruct.questgiver[questgiver].map != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
            {
                return;
            }


            if ((questgiver == 10) || (questgiver == 25))
            {
                if (MapRelations.getOpenProf(s) <= 0)
                {
                    SendData.sendMsgToPlayer(s, lang.quest_already_have_reached_max_profs, Globals.ColorRed, Globals.Msg_Type_Server);
                    return;
                }
            }

            int quest = QuestRelations.getActualPlayerQuestPerGiver(s, questgiver);

            if (String.IsNullOrEmpty(MapStruct.quest[questgiver, quest].type)) { PlayerStruct.queststatus[s, questgiver, quest].status = 0; return; }

            if (action == 0 && (PlayerStruct.queststatus[s, questgiver, quest].status == 0))
            {
                //Inicia a missão
                PlayerStruct.queststatus[s, questgiver, quest].status = 1;
                for (int k = 1; k < Globals.MaxQuestKills; k++)
                {
                    PlayerStruct.questkills[s, questgiver, quest, k].kills = 0;
                }
                for (int a = 1; a < Globals.MaxQuestActions; a++)
                {
                    PlayerStruct.questactions[s, questgiver, quest, a].actiondone = false;
                }
            }

            if (action == 1 && (PlayerStruct.queststatus[s, questgiver, quest].status == 1))
            {
                //Checar se a missão está concluída
                if (Convert.ToInt32(MapStruct.quest[questgiver, quest].type.Split('|')[0]) > 0)
                {
                    for (int k = 1; k <= MapStruct.quest[questgiver, quest].killvalue; k++)
                    {
                        if (PlayerStruct.questkills[s, questgiver, quest, k].kills < MapStruct.questkills[questgiver, quest, k].value)
                        {
                           // Não terminou
                           return;
                        }
                    }
                }

                if (Convert.ToInt32(MapStruct.quest[questgiver, quest].type.Split('|')[1]) > 0)
                {
                    for (int a = 1; a <= MapStruct.quest[questgiver, quest].actionvalue; a++)
                    {
                        if (PlayerStruct.questactions[s, questgiver, quest, a].actiondone == false)
                        {
                            // Não terminou
                            return;
                        }
                    }
                }

                if (Convert.ToInt32(MapStruct.quest[questgiver, quest].type.Split('|')[2]) > 0)
                {
                    for (int i = 1; i <= MapStruct.quest[questgiver, quest].itemvalue; i++)
                    {
                        if (!InventoryRelations.hasItem(s, MapStruct.questitems[questgiver, quest, i].item))
                        {
                            //Não terminou
                            return;
                        }
                    }
                }


                if ((questgiver == 42) && (quest == 1))
                {
                    for (int i = 1; i <= 41; i++)
                    {
                        if ((i != 10) && (i != 25) && (i != 7))
                        {
                            for (int g = 1; g <= MapStruct.questgiver[questgiver].quest_count; g++)
                            {
                                if (PlayerStruct.queststatus[s, i, g].status != 2)
                                {
                                    //Não terminou
                                  //  return; 
                                }
                            }
                        }
                    }
                }


                if (MapStruct.quest[questgiver, quest].rewardvalue > InventoryRelations.getNumOfInvFreeSlots(s))
                {
                    SendData.sendMsgToPlayer(s, lang.quest_reward_inventory_full, Globals.ColorGreen, Globals.Msg_Type_Server);
                    return;
                }

                string[] item;

                if (Convert.ToInt32(MapStruct.quest[questgiver, quest].type.Split('|')[2]) > 0)
                {
                    for (int i = 1; i <= MapStruct.quest[questgiver, quest].itemvalue; i++)
                    {
                        InventoryRelations.pickItem(s, Convert.ToInt32(MapStruct.questitems[questgiver, quest, i].item.Split(',')[0]), Convert.ToInt32(MapStruct.questitems[questgiver, quest, i].item.Split(',')[1]), Convert.ToInt32(MapStruct.questitems[questgiver, quest, i].item.Split(',')[2]), Convert.ToInt32(MapStruct.questitems[questgiver, quest, i].item.Split(',')[3])); 
                    }
                }

                //Entrega as recompensas

                if (QuestRelations.IsQuestGiverRepeatable(questgiver))
                {
                    //Reinicia a missão
                    PlayerStruct.queststatus[s, questgiver, quest].status = 0;
                    for (int k = 1; k < Globals.MaxQuestKills; k++)
                    {
                        PlayerStruct.questkills[s, questgiver, quest, k].kills = 0;
                    }
                    for (int a = 1; a < Globals.MaxQuestActions; a++)
                    {
                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = false;
                    }
                }
                else
                {
                    //Finaliza a missão
                    PlayerStruct.queststatus[s, questgiver, quest].status = 2;
                    
                    if ((questgiver == 10) && (quest == 1))
                    {
                        int profnum = MapRelations.getOpenProf(s);
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[profnum] = Globals.Job_Miner;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum] = 1;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum] = 0;
                        SendData.sendMsgToPlayer(s, lang.learned_miner_prof, Globals.ColorYellow, Globals.Msg_Type_Server);
                        SendData.sendProfs(s);
                    }
                    if ((questgiver == 25) && (quest == 1))
                    {
                        int profnum = MapRelations.getOpenProf(s);
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[profnum] = Globals.Job_Blacksmith;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum] = 1;
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum] = 0;
                        SendData.sendMsgToPlayer(s, lang.learned_blacksmith_prof, Globals.ColorYellow, Globals.Msg_Type_Server);
                        SendData.sendProfs(s);
                    }
                }

                for (int i = 1; i <= MapStruct.quest[questgiver, quest].rewardvalue; i++)
                {
                    item = MapStruct.questrewards[questgiver, quest, i].item.Split('/');
                    InventoryRelations.giveItem(s, Convert.ToInt32(item[0]), Convert.ToInt32(item[1]), Convert.ToInt32(item[2]), Convert.ToInt32(item[3]), Globals.NullExp);
                }
                PlayerRelations.givePlayerExp(s, MapStruct.quest[questgiver, quest].exp);
                PlayerRelations.givePlayerGold(s, MapStruct.quest[questgiver, quest].gold);
                SendData.sendMsgToPlayer(s, lang.completed_a_mission, Globals.ColorGreen, Globals.Msg_Type_Server);
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            }

            SendData.sendAllQuests(s);

        }
        //*********************************************************************************************
        // receivedShopBuy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para comprar item da loja atual.
        //*********************************************************************************************
        public static void receivedShopBuy(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].InShop <= 0) { return; }

            string[] splited = data.Replace("<45>", "").Split(';');
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }

            int shopnum = Convert.ToInt32(splited[0]);

            if ((shopnum <= 0) || (shopnum > ShopStruct.shop[PlayerStruct.tempplayer[s].InShop].item_count)) { return; }

            if (InventoryRelations.getNumOfInvFreeSlots(s) <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.shop_inventory_full, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold >= ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].price)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold -= ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].price;
                InventoryRelations.giveItem(s, ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].type, ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].num, ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].value, ShopStruct.shopitem[PlayerStruct.tempplayer[s].InShop, shopnum].refin, Globals.NullExp);
                SendData.sendMsgToPlayer(s, lang.you_bought_a_item, Globals.ColorGreen, Globals.Msg_Type_Server);
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                SendData.sendPlayerG(s);
            }
            else
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_gold_to_this, Globals.ColorRed, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // receivedBuyPShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para comprar item da loja de um outro jogador.
        //*********************************************************************************************
        public static void receivedBuyPShop(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].InPShop <= 0) { return; }
            if (PlayerStruct.tempplayer[s].Shopping) { return; }

            string[] splited = data.Replace("<83>", "").Split(';');
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }

            int shopnum = Convert.ToInt32(splited[0]);
            int shops = PlayerStruct.tempplayer[s].InPShop;

            if ((shopnum <= 0) || (shopnum > Globals.Max_PShops - 1)) { return; }

            //Verifica se ele não se desconectou no processo
            if ((UserConnection.getS(shops) < 0) || (UserConnection.getS(shops) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.shop_is_gone, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }
            if (!WinsockAsync.Clients[(UserConnection.getS(shops))].IsConnected)
            {
                SendData.sendMsgToPlayer(s, lang.shop_is_gone, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (!PlayerStruct.tempplayer[shops].Shopping)
            {
                SendData.sendMsgToPlayer(s, lang.shop_is_gone, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (InventoryRelations.getNumOfInvFreeSlots(s) <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.dont_have_inventory_space_to_buy, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold >= PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].price)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold -= PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].price;
                PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].Gold += PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].price;
                InventoryRelations.giveItem(s, PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].type, PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].num, PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].value, PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].refin, PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[shopnum].exp);
                for (int i = (shopnum + 1); i < Globals.Max_PShops; i++)
                {
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i - 1].num = PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].num;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i - 1].type = PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].type;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i - 1].value = PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].value;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i - 1].refin = PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].refin;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i - 1].price = PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].price;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].num = 0;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].type = 0;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].value = 0;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].refin = 0;
                    PlayerStruct.character[shops, PlayerStruct.player[shops].SelectedChar].pshopslot[i].price = 0;
                }
                SendData.sendMsgToPlayer(s, lang.you_bought_a_item, Globals.ColorGreen, Globals.Msg_Type_Server);
                SendData.sendMsgToPlayer(shops, lang.you_sold_a_item, Globals.ColorGreen, Globals.Msg_Type_Server);
                SendData.sendPShopSlots(shops, s);
                SendData.sendPShopSlots(shops, shops);
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
                SendData.sendPlayerG(shops);
                SendData.sendPlayerG(s);
            }
            else
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_needed_amount_of_gold, Globals.ColorRed, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // receivedShopClose / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que fechou a loja.
        //*********************************************************************************************
        public static void receivedShopClose(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //Sair da loja, o servidor precisa saber
            PlayerStruct.tempplayer[s].InShop = 0;
        }
        //*********************************************************************************************
        // receivedShopSell / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para vender um item a loja.
        //*********************************************************************************************
        public static void receivedShopSell(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].InShop <= 0) { return; }

            string[] splited = data.Replace("<46>", "").Split(';');
            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (!isNumeric(splited[1])) { return; }

            int invslot = Convert.ToInt32(splited[0]);
            int value = Convert.ToInt32(splited[1]);

            if ((invslot <= 0) || (invslot > Globals.MaxInvSlot - 1)) { return; }
            if ((value <= 0) || (value > 999)) { return; }
           
            string[] splititem = PlayerStruct.invslot[s, invslot].item.Split(',');

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);

            if (!InventoryRelations.pickItem(s, itemType, itemNum, value, itemRefin)) { return; }

            int gold_value = 0;

            switch (itemType)
            {
                case 0:
                case 1:
                  gold_value = ItemStruct.item[itemNum].price * value;
                  break;
                case 2:
                  gold_value = WeaponStruct.weapon[itemNum].price * value;
                  break;
                case 3:
                  gold_value = ArmorStruct.armor[itemNum].price * value;
                  break;
                default:
                  Console.WriteLine("Recebeu um valor/tipo inválido em receivedShopSell: " + itemType);
                  return;
            }

            PlayerRelations.givePlayerGold(s, gold_value);
            SendData.sendPlayerG(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendMsgToPlayer(s, lang.you_sold_a_item, Globals.ColorGreen, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // ReceiveOpenCraft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para abrir a criação de itens.
        //*********************************************************************************************
        static void receivedOpenCraft(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_CraftPoints; i++)
            {
                if (MapStruct.craftpoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    int profnum = PlayerRelations.getPlayerProf(s, MapStruct.craftpoint[i].type);

                    if (profnum <= 0)
                    {
                        SendData.sendMsgToPlayer(s, lang.you_dont_have_prof_to_interact, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }
                    if (MapStruct.craftpoint[i].type == Globals.Job_Blacksmith)
                    {
                        if (EquipmentRelations.getPlayerWeapon(s) == 31)
                        {
                            PlayerStruct.tempplayer[s].InCraft = true;
                            PlayerStruct.tempplayer[s].CraftType = MapStruct.craftpoint[i].type;

                            //if ((PlayerStruct.tempplayer[s].CraftType == Globals.Job_ArmorBlacksmith) || (PlayerStruct.tempplayer[s].CraftType == Globals.Job_SwordBlacksmith)) { SendData.sendBlacksmith(s); }
                            // if (PlayerStruct.tempplayer[s].CraftType == Globals.Job_Alchemist) { SendData.sendAlchemist(s); }
                            break;
                        }
                        else
                        {
                            SendData.sendMsgToPlayer(s, lang.you_dont_have_tool_to_interact, Globals.ColorRed, Globals.Msg_Type_Server);
                        }
                    }
                }
            }
        }
        //*********************************************************************************************
        // receivedOpenBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para abrir o banco.
        //*********************************************************************************************
        static void receivedOpenBank(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_BankPoints; i++)
            {
                if (MapStruct.bankpoint[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    PlayerStruct.tempplayer[s].InBank = true;
                    SendData.sendBankSlots(s);
                    break;
                }
            }
        }
        //*********************************************************************************************
        // receivedOpenPShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para abrir a loja de um outro jogador.
        //*********************************************************************************************
        static void receivedOpenPShop(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].InPShop > 0) { return; }
            if (PlayerStruct.tempplayer[s].Shopping) { return; }
            string[] splited = data.Replace("<84>", "").Split(';');
            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if (Convert.ToInt32(splited[0]) < 0) { return; }
            if (Convert.ToInt32(splited[0]) > Globals.Player_Highs) { return; }

            //Contêm o jogador que é dono da loja
            int shops = Convert.ToInt32(splited[0]);

            //Se for o s, ele apenas está atualizando os dados da sua própria loja, mas como tudo, devemos sempre pensar em temporizar alguns
            //dados importantes.
            if (shops == s) { SendData.sendPShopSlots(s, s); }

            //Verificar se o jogador não se desconectou no processo
            if (!PlayerStruct.tempplayer[shops].Shopping) { return; }
            if ((UserConnection.getS(shops) < 0) || (UserConnection.getS(shops) >= WinsockAsync.Clients.Count())) { return; }
            if (!WinsockAsync.Clients[(UserConnection.getS(shops))].IsConnected) { return; }

            //Definir a loja e enviar.
            PlayerStruct.tempplayer[s].InPShop = shops;
            SendData.sendPShopSlots(shops, s);
        }
        //*********************************************************************************************
        // receivedStartPShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Pedido para inicia as vendas a própria loja.
        //*********************************************************************************************
        static void receivedStartPShop(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].InPShop > 0) { return; }
            if (PlayerStruct.tempplayer[s].Shopping) { return; }

            PlayerStruct.tempplayer[s].Shopping = true;
            SendData.sendPlayerShoppingToMap(s);
            SendData.sendMsgToPlayer(s, lang.you_started_your_shop, Globals.ColorGreen, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // ReceiveCloseBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o jogador fechou a janela do banco.
        //*********************************************************************************************
        static void receivedCloseBank(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //O servidor precisa saber quando o banco é fechado
            PlayerStruct.tempplayer[s].InBank = false;
        }
        //*********************************************************************************************
        // receivedClosePShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o jogador fechou a loja de outro jogador.
        //*********************************************************************************************
        static void receivedClosePShop(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //O jogador não está mais em uma loja
            PlayerStruct.tempplayer[s].InPShop = 0;
            
            //Se era uma loja, enviar ao mapa
            if (PlayerStruct.tempplayer[s].Shopping)
            {
                PlayerStruct.tempplayer[s].Shopping = false;
                SendData.sendPlayerShoppingToMap(s);
                SendData.sendMsgToPlayer(s, lang.you_stopped_your_shop, Globals.ColorGreen, Globals.Msg_Type_Server);
            }
        }
        //*********************************************************************************************
        // ReceiveCloseCraft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Aviso de que o jogador fechou a criação de itens.
        //*********************************************************************************************
        static void receivedCloseCraft(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InCraft) { return; }
            //Devolve os itens

            bool need_invslot = false;

            for (int i = 1; i < Globals.Max_Craft; i++)
            {
                if (PlayerStruct.craft[s, i].num > 0)
                {
                    InventoryRelations.giveItem(s, PlayerStruct.craft[s, i].type, PlayerStruct.craft[s, i].num, PlayerStruct.craft[s, i].value, PlayerStruct.craft[s, i].refin, PlayerStruct.craft[s, i].exp);
                    need_invslot = true;
                    PlayerStruct.craft[s, i].num = 0;
                    PlayerStruct.craft[s, i].type = 0;
                    PlayerStruct.craft[s, i].value = 0;
                    PlayerStruct.craft[s, i].refin = 0;
                }
            }

            //O servidor precisa saber quando o jogador fechou o craft
            PlayerStruct.tempplayer[s].InCraft = false;

            if (need_invslot) { SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar); SendData.sendCraft(s); }
        }
        //*********************************************************************************************
        // receivedCraftCreate / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Tentativa de criar determinado item.
        //*********************************************************************************************
        static void receivedCraftCreate(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InCraft) { return; }
            if (PlayerStruct.tempplayer[s].CraftType <= 0) { return; }
            if (PlayerStruct.tempplayer[s].CraftItem <= 0) { return; }
            if (MapStruct.craftrecipe[PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, 1].num <= 0) { return; }
            if (InventoryRelations.getNumOfInvFreeSlots(s) <= 0) { SendData.sendMsgToPlayer(s, lang.dont_have_inventory_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            int[] craftslot = new int[Globals.Max_Craft];

            for (int i = 1; i < Globals.Max_Craft; i++)
            {
                if (MapStruct.craftrecipe[PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, i].num > 0)
                {
                    craftslot[i] = CraftRelations.craftHasItem(s, MapStruct.craftrecipe[PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, i].type, MapStruct.craftrecipe[PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, i].num);
                    
                    if (craftslot[i] == -1)
                    {
                        SendData.sendMsgToPlayer(s, lang.incorrect_recipe, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }
                }
            }

                       
            for (int i = 1; i < Globals.Max_Craft; i++)
            {
                PlayerStruct.craft[s, craftslot[i]].value -= MapStruct.craftrecipe[PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, i].value;
                if (PlayerStruct.craft[s, craftslot[i]].value <= 0)
                {
                    PlayerStruct.craft[s, craftslot[i]].value = 0;
                    PlayerStruct.craft[s, craftslot[i]].num = 0;
                    PlayerStruct.craft[s, craftslot[i]].type = 0;
                    PlayerStruct.craft[s, craftslot[i]].refin = 0;
                    PlayerStruct.craft[s, craftslot[i]].exp = 0;
                }
            }


            InventoryRelations.giveItem(s, PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem, 1, CraftRelations.getRefinCraft(s, PlayerStruct.tempplayer[s].CraftType), Globals.NullExp);
            SendData.sendMsgToPlayer(s, lang.you_created_a_new_item, Globals.ColorGreen, Globals.Msg_Type_Server);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);

           // if ((PlayerStruct.tempplayer[s].CraftType == Globals.Job_ArmorBlacksmith) || (PlayerStruct.tempplayer[s].CraftType == Globals.Job_SwordBlacksmith)) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Blacksmith += 1; SendData.sendBlacksmith(s); }
          //  if (PlayerStruct.tempplayer[s].CraftType == Globals.Job_Alchemist) { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Alchemist += 1; SendData.sendAlchemist(s); }

            SendData.sendCraft(s);
        }
        //*********************************************************************************************
        // receivedItemCraft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedItemCraft(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InCraft) { return; }
            string[] splited = data.Replace("<53>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > WeaponStruct.weapon.Length - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            PlayerStruct.tempplayer[s].CraftItem = Convert.ToInt32(splited[0]);
            SendData.sendRecipe(s, PlayerStruct.tempplayer[s].CraftType, PlayerStruct.tempplayer[s].CraftItem);
        }
        //*********************************************************************************************
        // receivedCraftWith / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedCraftWith(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InCraft) { return; }
            string[] splited = data.Replace("<49>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.Max_Craft - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int craftslot = Convert.ToInt32(splited[0]);

            if (PlayerStruct.craft[s, craftslot].num <= 0) { return; }

            if (!InventoryRelations.giveItem(s, PlayerStruct.craft[s, craftslot].type, PlayerStruct.craft[s, craftslot].num, PlayerStruct.craft[s, craftslot].value, PlayerStruct.craft[s, craftslot].refin, PlayerStruct.craft[s, craftslot].exp)) { return; }
            PlayerStruct.craft[s, craftslot].type = 0;
            PlayerStruct.craft[s, craftslot].num = 0;
            PlayerStruct.craft[s, craftslot].value = 0;
            PlayerStruct.craft[s, craftslot].refin = 0;
            PlayerStruct.craft[s, craftslot].exp = 0;

            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendCraft(s);
        }
        //*********************************************************************************************
        // receivedImprovement / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedImprovement(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<72>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) <= 0)) { return; }

            int oriunslot = PlayerRelations.getPlayerOriunklatex(s);

            if (oriunslot == 0) { return; }

            int itemslot = Convert.ToInt32(splited[0]);
            
            string item = PlayerStruct.invslot[s, itemslot].item;
            string[] splititem = item.Split(',');

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if (itemNum <= 0) { return; }
            if ((itemType == 0) || (itemType == 1)) { return; }

            if (InventoryRelations.getNumOfInvFreeSlots(s) <= 0) { SendData.sendMsgToPlayer(s, lang.dont_have_inventory_space, Globals.ColorGreen, Globals.Msg_Type_Server); return; }

            //Pegar oriun
            InventoryRelations.pickItem(s, 1, 68, 1, 0);
            //Pegar um equipamento selecionado
            InventoryRelations.pickItem(s, itemType, itemNum, 1, itemRefin);
            //Entregar novo equipamento
            InventoryRelations.giveItem(s, itemType, itemNum, 1, itemRefin + 1, itemExp);

            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendMsgToPlayer(s, lang.you_evolved_a_item, Globals.ColorGreen, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // receivedWBuy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedWBuy(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<73>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxItems - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) <= 0)) { return; }

            int slot = Convert.ToInt32(splited[0]);

            if (ShopStruct.shopitem[Globals.Shop_W, slot].price > PlayerStruct.player[s].WPoints) { return; }
            if (!BankRelations.giveBankItem(s, ShopStruct.shopitem[Globals.Shop_W, slot].type, ShopStruct.shopitem[Globals.Shop_W, slot].num, ShopStruct.shopitem[Globals.Shop_W, slot].value, ShopStruct.shopitem[Globals.Shop_W, slot].refin, Globals.NullExp)) { SendData.sendNStatus(s, "Falha ao entrar o item, verifique o banco."); return; }
            PlayerStruct.player[s].WPoints -= ShopStruct.shopitem[Globals.Shop_W, slot].price;

            SendData.sendWPoints(s);
            SendData.sendNStatus(s, "Compra realizada com sucesso.");
        }
        //*********************************************************************************************
        // receivedBankGiveItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedBankGiveItem(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InBank) { return; }
            string[] splited = data.Replace("<57>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.Max_BankSlots - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int bankslot = Convert.ToInt32(splited[0]);

            int itemNum = PlayerStruct.player[s].bankslot[bankslot].num;
            int itemType = PlayerStruct.player[s].bankslot[bankslot].type;
            int itemValue = PlayerStruct.player[s].bankslot[bankslot].value;
            int itemRefin = PlayerStruct.player[s].bankslot[bankslot].refin;
            int itemExp = PlayerStruct.player[s].bankslot[bankslot].exp;

            if (itemNum <= 0) { return; }
            if (!InventoryRelations.giveItem(s, itemType, itemNum, itemValue, itemRefin, itemExp)) { return; }

            PlayerStruct.player[s].bankslot[bankslot].type = 0;
            PlayerStruct.player[s].bankslot[bankslot].num = 0;
            PlayerStruct.player[s].bankslot[bankslot].value = 0;
            PlayerStruct.player[s].bankslot[bankslot].refin = 0;
            PlayerStruct.player[s].bankslot[bankslot].exp = 0;

            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendBankSlots(s);
        }
        //*********************************************************************************************
        // receivedWithPShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedWithPShop(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<81>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.Max_PShops - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }

            int pshopslot = Convert.ToInt32(splited[0]);

            int itemNum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[pshopslot].num;
            int itemType = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[pshopslot].type;
            int itemValue = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[pshopslot].value;
            int itemRefin = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[pshopslot].refin;
            int itemExp = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[pshopslot].exp;

            if (itemNum <= 0) { return; }
            if (!InventoryRelations.giveItem(s, itemType, itemNum, itemValue, itemRefin, itemExp)) { return; }

            for (int i = (pshopslot + 1); i < Globals.Max_PShops; i++)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i - 1].num = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i - 1].type = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i - 1].value = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i - 1].refin = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i - 1].price = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num = 0;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type = 0;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value = 0;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin = 0;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price = 0;
            }

            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendPShopSlots(s, s);
        }
        //*********************************************************************************************
        // receivedCraftAdd / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedCraftAdd(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InCraft) { return; }
            string[] splited = data.Replace("<48>", "").Split(';');

            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[1]) > 999)) { return; }
            if ((Convert.ToInt32(splited[1]) < 0)) { return; }

            int slot = Convert.ToInt32(splited[0]);

            if (PlayerStruct.invslot[s, slot].item == Globals.NullItem) { SendData.sendMsgToPlayer(s, lang.item_nonexistent, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            string[] splititem = PlayerStruct.invslot[s, slot].item.Split(',');

            int value = Convert.ToInt32(splited[1]);

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if (itemValue < value)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_the_amount, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int craftslot = PlayerRelations.getFreeCraft(s);

            if (craftslot == -1) { return; }

            if (value != itemValue)
            {
                PlayerStruct.invslot[s, slot].item = itemType + "," + itemNum + "," + (itemValue - value) + "," + itemRefin + "," + itemExp;
                PlayerStruct.craft[s, craftslot].type = itemType;
                PlayerStruct.craft[s, craftslot].num = itemNum;
                PlayerStruct.craft[s, craftslot].value = value;
                PlayerStruct.craft[s, craftslot].refin = itemRefin;
                PlayerStruct.craft[s, craftslot].exp = itemExp;
            }
            else
            {
                PlayerStruct.invslot[s, slot].item = Globals.NullItem;
                PlayerStruct.craft[s, craftslot].type = itemType;
                PlayerStruct.craft[s, craftslot].num = itemNum;
                PlayerStruct.craft[s, craftslot].value = value;
                PlayerStruct.craft[s, craftslot].refin = itemRefin;
                PlayerStruct.craft[s, craftslot].exp = itemExp;
            }

            SendData.sendCraft(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
        }
        //*********************************************************************************************
        // receivedBankPickItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedBankPickItem(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            if (!PlayerStruct.tempplayer[s].InBank) { return; }
            string[] splited = data.Replace("<56>", "").Split(';');

            if (splited.Length != 2) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[1]) > 999)) { return; }
            if ((Convert.ToInt32(splited[1]) < 0)) { return; }

            int slot = Convert.ToInt32(splited[0]);

            if (PlayerStruct.invslot[s, slot].item == Globals.NullItem) { SendData.sendMsgToPlayer(s, lang.item_nonexistent, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            string[] splititem = PlayerStruct.invslot[s, slot].item.Split(',');

            int value = Convert.ToInt32(splited[1]);

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if (itemValue < value)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_the_amount, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (!BankRelations.giveBankItem(s, itemType, itemNum, value, itemRefin, itemExp)) { SendData.sendMsgToPlayer(s, lang.deliver_item_fail, Globals.ColorRed, Globals.Msg_Type_Server); return; }
            if (!InventoryRelations.pickItem(s, itemType, itemNum, value, itemRefin)) { SendData.sendMsgToPlayer(s, lang.deliver_item_fail, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            SendData.sendBankSlots(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
        }
        //*********************************************************************************************
        // receivedAddPShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        static void receivedAddPShop(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<80>", "").Split(';');

            if (splited.Length != 3) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxInvSlot - 1)) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[1]) > 999)) { return; }
            if ((Convert.ToInt32(splited[1]) < 0)) { return; }
            if ((Convert.ToInt32(splited[2]) < 0)) { return; }
            if ((Convert.ToInt32(splited[2]) > 999999999)) { return; }

            int price = Convert.ToInt32(splited[2]);
            int slot = Convert.ToInt32(splited[0]);

            if (PlayerStruct.invslot[s, slot].item == Globals.NullItem) { SendData.sendMsgToPlayer(s, lang.item_nonexistent, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            string[] splititem = PlayerStruct.invslot[s, slot].item.Split(',');

            int value = Convert.ToInt32(splited[1]);

            int itemNum = Convert.ToInt32(splititem[1]);
            int itemType = Convert.ToInt32(splititem[0]);
            int itemValue = Convert.ToInt32(splititem[2]);
            int itemRefin = Convert.ToInt32(splititem[3]);
            int itemExp = Convert.ToInt32(splititem[4]);

            if (itemValue < value)
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_the_amount, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (!ShopRelations.givePShopItem(s, itemType, itemNum, value, itemRefin, price, itemExp)) { SendData.sendMsgToPlayer(s, lang.deliver_item_fail_maybe_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }
            if (!InventoryRelations.pickItem(s, itemType, itemNum, value, itemRefin)) { SendData.sendMsgToPlayer(s, lang.deliver_item_fail_maybe_space, Globals.ColorRed, Globals.Msg_Type_Server); return; }

            SendData.sendPShopSlots(s, s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
        }
        //*********************************************************************************************
        // receivedCompleteAction / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedCompleteAction(int s, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, data) != null)
            {
                return;
            }

            //CÓDIGO
            string[] splited = data.Replace("<59>", "").Split(';');

            if (splited.Length != 1) { return; }
            if (!isNumeric(splited[0])) { return; }
            if ((Convert.ToInt32(splited[0]) < 0)) { return; }
            if ((Convert.ToInt32(splited[0]) > Globals.MaxQuestGivers - 1)) { return; }

            int questgiver = Convert.ToInt32(splited[0]);
            int map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int x = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            int y = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;

            int quest = QuestRelations.getActualPlayerQuestPerGiver(s, questgiver);

            int actionmap;
            int actionx;
            int actiony;

            if (MapStruct.quest[questgiver, quest].actionvalue > 0)
            {
                for (int a = 1; a <= MapStruct.quest[questgiver, quest].actionvalue; a++)
                {
                    if (MapStruct.questactions[questgiver, quest, a].type == 1)
                    {
                        if (!PlayerStruct.questactions[s, questgiver, quest, a].actiondone)
                        {
                            actionmap = Convert.ToInt32(MapStruct.questactions[questgiver, quest, a].data.Split(',')[0]);

                            if (actionmap == map)
                            {
                                actionx = Convert.ToInt32(MapStruct.questactions[questgiver, quest, a].data.Split(',')[1]);
                                actiony = Convert.ToInt32(MapStruct.questactions[questgiver, quest, a].data.Split(',')[2]);

                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == actiony) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == actionx))
                                {
                                    if (PlayerStruct.queststatus[s, questgiver, quest].status == 1)
                                    {
                                        SendData.sendActionMsg(s, lang.action_completed, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0);
                                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = true;
                                        SendData.sendQuestAction(s, questgiver, quest, a);
                                        return;
                                    }
                                }

                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == actiony) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X + 1 == actionx))
                                {
                                    if (PlayerStruct.queststatus[s, questgiver, quest].status == 1)
                                    {
                                        SendData.sendActionMsg(s, lang.action_completed, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0);
                                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = true;
                                        SendData.sendQuestAction(s, questgiver, quest, a);
                                        return;
                                    }
                                }

                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y == actiony) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X - 1 == actionx))
                                {
                                    if (PlayerStruct.queststatus[s, questgiver, quest].status == 1)
                                    {
                                        SendData.sendActionMsg(s, lang.action_completed, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0);
                                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = true;
                                        SendData.sendQuestAction(s, questgiver, quest, a);
                                        return;
                                    }
                                }

                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y + 1 == actiony) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == actionx))
                                {
                                    if (PlayerStruct.queststatus[s, questgiver, quest].status == 1)
                                    {
                                        SendData.sendActionMsg(s, lang.action_completed, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0);
                                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = true;
                                        SendData.sendQuestAction(s, questgiver, quest, a);
                                        return;
                                    }
                                }

                                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y - 1 == actiony) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X == actionx))
                                {
                                    if (PlayerStruct.queststatus[s, questgiver, quest].status == 1)
                                    {
                                        SendData.sendActionMsg(s, lang.action_completed, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0);
                                        PlayerStruct.questactions[s, questgiver, quest, a].actiondone = true;
                                        SendData.sendQuestAction(s, questgiver, quest, a);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //*********************************************************************************************
        // receivedRespawn / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedRespawn(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].isDead)
            {
                PlayerStruct.tempplayer[s].isDead = false;
                int mapnum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
                int bootmap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootMap;
                byte bootx = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootX;
                byte booty = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootY;

                MovementRelations.playerWarp(s, bootmap, bootx, booty);

                PlayerStruct.tempplayer[s].Vitality = 1;
                if (PlayerStruct.tempplayer[s].Party > 0)
                {
                    SendData.sendPlayerVitalityToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Vitality);
                }
                SendData.sendPlayerVitalityToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Vitality);
                SendData.sendPlayerDeathTo(s, s);
            }
        }
        //*********************************************************************************************
        // receivedShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void receivedShop(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Shops; i++)
            {
                if (ShopStruct.shop[i].map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                {
                    switch (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir)
                    {
                        case 8:
                            if ((ShopStruct.shop[i].y == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y - 1) && (ShopStruct.shop[i].x == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X)))
                            {
                                SendData.sendShop(s, i);
                                PlayerStruct.tempplayer[s].InShop = i;
                                return;
                            }
                            break;
                        case 2:
                            if ((ShopStruct.shop[i].y == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y) + 1) && (ShopStruct.shop[i].x == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X)))
                            {
                                SendData.sendShop(s, i);
                                PlayerStruct.tempplayer[s].InShop = i;
                                return;
                            }
                            break;
                        case 4:
                            if ((ShopStruct.shop[i].y == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y)) && (ShopStruct.shop[i].x == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X - 1)))
                            {
                                SendData.sendShop(s, i);
                                PlayerStruct.tempplayer[s].InShop = i;
                                return;
                            }
                            break;
                        case 6:
                            if ((ShopStruct.shop[i].y == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y)) && (ShopStruct.shop[i].x == Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X + 1)))
                            {
                                SendData.sendShop(s, i);
                                PlayerStruct.tempplayer[s].InShop = i;
                                return;
                            }
                            break;
                        default:
                            WinsockAsync.Log(String.Format("Tentativa de abrir loja inexistente."));
                            break;
                    }

                }
            } 
        }
    }
}

