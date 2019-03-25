using System;
using System.Reflection;
using System.Linq;

namespace __Forjerum
{
    class PartyRelations : Languages.LStruct
    {
        //*********************************************************************************************
        // partyShareExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Divide a Exp para determinado grupo baseado no atacante
        //*********************************************************************************************
        public static void partyShareExp(int Attacker, int Victim, int Map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim, Map) != null)
            {
                return;
            }

            //CÓDIGO
            int NpcX = NpcStruct.tempnpc[Map, Victim].X;
            int NpcY = NpcStruct.tempnpc[Map, Victim].Y;
            int PlayerX = Convert.ToInt32(PlayerStruct.character[Attacker, PlayerStruct.player[Attacker].SelectedChar].X);
            int PlayerY = Convert.ToInt32(PlayerStruct.character[Attacker, PlayerStruct.player[Attacker].SelectedChar].Y);

            //PARTY EXP
            int partynum = PlayerStruct.tempplayer[Attacker].Party;

            //Damos xp ao jogador e mostramos a xp ganha
            if (partynum > 0)
            {
                int memberscount = PartyRelations.getPartyMembersCount(partynum);
                for (int i = 1; i <= memberscount; i++)
                {
                    int members = PlayerStruct.partymembers[partynum, i].s;
                    if (PlayerStruct.character[members, PlayerStruct.player[members].SelectedChar].Map == PlayerStruct.character[Attacker, PlayerStruct.player[Attacker].SelectedChar].Map)
                    {
                        //Tem grupo para dividir a exp
                        //Adiciona uma kill se houver uma quest para esse npc
                        for (int g = 1; g < Globals.MaxQuestGivers; g++)
                        {
                            for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                            {
                                //Prevent
                                if ((String.IsNullOrEmpty(MapStruct.quest[g, q].type)) && (PlayerStruct.queststatus[members, g, q].status > 0)) { PlayerStruct.queststatus[members, g, q].status = 0; return; }

                                //Execute
                                if ((PlayerStruct.queststatus[members, g, q].status == 1) && (Convert.ToInt32(MapStruct.quest[g, q].type.Split('|')[0]) > 0))
                                {
                                    for (int k = 1; k < Globals.MaxQuestKills; k++)
                                    {
                                        if (MapStruct.questkills[g, q, k].monstername == NpcStruct.npc[Map, Victim].Name)
                                        {
                                            if (PlayerStruct.questkills[members, g, q, k].kills < MapStruct.questkills[g, q, k].value)
                                            {
                                                PlayerStruct.questkills[members, g, q, k].kills += 1;
                                                SendData.sendActionMsg(members, lang.quest_defeat + " " + MapStruct.questkills[g, q, k].monstername + " " + PlayerStruct.questkills[members, g, q, k].kills + "/" + MapStruct.questkills[g, q, k].value, Globals.ColorGreen, NpcX, NpcY, 0, PlayerStruct.character[Attacker, PlayerStruct.player[Attacker].SelectedChar].Dir);
                                                SendData.sendQuestKill(members, g, q, k);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        int exp = NpcStruct.npc[Map, Victim].Exp;
                        if (PlayerRelations.isPlayerPremmy(members)) { exp = Convert.ToInt32(exp * 1.5); }
                        PlayerRelations.givePlayerExp(members, exp);
                    }
                }
            }
            //Não tem grupo para dividir a exp
            else
            {
                //Adiciona uma kill se houver uma quest para esse npc
                for (int g = 1; g < Globals.MaxQuestGivers; g++)
                {
                    for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                    {
                        //Prevent
                        if ((String.IsNullOrEmpty(MapStruct.quest[g, q].type)) && (PlayerStruct.queststatus[Attacker, g, q].status > 0)) { PlayerStruct.queststatus[Attacker, g, q].status = 0; return; }

                        //Execute
                        if ((PlayerStruct.queststatus[Attacker, g, q].status == 1) && (Convert.ToInt32(MapStruct.quest[g, q].type.Split('|')[0]) > 0))
                        {
                            for (int k = 1; k < Globals.MaxQuestKills; k++)
                            {
                                if (MapStruct.questkills[g, q, k].monstername == NpcStruct.npc[Map, Victim].Name)
                                {
                                    if (PlayerStruct.questkills[Attacker, g, q, k].kills < MapStruct.questkills[g, q, k].value)
                                    {
                                        PlayerStruct.questkills[Attacker, g, q, k].kills += 1;
                                        SendData.sendActionMsg(Attacker, lang.quest_defeat + " " + MapStruct.questkills[g, q, k].monstername + " " + PlayerStruct.questkills[Attacker, g, q, k].kills + "/" + MapStruct.questkills[g, q, k].value, Globals.ColorGreen, NpcX, NpcY, 0, PlayerStruct.character[Attacker, PlayerStruct.player[Attacker].SelectedChar].Dir);
                                        SendData.sendQuestKill(Attacker, g, q, k);
                                    }
                                }
                            }
                        }
                    }
                }
                int exp = NpcStruct.npc[Map, Victim].Exp;
                if (PlayerRelations.isPlayerPremmy(Attacker)) { exp = Convert.ToInt32(exp * 1.5); }
                PlayerRelations.givePlayerExp(Attacker, exp);
            }
        }
        //*********************************************************************************************
        // kickParty / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retira determinado jogador do grupo
        //*********************************************************************************************
        public static void kickParty(int s, int kicktarget, bool order = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, kicktarget, order) != null)
            {
                return;
            }

            //CÓDIGO
            //Tentativas possíveis de hacker
            if ((PlayerStruct.tempplayer[kicktarget].Party == 0) || (PlayerStruct.tempplayer[kicktarget].Party != PlayerStruct.tempplayer[s].Party)) { return; }

            if ((UserConnection.getS(kicktarget) < 0) || (UserConnection.getS(kicktarget) >= WinsockAsync.Clients.Count()))
            {
                SendData.sendMsgToPlayer(s, lang.player_kick_offline, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verifica se ele não saiu no processo
            if (!WinsockAsync.Clients[(UserConnection.getS(kicktarget))].IsConnected && (!order))
            {
                SendData.sendMsgToPlayer(s, lang.player_kick_offline, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Verificar se ele é lider para tirar outro jogador
            if ((PlayerStruct.party[PlayerStruct.tempplayer[s].Party].leader != s) && (kicktarget != s))
            {
                SendData.sendMsgToPlayer(s, lang.you_is_not_the_party_leader, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            //Vamos trabalhar com isso
            int partynum = PlayerStruct.tempplayer[s].Party;
            int members = 0;

            if (kicktarget == s)
            {
                //O id do jogador no grupo
                members = getPartyPlayers(partynum, s);

                //Reposicionar todos os membros no grupo se quem saiu for maior que 3
                if (members <= 3)
                {
                    for (int i = (members + 1); i < Globals.MaxPartyMembers; i++)
                    {
                        PlayerStruct.partymembers[partynum, i - 1].s = PlayerStruct.partymembers[partynum, i].s;
                        PlayerStruct.partymembers[partynum, i].s = 0;
                    }
                }
                else
                {
                    PlayerStruct.partymembers[partynum, 4].s = 0;
                }

                if (kicktarget == PlayerStruct.party[partynum].leader)
                {
                    PlayerStruct.party[partynum].leader = PlayerStruct.partymembers[partynum, 1].s;
                }

                //Tiramos o grupo do jogador
                PlayerStruct.tempplayer[s].Party = 0;
            }
            else
            {
                //O id do jogador no grupo
                members = getPartyPlayers(partynum, kicktarget);

                //Reposicionar todos os membros no grupo se quem saiu for maior que 3
                if (members <= 3)
                {
                    for (int i = (members + 1); i < Globals.MaxPartyMembers; i++)
                    {
                        PlayerStruct.partymembers[partynum, i - 1].s = PlayerStruct.partymembers[partynum, i].s;
                        PlayerStruct.partymembers[partynum, i].s = 0;
                    }
                }
                else
                {
                    PlayerStruct.partymembers[partynum, 4].s = 0;
                }

                //Tiramos o grupo do jogador
                PlayerStruct.tempplayer[kicktarget].Party = 0;
            }


            //Algum jogador ficou sozinho?
            if (getPartyMembersCount(partynum) == 1)
            {
                //Jogador que ficou sozinho será sempre o 1
                int alone = PlayerStruct.partymembers[partynum, 1].s;

                //Limpamos o grupo do jogador que ficou sozinho
                PlayerStruct.tempplayer[alone].Party = 0;

                //Limpamos o grupo
                PlayerStruct.party[partynum].leader = 0;
                PlayerStruct.party[partynum].active = false;

                //Avisa ao jogador que ele não tem mais um grupo
                SendData.sendPartyKick(alone);

                //Verifica se não é um kick por ordem do servidor
                if (!order)
                {
                    SendData.sendPartyKick(kicktarget);
                }

                //Limpamos todos os membros do grupo
                for (int i = (members + 1); i < Globals.MaxPartyMembers; i++)
                {
                    PlayerStruct.partymembers[partynum, i].s = 0;
                }
                return;
            }

            //Verifica se não é um kick por ordem do servidor
            if (!order)
            {
                SendData.sendPartyKick(kicktarget);
            }

            //Envia o grupo atualizado
            SendData.sendPartyDataToParty(partynum);
        }
        //*********************************************************************************************
        // getPartyMembersCount
        // Retorna o número de membros em um grupo
        //*********************************************************************************************
        public static int getPartyMembersCount(int partynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 1; i < Globals.MaxPartyMembers; i++)
            {
                if (PlayerStruct.partymembers[partynum, i].s > 0) { count += 1; }
            }

            return count;
        }
        //*********************************************************************************************
        // getPartyPlayers
        // Retorna o s do jogador com base no s do grupo
        //*********************************************************************************************
        public static int getPartyPlayers(int partynum, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, partynum, s));
            }

            //CÓDIGO
            int finals = 0;

            //Checa o slot que possúi o jogador
            for (int i = 1; i < Globals.MaxPartyMembers; i++)
            {
                if (PlayerStruct.partymembers[partynum, i].s == s)
                {
                    finals = i;
                    break;
                }
            }

            return finals;
        }
        //*********************************************************************************************
        // getPartyFree
        // Retorna um slot de grupo livre
        //*********************************************************************************************
        public static int getPartyFree()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int partynum = 0;

            //Checa um grupo livre
            for (int i = 1; i < Globals.MaxParty; i++)
            {
                if (PlayerStruct.party[i].active == false)
                {
                    partynum = i;
                    break;
                }
            }

            return partynum;
        }
    }
}
