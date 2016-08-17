using System;
using System.Reflection;

namespace FORJERUM
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
            LoadLang();
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
            Extensions.ExtensionApp.InstanceTypes();

            //EXTEND
            if (Extensions.ExtensionApp.ExtendMyApp
                (MethodBase.GetCurrentMethod().Name, args) != null)
            {
                return;
            }

            //CÓDIGO PARA RESETAR JOGADORES
            //PStruct.InitializePlayerArrays();
            //Database.ResetAndGiveExp();

            Log(Languages.LStruct.lang.starting_server);
            Log(lang.setting_global_names);
            Globals.GAME_NAME = Database.GET_GAME_NAME();
            Globals.MOTD = Database.GET_MOTD();
            Globals.NOTICE = Database.GET_NOTICE();
            Log(lang.starting_server);
            Log(lang.client_defined);
            Log("");
            Log(lang.starting_player_arrays);
            PStruct.InitializePlayerArrays();
            Log(lang.starting_guild_arrays);
            GStruct.InitializeGuildArrays();
            Log(lang.arrays_started);
            Log("");
            Log(lang.loading_enemies);
            Database.LoadEnemies();
            Log(lang.loaded);
            Log(lang.loading_maps_monsters_and_missions);
            Database.LoadMaps();
            Log(lang.loaded);
            Log(lang.loading_items);
            Database.LoadItems();
            Log(lang.loaded);
            Log(lang.loading_weapons);
            Database.LoadWeapons();
            Log(lang.loaded);
            Log(lang.loading_armors);
            Database.LoadArmors();
            Log(lang.loaded);
            Log(lang.loading_skills);
            Database.LoadSkills();
            Log(lang.loaded);
            Log(lang.loading_shops);
            Database.LoadShops();
            //Rudimentar :/
            Database.LoadShopsRud();
            Log(lang.loaded);
            Log(lang.loading_recipes);
            Database.LoadRecipes();
            Log(lang.loaded);
            Log(lang.loading_guilds);
            Database.LoadGuilds();
            Log(lang.loaded);
            Log(lang.loading_classes);
            Database.DEFINE_CLASSES_DATA();
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
            if (Extensions.ExtensionApp.ExtendMyApp
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
