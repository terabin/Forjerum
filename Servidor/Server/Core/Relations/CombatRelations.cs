using System;
using System.Reflection;

namespace __Forjerum
{
    class CombatRelations : PlayerStruct
    {
        //*********************************************************************************************
        // playerAttackNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Determinado jogador efetua um ataque em determinado NPC
        //*********************************************************************************************
        public static void playerAttackNpc(int Attacker, int Victim, int isSpell = 0, int Map = 0, bool isPassive = false, int skill_level = 0, bool is_pet = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim, isSpell, Map, isPassive, skill_level, is_pet) != null)
            {
                return;
            }

            //CÓDIGO
            if (Map == 0) { Map = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].Map); }
            int Dir = character[Attacker, player[Attacker].SelectedChar].Dir;
            int NpcX = NpcStruct.tempnpc[Map, Victim].X;
            int NpcY = NpcStruct.tempnpc[Map, Victim].Y;
            int PlayerX = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].X);
            int PlayerY = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].Y);
            int Damage = 0;
            int chance = 0;
            bool is_critical = false;

            if ((!isPassive) && (isSpell == 0)) { SkillRelations.skillPassive(Attacker, Globals.Target_Npc, Victim); }
            if ((NpcStruct.tempnpc[Map, Victim].Vitality <= 0) || (NpcStruct.tempnpc[Map, Victim].Dead)) { return; }

            //Cálculo do dano

            //Magias
            if (isSpell > 0)
            {
                int skill_slot = 0;

                if (!isPassive) { skill_slot = SkillRelations.getPlayerSkillSlot(Attacker, isSpell); }
                else { skill_slot = SkillRelations.getPlayerSkillSlot(Attacker, isSpell, true); }

                if (skill_slot == 0) { return; }

                int extra_spellbuff = 0;

                for (int i = 1; i < Globals.MaxSpellBuffs; i++)
                {
                    if (pspellbuff[Attacker, i].active)
                    {
                        if (pspellbuff[Attacker, i].timer > Loops.TickCount.ElapsedMilliseconds) { extra_spellbuff += pspellbuff[Attacker, i].value; }
                        else
                        {
                            pspellbuff[Attacker, i].value = 0;
                            pspellbuff[Attacker, i].type = 0;
                            pspellbuff[Attacker, i].timer = 0;
                            pspellbuff[Attacker, i].active = false;
                        }
                    }
                }

                //Multiplicador de dano
                double multiplier = Convert.ToDouble(SkillStruct.skill[isSpell].scope) / 7.2;

                //Elemento mágico multiplicado
                double min_damage = PlayerRelations.getPlayerMinMagic(Attacker);
                double max_damage = PlayerRelations.getPlayerMaxMagic(Attacker);

                if (hotkey[Attacker, skill_slot].num > Globals.MaxPlayer_Skills)
                {
                    hotkey[Attacker, skill_slot].num = 0;
                    return;
                }

                //Multiplicador de nível
                double levelmultiplier = (1.0 + multiplier) * skill[Attacker, hotkey[Attacker, skill_slot].num].level; //Except

                //Verificando se a skill teve algum problema e corrigindo
                if (levelmultiplier < 1.0) { levelmultiplier = 1.0; }

                //Dano total que pode ser causado
                double totaldamage = max_damage + (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) * levelmultiplier);
                double totalmindamage = min_damage + (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) * levelmultiplier);

                //Passamos para int para tornar o dano exato
                int MinDamage = Convert.ToInt32(totalmindamage);
                int MaxDamage = Convert.ToInt32(totaldamage) + 1;

                if (MinDamage >= MaxDamage) { MaxDamage = MinDamage; }

                //Definição geral do dano
                Damage = Globals.Rand(MinDamage, MaxDamage);
                Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                Damage = Damage - ((Damage / 100) * NpcStruct.npc[Map, Victim].MagicDefense);

                if (tempplayer[Attacker].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(Victim, lang.damage_reduced, Globals.ColorWhite, NpcX, NpcY, 1, 0, Map);
                    tempplayer[Attacker].ReduceDamage = 0;
                }

                if (isSpell == 36)
                {
                    Damage += ((Damage / 100) * PlayerRelations.getPlayerDefense(Attacker));
                }

                if (character[Attacker, player[Attacker].SelectedChar].ClassId == 6)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Attacker, i].num == 42) && (skill[Attacker, i].level > 0))
                        {
                            //Dano crítico?
                            int critical_t = Globals.Rand(0, 100);

                            if (critical_t <= PlayerRelations.getPlayerCritical(Attacker))
                            {
                                Damage = Convert.ToInt32((Convert.ToDouble(Damage) * 1.5));
                                SendData.sendAnimation(Map, Globals.Target_Npc, Victim, 152);
                            }
                            //break;
                        }
                        if ((skill[Attacker, i].num == 41) && (skill[Attacker, i].level > 0))
                        {
                            if (isSpell == 40)
                            {
                                int open_passive = SkillRelations.getOpenPassiveEffect(Attacker);

                                if (open_passive == 0) { return; }

                                ppassiveffect[Attacker, open_passive].spellnum = skill[Attacker, i].num;
                                ppassiveffect[Attacker, open_passive].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[skill[Attacker, i].num].passive_interval;
                                ppassiveffect[Attacker, open_passive].target = Victim;
                                ppassiveffect[Attacker, open_passive].targettype = Globals.Target_Npc;
                                ppassiveffect[Attacker, open_passive].type = 1;
                                ppassiveffect[Attacker, open_passive].active = true;
                            }
                            //break;
                        }
                    }
                }

                if (Damage < 1)
                {
                    SendData.sendActionMsg(Attacker, lang.resisted, Globals.ColorPink, NpcX, NpcY, Globals.Action_Msg_Scroll, 0, Map);
                    return;
                }

                if (extra_spellbuff > 0)
                {
                    //BUFFF :DDDD
                    Damage += (Damage / 100) * extra_spellbuff;
                }

                int drain = SkillStruct.skill[isSpell].drain;

                //Drenagem de vida?
                if (drain > 0)
                {
                    double real_drain = (Convert.ToDouble(Damage) / 100) * drain;
                    PlayerLogic.HealPlayer(Attacker, Convert.ToInt32(real_drain));
                    //SendData.sendActionMsg(Attacker, Convert.ToInt32(real_drain).ToString(), Globals.ColorGreen, PlayerX, PlayerY, 1, 1);
                    //SendData.sendPlayerVitalityToMap(Map, Attacker, tempplayer[Attacker].Vitality);
                }

                NpcStruct.tempnpc[Map, Victim].Target = Attacker;
            }
            //Ataques básicos
            else
            {
                if (tempplayer[Attacker].Blind)
                {
                    SendData.sendActionMsg(Attacker, lang.attack_missed, Globals.ColorWhite, NpcX, NpcY, 1, 0, Map);
                    return;
                }

                //Desviar do golpe?
                int parry_test = Globals.Rand(0, 100);

                if (parry_test <= (NpcStruct.getNpcParry(Map, Victim) - PlayerRelations.getPlayerCritical(Attacker)))
                {
                    SendData.sendActionMsg(Attacker, lang.attack_missed, Globals.ColorWhite, NpcX, NpcY, 1, 0, Map);
                    return;
                }

                //Dano comum
                int MinDamage = PlayerRelations.getPlayerMinAttack(Attacker);
                int MaxDamage = PlayerRelations.getPlayerMaxAttack(Attacker);

                if (is_pet)
                {
                    string equipment = character[Attacker, player[Attacker].SelectedChar].Equipment;
                    string[] equipdata = equipment.Split(',');
                    string[] petdata = equipdata[4].Split(';');

                    int petnum = Convert.ToInt32(petdata[0]);
                    int petlvl = Convert.ToInt32(petdata[1]);

                    MinDamage = (Convert.ToInt32(ItemStruct.item[petnum].damage_variance)) + ((petlvl / 2) * Convert.ToInt32(ItemStruct.item[petnum].damage_formula));
                    MaxDamage = (Convert.ToInt32(ItemStruct.item[petnum].damage_variance)) + ((petlvl) * Convert.ToInt32(ItemStruct.item[petnum].damage_formula));

                    if (MinDamage >= MaxDamage)
                    {
                        Damage = MinDamage;
                        Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                        Damage = Damage - ((Damage / 100) * NpcStruct.npc[Map, Victim].Defense);
                    }
                    else
                    {
                        Damage = Globals.Rand(MinDamage, MaxDamage);
                        Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                        Damage = Damage - ((Damage / 100) * NpcStruct.npc[Map, Victim].Defense);
                    }

                    SendData.sendAnimation(Map, Globals.Target_Npc, Victim, ItemStruct.item[petnum].animation_id);
                    SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), Globals.ColorPurple, NpcX, NpcY, 1, character[Attacker, player[Attacker].SelectedChar].Dir, Map);
                    goto important;
                }

                if (MinDamage >= MaxDamage)
                {
                    Damage = MinDamage;
                    Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                    Damage = Damage - ((Damage / 100) * NpcStruct.npc[Map, Victim].Defense);
                }
                else
                {
                    Damage = Globals.Rand(MinDamage, MaxDamage);
                    Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                    Damage = Damage - ((Damage / 100) * NpcStruct.npc[Map, Victim].Defense);
                }

                if (character[Attacker, player[Attacker].SelectedChar].ClassId == 2)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Attacker, i].num == 52) && (skill[Attacker, i].level > 0))
                        {
                            Damage += ((NpcStruct.npc[Map, Victim].Vitality / 100) * (2 + skill[Attacker, i].level));
                        }
                    }
                }

                if (tempplayer[Attacker].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(Victim, lang.damage_reduced, Globals.ColorWhite, NpcX, NpcY, 1, 0, Map);
                    tempplayer[Attacker].ReduceDamage = 0;
                }

                if (Damage <= 0)
                {
                    Damage = 1;
                }

                //Dano crítico?
                int critical_test = Globals.Rand(0, 100);

                if (critical_test <= PlayerRelations.getPlayerCritical(Attacker))
                {
                    Damage = Convert.ToInt32((Convert.ToDouble(Damage) * 1.5));
                    is_critical = true;
                    NpcStruct.tempnpc[Map, Victim].Target = Attacker;
                }

                //Dano e animação
                SendData.sendAnimation(Map, 2, Victim, 7);
            }

            if (is_critical)
            {
                SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), 1, NpcX, NpcY, 1, character[Attacker, player[Attacker].SelectedChar].Dir, Map);
                int true_range = 0;
                for (int i = 1; i <= 2; i++)
                {
                    if (MovementRelations.canThrowNpc(Map, Victim, character[Attacker, player[Attacker].SelectedChar].Dir, i))
                    {
                        true_range += 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (true_range < 2)
                {
                    Damage += 2 - true_range;
                }

                if (true_range > 0)
                {
                    MovementRelations.throwNpc(Map, Victim, character[Attacker, player[Attacker].SelectedChar].Dir, true_range);
                }
            }
            else
            {
                if (isSpell > 0) { SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), Globals.ColorPink, NpcX, NpcY, Globals.Action_Msg_Scroll, character[Attacker, player[Attacker].SelectedChar].Dir, Map); }
                else { SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), 4, NpcX, NpcY, Globals.Action_Msg_Scroll, character[Attacker, player[Attacker].SelectedChar].Dir, Map); }
            }

            important:
            //Nova vida do npc
            NpcStruct.tempnpc[Map, Victim].Vitality -= Damage;

            //O NPC é um coletor?
            if (NpcStruct.tempnpc[Map, Victim].guildnum > 0)
            {
                if (!MapStruct.tempmap[Map].WarActive)
                {
                    MapStruct.tempmap[Map].WarActive = true;
                    SendData.sendMsgToGuild(NpcStruct.tempnpc[Map, Victim].guildnum, lang.the_colector_of + " " + MapStruct.map[Map].name + " " + lang.is_being_attacked, Globals.ColorYellow, Globals.Msg_Type_Server);
                }
                MapStruct.tempmap[Map].WarTimer = Loops.TickCount.ElapsedMilliseconds + 20000;
                //Avisar a guilda sobre seu ataque
            }

            //Sleep?
            if (NpcStruct.tempnpc[Map, Victim].Sleeping)
            {
                NpcStruct.tempnpc[Map, Victim].Sleeping = false;
                NpcStruct.tempnpc[Map, Victim].SleepTimer = 0;
                SendData.sendSleep(Map, 2, Victim, 0);
            }

            //Enviamos a nova vida do npc
            SendData.sendNpcVitality(Map, Victim, NpcStruct.tempnpc[Map, Victim].Vitality);

            if ((NpcStruct.npc[Map, Victim].Type == 1) && (NpcStruct.tempnpc[Map, Victim].Target == 0)) { NpcStruct.tempnpc[Map, Victim].Target = Attacker; }

            if (NpcStruct.tempnpc[Map, Victim].Vitality <= 0)
            {
                //Npc era um coletor?
                if (NpcStruct.tempnpc[Map, Victim].guildnum > 0)
                {
                    SendData.sendMsgToAll(lang.the_area + " " + MapStruct.map[Map].name + " " + lang.is_free_now, Globals.ColorYellow, Globals.Msg_Type_Server);
                    SendData.sendMsgToGuild(NpcStruct.tempnpc[Map, Victim].guildnum, lang.the_colector_of + " " + MapStruct.map[Map].name + " " + lang.has_been_defeated, Globals.ColorYellow, Globals.Msg_Type_Server);
                    SendData.sendMsgToPlayer(Attacker, lang.colector_defeated_success, Globals.ColorYellow, Globals.Msg_Type_Server);
                    PlayerRelations.givePlayerGold(Attacker, MapStruct.map[Map].guildgold);
                    MapStruct.map[Map].guildnum = 0;
                    MapStruct.map[Map].guildgold = 0;
                    NpcStruct.clearTempNpc(Map, Victim);
                    SendData.sendMapGuildToMap(Map);
                    MapStruct.tempmap[Map].NpcCount = MapStruct.getMapNpcCount(Map);
                    //Avisamos que o npc tem que sumir
                    SendData.sendNpcLeft(Map, Victim);
                    return;
                }

                //O mapa possúi um coletor?
                int guildnum = MapStruct.map[Map].guildnum;
                if (guildnum > 0)
                {
                    int total_exp = (NpcStruct.npc[Map, Victim].Exp / 100) * 10; //10%
                    if (total_exp <= 0) { total_exp = 1; }
                    int total_gold = (NpcStruct.npc[Map, Victim].Gold / 100) * 10; //10%
                    if (total_gold <= 0) { total_gold = 1; }
                    GuildStruct.guild[guildnum].exp += total_exp;
                    MapStruct.map[Map].guildgold += total_gold;
                }

                //Entrega a exp para o grupo
                PartyRelations.partyShareExp(Attacker, Victim, Map);

                //Avisamos que o npc tem que sumir
                SendData.sendNpcLeft(Map, Victim);

                //Morto
                NpcStruct.tempnpc[Map, Victim].Dead = true;

                //Drop
                for (int i = 0; i <= NpcStruct.getNpcDropCount(Map, Victim); i++)
                {
                    chance = Globals.Rand(1, NpcStruct.npcdrop[Map, Victim, i].Chance);
                    if (chance == NpcStruct.npcdrop[Map, Victim, i].Chance)
                    {
                        if (MapStruct.getNullMapItem(Map) == 0) { break; }
                        int NullMapItem = MapStruct.getNullMapItem(Map);
                        if (NpcStruct.npcdrop[Map, Victim, i].ItemType > 1) { DropRelations.dropItem(Map, NullMapItem, NpcX, NpcY, NpcStruct.npcdrop[Map, Victim, i].Value, NpcStruct.npcdrop[Map, Victim, i].ItemNum, NpcStruct.npcdrop[Map, Victim, i].ItemType, DropRelations.getRefinDrop()); }
                        else { DropRelations.dropItem(Map, NullMapItem, NpcX, NpcY, NpcStruct.npcdrop[Map, Victim, i].Value, NpcStruct.npcdrop[Map, Victim, i].ItemNum, NpcStruct.npcdrop[Map, Victim, i].ItemType, 0); }
                        SendData.sendMapItem(Map, NullMapItem);
                    }
                    else
                    {
                        //Tentar de novo
                        if (PlayerRelations.isPlayerPremmy(Attacker))
                        {
                            chance = Globals.Rand(1, NpcStruct.npcdrop[Map, Victim, i].Chance * 2);
                            if (chance == NpcStruct.npcdrop[Map, Victim, i].Chance * 2)
                            {
                                if (MapStruct.getNullMapItem(Map) == 0) { break; }
                                int NullMapItem = MapStruct.getNullMapItem(Map);
                                if (NpcStruct.npcdrop[Map, Victim, i].ItemType > 1) { DropRelations.dropItem(Map, NullMapItem, NpcX, NpcY, NpcStruct.npcdrop[Map, Victim, i].Value, NpcStruct.npcdrop[Map, Victim, i].ItemNum, NpcStruct.npcdrop[Map, Victim, i].ItemType, DropRelations.getRefinDrop()); }
                                else { DropRelations.dropItem(Map, NullMapItem, NpcX, NpcY, NpcStruct.npcdrop[Map, Victim, i].Value, NpcStruct.npcdrop[Map, Victim, i].ItemNum, NpcStruct.npcdrop[Map, Victim, i].ItemType, 0); }
                                SendData.sendMapItem(Map, NullMapItem);
                            }
                        }
                    }
                }


                //GOLD
                PlayerRelations.givePlayerGold(Attacker, NpcStruct.npc[Map, Victim].Gold);

                //Limpar dados de estudo de movimento
                NpcIA.clearPrevMove(Map, Victim);

                ///Temporizador para voltar
                NpcStruct.tempnpc[Map, Victim].RespawnTimer = Loops.TickCount.ElapsedMilliseconds + NpcStruct.npc[Map, Victim].Respawn;
            }
        }
        //*********************************************************************************************
        // playerAttackPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Determinado jogador efetua um ataque em outro jogador determinado
        //*********************************************************************************************
        public static void playerAttackPlayer(int Attacker, int Victim, int isSpell = 0, int Map = 0, bool isPassive = false, int skill_level = 0, int super_damage = 0, bool is_pet = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim, isSpell, Map, isPassive, skill_level, super_damage, is_pet) != null)
            {
                return;
            }

            //CÓDIGO
            if (Map == 0) { Map = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].Map); }
            int Dir = character[Attacker, player[Attacker].SelectedChar].Dir;
            int VictimX = character[Victim, player[Victim].SelectedChar].X;
            int VictimY = character[Victim, player[Victim].SelectedChar].Y;
            int AttackerX = character[Attacker, player[Attacker].SelectedChar].X;
            int AttackerY = character[Attacker, player[Attacker].SelectedChar].Y;
            int PlayerX = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].X);
            int PlayerY = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].Y);
            int Damage = 0;

            bool is_critical = false;

            if (tempplayer[Victim].isDead == true) { return; }
            if (!MapStruct.tempmap[Map].WarActive)
            {
                if (character[Victim, player[Victim].SelectedChar].Level < 10) { return; }
                if (!MapStruct.MapIsPVP(Map)) { return; }
            }

            if ((!isPassive) && (isSpell == 0)) { SkillRelations.skillPassive(Attacker, Globals.Target_Player, Victim); }
            if ((tempplayer[Victim].Vitality <= 0) || (tempplayer[Victim].isDead)) { return; }
            if ((!MapStruct.tempmap[Map].WarActive) && (!character[Attacker, player[Attacker].SelectedChar].PVP)) { return; }

            if (tempplayer[Victim].Reflect)
            {
                SendData.sendAnimation(Map, Globals.Target_Player, Victim, 155);
                SendData.sendAnimation(Map, Globals.Target_Player, Attacker, 156);
                playerAttackPlayer(Victim, Attacker, 0, 0, false, 0, PlayerRelations.getPlayerDefense(Victim) * 2);
                tempplayer[Victim].Reflect = false;
                tempplayer[Victim].ReflectTimer = 0;
                return;
            }

            //Cálculo do dano

            //Magias
            if (isSpell > 0)
            {
                if (character[Victim, player[Victim].SelectedChar].ClassId == 6)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Victim, i].num == 39) && (skill[Victim, i].level > 0))
                        {
                            //Desviar do golpe?
                            int parry_test = Globals.Rand(0, 100);

                            if (parry_test <= (PlayerRelations.getPlayerParry(Victim) - PlayerRelations.getPlayerCritical(Attacker)))
                            {
                                SendData.sendActionMsg(Victim, lang.attack_missed, Globals.ColorWhite, character[Victim, player[Victim].SelectedChar].X, character[Victim, player[Victim].SelectedChar].Y, 1, 0, Map);
                                return;
                            }
                            break;
                        }
                    }
                }

                int skill_slot = 0;

                if (!isPassive) { skill_slot = SkillRelations.getPlayerSkillSlot(Attacker, isSpell); }
                else { skill_slot = SkillRelations.getPlayerSkillSlot(Attacker, isSpell, true); }

                if (skill_slot == 0) { return; }

                int extra_spellbuff = 0;

                for (int i = 1; i < Globals.MaxSpellBuffs; i++)
                {
                    if (pspellbuff[Attacker, i].active)
                    {
                        if (pspellbuff[Attacker, i].timer > Loops.TickCount.ElapsedMilliseconds) { extra_spellbuff += pspellbuff[Attacker, i].value; }
                        else
                        {
                            pspellbuff[Attacker, i].value = 0;
                            pspellbuff[Attacker, i].type = 0;
                            pspellbuff[Attacker, i].timer = 0;
                            pspellbuff[Attacker, i].active = false;
                        }
                    }
                }

                //Multiplicador de dano
                double multiplier = Convert.ToDouble(SkillStruct.skill[isSpell].scope) / 7.2;

                //Elemento mágico multiplicado
                double min_damage = PlayerRelations.getPlayerMinMagic(Attacker);
                double max_damage = PlayerRelations.getPlayerMaxMagic(Attacker);


                if (hotkey[Attacker, skill_slot].num > Globals.MaxPlayer_Skills)
                {
                    hotkey[Attacker, skill_slot].num = 0;
                    return;
                }

                //Multiplicador de nível
                double levelmultiplier = (1.0 + multiplier) * skill[Attacker, hotkey[Attacker, skill_slot].num].level; //Except

                //Verificando se a skill teve algum problema e corrigindo
                if (levelmultiplier < 1.0) { levelmultiplier = 1.0; }

                //Dano total que pode ser causado
                double totaldamage = max_damage + (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) * levelmultiplier);
                double totalmindamage = min_damage + (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) * levelmultiplier);

                //Passamos para int para tornar o dano exato
                int MinDamage = Convert.ToInt32(totalmindamage);
                int MaxDamage = Convert.ToInt32(totaldamage) + 1;

                if (MinDamage >= MaxDamage) { MaxDamage = MinDamage; }

                //Definição geral do dano
                Damage = Globals.Rand(MinDamage, MaxDamage);
                Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                if (character[Victim, player[Victim].SelectedChar].ClassId == 3)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Victim, i].num == 56) && (skill[Victim, i].level > 0))
                        {
                            Damage -= ((Damage / 100) * (3 * skill[Victim, i].level));
                        }
                    }
                }
                Damage -= ((Damage / 100) * PlayerRelations.getPlayerMagicDef(Victim));

                if (tempplayer[Attacker].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(Victim, lang.damage_reduced + " ", Globals.ColorWhite, VictimX, VictimY, 1, 0, Map);
                    tempplayer[Attacker].ReduceDamage = 0;
                }

                if (isSpell == 36)
                {
                    Damage += ((Damage / 100) * PlayerRelations.getPlayerDefense(Attacker));
                }

                if (character[Attacker, player[Attacker].SelectedChar].ClassId == 6)
                {

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Attacker, i].num == 42) && (skill[Attacker, i].level > 0))
                        {
                            //Dano crítico?
                            int critical_t = Globals.Rand(0, 100);

                            if (critical_t <= PlayerRelations.getPlayerCritical(Attacker))
                            {
                                Damage = Convert.ToInt32((Convert.ToDouble(Damage) * 1.5));
                                SendData.sendAnimation(Map, Globals.Target_Player, Victim, 152);
                            }
                            break;
                        }
                        if ((skill[Attacker, i].num == 41) && (skill[Attacker, i].level > 0))
                        {
                            if (isSpell == 40)
                            {
                                int open_passive = SkillRelations.getOpenPassiveEffect(Attacker);

                                if (open_passive == 0) { return; }

                                ppassiveffect[Attacker, open_passive].spellnum = skill[Attacker, i].num;
                                ppassiveffect[Attacker, open_passive].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[skill[Attacker, i].num].passive_interval;
                                ppassiveffect[Attacker, open_passive].target = Victim;
                                ppassiveffect[Attacker, open_passive].targettype = Globals.Target_Player;
                                ppassiveffect[Attacker, open_passive].type = 1;
                                ppassiveffect[Attacker, open_passive].active = true;
                            }
                            //Dano crítico?
                            int critical_t = Globals.Rand(0, 100);

                            if (critical_t <= PlayerRelations.getPlayerCritical(Attacker))
                            {
                                Damage = Convert.ToInt32((Convert.ToDouble(Damage) * 1.5));
                                SendData.sendAnimation(Map, Globals.Target_Player, Victim, 152);
                            }
                            break;
                        }
                    }
                }

                if (Damage < 1)
                {
                    SendData.sendActionMsg(Victim, lang.resisted, Globals.ColorPink, VictimX, VictimY, 1, 0, Map);
                    return;
                }

                if (extra_spellbuff > 0)
                {
                    //BUFFF :DDDD
                    Damage += (Damage / 100) * extra_spellbuff;
                }

                int drain = SkillStruct.skill[isSpell].drain;

                //Drenagem de vida?
                if (drain > 0)
                {
                    double real_drain = (Convert.ToDouble(Damage) / 100) * drain;
                    PlayerLogic.HealPlayer(Attacker, Convert.ToInt32(real_drain));
                    //SendData.sendActionMsg(Attacker, Convert.ToInt32(real_drain).ToString(), Globals.ColorGreen, PlayerX, PlayerY, 1, 1);
                    //SendData.sendPlayerVitalityToMap(Map, Attacker, tempplayer[Attacker].Vitality);
                }
            }
            //Ataques básicos
            else
            {
                if (tempplayer[Attacker].Blind)
                {
                    SendData.sendActionMsg(Attacker, lang.attack_missed, Globals.ColorWhite, VictimX, VictimY, 1, 0, Map);
                    return;
                }

                //Desviar do golpe?
                int parry_test = Globals.Rand(0, 100);

                if (parry_test <= (PlayerRelations.getPlayerParry(Victim) - PlayerRelations.getPlayerCritical(Attacker)))
                {
                    SendData.sendActionMsg(Victim, lang.attack_missed, Globals.ColorWhite, VictimX, VictimY, 1, 0, Map);
                    return;
                }

                //Dano comum
                int MinDamage = PlayerRelations.getPlayerMinAttack(Attacker);
                int MaxDamage = PlayerRelations.getPlayerMaxAttack(Attacker);

                if (is_pet)
                {
                    string equipment = character[Attacker, player[Attacker].SelectedChar].Equipment;
                    string[] equipdata = equipment.Split(',');
                    string[] petdata = equipdata[4].Split(';');

                    int petnum = Convert.ToInt32(petdata[0]);
                    int petlvl = Convert.ToInt32(petdata[1]);

                    MinDamage = (Convert.ToInt32(ItemStruct.item[petnum].damage_variance)) + ((petlvl / 2) * Convert.ToInt32(ItemStruct.item[petnum].damage_formula));
                    MaxDamage = (Convert.ToInt32(ItemStruct.item[petnum].damage_variance)) + ((petlvl) * Convert.ToInt32(ItemStruct.item[petnum].damage_formula));

                    if (MinDamage >= MaxDamage)
                    {
                        Damage = MinDamage + super_damage;
                        Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                        Damage -= ((Damage / 100) * PlayerRelations.getPlayerDefense(Victim));
                    }
                    else
                    {
                        Damage = (Globals.Rand(MinDamage, MaxDamage)) + super_damage;
                        Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                        Damage -= ((Damage / 100) * PlayerRelations.getPlayerDefense(Victim));
                    }//

                    SendData.sendAnimation(Map, Globals.Target_Player, Victim, ItemStruct.item[petnum].animation_id);
                    SendData.sendActionMsg(Victim, "-" + Damage.ToString(), Globals.ColorPurple, VictimX, VictimY, 1, character[Attacker, player[Attacker].SelectedChar].Dir, Map);
                    goto important;
                }

                if (MinDamage >= MaxDamage)
                {
                    Damage = MinDamage + super_damage;
                    Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                    Damage -= ((Damage / 100) * PlayerRelations.getPlayerDefense(Victim));
                }
                else
                {
                    Damage = (Globals.Rand(MinDamage, MaxDamage)) + super_damage;
                    Damage -= (Damage / 100) * tempplayer[Attacker].ReduceDamage;
                    Damage -= ((Damage / 100) * PlayerRelations.getPlayerDefense(Victim));
                }//

                if (character[Attacker, player[Attacker].SelectedChar].ClassId == 2)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((skill[Attacker, i].num == 52) && (skill[Attacker, i].level > 0))
                        {
                            Damage += ((PlayerRelations.getPlayerMaxVitality(Victim) / 100) * (2 + skill[Attacker, i].level));
                        }
                    }
                }

                if (tempplayer[Attacker].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(Victim, lang.damage_reduced, Globals.ColorWhite, VictimX, VictimY, 1, 0, Map);
                    tempplayer[Attacker].ReduceDamage = 0;
                }

                //Dano crítico?
                int critical_test = Globals.Rand(0, 100);

                if (critical_test <= PlayerRelations.getPlayerCritical(Attacker))
                {
                    Damage = Convert.ToInt32((Convert.ToDouble(Damage) * 1.5));
                    is_critical = true;
                }

                //Dano e animação
                SendData.sendAnimation(Map, Globals.Target_Player, Victim, 7);
            }

            if (is_critical)
            {
                int true_range = 0;
                for (int i = 1; i <= 2; i++)
                {
                    if (MovementRelations.canThrowPlayer(Victim, character[Attacker, player[Attacker].SelectedChar].Dir, i))
                    {
                        true_range += 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (true_range < 2)
                {
                    Damage += 2 - true_range;
                }

                if (true_range > 0)
                {
                    MovementRelations.throwPlayer(Victim, character[Attacker, player[Attacker].SelectedChar].Dir, true_range);
                }

                if (tempplayer[Victim].preparingskill > 0)
                {
                    tempplayer[Victim].preparingskill = 0;
                    tempplayer[Victim].preparingskillslot = 0;
                    tempplayer[Victim].skilltimer = 0;
                    SendData.sendActionMsg(Victim, lang.spell_broken, Globals.ColorPink, VictimX, VictimY, 1, 0, Map);
                    tempplayer[Victim].movespeed = Globals.NormalMoveSpeed;
                    SendData.sendMoveSpeed(Globals.Target_Player, Victim);
                    SendData.sendBrokeSkill(Victim);
                }
                SendData.sendActionMsg(Victim, "-" + Damage.ToString(), 1, VictimX, VictimY, 1, character[Attacker, player[Attacker].SelectedChar].Dir, Map);
            }
            else
            {
                if (isSpell > 0) { SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), Globals.ColorPink, VictimX, VictimY, Globals.Action_Msg_Scroll, character[Attacker, player[Attacker].SelectedChar].Dir, Map); }
                else { SendData.sendActionMsg(Attacker, "-" + Damage.ToString(), 4, VictimX, VictimY, Globals.Action_Msg_Scroll, character[Attacker, player[Attacker].SelectedChar].Dir, Map); }
            }

            important:
            //Nova vida do jogador
            tempplayer[Victim].Vitality -= Damage;

            //Sleep?
            if (tempplayer[Victim].Sleeping)
            {
                tempplayer[Victim].Sleeping = false;
                tempplayer[Victim].SleepTimer = 0;
                SendData.sendSleep(Map, 2, Victim, 0);
            }

            //Enviamos a nova vida do jogador
            SendData.sendPlayerVitalityToMap(Map, Victim, tempplayer[Victim].Vitality);

            if (tempplayer[Victim].Vitality <= 0)
            {
                tempplayer[Victim].PetTarget = 0;
                tempplayer[Victim].PetTargetType = 0;
                if (!MapStruct.tempmap[Map].WarActive)
                {
                    if (!tempplayer[Victim].SORE)
                    {
                        int lvd = character[Attacker, player[Attacker].SelectedChar].Level - character[Victim, player[Victim].SelectedChar].Level;
                        if (lvd > 5)
                        {
                            if (!character[Victim, player[Victim].SelectedChar].PVP)
                            {
                                tempplayer[Attacker].SORE = true;
                                character[Attacker, player[Attacker].SelectedChar].PVPPenalty = 300000 + Loops.TickCount.ElapsedMilliseconds;
                                SendData.sendPlayerSoreToMap(Attacker);
                                SendData.sendPlayerPvpSoreTimer(Attacker);
                                SendData.sendAnimation(Map, Globals.Target_Player, Attacker, 147);

                                //Relacionado a definição de vida para novos e velhos jogadores
                                if (character[Attacker, player[Attacker].SelectedChar].Vitality > PlayerRelations.getPlayerMaxVitality(Attacker))
                                {
                                    character[Attacker, player[Attacker].SelectedChar].Vitality = PlayerRelations.getPlayerMaxVitality(Attacker);
                                    tempplayer[Attacker].Vitality = PlayerRelations.getPlayerMaxVitality(Attacker);
                                    SendData.sendPlayerVitalityToMap(Map, Attacker, tempplayer[Attacker].Vitality);
                                    if (tempplayer[Attacker].Party > 0)
                                    {
                                        SendData.sendPlayerVitalityToParty(tempplayer[Attacker].Party, Attacker, tempplayer[Attacker].Vitality);
                                    }
                                }


                                //Relacionado a definição de mana para novos e velhos jogadores
                                if (character[Attacker, player[Attacker].SelectedChar].Spirit > PlayerRelations.getPlayerMaxSpirit(Attacker))
                                {
                                    character[Attacker, player[Attacker].SelectedChar].Spirit = PlayerRelations.getPlayerMaxSpirit(Attacker);
                                    tempplayer[Attacker].Spirit = PlayerRelations.getPlayerMaxSpirit(Attacker);
                                    SendData.sendPlayerSpiritToMap(Map, Attacker, tempplayer[Attacker].Spirit);
                                    if (tempplayer[Attacker].Party > 0)
                                    {
                                        SendData.sendPlayerSpiritToParty(tempplayer[Attacker].Party, Attacker, tempplayer[Attacker].Spirit);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Matou na ordem, eai?
                        }
                    }
                }
                else
                {
                    int exp = character[Victim, player[Victim].SelectedChar].Exp / 2;
                    PlayerRelations.givePlayerExp(Attacker, exp);
                    character[Victim, player[Victim].SelectedChar].Exp -= exp;
                    SendData.sendPlayerExp(Victim);
                    SendData.sendActionMsg(Victim, "-" + exp + lang.exp, 0, PlayerX, PlayerY, 1, 0, Map);
                    SendData.sendAnimation(Map, Globals.Target_Player, Attacker, 148);
                    tempplayer[Victim].SORE = false;
                    character[Victim, player[Victim].SelectedChar].PVPPenalty = 0;
                    character[Victim, player[Victim].SelectedChar].PVP = false;
                    character[Victim, player[Victim].SelectedChar].PVPBanTimer = 60000 + Loops.TickCount.ElapsedMilliseconds;
                    SendData.sendPlayerPvpToMap(Victim);
                    SendData.sendPlayerSoreToMap(Victim);
                    SendData.sendPlayerPvpBanTimer(Victim);
                }
                //Morte
                tempplayer[Victim].isDead = true;
                SendData.sendPlayerDeathToMap(Victim);
            }
        }
        //*********************************************************************************************
        // CanplayerAttackNpc / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado jogador pode atacar determinado npc no contexto em que está
        //*********************************************************************************************
        public static bool canPlayerAttackNpc(int Attacker, int Victim)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim));
            }

            //CÓDIGO
            int Map = character[Attacker, player[Attacker].SelectedChar].Map;
            int Dir = character[Attacker, player[Attacker].SelectedChar].Dir;
            int NpcX = 0;
            int NpcY = 0;
            int PlayerX = character[Attacker, player[Attacker].SelectedChar].X;
            int PlayerY = character[Attacker, player[Attacker].SelectedChar].Y;

            if (NpcStruct.tempnpc[Map, Victim].Dead == true) { return false; }
            //if (NpcStruct.tempnpc[Map, Victim].guildnum == character[Attacker, player[Attacker].SelectedChar].Guild) { return false; }

            switch (Dir)
            {
                case 8:
                    NpcX = NpcStruct.tempnpc[Map, Victim].X;
                    NpcY = NpcStruct.tempnpc[Map, Victim].Y + 1;
                    break;
                case 2:
                    NpcX = NpcStruct.tempnpc[Map, Victim].X;
                    NpcY = NpcStruct.tempnpc[Map, Victim].Y - 1;
                    break;
                case 4:
                    NpcX = NpcStruct.tempnpc[Map, Victim].X + 1;
                    NpcY = NpcStruct.tempnpc[Map, Victim].Y;
                    break;
                case 6:
                    NpcX = NpcStruct.tempnpc[Map, Victim].X - 1;
                    NpcY = NpcStruct.tempnpc[Map, Victim].Y;
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    return false;
            }

            if ((NpcX == PlayerX) && (NpcY == PlayerY))
            {
                return true;
            }

            return false;
        }
        //*********************************************************************************************
        // CanplayerAttackPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se determinado jogador pode atacar outro jogador determinado no contexto em que
        // ambos estão.
        //*********************************************************************************************
        public static bool canPlayerAttackPlayer(int Attacker, int Victim)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Attacker, Victim));
            }

            //CÓDIGO
            int Map = Convert.ToInt32(character[Attacker, player[Attacker].SelectedChar].Map);
            int Dir = character[Attacker, player[Attacker].SelectedChar].Dir;
            int VictimX = character[Victim, player[Victim].SelectedChar].X;
            int VictimY = character[Victim, player[Victim].SelectedChar].Y;
            int PlayerX = character[Attacker, player[Attacker].SelectedChar].X;
            int PlayerY = character[Attacker, player[Attacker].SelectedChar].Y;

            if (tempplayer[Victim].isDead == true) { return false; }

            if (!MapStruct.tempmap[Map].WarActive)
            {
                if (character[Victim, player[Victim].SelectedChar].Level < 10) { SendData.sendMsgToPlayer(Attacker, lang.pvp_level_restriction, Globals.ColorRed, Globals.Msg_Type_Server); return false; }
                if (!MapStruct.MapIsPVP(Map)) { SendData.sendMsgToPlayer(Attacker, lang.pvp_safe_zone, Globals.ColorRed, Globals.Msg_Type_Server); return false; }
            }

            switch (Dir)
            {
                case 8:
                    VictimY += 1;
                    break;
                case 2:
                    VictimY -= 1;
                    break;
                case 4:
                    VictimX += 1;
                    break;
                case 6:
                    VictimX -= 1;
                    break;
                default:
                    WinsockAsync.Log(String.Format("Direção nula"));
                    return false;
            }

            if ((VictimX == PlayerX) && (VictimY == PlayerY))
            {
                return true;
            }

            return false;
        }
    }
}
