using System;
using System.Reflection;

namespace __Forjerum
{
    class InventoryRelations : Languages.LStruct
    {
        //*********************************************************************************************
        // GiveItem
        // Entrega determinado item para determinado jogador
        //*********************************************************************************************
        public static bool giveItem(int s, int itemt, int itemn, int itemv, int itemr, int itemex)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex));
            }

            //CÓDIGO
            //Não entregar itens inválidos.
            if (itemn <= 0) { return false; }

            //Já temos os item? Se sim, adicionar.
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                string item = PlayerStruct.invslot[s, i].item;
                string[] splititem = item.Split(',');

                int itemNum = Convert.ToInt32(splititem[1]);
                int itemType = Convert.ToInt32(splititem[0]);
                int itemValue = Convert.ToInt32(splititem[2]);
                int itemRefin = Convert.ToInt32(splititem[3]);
                int itemExp = Convert.ToInt32(splititem[4]);

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemex == itemExp))
                {
                    PlayerStruct.invslot[s, i].item = itemType + "," + itemNum + "," + (itemValue + itemv) + "," + itemRefin + "," + itemex;
                    return true;
                }
            }
            if (getInvOpenSlot(s) > 0)
            {
                PlayerStruct.invslot[s, getInvOpenSlot(s)].item = itemt + "," + itemn + "," + itemv + "," + itemr + "," + itemex;
                return true;
            }
            else
            {
                SendData.sendMsgToPlayer(s, lang.you_dont_have_inventory_slot, Globals.ColorRed, Globals.Msg_Type_Server);
                return false;
            }
        }
        //*********************************************************************************************
        // getNumOfInvFreeSlots
        //*********************************************************************************************
        public static int getNumOfInvFreeSlots(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int count = 0;

            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                if (PlayerStruct.invslot[s, i].item == Globals.NullItem) { count += 1; }
            }
            return count;
        }
        //*********************************************************************************************
        // getInvItemSlot
        // Retorna determinado item baseado no slot do inventário
        //*********************************************************************************************
        public static int getInvItemSlot(int s, string item)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, item) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                if (PlayerStruct.invslot[s, i].item == item) { return i; }
            }

            return 0;
        }
        //*********************************************************************************************
        // getInvOpenSlot
        // Retorna slot livre no inventário de determinado jogador
        //*********************************************************************************************
        public static int getInvOpenSlot(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                if (PlayerStruct.invslot[s, i].item == Globals.NullItem) { return i; }
            }

            return 0;
        }
        //*********************************************************************************************
        // PickItem
        //*********************************************************************************************
        public static bool pickItem(int s, int itemt, int itemn, int itemv, int itemr, int itemex = 0)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                string item = PlayerStruct.invslot[s, i].item;
                string[] splititem = item.Split(',');

                int itemNum = Convert.ToInt32(splititem[1]);
                int itemType = Convert.ToInt32(splititem[0]);
                int itemValue = Convert.ToInt32(splititem[2]);
                int itemRefin = Convert.ToInt32(splititem[3]);
                int itemExp = Convert.ToInt32(splititem[4]);

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemv == itemValue))
                {
                    PlayerStruct.invslot[s, i].item = Globals.NullItem;
                    return true;
                }

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemv <= itemValue) && (itemex <= itemExp))
                {
                    PlayerStruct.invslot[s, i].item = itemType + "," + itemNum + "," + (itemValue - itemv) + "," + itemRefin + "," + itemExp;
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // clearItem
        // Limpar o item (?)
        //*********************************************************************************************
        public static bool clearItem(int s, string item)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, item) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, item));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                if (PlayerStruct.invslot[s, i].item == item) { PlayerStruct.invslot[s, i].item = Globals.NullItem; return true; }
            }

            return false;
        }
        //*********************************************************************************************
        // HasItem
        // Retorna se o jogador tem determinado item
        //*********************************************************************************************
        public static bool hasItem(int s, string item)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, item) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, item));
            }

            //CÓDIGO
            string[] data = item.Split(',');
            string itemtype = data[0];
            string itemnum = data[1];
            int itemvalue = Convert.ToInt32(data[2]);

            string[] datainv;
            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                datainv = PlayerStruct.invslot[s, i].item.Split(',');
                if ((itemtype == datainv[0]) && (itemnum == datainv[1]) && (itemvalue <= Convert.ToInt32(datainv[2]))) { return true; }
            }

            return false;
        }
    }
}
