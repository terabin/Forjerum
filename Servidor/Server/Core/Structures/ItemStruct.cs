using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a itens.
    // ItemStruct.cs
    //*********************************************************************************************
    class ItemStruct
    {
        public static Item[] item = new Item[Globals.MaxItems];
        public static ItemExtra[] itemextra = new ItemExtra[Globals.MaxItems];
        public static ItemEffect[,] itemeffect = new ItemEffect[Globals.MaxItems, 100];

        public struct Item
        {
            public string name;
            public int price;
            public string consumable;
            public int success_rate;
            public int animation_id;
            public string note;
            public int speed;
            public int repeats;
            public int tp_gain;
            public int hit_type;
            public int damage_type;
            public string damage_formula;
            public int damage_element;
            public int damage_variance;
            public string damage_critical;
            public int effects_count;
        }

        public struct ItemExtra
        {
            public int type;
            public string sprite;
            public int sprite_s;
        }

        public struct ItemEffect
        {
            public int code;
            public int data_id;
            public double value1;
            public double value2;
        }
    }
}
