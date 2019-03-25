using System;
using System.Reflection;

namespace __Forjerum
{
    class EquipmentRelations
    {
        //*********************************************************************************************
        // getPlayerHelmet
        // Retorna o equipamento superior do jogador
        //*********************************************************************************************
        public static int getPlayerHelmet(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Helmet = Convert.ToInt32(splited[0].Split(';')[0]);
            return Helmet;
        }
        //*********************************************************************************************
        // getPlayerArmor
        // Retorna a armadura do jogador
        //*********************************************************************************************
        public static int getPlayerArmor(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');
            int Armor = Convert.ToInt32(splited[1].Split(';')[0]);
            return Armor;
        }
        //*********************************************************************************************
        // getPlayerWeapon
        // Retorna a arma do jogador
        //*********************************************************************************************
        public static int getPlayerWeapon(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Weapon = Convert.ToInt32(splited[2].Split(';')[0]);
            return Weapon;
        }
        //*********************************************************************************************
        // getPlayerShield
        // Retorna o escudo do jogador
        //*********************************************************************************************
        public static int getPlayerShield(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Shield = Convert.ToInt32(splited[3].Split(';')[0]);
            return Shield;
        }
        //*********************************************************************************************
        // getPlayerHelmetRefin
        //*********************************************************************************************
        public static int getPlayerHelmetRefin(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Helmet = Convert.ToInt32(splited[0].Split(';')[1]);
            return Helmet;
        }
        //*********************************************************************************************
        // getPlayerArmorRefin
        //*********************************************************************************************
        public static int getPlayerArmorRefin(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');
            int Armor = Convert.ToInt32(splited[1].Split(';')[1]);
            return Armor;
        }
        //*********************************************************************************************
        // getPlayerWeaponRefin
        //*********************************************************************************************
        public static int getPlayerWeaponRefin(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Weapon = Convert.ToInt32(splited[2].Split(';')[1]);
            return Weapon;
        }
        //*********************************************************************************************
        // getPlayerShieldRefin
        //*********************************************************************************************
        public static int getPlayerShieldRefin(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            string[] splited = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment.Split(',');

            int Shield = Convert.ToInt32(splited[3].Split(';')[1]);
            return Shield;
        }
    }
}
