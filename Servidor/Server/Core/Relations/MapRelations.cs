using System;
using System.Reflection;

namespace __Forjerum
{
    class MapRelations : Languages.LStruct
    {
        //*********************************************************************************************
        // HaveToolToWork / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado jogador tem a ferramenta para interagir com determinado tipo de 
        // objeto
        //*********************************************************************************************
        public static bool haveToolToWork(int s, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type));
            }

            //CÓDIGO
            if (type == Globals.Job_Miner)
            {
                if (EquipmentRelations.getPlayerWeapon(s) == 28)
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // playerAttackWorkPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Interação com objetos de profissão
        //*********************************************************************************************
        public static void playerAttackWorkPoint(int s, int workpoint)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, workpoint) != null)
            {
                return;
            }

            //CÓDIGO
            if (MapStruct.tempworkpoint[workpoint].vitality <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.resource_empty, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            int damage = 0;
            int profnum = 0;


            profnum = PlayerRelations.getPlayerProf(s, MapStruct.workpoint[workpoint].type);

            if (profnum <= 0)
            {
                SendData.sendMsgToPlayer(s, lang.dont_have_prof_to_explore, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            if (!haveToolToWork(s, MapStruct.workpoint[workpoint].type))
            {
                SendData.sendMsgToPlayer(s, lang.dont_have_tool_to_interact, Globals.ColorRed, Globals.Msg_Type_Server);
                return;
            }

            damage = 1 + Convert.ToInt32(Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum] * 0.5));
            SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, Globals.Target_Player, s, WeaponStruct.weapon[28].animation_id);

            if (damage == 0) { damage = 1; }

            MapStruct.tempworkpoint[workpoint].vitality -= 1;
            SendData.sendActionMsg(s, "-" + damage, Globals.ColorWhite, MapStruct.workpoint[workpoint].x, MapStruct.workpoint[workpoint].y, 1, 0, MapStruct.workpoint[workpoint].map);

            if (MapStruct.tempworkpoint[workpoint].vitality <= 0)
            {
                InventoryRelations.giveItem(s, 1, MapStruct.workpoint[workpoint].reward, 1, 0, 0);
                MapStruct.tempworkpoint[workpoint].respawn = Loops.TickCount.ElapsedMilliseconds + (MapStruct.workpoint[workpoint].respawn_timer * 10000);
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum] += MapStruct.workpoint[workpoint].exp;
                //Verificamos se ele subiu de nível
                if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum] >= LevelRelations.getpProfExpToNextLevel(s, profnum)) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum] < 80))
                {
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[profnum] -= LevelRelations.getpProfExpToNextLevel(s, profnum);
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[profnum] += 1;
                    SendData.sendProfLEVEL(s, profnum);
                }
                else
                {
                    //GetExpToNextLevel
                    SendData.sendProfEXP(s, profnum);
                    // SendData.sendPlayerExp(members);
                    //Enviamos uma animação bonitinha de exp :D
                    SendData.sendActionMsg(s, "+" + MapStruct.workpoint[workpoint].exp + lang.pexp, 0, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, MapStruct.workpoint[workpoint].map);
                }
                SendData.sendEventGraphicToMap(MapStruct.workpoint[workpoint].map, MapStruct.tile[MapStruct.workpoint[workpoint].map, MapStruct.workpoint[workpoint].x, MapStruct.workpoint[workpoint].y].Event_Id, "", 0, 49);
                SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            }
        }
        //*********************************************************************************************
        // getOpenProf
        // Retorna slot de profissão livre
        //*********************************************************************************************
        public static int getOpenProf(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            //Limpa slot de troca
            for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
            {
                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[i] == 0)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
