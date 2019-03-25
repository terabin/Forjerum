using System;
using System.Reflection;

namespace __Forjerum
{
    class QuestRelations
    {
        //*********************************************************************************************
        // getActualPlayerQuestPerGiver
        // Retorna o número de quests que o jogador tem por npc
        //*********************************************************************************************
        public static int getActualPlayerQuestPerGiver(int s, int questgiver)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, questgiver) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, questgiver));
            }

            //CÓDIGO
            int quest = 1;

            for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
            {
                if (PlayerStruct.queststatus[s, questgiver, q].status == 2)
                {
                    quest += 1;
                }
            }

            return quest;
        }
        //*********************************************************************************************
        // getPlayerQuestGiversCount
        // Número de npc's que deram quest ao jogador
        //*********************************************************************************************
        public static int getPlayerQuestGiversCount(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int count = 0;

            for (int g = 1; g <= Globals.MaxQuestGivers; g++)
            {
                if (PlayerStruct.queststatus[s, g, 1].status != 0)
                {
                    count += 1;
                }
            }

            return count;
        }
        //*********************************************************************************************
        // getPlayerQuestsCount
        // Número total de quests
        //*********************************************************************************************
        public static int getPlayerQuestsCount(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int count = 0;

            for (int g = 1; g < Globals.MaxQuestGivers; g++)
            {
                for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                {
                    if (PlayerStruct.queststatus[s, g, q].status != 0)
                    {
                        count += 1;
                    }
                }
            }

            return count;
        }
        //*********************************************************************************************
        // IsQuestGiverRepeatable
        // Retorna se a missão pode ser repetida
        //*********************************************************************************************
        public static bool IsQuestGiverRepeatable(int questgiver)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, questgiver) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, questgiver));
            }

            //CÓDIGO
            if (questgiver == 7)
            {
                return true;
            }
            return false;
        }
    }
}
