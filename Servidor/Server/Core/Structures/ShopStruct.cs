using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados as lojas.
    // ShopStruct.cs
    //*********************************************************************************************
    class ShopStruct
    {
        public static Shop[] shop = new Shop[1001];
        public static ShopItems[,] shopitem = new ShopItems[1001, 100];

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