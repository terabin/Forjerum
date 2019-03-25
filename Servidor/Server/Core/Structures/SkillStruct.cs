using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados as magias.
    // SkillStruct.cs
    //*********************************************************************************************
    class SkillStruct
    {
        public static Skill[] skill = new Skill[1001];
        public static SkillEffect[,] skilleffect = new SkillEffect[1001, 100];

        public struct Skill
        { 
            public int scope;
            public int stype_id;
            public int mp_cost;
            public int tp_cost;
            public string message1;
            public string message2;
            public int required_wtype_id1;
            public int required_wtype_id2;
            public int occasion;
            public int success_rate;
            public int repeats;
            public int tp_gain;
            public int hit_type;
            public int animation_id;
            public int speed;
            public string note;
            public int damage_type;
            public string damage_formula;
            public int damage_element;
            public int damage_variance;
            public string damage_critical;
            public int effects_count;
            public int type;
            public int range_effect;
            public int second_anim;
            public bool is_line;
            public int interval;
            public int passive_type;
            public int passive_chance;
            public int passive_multiplier;
            public int passive_interval;
            public bool slow;
            public bool stun;
            public int drain;
        }

        public struct SkillEffect
        {
            public int code;
            public int data_id;
            public double value1;
            public double value2;
        }
    }
}