using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Banks
    {
        //*********************************************************************************************
        // saveBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Salva o banco de determinado jogador online.
        //*********************************************************************************************
        public static bool saveBank(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s)); }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PlayerStruct.player[s].Username + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            for (int i = 1; i < Globals.Max_BankSlots; i++)
            {
                bw.Write(PlayerStruct.player[s].bankslot[i].type);
                bw.Write(PlayerStruct.player[s].bankslot[i].num);
                bw.Write(PlayerStruct.player[s].bankslot[i].value);
                bw.Write(PlayerStruct.player[s].bankslot[i].refin);
                bw.Write(PlayerStruct.player[s].bankslot[i].exp);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // loadBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega os itens no banco de determinado jogador.
        //*********************************************************************************************
        public static bool loadBank(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PlayerStruct.player[s].Username + ".dat"))
                {

                    //representa o arquivo
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Banks/" + PlayerStruct.player[s].Username + ".dat", FileMode.Open);

                    //cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    for (int i = 1; i < Globals.Max_BankSlots; i++)
                    {
                        PlayerStruct.player[s].bankslot[i].type = br.ReadInt32();
                        PlayerStruct.player[s].bankslot[i].num = br.ReadInt32();
                        PlayerStruct.player[s].bankslot[i].value = br.ReadInt32();
                        PlayerStruct.player[s].bankslot[i].refin = br.ReadInt32();
                        PlayerStruct.player[s].bankslot[i].exp = br.ReadInt32();
                    }

                    //Fecha o leitor
                    br.Close();

                    //if (String.IsNullOrEmpty(MapStruct.map[Convert.ToInt32(mapnum)].max_width)) { clearMap(mapnum); saveMap(mapnum); }

                    //Responde que o item foi carregado
                    return true;
                }
                else
                //Responde que o mapa não existe
                { return false; }
            }
        }
        //*********************************************************************************************
        // clearBank / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Apenas descarrega a estrutura do banco de determinado jogador.
        //*********************************************************************************************
        public static bool clearBank(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s) != null) { return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s)); }

            //CÓDIGO
            try
            {

                for (int i = 1; i < Globals.Max_BankSlots; i++)
                {
                    PlayerStruct.player[s].bankslot[i].type = 0;
                    PlayerStruct.player[s].bankslot[i].num = 0;
                    PlayerStruct.player[s].bankslot[i].value = 0;
                    PlayerStruct.player[s].bankslot[i].refin = 0;
                }
                return true;
            }
            catch { return false; }
        }
    }
}
