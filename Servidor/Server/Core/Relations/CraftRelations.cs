using System;
using System.Reflection;

namespace __Forjerum
{
    class CraftRelations
    {
        //*********************************************************************************************
        // getRefinCraft / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getRefinCraft(int s, int type)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type));
            }

            //CÓDIGO
            //Váriaveis
            int RefinChance = DropRelations.getRefinDrop();
            int Refin = RefinChance;

            //Retorna o valor de Refin
            return Refin;
        }
        //*********************************************************************************************
        // CraftHasItem
        // Retorna se existe determinado item no craft
        //*********************************************************************************************
        public static int craftHasItem(int s, int type, int num)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type, num) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, type, num));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_Craft; i++)
            {
                if ((PlayerStruct.craft[s, i].num == num) && (PlayerStruct.craft[s, i].type == type))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
