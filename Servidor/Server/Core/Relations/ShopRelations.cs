using System;
using System.Reflection;

namespace __Forjerum
{
    class ShopRelations
    {
        //*********************************************************************************************
        // getPShopOpenSlot
        // Retorna slot livre na loja pessoal de determinado jogador
        //*********************************************************************************************
        public static int getPShopOpenSlot(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_PShops; i++)
            {
                if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num == 0) { return i; }
            }

            return 0;
        }
        //*********************************************************************************************
        // GivePShopItem
        // Entrega item da loja do jogador ao próprio jogador
        //*********************************************************************************************
        public static bool givePShopItem(int s, int itemt, int itemn, int itemv, int itemr, int itemp, int itemex)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemp, itemex) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemp, itemex));
            }

            //CÓDIGO
            //Já temos os item? Se sim, adicionar.
            for (int i = 1; i < Globals.Max_PShops; i++)
            {

                int itemNum = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num;
                int itemType = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type;
                int itemValue = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value;
                int itemRefin = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin;
                int itemExp = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].exp;
                int itemPrice = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price;

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemp == itemPrice) && (itemex == itemExp))
                {
                    PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value += itemv;
                    return true;
                }
            }
            if (getPShopOpenSlot(s) > 0)
            {
                int openslot = getPShopOpenSlot(s);
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].type = itemt;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].num = itemn;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].value = itemv;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].refin = itemr;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].exp = itemex;
                PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[openslot].price = itemp;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
