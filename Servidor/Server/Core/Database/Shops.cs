using System;
using System.Reflection;
using System.IO;

namespace __Forjerum.Database
{
    class Shops
    {
        //*********************************************************************************************
        // loadShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, shopnum));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat"))
            {

                //representa o arquivo
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat", FileMode.Open);

                //cria o leitor do arquivo
                BinaryReader br = new BinaryReader(file);

                int intshopnum = Convert.ToInt32(shopnum);

                //Lê-mos os dados básicos da loja
                ShopStruct.shop[intshopnum].map = br.ReadInt32();
                ShopStruct.shop[intshopnum].x = br.ReadInt32();
                ShopStruct.shop[intshopnum].y = br.ReadInt32();
                ShopStruct.shop[intshopnum].item_count = br.ReadInt32();

                //Carregamos os efeitos em seguida
                for (int i = 0; i <= ShopStruct.shop[intshopnum].item_count; i++)
                {
                    ShopStruct.shopitem[intshopnum, i].type = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].num = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].value = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].refin = br.ReadInt32();
                    ShopStruct.shopitem[intshopnum, i].price = br.ReadInt32();
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
        // saveShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool saveShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, shopnum));
            }

            //CÓDIGO
            //representa o arquivo que vamos criar
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Shops/" + shopnum + ".dat", FileMode.Create);

            //Definimos o escrivão do arquivo. hue
            BinaryWriter bw = new BinaryWriter(file);

            int intshopnum = Convert.ToInt32(shopnum);

            //Salvamos os dados básicos do item
            bw.Write(ShopStruct.shop[intshopnum].map);
            bw.Write(ShopStruct.shop[intshopnum].x);
            bw.Write(ShopStruct.shop[intshopnum].y);
            bw.Write(ShopStruct.shop[intshopnum].item_count);

            //Salvamos os efeitos dos itens
            for (int i = 0; i <= ShopStruct.shop[intshopnum].item_count; i++)
            {
                bw.Write(ShopStruct.shopitem[intshopnum, i].type);
                bw.Write(ShopStruct.shopitem[intshopnum, i].num);
                bw.Write(ShopStruct.shopitem[intshopnum, i].value);
                bw.Write(ShopStruct.shopitem[intshopnum, i].refin);
                bw.Write(ShopStruct.shopitem[intshopnum, i].price);
            }

            bw.Close();

            //Retorna que deu tudo certo
            return true;
        }
        //*********************************************************************************************
        // clearShop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void clearShop(string shopnum)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, shopnum) != null)
            {
                return;
            }

            //CÓDIGO
            int intshopnum = Convert.ToInt32(shopnum);

            //Limpamos o tamanho do mapa
            ShopStruct.shop[intshopnum].map = 0;
            ShopStruct.shop[intshopnum].x = 0;
            ShopStruct.shop[intshopnum].y = 0;

            int effects_count = ShopStruct.shop[intshopnum].item_count;

            ShopStruct.shop[intshopnum].item_count = 0;

            //Limpamos os efeitos
            for (int i = 0; i <= effects_count; i++)
            {
                ShopStruct.shopitem[intshopnum, i].type = 0;
                ShopStruct.shopitem[intshopnum, i].num = 0;
                ShopStruct.shopitem[intshopnum, i].value = 0;
                ShopStruct.shopitem[intshopnum, i].refin = 0;
                ShopStruct.shopitem[intshopnum, i].price = 0;
            }
        }
        //*********************************************************************************************
        // loadShops / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadShops()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Vamos analisar qual s está disponível para o jogador
            for (int i = 1; i < Globals.Max_Shops; i++)
            {
                if (loadShop(Convert.ToString(i)))
                {
                    // okay
                }
                else
                {
                    clearShop(Convert.ToString(i));
                    saveShop(Convert.ToString(i));
                }
            }

        }
    }
}
