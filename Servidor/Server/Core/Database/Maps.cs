using System;
using System.Reflection;
using System.Linq;
using System.IO;

namespace __Forjerum.Database
{
    class Maps
    {
        //*********************************************************************************************
        // saveMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool save(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, mapnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            //Salvamos o tamanho do mapa primeiro
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].name);
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].max_width);
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].max_height);
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].guildnum);
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].guildgold);
            bw.Write(MapStruct.map[Convert.ToInt32(mapnum)].guildmember);

            //Salvamos os tiles em seguida
            for (int x = 0; x <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                for (int y = 0; y <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                {

                    {
                        bw.Write(MapStruct.tile[mapnum, x, y].Event_Id);
                        bw.Write(MapStruct.tile[mapnum, x, y].Data1);
                        bw.Write(MapStruct.tile[mapnum, x, y].Data2);
                        bw.Write(MapStruct.tile[mapnum, x, y].Data3);
                        bw.Write(MapStruct.tile[mapnum, x, y].Data4);
                        bw.Write(MapStruct.tile[mapnum, x, y].DownBlock);
                        bw.Write(MapStruct.tile[mapnum, x, y].LeftBlock);
                        bw.Write(MapStruct.tile[mapnum, x, y].RightBlock);
                        bw.Write(MapStruct.tile[mapnum, x, y].UpBlock);
                    }

                }

            bw.Close();
            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // loadMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool load(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, mapnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Maps/" + mapnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                //Lê o tamanho do mapa
                MapStruct.map[Convert.ToInt32(mapnum)].name = br.ReadString();
                MapStruct.map[Convert.ToInt32(mapnum)].max_width = br.ReadString();
                MapStruct.map[Convert.ToInt32(mapnum)].max_height = br.ReadString();
                MapStruct.map[Convert.ToInt32(mapnum)].guildnum = br.ReadInt32();
                MapStruct.map[Convert.ToInt32(mapnum)].guildgold = br.ReadInt32();
                MapStruct.map[Convert.ToInt32(mapnum)].guildmember = br.ReadString();

                //Carregamos os tiles em seguida
                for (int x = 0; x <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                    for (int y = 0; y <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                    {
                        {
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].Event_Id = br.ReadInt32();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2 = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data3 = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data4 = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].DownBlock = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].LeftBlock = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].RightBlock = br.ReadString();
                            MapStruct.tile[Convert.ToInt32(mapnum), x, y].UpBlock = br.ReadString();

                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "30")
                            {
                                int e = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);
                                string[] notedata = EnemyStruct.enemy[e].note.Split(',');
                                for (int i2 = 1; i2 <= Globals.MaxMapNpcs; i2++)
                                {
                                    if (String.IsNullOrEmpty(NpcStruct.npc[mapnum, i2].Name))
                                    {
                                        NpcStruct.npc[mapnum, i2].Name = EnemyStruct.enemy[e].battler_name;
                                        NpcStruct.npc[mapnum, i2].Map = mapnum;
                                        NpcStruct.npc[mapnum, i2].X = x;
                                        NpcStruct.npc[mapnum, i2].Y = y;
                                        NpcStruct.npc[mapnum, i2].Vitality = Convert.ToInt32(EnemyStruct.enemyparams[e, 0].value);
                                        NpcStruct.npc[mapnum, i2].Spirit = Convert.ToInt32(EnemyStruct.enemyparams[e, 1].value);
                                        NpcStruct.tempnpc[mapnum, i2].Target = 0;
                                        NpcStruct.tempnpc[mapnum, i2].X = x;
                                        NpcStruct.tempnpc[mapnum, i2].Y = y;
                                        NpcStruct.tempnpc[mapnum, i2].curTargetX = NpcStruct.npc[mapnum, i2].X;
                                        NpcStruct.tempnpc[mapnum, i2].curTargetY = NpcStruct.npc[mapnum, i2].Y;
                                        NpcStruct.tempnpc[mapnum, i2].Vitality = NpcStruct.npc[mapnum, i2].Vitality;
                                        NpcStruct.npc[mapnum, i2].Attack = Convert.ToInt32(EnemyStruct.enemyparams[e, 2].value);
                                        NpcStruct.npc[mapnum, i2].Defense = Convert.ToInt32(EnemyStruct.enemyparams[e, 6].value); ;
                                        NpcStruct.npc[mapnum, i2].Agility = Convert.ToInt32(EnemyStruct.enemyparams[e, 7].value); ;
                                        NpcStruct.npc[mapnum, i2].MagicDefense = Convert.ToInt32(EnemyStruct.enemyparams[e, 5].value);
                                        NpcStruct.npc[mapnum, i2].MagicAttack = Convert.ToInt32(EnemyStruct.enemyparams[e, 4].value);
                                        NpcStruct.npc[mapnum, i2].Luck = Convert.ToInt32(EnemyStruct.enemyparams[e, 3].value);
                                        NpcStruct.npc[mapnum, i2].Sprite = notedata[0];
                                        NpcStruct.npc[mapnum, i2].s = Convert.ToInt32(notedata[1]);
                                        NpcStruct.npc[mapnum, i2].Type = Convert.ToInt32(notedata[6]);
                                        NpcStruct.npc[mapnum, i2].Range = 1;

                                        if (notedata.Length - 1 > 11)
                                        {
                                            NpcStruct.npc[mapnum, i2].KnockAttack = Convert.ToBoolean(notedata[12]);
                                            NpcStruct.npc[mapnum, i2].KnockRange = Convert.ToInt32(notedata[13]);
                                            NpcStruct.nspell[mapnum, i2, 1].spellnum = Convert.ToInt32(notedata[14]);
                                            NpcStruct.nspell[mapnum, i2, 2].spellnum = Convert.ToInt32(notedata[15]);
                                            NpcStruct.nspell[mapnum, i2, 3].spellnum = Convert.ToInt32(notedata[16]);
                                            NpcStruct.nspell[mapnum, i2, 4].spellnum = Convert.ToInt32(notedata[17]);
                                        }

                                        NpcStruct.npc[mapnum, i2].Animation = Convert.ToInt32(notedata[8]);
                                        NpcStruct.npc[mapnum, i2].SpeedMove = Convert.ToInt32(notedata[5]);
                                        NpcStruct.tempnpc[mapnum, i2].movespeed = NpcStruct.npc[mapnum, i2].SpeedMove / 64;
                                        NpcStruct.npc[mapnum, i2].Respawn = Convert.ToInt32(notedata[7]) * 10;
                                        NpcStruct.npc[mapnum, i2].Exp = EnemyStruct.enemy[e].exp;
                                        NpcStruct.npc[mapnum, i2].Gold = EnemyStruct.enemy[e].gold;
                                        NpcStruct.npcdrop[mapnum, i2, 0].ItemNum = EnemyStruct.enemydrops[e, 0].data_id;
                                        NpcStruct.npcdrop[mapnum, i2, 0].ItemType = EnemyStruct.enemydrops[e, 0].kind;
                                        NpcStruct.npcdrop[mapnum, i2, 0].Chance = Convert.ToInt32(EnemyStruct.enemydrops[e, 0].denominator);
                                        NpcStruct.npcdrop[mapnum, i2, 0].Value = 1;
                                        NpcStruct.npcdrop[mapnum, i2, 1].ItemNum = EnemyStruct.enemydrops[e, 1].data_id;
                                        NpcStruct.npcdrop[mapnum, i2, 1].ItemType = EnemyStruct.enemydrops[e, 1].kind;
                                        NpcStruct.npcdrop[mapnum, i2, 1].Chance = Convert.ToInt32(EnemyStruct.enemydrops[e, 1].denominator);
                                        NpcStruct.npcdrop[mapnum, i2, 1].Value = 1;
                                        NpcStruct.npcdrop[mapnum, i2, 2].ItemNum = EnemyStruct.enemydrops[e, 2].data_id;
                                        NpcStruct.npcdrop[mapnum, i2, 2].ItemType = EnemyStruct.enemydrops[e, 2].kind;
                                        NpcStruct.npcdrop[mapnum, i2, 2].Chance = Convert.ToInt32(EnemyStruct.enemydrops[e, 2].denominator);
                                        NpcStruct.npcdrop[mapnum, i2, 2].Value = 1;
                                        NpcStruct.tempnpc[mapnum, i2].prev_move = new NpcStruct.Point[7];
                                        break;
                                    }
                                }
                            }



                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "22")
                            {
                                int tppoint = MapStruct.getOpenTpPoint();
                                MapStruct.tppoint[tppoint].map = mapnum;
                                MapStruct.tppoint[tppoint].cost = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data4);

                                int tp_count = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);

                                MapStruct.tppoint[tppoint].tp_map = new int[tp_count];
                                MapStruct.tppoint[tppoint].tp_x = new byte[tp_count];
                                MapStruct.tppoint[tppoint].tp_y = new byte[tp_count];

                                string[] tp_data = MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(':');

                                MapStruct.tppoint[tppoint].count = tp_data.Length;

                                for (int i = 0; i < tp_data.Length; i++)
                                {
                                    MapStruct.tppoint[tppoint].tp_map[i] = Convert.ToInt32(tp_data[i].Split(',')[0]);
                                    MapStruct.tppoint[tppoint].tp_x[i] = Convert.ToByte(tp_data[i].Split(',')[1]);
                                    MapStruct.tppoint[tppoint].tp_y[i] = Convert.ToByte(tp_data[i].Split(',')[2]);

                                }
                            }

                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "23")
                            {
                                int savepoint = MapStruct.getOpensavePoint();
                                MapStruct.savepoint[savepoint].map = mapnum;

                                string[] save_data = MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(',');

                                MapStruct.savepoint[savepoint].save_map = Convert.ToInt32(save_data[0]);
                                MapStruct.savepoint[savepoint].save_x = Convert.ToByte(save_data[1]);
                                MapStruct.savepoint[savepoint].save_y = Convert.ToByte(save_data[2]);
                            }



                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "14")
                            {
                                int bankpoint = MapStruct.getOpenBankPoint();
                                MapStruct.bankpoint[bankpoint].map = mapnum;
                            }

                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "15")
                            {
                                int craftpoint = MapStruct.getOpenCraftPoint();
                                MapStruct.craftpoint[craftpoint].map = mapnum;
                                MapStruct.craftpoint[craftpoint].type = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);
                            }

                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "17")
                            {
                                int workpoint = MapStruct.getOpenWorkPoint();
                                MapStruct.workpoint[workpoint].map = mapnum;
                                MapStruct.workpoint[workpoint].x = x;
                                MapStruct.workpoint[workpoint].y = y;
                                MapStruct.workpoint[workpoint].req_tool = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data2.Split(',')[0]);
                                MapStruct.workpoint[workpoint].type = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data2.Split(',')[1]);
                                MapStruct.workpoint[workpoint].vitality = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data3.Split(',')[0]);
                                MapStruct.workpoint[workpoint].exp = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data3.Split(',')[1]);
                                MapStruct.workpoint[workpoint].level_req = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data3.Split(',')[2]);
                                MapStruct.workpoint[workpoint].reward = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data4.Split(',')[0]);
                                MapStruct.workpoint[workpoint].respawn_timer = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data4.Split(',')[1]);
                                MapStruct.workpoint[workpoint].active_sprite = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data4.Split(',')[2]);
                                MapStruct.workpoint[workpoint].inactive_sprite = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data4.Split(',')[3]);
                                MapStruct.tempworkpoint[workpoint].vitality = MapStruct.workpoint[workpoint].vitality;
                                MapStruct.tempworkpoint[workpoint].respawn = 0;
                            }

                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "18")
                            {
                                int chestpoint = MapStruct.getOpenChestPoint();
                                MapStruct.chestpoint[chestpoint].map = mapnum;
                                MapStruct.chestpoint[chestpoint].x = x;
                                MapStruct.chestpoint[chestpoint].y = y;
                                MapStruct.chestpoint[chestpoint].active_sprite = MapStruct.tile[mapnum, x, y].Data2.Split(',')[0];
                                MapStruct.chestpoint[chestpoint].active_sprite_s = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data2.Split(',')[1]);
                                MapStruct.chestpoint[chestpoint].inactive_sprite = MapStruct.tile[mapnum, x, y].Data2.Split(',')[2];
                                MapStruct.chestpoint[chestpoint].inactive_sprite_s = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data2.Split(',')[3]);
                                if (MapStruct.tile[mapnum, x, y].Data3.Contains('?'))
                                {
                                    //MapStruct.chestpoint[chestpoint].is_random = true;
                                    //MapStruct.chestpoint[chestpoint].reward = new string[0];
                                    //MapStruct.chestpoint[chestpoint].reward[0] = MapStruct.tile[mapnum, x, y].Data3.Split('?')[0];
                                }
                                else
                                {
                                    MapStruct.chestpoint[chestpoint].is_random = false;
                                    MapStruct.chestpoint[chestpoint].reward_count = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data3.Split(',')[0]);
                                    MapStruct.chestpoint[chestpoint].reward = new string[MapStruct.chestpoint[chestpoint].reward_count + 1];

                                    int reader = 1;
                                    for (int i = 1; i <= MapStruct.chestpoint[chestpoint].reward_count; i++)
                                    {
                                        MapStruct.chestpoint[chestpoint].reward[i] += MapStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MapStruct.chestpoint[chestpoint].reward[i] += MapStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MapStruct.chestpoint[chestpoint].reward[i] += MapStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                        MapStruct.chestpoint[chestpoint].reward[i] += MapStruct.tile[mapnum, x, y].Data3.Split(',')[reader] + ","; reader += 1;
                                    }
                                }
                                MapStruct.chestpoint[chestpoint].key = Convert.ToInt32(MapStruct.tile[mapnum, x, y].Data4);
                            }

                            //QUESTS
                            if (MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 == "10")
                            {
                                string[] questdata = MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data3.Split(':');
                                int questgiver = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data4);

                                //Informações de onde fica nosso "quest giver"
                                MapStruct.questgiver[questgiver].map = mapnum;
                                MapStruct.questgiver[questgiver].x = x;
                                MapStruct.questgiver[questgiver].y = y;
                                MapStruct.questgiver[questgiver].quest_count = Convert.ToInt32(MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2);

                                int reader = 0;

                                for (int q = 1; q <= MapStruct.questgiver[questgiver].quest_count; q++)
                                {

                                    MapStruct.quest[questgiver, q].type = questdata[reader];

                                    int typekill = Convert.ToInt32(questdata[reader].Split('|')[0]);
                                    int typeaction = Convert.ToInt32(questdata[reader].Split('|')[1]);
                                    int typeitem = Convert.ToInt32(questdata[reader].Split('|')[2]);

                                    reader += 1;

                                    int killvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    int actionvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    int itemvalue = Convert.ToInt32(questdata[reader]); reader += 1;


                                    if (typekill > 0)
                                    {
                                        MapStruct.quest[questgiver, q].killvalue = killvalue;
                                        for (int k = 1; k <= Convert.ToInt32(MapStruct.quest[questgiver, q].killvalue); k++)
                                        {
                                            MapStruct.questkills[questgiver, q, k].monstername = questdata[reader]; reader += 1;
                                            MapStruct.questkills[questgiver, q, k].value = Convert.ToInt32(questdata[reader]); reader += 1;
                                        }
                                    }
                                    if (typeaction > 0)
                                    {
                                        MapStruct.quest[questgiver, q].actionvalue = actionvalue;

                                        for (int a = 1; a <= Convert.ToInt32(MapStruct.quest[questgiver, q].actionvalue); a++)
                                        {
                                            MapStruct.questactions[questgiver, q, a].type = Convert.ToInt32(questdata[reader]); reader += 1;
                                            MapStruct.questactions[questgiver, q, a].data = questdata[reader]; reader += 1;
                                        }
                                    }
                                    if (typeitem > 0)
                                    {
                                        MapStruct.quest[questgiver, q].itemvalue = itemvalue;
                                        for (int i = 1; i <= Convert.ToInt32(MapStruct.quest[questgiver, q].itemvalue); i++)
                                        {
                                            MapStruct.questitems[questgiver, q, i].item = questdata[reader]; reader += 1;
                                        }
                                    }

                                    MapStruct.quest[questgiver, q].rewardvalue = Convert.ToInt32(questdata[reader]); reader += 1;
                                    for (int i = 1; i <= Convert.ToInt32(MapStruct.quest[questgiver, q].rewardvalue); i++)
                                    {
                                        MapStruct.questrewards[questgiver, q, i].item = questdata[reader]; reader += 1;
                                    }

                                    MapStruct.quest[questgiver, q].exp = Convert.ToInt32(questdata[reader]); reader += 1;
                                    MapStruct.quest[questgiver, q].gold = Convert.ToInt32(questdata[reader]); reader += 1;

                                }
                            }
                        }

                    }

                //Fecha o leitor
                br.Close();

                if (String.IsNullOrEmpty(MapStruct.map[Convert.ToInt32(mapnum)].max_width)) { Maps.clear(mapnum); Maps.save(mapnum); }

                //Responde que o mapa foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }
        }
        //*********************************************************************************************
        // clearMap / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clear(int mapnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum) != null)
            {
                return;
            }

            //CÓDIGO
            //Limpamos o tamanho do mapa
            MapStruct.map[Convert.ToInt32(mapnum)].name = "";
            MapStruct.map[Convert.ToInt32(mapnum)].max_width = "19";
            MapStruct.map[Convert.ToInt32(mapnum)].max_height = "14";
            MapStruct.map[Convert.ToInt32(mapnum)].guildnum = 0;
            MapStruct.map[Convert.ToInt32(mapnum)].guildgold = 0;
            MapStruct.map[Convert.ToInt32(mapnum)].guildmember = "";

            //Limpamos os tiles

            for (int x = 0; x <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_width); x++)
                for (int y = 0; y <= Convert.ToInt32(MapStruct.map[Convert.ToInt32(mapnum)].max_height); y++)
                {
                    {
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].Event_Id = 0;
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data1 = "0";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data2 = "0";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data3 = "0";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].Data4 = "0";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].DownBlock = "true";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].LeftBlock = "true";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].RightBlock = "true";
                        MapStruct.tile[Convert.ToInt32(mapnum), x, y].UpBlock = "true";
                    }

                }
        }
        //*********************************************************************************************
        // loadMaps / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadAll()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxMaps; i++)
            {
                if (load(i))
                {
                    // okay
                }
                else
                {
                    clear(i);
                    save(i);
                }
                MapStruct.tempmap[i].NpcCount = MapStruct.getMapNpcCount(i);
            }

        }
    }
}
