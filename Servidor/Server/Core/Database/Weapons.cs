using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Weapons
    {
        //*********************************************************************************************
        // loadWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, weaponnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intweaponnum = Convert.ToInt32(weaponnum);

                //Lê-mos os dados básicos da arma
                WeaponStruct.weapon[intweaponnum].name = br.ReadString();
                WeaponStruct.weapon[intweaponnum].price = br.ReadInt32();
                WeaponStruct.weapon[intweaponnum].etype_id = br.ReadInt32();
                WeaponStruct.weapon[intweaponnum].wtype_id = br.ReadInt32();
                WeaponStruct.weapon[intweaponnum].animation_id = br.ReadInt32();
                WeaponStruct.weapon[intweaponnum].params_size = br.ReadInt32();
                WeaponStruct.weapon[intweaponnum].features_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= WeaponStruct.weapon[intweaponnum].params_size; i++)
                {
                    WeaponStruct.weaponparams[intweaponnum, i].value = br.ReadDouble();
                }

                //Carregamos os features em seguida
                for (int i = 0; i <= WeaponStruct.weapon[intweaponnum].features_size; i++)
                {
                    WeaponStruct.weaponfeatures[intweaponnum, i].code = br.ReadInt32();
                    WeaponStruct.weaponfeatures[intweaponnum, i].data_id = br.ReadInt32();
                    WeaponStruct.weaponfeatures[intweaponnum, i].value = br.ReadInt32();
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
        // saveWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, weaponnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Weapons/" + weaponnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intweaponnum = Convert.ToInt32(weaponnum);

            //Salvamos os dados básicos da arma
            bw.Write(WeaponStruct.weapon[intweaponnum].name);
            bw.Write(WeaponStruct.weapon[intweaponnum].price);
            bw.Write(WeaponStruct.weapon[intweaponnum].etype_id);
            bw.Write(WeaponStruct.weapon[intweaponnum].wtype_id);
            bw.Write(WeaponStruct.weapon[intweaponnum].animation_id);
            bw.Write(WeaponStruct.weapon[intweaponnum].params_size);
            bw.Write(WeaponStruct.weapon[intweaponnum].features_size);

            //Salvamos os params das armas
            for (int i = 0; i <= WeaponStruct.weapon[intweaponnum].params_size; i++)
            {
                bw.Write(WeaponStruct.weaponparams[intweaponnum, i].value);
            }

            //Salvamos as features das armas
            for (int i = 0; i <= WeaponStruct.weapon[intweaponnum].features_size; i++)
            {
                bw.Write(WeaponStruct.weaponfeatures[intweaponnum, i].code);
                bw.Write(WeaponStruct.weaponfeatures[intweaponnum, i].data_id);
                bw.Write(WeaponStruct.weaponfeatures[intweaponnum, i].value);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearWeapon / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearWeapon(string weaponnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, weaponnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intweaponnum = Convert.ToInt32(weaponnum);

            //Limpamos os dados básicos da arma
            WeaponStruct.weapon[intweaponnum].name = "";
            WeaponStruct.weapon[intweaponnum].price = 0;
            WeaponStruct.weapon[intweaponnum].etype_id = 0;
            WeaponStruct.weapon[intweaponnum].wtype_id = 0;
            WeaponStruct.weapon[intweaponnum].animation_id = 0;

            int params_size = WeaponStruct.weapon[intweaponnum].params_size;
            int features_size = WeaponStruct.weapon[intweaponnum].features_size;

            WeaponStruct.weapon[intweaponnum].params_size = 0;
            WeaponStruct.weapon[intweaponnum].features_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                WeaponStruct.weaponparams[intweaponnum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                WeaponStruct.weaponfeatures[intweaponnum, i].code = 0;
                WeaponStruct.weaponfeatures[intweaponnum, i].data_id = 0;
                WeaponStruct.weaponfeatures[intweaponnum, i].value = 0.0;
            }
        }
        //*********************************************************************************************
        // loadWeapons / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadWeapons()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxWeapons; i++)
            {
                if (loadWeapon(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearWeapon(Convert.ToString(i));
                    saveWeapon(Convert.ToString(i));
                }
            }

        }
    }
}
