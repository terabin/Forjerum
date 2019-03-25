using System;
using System.Reflection;
using System.Linq;
using System.IO;

namespace __Forjerum.Database
{
    class Characters : Languages.LStruct
    {
        //*********************************************************************************************
        // SlotExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se o Slot para criação de personagem está livre
        //*********************************************************************************************
        public static bool slotExists(string username, string ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, username, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, username, ID));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função getFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                if (fileinfo.Name.ToLower().Contains(username + "slot" + ID)) { return true; }
            }

            return false;
        }
        //*********************************************************************************************
        // ResetAndGiveExp / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Método não é chamado em nenhum momento no servidor, serve para executar o reset do jogador
        // e devolver o nível, este método está em testes, então o risco é seu.
        //*********************************************************************************************
        public static bool ResetAndGiveExp()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função getFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                //if (fileinfo.Name.Contains(username + "slot" + ID)) { return true; }
                Console.WriteLine(fileinfo.Name);
                loadUnCompleteChar(fileinfo.Name);
                int level = PlayerStruct.character[1, 0].Level;
                int totalexp = 0;

                for (int i = 1; i < level; i++)
                {
                    double total = (i * 500) * 1.2;
                    int exp = Convert.ToInt32(total);
                    totalexp += exp;
                }

                Console.WriteLine(totalexp);
                PlayerStruct.character[1, 0].Level = 1;
                PlayerStruct.character[1, 0].Exp = totalexp;
                PlayerStruct.player[1].SelectedChar = 0;

                int s = 1;
                int classid = PlayerStruct.character[1, 0].ClassId;
                PlayerStruct.character[1, 0].SkillPoints = 0;

                if (classid == 1)
                {
                    //Prece ao vento
                    PlayerStruct.skill[s, 1].num = 2;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Onos Aroga
                    PlayerStruct.skill[s, 2].num = 6;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Etrof Otnev
                    PlayerStruct.skill[s, 3].num = 7;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Ogral Ossap
                    PlayerStruct.skill[s, 4].num = 8;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                if (classid == 2)
                {
                    //Tempo ruim
                    PlayerStruct.skill[s, 1].num = 14;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Ponto de corrupção
                    PlayerStruct.skill[s, 2].num = 16;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Ambição Arut Neva
                    PlayerStruct.skill[s, 3].num = 15;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Antes que você possa notar!
                    PlayerStruct.skill[s, 4].num = 9;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                if (classid == 3)
                {
                    //Motivação Aiprah
                    PlayerStruct.skill[s, 1].num = 4;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Julgamento Aiprah
                    PlayerStruct.skill[s, 2].num = 3;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Maldição Aiprah
                    PlayerStruct.skill[s, 3].num = 1;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Controle Aiprah
                    PlayerStruct.skill[s, 4].num = 5;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                if (classid == 4)
                {
                    //Primeiro Corte
                    PlayerStruct.skill[s, 1].num = 10;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Segundo Corte
                    PlayerStruct.skill[s, 2].num = 11;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Terceiro Corte
                    PlayerStruct.skill[s, 3].num = 12;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Daishi ni Katto
                    PlayerStruct.skill[s, 4].num = 13;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                if (classid == 5)
                {
                    //Coração Ritelf
                    PlayerStruct.skill[s, 1].num = 35;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Esmagamento
                    PlayerStruct.skill[s, 2].num = 36;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Afugentar
                    PlayerStruct.skill[s, 3].num = 38;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Contra Ataque
                    PlayerStruct.skill[s, 4].num = 37;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                if (classid == 6)
                {
                    //Benção Cani
                    PlayerStruct.skill[s, 1].num = 39;
                    PlayerStruct.skill[s, 1].level = 0;

                    //Dança da Folha
                    PlayerStruct.skill[s, 2].num = 40;
                    PlayerStruct.skill[s, 2].level = 0;

                    //Empolgação
                    PlayerStruct.skill[s, 3].num = 41;
                    PlayerStruct.skill[s, 3].level = 0;

                    //Masterização
                    PlayerStruct.skill[s, 4].num = 42;
                    PlayerStruct.skill[s, 4].level = 0;
                }

                int extrafire = 0;
                int extraearth = 0;
                int extrawater = 0;
                int extrawind = 0;
                int extradark = 0;
                int extralight = 0;

                if (classid == 1)
                {
                    extrafire = 3;
                    extraearth = 4;
                    extrawater = 1;
                    extrawind = 5;
                    extradark = 16;
                    extralight = 11;
                }

                if (classid == 2)
                {
                    extrafire = 7;
                    extraearth = 12;
                    extrawater = 2;
                    extrawind = 8;
                    extradark = 8;
                    extralight = 3;
                }

                if (classid == 3)
                {
                    extrafire = 5;
                    extraearth = 8;
                    extrawater = 2;
                    extrawind = 5;
                    extradark = 10;
                    extralight = 10;
                }

                if (classid == 4)
                {
                    extrafire = 5;
                    extraearth = 5;
                    extrawater = 12;
                    extrawind = 10;
                    extradark = 6;
                    extralight = 2;
                }


                if (classid == 5)
                {
                    extrafire = 4;
                    extraearth = 15;
                    extrawater = 1;
                    extrawind = 1;
                    extradark = 4;
                    extralight = 15;
                }

                if (classid == 6)
                {
                    extrafire = 4;
                    extraearth = 3;
                    extrawater = 10;
                    extrawind = 20;
                    extradark = 1;
                    extralight = 2;
                }

                PlayerStruct.character[1, 0].Fire = extrafire;
                PlayerStruct.character[1, 0].Earth = extraearth;
                PlayerStruct.character[1, 0].Water = extrawater;
                PlayerStruct.character[1, 0].Wind = extrawind;
                PlayerStruct.character[1, 0].Light = extralight;
                PlayerStruct.character[1, 0].Dark = extradark;

                PlayerStruct.character[1, 0].Points = 16;

                unsaveCharacter(1, fileinfo.Name);
            }

            return false;
        }
        //*********************************************************************************************
        // UnsaveCharacter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Complemento do sistema de resetar e devolver exp.
        //*********************************************************************************************
        public static void unsaveCharacter(int s, string filename, bool isnewchar = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, filename, isnewchar) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName != null)
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/convert/" + filename) == false)
                {
                    //representa o arquivo que vamos criar
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/convert/" + filename, FileMode.Create);

                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //Define as váriaveis a serem salvas
                    string charName = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName);
                    int charSprites = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites);
                    int charClass = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId);
                    string charSprite = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite);
                    int charLevel = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level);
                    int charExp = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp);
                    int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
                    int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
                    int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
                    int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
                    int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
                    int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
                    int charPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points);
                    int charMap = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                    byte charX = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
                    byte charY = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
                    byte charDir = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir);
                    string Equipment = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment);
                    int Vitality = (PlayerStruct.tempplayer[Convert.ToInt32(s)].Vitality);
                    int Spirit = (PlayerStruct.tempplayer[Convert.ToInt32(s)].Spirit);
                    int Access = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access);
                    int SkillPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints);
                    int Gold = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold);
                    int Cash = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Cash);
                    int Hue = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue);
                    int Gender = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gender);
                    int Guild = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild);

                    //PVP
                    long PVPChangeTimer;
                    long PVPBanTimer;
                    long PVPPenalty;
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer > 0)
                    {
                        PVPChangeTimer = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPChangeTimer < 0) { PVPChangeTimer = 0; }
                    }
                    else { PVPChangeTimer = 0; }
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer > 0)
                    {
                        PVPBanTimer = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPBanTimer < 0) { PVPBanTimer = 0; }
                    }
                    else { PVPBanTimer = 0; }
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty > 0)
                    {
                        PVPPenalty = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPPenalty < 0) { PVPPenalty = 0; }
                    }
                    else { PVPPenalty = 0; }

                    bool PVP = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP;

                    //QUEST
                    int questcount = QuestRelations.getPlayerQuestsCount(s);

                    if (isnewchar)
                    {
                        int totalpoints = Convert.ToInt32(charFire) + Convert.ToInt32(charEarth) + Convert.ToInt32(charWater) + Convert.ToInt32(charWind) + Convert.ToInt32(charDark) + Convert.ToInt32(charLight);
                        if (totalpoints != 16) { return; }
                    }

                    //grava os dados no arquivo
                    bw.Write(charName);
                    bw.Write(Guild);
                    bw.Write(charSprites);
                    bw.Write(charClass);
                    bw.Write(charSprite);
                    bw.Write(charLevel);
                    bw.Write(charExp);
                    bw.Write(charFire);
                    bw.Write(charEarth);
                    bw.Write(charWater);
                    bw.Write(charWind);
                    bw.Write(charDark);
                    bw.Write(charLight);
                    bw.Write(charPoints);
                    bw.Write(charMap);
                    bw.Write(charX);
                    bw.Write(charY);
                    bw.Write(charDir);
                    bw.Write(Equipment);
                    bw.Write(Vitality);
                    bw.Write(Spirit);
                    bw.Write(Access);
                    bw.Write(SkillPoints);
                    bw.Write(Gold);
                    bw.Write(Cash);
                    bw.Write(Hue);
                    bw.Write(Gender);
                    bw.Write(PVPChangeTimer);
                    bw.Write(PVPBanTimer);
                    bw.Write(PVPPenalty);
                    bw.Write(PVP);

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    bw.Write(questcount);

                    for (int g = 1; g < Globals.MaxQuestGivers; g++)
                    {
                        for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                        {
                            if (PlayerStruct.queststatus[s, g, q].status != 0)
                            {
                                bw.Write(g);
                                bw.Write(q);
                                bw.Write(PlayerStruct.queststatus[s, g, q].status);
                                for (int k = 1; k < Globals.MaxQuestKills; k++)
                                {
                                    bw.Write(PlayerStruct.questkills[s, g, q, k].kills);
                                }
                                for (int a = 1; a < Globals.MaxQuestActions; a++)
                                {
                                    bw.Write(PlayerStruct.questactions[s, g, q, a].actiondone);
                                }
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PlayerStruct.skill[s, i].num);
                        bw.Write(PlayerStruct.skill[s, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PlayerStruct.invslot[s, i].item);
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PlayerStruct.hotkey[s, i].type);
                        bw.Write(PlayerStruct.hotkey[s, i].num);
                    }

                    bw.Close();
                }
            }
        }
        //*********************************************************************************************
        // loadUnCompleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Complemento do sistema de resetar e devolver exp.
        //*********************************************************************************************
        public static bool loadUnCompleteChar(string filename)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, filename) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, filename));
            }

            //CÓDIGO
            int s = 1;
            int ID = 0;
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + filename) == true)
            {
                //Representa o arquivo que vamos abrir

                FileStream file;

                try
                {
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + filename, FileMode.Open);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PlayerStruct.character[s, ID].CharacterName = br.ReadString();
                    PlayerStruct.character[s, ID].Guild = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprites = br.ReadInt32();
                    PlayerStruct.character[s, ID].ClassId = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprite = br.ReadString();
                    PlayerStruct.character[s, ID].Level = br.ReadInt32();
                    PlayerStruct.character[s, ID].Exp = br.ReadInt32();
                    PlayerStruct.character[s, ID].Fire = br.ReadInt32();
                    PlayerStruct.character[s, ID].Earth = br.ReadInt32();
                    PlayerStruct.character[s, ID].Water = br.ReadInt32();
                    PlayerStruct.character[s, ID].Wind = br.ReadInt32();
                    PlayerStruct.character[s, ID].Dark = br.ReadInt32();
                    PlayerStruct.character[s, ID].Light = br.ReadInt32();
                    PlayerStruct.character[s, ID].Points = br.ReadInt32();
                    PlayerStruct.character[s, ID].Map = br.ReadInt32();
                    PlayerStruct.character[s, ID].X = br.ReadByte();
                    PlayerStruct.character[s, ID].Y = br.ReadByte();
                    PlayerStruct.character[s, ID].Dir = br.ReadByte();
                    PlayerStruct.character[s, ID].Equipment = br.ReadString();
                    PlayerStruct.character[s, ID].Vitality = br.ReadInt32();
                    PlayerStruct.character[s, ID].Spirit = br.ReadInt32();
                    PlayerStruct.character[s, ID].Access = br.ReadInt32();
                    PlayerStruct.character[s, ID].SkillPoints = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gold = br.ReadInt32();
                    PlayerStruct.character[s, ID].Cash = br.ReadInt32();
                    PlayerStruct.character[s, ID].Hue = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gender = br.ReadInt32();
                    PlayerStruct.character[s, ID].PVPChangeTimer = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVPBanTimer = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVPPenalty = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVP = br.ReadBoolean();

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    int questcount = br.ReadInt32();
                    int g = 0;
                    int z = 0;

                    for (int q = 1; q <= questcount; q++)
                    {
                        {
                            g = br.ReadInt32();
                            z = br.ReadInt32();
                            PlayerStruct.queststatus[s, g, z].status = br.ReadInt32();
                            for (int k = 1; k < Globals.MaxQuestKills; k++)
                            {
                                PlayerStruct.questkills[s, g, z, k].kills = br.ReadInt32();
                            }
                            for (int a = 1; a < Globals.MaxQuestActions; a++)
                            {
                                PlayerStruct.questactions[s, g, z, a].actiondone = br.ReadBoolean();
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        PlayerStruct.character[s, ID].Chest[i] = br.ReadBoolean();
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        PlayerStruct.character[s, ID].Prof_Type[i] = br.ReadInt32();
                        PlayerStruct.character[s, ID].Prof_Level[i] = br.ReadInt32();
                        PlayerStruct.character[s, ID].Prof_Exp[i] = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        PlayerStruct.skill[s, i].num = br.ReadInt32();
                        PlayerStruct.skill[s, i].level = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PlayerStruct.invslot[s, i].item = br.ReadString();
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PlayerStruct.hotkey[s, i].type = br.ReadByte();
                        PlayerStruct.hotkey[s, i].num = br.ReadInt32();
                    }

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed); return false; }
        }
        //*********************************************************************************************
        // CharExists / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Checa se determinado personagem já existe.
        //*********************************************************************************************
        public static bool charExists(string name)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, name) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, name));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função getFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                for (int i = 0; i <= Globals.MaxChars; i++)
                {
                    if (fileinfo.Name.ToLower().Contains("slot" + i + " - " + name.ToLower() + ".dat")) { return true; }
                }

            }

            return false;
        }
        //*********************************************************************************************
        // getCharBySlot / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static string getCharBySlot(string username, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, username, ID) != null)
            {
                return Convert.ToString
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, username, ID));
            }

            //CÓDIGO
            //Marca o diretório a ser listado
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/");

            //Executa função getFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Archives = directory.GetFiles("*.*");

            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Archives)
            {
                if (fileinfo.Name.Contains(username + "slot" + ID)) { return fileinfo.Name; }
            }
            Console.WriteLine(username);
            return null;
        }
        //*********************************************************************************************
        // CreateNewChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Cria um novo personagem com os parâmetros determinados.
        //*********************************************************************************************
        public static bool createNewChar(int s, string email, int ID, string name, int classid, int fire, int earth, int water, int wind, int dark, int light, int hue, int gender)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, email, ID, name, classid, fire, earth, water, wind, dark, light, hue, gender) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, email, ID, name, classid, fire, earth, water, wind, dark, light, hue, gender));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat") == false)
            {
                FileStream file;

                try
                {
                    //representa o arquivo que vamos criar
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat", FileMode.Create);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    Console.WriteLine(name);
                    return false;
                }

                try
                {
                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //incorrect class
                    if ((classid > Globals.MaxClasses) || (classid < 1)) { return false; }

                    if ((hue < 0) || (hue > 500)) { return false; }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PlayerStruct.invslot[s, i].item = Globals.NullItem;
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PlayerStruct.character[s, ID].pshopslot[i].num = 0;
                        PlayerStruct.character[s, ID].pshopslot[i].type = 0;
                        PlayerStruct.character[s, ID].pshopslot[i].value = 0;
                        PlayerStruct.character[s, ID].pshopslot[i].refin = 0;
                        PlayerStruct.character[s, ID].pshopslot[i].price = 0;
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PlayerStruct.hotkey[s, i].type = 0;
                        PlayerStruct.hotkey[s, i].num = 0;
                    }

                    int totalpoints = fire + earth + water + wind + dark + light;
                    if (totalpoints != 16)
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + email + "slot" + ID + " - " + name + ".dat");
                        }
                        SendData.sendNStatus(s, "Falha ao criar personagem, por favor, recomece.");
                        return false;
                    }

                    string sprite = PlayerStruct.classes[classid].sprite_name[gender];
                    int sprites = PlayerStruct.classes[classid].sprite_s[gender];

                    int extrafire = PlayerStruct.classes[classid].fire;
                    int extraearth = PlayerStruct.classes[classid].earth;
                    int extrawater = PlayerStruct.classes[classid].water;
                    int extrawind = PlayerStruct.classes[classid].wind;
                    int extradark = PlayerStruct.classes[classid].dark;
                    int extralight = PlayerStruct.classes[classid].light;

                    //grava os dados no arquivo
                    bw.Write(name);
                    bw.Write(0);
                    bw.Write(sprites);
                    bw.Write(classid);
                    bw.Write(sprite);
                    bw.Write(1);
                    bw.Write(0);
                    bw.Write(fire + extrafire);
                    bw.Write(earth + extraearth);
                    bw.Write(water + extrawater);
                    bw.Write(wind + extrawind);
                    bw.Write(dark + extradark);
                    bw.Write(light + extralight);
                    bw.Write(0);
                    bw.Write(Globals.InitialMap);
                    bw.Write(Globals.InitialX);
                    bw.Write(Globals.InitialY);
                    bw.Write(Globals.InitialMap);
                    bw.Write(Globals.InitialX);
                    bw.Write(Globals.InitialY);
                    bw.Write(Globals.DirDown);
                    bw.Write(Globals.InitialHelmet + "," + Globals.InitialArmor + "," + Globals.InitialWeapon + "," + Globals.InitialShield + "," + Globals.InitialPet);
                    bw.Write(200);
                    bw.Write(200);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(hue);
                    bw.Write(gender);
                    bw.Write(0);
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(Convert.ToInt64(0));
                    bw.Write(false);

                    if (classid == 1)
                    {
                        //Incendiar
                        PlayerStruct.skill[s, 1].num = 44;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Onos Aroga
                        PlayerStruct.skill[s, 2].num = 6;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Etrof Otnev
                        PlayerStruct.skill[s, 3].num = 8;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Ogral Ossap
                        PlayerStruct.skill[s, 4].num = 7;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Aprimoramento Mágico
                        PlayerStruct.skill[s, 5].num = 46;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Estrela congelante
                        PlayerStruct.skill[s, 6].num = 45;
                        PlayerStruct.skill[s, 6].level = 0;


                    }

                    if (classid == 2)
                    {
                        //Tempo ruim
                        PlayerStruct.skill[s, 1].num = 14;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Aprimoramento Vital
                        PlayerStruct.skill[s, 2].num = 52;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Ponto de corrupção
                        PlayerStruct.skill[s, 3].num = 16;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Ambição Arut Neva
                        PlayerStruct.skill[s, 4].num = 15;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Manipulação Vital
                        PlayerStruct.skill[s, 5].num = 53;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Antes que você possa notar!
                        PlayerStruct.skill[s, 6].num = 9;
                        PlayerStruct.skill[s, 6].level = 0;
                    }

                    if (classid == 3)
                    {
                        //Lembrança do Deserto
                        PlayerStruct.skill[s, 1].num = 55;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Motivação Aiprah
                        PlayerStruct.skill[s, 2].num = 4;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Julgamento Aiprah
                        PlayerStruct.skill[s, 3].num = 3;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Maldição Aiprah
                        PlayerStruct.skill[s, 4].num = 1;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Filhos da Areia
                        PlayerStruct.skill[s, 5].num = 56;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Controle Aiprah
                        PlayerStruct.skill[s, 6].num = 5;
                        PlayerStruct.skill[s, 6].level = 0;
                    }

                    if (classid == 4)
                    {
                        //Primeiro Corte
                        PlayerStruct.skill[s, 1].num = 10;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Segundo Corte
                        PlayerStruct.skill[s, 2].num = 11;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Embainhar
                        PlayerStruct.skill[s, 3].num = 47;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Terceiro Corte
                        PlayerStruct.skill[s, 4].num = 12;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Laço de vida
                        PlayerStruct.skill[s, 5].num = 48;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Daishi ni Katto
                        PlayerStruct.skill[s, 6].num = 13;
                        PlayerStruct.skill[s, 6].level = 0;
                    }

                    if (classid == 5)
                    {
                        //Lâmina Ritelf
                        PlayerStruct.skill[s, 1].num = 54;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Coração Ritelf
                        PlayerStruct.skill[s, 2].num = 35;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Esmagamento
                        PlayerStruct.skill[s, 3].num = 36;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Afugentar
                        PlayerStruct.skill[s, 4].num = 38;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Cortes Gêmeos
                        PlayerStruct.skill[s, 5].num = 51;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Contra Ataque
                        PlayerStruct.skill[s, 6].num = 37;
                        PlayerStruct.skill[s, 6].level = 0;
                    }

                    if (classid == 6)
                    {
                        //Benção Cani
                        PlayerStruct.skill[s, 1].num = 39;
                        PlayerStruct.skill[s, 1].level = 0;

                        //Dança da Folha
                        PlayerStruct.skill[s, 2].num = 40;
                        PlayerStruct.skill[s, 2].level = 0;

                        //Fluxo da Alma
                        PlayerStruct.skill[s, 3].num = 50;
                        PlayerStruct.skill[s, 3].level = 0;

                        //Lua Nova
                        PlayerStruct.skill[s, 4].num = 49;
                        PlayerStruct.skill[s, 4].level = 0;

                        //Empolgação
                        PlayerStruct.skill[s, 5].num = 41;
                        PlayerStruct.skill[s, 5].level = 0;

                        //Masterização
                        PlayerStruct.skill[s, 6].num = 42;
                        PlayerStruct.skill[s, 6].level = 0;
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PlayerStruct.skill[s, i].num);
                        bw.Write(PlayerStruct.skill[s, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PlayerStruct.invslot[s, i].item.ToString());
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].type);
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].num);
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].refin);
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].value);
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].exp);
                        bw.Write(PlayerStruct.character[s, ID].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PlayerStruct.hotkey[s, i].type);
                        bw.Write(PlayerStruct.hotkey[s, i].num);
                    }

                    bw.Close();

                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { return false; }
        }
        //*********************************************************************************************
        // loadShowChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Carrega o personagem apenas para ser mostrado na seleção.
        //*********************************************************************************************
        public static bool loadShowChar(int s, string email, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, email, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, email, ID));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID)) == true)
            {
                FileStream file;

                try
                {
                    //Representa o arquivo que vamos abrir
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID), FileMode.Open);

                }
                catch
                {
                    Console.WriteLine(lang.load_show_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PlayerStruct.character[s, ID].CharacterName = br.ReadString();
                    PlayerStruct.character[s, ID].Guild = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprites = br.ReadInt32();
                    PlayerStruct.character[s, ID].ClassId = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprite = br.ReadString();
                    PlayerStruct.character[s, ID].Level = br.ReadInt32();
                    PlayerStruct.character[s, ID].Exp = br.ReadInt32();
                    PlayerStruct.character[s, ID].Fire = br.ReadInt32();
                    PlayerStruct.character[s, ID].Earth = br.ReadInt32();
                    PlayerStruct.character[s, ID].Water = br.ReadInt32();
                    PlayerStruct.character[s, ID].Wind = br.ReadInt32();
                    PlayerStruct.character[s, ID].Dark = br.ReadInt32();
                    PlayerStruct.character[s, ID].Light = br.ReadInt32();
                    PlayerStruct.character[s, ID].Points = br.ReadInt32();
                    PlayerStruct.character[s, ID].Map = br.ReadInt32();
                    PlayerStruct.character[s, ID].X = br.ReadByte();
                    PlayerStruct.character[s, ID].Y = br.ReadByte();
                    PlayerStruct.character[s, ID].BootMap = br.ReadInt32();
                    PlayerStruct.character[s, ID].BootX = br.ReadByte();
                    PlayerStruct.character[s, ID].BootY = br.ReadByte();
                    PlayerStruct.character[s, ID].Dir = br.ReadByte();
                    PlayerStruct.character[s, ID].Equipment = br.ReadString();
                    PlayerStruct.character[s, ID].Vitality = br.ReadInt32();
                    PlayerStruct.character[s, ID].Spirit = br.ReadInt32();
                    PlayerStruct.character[s, ID].Access = br.ReadInt32();
                    PlayerStruct.character[s, ID].SkillPoints = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gold = br.ReadInt32();
                    PlayerStruct.character[s, ID].Cash = br.ReadInt32();
                    PlayerStruct.character[s, ID].Hue = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gender = br.ReadInt32();

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed); return false; }
        }
        //*********************************************************************************************
        // DeleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Deletar determinado personagem.
        //*********************************************************************************************
        public static bool deleteChar(int s, int slot)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, slot) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, slot));
            }

            //CÓDIGO
            try
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(PlayerStruct.player[s].Email, slot)) == true)
                {
                    FileStream file;

                    try
                    {
                        //Representa o arquivo que vamos abrir
                        file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(PlayerStruct.player[s].Email, slot), FileMode.Open);

                    }
                    catch
                    {
                        return false;
                    }

                    try
                    {
                        //Cria o leitor do arquivo
                        BinaryReader br = new BinaryReader(file);

                        //Lê os dados no arquivo
                        string name = br.ReadString();
                        int guild = br.ReadInt32();

                        if (guild <= 0)
                        {
                            br.Close();
                        }
                        else
                        {
                            //Retira o idiota da guilda
                            for (int i = 1; i < Globals.Max_Guild_Members; i++)
                            {
                                if (GuildStruct.guild[guild].memberlist[i] == name)
                                {
                                    GuildStruct.guild[guild].memberlist[i] = "";
                                    GuildStruct.guild[guild].membersprite[i] = "";
                                    GuildStruct.guild[guild].membersprite_s[i] = 0;
                                }
                            }

                            Guilds.saveGuild(guild.ToString());

                            SendData.sendCompleteGuildToGuildG(guild);
                            SendData.sendMsgToGuild(guild, lang.the_player + " " + name + " " + lang.has_left_the_guild, Globals.ColorWhite, Globals.Msg_Type_Server);

                            br.Close();
                        }
                    }
                    catch
                    { return false; }
                }
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(PlayerStruct.player[s].Email, slot));
                return true;
            }
            catch
            {
                Console.WriteLine(lang.error);
                return false;
            }
        }
        //*********************************************************************************************
        // loadCompleteChar / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool loadCompleteChar(int s, string email, int ID)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, email, ID) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, s, email, ID));
            }

            //CÓDIGO
            //Verifica se o arquivo existe
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID)) == true)
            {
                //Representa o arquivo que vamos abrir

                FileStream file;

                try
                {
                    file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID), FileMode.Open);
                }
                catch
                {
                    Console.WriteLine(lang.load_char_error);
                    Console.WriteLine(email);
                    Console.WriteLine(ID);
                    Console.WriteLine(lang.character_deleted);
                    return false;
                }

                try
                {
                    //Cria o leitor do arquivo
                    BinaryReader br = new BinaryReader(file);

                    //Lê os dados no arquivo
                    PlayerStruct.character[s, ID].CharacterName = br.ReadString();
                    PlayerStruct.character[s, ID].Guild = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprites = br.ReadInt32();
                    PlayerStruct.character[s, ID].ClassId = br.ReadInt32();
                    PlayerStruct.character[s, ID].Sprite = br.ReadString();
                    PlayerStruct.character[s, ID].Level = br.ReadInt32();
                    PlayerStruct.character[s, ID].Exp = br.ReadInt32();
                    PlayerStruct.character[s, ID].Fire = br.ReadInt32();
                    PlayerStruct.character[s, ID].Earth = br.ReadInt32();
                    PlayerStruct.character[s, ID].Water = br.ReadInt32();
                    PlayerStruct.character[s, ID].Wind = br.ReadInt32();
                    PlayerStruct.character[s, ID].Dark = br.ReadInt32();
                    PlayerStruct.character[s, ID].Light = br.ReadInt32();
                    PlayerStruct.character[s, ID].Points = br.ReadInt32();
                    PlayerStruct.character[s, ID].Map = br.ReadInt32();
                    PlayerStruct.character[s, ID].X = br.ReadByte();
                    PlayerStruct.character[s, ID].Y = br.ReadByte();
                    PlayerStruct.character[s, ID].BootMap = br.ReadInt32();
                    PlayerStruct.character[s, ID].BootX = br.ReadByte();
                    PlayerStruct.character[s, ID].BootY = br.ReadByte();
                    PlayerStruct.character[s, ID].Dir = br.ReadByte();
                    PlayerStruct.character[s, ID].Equipment = br.ReadString();
                    PlayerStruct.character[s, ID].Vitality = br.ReadInt32();
                    PlayerStruct.character[s, ID].Spirit = br.ReadInt32();
                    PlayerStruct.character[s, ID].Access = br.ReadInt32();
                    PlayerStruct.character[s, ID].SkillPoints = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gold = br.ReadInt32();
                    PlayerStruct.character[s, ID].Cash = br.ReadInt32();
                    PlayerStruct.character[s, ID].Hue = br.ReadInt32();
                    PlayerStruct.character[s, ID].Gender = br.ReadInt32();
                    PlayerStruct.character[s, ID].PVPChangeTimer = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVPBanTimer = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVPPenalty = br.ReadInt64();
                    PlayerStruct.character[s, ID].PVP = br.ReadBoolean();

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    int questcount = br.ReadInt32();
                    int g = 0;
                    int z = 0;

                    for (int q = 1; q <= questcount; q++)
                    {
                        {
                            g = br.ReadInt32();
                            z = br.ReadInt32();
                            PlayerStruct.queststatus[s, g, z].status = br.ReadInt32();
                            for (int k = 1; k < Globals.MaxQuestKills; k++)
                            {
                                PlayerStruct.questkills[s, g, z, k].kills = br.ReadInt32();
                            }
                            for (int a = 1; a < Globals.MaxQuestActions; a++)
                            {
                                PlayerStruct.questactions[s, g, z, a].actiondone = br.ReadBoolean();
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        PlayerStruct.character[s, ID].Chest[i] = br.ReadBoolean();
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        PlayerStruct.character[s, ID].Prof_Type[i] = br.ReadInt32();
                        PlayerStruct.character[s, ID].Prof_Level[i] = br.ReadInt32();
                        PlayerStruct.character[s, ID].Prof_Exp[i] = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        PlayerStruct.skill[s, i].num = br.ReadInt32();
                        PlayerStruct.skill[s, i].level = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        PlayerStruct.invslot[s, i].item = br.ReadString();
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].exp = br.ReadInt32();
                        PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price = br.ReadInt32();
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        PlayerStruct.hotkey[s, i].type = br.ReadByte();
                        PlayerStruct.hotkey[s, i].num = br.ReadInt32();
                    }

                    br.Close();
                    //Retorna que deu tudo certo
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { Console.WriteLine(lang.failed); return false; }
        }
        //*********************************************************************************************
        // UpdateCharData / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static bool updateCharData(string user, string name, string data)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, user, name, data) != null)
            {
                return Convert.ToBoolean
                (Extensions.ExtensionApp.extendMyApp(MethodBase.GetCurrentMethod().Name, user, name, data));
            }

            //CÓDIGO
            try
            {
                //DeleteChar(name, data[0]);
                //CreateNewChar(user, name, data);
                return true;
            }
            catch { return false; }
        }
        //*********************************************************************************************
        // saveCharacter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void saveCharacter(int s, string email, int ID, bool isnewchar = false)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, s, email, ID, isnewchar) != null)
            {
                return;
            }

            //CÓDIGO
            if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName != null)
            {
                //Verifica se o arquivo existe
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID)) == true)
                {
                    //representa o arquivo que vamos criar
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data/Chars/" + getCharBySlot(email, ID), FileMode.Create);

                    //Definimos o escrivão do arquivo. hue
                    BinaryWriter bw = new BinaryWriter(file);

                    //Define as váriaveis a serem salvas
                    string charName = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].CharacterName);
                    int charSprites = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprites);
                    int charClass = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].ClassId);
                    string charSprite = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Sprite);
                    int charLevel = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Level);
                    int charExp = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Exp);
                    int charFire = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Fire);
                    int charEarth = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Earth);
                    int charWater = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Water);
                    int charWind = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Wind);
                    int charDark = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dark);
                    int charLight = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Light);
                    int charPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Points);
                    int charMap = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Map);
                    byte charX = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].X);
                    byte charY = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Y);
                    int charBootMap = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootMap);
                    byte charBootX = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootX);
                    byte charBootY = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].BootY);
                    byte charDir = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Dir);
                    string Equipment = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Equipment);
                    int Vitality = (PlayerStruct.tempplayer[Convert.ToInt32(s)].Vitality);
                    int Spirit = (PlayerStruct.tempplayer[Convert.ToInt32(s)].Spirit);
                    int Access = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Access);
                    int SkillPoints = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].SkillPoints);
                    int Gold = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gold);
                    int Cash = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Cash);
                    int Hue = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Hue);
                    int Gender = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Gender);
                    int Guild = (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Guild);

                    //PVP
                    long PVPChangeTimer;
                    long PVPBanTimer;
                    long PVPPenalty;
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer > 0)
                    {
                        PVPChangeTimer = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPChangeTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPChangeTimer < 0) { PVPChangeTimer = 0; }
                    }
                    else { PVPChangeTimer = 0; }
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer > 0)
                    {
                        PVPBanTimer = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPBanTimer - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPBanTimer < 0) { PVPBanTimer = 0; }
                    }
                    else { PVPBanTimer = 0; }
                    if (PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty > 0)
                    {
                        PVPPenalty = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVPPenalty - Loops.TickCount.ElapsedMilliseconds;
                        if (PVPPenalty < 0) { PVPPenalty = 0; }
                    }
                    else { PVPPenalty = 0; }

                    bool PVP = PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].PVP;

                    //QUEST
                    int questcount = QuestRelations.getPlayerQuestsCount(s);

                    if (isnewchar)
                    {
                        int totalpoints = Convert.ToInt32(charFire) + Convert.ToInt32(charEarth) + Convert.ToInt32(charWater) + Convert.ToInt32(charWind) + Convert.ToInt32(charDark) + Convert.ToInt32(charLight);
                        if (totalpoints != 16) { return; }
                    }

                    //grava os dados no arquivo
                    bw.Write(charName);
                    bw.Write(Guild);
                    bw.Write(charSprites);
                    bw.Write(charClass);
                    bw.Write(charSprite);
                    bw.Write(charLevel);
                    bw.Write(charExp);
                    bw.Write(charFire);
                    bw.Write(charEarth);
                    bw.Write(charWater);
                    bw.Write(charWind);
                    bw.Write(charDark);
                    bw.Write(charLight);
                    bw.Write(charPoints);
                    bw.Write(charMap);
                    bw.Write(charX);
                    bw.Write(charY);
                    bw.Write(charBootMap);
                    bw.Write(charBootX);
                    bw.Write(charBootY);
                    bw.Write(charDir);
                    bw.Write(Equipment);
                    bw.Write(Vitality);
                    bw.Write(Spirit);
                    bw.Write(Access);
                    bw.Write(SkillPoints);
                    bw.Write(Gold);
                    bw.Write(Cash);
                    bw.Write(Hue);
                    bw.Write(Gender);
                    bw.Write(PVPChangeTimer);
                    bw.Write(PVPBanTimer);
                    bw.Write(PVPPenalty);
                    bw.Write(PVP);

                    //Salvar missões (GIANT CODO) ~~ Precisa de aprimoramento
                    bw.Write(questcount);

                    for (int g = 1; g < Globals.MaxQuestGivers; g++)
                    {
                        for (int q = 1; q < Globals.MaxQuestPerGiver; q++)
                        {
                            if (PlayerStruct.queststatus[s, g, q].status != 0)
                            {
                                bw.Write(g);
                                bw.Write(q);
                                bw.Write(PlayerStruct.queststatus[s, g, q].status);
                                for (int k = 1; k < Globals.MaxQuestKills; k++)
                                {
                                    bw.Write(PlayerStruct.questkills[s, g, q, k].kills);
                                }
                                for (int a = 1; a < Globals.MaxQuestActions; a++)
                                {
                                    bw.Write(PlayerStruct.questactions[s, g, q, a].actiondone);
                                }
                            }
                        }
                    }

                    for (int i = 1; i < Globals.Max_Chests; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Chest[i]);
                    }

                    for (int i = 1; i < Globals.Max_Profs_Per_Char; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Type[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Level[i]);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].Prof_Exp[i]);
                    }

                    for (int i = 1; i < Globals.MaxPlayer_Skills; i++)
                    {
                        bw.Write(PlayerStruct.skill[s, i].num);
                        bw.Write(PlayerStruct.skill[s, i].level);
                    }

                    for (int i = 1; i < Globals.MaxInvSlot; i++)
                    {
                        bw.Write(PlayerStruct.invslot[s, i].item);
                    }

                    for (int i = 1; i < Globals.Max_PShops; i++)
                    {
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].type);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].num);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].refin);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].value);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].exp);
                        bw.Write(PlayerStruct.character[s, PlayerStruct.player[s].SelectedChar].pshopslot[i].price);
                    }

                    for (int i = 1; i < Globals.MaxHotkeys; i++)
                    {
                        bw.Write(PlayerStruct.hotkey[s, i].type);
                        bw.Write(PlayerStruct.hotkey[s, i].num);
                    }

                    bw.Close();
                }
            }
        }
    }
}
