using System;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a guildas.
    // GuildStruct.cs
    //*********************************************************************************************
    class GuildStruct
    {
        //*********************************************************************************************
        // ESTRUTURA DAS GUILDAS
        //*********************************************************************************************
        public static Guild[] guild = new Guild[Globals.Max_Guilds];

        public struct Guild
        {
            public string name;
            public int level;
            public int exp;
            public int shield;
            public int hue;
            public int leader;
            public string[] memberlist;
            public string[] membersprite;
            public int[] memberhue;
            public int[] membersprite_s;
        }
        //*********************************************************************************************
        // InitializeGuildArrays / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void initializeGuildArrays()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                guild[i].memberlist = new string[Globals.Max_Guild_Members];
                guild[i].membersprite = new string[Globals.Max_Guild_Members];
                guild[i].memberhue = new int[Globals.Max_Guild_Members];
                guild[i].membersprite_s = new int[Globals.Max_Guild_Members];
            }
        }
        //*********************************************************************************************
        // getOpenGuildSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenGuildSlot()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int count = 0;
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                if (String.IsNullOrEmpty(guild[i].name))
                {
                    return i;
                }
            }

            return count;
        }
        //*********************************************************************************************
        // getMember_Count / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna número de membros em determinada guilda.
        //*********************************************************************************************
        public static int getMember_Count(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            int count = 0;
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                if (!String.IsNullOrEmpty(guild[guildnum].memberlist[i]))
                {
                    count += 1;
                }
            }

            return count;
        }
        //*********************************************************************************************
        // getOpenMemberSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getOpenMemberSlot(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            int refuse = 0;

            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                if (String.IsNullOrEmpty(guild[guildnum].memberlist[i]))
                {
                    return i;
                }
            }

            return refuse;
        }
    }
}