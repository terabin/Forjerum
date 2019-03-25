using System;
using System.Reflection;

namespace __Forjerum
{
    class BankRelations
    {
        //*********************************************************************************************
        // giveBankItem
        // Entrega determinado item do banco para determinado jogador
        //*********************************************************************************************
        public static bool giveBankItem(int s, int itemt, int itemn, int itemv, int itemr, int itemex)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr, itemex));
            }

            //CÓDIGO
            //Já temos os item? Se sim, adicionar.
            for (int i = 1; i < Globals.Max_BankSlots; i++)
            {

                int itemNum = PlayerStruct.player[s].bankslot[i].num;
                int itemType = PlayerStruct.player[s].bankslot[i].type;
                int itemValue = PlayerStruct.player[s].bankslot[i].value;
                int itemRefin = PlayerStruct.player[s].bankslot[i].refin;
                int itemExp = PlayerStruct.player[s].bankslot[i].exp;

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemex == itemExp))
                {
                    PlayerStruct.player[s].bankslot[i].value += itemv;
                    return true;
                }
            }
            if (getBankOpenSlot(s) > 0)
            {
                int openslot = getBankOpenSlot(s);
                PlayerStruct.player[s].bankslot[openslot].type = itemt;
                PlayerStruct.player[s].bankslot[openslot].num = itemn;
                PlayerStruct.player[s].bankslot[openslot].value = itemv;
                PlayerStruct.player[s].bankslot[openslot].refin = itemr;
                PlayerStruct.player[s].bankslot[openslot].exp = itemex;
                return true;
            }
            else
            {
                return false;
            }
        }
        //*********************************************************************************************
        // getBankOpenSlot
        // Retorna slot livre no banco de determinado jogador
        //*********************************************************************************************
        public static int getBankOpenSlot(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_BankSlots; i++)
            {
                if (PlayerStruct.player[s].bankslot[i].num == 0) { return i; }
            }

            return 0;
        }
        //*********************************************************************************************
        // pickBankItem
        // Pegar determinado item do banco
        //*********************************************************************************************
        public static bool pickBankItem(int s, int itemt, int itemn, int itemv, int itemr)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, itemt, itemn, itemv, itemr));
            }

            //CÓDIGO
            for (int i = 1; i < Globals.Max_BankSlots; i++)
            {
                int itemNum = PlayerStruct.player[s].bankslot[i].num;
                int itemType = PlayerStruct.player[s].bankslot[i].type;
                int itemValue = PlayerStruct.player[s].bankslot[i].value;
                int itemRefin = PlayerStruct.player[s].bankslot[i].refin;

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemv == itemValue))
                {
                    PlayerStruct.player[s].bankslot[i].type = 0;
                    PlayerStruct.player[s].bankslot[i].num = 0;
                    PlayerStruct.player[s].bankslot[i].value = 0;
                    PlayerStruct.player[s].bankslot[i].refin = 0;
                    PlayerStruct.player[s].bankslot[i].exp = 0;

                    return true;
                }

                if ((itemn == itemNum) && (itemt == itemType) && (itemr == itemRefin) && (itemv <= itemValue))
                {
                    PlayerStruct.player[s].bankslot[i].type = 0;
                    PlayerStruct.player[s].bankslot[i].num = 0;
                    PlayerStruct.player[s].bankslot[i].value -= itemv;
                    PlayerStruct.player[s].bankslot[i].refin = 0;
                    PlayerStruct.player[s].bankslot[i].exp = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
