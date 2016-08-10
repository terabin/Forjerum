using System;
using System.Linq;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Responsável por métodos lógicos baseados nos jogadores.
    // PlayerLogic.cs
    //*********************************************************************************************
    class PlayerLogic : Languages.LStruct
    {
        //*********************************************************************************************
        // isPlayerConnected
        // Retorna se o jogador está conectado, mas não usa nenhuma
        // referência real da conexão.
        //*********************************************************************************************
        public static bool isPlayerConnected(string email)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, email) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, email));
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highindex; i++)
            {
                if (PStruct.player[i].Email == email)
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // HealPlayer
        //*********************************************************************************************
        public static void HealPlayer(int index, int damage)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, damage) != null)
            {
                return;
            }

            //CÓDIGO
            PStruct.tempplayer[index].Vitality += damage;
            if (PStruct.tempplayer[index].Vitality > PStruct.GetPlayerMaxVitality(index))
            {
                PStruct.tempplayer[index].Vitality = PStruct.GetPlayerMaxVitality(index);
            }

            SendData.Send_ActionMsg(index, "+" + damage, Globals.ColorGreen, PStruct.character[index, PStruct.player[index].SelectedChar].X, PStruct.character[index, PStruct.player[index].SelectedChar].Y, 1, PStruct.character[index, PStruct.player[index].SelectedChar].Dir, PStruct.character[index, PStruct.player[index].SelectedChar].Map);
            SendData.Send_PlayerVitalityToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, index, PStruct.tempplayer[index].Vitality);
            if (PStruct.tempplayer[index].Party > 0)
            {
                SendData.Send_PlayerVitalityToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Vitality);
            }
        }
        //*********************************************************************************************
        // SpiritPlayer
        //*********************************************************************************************
        public static void SpiritPlayer(int index, int damage)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, damage) != null)
            {
                return;
            }

            //CÓDIGO
            PStruct.tempplayer[index].Spirit += damage;
            if (PStruct.tempplayer[index].Spirit > PStruct.GetPlayerMaxSpirit(index))
            {
                PStruct.tempplayer[index].Spirit = PStruct.GetPlayerMaxSpirit(index);
            }

            SendData.Send_ActionMsg(index, "+" + damage, Globals.ColorBlue, PStruct.character[index, PStruct.player[index].SelectedChar].X, PStruct.character[index, PStruct.player[index].SelectedChar].Y, 1, PStruct.character[index, PStruct.player[index].SelectedChar].Dir, PStruct.character[index, PStruct.player[index].SelectedChar].Map);
            SendData.Send_PlayerSpiritToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, index, PStruct.tempplayer[index].Spirit);
            if (PStruct.tempplayer[index].Party > 0)
            {
                SendData.Send_PlayerSpiritToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Spirit);
            }
        }
        //*********************************************************************************************
        // HealSpellDamage
        // Retorna a quantidade de dano da cura (não dano, mas ok)
        //*********************************************************************************************
        public static int HealSpellDamage(int index, int isSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, isSpell) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index, isSpell));
            }

            //CÓDIGO
            //Multiplicador de dano
            double multiplier = Convert.ToDouble(SStruct.skill[isSpell].scope) / 10;

            //Elemento mágico multiplicado
            double min_damage = PStruct.GetPlayerMinMagic(index) * multiplier;
            double max_damage = PStruct.GetPlayerMaxMagic(index) * multiplier;

            //Multiplicador de nível
            double levelmultiplier = (1.0 + multiplier) * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level; //Except

            //Verificando se a skill teve algum problema e corrigindo
            if (levelmultiplier < 1.0) { levelmultiplier = 1.0; }

            //Dano total que pode ser causado
            double totaldamage = (Convert.ToDouble(SStruct.skill[isSpell].damage_formula) + max_damage) * levelmultiplier;
            double totalmindamage = (Convert.ToDouble(SStruct.skill[isSpell].damage_formula) + min_damage) * levelmultiplier;

            //Passamos para int para tornar o dano exato
            int MinDamage = Convert.ToInt32(totalmindamage);
            int MaxDamage = Convert.ToInt32(totaldamage) + 1;

            if (MinDamage >= MaxDamage)
            {
                MaxDamage = MinDamage;
            }

            //Definição geral do dano
            int Damage = Globals.Rand(MinDamage, MaxDamage);
            return Damage;
        }
        //*********************************************************************************************
        // ExecuteSelfSkill
        // Executa magias que são iniciadas no próprio jogador
        //*********************************************************************************************
        public static bool ExecuteSelfSkill(int index)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, index));
            }

            //CÓDIGO
            //Váriaveis de análise
            int Map = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int isSpell = PStruct.tempplayer[index].preparingskill;
            int target = PStruct.tempplayer[index].target;
            int targettype = PStruct.tempplayer[index].targettype;
            int playerx = PStruct.character[index, PStruct.player[index].SelectedChar].X;
            int playery = PStruct.character[index, PStruct.player[index].SelectedChar].Y;
            int preparingskill = PStruct.tempplayer[index].preparingskill;
            int preparingskillslot = PStruct.tempplayer[index].preparingskillslot;
            int tp_cost = SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost;
            byte playerdir = PStruct.character[index, PStruct.player[index].SelectedChar].Dir;

            //Magias que devem ser executadas apenas no jogador
            if ((SStruct.skill[preparingskill].type) == 14)
            {
                //Cooldown
                PStruct.skill[index, PStruct.hotkey[index, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                SendData.Send_PlayerSkillCooldown(index, preparingskillslot, tp_cost * 500);
                //Atualiza MP
                PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                SendData.Send_PlayerSpiritToMap(Map, index, PStruct.tempplayer[index].Spirit);

                int true_range = 0;
                int old_x = PStruct.character[index, PStruct.player[index].SelectedChar].X;
                int old_y = PStruct.character[index, PStruct.player[index].SelectedChar].Y;

                for (int i = 1; i <= 3; i++)
                {
                    if (PStruct.CanThrowPlayer(index, playerdir, i))
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
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i <= Globals.Player_Highindex; i++)
                    {
                        if ((PStruct.tempplayer[i].ingame == true) && (i != index))
                        {
                            if (Convert.ToInt32(PStruct.character[i, PStruct.player[i].SelectedChar].Map) == Map)
                            {
                                for (int p = 0; p <= true_range; p++)
                                {
                                    x = old_x;
                                    y = old_y;

                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == x) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == y)) { SendData.Send_Animation(Map, Globals.Target_Player, i, 149); PStruct.PlayerAttackPlayer(index, i, preparingskill); }
                                }
                            }

                        }
                    }

                    for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                    {
                        for (int p = 0; p <= true_range; p++)
                        {
                            x = old_x;
                            y = old_y;

                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirDown) { y += p; }
                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirRight) { x += p; }
                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                            if ((NStruct.tempnpc[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].X == x) && (NStruct.tempnpc[PStruct.character[index, PStruct.player[index].SelectedChar].Map, i].Y == y)) { SendData.Send_Animation(Map, Globals.Target_Npc, i, 149); PStruct.PlayerAttackNpc(index, i, preparingskill); }
                        }
                    }

                    PStruct.ThrowPlayer(index, PStruct.character[index, PStruct.player[index].SelectedChar].Dir, true_range);
                }

                //Tudo ok
                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, Globals.Target_Player, index, SStruct.skill[preparingskill].animation_id);

                //Paralizar inimigo
                PStruct.tempplayer[index].Slowed = true;
                PStruct.tempplayer[index].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 300;
                PStruct.tempplayer[index].movespeed -= 0.5;
                SendData.Send_MoveSpeed(Globals.Target_Player, index);

                //Resetar valores comuns
                PStruct.tempplayer[index].preparingskill = 0;
                PStruct.tempplayer[index].preparingskillslot = 0;
                return true;
            }
            if ((SStruct.skill[PStruct.tempplayer[index].preparingskill].type) == 16)
            {
                //Cooldown
                PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500);
                SendData.Send_PlayerSkillCooldown(index, PStruct.tempplayer[index].preparingskillslot, (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500));
                //Atualiza MP
                PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                SendData.Send_PlayerSpiritToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, index, PStruct.tempplayer[index].Spirit);

                if (PStruct.tempplayer[index].Party > 0) { SendData.Send_PlayerSpiritToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Spirit); }

                //Tudo ok
                //Enviar animação
                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, Globals.Target_Player, index, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                //Refletir dano
                PStruct.tempplayer[index].Reflect = true;
                PStruct.tempplayer[index].ReflectTimer = Loops.TickCount.ElapsedMilliseconds + 2000;

                //Resetar valores comuns
                PStruct.tempplayer[index].preparingskill = 0;
                PStruct.tempplayer[index].preparingskillslot = 0;
                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                SendData.Send_MoveSpeed(1, index);
                return true;
            }
            if (PStruct.tempplayer[index].preparingskill == 34)
            {
                if ((SStruct.skill[PStruct.tempplayer[index].preparingskill].type) == 0)
                {
                    int Damage = HealSpellDamage(index, PStruct.tempplayer[index].preparingskill);
                    //Cooldown
                    PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500);
                    SendData.Send_PlayerSkillCooldown(index, PStruct.tempplayer[index].preparingskillslot, (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500));
                    //Atualiza MP
                    PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                    SendData.Send_PlayerSpiritToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, index, PStruct.tempplayer[index].Spirit);

                    if (PStruct.tempplayer[index].Party > 0) { SendData.Send_PlayerSpiritToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Spirit); }
                    //Tudo ok
                    SendData.Send_Animation(Map, Globals.Target_Player, index, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                    HealPlayer(PStruct.tempplayer[index].target, Damage);
                    PStruct.tempplayer[index].preparingskill = 0;
                    PStruct.tempplayer[index].preparingskillslot = 0;
                    PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                    SendData.Send_MoveSpeed(1, index);
                    return true;
                }
            }
            //Cura
            if (SStruct.skill[preparingskill].damage_type == 3)
            {
                if ((SStruct.skill[preparingskill].type) == 1)
                {
                    //Cooldown
                    PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500);
                    SendData.Send_PlayerSkillCooldown(index, PStruct.tempplayer[index].preparingskillslot, (SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost * 500));
                    //Atualiza MP
                    PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                    SendData.Send_PlayerSpiritToMap(PStruct.character[index, PStruct.player[index].SelectedChar].Map, index, PStruct.tempplayer[index].Spirit);
                    int temp_spell = PStruct.GetOpenPTempSpell(index);
                    SendData.Send_Animation(Map, index, index, SStruct.skill[preparingskill].animation_id);
                    PStruct.ptempspell[index, temp_spell].active = true;
                    PStruct.ptempspell[index, temp_spell].attacker = index;
                    PStruct.ptempspell[index, temp_spell].spellnum = PStruct.tempplayer[index].preparingskill;
                    PStruct.ptempspell[index, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[PStruct.tempplayer[index].preparingskill].interval;
                    PStruct.ptempspell[index, temp_spell].repeats = SStruct.skill[PStruct.tempplayer[index].preparingskill].repeats;
                    PStruct.ptempspell[index, temp_spell].anim = SStruct.skill[PStruct.tempplayer[index].preparingskill].second_anim;
                    PStruct.ptempspell[index, temp_spell].area_range = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                    PStruct.ptempspell[index, temp_spell].is_line = SStruct.skill[PStruct.tempplayer[index].preparingskill].is_line;
                    PStruct.ptempspell[index, temp_spell].is_heal = true;
                    PStruct.ptempspell[index, temp_spell].fast_buff = true;

                    PStruct.tempplayer[index].movespeed = Globals.FastMoveSpeed;
                    SendData.Send_MoveSpeed(1, index);

                    PStruct.tempplayer[index].preparingskill = 0;
                    PStruct.tempplayer[index].preparingskillslot = 0;
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // ExecuteSkill
        // Executa magias fora do self na maior parte(?) Verifica vários aspectos como distância, mapa
        // e o contexto para que não haja erro de posicionamento.
        //*********************************************************************************************
        public static void ExecuteSkill(int index)
        {
            int Map = PStruct.character[index, PStruct.player[index].SelectedChar].Map;
            int isSpell = PStruct.tempplayer[index].preparingskill;
            int target = PStruct.tempplayer[index].target;
            int targettype = PStruct.tempplayer[index].targettype;
            int playerx = PStruct.character[index, PStruct.player[index].SelectedChar].X;
            int playery = PStruct.character[index, PStruct.player[index].SelectedChar].Y;
            int preparingskill = PStruct.tempplayer[index].preparingskill; 
            int preparingskillslot = PStruct.tempplayer[index].preparingskillslot;
            int tp_cost = SStruct.skill[PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].num].tp_cost;
            int DistanceX = 0;
            int DistanceY = 0;
            int n = 0;
            byte playerdir = PStruct.character[index, PStruct.player[index].SelectedChar].Dir;

            if (PStruct.tempplayer[index].Spirit < SStruct.skill[PStruct.tempplayer[index].preparingskill].mp_cost) { return; }
            if (ExecuteSelfSkill(index)) { return; }

            //O alvo é um jogador
            if (PStruct.tempplayer[index].targettype == 1)
            {
                int clientid = UserConnection.Getindex(PStruct.tempplayer[index].target);

                //Verificar se não é inválido para análise da conexão
                if ((clientid < 0) || (clientid >= WinsockAsync.Clients.Count()))
                {
                    PStruct.tempplayer[index].preparingskill = 0;
                    PStruct.tempplayer[index].preparingskillslot = 0;
                    PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                    return;
                }

                //Verificar se o jogador não se desconectou no processo ou se não há falhas nas variáveis
                if ((!WinsockAsync.Clients[clientid].IsConnected) || (PStruct.tempplayer[index].target > Globals.Player_Highindex) || (PStruct.tempplayer[index].target < 0) || (!PStruct.tempplayer[target].ingame))
                {
                    PStruct.tempplayer[index].preparingskill = 0;
                    PStruct.tempplayer[index].preparingskillslot = 0;
                    PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;   
                    return;
                }

                //Posição do alvo
                int targetx = PStruct.character[target, PStruct.player[target].SelectedChar].X;
                int targety = PStruct.character[target, PStruct.player[target].SelectedChar].Y;

                //Calcular distância
                n = SStruct.skill[PStruct.tempplayer[index].preparingskill].tp_gain;
                DistanceX = playerx - targetx;
                DistanceY = playery - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    if (((SStruct.skill[preparingskill].type) == 0) && (PStruct.character[target, PStruct.player[target].SelectedChar].Map == Map))
                    {
                        int Damage = HealSpellDamage(index, PStruct.tempplayer[index].preparingskill);
                        //Cooldown
                        PStruct.skill[index, PStruct.hotkey[index, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                        SendData.Send_PlayerSkillCooldown(index, preparingskillslot, tp_cost * 500);
                        //Atualiza MP
                        PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                        SendData.Send_PlayerSpiritToMap(Map, index, PStruct.tempplayer[index].Spirit);
                        //Tudo ok
                        SendData.Send_Animation(Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                        HealPlayer(PStruct.tempplayer[index].target, Damage);
                        PStruct.tempplayer[index].preparingskill = 0;
                        PStruct.tempplayer[index].preparingskillslot = 0;
                        PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                        SendData.Send_MoveSpeed(1, index);
                        return;
                    }

                    //Check in
                    if (!(PStruct.character[target, PStruct.player[target].SelectedChar].Map == Map) || !(target != index))
                    {
                        PStruct.tempplayer[index].preparingskill = 0;
                        PStruct.tempplayer[index].preparingskillslot = 0;
                        PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                        SendData.Send_MoveSpeed(1, index);
                        SendData.Send_MsgToPlayer(index, lang.could_not_perform_magic, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }

                    //Check in
                    if (!MStruct.tempmap[Map].WarActive)
                    {
                        if (!PStruct.character[index, PStruct.player[index].SelectedChar].PVP)
                        {
                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(1, index);
                            return;
                        }
                    }


                    //Cooldown
                    PStruct.skill[index, PStruct.hotkey[index, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                    SendData.Send_PlayerSkillCooldown(index, preparingskillslot, tp_cost * 500);
                    //Atualiza MP
                    PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                    SendData.Send_PlayerSpiritToMap(Map, index, PStruct.tempplayer[index].Spirit);
                    if (PStruct.tempplayer[index].Party > 0) { SendData.Send_PlayerSpiritToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Spirit); }

                    //Execução da magia de dano
                    if (SStruct.skill[PStruct.tempplayer[index].preparingskill].damage_type == 1)
                    {
                        //A skill é de repetição?
                        if (SStruct.skill[preparingskill].repeats <= 1)
                        {
                            if ((SStruct.skill[PStruct.tempplayer[index].preparingskill].type) == 0)
                            {
                                //Envio da animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Habilidade passiva do Iarumas
                                if (PStruct.character[index, PStruct.player[index].SelectedChar].ClassId == 4)
                                {
                                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                                    {
                                        if ((PStruct.skill[index, i].num == 47) && (PStruct.skill[index, i].level > 0))
                                        {
                                            if (PStruct.tempplayer[index].Sheathe < 3)
                                            {
                                                PStruct.tempplayer[index].Sheathe += 1;
                                            }
                                            else
                                            {
                                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[47].animation_id);
                                                PStruct.PlayerAttackPlayer(index, target, 47);
                                            }
                                            break;
                                        }
                                    }
                                }

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 6)
                            {
                                //Tudo ok

                                //Envio da animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                int spellbuff = PStruct.GetOpenSpellBuff(index);

                                PStruct.pspellbuff[index, spellbuff].active = true;
                                PStruct.pspellbuff[index, spellbuff].type = 1; //DAMAGE
                                PStruct.pspellbuff[index, spellbuff].value = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                                PStruct.pspellbuff[index, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SStruct.skill[PStruct.tempplayer[index].preparingskill].note.Split(',')[2]);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 7)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);
                                
                                //Colocar o inimigo para dormir
                                PStruct.tempplayer[target].Sleeping = true;
                                PStruct.tempplayer[target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[preparingskill].interval;
                                SendData.Send_Sleep(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, 1);
                                
                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 8)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Paralizar o inimigo
                                PStruct.tempplayer[PStruct.tempplayer[index].target].Stunned = true;
                                PStruct.tempplayer[PStruct.tempplayer[index].target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[PStruct.tempplayer[index].preparingskill].interval;
                                SendData.Send_Stun(Map, targettype, target, 1);
                               
                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 9)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 11)
                            {
                                //Tudo ok
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                                
                                //Alcance real
                                int true_range = 0;

                                //Considerar a distância e analisar até onde pode ser empurrado
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (PStruct.CanThrowPlayer(target, playerdir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                //É possível empurrar?
                                if (true_range > 0)
                                {
                                    //Empurrar
                                    PStruct.ThrowPlayer(target, playerdir, true_range);
                                }

                                //Quebra a conjuração do inimigo se estiver conjurando
                                if (PStruct.tempplayer[target].preparingskill > 0)
                                {
                                    //Resetar valores
                                    PStruct.tempplayer[target].preparingskill = 0;
                                    PStruct.tempplayer[target].preparingskillslot = 0;
                                    PStruct.tempplayer[target].skilltimer = 0;
                                    
                                    //MSG
                                    SendData.Send_ActionMsg(target, lang.spell_broken, Globals.ColorPink, PStruct.character[target, PStruct.player[target].SelectedChar].X, PStruct.character[target, PStruct.player[target].SelectedChar].Y, 1, 0, Map);
                                    
                                    //Movimento normal
                                    PStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                                    SendData.Send_MoveSpeed(Globals.Target_Player, target);
                                    SendData.Send_BrokeSkill(target);
                                }

                                //Atacar alvo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Movimento normal
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 12)
                            {
                                //Alcance real
                                int true_range = 0;

                                //Calcular alcance real
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (PStruct.CanThrowPlayer(index, playerdir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                //Alcance real é maior que 0?
                                if (true_range > 0)
                                {
                                    //Sim! Empurrar!
                                    PStruct.ThrowPlayer(index, playerdir, true_range);
                                }

                                //Tudo ok
                                //Animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Atacar algo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Ficará lento em seguida
                                PStruct.tempplayer[index].Slowed = true;
                                PStruct.tempplayer[index].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 2000;
                                PStruct.tempplayer[index].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(Globals.Target_Player, index);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 13)
                            {
                                //Tudo ok
                                //Animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Atacar alvo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Ficará cego
                                PStruct.tempplayer[target].Blind = true;
                                PStruct.tempplayer[target].BlindTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[preparingskill].interval;

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 15)
                            {
                                //Tudo ok
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Atacar o alvo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }

                            if ((SStruct.skill[preparingskill].type) == 18)
                            {
                                //Tudo ok
                                SendData.Send_Animation(Map, Globals.Target_Player, index, SStruct.skill[preparingskill].animation_id);

                                //Posições tmeporárias
                                int x = PStruct.character[target, PStruct.player[target].SelectedChar].X;
                                int y = PStruct.character[target, PStruct.player[target].SelectedChar].Y;
                                
                                //?
                                int p = 1;

                                //Preparar posições manualmente
                                if (PStruct.character[PStruct.tempplayer[index].target, PStruct.player[PStruct.tempplayer[index].target].SelectedChar].Dir == Globals.DirDown) { 
                                    if (PStruct.CanPlayerMove(PStruct.tempplayer[index].target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                if (PStruct.character[target, PStruct.player[target].SelectedChar].Dir == Globals.DirUp)
                                {
                                    if (PStruct.CanPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                if (PStruct.character[target, PStruct.player[target].SelectedChar].Dir == Globals.DirLeft)
                                {
                                    if (PStruct.CanPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                if (PStruct.character[target, PStruct.player[target].SelectedChar].Dir == Globals.DirRight)
                                {
                                    if (PStruct.CanPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    if (PStruct.CanPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (PStruct.CanPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                PStruct.tempplayer[target].Slowed = true;
                                PStruct.tempplayer[target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 1800;
                                PStruct.tempplayer[target].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(Globals.Target_Player, target);

                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(Globals.Target_Player, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 1)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                                
                                //Atacar alvo
                                PStruct.PlayerAttackPlayer(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);
                                
                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 2)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX == 0) && (DistanceY <= n))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX == 0) && (DistanceY <= n))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                                
                                //Atacar alvo
                                PStruct.PlayerAttackPlayer(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);
                                
                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 3)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                    {
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            //Se está no alcance, atacar
                                            PStruct.PlayerAttackNpc(index, i, preparingskill);
                                        }
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                        {
                                            if ((DistanceX <= n) && (DistanceY <= n))
                                            {
                                                //Se encontrou, e está no alcance, atacar!
                                                PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                            }
                                        }
                                    }
                                }
                                
                                //Enviar animaçõa
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                                
                                //Atacar alvo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);
                                
                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 4)
                            {
                                //Analisa todos os npcs do mapa
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY <= n))
                                    {
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                        NStruct.tempnpc[Map, i].isFrozen = true;
                                        NStruct.tempnpc[Map, i].movespeed -= 0.5;
                                        NStruct.tempnpc[Map, i].Slowed = true;
                                        NStruct.tempnpc[Map, i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                        SendData.Send_Frozen(Globals.Target_Npc, i, Map);
                                        SendData.Send_MoveSpeed(Globals.Target_Npc, i, Map);
                                        SendData.Send_ActionMsg(i, lang.frozen, Globals.ColorWhite, NStruct.tempnpc[Map, i].X, NStruct.tempnpc[Map, i].Y, Globals.Action_Msg_Scroll, 0, Map);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                            PStruct.tempplayer[i].isFrozen = true;
                                            PStruct.tempplayer[i].movespeed -= 0.5;
                                            PStruct.tempplayer[i].Slowed = true;
                                            PStruct.tempplayer[i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                            SendData.Send_Frozen(Globals.Target_Player, i);
                                            SendData.Send_MoveSpeed(Globals.Target_Player, i);
                                            SendData.Send_ActionMsg(i, lang.frozen, Globals.ColorWhite, PStruct.character[i, PStruct.player[i].SelectedChar].X, PStruct.character[i, PStruct.player[i].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                                
                                //Atacar o alvo
                                PStruct.PlayerAttackPlayer(index, target, preparingskill);
                                PStruct.tempplayer[target].isFrozen = true;
                                PStruct.tempplayer[target].movespeed -= 0.5;
                                PStruct.tempplayer[target].Slowed = true;
                                PStruct.tempplayer[target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                SendData.Send_Frozen(Globals.Target_Player, target);
                                SendData.Send_MoveSpeed(Globals.Target_Player, target);
                                SendData.Send_ActionMsg(target, lang.frozen, Globals.ColorWhite, PStruct.character[target, PStruct.player[target].SelectedChar].X, PStruct.character[target, PStruct.player[target].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                
                                //Resetar comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1,index);
                                return;
                            }
                        }
                        //Repetitivo
                        else
                        {
                            //Valor temporário
                            int temp_spell = PStruct.GetOpenPTempSpell(PStruct.tempplayer[index].target);
                            
                            //Enviar animação
                            SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                            //Reduzir dano
                            if (SStruct.skill[preparingskill].type == 20)
                            {
                                PStruct.tempplayer[target].ReduceDamage = 50;
                            }

                            //Pareparar objeto de execução
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].active = true;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].attacker = index;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].spellnum = PStruct.tempplayer[index].preparingskill;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[PStruct.tempplayer[index].preparingskill].interval;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].repeats = SStruct.skill[PStruct.tempplayer[index].preparingskill].repeats;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].anim = SStruct.skill[PStruct.tempplayer[index].preparingskill].second_anim;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].area_range = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].is_line = SStruct.skill[PStruct.tempplayer[index].preparingskill].is_line;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].is_heal = false;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].fast_buff = false;
                            
                            //Slow
                            if (SStruct.skill[PStruct.tempplayer[index].preparingskill].slow)
                            {
                                PStruct.tempplayer[PStruct.tempplayer[index].target].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(Globals.Target_Player, PStruct.tempplayer[index].target);
                            }

                            //Resetar valores comuns
                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(Globals.Target_Player,index);
                            return;
                        }
                    }
                    if (SStruct.skill[preparingskill].damage_type == 2)
                    {
                        //Tudo ok
                        //Enviar animações
                        SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                        
                        //Atacar alvo
                        PStruct.PlayerAttackPlayer(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);
                        
                        //Resetar valores comuns
                        PStruct.tempplayer[index].preparingskill = 0;
                        PStruct.tempplayer[index].preparingskillslot = 0;
                        PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                        SendData.Send_MoveSpeed(1,index);
                        return;
                    }
                    //Recuperação de mana
                    if (SStruct.skill[preparingskill].damage_type == 4)
                    {
                        //Tipo 1
                        if ((SStruct.skill[preparingskill].type) == 1)
                        {
                            //Dano de cura
                            int Damage = HealSpellDamage(index, preparingskill);

                            //Enviar animação
                            SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                            
                            //Recuperar energia
                            SpiritPlayer(index, Damage);
                            
                            //Resetar valores
                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(1, index);
                            return;
                        }
                    }
                    //Cura
                    if (SStruct.skill[preparingskill].damage_type == 3)
                    {
                        if ((SStruct.skill[preparingskill].type) == 1)
                        {
                            if (!(PStruct.character[target, PStruct.player[target].SelectedChar].Map == Map))
                            {
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                SendData.Send_MsgToPlayer(index, lang.could_not_perform_magic, Globals.ColorRed, Globals.Msg_Type_Server);
                                return;
                            }

                            int temp_spell = PStruct.GetOpenPTempSpell(target);
                            SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].active = true;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].attacker = index;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].spellnum = PStruct.tempplayer[index].preparingskill;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[PStruct.tempplayer[index].preparingskill].interval;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].repeats = SStruct.skill[PStruct.tempplayer[index].preparingskill].repeats;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].anim = SStruct.skill[PStruct.tempplayer[index].preparingskill].second_anim;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].area_range = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].is_line = SStruct.skill[PStruct.tempplayer[index].preparingskill].is_line;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].is_heal = true;
                            PStruct.ptempspell[PStruct.tempplayer[index].target, temp_spell].fast_buff = true;

                            PStruct.tempplayer[PStruct.tempplayer[index].target].movespeed = Globals.FastMoveSpeed;
                            SendData.Send_MoveSpeed(1, PStruct.tempplayer[index].target);

                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            if (PStruct.tempplayer[index].target != index)
                            {
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                            }
                            return;
                        }
                        if ((SStruct.skill[preparingskill].type) == 0)
                        {


                            int Damage = HealSpellDamage(index, PStruct.tempplayer[index].preparingskill);
                            //Tudo ok
                            SendData.Send_Animation(Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                            HealPlayer(PStruct.tempplayer[index].target, Damage);
                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(1, index);
                        }
                    }
                }
            }



            //NPC
            if (PStruct.tempplayer[index].targettype == 2)
            {
                //Posição do alvo
                int targetx = NStruct.tempnpc[Map, target].X;
                int targety = NStruct.tempnpc[Map, target].Y;

                //Calcular distância
                n = SStruct.skill[PStruct.tempplayer[index].preparingskill].tp_gain;
                DistanceX = playerx - targetx;
                DistanceY = playery - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    //Cooldown
                    PStruct.skill[index, PStruct.hotkey[index, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                    SendData.Send_PlayerSkillCooldown(index, preparingskillslot, tp_cost * 500);
                    //Atualiza MP
                    PStruct.tempplayer[index].Spirit -= SStruct.skill[preparingskill].mp_cost * PStruct.skill[index, PStruct.hotkey[index, PStruct.tempplayer[index].preparingskillslot].num].level;
                    SendData.Send_PlayerSpiritToMap(Map, index, PStruct.tempplayer[index].Spirit);
                    if (PStruct.tempplayer[index].Party > 0) { SendData.Send_PlayerSpiritToParty(PStruct.tempplayer[index].Party, index, PStruct.tempplayer[index].Spirit); }

                    //Execução da magia
                    if (SStruct.skill[preparingskill].damage_type == 1)
                    {
                        if (SStruct.skill[preparingskill].repeats <= 1)
                        {
                            if ((SStruct.skill[preparingskill].type) == 0)
                            {
                                //Tudo ok
                                //Animação para o npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, preparingskill);

                                //Habilidade passiva do Iarumas
                                if (PStruct.character[index, PStruct.player[index].SelectedChar].ClassId == 4)
                                {
                                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                                    {
                                        if ((PStruct.skill[index, i].num == 47) && (PStruct.skill[index, i].level > 0))
                                        {
                                            if (PStruct.tempplayer[index].Sheathe < 3)
                                            {
                                                PStruct.tempplayer[index].Sheathe += 1;
                                            }
                                            else
                                            {
                                                //Animação para o npc
                                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[47].animation_id);
                                                PStruct.PlayerAttackNpc(index, target, 47);
                                            }
                                            break;
                                        }
                                    }
                                }

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 6)
                            {
                                //Tudo ok
                                //Animação para o npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, target, preparingskill);

                                //Spell Buff slot
                                int spellbuff = PStruct.GetOpenSpellBuff(index);

                                //Define dados do buff spell no npc
                                PStruct.pspellbuff[index, spellbuff].active = true;
                                PStruct.pspellbuff[index, spellbuff].type = 1; //DAMAGE
                                PStruct.pspellbuff[index, spellbuff].value = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                                PStruct.pspellbuff[index, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SStruct.skill[PStruct.tempplayer[index].preparingskill].note.Split(',')[2]);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 7)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, target, preparingskill);

                                //Coloca o npc para dormir
                                NStruct.tempnpc[Map, target].Sleeping = true;
                                NStruct.tempnpc[Map, target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[preparingskill].interval;
                                SendData.Send_Sleep(Map, targettype, target, 1);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 8)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Paraliza o npc
                                NStruct.tempnpc[Map, target].Stunned = true;
                                NStruct.tempnpc[Map, target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[preparingskill].interval;
                                SendData.Send_Stun(Map, targettype, target, 1);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 9)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, target, preparingskill);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 11)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Alcance real
                                int true_range = 0;

                                //Cálcula do alcance real
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (PStruct.CanThrowNpc(Map, target, playerdir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                //Se o alcance for maior que zero, empurrar
                                if (true_range > 0)
                                {
                                    PStruct.ThrowNpc(Map, PStruct.tempplayer[index].target, PStruct.character[index, PStruct.player[index].SelectedChar].Dir, true_range);
                                }

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 15)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, target, preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[PStruct.tempplayer[index].preparingskill].type) == 12)
                            {
                                //Tudo ok
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                int true_range = 0;
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (PStruct.CanThrowPlayer(index, playerdir, i))
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
                                    PStruct.ThrowPlayer(index, playerdir, true_range);
                                }

                                PStruct.tempplayer[index].Slowed = true;
                                PStruct.tempplayer[index].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 800;
                                PStruct.tempplayer[index].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(Globals.Target_Player, index);

                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 18)
                            {
                                //Tudo ok
                                SendData.Send_Animation(Map, Globals.Target_Player, index, SStruct.skill[preparingskill].animation_id);

                                //Váriaveis comuns
                                int x = NStruct.tempnpc[Map, target].X;
                                int y = NStruct.tempnpc[Map, target].Y; ;

                                //Análise de posição - Baixo
                                if (NStruct.tempnpc[Map, PStruct.tempplayer[index].target].Dir == Globals.DirDown)
                                {
                                    if (NpcIA.CanNpcMove(Map, target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                //Análise de posição - Cima
                                if (NStruct.tempnpc[Map, target].Dir == Globals.DirUp)
                                {
                                    if (NpcIA.CanNpcMove(Map, target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                //Análise de posição - Esquerda
                                if (NStruct.tempnpc[Map, target].Dir == Globals.DirLeft)
                                {
                                    if (NpcIA.CanNpcMove(Map, target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                //Análise de posição - Direita
                                if (NStruct.tempnpc[Map, target].Dir == Globals.DirRight)
                                {
                                    if (NpcIA.CanNpcMove(Map, target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    if (NpcIA.CanNpcMove(Map, target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (NpcIA.CanNpcMove(Map, target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    PStruct.character[index, PStruct.player[index].SelectedChar].X = Convert.ToByte(x);
                                    PStruct.character[index, PStruct.player[index].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.Send_PlayerXY(index);
                                }

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 14)
                            {
                                //Valor real
                                int true_range = 0;

                                //Valores amarzenados
                                int old_x = PStruct.character[index, PStruct.player[index].SelectedChar].X;
                                int old_y = PStruct.character[index, PStruct.player[index].SelectedChar].Y;

                                //Cálculo do valor real
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (PStruct.CanThrowPlayer(index, playerdir, i))
                                    {
                                        true_range += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                //Se o valor real for maior que zero, processar possibilidades
                                if (true_range > 0)
                                {
                                    int x = 0;
                                    int y = 0;

                                    //Jogadores
                                    for (int i = 0; i <= Globals.Player_Highindex; i++)
                                    {
                                        if ((PStruct.tempplayer[i].ingame == true) && (i != index))
                                        {
                                            if (Convert.ToInt32(PStruct.character[i, PStruct.player[i].SelectedChar].Map) == Map)
                                            {
                                                for (int p = 0; p <= true_range; p++)
                                                {
                                                    x = old_x;
                                                    y = old_y;

                                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                                    if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                                    //Execução
                                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].X == x) && (PStruct.character[i, PStruct.player[i].SelectedChar].Y == y)) { SendData.Send_Animation(Map, Globals.Target_Player, i, 149); PStruct.PlayerAttackPlayer(index, i, preparingskill); }
                                                }
                                            }

                                        }
                                    }

                                    //Npc's
                                    for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                    {
                                        for (int p = 0; p <= true_range; p++)
                                        {
                                            x = old_x;
                                            y = old_y;

                                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                            if (PStruct.character[index, PStruct.player[index].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                            //Execução
                                            if ((NStruct.tempnpc[Map, i].X == x) && (NStruct.tempnpc[Map, i].Y == y)) { SendData.Send_Animation(Map, Globals.Target_Npc, i, 149); PStruct.PlayerAttackNpc(index, i, preparingskill); }
                                        }
                                    }
                                    PStruct.ThrowPlayer(index, PStruct.character[index, PStruct.player[index].SelectedChar].Dir, true_range);
                                }



                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(Map, Globals.Target_Player, index, SStruct.skill[preparingskill].animation_id);

                                //PStruct.PlayerAttackPlayer(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Deixa o jogador lento por 0.3 segs
                                PStruct.tempplayer[index].Slowed = true;
                                PStruct.tempplayer[index].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 300;
                                PStruct.tempplayer[index].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(Globals.Target_Player, index);

                                //Reseta valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 13)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, target, preparingskill);

                                //Deixa o npc alvo cego
                                NStruct.tempnpc[Map, PStruct.tempplayer[index].target].Blind = true;
                                NStruct.tempnpc[Map, PStruct.tempplayer[index].target].BlindTimer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[preparingskill].interval;

                                //Resete valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }

                            if ((SStruct.skill[preparingskill].type) == 1)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 2)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX == 0) && (DistanceY <= n))
                                    {
                                        //Se está no alcance, atacar
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX == 0) && (DistanceY <= n))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 3)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                    {
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            //Se está no alcance, atacar
                                            PStruct.PlayerAttackNpc(index, i, preparingskill);
                                        }
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                        {
                                            if ((DistanceX <= n) && (DistanceY <= n))
                                            {
                                                //Se encontrou, e está no alcance, atacar!
                                                PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                            }
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                                //Ataca o npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                            if ((SStruct.skill[preparingskill].type) == 4)
                            {
                                //Analisa todos os npcs do mapa
                                for (int i = 0; i <= MStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SStruct.skill[preparingskill].range_effect;
                                    DistanceX = NStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY <= n))
                                    {
                                        PStruct.PlayerAttackNpc(index, i, preparingskill);
                                        NStruct.tempnpc[Map, i].isFrozen = true;
                                        NStruct.tempnpc[Map, i].movespeed -= 0.5;
                                        NStruct.tempnpc[Map, i].Slowed = true;
                                        NStruct.tempnpc[Map, i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                        SendData.Send_Frozen(Globals.Target_Npc, i, Map);
                                        SendData.Send_MoveSpeed(Globals.Target_Npc, i, Map);
                                        SendData.Send_ActionMsg(i, lang.frozen, Globals.ColorWhite, NStruct.tempnpc[Map, i].X, NStruct.tempnpc[Map, i].Y, Globals.Action_Msg_Scroll, 0, Map);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highindex; i++)
                                {
                                    //O mapa é o mesmo do jogador? index diferente do atacante?
                                    if ((PStruct.character[i, PStruct.player[i].SelectedChar].Map == Map) && (i != index) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SStruct.skill[preparingskill].range_effect;
                                        DistanceX = PStruct.character[i, PStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PStruct.character[i, PStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            PStruct.PlayerAttackPlayer(index, i, preparingskill);
                                            PStruct.tempplayer[i].isFrozen = true;
                                            PStruct.tempplayer[i].movespeed -= 0.5;
                                            PStruct.tempplayer[i].Slowed = true;
                                            PStruct.tempplayer[i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                            SendData.Send_Frozen(Globals.Target_Player, i);
                                            SendData.Send_MoveSpeed(Globals.Target_Player, i);
                                            SendData.Send_ActionMsg(i, lang.frozen, Globals.ColorWhite, PStruct.character[i, PStruct.player[i].SelectedChar].X, PStruct.character[i, PStruct.player[i].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                        }
                                    }
                                }

                                //Enviar animação do npc
                                SendData.Send_Animation(Map, targettype, target, SStruct.skill[preparingskill].animation_id);

                                //Ataque ao npc alvo
                                PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);
                                NStruct.tempnpc[Map, target].isFrozen = true;
                                NStruct.tempnpc[Map, target].movespeed -= 0.5;
                                NStruct.tempnpc[Map, target].Slowed = true;
                                NStruct.tempnpc[Map, target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                SendData.Send_Frozen(Globals.Target_Npc, target, Map);
                                SendData.Send_MoveSpeed(Globals.Target_Npc, target, Map);
                                SendData.Send_ActionMsg(target, lang.frozen, Globals.ColorWhite, NStruct.tempnpc[Map, target].X, NStruct.tempnpc[Map, target].Y, Globals.Action_Msg_Scroll, 0, Map);

                                //Resetar valores comuns
                                PStruct.tempplayer[index].preparingskill = 0;
                                PStruct.tempplayer[index].preparingskillslot = 0;
                                PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                                SendData.Send_MoveSpeed(1, index);
                                return;
                            }
                        }
                        else
                        {
                            //Magia temporária
                            int temp_spell = NStruct.GetOpenNTempSpell(PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target);

                            //Enviar animação
                            SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, 2, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);

                            //Reduzir dano
                            if (SStruct.skill[preparingskill].type == 20)
                            {
                                NStruct.tempnpc[Map, target].ReduceDamage = 50;
                            }

                            //Armazenar informações da magia
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].active = true;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].attacker = index;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].spellnum = PStruct.tempplayer[index].preparingskill;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SStruct.skill[PStruct.tempplayer[index].preparingskill].interval;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].repeats = SStruct.skill[PStruct.tempplayer[index].preparingskill].repeats;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].anim = SStruct.skill[PStruct.tempplayer[index].preparingskill].second_anim;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].area_range = SStruct.skill[PStruct.tempplayer[index].preparingskill].range_effect;
                            NStruct.ntempspell[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target, temp_spell].is_line = SStruct.skill[PStruct.tempplayer[index].preparingskill].is_line;

                            //Lentidão no npc
                            if (SStruct.skill[PStruct.tempplayer[index].preparingskill].slow)
                            {
                                NStruct.tempnpc[PStruct.character[index, Convert.ToInt32(PStruct.player[index].SelectedChar)].Map, PStruct.tempplayer[index].target].movespeed -= 0.5;
                                SendData.Send_MoveSpeed(2, PStruct.tempplayer[index].target, PStruct.character[index, PStruct.player[index].SelectedChar].Map);
                            }


                            //Resetar valores comuns
                            PStruct.tempplayer[index].preparingskill = 0;
                            PStruct.tempplayer[index].preparingskillslot = 0;
                            PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                            SendData.Send_MoveSpeed(1, index);
                            return;
                        }
                    }
                    if (SStruct.skill[PStruct.tempplayer[index].preparingskill].damage_type == 2)
                    {
                        //Tudo ok
                        //Enviar animação
                        SendData.Send_Animation(PStruct.character[index, PStruct.player[index].SelectedChar].Map, PStruct.tempplayer[index].targettype, PStruct.tempplayer[index].target, SStruct.skill[PStruct.tempplayer[index].preparingskill].animation_id);
                        
                        //Atacar o npc alvo
                        PStruct.PlayerAttackNpc(index, PStruct.tempplayer[index].target, PStruct.tempplayer[index].preparingskill);
                        
                        //Resetar valores comuns
                        PStruct.tempplayer[index].preparingskill = 0;
                        PStruct.tempplayer[index].preparingskillslot = 0;
                        PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                        SendData.Send_MoveSpeed(1,index);
                        return;
                    }
                }
                //Não está! LOL
                else
                {
                    //Sem alcance
                    SendData.Send_MsgToPlayer(index, lang.target_distance_too_long, Globals.ColorRed, Globals.Msg_Type_Server);
                    
                    //Resetar valores comuns
                    PStruct.tempplayer[index].preparingskill = 0;
                    PStruct.tempplayer[index].preparingskillslot = 0;
                    PStruct.tempplayer[index].movespeed = Globals.NormalMoveSpeed;
                    SendData.Send_MoveSpeed(1,index);
                    return;
                }
            }
        }
    }
}
