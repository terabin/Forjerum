using System;
using System.Reflection;

namespace __Forjerum
{
    class InviteRelations
    {
        //*********************************************************************************************
        // isBusy
        // Jogador ocupado
        //*********************************************************************************************
        public static bool isBusy(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return Convert.ToBoolean(Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s));
            }

            //CÓDIGO
            if ((PlayerStruct.tempplayer[s].Inviting <= 0) && (PlayerStruct.tempplayer[s].Invited <= 0)) { return false; }
            if (PlayerStruct.tempplayer[s].InPShop > 0) { return false; }
            if (PlayerStruct.tempplayer[s].Shopping) { return false; }
            if (PlayerStruct.tempplayer[s].InBank) { return false; }
            if (PlayerStruct.tempplayer[s].InCraft) { return false; }
            if (PlayerStruct.tempplayer[s].InShop > 0) { return false; }
            if (PlayerStruct.tempplayer[s].InTrade > 0) { return false; }
            if (PlayerStruct.tempplayer[s].isDead) { return false; }
            return true;
        }
    }
}
