using System;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados aos mapas.
    // MStruct.cs
    //*********************************************************************************************
    class MStruct
    {
        //*********************************************************************************************
        // ESTRUTURA DOS MAPAS
        // Possui informações como localizações de bancos, baús, áreas de craft, e objetos de interação
        // para profissões, junto com pontos onde o jogador pode salvar a posição ou se teleportar, além
        // de claros algumas informações como onde estão determinados itens no mapa e informações gerais
        // sobre missões e receitas.
        //*********************************************************************************************
        public static MStruct.Map[] map = new MStruct.Map[Globals.MaxMaps];
        public static MStruct.Tile[,,] tile = new MStruct.Tile[Globals.MaxMaps, Globals.MaxMapsX, Globals.MaxMapsY];
        public static MStruct.TempMap[] tempmap = new MStruct.TempMap[Globals.MaxMaps];
        public static MStruct.MapItem[,] mapitem = new MStruct.MapItem[Globals.MaxMaps, Globals.MaxMapItems];
        public static MStruct.CraftPoint[] craftpoint = new MStruct.CraftPoint[Globals.Max_CraftPoints];
        public static MStruct.CraftRecipe[,,] craftrecipe = new MStruct.CraftRecipe[4, Globals.Max_CraftRecipes, Globals.Max_Craft];
        public static MStruct.WorkPoint[] workpoint = new MStruct.WorkPoint[Globals.Max_WorkPoints];
        public static MStruct.ChestPoint[] chestpoint = new MStruct.ChestPoint[Globals.Max_Chests];
        public static MStruct.BankPoint[] bankpoint = new MStruct.BankPoint[Globals.Max_BankPoints];
        public static MStruct.TpPoint[] tppoint = new MStruct.TpPoint[Globals.Max_TpPoints];
        public static MStruct.SavePoint[] savepoint = new MStruct.SavePoint[Globals.Max_SavePoints];
        public static MStruct.TempWorkPoint[] tempworkpoint = new MStruct.TempWorkPoint[Globals.Max_WorkPoints];
        public static MStruct.QuestGiver[] questgiver = new MStruct.QuestGiver[Globals.MaxQuestGivers];
        public static MStruct.Quest[,] quest = new MStruct.Quest[Globals.MaxQuestGivers, Globals.MaxQuestPerGiver];
        public static MStruct.QuestKills[,,] questkills = new MStruct.QuestKills[Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestKills];
        public static MStruct.QuestActions[,,] questactions = new MStruct.QuestActions[Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestActions];
        public static MStruct.QuestItems[,,] questitems = new MStruct.QuestItems[Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestItems];
        public static MStruct.QuestRewards[,,] questrewards = new MStruct.QuestRewards[Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestRewards];

        public struct Map
        {
            public string name;
            public string max_width;
            public string max_height;
            public int guildnum;
            public int guildgold;
            public string guildmember;
        }

        public struct MapItem
        {
            public int ItemNum;
            public int ItemType;
            public int Value;
            public int X;
            public int Y;
            public int Refin;
            public int Exp;
            public long Timer;
        }

        public struct TpPoint
        {
            public int map;
            public int cost;
            public int count;
            public int[] tp_map;
            public byte[] tp_x;
            public byte[] tp_y;
        }

        public struct SavePoint
        {
            public int map;
            public int save_map;
            public byte save_x;
            public byte save_y;
        }
        //dir
        //public static byte DirUp = 8;
        //public static byte DirDown = 2;
        //public static byte DirLeft = 4;
        //public static byte DirRight = 6;

        public struct Tile
        {
            public string Data1;
            public string Data2;
            public string Data3;
            public string Data4;
            public string DownBlock;
            public string LeftBlock;
            public string RightBlock;
            public string UpBlock;
            public int Event_Id;
        }

        public struct QuestGiver
        {
            public int map;
            public int x;
            public int y;
            public int quest_count;
        }

        public struct CraftPoint
        {
            public int map;
            public int type;
        }

        public struct BankPoint
        {
            public int map;
        }

        public struct WorkPoint
        {
            public int map;
            public int x;
            public int y;
            public int req_tool;
            public int reward;
            public int vitality;
            public int respawn_timer;
            public int exp;
            public int type;
            public int active_sprite;
            public int inactive_sprite;
            public int level_req;
        }

        public struct ChestPoint
        {
            public int map;
            public int x;
            public int y;
            public int reward_count;
            public string[] reward;
            public int key;
            public string active_sprite;
            public int active_sprite_index;
            public string inactive_sprite;
            public int inactive_sprite_index;
            public bool is_random;
        }

        public struct TempWorkPoint
        {
            public int vitality;
            public long respawn;
        }

        public struct CraftRecipe
        {
            public int type;
            public int num;
            public int value;
            public int refin;
        }

        public struct Quest
        {
            public string type;
            public int killvalue;
            public int actionvalue;
            public int itemvalue;
            public int rewardvalue;
            public int gold;
            public int exp;
        }

        public struct QuestItems
        {
            public string item;
        }

        public struct QuestRewards
        {
            public string item;
        }


        public struct QuestKills
        {
            public string monstername;
            public int value;
        }

        public struct QuestActions
        {
            public int type;
            public string data;
        }

        public struct TempMap
        {
            public int NpcCount;
            public bool WarActive;
            public long WarTimer;
        }
        //*********************************************************************************************
        // MapIsPVP / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna se determinado mapa é livre para o PVP.
        //*********************************************************************************************
        public static bool MapIsPVP(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum));
            }

            //CÓDIGO
            if ((mapnum == 22) || (mapnum == 1) || (mapnum == 2) || (mapnum == 3) || (mapnum == 4) || 
                (mapnum == 5) || (mapnum == 12) || (mapnum == 149) || (mapnum == 19) || (mapnum == 147) || 
                (mapnum == 146) || (mapnum == 136) || (mapnum == 22) || (mapnum == 148) || (mapnum == 137) || 
                (mapnum == 138) || (mapnum == 139) || (mapnum == 140) || (mapnum == 141) || (mapnum == 142) ||
                (mapnum == 143) || (mapnum == 144) || (mapnum == 6) || (mapnum == 20) || (mapnum == 132) ||
                (mapnum == 133) || (mapnum == 145) || (mapnum == 46) || (mapnum == 130) || (mapnum == 70) ||
                (mapnum == 128) || (mapnum == 129) || (mapnum == 135) || (mapnum == 77) || (mapnum == 126) ||
                (mapnum == 127) || (mapnum == 94) || (mapnum == 62) || (mapnum == 63) || (mapnum == 75) ||
                (mapnum == 123) || (mapnum == 84))
            {
                return false;
            }
            return true;
        }
        //*********************************************************************************************
        // GetOpenWorkPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array de interação livre.
        //*********************************************************************************************
        public static int GetOpenWorkPoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int worknum = 0;

            for (int i = 1; i < Globals.Max_WorkPoints; i++)
            {
                if (MStruct.workpoint[i].map == 0)
                {
                    worknum = i;
                    break;
                }
            }
            return worknum;
        }
        //*********************************************************************************************
        // GetOpenChestPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array de baú livre.
        //*********************************************************************************************
        public static int GetOpenChestPoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int chestnum = 0;

            for (int i = 1; i < Globals.Max_Chests; i++)
            {
                if (MStruct.chestpoint[i].map == 0)
                {
                    chestnum = i;
                    break;
                }
            }
            return chestnum;
        }
        //*********************************************************************************************
        // GetOpenBankPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array para bancos livre.
        //*********************************************************************************************
        public static int GetOpenBankPoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int banknum = 0;

            for (int i = 1; i < Globals.Max_BankPoints; i++)
            {
                if (MStruct.bankpoint[i].map == 0)
                {
                    banknum = i;
                    break;
                }
            }
            return banknum;
        }
        //*********************************************************************************************
        // GetOpenTpPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array de ponto de teleporte livre.
        //*********************************************************************************************
        public static int GetOpenTpPoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int tpnum = 0;

            for (int i = 1; i < Globals.Max_TpPoints; i++)
            {
                if (MStruct.tppoint[i].map == 0)
                {
                    tpnum = i;
                    break;
                }
            }
            return tpnum;
        }
        //*********************************************************************************************
        // GetOpenSavePoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array para save point livre.
        //*********************************************************************************************
        public static int GetOpenSavePoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int savenum = 0;

            for (int i = 1; i < Globals.Max_SavePoints; i++)
            {
                if (MStruct.savepoint[i].map == 0)
                {
                    savenum = i;
                    break;
                }
            }
            return savenum;
        }
        //*********************************************************************************************
        // GetOpenCraftPoint / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna array para criação de itens livre.
        //*********************************************************************************************
        public static int GetOpenCraftPoint()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int craftnum = 0;

            for (int i = 1; i < Globals.Max_CraftPoints; i++)
            {
                if (MStruct.craftpoint[i].map == 0)
                {
                    craftnum = i;
                    break;
                }
            }
            return craftnum;
        }
        //*********************************************************************************************
        // GetNullMapItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetNullMapItem(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map));
            }

            //CÓDIGO
            int itemnum = 0;

            for (int i = 1; i < Globals.MaxMapItems; i++)
            {
                if (MStruct.mapitem[map, i].ItemNum == 0)
                {
                    itemnum = i;
                    break;
                }
            }
            return itemnum;
        }
        //*********************************************************************************************
        // GetMapItemMaxIndex / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetMapItemMaxindex(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 1; i < Globals.MaxMapItems; i++)
            {
                if (MStruct.mapitem[map, i].ItemNum > 0)
                {
                    count = i;
                }
            }
            return count;
        }
        //*********************************************************************************************
        // GetMapNpcSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetMapNpcSlot(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 1; i < Globals.MaxMapNpcs; i++)
            {
                if (String.IsNullOrEmpty(NStruct.npc[map, i].Name))
                {
                    return i;
                }
            }
            return count;
        }
        //*********************************************************************************************
        // GetMapNpcCount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna o número de npcs em determinado mapa.
        //*********************************************************************************************
        public static int GetMapNpcCount(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 0; i < Globals.MaxMapNpcs; i++)
            {
                if (!String.IsNullOrEmpty(NStruct.npc[map, i].Name))
                {
                    count += 1;
                }
            }
            return count;
        }
        //*********************************************************************************************
        // ExistPlayerInMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna se existe um jogador em determinado mapa.
        //*********************************************************************************************
        public static bool ExistPlayerInMap(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map));
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if ((PStruct.tempplayer[i].ingame) && (PStruct.character[i, PStruct.player[i].SelectedChar].Map == map))
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // CheckMapItems / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void CheckMapItems(int map)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxMapItems; i++)
            {
                if ((MStruct.mapitem[map, i].ItemNum != 0) && (MStruct.mapitem[map,i].Timer < Loops.TickCount.ElapsedMilliseconds))
                {
                    MStruct.mapitem[map, i].Value = 0;
                    MStruct.mapitem[map, i].ItemType = 0;
                    MStruct.mapitem[map, i].X = 0;
                    MStruct.mapitem[map, i].Y = 0;
                    MStruct.mapitem[map, i].ItemNum = 0;
                    MStruct.mapitem[map, i].Refin = 0;
                    MStruct.mapitem[map, i].Exp = 0;
                    MStruct.mapitem[map, i].Timer = 0;
                    SendData.Send_MapItem(map, i);
                }
            }
        }
    }
}
