using System;
using System.Linq;
using System.Reflection;

namespace __Forjerum
{
    class SkillRelations : Languages.LStruct
    {
        //*********************************************************************************************
        // ExecutePTempSpell / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void executeTempSpell(int s, int PStempSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, PStempSpell) != null)
            {
                return;
            }

            //CÓDIGO
            int Attacker = PlayerStruct.ptempspell[s, PStempSpell].attacker;
            int Map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;

            if ((UserConnection.getS(Attacker) < 0) || (UserConnection.getS(Attacker) >= WinsockAsync.Clients.Count()))
            {
                PlayerStruct.ptempspell[s, PStempSpell].attacker = 0;
                PlayerStruct.ptempspell[s, PStempSpell].timer = 0;
                PlayerStruct.ptempspell[s, PStempSpell].spellnum = 0;
                PlayerStruct.ptempspell[s, PStempSpell].repeats = 0;
                PlayerStruct.ptempspell[s, PStempSpell].active = false;
                return;
            }

            //Verificar se o jogador não se desconectou no processo
            if (!WinsockAsync.Clients[(UserConnection.getS(Attacker))].IsConnected)
            {
                PlayerStruct.ptempspell[s, PStempSpell].attacker = 0;
                PlayerStruct.ptempspell[s, PStempSpell].timer = 0;
                PlayerStruct.ptempspell[s, PStempSpell].spellnum = 0;
                PlayerStruct.ptempspell[s, PStempSpell].repeats = 0;
                PlayerStruct.ptempspell[s, PStempSpell].active = false;
                return;
            }

            if (PlayerStruct.tempplayer[s].Vitality <= 0)
            {
                PlayerStruct.ptempspell[s, PStempSpell].attacker = 0;
                PlayerStruct.ptempspell[s, PStempSpell].timer = 0;
                PlayerStruct.ptempspell[s, PStempSpell].spellnum = 0;
                PlayerStruct.ptempspell[s, PStempSpell].repeats = 0;
                PlayerStruct.ptempspell[s, PStempSpell].active = false;
                return;
            }

            SendData.sendAnimation(Map, 1, s, PlayerStruct.ptempspell[s, PStempSpell].anim);

