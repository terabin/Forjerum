using System;
using System.Linq;
using System.Reflection;

namespace __Forjerum
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
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, email) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, email));
            }

            //CÓDIGO
            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.player[i].Email == email)
                {
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // HealPlayer
        //*********************************************************************************************
        public static void HealPlayer(int s, int damage)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, damage) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.tempplayer[s].Vitality += damage;
            if (PlayerStruct.tempplayer[s].Vitality > PlayerRelations.getPlayerMaxVitality(s))
            {
                PlayerStruct.tempplayer[s].Vitality = PlayerRelations.getPlayerMaxVitality(s);
            }

            SendData.sendActionMsg(s, "+" + damage, Globals.ColorGreen, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            SendData.sendPlayerVitalityToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Vitality);
            if (PlayerStruct.tempplayer[s].Party > 0)
            {
                SendData.sendPlayerVitalityToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Vitality);
            }
        }
        //*********************************************************************************************
        // SpiritPlayer
        //*********************************************************************************************
        public static void SpiritPlayer(int s, int damage)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, damage) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.tempplayer[s].Spirit += damage;
            if (PlayerStruct.tempplayer[s].Spirit > PlayerRelations.getPlayerMaxSpirit(s))
            {
                PlayerStruct.tempplayer[s].Spirit = PlayerRelations.getPlayerMaxSpirit(s);
            }

            SendData.sendActionMsg(s, "+" + damage, Globals.ColorBlue, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
            SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);
            if (PlayerStruct.tempplayer[s].Party > 0)
            {
                SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit);
            }
        }
        //*********************************************************************************************
        // HealSpellDamage
        // Retorna a quantidade de dano da cura (não dano, mas ok)
        //*********************************************************************************************
        public static int HealSpellDamage(int s, int isSpell)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, isSpell) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, isSpell));
            }

            //CÓDIGO
            //Multiplicador de dano
            double multiplier = Convert.ToDouble(SkillStruct.skill[isSpell].scope) / 10;

            //Elemento mágico multiplicado
            double min_damage = PlayerRelations.getPlayerMinMagic(s) * multiplier;
            double max_damage = PlayerRelations.getPlayerMaxMagic(s) * multiplier;

            //Multiplicador de nível
            double levelmultiplier = (1.0 + multiplier) * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level; //Except

            //Verificando se a skill teve algum problema e corrigindo
            if (levelmultiplier < 1.0) { levelmultiplier = 1.0; }

            //Dano total que pode ser causado
            double totaldamage = (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) + max_damage) * levelmultiplier;
            double totalmindamage = (Convert.ToDouble(SkillStruct.skill[isSpell].damage_formula) + min_damage) * levelmultiplier;

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
        public static bool ExecuteSelfSkill(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            //Váriaveis de análise
            int Map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int isSpell = PlayerStruct.tempplayer[s].preparingskill;
            int target = PlayerStruct.tempplayer[s].target;
            int targettype = PlayerStruct.tempplayer[s].targettype;
            int playerx = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            int playery = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;
            int preparingskill = PlayerStruct.tempplayer[s].preparingskill;
            int preparingskillslot = PlayerStruct.tempplayer[s].preparingskillslot;
            int tp_cost = SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost;
            byte playerdir = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir;

            //Magias que devem ser executadas apenas no jogador
            if ((SkillStruct.skill[preparingskill].type) == 14)
            {
                //Cooldown
                PlayerStruct.skill[s, PlayerStruct.hotkey[s, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                SendData.sendPlayerSkillCooldown(s, preparingskillslot, tp_cost * 500);
                //Atualiza MP
                PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                SendData.sendPlayerSpiritToMap(Map, s, PlayerStruct.tempplayer[s].Spirit);

                int true_range = 0;
                int old_x = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
                int old_y = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;

                for (int i = 1; i <= 3; i++)
                {
                    if (MovementRelations.canThrowPlayer(s, playerdir, i))
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
                    for (int i = 0; i <= Globals.Player_Highs; i++)
                    {
                        if ((PlayerStruct.tempplayer[i].ingame == true) && (i != s))
                        {
                            if (Convert.ToInt32(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map) == Map)
                            {
                                for (int p = 0; p <= true_range; p++)
                                {
                                    x = old_x;
                                    y = old_y;

                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == x) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == y)) { SendData.sendAnimation(Map, Globals.Target_Player, i, 149); CombatRelations.playerAttackPlayer(s, i, preparingskill); }
                                }
                            }

                        }
                    }

                    for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                    {
                        for (int p = 0; p <= true_range; p++)
                        {
                            x = old_x;
                            y = old_y;

                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirDown) { y += p; }
                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirRight) { x += p; }
                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                            if ((NpcStruct.tempnpc[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].X == x) && (NpcStruct.tempnpc[PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, i].Y == y)) { SendData.sendAnimation(Map, Globals.Target_Npc, i, 149); CombatRelations.playerAttackNpc(s, i, preparingskill); }
                        }
                    }

                    MovementRelations.throwPlayer(s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir, true_range);
                }

                //Tudo ok
                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, Globals.Target_Player, s, SkillStruct.skill[preparingskill].animation_id);

                //Paralizar inimigo
                PlayerStruct.tempplayer[s].Slowed = true;
                PlayerStruct.tempplayer[s].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 300;
                PlayerStruct.tempplayer[s].movespeed -= 0.5;
                SendData.sendMoveSpeed(Globals.Target_Player, s);

                //Resetar valores comuns
                PlayerStruct.tempplayer[s].preparingskill = 0;
                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                return true;
            }
            if ((SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].type) == 16)
            {
                //Cooldown
                PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500);
                SendData.sendPlayerSkillCooldown(s, PlayerStruct.tempplayer[s].preparingskillslot, (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500));
                //Atualiza MP
                PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);

                if (PlayerStruct.tempplayer[s].Party > 0) { SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit); }

                //Tudo ok
                //Enviar animação
                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, Globals.Target_Player, s, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                //Refletir dano
                PlayerStruct.tempplayer[s].Reflect = true;
                PlayerStruct.tempplayer[s].ReflectTimer = Loops.TickCount.ElapsedMilliseconds + 2000;

                //Resetar valores comuns
                PlayerStruct.tempplayer[s].preparingskill = 0;
                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                SendData.sendMoveSpeed(1, s);
                return true;
            }
            if (PlayerStruct.tempplayer[s].preparingskill == 34)
            {
                if ((SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].type) == 0)
                {
                    int Damage = HealSpellDamage(s, PlayerStruct.tempplayer[s].preparingskill);
                    //Cooldown
                    PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500);
                    SendData.sendPlayerSkillCooldown(s, PlayerStruct.tempplayer[s].preparingskillslot, (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500));
                    //Atualiza MP
                    PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                    SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);

                    if (PlayerStruct.tempplayer[s].Party > 0) { SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit); }
                    //Tudo ok
                    SendData.sendAnimation(Map, Globals.Target_Player, s, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                    HealPlayer(PlayerStruct.tempplayer[s].target, Damage);
                    PlayerStruct.tempplayer[s].preparingskill = 0;
                    PlayerStruct.tempplayer[s].preparingskillslot = 0;
                    PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                    SendData.sendMoveSpeed(1, s);
                    return true;
                }
            }
            //Cura
            if (SkillStruct.skill[preparingskill].damage_type == 3)
            {
                if ((SkillStruct.skill[preparingskill].type) == 1)
                {
                    //Cooldown
                    PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500);
                    SendData.sendPlayerSkillCooldown(s, PlayerStruct.tempplayer[s].preparingskillslot, (SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost * 500));
                    //Atualiza MP
                    PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                    SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);
                    int temp_spell = PlayerRelations.getOpenTempSpell(s);
                    SendData.sendAnimation(Map, s, s, SkillStruct.skill[preparingskill].animation_id);
                    PlayerStruct.ptempspell[s, temp_spell].active = true;
                    PlayerStruct.ptempspell[s, temp_spell].attacker = s;
                    PlayerStruct.ptempspell[s, temp_spell].spellnum = PlayerStruct.tempplayer[s].preparingskill;
                    PlayerStruct.ptempspell[s, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].interval;
                    PlayerStruct.ptempspell[s, temp_spell].repeats = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].repeats;
                    PlayerStruct.ptempspell[s, temp_spell].anim = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].second_anim;
                    PlayerStruct.ptempspell[s, temp_spell].area_range = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                    PlayerStruct.ptempspell[s, temp_spell].is_line = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].is_line;
                    PlayerStruct.ptempspell[s, temp_spell].is_heal = true;
                    PlayerStruct.ptempspell[s, temp_spell].fast_buff = true;

                    PlayerStruct.tempplayer[s].movespeed = Globals.FastMoveSpeed;
                    SendData.sendMoveSpeed(1, s);

                    PlayerStruct.tempplayer[s].preparingskill = 0;
                    PlayerStruct.tempplayer[s].preparingskillslot = 0;
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
        public static void ExecuteSkill(int s)
        {
            int Map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            int isSpell = PlayerStruct.tempplayer[s].preparingskill;
            int target = PlayerStruct.tempplayer[s].target;
            int targettype = PlayerStruct.tempplayer[s].targettype;
            int playerx = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            int playery = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;
            int preparingskill = PlayerStruct.tempplayer[s].preparingskill; 
            int preparingskillslot = PlayerStruct.tempplayer[s].preparingskillslot;
            int tp_cost = SkillStruct.skill[PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].num].tp_cost;
            int DistanceX = 0;
            int DistanceY = 0;
            int n = 0;
            byte playerdir = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir;

            if (PlayerStruct.tempplayer[s].Spirit < SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].mp_cost) { return; }
            if (ExecuteSelfSkill(s)) { return; }

            //O alvo é um jogador
            if (PlayerStruct.tempplayer[s].targettype == 1)
            {
                int clientid = UserConnection.getS(PlayerStruct.tempplayer[s].target);

                //Verificar se não é inválido para análise da conexão
                if ((clientid < 0) || (clientid >= WinsockAsync.Clients.Count()))
                {
                    PlayerStruct.tempplayer[s].preparingskill = 0;
                    PlayerStruct.tempplayer[s].preparingskillslot = 0;
                    PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                    return;
                }

                //Verificar se o jogador não se desconectou no processo ou se não há falhas nas variáveis
                if ((!WinsockAsync.Clients[clientid].IsConnected) || (PlayerStruct.tempplayer[s].target > Globals.Player_Highs) || (PlayerStruct.tempplayer[s].target < 0) || (!PlayerStruct.tempplayer[target].ingame))
                {
                    PlayerStruct.tempplayer[s].preparingskill = 0;
                    PlayerStruct.tempplayer[s].preparingskillslot = 0;
                    PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;   
                    return;
                }

                //Posição do alvo
                int targetx = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X;
                int targety = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y;

                //Calcular distância
                n = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].tp_gain;
                DistanceX = playerx - targetx;
                DistanceY = playery - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    if (((SkillStruct.skill[preparingskill].type) == 0) && (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map == Map))
                    {
                        int Damage = HealSpellDamage(s, PlayerStruct.tempplayer[s].preparingskill);
                        //Cooldown
                        PlayerStruct.skill[s, PlayerStruct.hotkey[s, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                        SendData.sendPlayerSkillCooldown(s, preparingskillslot, tp_cost * 500);
                        //Atualiza MP
                        PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                        SendData.sendPlayerSpiritToMap(Map, s, PlayerStruct.tempplayer[s].Spirit);
                        //Tudo ok
                        SendData.sendAnimation(Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                        HealPlayer(PlayerStruct.tempplayer[s].target, Damage);
                        PlayerStruct.tempplayer[s].preparingskill = 0;
                        PlayerStruct.tempplayer[s].preparingskillslot = 0;
                        PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(1, s);
                        return;
                    }

                    //Check in
                    if (!(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map == Map) || !(target != s))
                    {
                        PlayerStruct.tempplayer[s].preparingskill = 0;
                        PlayerStruct.tempplayer[s].preparingskillslot = 0;
                        PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(1, s);
                        SendData.sendMsgToPlayer(s, lang.could_not_perform_magic, Globals.ColorRed, Globals.Msg_Type_Server);
                        return;
                    }

                    //Check in
                    if (!MapStruct.tempmap[Map].WarActive)
                    {
                        if (!PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP)
                        {
                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(1, s);
                            return;
                        }
                    }


                    //Cooldown
                    PlayerStruct.skill[s, PlayerStruct.hotkey[s, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                    SendData.sendPlayerSkillCooldown(s, preparingskillslot, tp_cost * 500);
                    //Atualiza MP
                    PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                    SendData.sendPlayerSpiritToMap(Map, s, PlayerStruct.tempplayer[s].Spirit);
                    if (PlayerStruct.tempplayer[s].Party > 0) { SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit); }

                    //Execução da magia de dano
                    if (SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].damage_type == 1)
                    {
                        //A skill é de repetição?
                        if (SkillStruct.skill[preparingskill].repeats <= 1)
                        {
                            if ((SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].type) == 0)
                            {
                                //Envio da animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Habilidade passiva do Iarumas
                                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 4)
                                {
                                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                                    {
                                        if ((PlayerStruct.skill[s, i].num == 47) && (PlayerStruct.skill[s, i].level > 0))
                                        {
                                            if (PlayerStruct.tempplayer[s].Sheathe < 3)
                                            {
                                                PlayerStruct.tempplayer[s].Sheathe += 1;
                                            }
                                            else
                                            {
                                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[47].animation_id);
                                                CombatRelations.playerAttackPlayer(s, target, 47);
                                            }
                                            break;
                                        }
                                    }
                                }

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 6)
                            {
                                //Tudo ok

                                //Envio da animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                int spellbuff = SkillRelations.getOpenSpellBuff(s);

                                PlayerStruct.pspellbuff[s, spellbuff].active = true;
                                PlayerStruct.pspellbuff[s, spellbuff].type = 1; //DAMAGE
                                PlayerStruct.pspellbuff[s, spellbuff].value = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                                PlayerStruct.pspellbuff[s, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].note.Split(',')[2]);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 7)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);
                                
                                //Colocar o inimigo para dormir
                                PlayerStruct.tempplayer[target].Sleeping = true;
                                PlayerStruct.tempplayer[target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[preparingskill].interval;
                                SendData.sendSleep(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, 1);
                                
                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 8)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Paralizar o inimigo
                                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].target].Stunned = true;
                                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].interval;
                                SendData.sendStun(Map, targettype, target, 1);
                               
                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 9)
                            {
                                //Tudo ok
                                //Envio da animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Execução do ataque
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 11)
                            {
                                //Tudo ok
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                                
                                //Alcance real
                                int true_range = 0;

                                //Considerar a distância e analisar até onde pode ser empurrado
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (MovementRelations.canThrowPlayer(target, playerdir, i))
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
                                    MovementRelations.throwPlayer(target, playerdir, true_range);
                                }

                                //Quebra a conjuração do inimigo se estiver conjurando
                                if (PlayerStruct.tempplayer[target].preparingskill > 0)
                                {
                                    //Resetar valores
                                    PlayerStruct.tempplayer[target].preparingskill = 0;
                                    PlayerStruct.tempplayer[target].preparingskillslot = 0;
                                    PlayerStruct.tempplayer[target].skilltimer = 0;
                                    
                                    //MSG
                                    SendData.sendActionMsg(target, lang.spell_broken, Globals.ColorPink, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y, 1, 0, Map);
                                    
                                    //Movimento normal
                                    PlayerStruct.tempplayer[target].movespeed = Globals.NormalMoveSpeed;
                                    SendData.sendMoveSpeed(Globals.Target_Player, target);
                                    SendData.sendBrokeSkill(target);
                                }

                                //Atacar alvo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Movimento normal
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 12)
                            {
                                //Alcance real
                                int true_range = 0;

                                //Calcular alcance real
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (MovementRelations.canThrowPlayer(s, playerdir, i))
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
                                    MovementRelations.throwPlayer(s, playerdir, true_range);
                                }

                                //Tudo ok
                                //Animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Atacar algo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Ficará lento em seguida
                                PlayerStruct.tempplayer[s].Slowed = true;
                                PlayerStruct.tempplayer[s].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 2000;
                                PlayerStruct.tempplayer[s].movespeed -= 0.5;
                                SendData.sendMoveSpeed(Globals.Target_Player, s);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 13)
                            {
                                //Tudo ok
                                //Animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Atacar alvo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Ficará cego
                                PlayerStruct.tempplayer[target].Blind = true;
                                PlayerStruct.tempplayer[target].BlindTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[preparingskill].interval;

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 15)
                            {
                                //Tudo ok
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Atacar o alvo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 18)
                            {
                                //Tudo ok
                                SendData.sendAnimation(Map, Globals.Target_Player, s, SkillStruct.skill[preparingskill].animation_id);

                                //Posições tmeporárias
                                int x = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X;
                                int y = PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y;
                                
                                //?
                                int p = 1;

                                //Preparar posições manualmente
                                if (PlayerStruct.character[PlayerStruct.tempplayer[s].target, PlayerStruct.player[PlayerStruct.tempplayer[s].target].SelectedChar].Dir == Globals.DirDown) { 
                                    if (MovementRelations.canPlayerMove(PlayerStruct.tempplayer[s].target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Dir == Globals.DirUp)
                                {
                                    if (MovementRelations.canPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Dir == Globals.DirLeft)
                                {
                                    if (MovementRelations.canPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                if (PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Dir == Globals.DirRight)
                                {
                                    if (MovementRelations.canPlayerMove(target, Globals.DirRight))
                                    {
                                        x += 1;
                                    }
                                    if (MovementRelations.canPlayerMove(target, Globals.DirLeft))
                                    {
                                        x -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirUp))
                                    {
                                        y -= 1;
                                    }
                                    else if (MovementRelations.canPlayerMove(target, Globals.DirDown))
                                    {
                                        y += 1;
                                    }
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                PlayerStruct.tempplayer[target].Slowed = true;
                                PlayerStruct.tempplayer[target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 1800;
                                PlayerStruct.tempplayer[target].movespeed -= 0.5;
                                SendData.sendMoveSpeed(Globals.Target_Player, target);

                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(Globals.Target_Player, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 1)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                                
                                //Atacar alvo
                                CombatRelations.playerAttackPlayer(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);
                                
                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 2)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX == 0) && (DistanceY <= n))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX == 0) && (DistanceY <= n))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                                
                                //Atacar alvo
                                CombatRelations.playerAttackPlayer(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);
                                
                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 3)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                    {
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            //Se está no alcance, atacar
                                            CombatRelations.playerAttackNpc(s, i, preparingskill);
                                        }
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                        {
                                            if ((DistanceX <= n) && (DistanceY <= n))
                                            {
                                                //Se encontrou, e está no alcance, atacar!
                                                CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                            }
                                        }
                                    }
                                }
                                
                                //Enviar animaçõa
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                                
                                //Atacar alvo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);
                                
                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 4)
                            {
                                //Analisa todos os npcs do mapa
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY <= n))
                                    {
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                        NpcStruct.tempnpc[Map, i].isFrozen = true;
                                        NpcStruct.tempnpc[Map, i].movespeed -= 0.5;
                                        NpcStruct.tempnpc[Map, i].Slowed = true;
                                        NpcStruct.tempnpc[Map, i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                        SendData.sendFrozen(Globals.Target_Npc, i, Map);
                                        SendData.sendMoveSpeed(Globals.Target_Npc, i, Map);
                                        SendData.sendActionMsg(i, lang.frozen, Globals.ColorWhite, NpcStruct.tempnpc[Map, i].X, NpcStruct.tempnpc[Map, i].Y, Globals.Action_Msg_Scroll, 0, Map);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                            PlayerStruct.tempplayer[i].isFrozen = true;
                                            PlayerStruct.tempplayer[i].movespeed -= 0.5;
                                            PlayerStruct.tempplayer[i].Slowed = true;
                                            PlayerStruct.tempplayer[i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                            SendData.sendFrozen(Globals.Target_Player, i);
                                            SendData.sendMoveSpeed(Globals.Target_Player, i);
                                            SendData.sendActionMsg(i, lang.frozen, Globals.ColorWhite, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                        }
                                    }
                                }

                                //Enviar animação
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                                
                                //Atacar o alvo
                                CombatRelations.playerAttackPlayer(s, target, preparingskill);
                                PlayerStruct.tempplayer[target].isFrozen = true;
                                PlayerStruct.tempplayer[target].movespeed -= 0.5;
                                PlayerStruct.tempplayer[target].Slowed = true;
                                PlayerStruct.tempplayer[target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                SendData.sendFrozen(Globals.Target_Player, target);
                                SendData.sendMoveSpeed(Globals.Target_Player, target);
                                SendData.sendActionMsg(target, lang.frozen, Globals.ColorWhite, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].X, PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                
                                //Resetar comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1,s);
                                return;
                            }
                        }
                        //Repetitivo
                        else
                        {
                            //Valor temporário
                            int temp_spell = PlayerRelations.getOpenTempSpell(PlayerStruct.tempplayer[s].target);
                            
                            //Enviar animação
                            SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                            //Reduzir dano
                            if (SkillStruct.skill[preparingskill].type == 20)
                            {
                                PlayerStruct.tempplayer[target].ReduceDamage = 50;
                            }

                            //Pareparar objeto de execução
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].active = true;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].attacker = s;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].spellnum = PlayerStruct.tempplayer[s].preparingskill;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].interval;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].repeats = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].repeats;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].anim = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].second_anim;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].area_range = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].is_line = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].is_line;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].is_heal = false;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].fast_buff = false;
                            
                            //Slow
                            if (SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].slow)
                            {
                                PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].target].movespeed -= 0.5;
                                SendData.sendMoveSpeed(Globals.Target_Player, PlayerStruct.tempplayer[s].target);
                            }

                            //Resetar valores comuns
                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(Globals.Target_Player,s);
                            return;
                        }
                    }
                    if (SkillStruct.skill[preparingskill].damage_type == 2)
                    {
                        //Tudo ok
                        //Enviar animações
                        SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                        
                        //Atacar alvo
                        CombatRelations.playerAttackPlayer(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);
                        
                        //Resetar valores comuns
                        PlayerStruct.tempplayer[s].preparingskill = 0;
                        PlayerStruct.tempplayer[s].preparingskillslot = 0;
                        PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(1,s);
                        return;
                    }
                    //Recuperação de mana
                    if (SkillStruct.skill[preparingskill].damage_type == 4)
                    {
                        //Tipo 1
                        if ((SkillStruct.skill[preparingskill].type) == 1)
                        {
                            //Dano de cura
                            int Damage = HealSpellDamage(s, preparingskill);

                            //Enviar animação
                            SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                            
                            //Recuperar energia
                            SpiritPlayer(s, Damage);
                            
                            //Resetar valores
                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(1, s);
                            return;
                        }
                    }
                    //Cura
                    if (SkillStruct.skill[preparingskill].damage_type == 3)
                    {
                        if ((SkillStruct.skill[preparingskill].type) == 1)
                        {
                            if (!(PlayerStruct.character[target, PlayerStruct.player[target].SelectedChar].Map == Map))
                            {
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                SendData.sendMsgToPlayer(s, lang.could_not_perform_magic, Globals.ColorRed, Globals.Msg_Type_Server);
                                return;
                            }

                            int temp_spell = PlayerRelations.getOpenTempSpell(target);
                            SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].active = true;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].attacker = s;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].spellnum = PlayerStruct.tempplayer[s].preparingskill;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].interval;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].repeats = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].repeats;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].anim = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].second_anim;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].area_range = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].is_line = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].is_line;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].is_heal = true;
                            PlayerStruct.ptempspell[PlayerStruct.tempplayer[s].target, temp_spell].fast_buff = true;

                            PlayerStruct.tempplayer[PlayerStruct.tempplayer[s].target].movespeed = Globals.FastMoveSpeed;
                            SendData.sendMoveSpeed(1, PlayerStruct.tempplayer[s].target);

                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            if (PlayerStruct.tempplayer[s].target != s)
                            {
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                            }
                            return;
                        }
                        if ((SkillStruct.skill[preparingskill].type) == 0)
                        {


                            int Damage = HealSpellDamage(s, PlayerStruct.tempplayer[s].preparingskill);
                            //Tudo ok
                            SendData.sendAnimation(Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                            HealPlayer(PlayerStruct.tempplayer[s].target, Damage);
                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(1, s);
                        }
                    }
                }
            }



            //NPC
            if (PlayerStruct.tempplayer[s].targettype == 2)
            {
                //Posição do alvo
                int targetx = NpcStruct.tempnpc[Map, target].X;
                int targety = NpcStruct.tempnpc[Map, target].Y;

                //Calcular distância
                n = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].tp_gain;
                DistanceX = playerx - targetx;
                DistanceY = playery - targety;

                if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                //Verificar se está no alcance
                if ((DistanceX <= n) && (DistanceY <= n))
                {
                    //Cooldown
                    PlayerStruct.skill[s, PlayerStruct.hotkey[s, preparingskillslot].num].cooldown = Loops.TickCount.ElapsedMilliseconds + (tp_cost * 500);
                    SendData.sendPlayerSkillCooldown(s, preparingskillslot, tp_cost * 500);
                    //Atualiza MP
                    PlayerStruct.tempplayer[s].Spirit -= SkillStruct.skill[preparingskill].mp_cost * PlayerStruct.skill[s, PlayerStruct.hotkey[s, PlayerStruct.tempplayer[s].preparingskillslot].num].level;
                    SendData.sendPlayerSpiritToMap(Map, s, PlayerStruct.tempplayer[s].Spirit);
                    if (PlayerStruct.tempplayer[s].Party > 0) { SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit); }

                    //Execução da magia
                    if (SkillStruct.skill[preparingskill].damage_type == 1)
                    {
                        if (SkillStruct.skill[preparingskill].repeats <= 1)
                        {
                            if ((SkillStruct.skill[preparingskill].type) == 0)
                            {
                                //Tudo ok
                                //Animação para o npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, preparingskill);

                                //Habilidade passiva do Iarumas
                                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 4)
                                {
                                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                                    {
                                        if ((PlayerStruct.skill[s, i].num == 47) && (PlayerStruct.skill[s, i].level > 0))
                                        {
                                            if (PlayerStruct.tempplayer[s].Sheathe < 3)
                                            {
                                                PlayerStruct.tempplayer[s].Sheathe += 1;
                                            }
                                            else
                                            {
                                                //Animação para o npc
                                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[47].animation_id);
                                                CombatRelations.playerAttackNpc(s, target, 47);
                                            }
                                            break;
                                        }
                                    }
                                }

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 6)
                            {
                                //Tudo ok
                                //Animação para o npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, target, preparingskill);

                                //Spell Buff slot
                                int spellbuff = SkillRelations.getOpenSpellBuff(s);

                                //Define dados do buff spell no npc
                                PlayerStruct.pspellbuff[s, spellbuff].active = true;
                                PlayerStruct.pspellbuff[s, spellbuff].type = 1; //DAMAGE
                                PlayerStruct.pspellbuff[s, spellbuff].value = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                                PlayerStruct.pspellbuff[s, spellbuff].timer = Loops.TickCount.ElapsedMilliseconds + Convert.ToInt32(SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].note.Split(',')[2]);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 7)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, target, preparingskill);

                                //Coloca o npc para dormir
                                NpcStruct.tempnpc[Map, target].Sleeping = true;
                                NpcStruct.tempnpc[Map, target].SleepTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[preparingskill].interval;
                                SendData.sendSleep(Map, targettype, target, 1);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 8)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Paraliza o npc
                                NpcStruct.tempnpc[Map, target].Stunned = true;
                                NpcStruct.tempnpc[Map, target].StunTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[preparingskill].interval;
                                SendData.sendStun(Map, targettype, target, 1);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 9)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, target, preparingskill);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 11)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Alcance real
                                int true_range = 0;

                                //Cálcula do alcance real
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (MovementRelations.canThrowNpc(Map, target, playerdir, i))
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
                                    MovementRelations.throwNpc(Map, PlayerStruct.tempplayer[s].target, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir, true_range);
                                }

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 15)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, target, preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].type) == 12)
                            {
                                //Tudo ok
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                int true_range = 0;
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (MovementRelations.canThrowPlayer(s, playerdir, i))
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
                                    MovementRelations.throwPlayer(s, playerdir, true_range);
                                }

                                PlayerStruct.tempplayer[s].Slowed = true;
                                PlayerStruct.tempplayer[s].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 800;
                                PlayerStruct.tempplayer[s].movespeed -= 0.5;
                                SendData.sendMoveSpeed(Globals.Target_Player, s);

                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 18)
                            {
                                //Tudo ok
                                SendData.sendAnimation(Map, Globals.Target_Player, s, SkillStruct.skill[preparingskill].animation_id);

                                //Váriaveis comuns
                                int x = NpcStruct.tempnpc[Map, target].X;
                                int y = NpcStruct.tempnpc[Map, target].Y; ;

                                //Análise de posição - Baixo
                                if (NpcStruct.tempnpc[Map, PlayerStruct.tempplayer[s].target].Dir == Globals.DirDown)
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
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                //Análise de posição - Cima
                                if (NpcStruct.tempnpc[Map, target].Dir == Globals.DirUp)
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
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                //Análise de posição - Esquerda
                                if (NpcStruct.tempnpc[Map, target].Dir == Globals.DirLeft)
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
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                //Análise de posição - Direita
                                if (NpcStruct.tempnpc[Map, target].Dir == Globals.DirRight)
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
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X = Convert.ToByte(x);
                                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y = Convert.ToByte(y);
                                    SendData.sendPlayerXY(s);
                                }

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 14)
                            {
                                //Valor real
                                int true_range = 0;

                                //Valores amarzenados
                                int old_x = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
                                int old_y = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;

                                //Cálculo do valor real
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (MovementRelations.canThrowPlayer(s, playerdir, i))
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
                                    for (int i = 0; i <= Globals.Player_Highs; i++)
                                    {
                                        if ((PlayerStruct.tempplayer[i].ingame == true) && (i != s))
                                        {
                                            if (Convert.ToInt32(PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map) == Map)
                                            {
                                                for (int p = 0; p <= true_range; p++)
                                                {
                                                    x = old_x;
                                                    y = old_y;

                                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                                    //Execução
                                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X == x) && (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y == y)) { SendData.sendAnimation(Map, Globals.Target_Player, i, 149); CombatRelations.playerAttackPlayer(s, i, preparingskill); }
                                                }
                                            }

                                        }
                                    }

                                    //Npc's
                                    for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                    {
                                        for (int p = 0; p <= true_range; p++)
                                        {
                                            x = old_x;
                                            y = old_y;

                                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirDown) { y += p; }
                                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirUp) { y -= p; }
                                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirRight) { x += p; }
                                            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir == Globals.DirLeft) { x -= p; }

                                            //Execução
                                            if ((NpcStruct.tempnpc[Map, i].X == x) && (NpcStruct.tempnpc[Map, i].Y == y)) { SendData.sendAnimation(Map, Globals.Target_Npc, i, 149); CombatRelations.playerAttackNpc(s, i, preparingskill); }
                                        }
                                    }
                                    MovementRelations.throwPlayer(s, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir, true_range);
                                }



                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(Map, Globals.Target_Player, s, SkillStruct.skill[preparingskill].animation_id);

                                //CombatRelations.playerAttackPlayer(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Deixa o jogador lento por 0.3 segs
                                PlayerStruct.tempplayer[s].Slowed = true;
                                PlayerStruct.tempplayer[s].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 300;
                                PlayerStruct.tempplayer[s].movespeed -= 0.5;
                                SendData.sendMoveSpeed(Globals.Target_Player, s);

                                //Reseta valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 13)
                            {
                                //Tudo ok
                                //Envia animação do npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, target, preparingskill);

                                //Deixa o npc alvo cego
                                NpcStruct.tempnpc[Map, PlayerStruct.tempplayer[s].target].Blind = true;
                                NpcStruct.tempnpc[Map, PlayerStruct.tempplayer[s].target].BlindTimer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[preparingskill].interval;

                                //Resete valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }

                            if ((SkillStruct.skill[preparingskill].type) == 1)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY == 0))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY == 0))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 2)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX == 0) && (DistanceY <= n))
                                    {
                                        //Se está no alcance, atacar
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX == 0) && (DistanceY <= n))
                                        {
                                            //Se encontrou, e está no alcance, atacar!
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 3)
                            {
                                //Verificar todos os npcs
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                    {
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            //Se está no alcance, atacar
                                            CombatRelations.playerAttackNpc(s, i, preparingskill);
                                        }
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n - 1) || (DistanceY <= n - 1))
                                        {
                                            if ((DistanceX <= n) && (DistanceY <= n))
                                            {
                                                //Se encontrou, e está no alcance, atacar!
                                                CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                            }
                                        }
                                    }
                                }

                                //Envia animação do npc
                                SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                                //Ataca o npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                            if ((SkillStruct.skill[preparingskill].type) == 4)
                            {
                                //Analisa todos os npcs do mapa
                                for (int i = 0; i <= MapStruct.tempmap[Map].NpcCount; i++)
                                {
                                    //Calcular distância
                                    n = SkillStruct.skill[preparingskill].range_effect;
                                    DistanceX = NpcStruct.tempnpc[Map, i].X - targetx;
                                    DistanceY = NpcStruct.tempnpc[Map, i].Y - targety;

                                    if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                    if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                    //Verificar se está no alcance
                                    if ((DistanceX <= n) && (DistanceY <= n))
                                    {
                                        CombatRelations.playerAttackNpc(s, i, preparingskill);
                                        NpcStruct.tempnpc[Map, i].isFrozen = true;
                                        NpcStruct.tempnpc[Map, i].movespeed -= 0.5;
                                        NpcStruct.tempnpc[Map, i].Slowed = true;
                                        NpcStruct.tempnpc[Map, i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                        SendData.sendFrozen(Globals.Target_Npc, i, Map);
                                        SendData.sendMoveSpeed(Globals.Target_Npc, i, Map);
                                        SendData.sendActionMsg(i, lang.frozen, Globals.ColorWhite, NpcStruct.tempnpc[Map, i].X, NpcStruct.tempnpc[Map, i].Y, Globals.Action_Msg_Scroll, 0, Map);
                                    }
                                }
                                for (int i = 0; i <= Globals.Player_Highs; i++)
                                {
                                    //O mapa é o mesmo do jogador? s diferente do atacante?
                                    if ((PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Map == Map) && (i != s) && (i != target))
                                    {
                                        //Calcular distância
                                        n = SkillStruct.skill[preparingskill].range_effect;
                                        DistanceX = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X - targetx;
                                        DistanceY = PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y - targety;

                                        if (DistanceX < 0) { DistanceX = DistanceX * -1; }
                                        if (DistanceY < 0) { DistanceY = DistanceY * -1; }

                                        //Verificar se está no alcance
                                        if ((DistanceX <= n) && (DistanceY <= n))
                                        {
                                            CombatRelations.playerAttackPlayer(s, i, preparingskill);
                                            PlayerStruct.tempplayer[i].isFrozen = true;
                                            PlayerStruct.tempplayer[i].movespeed -= 0.5;
                                            PlayerStruct.tempplayer[i].Slowed = true;
                                            PlayerStruct.tempplayer[i].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                            SendData.sendFrozen(Globals.Target_Player, i);
                                            SendData.sendMoveSpeed(Globals.Target_Player, i);
                                            SendData.sendActionMsg(i, lang.frozen, Globals.ColorWhite, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].X, PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Y, Globals.Action_Msg_Scroll, 0, Map);
                                        }
                                    }
                                }

                                //Enviar animação do npc
                                SendData.sendAnimation(Map, targettype, target, SkillStruct.skill[preparingskill].animation_id);

                                //Ataque ao npc alvo
                                CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);
                                NpcStruct.tempnpc[Map, target].isFrozen = true;
                                NpcStruct.tempnpc[Map, target].movespeed -= 0.5;
                                NpcStruct.tempnpc[Map, target].Slowed = true;
                                NpcStruct.tempnpc[Map, target].SlowTimer = Loops.TickCount.ElapsedMilliseconds + 4000;
                                SendData.sendFrozen(Globals.Target_Npc, target, Map);
                                SendData.sendMoveSpeed(Globals.Target_Npc, target, Map);
                                SendData.sendActionMsg(target, lang.frozen, Globals.ColorWhite, NpcStruct.tempnpc[Map, target].X, NpcStruct.tempnpc[Map, target].Y, Globals.Action_Msg_Scroll, 0, Map);

                                //Resetar valores comuns
                                PlayerStruct.tempplayer[s].preparingskill = 0;
                                PlayerStruct.tempplayer[s].preparingskillslot = 0;
                                PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                                SendData.sendMoveSpeed(1, s);
                                return;
                            }
                        }
                        else
                        {
                            //Magia temporária
                            int temp_spell = NpcStruct.getOpenNTempSpell(PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target);

                            //Enviar animação
                            SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, 2, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);

                            //Reduzir dano
                            if (SkillStruct.skill[preparingskill].type == 20)
                            {
                                NpcStruct.tempnpc[Map, target].ReduceDamage = 50;
                            }

                            //Armazenar informações da magia
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].active = true;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].attacker = s;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].spellnum = PlayerStruct.tempplayer[s].preparingskill;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].timer = Loops.TickCount.ElapsedMilliseconds + SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].interval;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].repeats = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].repeats;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].anim = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].second_anim;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].area_range = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].range_effect;
                            NpcStruct.ntempspell[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target, temp_spell].is_line = SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].is_line;

                            //Lentidão no npc
                            if (SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].slow)
                            {
                                NpcStruct.tempnpc[PlayerStruct.character[s, Convert.ToInt32(PlayerStruct.player[s].SelectedChar)].Map, PlayerStruct.tempplayer[s].target].movespeed -= 0.5;
                                SendData.sendMoveSpeed(2, PlayerStruct.tempplayer[s].target, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                            }


                            //Resetar valores comuns
                            PlayerStruct.tempplayer[s].preparingskill = 0;
                            PlayerStruct.tempplayer[s].preparingskillslot = 0;
                            PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                            SendData.sendMoveSpeed(1, s);
                            return;
                        }
                    }
                    if (SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].damage_type == 2)
                    {
                        //Tudo ok
                        //Enviar animação
                        SendData.sendAnimation(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, PlayerStruct.tempplayer[s].targettype, PlayerStruct.tempplayer[s].target, SkillStruct.skill[PlayerStruct.tempplayer[s].preparingskill].animation_id);
                        
                        //Atacar o npc alvo
                        CombatRelations.playerAttackNpc(s, PlayerStruct.tempplayer[s].target, PlayerStruct.tempplayer[s].preparingskill);
                        
                        //Resetar valores comuns
                        PlayerStruct.tempplayer[s].preparingskill = 0;
                        PlayerStruct.tempplayer[s].preparingskillslot = 0;
                        PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                        SendData.sendMoveSpeed(1,s);
                        return;
                    }
                }
                //Não está! LOL
                else
                {
                    //Sem alcance
                    SendData.sendMsgToPlayer(s, lang.target_distance_too_long, Globals.ColorRed, Globals.Msg_Type_Server);
                    
                    //Resetar valores comuns
                    PlayerStruct.tempplayer[s].preparingskill = 0;
                    PlayerStruct.tempplayer[s].preparingskillslot = 0;
                    PlayerStruct.tempplayer[s].movespeed = Globals.NormalMoveSpeed;
                    SendData.sendMoveSpeed(1,s);
                    return;
                }
            }
        }
    }
}
