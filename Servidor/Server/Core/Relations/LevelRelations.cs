using System;
using System.Reflection;

namespace __Forjerum
{
    class LevelRelations
    {
        //*********************************************************************************************
        // getPetExpToNextLevel
        // Cálculo da exp necessária para subir de nível para o mascote
        //*********************************************************************************************
        public static int getPetExpToNextLevel(int s, int level)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, level) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, level));
            }

            //CÓDIGO
            int exp = 0;
            if (level < 10)
            {
                double exptonextlevel = (level * 100) * 1.2;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 10) && (level < 20))
            {
                double exptonextlevel = (level * 300) * 1.2;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 20) && (level < 30))
            {
                double exptonextlevel = (level * 600) * 1.4;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 30) && (level < 40))
            {
                double exptonextlevel = (level * 900) * 1.5;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 40) && (level < 60))
            {
                double exptonextlevel = (level * 1700) * 1.6;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 60) && (level < 70))
            {
                double exptonextlevel = (level * 2800) * 1.7;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 70) && (level < 80))
            {
                double exptonextlevel = (level * 4000) * 1.8;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 80) && (level < 90))
            {
                double exptonextlevel = (level * 7000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 90) && (level < 100))
            {
                double exptonextlevel = (level * 13000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 100))
            {
                double exptonextlevel = (level * 29000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            return exp;
        }
        //*********************************************************************************************
        // getExpToNextLevel
        // Cálculo da exp necessária para o jogador subir de nível
        //*********************************************************************************************
        public static int getExpToNextLevel(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int level = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level;
            int exp = 0;
            if (level < 10)
            {
                double exptonextlevel = (level * 100) * 1.2;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 10) && (level < 20))
            {
                double exptonextlevel = (level * 300) * 1.2;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 20) && (level < 30))
            {
                double exptonextlevel = (level * 600) * 1.4;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 30) && (level < 40))
            {
                double exptonextlevel = (level * 900) * 1.5;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 40) && (level < 60))
            {
                double exptonextlevel = (level * 1700) * 1.6;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 60) && (level < 70))
            {
                double exptonextlevel = (level * 2800) * 1.7;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 70) && (level < 80))
            {
                double exptonextlevel = (level * 4000) * 1.8;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 80) && (level < 90))
            {
                double exptonextlevel = (level * 7000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 90) && (level < 100))
            {
                double exptonextlevel = (level * 13000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            if ((level >= 100))
            {
                double exptonextlevel = (level * 29000) * 1.9;
                exp = Convert.ToInt32(exptonextlevel);
            }
            return exp;
        }
        //*********************************************************************************************
        // getProfExpToNextLevel
        // Cálculo da exp necessária para a profissão do jogador subir de nível
        //*********************************************************************************************
        public static int getpProfExpToNextLevel(int s, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type));
            }

            //CÓDIGO
            int level = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[type];
            double exptonextlevel = (level * 10) * 1.2;
            int exp = Convert.ToInt32(exptonextlevel);
            return exp;
        }
    }
}
