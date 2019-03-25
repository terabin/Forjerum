using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a armadura.
    // ArmorStruct.cs
    //*********************************************************************************************
    class ArmorStruct
    {
        public static Armor[] armor = new Armor[1001];
        public static ArmorParams[,] armorparams = new ArmorParams[1001, 20];
        public static ArmorFeature[,] armorfeatures = new ArmorFeature[1001, 100];

        public struct Armor
        {
            public string name;
            public int price;
            public int etype_id;
            public int atype_id;
            public int params_size;
            public int features_size;
        }

        public struct ArmorParams
        {
            public double value;
        }

        public struct ArmorFeature
        {
            public int code;
            public int data_id;
            public double value;
        }
    }
}