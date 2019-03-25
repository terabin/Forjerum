using System;
using System.Reflection;

namespace __Forjerum
{
    class TradeRelations
    {
        //*********************************************************************************************
        // getPlayerTradeOffersCount
        // Retorna o número de ofertas na troca
        //*********************************************************************************************
        public static int getPlayerTradeOffersCount(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int finals = 0;

            //Checa o slot que não possúi item
            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                if ((PlayerStruct.tradeslot[s, i].item == Globals.NullItem) || (String.IsNullOrEmpty(PlayerStruct.tradeslot[s, i].item)))
                {
                    finals = i;
                    break;
                }
            }

            if (finals == 0) { finals = 9; }

            int totalcount = finals - 1;

            return totalcount;
        }
        //*********************************************************************************************
        // getFreeTradeOffer
        // Retorna um slot na oferta que esteja livre
        //*********************************************************************************************
        public static int getFreeTradeOffer(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToInt32(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            int finals = 0;

            //Checa o slot que possúi o jogador
            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                if ((PlayerStruct.tradeslot[s, i].item == Globals.NullItem) || (String.IsNullOrEmpty(PlayerStruct.tradeslot[s, i].item)))
                {
                    finals = i;
                    break;
                }
            }

            return finals;
        }
        //*********************************************************************************************
        // giveTradeTo
        // Entrega itens da troca para determinado jogador
        //*********************************************************************************************
        public static void giveTradeTo(int s, int intrade)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, intrade) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].TradeG > 0)
            {
                PlayerRelations.givePlayerGold(intrade, PlayerStruct.tempplayer[s].TradeG);
            }

            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                if ((PlayerStruct.tradeslot[s, i].item != Globals.NullItem) && (!String.IsNullOrEmpty(PlayerStruct.tradeslot[s, i].item)))
                {
                    string[] splititem = PlayerStruct.tradeslot[s, i].item.Split(',');

                    int itemNum = Convert.ToInt32(splititem[1]);
                    int itemType = Convert.ToInt32(splititem[0]);
                    int itemValue = Convert.ToInt32(splititem[2]);
                    int itemRefin = Convert.ToInt32(splititem[3]);
                    int itemExp = Convert.ToInt32(splititem[4]);

                    InventoryRelations.giveItem(intrade, itemType, itemNum, itemValue, itemRefin, itemExp);

                    PlayerStruct.tradeslot[s, i].item = Globals.NullItem;
                }
            }
        }
        //*********************************************************************************************
        // giveTrade
        // Entrega itens da troca
        //*********************************************************************************************
        public static void giveTrade(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.tempplayer[s].TradeG > 0)
            {
                PlayerRelations.givePlayerGold(s, PlayerStruct.tempplayer[s].TradeG);
            }
            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                if ((PlayerStruct.tradeslot[s, i].item != Globals.NullItem) && (!String.IsNullOrEmpty(PlayerStruct.tradeslot[s, i].item)))
                {
                    string[] splititem = PlayerStruct.tradeslot[s, i].item.Split(',');

                    int itemNum = Convert.ToInt32(splititem[1]);
                    int itemType = Convert.ToInt32(splititem[0]);
                    int itemValue = Convert.ToInt32(splititem[2]);
                    int itemRefin = Convert.ToInt32(splititem[3]);
                    int itemExp = Convert.ToInt32(splititem[4]);

                    InventoryRelations.giveItem(s, itemType, itemNum, itemValue, itemRefin, itemExp);

                    PlayerStruct.tradeslot[s, i].item = Globals.NullItem;
                }
            }
            SendData.sendInvSlots(s, PlayerStruct.player[s].SelectedChar);
        }
        //*********************************************************************************************
        // clearTempTrade
        // Limpa dados da troca de determinado jogador
        //*********************************************************************************************
        public static void clearTempTrade(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            PlayerStruct.tempplayer[s].InTrade = 0;
            PlayerStruct.tempplayer[s].TradeG = 0;
            PlayerStruct.tempplayer[s].TradeStatus = 0;

            //Limpa slot de troca
            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                PlayerStruct.tradeslot[s, i].item = Globals.NullItem;
            }
        }
    }
}
