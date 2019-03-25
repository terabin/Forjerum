using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Enemies
    {
        //*********************************************************************************************
        // loadEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, enemynum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intenemynum = Convert.ToInt32(enemynum);

                //Lê-mos os dados básicos da arma
                EnemyStruct.enemy[intenemynum].battler_name = br.ReadString();
                EnemyStruct.enemy[intenemynum].battler_hue = br.ReadInt32();
                EnemyStruct.enemy[intenemynum].exp = br.ReadInt32();
                EnemyStruct.enemy[intenemynum].gold = br.ReadInt32();
                EnemyStruct.enemy[intenemynum].note = br.ReadString();
                EnemyStruct.enemy[intenemynum].params_size = br.ReadInt32();

                //Carregamos os params em seguida
                for (int i = 0; i <= EnemyStruct.enemy[intenemynum].params_size; i++)
                {
                    EnemyStruct.enemyparams[intenemynum, i].value = br.ReadDouble();
                }

                EnemyStruct.enemy[intenemynum].drops_size = br.ReadInt32();

                //Carregamos os features em seguida
                for (int i = 0; i <= EnemyStruct.enemy[intenemynum].drops_size; i++)
                {
                    EnemyStruct.enemydrops[intenemynum, i].kind = br.ReadInt32();
                    EnemyStruct.enemydrops[intenemynum, i].data_id = br.ReadInt32();
                    EnemyStruct.enemydrops[intenemynum, i].denominator = br.ReadDouble();
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
        // saveEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, enemynum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Enemies/" + enemynum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intenemynum = Convert.ToInt32(enemynum);

            //Salvamos os dados básicos do inimigo
            bw.Write(EnemyStruct.enemy[intenemynum].battler_name);
            bw.Write(EnemyStruct.enemy[intenemynum].battler_hue);
            bw.Write(EnemyStruct.enemy[intenemynum].exp);
            bw.Write(EnemyStruct.enemy[intenemynum].gold);
            bw.Write(EnemyStruct.enemy[intenemynum].note);
            bw.Write(EnemyStruct.enemy[intenemynum].params_size);

            //Salvamos os params dos inimigos
            for (int i = 0; i <= EnemyStruct.enemy[intenemynum].params_size; i++)
            {
                bw.Write(EnemyStruct.enemyparams[intenemynum, i].value);
            }

            bw.Write(EnemyStruct.enemy[intenemynum].drops_size);

            //Salvamos as features das armas
            for (int i = 0; i <= EnemyStruct.enemy[intenemynum].drops_size; i++)
            {
                bw.Write(EnemyStruct.enemydrops[intenemynum, i].kind);
                bw.Write(EnemyStruct.enemydrops[intenemynum, i].data_id);
                bw.Write(EnemyStruct.enemydrops[intenemynum, i].denominator);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearEnemy / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearEnemy(string enemynum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, enemynum) != null)
            {
                return;
            }

            //CÓDIGO
            int intenemynum = Convert.ToInt32(enemynum);

            //Limpamos os dados básicos do inimigo
            EnemyStruct.enemy[intenemynum].battler_name = "";
            EnemyStruct.enemy[intenemynum].battler_hue = 0;
            EnemyStruct.enemy[intenemynum].exp = 0;
            EnemyStruct.enemy[intenemynum].gold = 0;
            EnemyStruct.enemy[intenemynum].note = "";

            int params_size = EnemyStruct.enemy[intenemynum].params_size;
            int features_size = EnemyStruct.enemy[intenemynum].drops_size;

            EnemyStruct.enemy[intenemynum].params_size = 0;
            EnemyStruct.enemy[intenemynum].drops_size = 0;

            //Limpamos os params
            for (int i = 0; i <= params_size; i++)
            {
                EnemyStruct.enemyparams[intenemynum, i].value = 0;
            }

            //Limpamos as features
            for (int i = 0; i <= features_size; i++)
            {
                EnemyStruct.enemydrops[intenemynum, i].kind = 0;
                EnemyStruct.enemydrops[intenemynum, i].data_id = 0;
                EnemyStruct.enemydrops[intenemynum, i].denominator = 0.0;
            }
        }
        //*********************************************************************************************
        // loadEnemies / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadEnemies()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxEnemies; i++)
            {
                if (loadEnemy(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearEnemy(Convert.ToString(i));
                    saveEnemy(Convert.ToString(i));
                }
            }

        }
    }
}
