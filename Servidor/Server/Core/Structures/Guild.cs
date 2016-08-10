using System;
using System.Reflection;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a guildas.
    // GStruct.cs
    //*********************************************************************************************
    class GStruct
    {
        //*********************************************************************************************
        // ESTRUTURA DAS GUILDAS
        //*********************************************************************************************
        public static GStruct.Guild[] guild = new GStruct.Guild[Globals.Max_Guilds];

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
            public int[] membersprite_index;
        }
        //*********************************************************************************************
        // InitializeGuildArrays / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void InitializeGuildArrays()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                GStruct.guild[i].memberlist = new string[Globals.Max_Guild_Members];
                GStruct.guild[i].membersprite = new string[Globals.Max_Guild_Members];
                GStruct.guild[i].memberhue = new int[Globals.Max_Guild_Members];
                GStruct.guild[i].membersprite_index = new int[Globals.Max_Guild_Members];
            }
        }
        //*********************************************************************************************
        // GetOpenGuildSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetOpenGuildSlot()
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            int count = 0;
            for (int i = 1; i < Globals.Max_Guilds; i++)
            {
                if (String.IsNullOrEmpty(GStruct.guild[i].name))
                {
                    return i;
                }
            }

            return count;
        }
        //*********************************************************************************************
        // GetMember_Count / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna número de membros em determinada guilda.
        //*********************************************************************************************
        public static int GetMember_Count(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            int count = 0;
            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                if (!String.IsNullOrEmpty(GStruct.guild[guildnum].memberlist[i]))
                {
                    count += 1;
                }
            }

            return count;
        }
        //*********************************************************************************************
        // GetOpenMemberSlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int GetOpenMemberSlot(int guildnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, guildnum));
            }

            //CÓDIGO
            int refuse = 0;

            for (int i = 1; i < Globals.Max_Guild_Members; i++)
            {
                if (String.IsNullOrEmpty(GStruct.guild[guildnum].memberlist[i]))
                {
                    return i;
                }
            }

            return refuse;
        }
    }
}