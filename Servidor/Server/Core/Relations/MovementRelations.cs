using System;
using System.Reflection;

namespace __Forjerum
{
    class MovementRelations : PlayerStruct
    {
        //*********************************************************************************************
        // CanPlayerMove / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado jogador pode se mover no contexto em que está atualmente
        //*********************************************************************************************
        public static bool canPlayerMove(int s, byte Dir, int x = 0, int y = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Dir, x, y) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Dir, x, y));
            }

            //CÓDIGO
            int map = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            if (x <= 0 || y <= 0)
            {
                x = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
                y = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
            }

            //Tentamos nos mover
            switch (Dir)
            {
                case 8:
                    if (y - 1 < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].UpBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y - 1].DownBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x, y - 1].Data1 == "3") || (MapStruct.tile[map, x, y - 1].Data1 == "10")) { return false; }
                    if (MapStruct.tile[map, x, y - 1].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            playerMove(s, Convert.ToByte(Convert.ToInt32(MapStruct.tile[map, x, y - 1].Data2)));
                            return false;
                        }
                    }
                    break;
                case 2:
                    if (y + 1 > 14)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].DownBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y + 1].UpBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x, y + 1].Data1 == "3") || (MapStruct.tile[map, x, y + 1].Data1 == "10")) { return false; }
                    if (MapStruct.tile[map, x, y + 1].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            playerMove(s, Convert.ToByte(Convert.ToInt32(MapStruct.tile[map, x, y + 1].Data2)));
                            return false;
                        }
                    }
                    break;
                case 4:
                    if (x - 1 < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].LeftBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x - 1, y].RightBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x - 1, y].Data1 == "3") || (MapStruct.tile[map, x - 1, y].Data1 == "10")) { return false; }
                    if (MapStruct.tile[map, x - 1, y].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            playerMove(s, Convert.ToByte(Convert.ToInt32(MapStruct.tile[map, x - 1, y].Data2)));
                            return false;
                        }
                    }
                    break;
                case 6:
                    if (x + 1 > 19)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].RightBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x + 1, y].LeftBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x + 1, y].Data1 == "3") || (MapStruct.tile[map, x + 1, y].Data1 == "10")) { return false; }
                    if (MapStruct.tile[map, x + 1, y].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            playerMove(s, Convert.ToByte(Convert.ToInt32(MapStruct.tile[map, x + 1, y].Data2)));
                            return false;
                        }
                    }
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }

            return true;
        }
        //*********************************************************************************************
        // PlayerMove / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Move determinado jogador para determinada posição
        //*********************************************************************************************
        public static void playerMove(int s, byte Dir)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Dir) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].Warping) { return; }
            //Tentamos nos mover
            switch (Dir)
            {
                case 8:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y) - 1);
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirUp;
                    break;
                case 2:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y) + 1);
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirDown;
                    break;
                case 4:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) - 1);
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirLeft;
                    break;
                case 6:
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) + 1);
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir = Globals.DirRight;
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }

            int map = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            int x = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
            int y = Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
            //Verifica os tipos de tiles
            if (MapStruct.tile[map, x, y].Data1 == "2")
            {
                PlayerStruct.tempplayer[s].Warping = true;
                playerWarp(s, Convert.ToInt32(MapStruct.tile[map, x, y].Data2), Convert.ToByte(MapStruct.tile[map, x, y].Data3), Convert.ToByte(MapStruct.tile[map, x, y].Data4));
                return;
            }

            //Se nenhum tile tem ação, enviar as novas coordenadas do jogador após o movimento 
            SendData.sendPlayerXY(s);
            SendData.sendPlayerDir(s, 1);
        }
        //*********************************************************************************************
        // PlayerWarp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Move o jogador para outro mapa, importante perceber que tudo deve ser atualizado para ele e
        // para quem está no outro mapa.
        //*********************************************************************************************
        public static void playerWarp(int s, int Map, byte X, byte Y)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Map, X, Y) != null)
            {
                return;
            }

            //CÓDIGO
            //Salvamos o mapa antigo
            int oldmap = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;

            if (Map == oldmap)
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = X;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Y;
                SendData.sendPlayerWarp(s);
                SendData.sendPlayerXY(s);
                SendData.sendPlayerDeathToMap(s);
                PlayerStruct.tempplayer[s].Warping = false;
                return;
            }

            //Definimos as novas coordenadas do jogador
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map = Map;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = X;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Y;

            //Valores sobre magias
            if (PlayerStruct.tempplayer[s].preparingskill > 0)
            {
                PlayerStruct.tempplayer[s].preparingskill = 0;
                PlayerStruct.tempplayer[s].skilltimer = 0;
                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                SendData.sendBrokeSkill(s);
                SendData.sendMoveSpeed(1, s);
            }

            //Enviamos o jogador ao novo mapa
            SendData.sendPlayerWarp(s);
            SendData.sendPlayerDataToMapBut(s, PlayerStruct.player[s].Username, PlayerStruct.player[s].SelectedChar);
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map)
                    if (i != s)
                    {
                        {
                            SendData.sendPlayerDataTo(s, i, PlayerStruct.player[i].Username, PlayerStruct.player[i].SelectedChar);
                            SendData.sendGuildTo(s, i);
                            SendData.sendPlayerSoreTo(s, i);
                            SendData.sendPlayerPvpTo(s, i);
                            SendData.sendPlayerShoppingTo(s, i);
                            if (PlayerStruct.tempplayer[i].Stunned) { SendData.sendStun(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 1, i, 1); }
                            if (PlayerStruct.tempplayer[i].Sleeping) { SendData.sendSleep(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 1, i, 1); }
                            if (PlayerStruct.tempplayer[i].isDead) { SendData.sendPlayerDeathTo(s, i); }
                            //SendData.sendPlayerMoveSpeedTo(s, i);
                        }
                    }
            }

            for (int i = 0; i <= Globals.Max_WorkPoints - 1; i++)
            {
                if (MapStruct.workpoint[i].map == Map)
                {
                    if ((MapStruct.tempworkpoint[i].vitality <= 0))
                    {
                        SendData.sendEventGraphicToMap(MapStruct.workpoint[i].map, MapStruct.tile[MapStruct.workpoint[i].map, MapStruct.workpoint[i].x, MapStruct.workpoint[i].y].Event_Id, "", 0, Convert.ToByte(MapStruct.workpoint[i].inactive_sprite));
                    }
                }
            }

            for (int i = 0; i <= Globals.Max_Chests - 1; i++)
            {
                if (MapStruct.chestpoint[i].map == Map)
                {
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i])
                    {
                        SendData.sendEventGraphic(s, MapStruct.tile[MapStruct.chestpoint[i].map, MapStruct.chestpoint[i].x, MapStruct.chestpoint[i].y].Event_Id, MapStruct.chestpoint[i].inactive_sprite, MapStruct.chestpoint[i].inactive_sprite_s, 0, 8);
                    }
                }
            }

            //????
            SendData.sendMapGuildTo(s);
            SendData.sendPlayerSkills(s);
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
            SendData.sendPlayerVitalityToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Vitality);
            SendData.sendGuildToMapBut(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s);
            SendData.sendCompleteGuild(s);
            SendData.sendPlayerPvpToMap(s);
            SendData.sendPlayerSoreToMap(s);
            SendData.sendPlayerExtraVitalityToMap(s);
            SendData.sendPlayerExtraSpiritToMap(s);
            //SendData.sendPlayerMoveSpeedToMapBut(s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s);

            //Enviamos os npcs do novo mapa
            SendData.sendMapNpcsTo(s);
            SendData.sendMapItems(s);

            //Avisamos aos jogadores do antigo mapa que ele saiu
            SendData.sendPlayerLeft(oldmap, s);

            PlayerStruct.tempplayer[s].Warping = false;
        }
        //*********************************************************************************************
        // CanThrowPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado pode ser empurrado em determinada direção e distância
        //*********************************************************************************************
        public static bool canThrowPlayer(int s, byte Dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Dir, range) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, Dir, range));
            }

            //CÓDIGO
            int map = character[s, player[s].SelectedChar].Map;
            int x = character[s, player[s].SelectedChar].X;
            int y = character[s, player[s].SelectedChar].Y;
            //Tentamos nos mover
            switch (Dir)
            {
                case 8:
                    if (y - range < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].UpBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y - range].DownBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y - (range - 1)].UpBlock) == false) { return false; }

                    if ((MapStruct.tile[map, x, y - range].Data1 == "3") || (MapStruct.tile[map, x, y - range].Data1 == "10") || (MapStruct.tile[map, x, y - range].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x, y - range].Data1 == "17") || (MapStruct.tile[map, x, y - range].Data1 == "18")) { return false; }
                    if (MapStruct.tile[map, x, y - range].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            return false;
                        }
                    }
                    break;
                case 2:
                    if (y + range > 14)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].DownBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y + range].UpBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y + (range - 1)].DownBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x, y + range].Data1 == "3") || (MapStruct.tile[map, x, y + range].Data1 == "10") || (MapStruct.tile[map, x, y + range].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x, y + range].Data1 == "17") || (MapStruct.tile[map, x, y + range].Data1 == "18")) { return false; }
                    if (MapStruct.tile[map, x, y + range].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            return false;
                        }
                    }
                    break;
                case 4:
                    if (x - range < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].LeftBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x - range, y].RightBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x - (range - 1), y].LeftBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x - range, y].Data1 == "3") || (MapStruct.tile[map, x - range, y].Data1 == "10") || (MapStruct.tile[map, x - range, y].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x - range, y].Data1 == "17") || (MapStruct.tile[map, x - range, y].Data1 == "18")) { return false; }
                    if (MapStruct.tile[map, x - range, y].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            return false;
                        }
                    }
                    break;
                case 6:
                    if (x + range > 19)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].RightBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x + range, y].LeftBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x + (range - 1), y].RightBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x + range, y].Data1 == "3") || (MapStruct.tile[map, x + range, y].Data1 == "10") || (MapStruct.tile[map, x + range, y].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x + range, y].Data1 == "17") || (MapStruct.tile[map, x + range, y].Data1 == "18")) { return false; }
                    if (MapStruct.tile[map, x + range, y].Data1 == "21")
                    {
                        if (!PlayerRelations.isPlayerPremmy(s))
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }


            return true;
        }
        //*********************************************************************************************
        // ThrowPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Empurra determinado jogador para determinada direção e distância
        //*********************************************************************************************
        public static void throwPlayer(int s, int dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, dir, range) != null)
            {
                return;
            }

            //CÓDIGO
            int map = character[s, player[s].SelectedChar].Map;
            int x = character[s, player[s].SelectedChar].X;
            int y = character[s, player[s].SelectedChar].Y;

            switch (dir)
            {
                case 8:
                    character[s, player[s].SelectedChar].Y = Convert.ToByte(Convert.ToInt32(character[s, player[s].SelectedChar].Y) - range);
                    break;
                case 2:
                    character[s, player[s].SelectedChar].Y = Convert.ToByte(Convert.ToInt32(character[s, player[s].SelectedChar].Y) + range);
                    break;
                case 4:
                    character[s, player[s].SelectedChar].X = Convert.ToByte(Convert.ToInt32(character[s, player[s].SelectedChar].X) - range);
                    break;
                case 6:
                    character[s, player[s].SelectedChar].X = Convert.ToByte(Convert.ToInt32(character[s, player[s].SelectedChar].X) + range);
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }

            SendData.sendKnockBack(map, 1, s, dir, range);
        }
        //*********************************************************************************************
        // CanThrowNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado NPC pode ser empurrado para determinada direção e distância
        //*********************************************************************************************
        public static bool canThrowNpc(int map, int s, byte Dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, s, Dir, range) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, s, Dir, range));
            }

            //CÓDIGO
            int x = NpcStruct.tempnpc[map, s].X;
            int y = NpcStruct.tempnpc[map, s].Y;

            //Tentamos nos mover
            switch (Dir)
            {
                case 8:
                    if (y - range < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].UpBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y - range].DownBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x, y - range].Data1 == "3") || (MapStruct.tile[map, x, y - range].Data1 == "10") || (MapStruct.tile[map, x, y - range].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x, y - range].Data1 == "17") || (MapStruct.tile[map, x, y - range].Data1 == "18")) { return false; }
                    break;
                case 2:
                    if (y + range > 14)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].DownBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y + range].UpBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x, y + range].Data1 == "3") || (MapStruct.tile[map, x, y + range].Data1 == "10") || (MapStruct.tile[map, x, y + range].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x, y + range].Data1 == "17") || (MapStruct.tile[map, x, y + range].Data1 == "18")) { return false; }
                    break;
                case 4:
                    if (x - range < 0)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].LeftBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x - range, y].RightBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x - range, y].Data1 == "3") || (MapStruct.tile[map, x - range, y].Data1 == "10") || (MapStruct.tile[map, x - range, y].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x - range, y].Data1 == "17") || (MapStruct.tile[map, x - range, y].Data1 == "18")) { return false; }
                    break;
                case 6:
                    if (x + range > 19)
                    {
                        return false;
                    }
                    if (Convert.ToBoolean(MapStruct.tile[map, x, y].RightBlock) == false) { return false; }
                    if (Convert.ToBoolean(MapStruct.tile[map, x + range, y].LeftBlock) == false) { return false; }
                    if ((MapStruct.tile[map, x + range, y].Data1 == "3") || (MapStruct.tile[map, x + range, y].Data1 == "10") || (MapStruct.tile[map, x + range, y].Data1 == "2")) { return false; }
                    if ((MapStruct.tile[map, x + range, y].Data1 == "17") || (MapStruct.tile[map, x + range, y].Data1 == "18")) { return false; }
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }


            return true;
        }
        //*********************************************************************************************
        // ThrowNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Empurra determinado NPC para determinada direção e distância
        //*********************************************************************************************
        public static void throwNpc(int Map, int s, int dir, int range)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s, dir, range) != null)
            {
                return;
            }

            //CÓDIGO
            switch (dir)
            {
                case 8:
                    NpcStruct.tempnpc[Map, s].Y = Convert.ToByte(NpcStruct.tempnpc[Map, s].Y - range);
                    break;
                case 2:
                    NpcStruct.tempnpc[Map, s].Y = Convert.ToByte(NpcStruct.tempnpc[Map, s].Y + range);
                    break;
                case 4:
                    NpcStruct.tempnpc[Map, s].X = Convert.ToByte(NpcStruct.tempnpc[Map, s].X - range);
                    break;
                case 6:
                    NpcStruct.tempnpc[Map, s].X = Convert.ToByte(NpcStruct.tempnpc[Map, s].X + range);
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    break;
            }

            SendData.sendKnockBack(Map, 2, s, dir, range);
        }
    }
}
