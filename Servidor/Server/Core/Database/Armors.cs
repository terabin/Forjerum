using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Armors
    {
        //*********************************************************************************************
        // loadArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, armornum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intarmornum = Convert.ToInt32(armornum);

                //Lê-mos os dados básicos da arma
                ArmorStruct.armor[intarmornum].name = br.ReadString();
                ArmorStruct.armor[intarmornum].price = br.ReadInt32();
                ArmorStruct.armor[intarmornum].etype_id = br.ReadInt32();
                ArmorStruct.armor[intarmornum].atype_id = br.ReadInt32();
                ArmorStruct.armor[intarmornum].params_size = br.ReadInt32();
                ArmorStruct.armor[intarmornum].features_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= ArmorStruct.armor[intarmornum].params_size; i++)
                {
                    ArmorStruct.armorparams[intarmornum, i].value = br.ReadDouble();
                }

                //Carregamos os features em seguida
                for (int i = 0; i <= ArmorStruct.armor[intarmornum].features_size; i++)
                {
                    ArmorStruct.armorfeatures[intarmornum, i].code = br.ReadInt32();
                    ArmorStruct.armorfeatures[intarmornum, i].data_id = br.ReadInt32();
                    ArmorStruct.armorfeatures[intarmornum, i].value = br.ReadInt32();
                }

                //Fecha o leitor
                br.Close();

                //if (String.IsNullOrEmpty(MapStruct.map[Convert.ToInt32(mapnum)].max_width)) { clearMap(mapnum); saveMap(mapnum); }

                //Responde que o item foi carregado
                return true;
            }
            else
            //Responde que a arma não existe
            { return false; }

        }
        //*********************************************************************************************
        // saveArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, armornum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Armors/" + armornum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intarmornum = Convert.ToInt32(armornum);

            //Salvamos os dados básicos da arma
            bw.Write(ArmorStruct.armor[intarmornum].name);
            bw.Write(ArmorStruct.armor[intarmornum].price);
            bw.Write(ArmorStruct.armor[intarmornum].etype_id);
            bw.Write(ArmorStruct.armor[intarmornum].atype_id);
            bw.Write(ArmorStruct.armor[intarmornum].params_size);
            bw.Write(ArmorStruct.armor[intarmornum].features_size);

            //Salvamos os params das armas
            for (int i = 0; i <= ArmorStruct.armor[intarmornum].params_size; i++)
            {
                bw.Write(ArmorStruct.armorparams[intarmornum, i].value);
            }

            //Salvamos as features das armas
            for (int i = 0; i <= ArmorStruct.armor[intarmornum].features_size; i++)
            {
                bw.Write(ArmorStruct.armorfeatures[intarmornum, i].code);
                bw.Write(ArmorStruct.armorfeatures[intarmornum, i].data_id);
                bw.Write(ArmorStruct.armorfeatures[intarmornum, i].value);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearArmor / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearArmor(string armornum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, armornum) != null)
            {
                return;
            }

            //CÓDIGO
            int intarmornum = Convert.ToInt32(armornum);

            //Limpamos os dados básicos da arma
            ArmorStruct.armor[intarmornum].name = "";
            ArmorStruct.armor[intarmornum].price = 0;
            ArmorStruct.armor[intarmornum].etype_id = 0;
            ArmorStruct.armor[intarmornum].atype_id = 0;

            int params_size = ArmorStruct.armor[intarmornum].params_size;
            int features_size = ArmorStruct.armor[intarmornum].features_size;

            ArmorStruct.armor[intarmornum].params_size = 0;
            ArmorStruct.armor[intarmornum].features_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                ArmorStruct.armorparams[intarmornum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                ArmorStruct.armorfeatures[intarmornum, i].code = 0;
                ArmorStruct.armorfeatures[intarmornum, i].data_id = 0;
                ArmorStruct.armorfeatures[intarmornum, i].value = 0.0;
            }
        }
        //*********************************************************************************************
        // loadArmors / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadArmors()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxArmors; i++)
            {
                if (loadArmor(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearArmor(Convert.ToString(i));
                    saveArmor(Convert.ToString(i));
                }
            }

        }
    }
}
