using System;
using System.Linq;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados aos npcs.
    // NpcStruct.cs
    //*********************************************************************************************
    class NpcStruct
    {
        //*********************************************************************************************
        // ESTRUTURA DOS NPCS
        // Estruturas gerais contendo o drop, magias, e informações temporárias
        //*********************************************************************************************
        public static TempNpc[,] tempnpc = new TempNpc[Globals.MaxMaps, Globals.MaxMapNpcs];
        public static NTempSpell[,,] ntempspell = new NTempSpell[Globals.MaxMaps, Globals.MaxMapNpcs, Globals.MaxNTempSpells];
        public static NSpell[, ,] nspell = new NSpell[Globals.MaxMaps, Globals.MaxMapNpcs, Globals.Max_Npc_Spells];
        public static Npc[,] npc = new Npc[Globals.MaxMaps, Globals.MaxMapNpcs];
        public static NpcDrop[,,] npcdrop = new NpcDrop[Globals.MaxMaps, Globals.MaxMapNpcs, 5];

        public struct Npc
        {
            public int Map;
            public string Name;
            public string Sprite;
            public int s;
            public string Talk;
            public int X;
            public int Y;
            public int Attack;
            public int Defense;
            public int Agility;
            public int MagicDefense;
            public int MagicAttack;
            public int Luck;
            public int Range;
            public int Vitality;
            public int Spirit;
            public int Type;
            public int Animation;
            public int Respawn;
            public int SpeedMove;
            public int Exp;
            public int Gold;
            public bool KnockAttack;
            public int KnockRange;
        }

        public struct NpcDrop
        {
            public int ItemNum;
            public int ItemType;
            public int Chance;
            public int Value;
        }

        public struct TempNpc
        {
            public int Target;
            public int curTargetX;
            public int curTargetY;
            public long curTimer;
            public int X;
            public int Y;
            public byte Dir;
            public byte Moving;
            public long AttackTimer;
            public int curTarget;
            public int Current;
            public int Vitality;
            public int Spirit;
            public long RespawnTimer;
            public long MoveTimer;
            public bool Dead;
            public long ParalyzeTimer;
            public long Blindtimer;
            public long RegenTimer;
            public bool Stunned;
            public long StunTimer;
            public bool Sleeping;
            public long SleepTimer;
            public bool Slowed;
            public long SlowTimer;
            public double movespeed;
            public int preparingskill;
            public int preparingskillslot;
            public long skilltimer;
            public bool Blind;
            public long BlindTimer;
            public int guildnum;
            public Point[] prev_move;
            public bool isFrozen;
            public byte ReduceDamage;
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        public struct NTempSpell
        {
            public bool active;
            public int attacker;
            public int spellnum;
            public long timer;
            public int repeats;
            public int anim;
            public int area_range;
            public bool is_line;
        }

        public struct NSpell
        {
            public int spellnum;
            public long cooldown; //??? askapsoksa
        }

        //*********************************************************************************************
        // getOpenNTempSpell / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenNTempSpell(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxNTempSpells; i++)
            {
                if (ntempspell[map, id, i].active == false)
                {
                    return i;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // getNpcDropCount / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Quantidade de itens a dropar de determinado monstro
        //*********************************************************************************************
        public static int getNpcDropCount(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 0; i < Globals.MaxNpcDrops; i++)
            {
                if (npcdrop[map, id, i].ItemNum > 0)
                {
                    count += 1;
                }
            }
            return count;
        }
        //*********************************************************************************************
        // getNpcCritical / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getNpcCritical(int Map, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s));
            }

            //CÓDIGO
            double WaterCritical = Convert.ToDouble(npc[Map, s].Luck) * 1.0;
            int critical = Convert.ToInt32(WaterCritical);

            return critical;
        }
        //*********************************************************************************************
        // getNpcParry / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getNpcParry(int Map, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s));
            }

            //CÓDIGO
            double WindParry = Convert.ToDouble(npc[Map, s].Agility) * 1.0;
            int parry = Convert.ToInt32(WindParry);

            return parry;
        }
        //*********************************************************************************************
        // RegenNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void RegenNpc(int Map, int s, bool SuperRegen = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s, SuperRegen) != null)
            {
                return;
            }

            //CÓDIGO
            if ((tempnpc[Map, s].Vitality <= 0) || (tempnpc[Map, s].Dead)) { return; }
            if ((tempnpc[Map, s].Vitality == npc[Map, s].Vitality) && (tempnpc[Map, s].Spirit == npc[Map, s].Spirit)) { return; }

            if (SuperRegen)
            {
                if (tempnpc[Map, s].Vitality < npc[Map, s].Vitality)
                {
                    tempnpc[Map, s].Vitality = npc[Map, s].Vitality;
                    return;
                }

                if (tempnpc[Map, s].Spirit < npc[Map, s].Spirit)
                {
                    tempnpc[Map, s].Spirit = npc[Map, s].Spirit;
                    return;
                }
                return;
            }

            if (tempnpc[Map, s].Vitality < npc[Map, s].Vitality)
            {
                tempnpc[Map, s].Vitality += (npc[Map, s].Vitality / 150);
                if (tempnpc[Map, s].Vitality > npc[Map, s].Vitality)
                {
                    tempnpc[Map, s].Vitality = npc[Map, s].Vitality;
                }
                SendData.sendNpcVitality(Map, s, tempnpc[Map, s].Vitality);
            }

            if (tempnpc[Map, s].Spirit < npc[Map, s].Spirit)
            {
                tempnpc[Map, s].Spirit += (npc[Map, s].Spirit / 10);
                if (tempnpc[Map, s].Spirit > npc[Map, s].Spirit)
                {
                    tempnpc[Map, s].Spirit = npc[Map, s].Spirit;
                }
            }

            tempnpc[Map, s].RegenTimer = Loops.TickCount.ElapsedMilliseconds + 1000;
        }
        //*********************************************************************************************
        // clearTempNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Limpa informações temporárias de um determinado NPC.
        //*********************************************************************************************
        public static void clearTempNpc(int mapnum, int i2)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, mapnum, i2) != null)
            {
                return;
            }

            //CÓDIGO
            npc[mapnum, i2].Name = "";
            npc[mapnum, i2].Map = 0;
            npc[mapnum, i2].X = 0;
            npc[mapnum, i2].Y = 0;
            npc[mapnum, i2].Vitality = 0;
            npc[mapnum, i2].Spirit = 0;
            tempnpc[mapnum, i2].Target = 0;
            tempnpc[mapnum, i2].X = 0;
            tempnpc[mapnum, i2].Y = 0;
            tempnpc[mapnum, i2].curTargetX = 0;
            tempnpc[mapnum, i2].curTargetY = 0;
            tempnpc[mapnum, i2].Vitality = 0;
            npc[mapnum, i2].Attack = 0;
            npc[mapnum, i2].Defense = 0;
            npc[mapnum, i2].Agility = 0;
            npc[mapnum, i2].MagicDefense = 0;
            npc[mapnum, i2].MagicAttack = 0;
            npc[mapnum, i2].Luck = 0;
            npc[mapnum, i2].Sprite = "";
            npc[mapnum, i2].s = 0;
            npc[mapnum, i2].Type = 0;
            npc[mapnum, i2].Range = 0;

            //if (notedata.Length - 1 > 11)
            //{
            //    npc[mapnum, i2].KnockAttack = Convert.ToBoolean(notedata[12]);
            //    npc[mapnum, i2].KnockRange = Convert.ToInt32(notedata[13]);
            //    nspell[mapnum, i2, 1].spellnum = Convert.ToInt32(notedata[14]);
            //    nspell[mapnum, i2, 2].spellnum = Convert.ToInt32(notedata[15]);
            //    nspell[mapnum, i2, 3].spellnum = Convert.ToInt32(notedata[16]);
            //    nspell[mapnum, i2, 4].spellnum = Convert.ToInt32(notedata[17]);
            //}

            npc[mapnum, i2].Animation = 0;
            npc[mapnum, i2].SpeedMove = 0;
            tempnpc[mapnum, i2].movespeed = 0;
            npc[mapnum, i2].Respawn = 0;
            npc[mapnum, i2].Exp = 0;
            npc[mapnum, i2].Gold = 0;
            tempnpc[mapnum, i2].guildnum = 0;
        }
        //*********************************************************************************************
        // ExecuteTempSpell / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void ExecuteTempSpell(int Map, int s, int NStempSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s, NStempSpell) != null)
            {
                return;
            }

            //CÓDIGO
            int Attacker = ntempspell[Map, s, NStempSpell].attacker;

            if ((UserConnection.getS(Attacker) < 0) || (UserConnection.getS(Attacker) >= WinsockAsync.Clients.Count()))
            {
                ntempspell[Map, s, NStempSpell].attacker = 0;
                ntempspell[Map, s, NStempSpell].timer = 0;
                ntempspell[Map, s, NStempSpell].spellnum = 0;
                ntempspell[Map, s, NStempSpell].repeats = 0;
                ntempspell[Map, s, NStempSpell].active = false;
                return;
            }

            //Verificar se o jogador não se desconectou no processo
            if (!WinsockAsync.Clients[(UserConnection.getS(Attacker))].IsConnected)
            {
                ntempspell[Map, s, NStempSpell].attacker = 0;
                ntempspell[Map, s, NStempSpell].timer = 0;
                ntempspell[Map, s, NStempSpell].spellnum = 0;
                ntempspell[Map, s, NStempSpell].repeats = 0;
                ntempspell[Map, s, NStempSpell].active = false;
                return;
            }

            if (tempnpc[Map, s].Vitality <= 0)
            {
                ntempspell[Map, s, NStempSpell].attacker = 0;
                ntempspell[Map, s, NStempSpell].timer = 0;
                ntempspell[Map, s, NStempSpell].spellnum = 0;
                ntempspell[Map, s, NStempSpell].repeats = 0;
                ntempspell[Map, s, NStempSpell].active = false;
                return;
            }

            SendData.sendAnimation(Map, 2, s, ntempspell[Map, s, NStempSpell].anim);
            
            if (ntempspell[Map, s, NStempSpell].area_range <= 0)
            {
                CombatRelations.playerAttackNpc(Attacker, s, ntempspell[Map, s, NStempSpell].spellnum, Map);
            }
            else
            {
                CombatRelations.playerAttackNpc(Attacker, s, ntempspell[Map, s, NStempSpell].spellnum, Map);
                for (int i = 0; i <= Globals.Player_Highs; i++)
                {
                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP) && (i != s))
                    {
                        for (int r = 1; r <= ntempspell[Map, s, NStempSpell].area_range; r++)
                        {
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }


                            //Is line?
                            if (ntempspell[Map, s, NStempSpell].is_line)
                            {
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == tempnpc[Map, s].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == tempnpc[Map, s].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == tempnpc[Map, s].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == tempnpc[Map, s].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == tempnpc[Map, s].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                                }
                            }
                        }
                    }
                }
                
                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                {
                    for (int r = 1; r <= ntempspell[Map, s, NStempSpell].area_range; r++)
                    {
                        if ((tempnpc[Map, i].X - r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y == tempnpc[Map, s].Y))
                        {
                            CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                        }
                        if ((tempnpc[Map, i].X + r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y == tempnpc[Map, s].Y))
                        {
                            CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                        }
                        if ((tempnpc[Map, i].X == tempnpc[Map, s].X) && (tempnpc[Map, i].Y - r == tempnpc[Map, s].Y))
                        {
                            CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                        }
                        if ((tempnpc[Map, i].X == tempnpc[Map, s].X) && (tempnpc[Map, i].Y + r == tempnpc[Map, s].Y))
                        {
                            CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                        }


                        //Is line?
                        if (ntempspell[Map, s, NStempSpell].is_line)
                        {
                            if ((tempnpc[Map, i].X - r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y + r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((tempnpc[Map, i].X + r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y - r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((tempnpc[Map, i].X + r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y + r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                            if ((tempnpc[Map, i].X - r == tempnpc[Map, s].X) && (tempnpc[Map, i].Y - r == tempnpc[Map, s].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, ntempspell[Map, s, NStempSpell].spellnum, Map);
                            }
                        }
                    }
                }
            }

            ntempspell[Map, s, NStempSpell].repeats -= 1;

            if (ntempspell[Map, s, NStempSpell].repeats <= 0)
            {
                if (SkillStruct.skill[ntempspell[Map, s, NStempSpell].spellnum].slow)
                {
                    tempnpc[Map, s].movespeed = npc[Map, s].SpeedMove / 64;
                    SendData.sendMoveSpeed(2, s, Map);
                }
                ntempspell[Map, s, NStempSpell].attacker = 0;
                ntempspell[Map, s, NStempSpell].timer = 0;
                ntempspell[Map, s, NStempSpell].spellnum = 0;
                ntempspell[Map, s, NStempSpell].repeats = 0;
                ntempspell[Map, s, NStempSpell].active = false;
            }

            ntempspell[Map, s, NStempSpell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[ntempspell[Map, s, NStempSpell].spellnum].interval;

        }
    }
}
