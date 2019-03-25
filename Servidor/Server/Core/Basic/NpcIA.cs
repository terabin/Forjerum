using System;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Responsável pela inteligência artifical do NPC(monstro) dentro do jogo.
    // NpcIA.cs
    //*********************************************************************************************
    class NpcIA : Languages.LStruct
    {
        //*********************************************************************************************
        // NpcRespawned
        // Retorna determinado NPC
        //*********************************************************************************************
        public static void NpcRespawned(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            NpcStruct.tempnpc[map, id].Dead = false;
            NpcStruct.tempnpc[map, id].Vitality = NpcStruct.npc[map, id].Vitality;
            NpcStruct.tempnpc[map, id].X = NpcStruct.npc[map, id].X;
            NpcStruct.tempnpc[map, id].Y = NpcStruct.npc[map, id].Y;
            NpcStruct.tempnpc[map, id].Target = 0;
            SendData.sendNpcToMap(map, id);
        }
        //*********************************************************************************************
        // ExecuteSkill
        // O mesmo do jogador, apesar de ter muita coisa que pode ser jogada fora, necessita
        // de alguma revisão
        //*********************************************************************************************
        public static void ExecuteSkill(int Map, int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, s) != null)
            {
                return;
            }

            //CÓDIGO
            int isSpell = NpcStruct.tempnpc[Map, s].preparingskill;
            int TargetType = 1; // FUCK
            int Target = NpcStruct.tempnpc[Map, s].Target;
            int NpcX = NpcStruct.tempnpc[Map, s].X;
            int NpcY = NpcStruct.tempnpc[Map, s].Y;

            //if (NpcStruct.tempnpc[Map, s].Spirit < SkillStruct.skill[isSpell].mp_cost) { return; }

            if (Target == 0) {
                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                return; 
            }

            //PLAYER
            if (TargetType == 1)
            {
                //Verificar se o jogador não se desconectou no processo
                if ((!WinsockAsync.Clients[(UserConnection.getS(Target))].IsConnected) || (Target > Globals.Player_Highs) || (Target < 0))
                {
                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                    return;
                }

                int PlayerX = PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X;
                int PlayerY = PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X;

                //Calcular distância
                int n = SkillStruct.skill[isSpell].tp_gain;
                int DistanceX = NpcX - PlayerX;
                int DistanceY = NpcY - PlayerY;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    //Atualiza MP
                    NpcStruct.tempnpc[Map, s].Spirit -= SkillStruct.skill[isSpell].mp_cost;

                    //Por enquanto não champz
                    //SendData.sendPlayerSpiritToMap(Map, s, PlayerStruct.tempplayer[s].Spirit);

                    //Execução da magia
                    if (SkillStruct.skill[isSpell].damage_type == 1)
                    {
                        if (SkillStruct.skill[isSpell].repeats <= 1)
                        {
                            if ((SkillStruct.skill[isSpell].type) == 0)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 6)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);

                                int spellbuff = SkillRelations.getOpenSpellBuff(s);

                                PlayerStruct.pspellbuff[s, spellbuff].active = true;
                                PlayerStruct.pspellbuff[s, spellbuff].type = 1; //DAMAGE
                                PlayerStruct.pspellbuff[s, spellbuff].value = SkillStruct.skill[isSpell].range_effect;
                                PlayerStruct.pspellbuff[s, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SkillStruct.skill[isSpell].note.Split(',')[2]);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 7)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);

                                PlayerStruct.tempplayer[Target].Sleeping = true;
                                PlayerStruct.tempplayer[Target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[isSpell].interval;

                                SendData.sendSleep(Map, TargetType, Target, 1);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 8)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, s, Target, isSpell);
                                PlayerStruct.tempplayer[Target].Stunned = true;
                                PlayerStruct.tempplayer[Target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[isSpell].interval;

                                SendData.sendStun(Map, TargetType, Target, 1);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 9)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 11)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);

                                int true_range = 0;
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (MovementRelations.canThrowPlayer(Target, NpcStruct.tempnpc[Map, s].Dir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (true_range > 0)
                                {
                                    MovementRelations.throwPlayer(Target, NpcStruct.tempnpc[Map, s].Dir, true_range);
                                }

                                if (PlayerStruct.tempplayer[Target].preparingskill > 0)
                                {
                                    PlayerStruct.tempplayer[Target].preparingskill = 0;
                                    PlayerStruct.tempplayer[Target].preparingskillslot = 0;
                                    PlayerStruct.tempplayer[Target].skilltimer = 0;
                                    SendData.sendActionMsg(Target, lang.spell_broken, Globals.ColorPink, PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X, PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y, 1, 0, Map);
                                    PlayerStruct.tempplayer[Target].movespeed = Globals.NormalMoveSpeed;
                                    SendData.sendMoveSpeed(Globals.Target_Player, Target);
                                    SendData.sendBrokeSkill(Target);
                                }

                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 12)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }

                                int true_range = 0;
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (MovementRelations.canThrowNpc(Map, s, NpcStruct.tempnpc[Map, s].Dir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (true_range > 0)
                                {
                                    MovementRelations.throwNpc(Map, s, NpcStruct.tempnpc[Map, s].Dir, true_range);
                                }

                                //Tudo ok
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, s, Target, isSpell);

                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            }

                            if ((SkillStruct.skill[isSpell].type) == 1)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SkillStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }
                            if ((SkillStruct.skill[isSpell].type) == 2)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SkillStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }
                            if ((SkillStruct.skill[isSpell].type) == 3)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SkillStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }
                            if ((SkillStruct.skill[isSpell].type) == 4)
                            {
                                if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SkillStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y + r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                            if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - r == PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, s, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, s, Target, isSpell);
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }
                        }
                        else
                        {
                            if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                            {
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }
                            int temp_spell = PlayerRelations.getOpenTempSpell(Target);
                            SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                            PlayerStruct.ptempspell[Target, temp_spell].active = true;
                            PlayerStruct.ptempspell[Target, temp_spell].attacker = s;
                            PlayerStruct.ptempspell[Target, temp_spell].spellnum = isSpell;
                            PlayerStruct.ptempspell[Target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[isSpell].interval;
                            PlayerStruct.ptempspell[Target, temp_spell].repeats = SkillStruct.skill[isSpell].repeats;
                            PlayerStruct.ptempspell[Target, temp_spell].anim = SkillStruct.skill[isSpell].second_anim;
                            PlayerStruct.ptempspell[Target, temp_spell].area_range = SkillStruct.skill[isSpell].range_effect;
                            PlayerStruct.ptempspell[Target, temp_spell].is_line = SkillStruct.skill[isSpell].is_line;
                            PlayerStruct.ptempspell[Target, temp_spell].is_heal = false;
                            PlayerStruct.ptempspell[Target, temp_spell].fast_buff = false;
                            if (SkillStruct.skill[isSpell].slow)
                            {
                                PlayerStruct.tempplayer[Target].movespeed -= 0.5;
                                SendData.sendMoveSpeed(1, Target);
                            }
                            NpcStruct.tempnpc[Map, s].preparingskill = 0;
                            NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                        }
                    }
                    if (SkillStruct.skill[isSpell].damage_type == 2)
                    {
                        if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                        {
                            NpcStruct.tempnpc[Map, s].preparingskill = 0;
                            NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            return;
                        }
                        //Tudo ok
                        SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                        NpcAttackPlayer(Map, s, Target, isSpell);
                        NpcStruct.tempnpc[Map, s].preparingskill = 0;
                        NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                    }
                    //Cura
                    if (SkillStruct.skill[isSpell].damage_type == 3)
                    {
                        if ((SkillStruct.skill[isSpell].type) == 1)
                        {
                            if (!(PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == Map))
                            {
                                NpcStruct.tempnpc[Map, s].preparingskill = 0;
                                NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                                return;
                            }

                            int temp_spell = PlayerRelations.getOpenTempSpell(Target);
                            SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                            PlayerStruct.ptempspell[Target, temp_spell].active = true;
                            PlayerStruct.ptempspell[Target, temp_spell].attacker = s;
                            PlayerStruct.ptempspell[Target, temp_spell].spellnum = isSpell;
                            PlayerStruct.ptempspell[Target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[isSpell].interval;
                            PlayerStruct.ptempspell[Target, temp_spell].repeats = SkillStruct.skill[isSpell].repeats;
                            PlayerStruct.ptempspell[Target, temp_spell].anim = SkillStruct.skill[isSpell].second_anim;
                            PlayerStruct.ptempspell[Target, temp_spell].area_range = SkillStruct.skill[isSpell].range_effect;
                            PlayerStruct.ptempspell[Target, temp_spell].is_line = SkillStruct.skill[isSpell].is_line;
                            PlayerStruct.ptempspell[Target, temp_spell].is_heal = true;
                            PlayerStruct.ptempspell[Target, temp_spell].fast_buff = true;

                            PlayerStruct.tempplayer[Target].movespeed = Globals.FastMoveSpeed;
                            SendData.sendMoveSpeed(1, Target);

                            NpcStruct.tempnpc[Map, s].preparingskill = 0;
                            NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            if (Target != s)
                            {
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }
                            return;
                        }
                        if ((SkillStruct.skill[isSpell].type) == 0)
                        {


                            //Tudo ok
                            SendData.sendAnimation(Map, TargetType, Target, SkillStruct.skill[isSpell].animation_id);
                            NpcStruct.tempnpc[Map, s].preparingskill = 0;
                            NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        }
                    }
                }
                //Não está! LOL
                else
                {
                    NpcStruct.tempnpc[Map, s].preparingskill = 0;
                    NpcStruct.tempnpc[Map, s].preparingskillslot = 0;
                }
            }
        }
        //*********************************************************************************************
        // NpcAttackPlayer
        // Cálculo geral do ataque executado pelo NPC e seu contexto
        //*********************************************************************************************
        public static void NpcAttackPlayer(int map, int id, int target, int isSpell = 0, int Map = 0, bool isPassive = false, int skill_level = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, target, isSpell, Map, isPassive, skill_level) != null)
            {
                return;
            }

            //CÓDIGO
            if (target <= 0) { return; }

            int PlayerX = Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X);
            int PlayerY = Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y);
            int NpcX = NpcStruct.tempnpc[map, id].X;
            int NpcY = NpcStruct.tempnpc[map, id].Y;

            SendData.sendAnimation(map, 1, target, NpcStruct.npc[map, id].Animation);

            int damage = 0;

            if (PlayerStruct.tempplayer[target].Reflect)
            {
                SendData.sendAnimation(map, Globals.Target_Player, target, 155);
                SendData.sendAnimation(map, Globals.Target_Npc, id, 156);
                CombatRelations.playerAttackNpc(target, id);
                PlayerStruct.tempplayer[target].Reflect = false;
                PlayerStruct.tempplayer[target].ReflectTimer = 0;
                return;
            }

            //Magias
            if (isSpell > 0)
            {
                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].ClassId == 6)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((PlayerStruct.skill[target, i].num == 39) && (PlayerStruct.skill[target, i].level > 0))
                        {
                            //Desviar do golpe?
                            int parry_test = Globals.Rand(0, 100);

                            if (parry_test <= (PlayerRelations.getPlayerParry(target) - PlayerRelations.getPlayerCritical(target)))
                            {
                                SendData.sendActionMsg(target, lang.attack_missed, Globals.ColorWhite, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y, 1, 0, map);
                                return;
                            }
                            break;
                        }
                    }
                }
                //Multiplicador de dano
                double multiplier = Convert.ToDouble(SkillStruct.skill[isSpell].scope) / 10;

                //Elemento mágico multiplicado
                double min_damage = (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) / 4) * multiplier;
                double max_damage = (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) / 2) * multiplier;

                //Dano total que pode ser causado
                double totaldamage = (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) + max_damage) + NpcStruct.npc[map, id].MagicAttack;
                double totalmindamage = ((Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) / 100) * 20) + NpcStruct.npc[map, id].MagicAttack;

                //Passamos para int para tornar o dano exato
                int MinDamage = Convert.ToInt32(totalmindamage);
                int MaxDamage = Convert.ToInt32(totaldamage) + 1;

                if (MinDamage >= MaxDamage) { MaxDamage = MinDamage; }

                //Definição geral do dano
                damage = Globals.Rand(MinDamage, MaxDamage);
                damage -= (damage / 100) * NpcStruct.tempnpc[map, id].ReduceDamage;
                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].ClassId == 3)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((PlayerStruct.skill[target, i].num == 56) && (PlayerStruct.skill[target, i].level > 0))
                        {
                            damage -= ((damage / 100) * (3 * PlayerStruct.skill[target, i].level));
                        }
                    }
                }
                damage -= ((damage / 100) * PlayerRelations.getPlayerMagicDef(target));

                if (NpcStruct.tempnpc[map, id].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(target, "Dano Reduzido", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    NpcStruct.tempnpc[map, id].ReduceDamage = 0;
                }

                if (damage < 1)
                {
                    SendData.sendActionMsg(target, "Resistiu", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                int drain = SkillStruct.skill[isSpell].drain;

                //Drenagem de vida?
                if (drain > 0)
                {
                    double real_drain = (Convert.ToDouble(damage) / 100) * drain;
                    NpcStruct.tempnpc[Map, id].Vitality += Convert.ToInt32(real_drain);
                    SendData.sendActionMsg(target, "+" + Convert.ToInt32(real_drain).ToString(), Globals.ColorGreen, NpcX, NpcY, 1, 0, map);
                }

                SendData.sendActionMsg(target, "-" + damage.ToString(), Globals.ColorPink, PlayerX, PlayerY, 1, NpcStruct.tempnpc[map, id].Dir, map);
            }
            //Ataques básicos
            else
            {
                if (NpcStruct.tempnpc[map, id].Blind)
                {
                    SendData.sendActionMsg(target, "Errou", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                //Desvia do golpe?
                int parry_test = Globals.Rand(0, 100);

                if (parry_test <= (PlayerRelations.getPlayerParry(target) - NpcStruct.getNpcCritical(map, id)))
                {
                    SendData.sendActionMsg(target, "Errou", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                //Variar 20% e diminuir pela defesa
                double Ddamage = Globals.Rand((NpcStruct.npc[map, id].Attack / 10) * 7, NpcStruct.npc[map, id].Attack);
                damage = Convert.ToInt32(Ddamage);
                damage -= (damage / 100) * NpcStruct.tempnpc[map, id].ReduceDamage;
                damage -= ((damage / 100) * PlayerRelations.getPlayerDefense(target));

                if (NpcStruct.tempnpc[map, id].ReduceDamage > 0)
                {
                    SendData.sendActionMsg(target, "Dano Reduzido", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    NpcStruct.tempnpc[map, id].ReduceDamage = 0;
                }

                //Se a defesa do jogador for muito alta, o dano é igual a 1
                if (damage < 1) { damage = 1; }

                //Dano crítico?
                bool is_critical = false;
                int critical_test = Globals.Rand(0, 100);

                if (critical_test <= NpcStruct.getNpcCritical(map, id))
                {
                    damage = Convert.ToInt32((Convert.ToDouble(damage) * 2));
                    is_critical = true;
                }


                if (NpcStruct.npc[map, id].KnockAttack)
                {
                    int true_range = 0;
                    for (int i = 1; i <= NpcStruct.npc[map, id].KnockRange; i++)
                    {
                        if (MovementRelations.canThrowPlayer(target, NpcStruct.tempnpc[map, id].Dir, i))
                        {
                            true_range += 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (true_range < NpcStruct.npc[map, id].KnockRange)
                    {
                        damage += NpcStruct.npc[map, id].KnockRange - true_range;
                    }

                    if (true_range > 0)
                    {
                        MovementRelations.throwPlayer(target, NpcStruct.tempnpc[map, id].Dir, true_range);
                    }

                    if (PlayerStruct.tempplayer[target].preparingskill > 0)
                    {
                        PlayerStruct.tempplayer[target].preparingskill = 0;
                        PlayerStruct.tempplayer[target].preparingskillslot = 0;
                        PlayerStruct.tempplayer[target].skilltimer = 0;
                        SendData.sendActionMsg(target, "Conjuração quebrada!", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                        PlayerStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(Globals.Target_Player, target);
                        SendData.sendBrokeSkill(target);
                    }

                }


                //Dano e animação
                if (is_critical)
                {
                    int true_range = 0;
                    for (int i = 1; i <= 2; i++)
                    {
                        if (MovementRelations.canThrowPlayer(target, NpcStruct.tempnpc[map, id].Dir, i))
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
                        damage += 2 - true_range;
                    }

                    if (true_range > 0)
                    {
                        MovementRelations.throwPlayer(target, NpcStruct.tempnpc[map, id].Dir, true_range);
                    }

                    if (!NpcStruct.npc[map, id].KnockAttack)
                    {
                        if (PlayerStruct.tempplayer[target].preparingskill > 0)
                        {
                            PlayerStruct.tempplayer[target].preparingskill = 0;
                            PlayerStruct.tempplayer[target].preparingskillslot = 0;
                            PlayerStruct.tempplayer[target].skilltimer = 0;
                            SendData.sendActionMsg(target, "Conjuração quebrada!", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                            PlayerStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(Globals.Target_Player, target);
                            SendData.sendBrokeSkill(target);
                        }
                    }

                    SendData.sendActionMsg(target, "-" + damage.ToString(), 1, PlayerX, PlayerY, 1, NpcStruct.tempnpc[map, id].Dir, map);
                }
                else
                {
                    SendData.sendActionMsg(target, "-" + damage.ToString(), 4, PlayerX, PlayerY, 1, NpcStruct.tempnpc[map, id].Dir, map);
                }
            }

            //Nova vida
            PlayerStruct.tempplayer[target].Vitality = PlayerStruct.tempplayer[target].Vitality - damage;

            //Atualiza vida do jogador para quem está no mapa vendo ele
            SendData.sendPlayerVitalityToMap(map, target, PlayerStruct.tempplayer[target].Vitality);

            //Atualiza vida do jogador para quem está em seu grupo
            if (PlayerStruct.tempplayer[target].Party > 0)
            {
                SendData.sendPlayerVitalityToParty(PlayerStruct.tempplayer[target].Party, target, PlayerStruct.tempplayer[target].Vitality);
            }

            if (PlayerStruct.tempplayer[target].Vitality <= 0)
            {
                PlayerStruct.tempplayer[target].PetTarget = 0;
                PlayerStruct.tempplayer[target].PetTargetType = 0;
                //jogador morre
                for (int i = 1; i < Globals.MaxMapNpcs; i++)
                {
                    if (NpcStruct.tempnpc[map, i].Target == target)
                    {
                        NpcStruct.tempnpc[map, i].Target = 0;
                    }
                }
                PlayerStruct.tempplayer[target].isDead = true;
                SendData.sendPlayerDeathToMap(target);
            }
        }
        //*********************************************************************************************
        // NpcMove
        // Simplesmente executa o movimento de determinado NPC
        //*********************************************************************************************
        public static void NpcMove(int map, int id, byte dir)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir) != null)
            {
                return;
            }

            //CÓDIGO
            //Definição da nova posição
            if (dir == Globals.DirUp) { NpcStruct.tempnpc[map, id].Y -= 1; }
            if (dir == Globals.DirDown) { NpcStruct.tempnpc[map, id].Y += 1; }
            if (dir == Globals.DirLeft) { NpcStruct.tempnpc[map, id].X -= 1; }
            if (dir == Globals.DirRight) { NpcStruct.tempnpc[map, id].X += 1; }
            //Definir dados para estudo do próximo movimento
            DefinePrevMove(map, id);
            //Enviar movimento para o mapa
            SendData.sendNpcMove(map, id);
        }
        //*********************************************************************************************
        // clearPrevMove
        // Ponto importante da inteligência artificial baseada em "errando e aprendendo".
        //*********************************************************************************************
        public static void clearPrevMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            //Limpar dados de estudo de movimento
            for (int i = 1; i <= 6; i++)
            {
                NpcStruct.tempnpc[map, id].prev_move[i].x = 0;
                NpcStruct.tempnpc[map, id].prev_move[i].y = 0;
            }
        }
        //*********************************************************************************************
        // DefinePrevMove
        // Inteligência no movimento
        //*********************************************************************************************
        public static void DefinePrevMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            //Se o npc não tem alvo, no prob.
            if (NpcStruct.tempnpc[map, id].Target <= 0)
            {
                if ((NpcStruct.tempnpc[map, id].prev_move[6].x > 0) || (NpcStruct.tempnpc[map, id].prev_move[6].y > 0))
                {
                    clearPrevMove(map, id);
                }
                return;
            }

            //Re organizar todos os dados
            for (int i = 1; i <= 5; i++)
            {
                NpcStruct.tempnpc[map, id].prev_move[i].x = NpcStruct.tempnpc[map, id].prev_move[i + 1].x;
                NpcStruct.tempnpc[map, id].prev_move[i].y = NpcStruct.tempnpc[map, id].prev_move[i + 1].y;
            }

            //O dado adicionado equivale ao 6.
            NpcStruct.tempnpc[map, id].prev_move[6].x = NpcStruct.tempnpc[map, id].X;
            NpcStruct.tempnpc[map, id].prev_move[6].y = NpcStruct.tempnpc[map, id].Y;
        }
        //*********************************************************************************************
        // CanNpcMove
        // Determina se o NPC pode se mover
        //*********************************************************************************************
        public static bool CanNpcMove(int map, int id, byte dir, int x = 0, int y = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir, x, y) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir, x, y));
            }

            //CÓDIGO
            int target = NpcStruct.tempnpc[map, id].Target;
            if (x == 0) { x = NpcStruct.tempnpc[map, id].X; }
            if (y == 0) { y = NpcStruct.tempnpc[map, id].Y; }
            long attacktimer = NpcStruct.tempnpc[map, id].AttackTimer;

            if (NpcStruct.tempnpc[map, id].Dir != dir)
            {
                NpcStruct.tempnpc[map, id].Dir = dir;
                //SendNpcChangeDir(mapID, id, dir)
            }

            if ((dir == Globals.DirUp) && (Convert.ToBoolean(MapStruct.tile[map, x, y].UpBlock) == false)) { return false; }
            if ((dir == Globals.DirDown) && (Convert.ToBoolean(MapStruct.tile[map, x, y].DownBlock) == false)) { return false; }
            if ((dir == Globals.DirLeft) && (Convert.ToBoolean(MapStruct.tile[map, x, y].LeftBlock) == false)) { return false; }
            if ((dir == Globals.DirRight) && (Convert.ToBoolean(MapStruct.tile[map, x, y].RightBlock) == false)) { return false; }

            if (dir == Globals.DirUp) { y -= 1; }
            if (dir == Globals.DirDown) { y += 1; }
            if (dir == Globals.DirLeft) { x -= 1; }
            if (dir == Globals.DirRight) { x += 1; }

            //Verificar se é preciso adicionar algo
            for (int i = 1; i <= 6; i++)
            {
                if ((NpcStruct.tempnpc[map, id].prev_move[i].x == x) && (NpcStruct.tempnpc[map, id].prev_move[i].y == y))
                {
                    DefinePrevMove(map, id);
                    return false;
                }
            }

            if ((x < 0) || (x > Convert.ToInt32(MapStruct.map[map].max_width))) { return false; }
            if ((y < 0) || (y > Convert.ToInt32(MapStruct.map[map].max_height))) { return false; }

            if ((dir == Globals.DirUp) && (Convert.ToBoolean(MapStruct.tile[map, x, y].DownBlock) == false)) { return false; }
            if ((dir == Globals.DirDown) && (Convert.ToBoolean(MapStruct.tile[map, x, y].UpBlock) == false)) { return false; }
            if ((dir == Globals.DirLeft) && (Convert.ToBoolean(MapStruct.tile[map, x, y].RightBlock) == false)) { return false; }
            if ((dir == Globals.DirRight) && (Convert.ToBoolean(MapStruct.tile[map, x, y].LeftBlock) == false)) { return false; }

            if (MapStruct.tile[map, x, y].Data1 == "3") { return false; }
            if (MapStruct.tile[map, x, y].Data1 == "10") { return false; }
            if (MapStruct.tile[map, x, y].Data1 == "17") { return false; }
            if (MapStruct.tile[map, x, y].Data1 == "18") { return false; }
            if (MapStruct.tile[map, x, y].Data1 == "2") { return false; }

            //if (Convert.ToByte(MapStruct.tile[map, x, y].Passable) == Globals.NoPassable)  { return false; }
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.tempplayer[i].ingame == true)
                {
                    if (Convert.ToInt32(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map) == map)
                    {
                        if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == x) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == y)) 
                        {
                            if (NpcStruct.tempnpc[map, id].Target > 0)
                            {
                                NpcStruct.tempnpc[map, id].Target = i;
                            }
                            return false; 
                        }
                    }
                }
            }

            for (int i = 0; i <= MapStruct.tempmap[map].NpcCount; i++)
            {
                if (i != id)
                {
                    if ((NpcStruct.tempnpc[map, i].X == x) && (NpcStruct.tempnpc[map, i].Y == y) && (!NpcStruct.tempnpc[map, i].Dead)) { return false; }
                }
            }

            return true;
        }
        //*********************************************************************************************
        // CanNpcAttack
        // Verifica se o NPC pode atacar no devido contexto
        //*********************************************************************************************
        public static bool CanNpcAttack(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            int target = NpcStruct.tempnpc[map, id].Target;
            int x = NpcStruct.tempnpc[map, id].X;
            int y = NpcStruct.tempnpc[map, id].Y;
            int dir = NpcStruct.tempnpc[map, id].Dir;
            long attacktimer = NpcStruct.tempnpc[map, id].AttackTimer;

            if (dir == Globals.DirUp) { y -= 1; }
            if (dir == Globals.DirDown) { y += 1; }
            if (dir == Globals.DirLeft) { x -= 1; }
            if (dir == Globals.DirRight) { x += 1; }

            if (Loops.TickCount.ElapsedMilliseconds <= attacktimer) { return false; }
            if (Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map) != map) { return false; }

            if ((Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X) == x) && (Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y) == y))
            {

                //Tempo de ataque
                NpcStruct.tempnpc[map, id].AttackTimer = Loops.TickCount.ElapsedMilliseconds + 1000;
                return true;
            }

            return false;
        }
        //*********************************************************************************************
        // NpcHaveSpell
        //*********************************************************************************************
        public static int NpcHaveSpell(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Npc_Spells; i++)
            {
                if ((NpcStruct.nspell[map, id, i].spellnum > 0) && (Loops.TickCount.ElapsedMilliseconds > NpcStruct.nspell[map, id, i].cooldown))
                {
                    return i;
                }
            }
            return 0;
        }
        //*********************************************************************************************
        // getNpcSpellTarget
        // Retorna o algo de determinado cast do NPC
        //*********************************************************************************************
        public static int getNpcSpellTarget(int map, int id, int isSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, isSpell) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, isSpell));
            }

            //CÓDIGO
            int NpcX = NpcStruct.tempnpc[map, id].X;
            int NpcY = NpcStruct.tempnpc[map, id].Y;
            int PlayerX = 0;
            int PlayerY = 0;
            int DistanceX = 0;
            int DistanceY = 0;
            int Target = NpcStruct.tempnpc[map, id].Target;


            //Calcular distância
            int n = SkillStruct.skill[isSpell].tp_gain;
            if (PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].Map == map)
            {
                PlayerX = PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X;
                PlayerY = PlayerStruct.character[Target, PlayerStruct.player[Target].SelectedChar].X;
                DistanceX = NpcX - PlayerX;
                DistanceY = NpcY - PlayerY;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    return Target;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // CalculateMove
        // Calcula o movimento de determinado NPC
        //*********************************************************************************************
        public static void CalculateMove(int map, int id, int sx, int sy)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, sx, sy) != null)
            {
                return;
            }

            //CÓDIGO
            int distX = NpcStruct.tempnpc[map, id].X - sx;
            int distY = NpcStruct.tempnpc[map, id].Y - sy;

            if (distX < 0) { distX *= -1; }
            if (distY < 0) { distY *= -1; }

            int temp_dist_x = NpcStruct.tempnpc[map, id].X - sx;
            int temp_dist_y = NpcStruct.tempnpc[map, id].Y - sy;
        }
        //*********************************************************************************************
        // CheckNpcMove
        // Movimentação do NPC baseado nos métodos acima. Verificavamos vários contextos baseados em
        // sx, sy, X, Y  que se baseiam em movimentos e tentativas prévias. Não há PathFind, apenas
        // quis tentar um método diferente. A ideia é que o NPC aprenda com seus erros. ;)
        //*********************************************************************************************
        public static void CheckNpcMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            int ivy = 0;
            int move = 0;
            int target = NpcStruct.tempnpc[map, id].Target;
            int x = NpcStruct.tempnpc[map, id].X;
            int y = NpcStruct.tempnpc[map, id].Y;
            int sx = 0;
            int sy = 0;
            int dir = NpcStruct.tempnpc[map, id].Dir;

            if (NpcStruct.tempnpc[map, id].Dead == true) { return; }
            if (NpcStruct.tempnpc[map, id].Vitality <= 0) { return; }
            if (NpcStruct.tempnpc[map, id].StunTimer > Loops.TickCount.ElapsedMilliseconds) { return; }
            if (NpcStruct.tempnpc[map, id].SleepTimer > Loops.TickCount.ElapsedMilliseconds) { return; }

            long curTimer = NpcStruct.tempnpc[map, id].curTimer;

            if (target == 0)
            {
                for (int i = 0; i <= Globals.Player_Highs; i++)
                {
                    if ((!PlayerStruct.tempplayer[i].isDead) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == map))
                    {
                        int n = NpcStruct.npc[map, id].Range;
                        int DistanceX = NpcStruct.tempnpc[map, id].X - PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X;
                        int DistanceY = NpcStruct.tempnpc[map, id].Y - PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y;

                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }
                        if ((DistanceX <= n) && (DistanceY <= n))
                        {
                            if (NpcStruct.npc[map, id].Type == 2)
                            {
                                NpcStruct.tempnpc[map, id].Target = i; //foca o jogador, talvez eu faça npc depois?
                            }
                        }
                    }
                }
            }


            if (Loops.TickCount.ElapsedMilliseconds < NpcStruct.tempnpc[map, id].MoveTimer)
            {
                return;
            }
            else
            {
                NpcStruct.tempnpc[map, id].MoveTimer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt64((((8 + (4 - NpcStruct.tempnpc[map, id].movespeed) - NpcStruct.tempnpc[map, id].movespeed) * 64))); //25ms de tolerância;
            }

            if (target == 0)
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    return;
                }

                if (Loops.TickCount.ElapsedMilliseconds > curTimer)
                {
                    sx = NpcStruct.tempnpc[map, id].curTargetX;
                    sy = NpcStruct.tempnpc[map, id].curTargetY;
                }
                else
                {
                    // return; idiota
                    sx = NpcStruct.tempnpc[map, id].curTargetX;
                    sy = NpcStruct.tempnpc[map, id].curTargetY;
                }

            }
            else
            {
                if ((UserConnection.getS(target) >= 0) && (WinsockAsync.Clients[(UserConnection.getS(target))].IsConnected == true))
                {
                    //Nosso alvo.
                    sx = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X; //Player(.Target).X
                    sy = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y;
                    
                    //Estudo sobre movimentos - Variáveis
                    int distX = NpcStruct.tempnpc[map, id].X - sx;
                    int distY = NpcStruct.tempnpc[map, id].Y - sy;

                    //Garantir que seja positivo
                    if (distX < 0) { distX *= -1; }
                    if (distY < 0) { distY *= -1; }

                    //Se a distância for de um tile, apagar estudos anteriores pois chegamos no alvo.
                    if ((distX <= 1) && (distY <= 0))
                    {
                        clearPrevMove(map, id);
                    }

                    //Se a distância for de um tile, apagar estudos anteriores pois chegamos no alvo.
                    if ((distX <= 0) && (distY <= 1))
                    {
                        clearPrevMove(map, id);
                    }
                }
                else
                {
                    NpcStruct.tempnpc[map, id].Target = 0;
                    return;
                }
            }

            if (x - 1 > sx) { move = 1; } // LEFT
            if (y - 1 > sy) { move = 2; } // UP

            if ((x - 1 > sx) && (y - 1 > sy))
            {
                move = Globals.Rand(1, 2);
            }

            if ((x - 1 == sx) && (y - 1 == sy))
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    move = 1;
                }
                else
                {
                    move = 2;
                }
            }

            if ((x + 1 == sx) && (y - 1 == sy))
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    move = 4;
                }
                else
                {
                    move = 2;
                }
            }

            if (x + 1 < sx) { move = 4; } // RIGHT
            if (y + 1 < sy) { move = 5; } // DOWN

            if ((x + 1 < sx) && (y + 1 < sy))
            {
                move = Globals.Rand(4, 5);
            }

            if ((x + 1 == sx) && (y + 1 == sy))
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    move = 4;
                }
                else
                {
                    move = 5;
                }
            }

            if ((x - 1 == sx) && (y + 1 == sy))
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    move = 1;
                }
                else
                {
                    move = 5;
                }
            }

            if ((x - 1 > sx) && (y + 1 < sy))
            {
                move = Globals.Rand(1, 2);
                if (move == 1) { move = 5; } else { move = 1; }
            }

            if ((x + 1 < sx) && (y - 1 > sy))
            {
                move = Globals.Rand(1, 2);
                if (move == 1) { move = 2; } else { move = 4; }
            }

            if ((Loops.TickCount.ElapsedMilliseconds > NpcStruct.tempnpc[map, id].curTimer) && (target == 0)) { move = 0; }

            if (target > 0)
            {
                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map != map)
                {
                    NpcStruct.tempnpc[map, id].Target = 0;
                    return;
                }

                int spellnum = NpcHaveSpell(map, id);

                if (spellnum > 0)
                {
                    int spelltarget = getNpcSpellTarget(map, id, spellnum);

                    if (spelltarget > 0)
                    {
                        NpcStruct.tempnpc[map, id].preparingskillslot = spellnum;
                        NpcStruct.tempnpc[map, id].preparingskill = NpcStruct.nspell[map, id, spellnum].spellnum;
                        NpcStruct.tempnpc[map, id].skilltimer = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[spellnum].speed * 100);
                        NpcStruct.nspell[map, id, spellnum].cooldown = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[spellnum].tp_cost * 1000);
                        SendData.sendAnimation(map, 2, id, 81);
                    }
                }
            }
            if (move == 0)
            {
                if (target > 0)
                {
                    if (x < Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X)) { ivy = Globals.DirRight; }
                    if (x > Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X)) { ivy = Globals.DirLeft; }
                    if (y < Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y)) { ivy = Globals.DirDown; }
                    if (y > Convert.ToInt32(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y)) { ivy = Globals.DirUp; }

                    if (ivy != dir)
                    {
                        dir = ivy;
                        NpcStruct.tempnpc[map, id].Dir = Convert.ToByte(dir);
                        SendData.sendNpcDir(map, id, ivy);
                    }
                }

                if (NpcStruct.tempnpc[map, id].preparingskill == 0)
                {
                    if (NpcIA.CanNpcAttack(map, id)) { NpcAttackPlayer(map, id, target); }
                }

                NpcStruct.tempnpc[map, id].Moving = 0;
                if (Loops.TickCount.ElapsedMilliseconds > NpcStruct.tempnpc[map, id].curTimer)
                {
                    //wow:
                    NpcStruct.tempnpc[map, id].curTargetX = Globals.Rand(NpcStruct.tempnpc[map, id].X - 5, NpcStruct.tempnpc[map, id].X + 5);
                    if (NpcStruct.tempnpc[map, id].curTargetX < 0) { NpcStruct.tempnpc[map, id].curTargetX = NpcStruct.tempnpc[map, id].curTargetX * -1; }
                    if (NpcStruct.tempnpc[map, id].curTargetX > Convert.ToInt32(MapStruct.map[map].max_width)) { NpcStruct.tempnpc[map, id].curTargetX -= 6; }
                    //wew:
                    NpcStruct.tempnpc[map, id].curTargetY = Globals.Rand(NpcStruct.tempnpc[map, id].Y - 5, NpcStruct.tempnpc[map, id].Y + 5);
                    if (NpcStruct.tempnpc[map, id].curTargetY < 0) { NpcStruct.tempnpc[map, id].curTargetY = NpcStruct.tempnpc[map, id].curTargetY * -1; }
                    if (NpcStruct.tempnpc[map, id].curTargetY > Convert.ToInt32(MapStruct.map[map].max_height)) { NpcStruct.tempnpc[map, id].curTargetY -= 6; }
                    NpcStruct.tempnpc[map, id].curTimer = Loops.TickCount.ElapsedMilliseconds + 5000; // 5 Segundos hibernando
                }
                return;
            }

            if (move == 1)
            {
                if (CanNpcMove(map, id, Globals.DirLeft))
                {
                    NpcMove(map, id, Globals.DirLeft);
                    return;
                }
                else
                {
                    ivy = Globals.Rand(1, 4);
                    ivy = ivy * 2;
                    if (ivy == Globals.DirUp)
                    {
                        if (CanNpcMove(map, id, Globals.DirUp)) { NpcMove(map, id, Globals.DirUp); return; }
                    }
                    if (ivy == Globals.DirDown)
                    {
                        if (CanNpcMove(map, id, Globals.DirDown)) { NpcMove(map, id, Globals.DirDown); return; }
                    }
                    if (ivy == Globals.DirRight)
                    {
                        if (CanNpcMove(map, id, Globals.DirRight)) { NpcMove(map, id, Globals.DirRight); return; }
                    }
                }
                NpcStruct.tempnpc[map, id].curTarget = NpcStruct.tempnpc[map, id].Current;
            }
            if (move == 2)
            {
                if (CanNpcMove(map, id, Globals.DirUp))
                {
                    NpcMove(map, id, Globals.DirUp);
                    return;
                }
                else
                {
                    ivy = Globals.Rand(1, 4);
                    ivy = ivy * 2;

                    
                    if (ivy == Globals.DirLeft)
                    {
                        if (CanNpcMove(map, id, Globals.DirLeft)) { NpcMove(map, id, Globals.DirLeft); return; }
                    }
                    if (ivy == Globals.DirDown)
                    {
                        if (CanNpcMove(map, id, Globals.DirDown)) { NpcMove(map, id, Globals.DirDown); return; }
                    }
                    if (ivy == Globals.DirRight)
                    {
                        if (CanNpcMove(map, id, Globals.DirRight)) { NpcMove(map, id, Globals.DirRight); return; }
                    }
                }
                NpcStruct.tempnpc[map, id].curTarget = NpcStruct.tempnpc[map, id].Current;
            }
            if (move == 4)
            {
                if (CanNpcMove(map, id, Globals.DirRight))
                {
                    NpcMove(map, id, Globals.DirRight);
                    return;
                }
                else
                {
                    ivy = Globals.Rand(1, 4);
                    ivy = ivy * 2;
                    if (ivy == Globals.DirUp)
                    {
                        if (CanNpcMove(map, id, Globals.DirUp)) { NpcMove(map, id, Globals.DirUp); return; }
                    }
                    if (ivy == Globals.DirDown)
                    {
                        if (CanNpcMove(map, id, Globals.DirDown)) { NpcMove(map, id, Globals.DirDown); return; }
                    }
                    if (ivy == Globals.DirLeft)
                    {
                        if (CanNpcMove(map, id, Globals.DirLeft)) { NpcMove(map, id, Globals.DirLeft); return; }
                    }
                }
                NpcStruct.tempnpc[map, id].curTarget = NpcStruct.tempnpc[map, id].Current;
            }
            if (move == 5)
            {
                if (CanNpcMove(map, id, Globals.DirDown))
                {
                    NpcMove(map, id, Globals.DirDown);
                    return;
                }
                else
                {
                    ivy = Globals.Rand(1, 4);
                    ivy = ivy * 2;

                    if (ivy == Globals.DirUp)
                    {
                        if (CanNpcMove(map, id, Globals.DirUp)) { NpcMove(map, id, Globals.DirUp); return; }
                    }
                    if (ivy == Globals.DirLeft)
                    {
                        if (CanNpcMove(map, id, Globals.DirLeft)) { NpcMove(map, id, Globals.DirLeft); return; }
                    }
                    if (ivy == Globals.DirRight)
                    {
                        if (CanNpcMove(map, id, Globals.DirRight)) { NpcMove(map, id, Globals.DirRight); return; }
                    }
                }
                NpcStruct.tempnpc[map, id].curTarget = NpcStruct.tempnpc[map, id].Current;
            }
        }
    }
}
