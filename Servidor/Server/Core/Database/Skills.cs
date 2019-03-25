using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Skills
    {
        //*********************************************************************************************
        // loadSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, skillnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intskillnum = Convert.ToInt32(skillnum);

                //Lê-mos os dados básicos do item
                SkillStruct.skill[intskillnum].scope = br.ReadInt32();
                SkillStruct.skill[intskillnum].stype_id = br.ReadInt32();
                SkillStruct.skill[intskillnum].mp_cost = br.ReadInt32();
                SkillStruct.skill[intskillnum].tp_cost = br.ReadInt32();
                SkillStruct.skill[intskillnum].message1 = br.ReadString();
                SkillStruct.skill[intskillnum].message2 = br.ReadString();
                SkillStruct.skill[intskillnum].required_wtype_id1 = br.ReadInt32();
                SkillStruct.skill[intskillnum].required_wtype_id2 = br.ReadInt32();
                SkillStruct.skill[intskillnum].occasion = br.ReadInt32();
                SkillStruct.skill[intskillnum].success_rate = br.ReadInt32();
                SkillStruct.skill[intskillnum].repeats = br.ReadInt32();
                SkillStruct.skill[intskillnum].tp_gain = br.ReadInt32();
                SkillStruct.skill[intskillnum].hit_type = br.ReadInt32();
                SkillStruct.skill[intskillnum].animation_id = br.ReadInt32();
                SkillStruct.skill[intskillnum].speed = br.ReadInt32();
                SkillStruct.skill[intskillnum].note = br.ReadString();
                SkillStruct.skill[intskillnum].damage_type = br.ReadInt32();
                SkillStruct.skill[intskillnum].damage_formula = br.ReadString();
                SkillStruct.skill[intskillnum].damage_element = br.ReadInt32();
                SkillStruct.skill[intskillnum].damage_variance = br.ReadInt32();
                SkillStruct.skill[intskillnum].damage_critical = br.ReadString();
                SkillStruct.skill[intskillnum].effects_count = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 0; i <= SkillStruct.skill[intskillnum].effects_count; i++)
                {
                    SkillStruct.skilleffect[intskillnum, i].code = br.ReadInt32();
                    SkillStruct.skilleffect[intskillnum, i].data_id = br.ReadInt32();
                    SkillStruct.skilleffect[intskillnum, i].value1 = br.ReadDouble();
                    SkillStruct.skilleffect[intskillnum, i].value2 = br.ReadDouble();
                }

                //Fecha o leitor
                br.Close();

                if (String.IsNullOrEmpty(SkillStruct.skill[intskillnum].note)) { return true; }

                if ((SkillStruct.skill[intskillnum].repeats <= 1))
                {
                    SkillStruct.skill[intskillnum].type = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[0]);
                    SkillStruct.skill[intskillnum].range_effect = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[1]);
                    if (SkillStruct.skill[intskillnum].type == 10)
                    {
                        SkillStruct.skill[intskillnum].passive_type = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[2]);
                        SkillStruct.skill[intskillnum].passive_chance = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[3]);
                        SkillStruct.skill[intskillnum].passive_multiplier = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[4]);
                        SkillStruct.skill[intskillnum].passive_interval = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[5]);
                    }
                    if ((SkillStruct.skill[intskillnum].type == 7) || (SkillStruct.skill[intskillnum].type == 8) || (SkillStruct.skill[intskillnum].type == 13))
                    {
                        SkillStruct.skill[intskillnum].interval = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[2]);
                    }
                    if (SkillStruct.skill[intskillnum].type == 9)
                    {
                        SkillStruct.skill[intskillnum].drain = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[2]);
                    }
                }
                else
                {
                    SkillStruct.skill[intskillnum].type = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[0]);
                    SkillStruct.skill[intskillnum].second_anim = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[1]);
                    SkillStruct.skill[intskillnum].interval = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[2]);
                    SkillStruct.skill[intskillnum].range_effect = Convert.ToInt32(SkillStruct.skill[intskillnum].note.Split(',')[3]);
                    SkillStruct.skill[intskillnum].is_line = Convert.ToBoolean(SkillStruct.skill[intskillnum].note.Split(',')[4]);
                    SkillStruct.skill[intskillnum].slow = Convert.ToBoolean(SkillStruct.skill[intskillnum].note.Split(',')[5]);
                }


                //if (String.IsNullOrEmpty(MapStruct.map[Convert.ToInt32(mapnum)].max_width)) { clearMap(mapnum); saveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que o mapa não existe
            { return false; }

        }
        //*********************************************************************************************
        // saveSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, skillnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Skills/" + skillnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intskillnum = Convert.ToInt32(skillnum);

            //Salvamos os dados básicos do item
            bw.Write(SkillStruct.skill[intskillnum].scope);
            bw.Write(SkillStruct.skill[intskillnum].stype_id);
            bw.Write(SkillStruct.skill[intskillnum].mp_cost);
            bw.Write(SkillStruct.skill[intskillnum].tp_cost);
            bw.Write(SkillStruct.skill[intskillnum].message1);
            bw.Write(SkillStruct.skill[intskillnum].message2);
            bw.Write(SkillStruct.skill[intskillnum].required_wtype_id1);
            bw.Write(SkillStruct.skill[intskillnum].required_wtype_id2);
            bw.Write(SkillStruct.skill[intskillnum].occasion);
            bw.Write(SkillStruct.skill[intskillnum].success_rate);
            bw.Write(SkillStruct.skill[intskillnum].repeats);
            bw.Write(SkillStruct.skill[intskillnum].tp_gain);
            bw.Write(SkillStruct.skill[intskillnum].hit_type);
            bw.Write(SkillStruct.skill[intskillnum].animation_id);
            bw.Write(SkillStruct.skill[intskillnum].speed);
            bw.Write(SkillStruct.skill[intskillnum].note);
            bw.Write(SkillStruct.skill[intskillnum].damage_type);
            bw.Write(SkillStruct.skill[intskillnum].damage_formula);
            bw.Write(SkillStruct.skill[intskillnum].damage_element);
            bw.Write(SkillStruct.skill[intskillnum].damage_variance);
            bw.Write(SkillStruct.skill[intskillnum].damage_critical);
            bw.Write(SkillStruct.skill[intskillnum].effects_count);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= SkillStruct.skill[intskillnum].effects_count; i++)
            {
                bw.Write(SkillStruct.skilleffect[intskillnum, i].code);
                bw.Write(SkillStruct.skilleffect[intskillnum, i].data_id);
                bw.Write(SkillStruct.skilleffect[intskillnum, i].value1);
                bw.Write(SkillStruct.skilleffect[intskillnum, i].value2);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearSkill / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearSkill(string skillnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, skillnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intskillnum = Convert.ToInt32(skillnum);

            //Limpamos o tamanho do mapa
            SkillStruct.skill[intskillnum].scope = 0;
            SkillStruct.skill[intskillnum].stype_id = 0;
            SkillStruct.skill[intskillnum].mp_cost = 0;
            SkillStruct.skill[intskillnum].tp_cost = 0;
            SkillStruct.skill[intskillnum].message1 = "";
            SkillStruct.skill[intskillnum].message2 = "";
            SkillStruct.skill[intskillnum].required_wtype_id1 = 0;
            SkillStruct.skill[intskillnum].required_wtype_id2 = 0;
            SkillStruct.skill[intskillnum].occasion = 0;
            SkillStruct.skill[intskillnum].success_rate = 0;
            SkillStruct.skill[intskillnum].repeats = 0;
            SkillStruct.skill[intskillnum].tp_gain = 0;
            SkillStruct.skill[intskillnum].hit_type = 0;
            SkillStruct.skill[intskillnum].animation_id = 0;
            SkillStruct.skill[intskillnum].speed = 0;
            SkillStruct.skill[intskillnum].note = "";
            SkillStruct.skill[intskillnum].damage_type = 0;
            SkillStruct.skill[intskillnum].damage_formula = "";
            SkillStruct.skill[intskillnum].damage_element = 0;
            SkillStruct.skill[intskillnum].damage_variance = 0;
            SkillStruct.skill[intskillnum].damage_critical = "false";

            int effects_count = SkillStruct.skill[intskillnum].effects_count;

            SkillStruct.skill[intskillnum].effects_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                SkillStruct.skilleffect[intskillnum, i].code = 0;
                SkillStruct.skilleffect[intskillnum, i].data_id = 0;
                SkillStruct.skilleffect[intskillnum, i].value1 = 0.0;
                SkillStruct.skilleffect[intskillnum, i].value2 = 0.0;
            }
        }
        //*********************************************************************************************
        // loadSkills / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadSkills()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxSkills; i++)
            {
                if (loadSkill(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearSkill(Convert.ToString(i));
                    saveSkill(Convert.ToString(i));
                }
            }

        }
    }
}
