using System;
using System.Reflection;
using System.Linq;

namespace __Forjerum
{
    class PetRelations
    {
        //*********************************************************************************************
        // PetMove
        //*********************************************************************************************
        public static void petMove(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].PetTimer > Loops.TickCount.ElapsedMilliseconds) { return; }
            if (PlayerStruct.tempplayer[s].isDead) { return; }

            string equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;
            string[] equipdata = equipment.Split(',');
            string[] petdata = equipdata[4].Split(';');

            int petnum = Convert.ToInt32(petdata[0]);
            int petlvl = Convert.ToInt32(petdata[1]);
            int petexp = Convert.ToInt32(petdata[2]);
            int Map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int targetx = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            int targety = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;
            int lasttarget = PlayerStruct.tempplayer[s].LastTarget;
            int lasttargettype = PlayerStruct.tempplayer[s].LastTargetType;
            int target = PlayerStruct.tempplayer[s].PetTarget;
            int targettype = PlayerStruct.tempplayer[s].PetTargetType;
            int DistanceX = 0;
            int DistanceY = 0;
            int n = 2;

            if (petnum <= 0) { return; }




            if ((targettype == Globals.Target_Npc))
            {
                if ((NpcStruct.tempnpc[Map, target].Dead) || (NpcStruct.tempnpc[Map, target].Vitality <= 0))
                {
                    PlayerStruct.tempplayer[s].PetTarget = 0;
                    PlayerStruct.tempplayer[s].PetTargetType = 0;
                    return;
                }
                DistanceX = NpcStruct.tempnpc[Map, target].X - targetx;
                DistanceY = NpcStruct.tempnpc[Map, target].Y - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    petAttack(s, target, Globals.Target_Npc);
                    return;
                }
            }

            if ((targettype == Globals.Target_Player))
            {
                //Verificar se o jogador não se desconectou no processo
                int clients = UserConnection.getS(target);
                if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
                {
                    PlayerStruct.tempplayer[s].PetTarget = 0;
                    PlayerStruct.tempplayer[s].PetTargetType = 0;
                    return;
                }
                if ((clients < 0) || (clients >= WinsockAsync.Clients.Count()))
                {
                    PlayerStruct.tempplayer[s].PetTarget = 0;
                    PlayerStruct.tempplayer[s].PetTargetType = 0;
                    return;
                }
                if (!WinsockAsync.Clients[clients].IsConnected)
                {
                    PlayerStruct.tempplayer[s].PetTarget = 0;
                    PlayerStruct.tempplayer[s].PetTargetType = 0;
                    return;
                }

                //Verificar se não está morto ou sem vida
                if ((PlayerStruct.tempplayer[target].isDead) || (PlayerStruct.tempplayer[target].Vitality <= 0))
                {
                    PlayerStruct.tempplayer[s].PetTarget = 0;
                    PlayerStruct.tempplayer[s].PetTargetType = 0;
                    return;
                }
                //Condições preventivas
                if ((PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map == Map) && (target != s))
                {
                    if ((PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].PVP) || (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP))
                    {
                        DistanceX = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X - targetx;
                        DistanceY = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y - targety;

                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                        //Verificar se está no alcance
                        if ((DistanceX <= n) && (DistanceY <= n))
                        {
                            petAttack(s, target, Globals.Target_Player);
                            return;
                        }
                    }
                }
            }

            if ((lasttargettype == Globals.Target_Npc) && (NpcStruct.tempnpc[Map, lasttarget].Vitality > 0))
            {
                DistanceX = NpcStruct.tempnpc[Map, lasttarget].X - targetx;
                DistanceY = NpcStruct.tempnpc[Map, lasttarget].Y - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    petAttack(s, lasttarget, Globals.Target_Npc);
                    return;
                }
            }

            if ((lasttargettype == Globals.Target_Player) && (PlayerStruct.tempplayer[lasttarget].Vitality > 0))
            {
                //Condições preventivas
                if ((PlayerStruct.character[lasttarget, PlayerStruct.player[lasttarget].SelectedChar].Map == Map) && (lasttarget != s))
                {
                    if ((PlayerStruct.character[lasttarget, PlayerStruct.player[lasttarget].SelectedChar].PVP) || (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP))
                    {
                        DistanceX = PlayerStruct.character[lasttarget, PlayerStruct.player[lasttarget].SelectedChar].X - targetx;
                        DistanceY = PlayerStruct.character[lasttarget, PlayerStruct.player[lasttarget].SelectedChar].Y - targety;

                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                        //Verificar se está no alcance
                        if ((DistanceX <= n) && (DistanceY <= n))
                        {
                            petAttack(s, lasttarget, Globals.Target_Player);
                            return;
                        }
                    }

                }
            }

            //Analisa todos os npcs do mapa
            for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
            {
                if (NpcStruct.tempnpc[Map, i].Vitality > 0)
                {
                    if (NpcStruct.tempnpc[Map, i].Target > 0)
                    {
                        DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                        DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                        //Verificar se está no alcance
                        if ((DistanceX <= n) && (DistanceY <= n))
                        {
                            petAttack(s, i, Globals.Target_Npc);
                            return;
                        }
                    }
                }
            }

            //Analisar todos os jogadores online
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if ((!PlayerStruct.tempplayer[i].isDead) && (PlayerStruct.tempplayer[i].Vitality > 0))
                {
                    //Condições preventivas
                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s))
                    {
                        if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Guild != PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild)
                        {
                            if (PlayerStruct.tempplayer[i].Party != PlayerStruct.tempplayer[s].Party)
                            {
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].PVP) || (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP))
                                {
                                    DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                    DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY <= n))
                                    {
                                        petAttack(s, i, Globals.Target_Player);
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
        // PetAttack
        //*********************************************************************************************
        public static void petAttack(int s, int target, int targettype)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, target, targettype) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.tempplayer[s].PetTarget = target;
            PlayerStruct.tempplayer[s].PetTargetType = targettype;
            if (targettype == Globals.Target_Npc)
            {
                CombatRelations.playerAttackNpc(s, target, 0, 0, false, 0, true);
            }
            else if (targettype == Globals.Target_Player)
            {
                CombatRelations.playerAttackPlayer(s, target, 0, 0, false, 0, 0, true);
            }
            SendData.sendPetAttack(s, target, targettype);
            PlayerStruct.tempplayer[s].PetTimer = Loops.TickCount.ElapsedMilliseconds + 2000;
        }
    }
}