            if (PlayerStruct.ptempspell[s, PStempSpell].area_range <= 0)
            {
                if (!PlayerStruct.ptempspell[s, PStempSpell].is_heal)
                {
                    CombatRelations.playerAttackPlayer(Attacker, s, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                }
                else
                {
                    PlayerLogic.HealPlayer(s, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                }
            }
            else
            {
                if (!PlayerStruct.ptempspell[s, PStempSpell].is_heal)
                {
                    CombatRelations.playerAttackPlayer(Attacker, s, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                    for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                    {
                        for (int r = 1; r <= PlayerStruct.ptempspell[s, PStempSpell].area_range; r++)
                        {
                            if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }


                            //Is line?
                            if (PlayerStruct.ptempspell[s, PStempSpell].is_line)
                            {
                                if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                            }
                        }
                    }

                    for (int i = 0; i <= Globals.Player_Highs; i++)
                    {
                        if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP) && (i != s))
                        {
                            for (int r = 1; r <= PlayerStruct.ptempspell[s, PStempSpell].area_range; r++)
                            {
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }


                                //Is line?
                                if (PlayerStruct.ptempspell[s, PStempSpell].is_line)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        CombatRelations.playerAttackPlayer(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    PlayerLogic.HealPlayer(s, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                    CombatRelations.playerAttackPlayer(Attacker, s, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                    for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                    {
                        for (int r = 1; r <= PlayerStruct.ptempspell[s, PStempSpell].area_range; r++)
                        {
                            if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }
                            if ((NpcStruct.tempnpc[Map, i].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                            {
                                CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                            }


                            //Is line?
                            if (PlayerStruct.ptempspell[s, PStempSpell].is_line)
                            {
                                if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                                if ((NpcStruct.tempnpc[Map, i].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (NpcStruct.tempnpc[Map, i].Y - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y))
                                {
                                    CombatRelations.playerAttackNpc(Attacker, i, PlayerStruct.ptempspell[s, PStempSpell].spellnum, Map);
                                }
                            }
                        }
                    }

                    for (int i = 0; i <= Globals.Player_Highs; i++)
                    {
                        if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP) && (i != s))
                        {
                            for (int r = 1; r <= PlayerStruct.ptempspell[s, PStempSpell].area_range; r++)
                            {
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                }
                                if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                {
                                    PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                }


                                //Is line?
                                if (PlayerStruct.ptempspell[s, PStempSpell].is_line)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                    }
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[s, PlayerStruct.player[i].SelectedChar].Y))
                                    {
                                        PlayerLogic.HealPlayer(i, PlayerLogic.HealSpellDamage(Attacker, PlayerStruct.ptempspell[s, PStempSpell].spellnum));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            PlayerStruct.ptempspell[s, PStempSpell].repeats -= 1;

            if (PlayerStruct.ptempspell[s, PStempSpell].repeats <= 0)
            {
                if ((SkillStruct.skill[PlayerStruct.ptempspell[s, PStempSpell].spellnum].slow) || (PlayerStruct.ptempspell[s, PStempSpell].fast_buff))
                {
                    PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                    SendData.sendMoveSpeed(1, s);
                }
                PlayerStruct.ptempspell[s, PStempSpell].attacker = 0;
                PlayerStruct.ptempspell[s, PStempSpell].timer = 0;
                PlayerStruct.ptempspell[s, PStempSpell].spellnum = 0;
                PlayerStruct.ptempspell[s, PStempSpell].repeats = 0;
                PlayerStruct.ptempspell[s, PStempSpell].active = false;
            }

            PlayerStruct.ptempspell[s, PStempSpell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.ptempspell[s, PStempSpell].spellnum].interval;

        }
        //*********************************************************************************************
        // GiveSpell
        // Entrega determinada magia para determinado jogador
        //*********************************************************************************************
        public static bool giveSpell(int s, int spellnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, spellnum) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, spellnum));
            }

            //CÓDIGO
            if (PlayerRelations.getSkillOpenSlot(s) > 0)
            {
                int openslot = PlayerRelations.getSkillOpenSlot(s);
                PlayerStruct.skill[s, openslot].level = 0;
                PlayerStruct.skill[s, openslot].cooldown = 0;
                PlayerStruct.skill[s, openslot].num = spellnum;
                return true;
            }
            else
            {
                SendData.sendMsgToPlayer(s, lang.you_cant_learn_more_skills, Globals.ColorRed, Globals.Msg_Type_Server);
                return false;
            }
        }
        //*********************************************************************************************
        // getPlayerSkillSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna determinada magia baseado no slot em que está.
        //*********************************************************************************************
        public static int getPlayerSkillSlot(int s, int SkillNum, bool by_skill_slot = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, SkillNum, by_skill_slot) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, SkillNum, by_skill_slot));
            }

