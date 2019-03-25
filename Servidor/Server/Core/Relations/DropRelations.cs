using System;
using System.Reflection;

namespace __Forjerum
{
    class DropRelations
    {
        //*********************************************************************************************
        // getRefinDrop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static int getRefinDrop()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            //Váriaveis
            int RefinChance = Globals.Rand(1, 100);
            int Refin = 0;


            //Verificação e definição do nível de Refin
            if ((RefinChance <= 30) && (RefinChance >= 16))// 30% Refin 1
            {
                Refin = 1;
            }
            else if ((RefinChance <= 51) && (RefinChance >= 31)) // 20% Refin 2
            {
                Refin = 2;
            }
            else if ((RefinChance <= 67) && (RefinChance >= 52)) // 15% Refin 3
            {
                Refin = 3;
            }
            else if ((RefinChance <= 78) && (RefinChance >= 68)) // 10% Refin 4
            {
                Refin = 4;
            }
            else if ((RefinChance <= 87) && (RefinChance >= 79)) // 8% Refin 5
            {
                Refin = 5;
            }
            else if ((RefinChance <= 87) && (RefinChance >= 85)) // 5% Refin 6
            {
                Refin = 6;
            }
            else if ((RefinChance <= 90) && (RefinChance >= 88)) // 3% Refin 7
            {
                Refin = 7;
            }
            else if ((RefinChance <= 92) && (RefinChance >= 91)) // 1.x% Refin 8
            {
                Refin = 8;
            }
            else if ((RefinChance <= 94) && (RefinChance >= 93)) // 1.x% Refin 9
            {
                Refin = 9;
            }
            else if ((RefinChance <= 96) && (RefinChance >= 95)) // 1.x% Refin 10
            {
                Refin = 10;
            }
            else if ((RefinChance <= 98) && (RefinChance >= 97)) // 1.x% Refin 11
            {
                Refin = 11;
            }
            else if (RefinChance == 99) // 0.5% Refin 12
            {
                Refin = 12;
            }
            else if (RefinChance == 100) // 0.5% Refin 13
            {
                Refin = 13;
            }

            //Retorna o valor de Refin
            return Refin;
        }
        //*********************************************************************************************
        // DropItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Faz com que determinado item apareça em determinado mapa
        //*********************************************************************************************
        public static void dropItem(int Map, int MapItem, int x, int y, int Value, int ItemNum, int ItemType, int ItemRefin)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, Map, MapItem, x, y, Value, ItemNum, ItemType, ItemRefin) != null)
            {
                return;
            }

            //CÓDIGO
            MapStruct.mapitem[Map, MapItem].Value = Value;
            MapStruct.mapitem[Map, MapItem].X = x;
            MapStruct.mapitem[Map, MapItem].Y = y;
            MapStruct.mapitem[Map, MapItem].ItemNum = ItemNum;
            MapStruct.mapitem[Map, MapItem].ItemType = ItemType;
            MapStruct.mapitem[Map, MapItem].Refin = ItemRefin;
            MapStruct.mapitem[Map, MapItem].Timer = Loops.TickCount.ElapsedMilliseconds + 600000;
        }
    }
}
