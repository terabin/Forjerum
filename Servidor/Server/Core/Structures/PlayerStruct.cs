using System;
using System.Linq;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // Estruturas e métodos relacionados ao jogadores.
    // PlayerStruct.cs
    //*********************************************************************************************
    class PlayerStruct : Languages.LStruct
    {
        //*********************************************************************************************
        // ESTRUTURAS DO JOGADOR 
        // Se inclui também pontos de interação como hotkeys, craft, quest e outros.
        //*********************************************************************************************
        public static Player[] player = new Player[Globals.MaxPlayers];
        public static ClassData[] classes = new ClassData[Globals.MaxClasses + 1];
        public static Character[,] character = new Character[Globals.MaxPlayers, 3];
        public static TempPlayer[] tempplayer = new TempPlayer[Globals.MaxPlayers];
        public static Skill[,] skill = new Skill[Globals.MaxPlayers, 17];
        public static InvSlot[,] invslot = new InvSlot[Globals.MaxPlayers, Globals.MaxInvSlot];
        public static Craft[,] craft = new Craft[Globals.MaxPlayers, Globals.Max_Craft];
        public static QuestStatus[,,] queststatus = new QuestStatus[Globals.MaxPlayers, Globals.MaxQuestGivers, Globals.MaxQuestPerGiver];
        public static QuestKills[,,,] questkills = new QuestKills[Globals.MaxPlayers, Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestKills];
        public static QuestActions[,,,] questactions = new QuestActions[Globals.MaxPlayers, Globals.MaxQuestGivers, Globals.MaxQuestPerGiver, Globals.MaxQuestActions];
        public static TradeSlot[,] tradeslot = new TradeSlot[Globals.MaxPlayers, 17];
        public static Hotkey[,] hotkey = new Hotkey[Globals.MaxPlayers, 11];
        public static Party[] party = new Party[100];
        public static PartyMembers[,] partymembers = new PartyMembers[100, 5];
        public static PTempSpell[,] ptempspell = new PTempSpell[Globals.MaxPlayers, Globals.MaxPTempSpells];
        public static PPassiveEffect[,] ppassiveffect = new PPassiveEffect[Globals.MaxPlayers, Globals.MaxPassiveEffects];
        public static PSpellBuff[,] pspellbuff = new PSpellBuff[Globals.MaxPlayers, Globals.MaxSpellBuffs];

        public struct Player
        {
            public string Email;
            public string Password;
            public string Username;
            public string Premmy;
            public bool Confirmed;
            public string Banned;
            public int SelectedChar;
            public int WPoints;
            public BankSlot[] bankslot;
            public int skin_count;
            public bool[] skin;
            public FriendList[] friend;
        }

        public struct ClassData
        {
            public string[] sprite_name;
            public int[] sprite_s;
            public int fire;
            public int earth;
            public int water;
            public int wind;
            public int dark;
            public int light;
        }

        public struct FriendList
        {
            public string name;
            public string sprite;
            public int sprite_s;
            public int classid;
            public int level;
            public string guildname;
        }

        public struct BankSlot
        {
            public int type;
            public int num;
            public int value;
            public int refin;
            public int exp;
        }

        public struct PShopSlot
        {
            public int type;
            public int num;
            public int value;
            public int refin;
            public int price;
            public int exp;
        }

        public struct Character
        {
            public string CharacterName;
            public int Sprites;
            public int ClassId;
            public string Sprite;
            public int Level;
            public int Exp;
            public int Fire;
            public int Earth;
            public int Water;
            public int Wind;
            public int Dark;
            public int Light;
            public int Points;
            public int Map;
            public byte X;
            public byte Y;
            public byte Dir;
            public string Equipment;
            public int Vitality;
            public int Spirit;
            public int Access;
            public int SkillPoints;
            public int Gold;
            public int Cash;
            public int Hue;
            public int Gender;
            public int Guild;
            public long PVPChangeTimer;
            public long PVPBanTimer;
            public long PVPPenalty;
            public bool PVP;
            public int BootMap;
            public byte BootX;
            public byte BootY;
            //Profissões
            public int[] Prof_Type;
            public int[] Prof_Level;
            public int[] Prof_Exp;
            public bool[] Chest;
            public PShopSlot[] pshopslot;
        }

        public struct Skill
        {
            public int num;
            public int level;
            public long cooldown;
        }

        public struct InvSlot
        {
            public string item;
        }

        public struct Craft
        {
            public int type;
            public int num;
            public int value;
            public int refin;
            public int exp;
        }

        public struct QuestStatus
        {
            public int status;
        }

        public struct QuestKills
        {
            public int kills;
        }

        public struct QuestActions
        {
            public bool actiondone;
        }

        public struct Hotkey
        {
            public byte type;
            public int num;
        }

        public struct TempPlayer
        {
            public bool ingame;
            public int MaxVitality;
            public int Vitality;
            public int MaxSpirit;
            public int Spirit;
            public bool Warping;
            public byte targettype;
            public int target;
            public long skilltimer;
            public int preparingskill;
            public int preparingskillslot;
            public double  movespeed;
            public long MoveTimer;
            public long InviteTimer;
            public int Inviting;
            public int Invited;
            public int Party;
            public int InTrade;
            public int TradeG;
            public int TradeStatus;
            public long RegenTimer;
            public bool Sleeping;
            public long SleepTimer;
            public bool Stunned;
            public long StunTimer;
            public bool Slowed;
            public long SlowTimer;
            public long AttackTimer;
            public int InShop;
            public bool InCraft;
            public bool InBank;
            public int CraftType;
            public int CraftItem;
            public bool Blind;
            public long BlindTimer;
            public bool Logged;
            public long DataTimer;
            public long ChatTimer;
            public int ChatExcept;
            public long AllChatTimer;
            public bool isDead;
            public int ActivationCode;
            public bool SORE;
            public bool Reflect;
            public long ReflectTimer;
            public bool Shopping;
            public int InPShop;
            public bool isFrozen;
            public byte Sheathe;
            public byte ReduceDamage;
            public long PetTimer;
            public int PetTarget;
            public int PetTargetType;
            public int LastTarget;
            public int LastTargetType;
        }

        public struct PTempSpell
        {
            public bool active;
            public int attacker;
            public int spellnum;
            public long timer;
            public int repeats;
            public int anim;
            public int area_range;
            public bool is_line;
            public bool is_heal;
            public bool fast_buff;
        }

        public struct PPassiveEffect
        {
            public bool active;
            public int type;
            public long timer;
            public int spellnum;
            public int targettype;
            public int target;
        }

        public struct PSpellBuff
        {
            public bool active;
            public int type;
            public long timer;
            public int value; //porcentagem
        }

        public struct Party
        {
            public int leader;
            public bool active;
        }
        public struct PartyMembers
        {
            public int s;
        }
        public struct TradeSlot
        {
            public string item;
        }
        //*********************************************************************************************
        // InitializePlayerArrays
        //*********************************************************************************************
        public static void initializePlayerArrays()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            for (int i = 1; i < Globals.MaxPlayers; i++)
            {
                player[i].bankslot = new BankSlot[Globals.Max_BankSlots];
                player[i].friend = new FriendList[Globals.Max_Friends];
                for (int c = 0; c < Globals.MaxChars; c++)
                {
                    character[i, c].Prof_Type = new int[Globals.Max_Profs_Per_Char];
                    character[i, c].Prof_Level = new int[Globals.Max_Profs_Per_Char];
                    character[i, c].Prof_Exp = new int[Globals.Max_Profs_Per_Char];
                    character[i, c].Chest = new bool[Globals.Max_Chests];
                    character[i, c].pshopslot = new PShopSlot[Globals.Max_PShops];
                }
            }
        }
        //*********************************************************************************************
        // clearTempPlayer
        // Limpa informações temporárias do jogador
        //*********************************************************************************************
        public static void clearTempPlayer(int s)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s) != null)
            {
                return;
            }

            //CÓDIGO
            tempplayer[s].ingame = false;
            tempplayer[s].preparingskill = 0;
            tempplayer[s].movespeed = 0;
            tempplayer[s].Inviting = 0;
            tempplayer[s].Invited = 0;
            tempplayer[s].MaxSpirit = 0;
            tempplayer[s].MaxVitality = 0;
            tempplayer[s].Party = 0;
            tempplayer[s].skilltimer = 0;
            tempplayer[s].Spirit = 0;
            tempplayer[s].target = 0;
            tempplayer[s].targettype = 0;
            tempplayer[s].Warping = false;
            tempplayer[s].Vitality = 0;
            tempplayer[s].preparingskillslot = 0;
            tempplayer[s].InTrade = 0;
            tempplayer[s].InCraft = false;
            tempplayer[s].InBank = false;
            tempplayer[s].Stunned = false;
            tempplayer[s].Sleeping = false;
            tempplayer[s].StunTimer = 0;
            tempplayer[s].SleepTimer = 0;
            tempplayer[s].ActivationCode = 0;
            tempplayer[s].AttackTimer = 0;
            tempplayer[s].InShop = 0;
            tempplayer[s].InCraft = false;
            tempplayer[s].InBank = false;
            tempplayer[s].CraftType = 0;
            tempplayer[s].CraftItem = 0;
            tempplayer[s].Blind = false;
            tempplayer[s].BlindTimer = 0;
            tempplayer[s].Logged = false;
            tempplayer[s].DataTimer = 0;
            tempplayer[s].ChatTimer = 0;
            tempplayer[s].ChatExcept = 0;
            tempplayer[s].AllChatTimer = 0;
            tempplayer[s].isDead = false;
            tempplayer[s].ActivationCode = 0;
            tempplayer[s].SORE = false;
            tempplayer[s].Reflect = false;
            tempplayer[s].ReflectTimer = 0;
            tempplayer[s].Shopping = false;
            tempplayer[s].InPShop = 0;
            //Limpa slot de troca
            for (int i = 1; i < Globals.MaxTradeOffers; i++)
            {
                tradeslot[s, i].item = Globals.NullItem;
            }
        }

        //*********************************************************************************************
        // clearPlayer / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Limpa os dados de determinado jogador.
        //*********************************************************************************************
        public static void clearPlayer(int s, bool complete = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, complete) != null)
            {
                return;
            }

            //CÓDIGO
            //Define dados básicos sobre o personagem a ser apagado
            string username = PlayerStruct.player[Convert.ToInt32(s)].Username;
            string ID = PlayerStruct.player[Convert.ToInt32(s)].SelectedChar.ToString();
            for (int i = 0; i <= 2; i++)
            {
                PlayerStruct.character[s, i].CharacterName = null;
                PlayerStruct.character[s, i].Sprites = 0;
                PlayerStruct.character[s, i].ClassId = 0;
                PlayerStruct.character[s, i].Sprite = null;
                PlayerStruct.character[s, i].Level = 0;
                PlayerStruct.character[s, i].Exp = 0;
                PlayerStruct.character[s, i].Fire = 0;
                PlayerStruct.character[s, i].Earth = 0;
                PlayerStruct.character[s, i].Water = 0;
                PlayerStruct.character[s, i].Wind = 0;
                PlayerStruct.character[s, i].Dark = 0;
                PlayerStruct.character[s, i].Light = 0;
                PlayerStruct.character[s, i].Map = 0;
                PlayerStruct.character[s, i].X = 0;
                PlayerStruct.character[s, i].Y = 0;
                PlayerStruct.character[s, i].Dir = 0;
                PlayerStruct.character[s, i].Equipment = "0;0,0;0,0;0,0;0,0;0";
                PlayerStruct.character[s, i].Vitality = 0;
                PlayerStruct.character[s, i].Spirit = 0;
                PlayerStruct.character[s, i].Access = 0;
                PlayerStruct.character[s, i].SkillPoints = 0;
                PlayerStruct.character[s, i].Gold = 0;
                PlayerStruct.character[s, i].Cash = 0;
                PlayerStruct.character[s, i].Hue = 0;
                PlayerStruct.character[s, i].Gender = 0;
                PlayerStruct.character[s, i].Guild = 0;
                PlayerStruct.character[s, i].PVPChangeTimer = 0;
                PlayerStruct.character[s, i].PVPBanTimer = 0;
                PlayerStruct.character[s, i].PVPPenalty = 0;
                PlayerStruct.character[s, i].PVP = false;

                for (int z = 1; z < Globals.Max_Chests; z++)
                {
                    PlayerStruct.character[s, i].Chest[z] = false;
                }

                for (int z = 1; z < Globals.Max_Profs_Per_Char; z++)
                {
                    PlayerStruct.character[s, i].Prof_Type[z] = 0;
                    PlayerStruct.character[s, i].Prof_Level[z] = 0;
                    PlayerStruct.character[s, i].Prof_Exp[z] = 0;
                }

                for (int z = 1; z < Globals.Max_PShops; z++)
                {
                    PlayerStruct.character[s, i].pshopslot[z].type = 0;
                    PlayerStruct.character[s, i].pshopslot[z].num = 0;
                    PlayerStruct.character[s, i].pshopslot[z].value = 0;
                    PlayerStruct.character[s, i].pshopslot[z].refin = 0;
                    PlayerStruct.character[s, i].pshopslot[z].price = 0;
                }

            }

            //QUEST GIANT CODO
            for (int g = 1; g < Globals.MaxQuestGivers; g++)
            {
                for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                {
                    if (PlayerStruct.queststatus[s, g, q].status != 0)
                    {
                        PlayerStruct.queststatus[s, g, q].status = 0;
                        for (int k = 1; k < Globals.MaxQuestKills; k++)
                        {
                            PlayerStruct.questkills[s, g, q, k].kills = 0;
                        }
                        for (int a = 1; a < Globals.MaxQuestActions; a++)
                        {
                            PlayerStruct.questactions[s, g, q, a].actiondone = false;
                        }
                    }
                }
            }

            for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
            {
                PlayerStruct.skill[s, i].num = 0;
                PlayerStruct.skill[s, i].level = 0;
            }

            for (int i = 1; i < Globals.MaxInvSlot; i++)
            {
                PlayerStruct.invslot[s, i].item = Globals.NullItem;
            }

            for (int i = 1; i < Globals.MaxHotkeys; i++)
            {
                PlayerStruct.hotkey[s, i].type = 0;
                PlayerStruct.hotkey[s, i].num = 0;
            }

            if (complete)
            {
                PlayerStruct.player[s].Username = "";
                PlayerStruct.player[s].Password = "";
                PlayerStruct.player[s].Premmy = "";
                PlayerStruct.player[s].WPoints = 0;
                PlayerStruct.player[s].Confirmed = false;
                PlayerStruct.player[s].Email = "";
                PlayerStruct.player[s].SelectedChar = 0;
            }
        }
    }

}
