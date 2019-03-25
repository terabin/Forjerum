using System;
using System.Reflection;

namespace __Forjerum
{
    class PlayerRelations : Languages.LStruct
    {
        //*********************************************************************************************
        // getPlayerMaxSpirit
        // Retorna enegia máxima de determinado jogador
        //*********************************************************************************************
        public static int getPlayerMaxSpirit(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int LevelVital = 0;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 1) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 30; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 2) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 22; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 3) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 60; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 4) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 18; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 14; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 40; }
            double FireVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire) * 0.5;
            double EarthVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 0.8;
            double WaterVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water) * 1.2;
            double WindVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 1.3;
            double DarkVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark) * 2;
            double LightVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light) * 1.5;
            double DVital = FireVital + EarthVital + WaterVital + WindVital + DarkVital + LightVital;
            int vital = Convert.ToInt32(DVital) + LevelVital;
            if (getExtraSpirit(s) > 0) { vital += (vital / 100) * getExtraSpirit(s); }
            if (PlayerStruct.tempplayer[s].SORE) { vital = vital / 2; }
            return vital;
        }
        //*********************************************************************************************
        // getPlayerMaxVitality
        // Retorna vida máxima de determinado jogador
        //*********************************************************************************************
        public static int getPlayerMaxVitality(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int LevelVital = 0;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 1) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 52; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 2) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 63; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 3) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 34; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 4) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 40; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 76; }
            else if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6) { LevelVital = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level * 32; }
            //int LevelVital = character[s, (PlayerStruct.player[s].SelectedChar].Level * 75;
            double FireVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire) * 2.5;
            double EarthVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 4;
            double WaterVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water) * 2.3;
            double WindVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 2.2;
            double DarkVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark) * 1.8;
            double LightVital = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light) * 1.5;
            double DVital = FireVital + EarthVital + WaterVital + WindVital + DarkVital + LightVital;
            int vital = Convert.ToInt32(DVital) + LevelVital;
            if (getExtraVitality(s) > 0) { vital += (vital / 100) * getExtraVitality(s); }
            if (PlayerStruct.tempplayer[s].SORE) { vital = vital / 2; }
            return vital;
        }
        //*********************************************************************************************
        // getExtraVitality
        // Vida que deve ser adicionada baseado em algum status, magia ou item especial
        //*********************************************************************************************
        public static int getExtraVitality(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int vital = 0;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 35) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        vital = 40;
                        break;
                    }
                }
            }
            return vital;
        }
        //*********************************************************************************************
        // getExtraSpirit
        // Energia que deve ser adicionada baseada em algum status, item ou magia especial
        //*********************************************************************************************
        public static int getExtraSpirit(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int vital = 0;
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 1)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 46) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        vital = 10;
                        break;
                    }
                }
            }
            return vital;
        }
        //*********************************************************************************************
        // getPlayerVitalityRegen
        //*********************************************************************************************
        public static int getPlayerVitalityRegen(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double LightRegen = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light) * 0.6;
            int vital = 1 + Convert.ToInt32(LightRegen); //per second
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 2)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 52) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        vital += ((getPlayerMaxVitality(s) / 100) * PlayerStruct.skill[s, i].level);
                        break;
                    }
                }
            }
            return vital;
        }
        //*********************************************************************************************
        // getPlayerSpiritRegen
        //*********************************************************************************************
        public static int getPlayerSpiritRegen(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double DarkRegen = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark) * 0.3;
            int vital = 1 + Convert.ToInt32(DarkRegen); //per second
            return vital;
        }
        //*********************************************************************************************
        // getPlayerCritical
        //*********************************************************************************************
        public static int getPlayerCritical(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armorcrit = 0.0;
            double weaponcrit = 0.0;
            double shieldcrit = 0.0;
            double helmetcrit = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armorcrit = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 7].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 7].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponcrit = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 7].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 7].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldcrit = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 7].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 7].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetcrit = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 7].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 7].value / 100) * (level * 7));
            }

            double totalitemcrit = armorcrit + weaponcrit + shieldcrit + helmetcrit;

            double watercrit = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water) * 0.2;
            double dtotalcrit = totalitemcrit + watercrit;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 4)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 48) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        dtotalcrit += (PlayerStruct.skill[s, i].level * 1.5);
                        break;
                    }
                }
            }

            if (PlayerStruct.tempplayer[s].SORE) { dtotalcrit = dtotalcrit / 2; }

            int totalcrit = Convert.ToInt32(dtotalcrit);

            return totalcrit;
        }
        //*********************************************************************************************
        // getPlayerParry
        // Chance de bloqueio
        //*********************************************************************************************
        public static int getPlayerParry(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armorparry = 0.0;
            double weaponparry = 0.0;
            double shieldparry = 0.0;
            double helmetparry = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armorparry = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 6].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 6].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponparry = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 6].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 6].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldparry = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 6].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 6].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetparry = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 6].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 6].value / 100) * (level * 7));
            }

            double totalitemparry = armorparry + weaponparry + shieldparry + helmetparry;

            double windparry = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 0.3;
            double dtotalparry = totalitemparry + windparry;
            if (PlayerStruct.tempplayer[s].SORE) { dtotalparry = dtotalparry / 2; }
            int totalparry = Convert.ToInt32(dtotalparry);

            return totalparry;
        }
        //*********************************************************************************************
        // getPlayerDefense
        //*********************************************************************************************
        public static int getPlayerDefense(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armordef = 0.0;
            double weapondef = 0.0;
            double shielddef = 0.0;
            double helmetdef = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armordef = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 3].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 3].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weapondef = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 3].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 3].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shielddef = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 3].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 3].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetdef = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 3].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 3].value / 100) * (level * 7));
            }

            double totalitemdef = armordef + weapondef + shielddef + helmetdef;

            double earthdefense = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 0.05;
            double dtotaldefense = totalitemdef + earthdefense;
            if (PlayerStruct.tempplayer[s].SORE) { dtotaldefense = dtotaldefense / 2; }
            int totaldefense = Convert.ToInt32(dtotaldefense);

            return totaldefense;
        }
        //*********************************************************************************************
        // getPlayerMinAttack
        //*********************************************************************************************
        public static int getPlayerMinAttack(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armoratk = 0.0;
            double weaponatk = 0.0;
            double shieldatk = 0.0;
            double helmetatk = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armoratk = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 0].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 0].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponatk = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 0].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 0].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 0].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 0].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 0].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 0].value / 100) * (level * 7));
            }

            double totalitematk = armoratk + weaponatk + shieldatk + helmetatk;

            double earthatk = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 0.7;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 39) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 0.7;
                        break;
                    }
                }
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 35) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 0.7;
                        break;
                    }
                }
            }

            double dtotalatk = totalitematk + earthatk;
            if (PlayerStruct.tempplayer[s].SORE) { dtotalatk = dtotalatk / 2; }
            int totalatk = Convert.ToInt32(dtotalatk);

            return totalatk;
        }
        //*********************************************************************************************
        // getPlayerMinMagic
        //*********************************************************************************************
        public static int getPlayerMinMagic(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armoratk = 0.0;
            double weaponatk = 0.0;
            double shieldatk = 0.0;
            double helmetatk = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armoratk = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 1].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 1].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponatk = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 1].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 1].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 1].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 1].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 1].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 1].value / 100) * (level * 7));
            }

            double totalitematk = armoratk + weaponatk + shieldatk + helmetatk;

            double earthatk = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark) * 0.6;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 39) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 0.6;
                        break;
                    }
                }
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 35) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 0.6;
                        break;
                    }
                }
            }

            double dtotalatk = totalitematk + earthatk;


            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 1)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 46) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        dtotalatk += ((dtotalatk / 100) * (5 * PlayerStruct.skill[s, i].level));
                        break;
                    }
                }
            }

            if (PlayerStruct.tempplayer[s].SORE) { dtotalatk = dtotalatk / 2; }
            int totalatk = Convert.ToInt32(dtotalatk);

            return totalatk;
        }
        //*********************************************************************************************
        // getPlayerMaxMagic
        //*********************************************************************************************
        public static int getPlayerMaxMagic(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armoratk = 0.0;
            double weaponatk = 0.0;
            double shieldatk = 0.0;
            double helmetatk = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armoratk = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 4].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 4].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponatk = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 4].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 4].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 4].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 4].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 4].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 4].value / 100) * (level * 7));
            }

            double totalitematk = armoratk + weaponatk + shieldatk + helmetatk;

            double earthatk = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark) * 1.5;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 39) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 1.5;
                        break;
                    }
                }
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 35) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        earthatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 1.5;
                        break;
                    }
                }
            }

            double dtotalatk = totalitematk + earthatk;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 1)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 46) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        dtotalatk += ((dtotalatk / 100) * (5 * PlayerStruct.skill[s, i].level));
                        break;
                    }
                }
            }

            if (PlayerStruct.tempplayer[s].SORE) { dtotalatk = dtotalatk / 2; }
            int totalatk = Convert.ToInt32(dtotalatk);
            return totalatk;
        }
        //*********************************************************************************************
        // getPlayerMagicDef / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getPlayerMagicDef(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armoratk = 0.0;
            double weaponatk = 0.0;
            double shieldatk = 0.0;
            double helmetatk = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armoratk = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 5].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 5].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponatk = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 5].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 5].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 5].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 5].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 5].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 5].value / 100) * (level * 7));
            }

            double totalitematk = armoratk + weaponatk + shieldatk + helmetatk;

            double earthatk = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light) * 0.05;
            double dtotalatk = totalitematk + earthatk;
            if (PlayerStruct.tempplayer[s].SORE) { dtotalatk = dtotalatk / 2; }
            int totalatk = Convert.ToInt32(dtotalatk);

            return totalatk;
        }
        //*********************************************************************************************
        // getPlayerMaxAttack / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getPlayerMaxAttack(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            double armoratk = 0.0;
            double weaponatk = 0.0;
            double shieldatk = 0.0;
            double helmetatk = 0.0;
            int level = 0;

            if (EquipmentRelations.getPlayerArmor(s) != 0)
            {
                level = EquipmentRelations.getPlayerArmorRefin(s);
                armoratk = ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 2].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerArmor(s), 2].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerWeapon(s) != 0)
            {
                level = EquipmentRelations.getPlayerWeaponRefin(s);
                weaponatk = WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 2].value + ((WeaponStruct.weaponparams[EquipmentRelations.getPlayerWeapon(s), 2].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerShield(s) != 0)
            {
                level = EquipmentRelations.getPlayerShieldRefin(s);
                shieldatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 2].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerShield(s), 2].value / 100) * (level * 7));
            }
            if (EquipmentRelations.getPlayerHelmet(s) != 0)
            {
                level = EquipmentRelations.getPlayerHelmetRefin(s);
                helmetatk = ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 2].value + ((ArmorStruct.armorparams[EquipmentRelations.getPlayerHelmet(s), 2].value / 100) * (level * 7));
            }

            double totalitematk = armoratk + weaponatk + shieldatk + helmetatk;

            double fireatk = Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire) * 1.8;

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 6)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 39) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        fireatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind) * 1.8;
                        break;
                    }
                }
            }

            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId == 5)
            {
                for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                {
                    if ((PlayerStruct.skill[s, i].num == 35) && (PlayerStruct.skill[s, i].level > 0))
                    {
                        fireatk += Convert.ToDouble(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth) * 1.8;
                        break;
                    }
                }
            }

            double dtotalatk = totalitematk + fireatk;
            if (PlayerStruct.tempplayer[s].SORE) { dtotalatk = dtotalatk / 2; }
            int totalatk = Convert.ToInt32(dtotalatk);

            return totalatk;
        }
        //*********************************************************************************************
        // GivePlayerGold / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Entrega determinada quantidade de ouro para determinado jogador
        //*********************************************************************************************
        public static void givePlayerGold(int s, int gold)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, gold) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold += gold;
            SendData.sendPlayerG(s);
        }
        public static int getPlayerOriunklatex(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                string item = PlayerStruct.invslot[s, i].item;
                string[] splititem = item.Split(',');

                int itemNum = Convert.ToInt32(splititem[1]);
                int itemValue = Convert.ToInt32(splititem[2]);
                if ((itemNum == 68) && (itemValue > 0)) { return i; }
            }

            return 0;
        }
        //*********************************************************************************************
        // getPlayerExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Entrega determinada quantidade de exp para determinado jogador
        //*********************************************************************************************
        public static void givePlayerExp(int s, int exp)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, exp) != null)
            {
                return;
            }

            //CÓDIGO
            int PlayerX = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X;
            int PlayerY = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y;
            int Map = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp += exp;

            string equipment = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment;
            string[] equipdata = equipment.Split(',');
            string[] petdata = equipdata[4].Split(';');

            int petnum = Convert.ToInt32(petdata[0]);
            int petlvl = Convert.ToInt32(petdata[1]);
            int petexp = Convert.ToInt32(petdata[2]);

            if (petnum > 0)
            {
                petexp += exp;

                if (petexp >= LevelRelations.getPetExpToNextLevel(s, petlvl))
                {
                    petexp -= LevelRelations.getPetExpToNextLevel(s, petlvl);
                    petlvl += 1;
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment = equipdata[0] + "," + equipdata[1] + "," + equipdata[2] + "," + equipdata[3] + "," + petnum + ";" + petlvl + ";" + petexp;
                    SendData.sendActionMsg(s, lang.pet_evolve, 3, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, Map);
                    SendData.sendPlayerEquipmentTo(s, s);
                }
                else
                {
                    //Enviar nova exp
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment = equipdata[0] + "," + equipdata[1] + "," + equipdata[2] + "," + equipdata[3] + "," + petnum + ";" + petlvl + ";" + petexp;
                    SendData.sendPlayerEquipmentTo(s, s);
                }
            }

            //Verificamos se ele subiu de nível
            if ((PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp >= LevelRelations.getExpToNextLevel(s)) && (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level < 99))
            {
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp -= LevelRelations.getExpToNextLevel(s);
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level += 1;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points += 5;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints += 1;
                SendData.sendActionMsg(s, lang.level_up, 3, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X, PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y, 1, 0, Map);
                SendData.sendAnimation(Map, 1, s, 109);
                SendData.sendPlayerExp(s);
                SendData.sendPlayerLevel(s, s);
                SendData.sendPlayerSkillPoints(s);
                SendData.sendPlayerAtrTo(s);
            }
            else
            {
                //GetExpToNextLevel
                SendData.sendPlayerExp(s);
                //Enviamos uma animação bonitinha de exp :D
                SendData.sendActionMsg(s, "+" + exp + lang.exp, 0, PlayerX, PlayerY, 1, 0, Map);
            }
        }
        //*********************************************************************************************
        // IsPlayerPremmy
        // Retorna se determinado jogador é assinante
        //*********************************************************************************************
        public static bool isPlayerPremmy(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            DateTime myDate = DateTime.Parse(PlayerStruct.player[s].Premmy);
            int result = DateTime.Compare(myDate, DateTime.Now);

            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //*********************************************************************************************
        // IsPlayerBanned
        // Retorna se o jogador está banido
        //*********************************************************************************************
        public static bool isPlayerBanned(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            DateTime myDate = DateTime.Parse(PlayerStruct.player[s].Banned);
            int result = DateTime.Compare(myDate, DateTime.Now);

            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //*********************************************************************************************
        // PlayerRegen / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void playerRegen(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].isDead) { return; }
            if (PlayerStruct.tempplayer[s].Vitality < getPlayerMaxVitality(s))
            {
                //Regen por segundo
                PlayerStruct.tempplayer[s].Vitality += getPlayerVitalityRegen(s);
                //Vida atual ficou maior que a máxima?
                if (PlayerStruct.tempplayer[s].Vitality > getPlayerMaxVitality(s))
                {
                    PlayerStruct.tempplayer[s].Vitality = getPlayerMaxVitality(s);
                }
                //Envia vida recuperada
                SendData.sendPlayerVitalityToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Vitality);
                //Se estiver em grupo atualiza para o grupo também
                if (PlayerStruct.tempplayer[s].Party > 0)
                {
                    SendData.sendPlayerVitalityToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Vitality);
                }
            }
            if (PlayerStruct.tempplayer[s].Spirit < getPlayerMaxSpirit(s))
            {
                //Regen por segundo
                PlayerStruct.tempplayer[s].Spirit += getPlayerSpiritRegen(s);
                //Mana atual ficou maior que a máxima?
                if (PlayerStruct.tempplayer[s].Spirit > getPlayerMaxSpirit(s))
                {
                    PlayerStruct.tempplayer[s].Spirit = getPlayerMaxSpirit(s);
                }
                //Envia vida recuperada
                SendData.sendPlayerSpiritToMap(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map, s, PlayerStruct.tempplayer[s].Spirit);
                //Se estiver em grupo atualiza para o grupo também
                if (PlayerStruct.tempplayer[s].Party > 0)
                {
                    SendData.sendPlayerSpiritToParty(PlayerStruct.tempplayer[s].Party, s, PlayerStruct.tempplayer[s].Spirit);
                }
            }

            PlayerStruct.tempplayer[s].RegenTimer = Loops.TickCount.ElapsedMilliseconds + 1000;
        }
        //*********************************************************************************************
        // PlayerDeath / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void playerDeath(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.tempplayer[s].Vitality = 1;
            PlayerStruct.tempplayer[s].Spirit = 1;

        }
        //*********************************************************************************************
        // PlayerIsSore / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool playerIsSore(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty > Loops.TickCount.ElapsedMilliseconds) { return true; } else { PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty = 0; return false; }
        }
        //*********************************************************************************************
        // getOpenPTempSpell / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenTempSpell(int id)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, id) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, id));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxPTempSpells; i++)
            {
                if (PlayerStruct.ptempspell[id, i].active == false)
                {
                    return i;
                }
            }

            return 0;
        }
        //*********************************************************************************************
        // getMinerLevel / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getMinerLevel(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int exp = 0;//character[s, (PlayerStruct.player[s].SelectedChar].Miner;

            int level = (exp / 100);

            return level;
        }
        //*********************************************************************************************
        // getSkillOpenSlot
        // Retorna slot de skill livre para determinado jogador
        //*********************************************************************************************
        public static int getSkillOpenSlot(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 9; i < Globals.MaxPlayer_Skills; i++)
            {
                if (PlayerStruct.skill[s, i].num == 0) { return i; }
            }

            return 0;
        }

        //*********************************************************************************************
        // getPlayerElement / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getPlayerElement(int s, int element)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, element) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, element));
            }

            //CÓDIGO
            int quantity = 0;

            if (element == 1)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
            }

            if (element == 2)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
            }
            if (element == 3)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
            }
            if (element == 4)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
            }
            if (element == 5)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
            }
            if (element == 6)
            {
                return Convert.ToInt32(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
            }

            return quantity;
        }
        //*********************************************************************************************
        // ResetPlayerStatus
        // Reinicia os status de determinado jogador (OBSOLETO)
        //*********************************************************************************************
        public static void resetPlayerStatus(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            int totalpoints = 0;

            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth - 1;
            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind - 1;
            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark - 1;
            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light - 1;
            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water - 1;
            totalpoints += PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire - 1;

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth = 1;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind = 1;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark = 1;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light = 1;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water = 1;
            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire = 1;

            PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points += totalpoints;
            SendData.sendPlayerAtrToMap(s);
            SendData.sendMsgToPlayer(s, lang.success_atributte_reset, Globals.ColorGreen, Globals.Msg_Type_Server);
        }
        //*********************************************************************************************
        // PlayerAddPremmy
        // Adiciona tempo de assinatura
        //*********************************************************************************************
        public static void playerAddPremmy(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            //return;
            DateTime myDate = DateTime.Parse(PlayerStruct.player[s].Premmy);
            myDate = myDate.AddDays(30);
            PlayerStruct.player[s].Premmy = myDate.ToString();
            Console.WriteLine(PlayerStruct.player[s].Premmy);
            Database.Accounts.saveAccount(s);
        }
        //*********************************************************************************************
        // getPlayerProf
        // Retorna profissão do jogador
        //*********************************************************************************************
        public static int getPlayerProf(int s, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type));
            }

            //CÓDIGO
            int prof = 0;

            for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
            {
                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[i] == type)
                {
                    prof = i;
                    break;
                }
            }

            return prof;
        }

        //*********************************************************************************************
        // getFreeCraft
        // Retorna um slot no craft que esteja livre
        //*********************************************************************************************
        public static int getFreeCraft(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int finals = -1;

            //Checa o slot que possúi o jogador
            for (int i = 1; i < Globals.Max_Craft; i++)
            {
                if ((PlayerStruct.craft[s, i].num == 0))
                {
                    finals = i;
                    break;
                }
            }

            return finals;
        }
    }
}
