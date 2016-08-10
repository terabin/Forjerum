using System;

namespace FORJERUM
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a inimigos.
    // EStruct.cs
    //*********************************************************************************************
    class EStruct
    {
        public static EStruct.Enemy[] enemy = new EStruct.Enemy[1001];
        public static EStruct.EnemyParams[,] enemyparams = new EStruct.EnemyParams[1001, 20];
        public static EStruct.EnemyDrops[,] enemydrops = new EStruct.EnemyDrops[1001, 20];

        public struct Enemy
        {
            public int price;
            public string battler_name;
            public int battler_hue;
            public int exp;
            public int gold;
            public string note;
            public int params_size;
            public int drops_size;
        }

        public struct EnemyParams
        {
            public double value;
        }

        public struct EnemyDrops
        {
            public int kind;
            public int data_id;
            public double denominator;
        }
    }
}
