using System;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados as lojas.
    // ShopStruct.cs
    //*********************************************************************************************
    class ShopStruct
    {
        public static ShopStruct.Shop[] shop = new ShopStruct.Shop[1001];
        public static ShopStruct.ShopItems[,] shopitem = new ShopStruct.ShopItems[1001, 100];

        public struct Shop
        {
            public int x;
            public int y;
            public int map;
            public int item_count;
        }

        public struct ShopItems
        {
            public int type;
            public int num;
            public int value;
            public int refin;
            public int price;
        }
    }
}