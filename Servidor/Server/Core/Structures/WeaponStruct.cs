using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados as armas.
    // WeaponStruct.cs
    //*********************************************************************************************
    class WeaponStruct
    {
        public static Weapon[] weapon = new Weapon[1001];
        public static WeaponParams[,] weaponparams = new WeaponParams[1001, 20];
        public static WeaponFeature[,] weaponfeatures = new WeaponFeature[1001, 100];

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