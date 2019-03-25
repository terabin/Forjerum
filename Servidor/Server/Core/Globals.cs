using System;

namespace __Forjerum
{
    //*********************************************************************************************
    // Valores globais utilizados pelos métodos do Core.
    // Globals.cs
    //*********************************************************************************************
    class Globals
    {
        //player high
        public static int Player_Highs = 0;
        public const int Item_Highs = 0;
        //class Basic
        public const int Max_Classes = 6;

        //map basic
        public const int InitialMap = 1;
        public const byte InitialX = 10;
        public const byte InitialY = 8;
        //equip basic
        public const string InitialArmor = "1;0";
        public const string InitialHelmet = "6;0";
        public const string InitialWeapon = "1;0";
        public const string InitialShield = "41;0";
        public const string InitialPet = "77;1;0";
        //chest basic
        //public string[] chest_1 = new string[2] { "2,2,1,0,0", "2,3,1,0,0", "2,4,1,0,0", "2,5,1,0,0", "2,6,1,0,0",
        //"2,7,1,0,0", "2,8,1,0,0", "2,9,1,0,0", "2,10,1,0,0", "2,11,1,0,0", "2,12,1,0,0", "2,13,1,0,0"};
        //msg basic
        public const int Msg_Type_Server = 1;
        public const int Msg_Type_Global = 2;
        public const int Msg_Type_Guild = 3;
        public const int Msg_Type_Map = 4;
        public const int Msg_Type_Community = 5;
        //dir
        public const byte DirUp = 8;
        public const byte DirDown = 2;
        public const byte DirLeft = 4;
        public const byte DirRight = 6;
        //tiles
        public const byte Passable = 0;
        public const byte NoPassable = 1;
        //job type
        public const int Job_Alchemist = 1;
        public const int Job_Blacksmith = 2;
        public const int Job_Miner = 3;
        public const int Job_Woodcutter = 4;
        //max values
        public const int MaxQuestRewards = 5;
        public const int MaxQuestGivers = 200;
        public const int MaxQuestPerGiver = 11;
        public const int MaxQuestItems = 11;
        public const int MaxQuestKills = 11;
        public const int MaxQuestActions = 11;
        public const int MaxPartys = 101;
        public const int MaxPlayers = 101;
        public const int MaxInvSlot = 21;
        public const int MaxChars = 3;
        public const int MaxMaps = 300;
        public const int MaxMapsX = 21;
        public const int MaxMapsY = 16;
        public const int MaxNpcs = 1001;
        public const int MaxMapNpcs = 41;
        public const int MaxMapItems = 101;
        public const int MaxNpcDrops = 5;
        public const int MaxItems = 101;
        public const int MaxSkills = 201;
        public const int MaxWeapons = 101;
        public const int MaxArmors = 101;
        public const int MaxEnemies = 1001;
        public const int MaxHotkeys = 8;
        public const int MaxPlayer_Skills = 17;
        public const int MaxParty = 101;
        public const int MaxPartyMembers = 5;
        public const int MaxTradeOffers = 9;
        public const int MaxNTempSpells = 21;
        public const int MaxPTempSpells = 21;
        public const int MaxPassiveEffects = 10;
        public const int MaxSpellBuffs = 5;
        public const int MaxClasses = 6;
        public const int Max_Npc_Spells = 5;
        public const int Max_Shops = 101;
        public const int Max_Craft = 6;
        public const int Max_CraftPoints = 100;
        public const int Max_CraftRecipes = 200;
        public const int Total_Craft = 10000;
        public const int Max_WorkPoints = 501;
        public const int Max_Profs_Per_Char = 4;
        public const int Max_Chests = 101;
        public const int Max_BankSlots = 21;
        public const int Max_BankPoints = 11;
        public const int Max_Guilds = 1001;
        public const int Max_Guild_Members = 21;
        public const int Max_TpPoints = 51;
        public const int Max_savePoints = 51;
        public const int Max_PShops = 21;
        public const int Max_Skins = 1000;
        public const int Max_Friends = 1000;
        //
        public const string NullItem = "0,0,0,0,0";
        public const int NullExp = 0;
        //cores
        public const int ColorWhite = 0;
        public const int ColorYellow = 1;
        public const int ColorBlue = 2;
        public const int ColorGreen = 3;
        public const int ColorRed = 4;
        public const int ColorBlack = 5;
        public const int ColorPink = 6;
        public const int ColorPurple = 7;
        public const int ColorTurquoise = 8;
        public const int ColorBrown = 9;
        public const int ColorSilver = 10;
        //itens
        public const int HelmetType = 1;
        public const int ArmorType = 2;
        public const int WeaponType = 3;
        public const int ShieldType = 4;
        public const int MiscType = 5;
        public const int PotType = 6;
        public const int SpellType = 7;
        //atributtes
        public const int Fire = 1;
        public const int Earth = 2;
        public const int Water = 3;
        public const int Wind = 4;
        public const int Dark = 5;
        public const int Light = 6;
        public const int None = 7;
        //equips
        public const int Helmet = 0;
        public const int Armor = 1;
        public const int Weapon = 2;
        public const int Shield = 3;
        public const int Pet = 4;
        //speeds
        public const double NormalMoveSpeed = 4.0;
        public const double SlowMoveSpeed = 3.0;
        public const double FastMoveSpeed = 5.0;
        //Quests
        public const int QuestStatus_None = 0;
        public const int QuestStatus_Progress = 1;
        public const int QuestStatus_Completed = 2;
        //Targets
        public const int Target_Player = 1;
        public const int Target_Npc = 2;
        //Action MSG
        public const int Action_Msg_Static = 0;
        public const int Action_Msg_Scroll = 1;
        public const int Action_Msg_Screen = 2;
        //Hotkey
        public const int Hotkey_Type_Skill = 1;
        public const int Hotkey_Type_Item = 2;
        //Shop
        public const int Shop_W = 7;
        //public static string MOTD = "[Mapa] wes: açai";
        public static string GAME_NAME = "";
        public static string MOTD = "";
        public static string NOTICE = "";
        //MASTER
        public static string MASTER_EMAIL = "email.com";
        //Versão atual do jogo
        public static string Client_Version = "0.8.0.1";

        //Função para pegar um valor aleatório
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        
        //*********************************************************************************************
        // Rand / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Retorna um valor aleatório baseado em um início e fim.
        //*********************************************************************************************
        public static int Rand(int r, int rr)
        {
            rr += 1;
            int randomNumber = getrandom.Next(r, rr);
            return randomNumber;
        }
    }
}
