using System;
using System.Reflection;

namespace __Forjerum
{
    //*********************************************************************************************
    // FORJERUM - Forjerum BETA 1!
    // Autor: Allyson de Souza Bacon - Todos os direitos reservados
    //
    // Não se esqueça de respeitar a licença do projeto.
    // https://creativecommons.org/licenses/by-sa/4.0/
    // 
    // O desenvolvimento deste projeto se deve completamente ao seu autor e a equipe 4Tabern, para
    // encontrar novidades, aulas e extensões, não deixe de visitar o site http://4tabern.com.
    //
    // Copyright (C) 2016 Allyson S. Bacon
    //*********************************************************************************************
    //*********************************************************************************************
    // Início do servidor.
    // start.cs
    //*********************************************************************************************
    class start : Languages.LStruct
    {
        // Usado para valores aleatórios
        public static Random rdn;

        //*********************************************************************************************
        // Main / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Iniciamos o servidor aqui.
        //*********************************************************************************************
        static void Main(string[] args)
        {
            loadLang();
            Console.WriteLine("");
            Console.WriteLine("///////////////////////////////////////////////////////");
            Console.WriteLine("/                                                     /");
            Console.WriteLine(@"/        ___          _                               /");
            Console.WriteLine(@"/       / __\__  _ __(_) ___ _ __ _   _ _ __ ___      /");
            Console.WriteLine(@"/      / _\/ _ \| '__| |/ _ \ '__| | | | '_ ` _ \     /");
            Console.WriteLine(@"/     / / | (_) | |  | |  __/ |  | |_| | | | | | |    /");
            Console.WriteLine(@"/     \/   \___/|_| _/ |\___|_|   \__,_|_| |_| |_|    /");
            Console.WriteLine(@"/                  |__/                               /");
            Console.WriteLine("/                                                     /");
            Console.WriteLine("/                 http://4tabern.com                  /");
            Console.WriteLine("/         Copyright (C) 2016 Allyson S. Bacon         /");
            Console.WriteLine("/                                                     /");
            Console.WriteLine("///////////////////////////////////////////////////////");

            Console.WriteLine("");
            Console.WriteLine(lang.making_the_query_extensions_in_project);
            Extensions.ExtensionApp.instanceTypes();

            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, args) != null)
            {
                return;
            }

            //CÓDIGO PARA RESETAR JOGADORES
            //PlayerStruct.InitializePlayerArrays();
            //Database.ResetAndGiveExp();

            Log(Languages.LStruct.lang.starting_server);
            Log(lang.setting_global_names);
            Globals.GAME_NAME = Database.Handler.getGameName();
            Globals.MOTD = Database.Handler.getMOTD();
            Globals.NOTICE = Database.Handler.getNotice();
            Log(lang.starting_server);
            Log(lang.client_defined);
            Log("");
            Log(lang.starting_player_arrays);
            PlayerStruct.initializePlayerArrays();
            Log(lang.starting_guild_arrays);
            GuildStruct.initializeGuildArrays();
            Log(lang.arrays_started);
            Log("");
            Log(lang.loading_enemies);
            Database.Enemies.loadEnemies();
            Log(lang.loaded);
            Log(lang.loading_maps_monsters_and_missions);
            Database.Maps.loadAll();
            Log(lang.loaded);
            Log(lang.loading_items);
            Database.Items.loadItems();
            Log(lang.loaded);
            Log(lang.loading_weapons);
            Database.Weapons.loadWeapons();
            Log(lang.loaded);
            Log(lang.loading_armors);
            Database.Armors.loadArmors();
            Log(lang.loaded);
            Log(lang.loading_skills);
            Database.Skills.loadSkills();
            Log(lang.loaded);
            Log(lang.loading_shops);
            Database.Shops.loadShops();
            //Rudimentar :/
            Database.Manual.loadShopsRud();
            Log(lang.loaded);
            Log(lang.loading_recipes);
            Database.Manual.loadRecipes();
            Log(lang.loaded);
            Log(lang.loading_guilds);
            Database.Guilds.loadGuilds();
            Log(lang.loaded);
            Log(lang.loading_classes);
            Database.Handler.defineClassesData();
            Log(lang.loaded);
            Log("");
            Log(lang.setting_variables);
            rdn = new Random();
            Log(lang.OK);
            Log("");
            Log(lang.starting_server_loop);
            Log(lang.starting_tcpip_connection);

            WinsockAsync.start();
        }
        //*********************************************************************************************
        // Log / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void Log(string data, ConsoleColor c = ConsoleColor.Gray)
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name, data, c) != null)
            {
                return;
            }

            //CÓDIGO
            Console.ForegroundColor = c;
            Console.WriteLine(data);
        }
    }
}
