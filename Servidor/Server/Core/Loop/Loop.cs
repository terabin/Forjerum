using System;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Responsável por toda a execução baseada em tempo no servidor.
    // Loops.cs
    //*********************************************************************************************
    class Loops : Languages.LStruct
    {
        public static Stopwatch TickCount = Stopwatch.StartNew();
        public static long save_Timer = 0;
        public static long Status_Timer = 0;
        public static long Update_Timer = 0;
        public static long Accept_Timer = 0;
        public static string Command = "";
        public static string Last_Packet = "";

        //*********************************************************************************************
        // BetaLoop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Trabalha com estatísticas sobre o servidor.
        //*********************************************************************************************
        public static void BetaLoop()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            long tmr600;
            long tmr200;
            long Tick;
            tmr200 = 0;
            tmr600 = 0;
            
            do
            {
                Tick = TickCount.ElapsedMilliseconds;

                //EXTEND
                if (Extensions.ExtensionApp.extendMyApp
                    ("beta_inject", Tick) != null)
                {
                    return;
                }

                if (tmr600 < Tick)
                {
                    for (int i = 0; i <= WinsockAsync.Clients.Count - 1; i++)
                    {
                        WinsockAsync.Clients[i].IsConnected = UserConnection.isConnected(i);
                    }
                    tmr600 = Tick + 600;
                }
                if (tmr200 < Tick)
                {

                    if (Status_Timer < TickCount.ElapsedMilliseconds)
                    {
                        Database.Handler.statusAdd(lang.automatic_log_server + " " + lang.created_in_date + " " + DateTime.Now.ToString());
                        Database.Handler.statusAdd(lang.exists + " " + WinsockAsync.Clients.Count + " " + lang.players_online);
                        Database.Handler.statusAdd(lang.last_receive_refresh_happened + " " + (TickCount.ElapsedMilliseconds - Update_Timer).ToString() + " ms");
                        Database.Handler.statusAdd(lang.last_connection_refresh_happened + " " + (TickCount.ElapsedMilliseconds - Accept_Timer).ToString() + " ms");
                        Database.Handler.statusAdd(lang.last_loop_command_is + " " + Command);
                        Database.Handler.statusAdd(lang.last_received_packet_is + " " + Last_Packet);
                        Status_Timer = TickCount.ElapsedMilliseconds + 300000;
                        Console.WriteLine(lang.new_log_status_generated);

                        //Escrever tudo o que há para ser escrito no log e status
                        Database.Handler.dischargeWriter();
                    }
                    tmr200 = Tick + 200;
                }
                Thread.Sleep(10);
            }
            while (true);
        }
        //*********************************************************************************************
        // AlphaLoop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Trabalha com informações direcionadas ao jogo e os jogadores.
        //*********************************************************************************************
        public static void AlphaLoop()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            long tmr100;
            long tmr256;
            long Tick;
            tmr100 = 0;
            tmr256 = 0;

            Database.Handler.defineAdmin();

            WinsockAsync.Log(lang.server_started_in_date + " " + DateTime.Now);

            do
            {
                Tick = TickCount.ElapsedMilliseconds;

                //EXTEND
                if (Extensions.ExtensionApp.extendMyApp
                    ("alpha_inject", Tick) != null)
                {
                    return;
                }

                //WinsockAsync.Listen();

                Command = "First For";
                for (int i = 1; i <= Globals.Player_Highs; i++)
                {
                    if (PlayerStruct.tempplayer[i].ingame) { PetRelations.petMove(i); }
                    if ((PlayerStruct.tempplayer[i].SORE) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].PVPPenalty < Loops.TickCount.ElapsedMilliseconds))
                    {
                        PlayerStruct.tempplayer[i].SORE = false;
                        PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].PVPPenalty = 0;
                        SendData.sendPlayerSoreToMap(i);
                        SendData.sendAnimation(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map, Globals.Target_Player, i, 148);
                    }
                    for (int p = 1; p < Globals.MaxPassiveEffects; p++)
                    {
                        if ((PlayerStruct.ppassiveffect[i, p].active) && (Tick > PlayerStruct.ppassiveffect[i, p].timer))
                        {
                            SkillRelations.executePassiveEffect(i, p);
                        }
                    }
                    if (PlayerStruct.tempplayer[i].RegenTimer < Tick)
                    {
                        PlayerRelations.playerRegen(i);
                    }
                    if ((PlayerStruct.tempplayer[i].preparingskill > 0) && (Tick > PlayerStruct.tempplayer[i].skilltimer))
                    {
                        PlayerLogic.ExecuteSkill(i);
                    }
                    //Dano sobre tempo
                    for (int s = 1; s < Globals.MaxNTempSpells; s++)
                    {
                        if ((PlayerStruct.ptempspell[i, s].active) && (Tick > PlayerStruct.ptempspell[i, s].timer))
                        {
                            SkillRelations.executeTempSpell(i, s);
                        }
                    }
                    if ((PlayerStruct.tempplayer[i].Stunned) && (PlayerStruct.tempplayer[i].StunTimer < Tick))
                    {
                        SendData.sendStun(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map, 1, i, 0);
                        PlayerStruct.tempplayer[i].Stunned = false;
                    }
                    if ((PlayerStruct.tempplayer[i].Sleeping) && (PlayerStruct.tempplayer[i].SleepTimer < Tick))
                    {
                        SendData.sendSleep(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map, 1, i, 0);
                        PlayerStruct.tempplayer[i].Sleeping = false;
                    }
                    if ((PlayerStruct.tempplayer[i].Blind) && (PlayerStruct.tempplayer[i].BlindTimer < Tick))
                    {
                        //SendData.sendStun(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map, 1, i, 0);
                        PlayerStruct.tempplayer[i].Blind = false;
                    }
                    if ((PlayerStruct.tempplayer[i].Slowed) && (PlayerStruct.tempplayer[i].SlowTimer < Tick))
                    {
                        if (PlayerStruct.tempplayer[i].isFrozen)
                        {
                            PlayerStruct.tempplayer[i].isFrozen = false;
                            SendData.sendFrozen(Globals.Target_Player, i);
                        }
                        PlayerStruct.tempplayer[i].Slowed = false;
                        PlayerStruct.tempplayer[i].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(Globals.Target_Player, i);
                    }
                }


                Command = "tmr100";
                if (tmr100 < Tick)
                {
                    for (int i = 0; i <= WinsockAsync.Clients.Count - 1; i++)
                        if (!WinsockAsync.Clients[i].IsConnected)
                        {
                            if (WinsockAsync.Clients[i].s >= 0)
                            {
                                WinsockAsync.disconnectUser(i);
                            }
                        }

                    for (int i = 0; i <= Globals.Max_WorkPoints - 1; i++)
                    {
                        if ((MapStruct.tempworkpoint[i].respawn > 0) && (Loops.TickCount.ElapsedMilliseconds > MapStruct.tempworkpoint[i].respawn))
                        {
                            MapStruct.tempworkpoint[i].vitality = MapStruct.workpoint[i].vitality;
                            MapStruct.tempworkpoint[i].respawn = 0;
                            SendData.sendEventGraphicToMap(MapStruct.workpoint[i].map, MapStruct.tile[MapStruct.workpoint[i].map, MapStruct.workpoint[i].x, MapStruct.workpoint[i].y].Event_Id, "", 0, Convert.ToByte(MapStruct.workpoint[i].active_sprite));
                        }
                    }

                    tmr100 = Tick + 100;
                }

                Command = "tmr256";

                if (tmr256 < Tick)
                {
                    for (int i = 0; i < Globals.MaxMaps; i++)
                    {
                        MapStruct.CheckMapItems(i);

                        if (MapStruct.tempmap[i].WarActive)
                        {
                            if (Tick > MapStruct.tempmap[i].WarTimer)
                            {
                                bool b = false;
                                for (int i2 = 0; i2 <= MapStruct.tempmap[i].NpcCount; i2++)
                                {
                                    if (NpcStruct.tempnpc[i, i2].guildnum > 0)
                                    {
                                        if (NpcStruct.tempnpc[i, i2].Target > 0)
                                        {
                                            MapStruct.tempmap[i].WarTimer += Tick + 20000;
                                            b = true;
                                        }
                                        NpcStruct.RegenNpc(i, i2, true);
                                    }
                                }
                                if (b) { break; }
                                SendData.sendMsgToGuild(MapStruct.map[i].guildnum, lang.the_colector_of + " " + MapStruct.map[i].name + " " + lang.has_survived, Globals.ColorYellow, Globals.Msg_Type_Server);
                                MapStruct.tempmap[i].WarActive = false;
                            }
                        }
                        Command = "NPC COUNT";
                        for (int i2 = 0; i2 <= MapStruct.tempmap[i].NpcCount; i2++)
                        {
                            if (i2 > 0)
                            {
                                Command = "TIME DAMAGE";
                                //Dano sobre tempo
                                for (int s = 1; s < Globals.MaxNTempSpells; s++)
                                {
                                    if ((NpcStruct.ntempspell[i, i2, s].active) && (Tick > NpcStruct.ntempspell[i, i2, s].timer))
                                    {
                                        NpcStruct.ExecuteTempSpell(i, i2, s);
                                    }
                                }

                                //Processamento do NPC
                                if (MapStruct.ExistPlayerInMap(i))
                                {
                                    Command = "NPC MOVE";
                                    NpcIA.CheckNpcMove(i, i2);
                                    if ((NpcStruct.tempnpc[i, i2].preparingskill > 0) && (Tick > NpcStruct.tempnpc[i, i2].skilltimer))
                                    {
                                        Command = "NPC EXECUTE SKILL";
                                        NpcIA.ExecuteSkill(i, i2);
                                    }
                                    if (NpcStruct.tempnpc[i, i2].RegenTimer < Tick)
                                    {
                                        Command = "REGEN NPC";
                                        NpcStruct.RegenNpc(i, i2);
                                    }
                                    if ((NpcStruct.tempnpc[i, i2].Stunned) && (NpcStruct.tempnpc[i, i2].StunTimer < Tick))
                                    {
                                        Command = "NPC STUN";
                                        SendData.sendStun(i, 2, i2, 0);
                                        NpcStruct.tempnpc[i, i2].Stunned = false;
                                    }
                                    if ((NpcStruct.tempnpc[i, i2].Sleeping) && (NpcStruct.tempnpc[i, i2].SleepTimer < Tick))
                                    {
                                        Command = "NPC SLEEP";
                                        SendData.sendSleep(i, 2, i2, 0);
                                        NpcStruct.tempnpc[i, i2].Sleeping = false;
                                    }
                                    if ((NpcStruct.tempnpc[i, i2].Blind) && (NpcStruct.tempnpc[i, i2].BlindTimer < Tick))
                                    {
                                        Command = "NPC BLIND";
                                        // A fazer SendData.sendStun(i, 2, i2, 0);
                                        NpcStruct.tempnpc[i, i2].Blind = false;
                                    }
                                    if ((NpcStruct.tempnpc[i, i2].Slowed) && (NpcStruct.tempnpc[i, i2].SlowTimer < Tick))
                                    {
                                        if (NpcStruct.tempnpc[i, i2].isFrozen)
                                        {
                                            NpcStruct.tempnpc[i, i2].isFrozen = false;
                                            SendData.sendFrozen(Globals.Target_Npc, i2, i);
                                        }
                                        NpcStruct.tempnpc[i, i2].Slowed = false;
                                        NpcStruct.tempnpc[i, i2].movespeed = Globals.NormalMoveSpeed;
                                        SendData.sendMoveSpeed(Globals.Target_Npc, i2, i);
                                    }
                                }
                                else
                                {
                                    Command = "NPC SUPER REGEN";
                                    NpcStruct.RegenNpc(i, i2, true);
                                    if (NpcStruct.tempnpc[i, i2].Target > 0) { NpcStruct.tempnpc[i, i2].Target = 0; }
                                }
                                Command = "NPC RESPAWN";
                                if ((NpcStruct.tempnpc[i, i2].Dead == true) && (Tick > NpcStruct.tempnpc[i, i2].RespawnTimer))
                                {
                                    //Respawn
                                    NpcIA.NpcRespawned(i, i2);
                                }
                            }
                        }
                        //for (int i2 = 0; i2 <= MapStruct.Ge; i2++)
                        // {
                        //}
                    }
                    tmr256 = Tick + 256;
                }
                long test1 = Tick;
                Command = "save Timer";
                if (save_Timer < TickCount.ElapsedMilliseconds)
                {                        //Salva o jogador SE PRECISAR
                    if (WinsockAsync.Clients.Count > 0)
                    {
                        for (int i = 0; i < WinsockAsync.Clients.Count; i++)
                        {
                            if (PlayerStruct.tempplayer[WinsockAsync.Clients[i].s].ingame)
                            {
                                Database.Characters.saveCharacter(WinsockAsync.Clients[i].s, PlayerStruct.player[WinsockAsync.Clients[i].s].Email, PlayerStruct.player[WinsockAsync.Clients[i].s].SelectedChar);
                                Database.Banks.saveBank(WinsockAsync.Clients[i].s);
                            }
                        }
                    }
                    save_Timer = TickCount.ElapsedMilliseconds + 600000;
                    Console.WriteLine(lang.server_has_been_saved);
                }

                Command = "TPP";
                if ((test1 - Tick) > 0) { Console.WriteLine("TPP: " + (test1 - Tick)); }
                Thread.Sleep(10);
            }
            while (true);
        }
    }
}