            //CÓDIGO
            if (!by_skill_slot)
            {
                for (int i = 1; i < Globals.MaxHotkeys; i++)
                {
                    if (PlayerStruct.skill[s, PlayerStruct.hotkey[s, i].num].num == SkillNum)
                    {
                        return i;
                    }
                }
            }

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                if (PlayerStruct.skill[s, i].num == SkillNum)
                {
                    return i;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // getOpenPassiveEffect / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenPassiveEffect(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxPassiveEffects; i++)
            {
                if (!PlayerStruct.ppassiveffect[s, i].active)
                {
                    return i;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // getOpenSpellBuff / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenSpellBuff(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxSpellBuffs; i++)
            {
                if (!PlayerStruct.pspellbuff[s, i].active)
                {
                    return i;
                }
            }

            return 0;
        }

        //*********************************************************************************************
        // IsActiveSpellBuff / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int isActiveSpellBuff(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxSpellBuffs; i++)
            {
                if (!PlayerStruct.pspellbuff[s, i].active)
                {
                    return i;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // ExecutePassiveEffect / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void executePassiveEffect(int s, int passive)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, passive) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.ppassiveffect[s, passive].targettype == 1)
            {
                //Jogador
                if (PlayerStruct.ppassiveffect[s, passive].type == 1) //DANO
                {
                    SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.ppassiveffect[s, passive].targettype, PlayerStruct.ppassiveffect[s, passive].target, SkillStruct.skill[PlayerStruct.ppassiveffect[s, passive].spellnum].animation_id);
                    CombatRelations.playerAttackPlayer(s, PlayerStruct.ppassiveffect[s, passive].target, PlayerStruct.ppassiveffect[s, passive].spellnum, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, true);
                    PlayerStruct.ppassiveffect[s, passive].active = false;
                    PlayerStruct.ppassiveffect[s, passive].type = 0;
                    PlayerStruct.ppassiveffect[s, passive].timer = 0;
                    PlayerStruct.ppassiveffect[s, passive].target = 0;
                    PlayerStruct.ppassiveffect[s, passive].targettype = 0;
                    PlayerStruct.ppassiveffect[s, passive].spellnum = 0;
                    return;
                }
            }
            if (PlayerStruct.ppassiveffect[s, passive].targettype == 2)
            {
                //NPC
                if (PlayerStruct.ppassiveffect[s, passive].type == 1) //DANO
                {
                    SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.ppassiveffect[s, passive].targettype, PlayerStruct.ppassiveffect[s, passive].target, SkillStruct.skill[PlayerStruct.ppassiveffect[s, passive].spellnum].animation_id);
                    CombatRelations.playerAttackNpc(s, PlayerStruct.ppassiveffect[s, passive].target, PlayerStruct.ppassiveffect[s, passive].spellnum, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, true);
                    PlayerStruct.ppassiveffect[s, passive].active = false;
                    PlayerStruct.ppassiveffect[s, passive].type = 0;
                    PlayerStruct.ppassiveffect[s, passive].timer = 0;
                    PlayerStruct.ppassiveffect[s, passive].target = 0;
                    PlayerStruct.ppassiveffect[s, passive].targettype = 0;
                    PlayerStruct.ppassiveffect[s, passive].spellnum = 0;
                    return;
                }
            }
            PlayerStruct.ppassiveffect[s, passive].active = false;
            PlayerStruct.ppassiveffect[s, passive].type = 0;
            PlayerStruct.ppassiveffect[s, passive].timer = 0;
            PlayerStruct.ppassiveffect[s, passive].target = 0;
            PlayerStruct.ppassiveffect[s, passive].targettype = 0;
            PlayerStruct.ppassiveffect[s, passive].spellnum = 0;
        }
        //*********************************************************************************************
        // SkillPassive / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void skillPassive(int s, int targettype, int target)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, targettype, target) != null)
            {
                return;
            }

            //CÓDIGO
            int open_passive = getOpenPassiveEffect(s);

            if (open_passive == 0) { return; }

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                if (SkillStruct.skill[PlayerStruct.skill[s, i].num].type == 10)
                {
                    int levelmultiplier = (SkillStruct.skill[PlayerStruct.skill[s, i].num].passive_multiplier) * PlayerStruct.skill[s, i].level;
                    int chance = SkillStruct.skill[PlayerStruct.skill[s, i].num].passive_chance + levelmultiplier;


                    //Motivação Aiprah
                    if (SkillStruct.skill[PlayerStruct.skill[s, i].num].passive_type == 1)
                    {
                        int passive_test = Globals.Rand(1, 100);
                        if (passive_test <= chance)
                        {
                            if (targettype == 2)
                            {
                                PlayerStruct.ppassiveffect[s, open_passive].spellnum = PlayerStruct.skill[s, i].num;
                                PlayerStruct.ppassiveffect[s, open_passive].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.skill[s, i].num].passive_interval;
                                PlayerStruct.ppassiveffect[s, open_passive].target = target;
                                PlayerStruct.ppassiveffect[s, open_passive].targettype = targettype;
                                PlayerStruct.ppassiveffect[s, open_passive].type = 1;
                                PlayerStruct.ppassiveffect[s, open_passive].active = true;
                            }
                            if (targettype == 1)
                            {
                                PlayerStruct.ppassiveffect[s, open_passive].spellnum = PlayerStruct.skill[s, i].num;
                                PlayerStruct.ppassiveffect[s, open_passive].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.skill[s, i].num].passive_interval;
                                PlayerStruct.ppassiveffect[s, open_passive].target = target;
                                PlayerStruct.ppassiveffect[s, open_passive].targettype = targettype;
                                PlayerStruct.ppassiveffect[s, open_passive].type = 1;
                                PlayerStruct.ppassiveffect[s, open_passive].active = true;
                            }
                        }
                    }

                }
            }
        }
    }
}
