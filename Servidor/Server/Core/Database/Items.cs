using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Items
    {
        //*********************************************************************************************
        // loadItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, itemnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intitemnum = Convert.ToInt32(itemnum);

                //Lê-mos os dados básicos do item
                ItemStruct.item[intitemnum].name = br.ReadString();
                ItemStruct.item[intitemnum].price = br.ReadInt32();
                ItemStruct.item[intitemnum].consumable = br.ReadString();
                ItemStruct.item[intitemnum].success_rate = br.ReadInt32();
                ItemStruct.item[intitemnum].animation_id = br.ReadInt32();
                ItemStruct.item[intitemnum].note = br.ReadString();

                if (ItemStruct.item[intitemnum].note.Length > 1)
                {
                    string[] data = ItemStruct.item[intitemnum].note.Split(',');
                    ItemStruct.itemextra[intitemnum].type = Convert.ToInt32(data[0]);

                    if (ItemStruct.itemextra[intitemnum].type == 1)
                    {
                        ItemStruct.itemextra[intitemnum].sprite = data[1];
                        ItemStruct.itemextra[intitemnum].sprite_s = Convert.ToInt32(data[2]);
                    }
                }

                ItemStruct.item[intitemnum].speed = br.ReadInt32();
                ItemStruct.item[intitemnum].repeats = br.ReadInt32();
                ItemStruct.item[intitemnum].tp_gain = br.ReadInt32();
                ItemStruct.item[intitemnum].hit_type = br.ReadInt32();
                ItemStruct.item[intitemnum].effects_count = br.ReadInt32();
                ItemStruct.item[intitemnum].damage_type = br.ReadInt32();
                ItemStruct.item[intitemnum].damage_formula = br.ReadString();
                ItemStruct.item[intitemnum].damage_element = br.ReadInt32();
                ItemStruct.item[intitemnum].damage_variance = br.ReadInt32();
                ItemStruct.item[intitemnum].damage_critical = br.ReadString();


                //Carregamos os efeitos em seguida
                for (int i = 0; i <= ItemStruct.item[intitemnum].effects_count; i++)
                {
                    ItemStruct.itemeffect[intitemnum, i].code = br.ReadInt32();
                    ItemStruct.itemeffect[intitemnum, i].data_id = br.ReadInt32();
                    ItemStruct.itemeffect[intitemnum, i].value1 = br.ReadDouble();
                    ItemStruct.itemeffect[intitemnum, i].value2 = br.ReadDouble();
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
        //*********************************************************************************************
        // saveItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, itemnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Items/" + itemnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intitemnum = Convert.ToInt32(itemnum);

            //Salvamos os dados básicos do item
            bw.Write(ItemStruct.item[intitemnum].name);
            bw.Write(ItemStruct.item[intitemnum].price);
            bw.Write(ItemStruct.item[intitemnum].consumable);
            bw.Write(ItemStruct.item[intitemnum].success_rate);
            bw.Write(ItemStruct.item[intitemnum].animation_id);
            bw.Write(ItemStruct.item[intitemnum].note);
            bw.Write(ItemStruct.item[intitemnum].speed);
            bw.Write(ItemStruct.item[intitemnum].repeats);
            bw.Write(ItemStruct.item[intitemnum].tp_gain);
            bw.Write(ItemStruct.item[intitemnum].hit_type);
            bw.Write(ItemStruct.item[intitemnum].effects_count);
            bw.Write(ItemStruct.item[intitemnum].damage_type);
            bw.Write(ItemStruct.item[intitemnum].damage_formula);
            bw.Write(ItemStruct.item[intitemnum].damage_element);
            bw.Write(ItemStruct.item[intitemnum].damage_variance);
            bw.Write(ItemStruct.item[intitemnum].damage_critical);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= ItemStruct.item[intitemnum].effects_count; i++)
            {
                bw.Write(ItemStruct.itemeffect[intitemnum, i].code);
                bw.Write(ItemStruct.itemeffect[intitemnum, i].data_id);
                bw.Write(ItemStruct.itemeffect[intitemnum, i].value1);
                bw.Write(ItemStruct.itemeffect[intitemnum, i].value2);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearItem / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearItem(string itemnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, itemnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intitemnum = Convert.ToInt32(itemnum);

            //Limpamos o tamanho do mapa
            ItemStruct.item[intitemnum].name = "";
            ItemStruct.item[intitemnum].price = 0;
            ItemStruct.item[intitemnum].consumable = "";
            ItemStruct.item[intitemnum].success_rate = 0;
            ItemStruct.item[intitemnum].animation_id = 0;
            ItemStruct.item[intitemnum].note = "";
            ItemStruct.item[intitemnum].speed = 0;
            ItemStruct.item[intitemnum].repeats = 0;
            ItemStruct.item[intitemnum].tp_gain = 0;
            ItemStruct.item[intitemnum].hit_type = 0;
            ItemStruct.item[intitemnum].damage_type = 0;
            ItemStruct.item[intitemnum].damage_formula = "";
            ItemStruct.item[intitemnum].damage_element = 0;
            ItemStruct.item[intitemnum].damage_variance = 0;
            ItemStruct.item[intitemnum].damage_critical = "";

            int effects_count = ItemStruct.item[intitemnum].effects_count;

            ItemStruct.item[intitemnum].effects_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                ItemStruct.itemeffect[intitemnum, i].code = 0;
                ItemStruct.itemeffect[intitemnum, i].data_id = 0;
                ItemStruct.itemeffect[intitemnum, i].value1 = 0.0;
                ItemStruct.itemeffect[intitemnum, i].value2 = 0.0;
            }
        }
        //*********************************************************************************************
        // loadItems / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadItems()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.MaxItems; i++)
            {
                if (loadItem(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearItem(Convert.ToString(i));
                    saveItem(Convert.ToString(i));
                }
            }

        }
    }
}
