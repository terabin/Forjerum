using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados a inimigos.
    // EnemyStruct.cs
    //*********************************************************************************************
    class EnemyStruct
    {
        public static Enemy[] enemy = new Enemy[Globals.MaxEnemies];
        public static EnemyParams[,] enemyparams = new EnemyParams[Globals.MaxEnemies, 20];
        public static EnemyDrops[,] enemydrops = new EnemyDrops[Globals.MaxEnemies, 20];

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
