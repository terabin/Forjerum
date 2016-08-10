using System;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a itens.
    // IStruct.cs
    //*********************************************************************************************
    class IStruct
    {
        public static IStruct.Item[] item = new IStruct.Item[1001];
        public static IStruct.ItemExtra[] itemextra = new IStruct.ItemExtra[1001];
        public static IStruct.ItemEffect[,] itemeffect = new IStruct.ItemEffect[1001, 100];

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
            public int sprite_index;
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
