using System;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados as armas.
    // WStruct.cs
    //*********************************************************************************************
    class WStruct
    {
        public static WStruct.Weapon[] weapon = new WStruct.Weapon[1001];
        public static WStruct.WeaponParams[,] weaponparams = new WStruct.WeaponParams[1001, 20];
        public static WStruct.WeaponFeature[,] weaponfeatures = new WStruct.WeaponFeature[1001, 100];

        public struct Weapon
        {
            public string name;
            public int price;
            public int etype_id;
            public int wtype_id;
            public int animation_id;
            public int params_size;
            public int features_size;
        }

        public struct WeaponParams
        {
            public double value;
        }

        public struct WeaponFeature
        {
            public int code;
            public int data_id;
            public double value;
        }
    }
}