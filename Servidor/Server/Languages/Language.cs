using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace __Forjerum.Languages
{
    class LStruct
    {
        public static void loadLang()
        {
            StreamReader s = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\language.txt");

            string language = s.ReadToEnd();
            s.Close();

            try
            {
                Type t = Type.GetType("FORJERUM.Languages." + language);
                MethodInfo method
                     = t.GetMethod("load", BindingFlags.Static | BindingFlags.Public);

                method.Invoke(null, null);
            }
            catch
            {
                Console.WriteLine("Pacote de língua inválido.");
                pt_br.load();
            }
        }

        public static LStruct.Lang lang = new LStruct.Lang();
        public struct Lang
        {
            public string starting_server;
            public string client_defined;
            public string starting_array_players;
            public string loading_classes;
            public string setting_variables;
            public string starting_server_loop;
            public string starting_tcpip_connection;
            public string loading_recipes;
            public string loading_shops;
            public string loading_skills;
            public string loading_armors;
            public string loading_weapons;
            public string loading_maps_monsters_and_missions;
            public string loading_enemies;
            public string loading_guilds;
            public string loading_items;
            public string setting_global_names;
            public string arrays_started;
            public string making_the_query_extensions_in_project;
            public string OK;
            public string success_atributte_reset;
            public string starting_player_arrays;
            public string starting_guild_arrays;
            public string loaded;
            public string you_dont_have_inventory_slot;
            public string you_cant_learn_more_skills;
            public string pvp_level_restriction;
            public string pvp_safe_zone;
            public string the_colector_of;
            public string is_being_attacked;
            public string the_area;
            public string is_free_now;
            public string has_been_defeated;
            public string colector_defeated_success;
            public string resource_empty;
            public string dont_have_prof_to_explore;
            public string dont_have_tool_to_interact;
            public string player_kick_offline;
            public string you_is_not_the_party_leader;
            public string automatic_log_server;
            public string created_in_date;
            public string exists;
            public string players_online;
            public string last_receive_refresh_happened;
            public string last_connection_refresh_happened;
            public string last_loop_command_is;
           // public string last_received_packet;
            public string last_received_packet_is;
            public string new_log_status_generated;
            public string server_started_in_date;
            public string has_survived;
            public string server_has_been_saved;
            public string sent_to;
            public string confirmation_code;
            public string your_confirmation_code;
            public string your_confirmation_code_is;
            public string problems_in_email_sent;
            public string error;
            public string account_recover;
            public string email;
            public string login;
            public string password;
            public string could_not_perform_magic;
            public string target_distance_too_long;
            public string you_can_not_invite_yourself;
            public string player_is_not_connected;
            public string player_already_is_your_friend;
            public string player_who_invited_is_not_connected;
            public string you_have_a_new_friend;
            public string accepted_your_friend_request;
            public string refused_your_friend_request;
            public string account_received_wpoints_success;
            public string player_can_not_receive_invite_at_this_moment;
            public string player_is_busy;
            public string wpoints_deliver_fail;
            public string player_has_been_kicked;
            public string player_has_been_kicked_success;
            public string player_not_found;
            public string account_received_premmy_success;
            public string account_received_premmy_fail;
            public string private_msg;
            public string map_msg;
            public string admin_msg;
            public string guild_msg;
            public string party_msg;
            public string global_msg;
            public string you_have_learned_a_new_spell;
            public string players_has_been_saved_with_success;
            public string the_player;
            public string has_left_the_guild;
            public string use_item_fail;
            public string you_dont_have_inventory_space;
            public string finded_a_item;
            public string you_have_obtained_new_items;
            public string you_is_not_the_collector_master;
            public string this_map_dont_have_collector_to_collect;
            public string has_been_collected;
            public string the_collector_of;
            public string you_dont_have_gold_to_create_collector;
            public string a_collector_already_exist_in_this_map;
            public string only_for_guild_members;
            public string this_map_cannot_have_collectors;
            public string a_collector_has_been_created;
            public string a_collector_has_been_created_in;
            public string guild_collector;
            public string you_dont_have_this_item_amount;
            public string you_dont_have_mana_to_spell;
            public string this_spell_cannot_be_evolved;
            public string you_cannot_invite_yourself;
            public string player_is_in_another_party;
            public string map_item_is_busy;
            public string joined_in_party;
            public string party_created;
            public string player_refused_party_invite;
            public string player_is_already_trading;
            public string player_already_have_guild;
            public string you_dont_have_this_amount;
            public string item_null;
            public string dont_have_more_slots;
            public string not_enough_gold_to_guild;
            public string guild_name_already_exist;
            public string dont_have_guild_slots;
            public string has_joined_in_the_guild;
            public string you_need_to_exit_your_guild_to_create_a_new;
            public string player_cannot_enter_in_guild_at_this_moment;
            public string player_refused_guild_invite;
            public string dont_have_guild_to_kick_a_member;
            public string only_guild_leader_can_kick;
            public string guild_desbanded;
            public string has_been_kicked;
            public string player_cannot_trade_now;
            public string player_refused_trade_invite;
            public string you_dont_have_the_amount;
            public string quest_already_have_reached_max_profs;
            public string quest_reward_inventory_full;
            public string learned_miner_prof;
            public string learned_blacksmith_prof;
            public string shop_inventory_full;
            public string completed_a_mission;
            public string you_bought_a_item;
            public string you_dont_have_gold_to_this;
            public string shop_is_gone;
            public string dont_have_inventory_space_to_buy;
            public string you_sold_a_item;
            public string you_dont_have_needed_amount_of_gold;
            public string account_banned_success;
            public string account_banned_fail;
            public string you_dont_have_prof_to_interact;
            public string you_dont_have_tool_to_interact;
            public string you_started_your_shop;
            public string you_stopped_your_shop;
            public string dont_have_inventory_space;
            public string incorrect_recipe;
            public string you_created_a_new_item;
            public string you_evolved_a_item;
            public string item_nonexistent;
            public string deliver_item_fail;
            public string deliver_item_fail_maybe_space;
            public string player_buffer_limit_exceeded;
            public string connection_critical_error;
            public string player_critical_error;
            public string forcibly_closed_connection;
            public string player_cleared;
            public string player_disconnected;
            public string player_authenticated;
            public string spell_broken;
            public string frozen;
            public string null_direction;
            public string admin_status_attr_to;
            public string admin_status_attr_to_account;
            public string error_log;
            public string attack_missed;
            public string origin;
            public string action_completed;
            public string extra;
            public string info_log;
            public string resisted;
            public string load_char_error;
            public string failed;
            public string load_show_char_error;
            public string damage_reduced;
            public string character_deleted;
            public string quest_defeat;
            public string pexp;
            public string exp;
            public string pet_evolve;
            public string level_up;
            public string no_level_to_use_this_item;
        }
    }
}
