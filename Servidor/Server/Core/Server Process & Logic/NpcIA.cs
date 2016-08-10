using System;
using System.Reflection;

namespace FORJERUM
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            NStruct.tempnpc[map, id].Dead = false;
            NStruct.tempnpc[map, id].Vitality = NStruct.npc[map, id].Vitality;
            NStruct.tempnpc[map, id].X = NStruct.npc[map, id].X;
            NStruct.tempnpc[map, id].Y = NStruct.npc[map, id].Y;
            NStruct.tempnpc[map, id].Target = 0;
            SendData.Send_NpcToMap(map, id);
        }
        //*********************************************************************************************
        // ExecuteSkill
        // O mesmo do jogador, apesar de ter muita coisa que pode ser jogada fora, necessita
        // de alguma revisão
        //*********************************************************************************************
        public static void ExecuteSkill(int Map, int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, index) != null)
            {
                return;
            }

            //CÓDIGO
            int isSpell = NStruct.tempnpc[Map, index].preparingskill;
            int TargetType = 1; // FUCK
            int Target = NStruct.tempnpc[Map, index].Target;
            int NpcX = NStruct.tempnpc[Map, index].X;
            int NpcY = NStruct.tempnpc[Map, index].Y;

            //if (NStruct.tempnpc[Map, index].Spirit < SStruct.skill[isSpell].mp_cost) { return; }

            if (Target == 0) {
                NStruct.tempnpc[Map, index].preparingskill = 0;
                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                return; 
            }

            //PLAYER
            if (TargetType == 1)
            {
                //Verificar se o jogador não se desconectou no processo
                if ((!WinsockAsync.Clients[(UserConnection.Getindex(Target))].IsConnected) || (Target > Globals.Player_Highindex) || (Target < 0))
                {
                    NStruct.tempnpc[Map, index].preparingskill = 0;
                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                    return;
                }

                int PlayerX = PStruct.character[Target, PStruct.player[Target].SelectedChar].X;
                int PlayerY = PStruct.character[Target, PStruct.player[Target].SelectedChar].X;

                //Calcular distância
                int n = SStruct.skill[isSpell].tp_gain;
                int DistanceX = NpcX - PlayerX;
                int DistanceY = NpcY - PlayerY;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    //Atualiza MP
                    NStruct.tempnpc[Map, index].Spirit -= SStruct.skill[isSpell].mp_cost;

                    //Por enquanto não champz
                    //SendData.Send_PlayerSpiritToMap(Map, index, PStruct.tempplayer[index].Spirit);

                    //Execução da magia
                    if (SStruct.skill[isSpell].damage_type == 1)
                    {
                        if (SStruct.skill[isSpell].repeats <= 1)
                        {
                            if ((SStruct.skill[isSpell].type) == 0)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 6)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);

                                int spellbuff = PStruct.GetOpenSpellBuff(index);

                                PStruct.pspellbuff[index, spellbuff].active = true;
                                PStruct.pspellbuff[index, spellbuff].type = 1; //DAMAGE
                                PStruct.pspellbuff[index, spellbuff].value = SStruct.skill[isSpell].range_effect;
                                PStruct.pspellbuff[index, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SStruct.skill[isSpell].note.Split(',')[2]);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 7)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);

                                PStruct.tempplayer[Target].Sleeping = true;
                                PStruct.tempplayer[Target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[isSpell].interval;

                                SendData.Send_Sleep(Map, TargetType, Target, 1);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 8)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, index, Target, isSpell);
                                PStruct.tempplayer[Target].Stunned = true;
                                PStruct.tempplayer[Target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[isSpell].interval;

                                SendData.Send_Stun(Map, TargetType, Target, 1);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 9)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 11)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }

                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);

                                int true_range = 0;
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (PStruct.CanThrowPlayer(Target, NStruct.tempnpc[Map, index].Dir, i))
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
                                    PStruct.ThrowPlayer(Target, NStruct.tempnpc[Map, index].Dir, true_range);
                                }

                                if (PStruct.tempplayer[Target].preparingskill > 0)
                                {
                                    PStruct.tempplayer[Target].preparingskill = 0;
                                    PStruct.tempplayer[Target].preparingskillslot = 0;
                                    PStruct.tempplayer[Target].skilltimer = 0;
                                    SendData.Send_ActionMsg(Target, lang.spell_broken, Globals.ColorPink, PStruct.character[Target, PStruct.player[Target].SelectedChar].X, PStruct.character[Target, PStruct.player[Target].SelectedChar].Y, 1, 0, Map);
                                    PStruct.tempplayer[Target].movespeed = Globals.NormalMoveSpeed;
                                    SendData.Send_MoveSpeed(Globals.Target_Player, Target);
                                    SendData.Send_BrokeSkill(Target);
                                }

                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 12)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }

                                int true_range = 0;
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (PStruct.CanThrowNpc(Map, index, NStruct.tempnpc[Map, index].Dir, i))
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
                                    PStruct.ThrowNpc(Map, index, NStruct.tempnpc[Map, index].Dir, true_range);
                                }

                                //Tudo ok
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);

                                NpcAttackPlayer(Map, index, Target, isSpell);

                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            }

                            if ((SStruct.skill[isSpell].type) == 1)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }
                            if ((SStruct.skill[isSpell].type) == 2)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }
                            if ((SStruct.skill[isSpell].type) == 3)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }
                            if ((SStruct.skill[isSpell].type) == 4)
                            {
                                if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                                {
                                    NStruct.tempnpc[Map, index].preparingskill = 0;
                                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                    return;
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != Target))
                                    {
                                        for (int r = 1; r <= SStruct.skill[isSpell].range_effect; r++)
                                        {
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y + r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                            if ((PStruct.character[i, PStruct.player[i].SelectedChar].X - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].X) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y - r == PStruct.character[Target, PStruct.player[Target].SelectedChar].Y))
                                            {
                                                NpcAttackPlayer(Map, index, i, isSpell);
                                            }
                                        }
                                    }
                                }
                                SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                                NpcAttackPlayer(Map, index, Target, isSpell);
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }
                        }
                        else
                        {
                            if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                            {
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }
                            int temp_spell = PStruct.GetOpenPTempSpell(Target);
                            SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                            PStruct.ptempspell[Target, temp_spell].active = true;
                            PStruct.ptempspell[Target, temp_spell].attacker = index;
                            PStruct.ptempspell[Target, temp_spell].spellnum = isSpell;
                            PStruct.ptempspell[Target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[isSpell].interval;
                            PStruct.ptempspell[Target, temp_spell].repeats = SStruct.skill[isSpell].repeats;
                            PStruct.ptempspell[Target, temp_spell].anim = SStruct.skill[isSpell].second_anim;
                            PStruct.ptempspell[Target, temp_spell].area_range = SStruct.skill[isSpell].range_effect;
                            PStruct.ptempspell[Target, temp_spell].is_line = SStruct.skill[isSpell].is_line;
                            PStruct.ptempspell[Target, temp_spell].is_heal = false;
                            PStruct.ptempspell[Target, temp_spell].fast_buff = false;
                            if (SStruct.skill[isSpell].slow)
                            {
                                PStruct.tempplayer[Target].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(1, Target);
                            }
                            NStruct.tempnpc[Map, index].preparingskill = 0;
                            NStruct.tempnpc[Map, index].preparingskillslot = 0;
                        }
                    }
                    if (SStruct.skill[isSpell].damage_type == 2)
                    {
                        if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                        {
                            NStruct.tempnpc[Map, index].preparingskill = 0;
                            NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            return;
                        }
                        //Tudo ok
                        SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                        NpcAttackPlayer(Map, index, Target, isSpell);
                        NStruct.tempnpc[Map, index].preparingskill = 0;
                        NStruct.tempnpc[Map, index].preparingskillslot = 0;
                    }
                    //Cura
                    if (SStruct.skill[isSpell].damage_type == 3)
                    {
                        if ((SStruct.skill[isSpell].type) == 1)
                        {
                            if (!(PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == Map))
                            {
                                NStruct.tempnpc[Map, index].preparingskill = 0;
                                NStruct.tempnpc[Map, index].preparingskillslot = 0;
                                return;
                            }

                            int temp_spell = PStruct.GetOpenPTempSpell(Target);
                            SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                            PStruct.ptempspell[Target, temp_spell].active = true;
                            PStruct.ptempspell[Target, temp_spell].attacker = index;
                            PStruct.ptempspell[Target, temp_spell].spellnum = isSpell;
                            PStruct.ptempspell[Target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[isSpell].interval;
                            PStruct.ptempspell[Target, temp_spell].repeats = SStruct.skill[isSpell].repeats;
                            PStruct.ptempspell[Target, temp_spell].anim = SStruct.skill[isSpell].second_anim;
                            PStruct.ptempspell[Target, temp_spell].area_range = SStruct.skill[isSpell].range_effect;
                            PStruct.ptempspell[Target, temp_spell].is_line = SStruct.skill[isSpell].is_line;
                            PStruct.ptempspell[Target, temp_spell].is_heal = true;
                            PStruct.ptempspell[Target, temp_spell].fast_buff = true;

                            PStruct.tempplayer[Target].movespeed = Globals.FastMoveSpeed;
                            SendData.Send_MoveSpeed(1, Target);

                            NStruct.tempnpc[Map, index].preparingskill = 0;
                            NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            if (Target != index)
                            {
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }
                            return;
                        }
                        if ((SStruct.skill[isSpell].type) == 0)
                        {


                            //Tudo ok
                            SendData.Send_Animation(Map, TargetType, Target, SStruct.skill[isSpell].animation_id);
                            NStruct.tempnpc[Map, index].preparingskill = 0;
                            NStruct.tempnpc[Map, index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                        }
                    }
                }
                //Não está! LOL
                else
                {
                    NStruct.tempnpc[Map, index].preparingskill = 0;
                    NStruct.tempnpc[Map, index].preparingskillslot = 0;
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, target, isSpell, Map, isPassive, skill_level) != null)
            {
                return;
            }

            //CÓDIGO
            if (target <= 0) { return; }

            int PlayerX = Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].X);
            int PlayerY = Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].Y);
            int NpcX = NStruct.tempnpc[map, id].X;
            int NpcY = NStruct.tempnpc[map, id].Y;

            SendData.Send_Animation(map, 1, target, NStruct.npc[map, id].Animation);

            int damage = 0;

            if (PStruct.tempplayer[target].Reflect)
            {
                SendData.Send_Animation(map, Globals.Target_Player, target, 155);
                SendData.Send_Animation(map, Globals.Target_Npc, id, 156);
                PStruct.PlayerAttackNpc(target, id);
                PStruct.tempplayer[target].Reflect = false;
                PStruct.tempplayer[target].ReflectTimer = 0;
                return;
            }

            //Magias
            if (isSpell > 0)
            {
                if (PStruct.character[target, PStruct.player[target].SelectedChar].ClassId == 6)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((PStruct.skill[target, i].num == 39) && (PStruct.skill[target, i].level > 0))
                        {
                            //Desviar do golpe?
                            int parry_test = Globals.Rand(0, 100);

                            if (parry_test <= (PStruct.GetPlayerParry(target) - PStruct.GetPlayerCritical(target)))
                            {
                                SendData.Send_ActionMsg(target, lang.attack_missed, Globals.ColorWhite, PStruct.character[target, PStruct.player[target].SelectedChar].X, PStruct.character[target, PStruct.player[target].SelectedChar].Y, 1, 0, map);
                                return;
                            }
                            break;
                        }
                    }
                }
                //Multiplicador de dano
                double multiplier = Convert.ToDouble(SStruct.skill[isSpell].scope) / 10;

                //Elemento mágico multiplicado
                double min_damage = (Convert.ToDouble(SStruct.skill[isSpell].damage_formula) / 4) * multiplier;
                double max_damage = (Convert.ToDouble(SStruct.skill[isSpell].damage_formula) / 2) * multiplier;

                //Dano total que pode ser causado
                double totaldamage = (Convert.ToDouble(SStruct.skill[isSpell].damage_formula) + max_damage) + NStruct.npc[map, id].MagicAttack;
                double totalmindamage = ((Convert.ToDouble(SStruct.skill[isSpell].damage_formula) / 100) * 20) + NStruct.npc[map, id].MagicAttack;

                //Passamos para int para tornar o dano exato
                int MinDamage = Convert.ToInt32(totalmindamage);
                int MaxDamage = Convert.ToInt32(totaldamage) + 1;

                if (MinDamage >= MaxDamage) { MaxDamage = MinDamage; }

                //Definição geral do dano
                damage = Globals.Rand(MinDamage, MaxDamage);
                damage -= (damage / 100) * NStruct.tempnpc[map, id].ReduceDamage;
                if (PStruct.character[target, PStruct.player[target].SelectedChar].ClassId == 3)
                {
                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        if ((PStruct.skill[target, i].num == 56) && (PStruct.skill[target, i].level > 0))
                        {
                            damage -= ((damage / 100) * (3 * PStruct.skill[target, i].level));
                        }
                    }
                }
                damage -= ((damage / 100) * PStruct.GetPlayerMagicDef(target));

                if (NStruct.tempnpc[map, id].ReduceDamage > 0)
                {
                    SendData.Send_ActionMsg(target, "Dano Reduzido", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    NStruct.tempnpc[map, id].ReduceDamage = 0;
                }

                if (damage < 1)
                {
                    SendData.Send_ActionMsg(target, "Resistiu", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                int drain = SStruct.skill[isSpell].drain;

                //Drenagem de vida?
                if (drain > 0)
                {
                    double real_drain = (Convert.ToDouble(damage) / 100) * drain;
                    NStruct.tempnpc[Map, id].Vitality += Convert.ToInt32(real_drain);
                    SendData.Send_ActionMsg(target, "+" + Convert.ToInt32(real_drain).ToString(), Globals.ColorGreen, NpcX, NpcY, 1, 0, map);
                }

                SendData.Send_ActionMsg(target, "-" + damage.ToString(), Globals.ColorPink, PlayerX, PlayerY, 1, NStruct.tempnpc[map, id].Dir, map);
            }
            //Ataques básicos
            else
            {
                if (NStruct.tempnpc[map, id].Blind)
                {
                    SendData.Send_ActionMsg(target, "Errou", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                //Desvia do golpe?
                int parry_test = Globals.Rand(0, 100);

                if (parry_test <= (PStruct.GetPlayerParry(target) - NStruct.GetNpcCritical(map, id)))
                {
                    SendData.Send_ActionMsg(target, "Errou", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    return;
                }

                //Variar 20% e diminuir pela defesa
                double Ddamage = Globals.Rand((NStruct.npc[map, id].Attack / 10) * 7, NStruct.npc[map, id].Attack);
                damage = Convert.ToInt32(Ddamage);
                damage -= (damage / 100) * NStruct.tempnpc[map, id].ReduceDamage;
                damage -= ((damage / 100) * PStruct.GetPlayerDefense(target));

                if (NStruct.tempnpc[map, id].ReduceDamage > 0)
                {
                    SendData.Send_ActionMsg(target, "Dano Reduzido", Globals.ColorWhite, PlayerX, PlayerY, 1, 0, map);
                    NStruct.tempnpc[map, id].ReduceDamage = 0;
                }

                //Se a defesa do jogador for muito alta, o dano é igual a 1
                if (damage < 1) { damage = 1; }

                //Dano crítico?
                bool is_critical = false;
                int critical_test = Globals.Rand(0, 100);

                if (critical_test <= NStruct.GetNpcCritical(map, id))
                {
                    damage = Convert.ToInt32((Convert.ToDouble(damage) * 2));
                    is_critical = true;
                }


                if (NStruct.npc[map, id].KnockAttack)
                {
                    int true_range = 0;
                    for (int i = 1; i <= NStruct.npc[map, id].KnockRange; i++)
                    {
                        if (PStruct.CanThrowPlayer(target, NStruct.tempnpc[map, id].Dir, i))
                        {
                            true_range += 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (true_range < NStruct.npc[map, id].KnockRange)
                    {
                        damage += NStruct.npc[map, id].KnockRange - true_range;
                    }

                    if (true_range > 0)
                    {
                        PStruct.ThrowPlayer(target, NStruct.tempnpc[map, id].Dir, true_range);
                    }

                    if (PStruct.tempplayer[target].preparingskill > 0)
                    {
                        PStruct.tempplayer[target].preparingskill = 0;
                        PStruct.tempplayer[target].preparingskillslot = 0;
                        PStruct.tempplayer[target].skilltimer = 0;
                        SendData.Send_ActionMsg(target, "Conjuração quebrada!", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                        PStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                        SendData.Send_MoveSpeed(Globals.Target_Player, target);
                        SendData.Send_BrokeSkill(target);
                    }

                }


                //Dano e animação
                if (is_critical)
                {
                    int true_range = 0;
                    for (int i = 1; i <= 2; i++)
                    {
                        if (PStruct.CanThrowPlayer(target, NStruct.tempnpc[map, id].Dir, i))
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
                        PStruct.ThrowPlayer(target, NStruct.tempnpc[map, id].Dir, true_range);
                    }

                    if (!NStruct.npc[map, id].KnockAttack)
                    {
                        if (PStruct.tempplayer[target].preparingskill > 0)
                        {
                            PStruct.tempplayer[target].preparingskill = 0;
                            PStruct.tempplayer[target].preparingskillslot = 0;
                            PStruct.tempplayer[target].skilltimer = 0;
                            SendData.Send_ActionMsg(target, "Conjuração quebrada!", Globals.ColorPink, PlayerX, PlayerY, 1, 0, map);
                            PStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(Globals.Target_Player, target);
                            SendData.Send_BrokeSkill(target);
                        }
                    }

                    SendData.Send_ActionMsg(target, "-" + damage.ToString(), 1, PlayerX, PlayerY, 1, NStruct.tempnpc[map, id].Dir, map);
                }
                else
                {
                    SendData.Send_ActionMsg(target, "-" + damage.ToString(), 4, PlayerX, PlayerY, 1, NStruct.tempnpc[map, id].Dir, map);
                }
            }

            //Nova vida
            PStruct.tempplayer[target].Vitality = PStruct.tempplayer[target].Vitality - damage;

            //Atualiza vida do jogador para quem está no mapa vendo ele
            SendData.Send_PlayerVitalityToMap(map, target, PStruct.tempplayer[target].Vitality);

            //Atualiza vida do jogador para quem está em seu grupo
            if (PStruct.tempplayer[target].Party > 0)
            {
                SendData.Send_PlayerVitalityToParty(PStruct.tempplayer[target].Party, target, PStruct.tempplayer[target].Vitality);
            }

            if (PStruct.tempplayer[target].Vitality <= 0)
            {
                PStruct.tempplayer[target].PetTarget = 0;
                PStruct.tempplayer[target].PetTargetType = 0;
                //jogador morre
                for (int i = 1; i < Globals.MaxMapNpcs; i++)
                {
                    if (NStruct.tempnpc[map, i].Target == target)
                    {
                        NStruct.tempnpc[map, i].Target = 0;
                    }
                }
                PStruct.tempplayer[target].isDead = true;
                SendData.Send_PlayerDeathToMap(target);
            }
        }
        //*********************************************************************************************
        // NpcMove
        // Simplesmente executa o movimento de determinado NPC
        //*********************************************************************************************
        public static void NpcMove(int map, int id, byte dir)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir) != null)
            {
                return;
            }

            //CÓDIGO
            //Definição da nova posição
            if (dir == Globals.DirUp) { NStruct.tempnpc[map, id].Y -= 1; }
            if (dir == Globals.DirDown) { NStruct.tempnpc[map, id].Y += 1; }
            if (dir == Globals.DirLeft) { NStruct.tempnpc[map, id].X -= 1; }
            if (dir == Globals.DirRight) { NStruct.tempnpc[map, id].X += 1; }
            //Definir dados para estudo do próximo movimento
            DefinePrevMove(map, id);
            //Enviar movimento para o mapa
            SendData.Send_NpcMove(map, id);
        }
        //*********************************************************************************************
        // ClearPrevMove
        // Ponto importante da inteligência artificial baseada em "errando e aprendendo".
        //*********************************************************************************************
        public static void ClearPrevMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            //Limpar dados de estudo de movimento
            for (int i = 1; i <= 6; i++)
            {
                NStruct.tempnpc[map, id].prev_move[i].x = 0;
                NStruct.tempnpc[map, id].prev_move[i].y = 0;
            }
        }
        //*********************************************************************************************
        // DefinePrevMove
        // Inteligência no movimento
        //*********************************************************************************************
        public static void DefinePrevMove(int map, int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            //Se o npc não tem alvo, no prob.
            if (NStruct.tempnpc[map, id].Target <= 0)
            {
                if ((NStruct.tempnpc[map, id].prev_move[6].x > 0) || (NStruct.tempnpc[map, id].prev_move[6].y > 0))
                {
                    ClearPrevMove(map, id);
                }
                return;
            }

            //Re organizar todos os dados
            for (int i = 1; i <= 5; i++)
            {
                NStruct.tempnpc[map, id].prev_move[i].x = NStruct.tempnpc[map, id].prev_move[i + 1].x;
                NStruct.tempnpc[map, id].prev_move[i].y = NStruct.tempnpc[map, id].prev_move[i + 1].y;
            }

            //O dado adicionado equivale ao 6.
            NStruct.tempnpc[map, id].prev_move[6].x = NStruct.tempnpc[map, id].X;
            NStruct.tempnpc[map, id].prev_move[6].y = NStruct.tempnpc[map, id].Y;
        }
        //*********************************************************************************************
        // CanNpcMove
        // Determina se o NPC pode se mover
        //*********************************************************************************************
        public static bool CanNpcMove(int map, int id, byte dir, int x = 0, int y = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir, x, y) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, dir, x, y));
            }

            //CÓDIGO
            int target = NStruct.tempnpc[map, id].Target;
            if (x == 0) { x = NStruct.tempnpc[map, id].X; }
            if (y == 0) { y = NStruct.tempnpc[map, id].Y; }
            long attacktimer = NStruct.tempnpc[map, id].AttackTimer;

            if (NStruct.tempnpc[map, id].Dir != dir)
            {
                NStruct.tempnpc[map, id].Dir = dir;
                //SendNpcChangeDir(mapID, id, dir)
            }

            if ((dir == Globals.DirUp) && (Convert.ToBoolean(MStruct.tile[map, x, y].UpBlock) == false)) { return false; }
            if ((dir == Globals.DirDown) && (Convert.ToBoolean(MStruct.tile[map, x, y].DownBlock) == false)) { return false; }
            if ((dir == Globals.DirLeft) && (Convert.ToBoolean(MStruct.tile[map, x, y].LeftBlock) == false)) { return false; }
            if ((dir == Globals.DirRight) && (Convert.ToBoolean(MStruct.tile[map, x, y].RightBlock) == false)) { return false; }

            if (dir == Globals.DirUp) { y -= 1; }
            if (dir == Globals.DirDown) { y += 1; }
            if (dir == Globals.DirLeft) { x -= 1; }
            if (dir == Globals.DirRight) { x += 1; }

            //Verificar se é preciso adicionar algo
            for (int i = 1; i <= 6; i++)
            {
                if ((NStruct.tempnpc[map, id].prev_move[i].x == x) && (NStruct.tempnpc[map, id].prev_move[i].y == y))
                {
                    DefinePrevMove(map, id);
                    return false;
                }
            }

            if ((x < 0) || (x > Convert.ToInt32(MStruct.map[map].max_width))) { return false; }
            if ((y < 0) || (y > Convert.ToInt32(MStruct.map[map].max_height))) { return false; }

            if ((dir == Globals.DirUp) && (Convert.ToBoolean(MStruct.tile[map, x, y].DownBlock) == false)) { return false; }
            if ((dir == Globals.DirDown) && (Convert.ToBoolean(MStruct.tile[map, x, y].UpBlock) == false)) { return false; }
            if ((dir == Globals.DirLeft) && (Convert.ToBoolean(MStruct.tile[map, x, y].RightBlock) == false)) { return false; }
            if ((dir == Globals.DirRight) && (Convert.ToBoolean(MStruct.tile[map, x, y].LeftBlock) == false)) { return false; }

            if (MStruct.tile[map, x, y].Data1 == "3") { return false; }
            if (MStruct.tile[map, x, y].Data1 == "10") { return false; }
            if (MStruct.tile[map, x, y].Data1 == "17") { return false; }
            if (MStruct.tile[map, x, y].Data1 == "18") { return false; }
            if (MStruct.tile[map, x, y].Data1 == "2") { return false; }

            //if (Convert.ToByte(MStruct.tile[map, x, y].Passable) == Globals.NoPassable)  { return false; }
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if (PStruct.tempplayer[i].ingame == true)
                {
                    if (Convert.ToInt32(PStruct.character[i, PStruct.player[i].SelectedChar].Map) == map)
                    {
                        if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == x) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == y)) 
                        {
                            if (NStruct.tempnpc[map, id].Target > 0)
                            {
                                NStruct.tempnpc[map, id].Target = i;
                            }
                            return false; 
                        }
                    }
                }
            }

            for (int i = 0; i <= MStruct.tempmap[map].NpcCount; i++)
            {
                if (i != id)
                {
                    if ((NStruct.tempnpc[map, i].X == x) && (NStruct.tempnpc[map, i].Y == y) && (!NStruct.tempnpc[map, i].Dead)) { return false; }
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            int target = NStruct.tempnpc[map, id].Target;
            int x = NStruct.tempnpc[map, id].X;
            int y = NStruct.tempnpc[map, id].Y;
            int dir = NStruct.tempnpc[map, id].Dir;
            long attacktimer = NStruct.tempnpc[map, id].AttackTimer;

            if (dir == Globals.DirUp) { y -= 1; }
            if (dir == Globals.DirDown) { y += 1; }
            if (dir == Globals.DirLeft) { x -= 1; }
            if (dir == Globals.DirRight) { x += 1; }

            if (Loops.TickCount.ElapsedMilliseconds <= attacktimer) { return false; }
            if (Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].Map) != map) { return false; }

            if ((Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].X) == x) && (Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].Y) == y))
            {

                //Tempo de ataque
                NStruct.tempnpc[map, id].AttackTimer = Loops.TickCount.ElapsedMilliseconds + 1000;
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Npc_Spells; i++)
            {
                if ((NStruct.nspell[map, id, i].spellnum > 0) && (Loops.TickCount.ElapsedMilliseconds > NStruct.nspell[map, id, i].cooldown))
                {
                    return i;
                }
            }
            return 0;
        }
        //*********************************************************************************************
        // GetNpcSpellTarget
        // Retorna o algo de determinado cast do NPC
        //*********************************************************************************************
        public static int GetNpcSpellTarget(int map, int id, int isSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, isSpell) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, isSpell));
            }

            //CÓDIGO
            int NpcX = NStruct.tempnpc[map, id].X;
            int NpcY = NStruct.tempnpc[map, id].Y;
            int PlayerX = 0;
            int PlayerY = 0;
            int DistanceX = 0;
            int DistanceY = 0;
            int Target = NStruct.tempnpc[map, id].Target;


            //Calcular distância
            int n = SStruct.skill[isSpell].tp_gain;
            if (PStruct.character[Target, PStruct.player[Target].SelectedChar].Map == map)
            {
                PlayerX = PStruct.character[Target, PStruct.player[Target].SelectedChar].X;
                PlayerY = PStruct.character[Target, PStruct.player[Target].SelectedChar].X;
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id, sx, sy) != null)
            {
                return;
            }

            //CÓDIGO
            int distX = NStruct.tempnpc[map, id].X - sx;
            int distY = NStruct.tempnpc[map, id].Y - sy;

            if (distX < 0) { distX *= -1; }
            if (distY < 0) { distY *= -1; }

            int temp_dist_x = NStruct.tempnpc[map, id].X - sx;
            int temp_dist_y = NStruct.tempnpc[map, id].Y - sy;
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
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, map, id) != null)
            {
                return;
            }

            //CÓDIGO
            int ivy = 0;
            int move = 0;
            int target = NStruct.tempnpc[map, id].Target;
            int x = NStruct.tempnpc[map, id].X;
            int y = NStruct.tempnpc[map, id].Y;
            int sx = 0;
            int sy = 0;
            int dir = NStruct.tempnpc[map, id].Dir;

            if (NStruct.tempnpc[map, id].Dead == true) { return; }
            if (NStruct.tempnpc[map, id].Vitality <= 0) { return; }
            if (NStruct.tempnpc[map, id].StunTimer > Loops.TickCount.ElapsedMilliseconds) { return; }
            if (NStruct.tempnpc[map, id].SleepTimer > Loops.TickCount.ElapsedMilliseconds) { return; }

            long curTimer = NStruct.tempnpc[map, id].curTimer;

            if (target == 0)
            {
                for (int i = 0; i <= Globals.Player_Highindex; i++)
                {
                    if ((!PStruct.tempplayer[i].isDead) && (PStruct.character[i, PStruct.player[i].SelectedChar].Map == map))
                    {
                        int n = NStruct.npc[map, id].Range;
                        int DistanceX = NStruct.tempnpc[map, id].X - PStruct.character[i, PStruct.player[i].SelectedChar].X;
                        int DistanceY = NStruct.tempnpc[map, id].Y - PStruct.character[i, PStruct.player[i].SelectedChar].Y;

                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }
                        if ((DistanceX <= n) && (DistanceY <= n))
                        {
                            if (NStruct.npc[map, id].Type == 2)
                            {
                                NStruct.tempnpc[map, id].Target = i; //foca o jogador, talvez eu faça npc depois?
                            }
                        }
                    }
                }
            }


            if (Loops.TickCount.ElapsedMilliseconds < NStruct.tempnpc[map, id].MoveTimer)
            {
                return;
            }
            else
            {
                NStruct.tempnpc[map, id].MoveTimer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt64((((8 + (4 - NStruct.tempnpc[map, id].movespeed) - NStruct.tempnpc[map, id].movespeed) * 64))); //25ms de tolerância;
            }

            if (target == 0)
            {
                if (Globals.Rand(1, 2) == 1)
                {
                    return;
                }

                if (Loops.TickCount.ElapsedMilliseconds > curTimer)
                {
                    sx = NStruct.tempnpc[map, id].curTargetX;
                    sy = NStruct.tempnpc[map, id].curTargetY;
                }
                else
                {
                    // return; idiota
                    sx = NStruct.tempnpc[map, id].curTargetX;
                    sy = NStruct.tempnpc[map, id].curTargetY;
                }

            }
            else
            {
                if ((UserConnection.Getindex(target) >= 0) && (WinsockAsync.Clients[(UserConnection.Getindex(target))].IsConnected == true))
                {
                    //Nosso alvo.
                    sx = PStruct.character[target, PStruct.player[target].SelectedChar].X; //Player(.Target).X
                    sy = PStruct.character[target, PStruct.player[target].SelectedChar].Y;
                    
                    //Estudo sobre movimentos - Variáveis
                    int distX = NStruct.tempnpc[map, id].X - sx;
                    int distY = NStruct.tempnpc[map, id].Y - sy;

                    //Garantir que seja positivo
                    if (distX < 0) { distX *= -1; }
                    if (distY < 0) { distY *= -1; }

                    //Se a distância for de um tile, apagar estudos anteriores pois chegamos no alvo.
                    if ((distX <= 1) && (distY <= 0))
                    {
                        ClearPrevMove(map, id);
                    }

                    //Se a distância for de um tile, apagar estudos anteriores pois chegamos no alvo.
                    if ((distX <= 0) && (distY <= 1))
                    {
                        ClearPrevMove(map, id);
                    }
                }
                else
                {
                    NStruct.tempnpc[map, id].Target = 0;
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

            if ((Loops.TickCount.ElapsedMilliseconds > NStruct.tempnpc[map, id].curTimer) && (target == 0)) { move = 0; }

            if (target > 0)
            {
                if (PStruct.character[target, PStruct.player[target].SelectedChar].Map != map)
                {
                    NStruct.tempnpc[map, id].Target = 0;
                    return;
                }

                int spellnum = NpcHaveSpell(map, id);

                if (spellnum > 0)
                {
                    int spelltarget = GetNpcSpellTarget(map, id, spellnum);

                    if (spelltarget > 0)
                    {
                        NStruct.tempnpc[map, id].preparingskillslot = spellnum;
                        NStruct.tempnpc[map, id].preparingskill = NStruct.nspell[map, id, spellnum].spellnum;
                        NStruct.tempnpc[map, id].skilltimer = Loops.TickCount.ElapsedMilliseconds + (SStruct.skill[spellnum].speed * 100);
                        NStruct.nspell[map, id, spellnum].cooldown = Loops.TickCount.ElapsedMilliseconds + (SStruct.skill[spellnum].tp_cost * 1000);
                        SendData.Send_Animation(map, 2, id, 81);
                    }
                }
            }
            if (move == 0)
            {
                if (target > 0)
                {
                    if (x < Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].X)) { ivy = Globals.DirRight; }
                    if (x > Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].X)) { ivy = Globals.DirLeft; }
                    if (y < Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].Y)) { ivy = Globals.DirDown; }
                    if (y > Convert.ToInt32(PStruct.character[target, PStruct.player[target].SelectedChar].Y)) { ivy = Globals.DirUp; }

                    if (ivy != dir)
                    {
                        dir = ivy;
                        NStruct.tempnpc[map, id].Dir = Convert.ToByte(dir);
                        SendData.Send_NpcDir(map, id, ivy);
                    }
                }

                if (NStruct.tempnpc[map, id].preparingskill == 0)
                {
                    if (NpcIA.CanNpcAttack(map, id)) { NpcAttackPlayer(map, id, target); }
                }

                NStruct.tempnpc[map, id].Moving = 0;
                if (Loops.TickCount.ElapsedMilliseconds > NStruct.tempnpc[map, id].curTimer)
                {
                    //wow:
                    NStruct.tempnpc[map, id].curTargetX = Globals.Rand(NStruct.tempnpc[map, id].X - 5, NStruct.tempnpc[map, id].X + 5);
                    if (NStruct.tempnpc[map, id].curTargetX < 0) { NStruct.tempnpc[map, id].curTargetX = NStruct.tempnpc[map, id].curTargetX * -1; }
                    if (NStruct.tempnpc[map, id].curTargetX > Convert.ToInt32(MStruct.map[map].max_width)) { NStruct.tempnpc[map, id].curTargetX -= 6; }
                    //wew:
                    NStruct.tempnpc[map, id].curTargetY = Globals.Rand(NStruct.tempnpc[map, id].Y - 5, NStruct.tempnpc[map, id].Y + 5);
                    if (NStruct.tempnpc[map, id].curTargetY < 0) { NStruct.tempnpc[map, id].curTargetY = NStruct.tempnpc[map, id].curTargetY * -1; }
                    if (NStruct.tempnpc[map, id].curTargetY > Convert.ToInt32(MStruct.map[map].max_height)) { NStruct.tempnpc[map, id].curTargetY -= 6; }
                    NStruct.tempnpc[map, id].curTimer = Loops.TickCount.ElapsedMilliseconds + 5000; // 5 Segundos hibernando
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
                NStruct.tempnpc[map, id].curTarget = NStruct.tempnpc[map, id].Current;
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
                NStruct.tempnpc[map, id].curTarget = NStruct.tempnpc[map, id].Current;
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
                NStruct.tempnpc[map, id].curTarget = NStruct.tempnpc[map, id].Current;
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
                NStruct.tempnpc[map, id].curTarget = NStruct.tempnpc[map, id].Current;
            }
        }
    }
}
