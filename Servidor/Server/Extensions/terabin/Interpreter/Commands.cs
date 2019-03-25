using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace __Forjerum.Extensions
{
    //*********************************************************************************************
    // Exemplo de comandos no interpreter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
    // Commands.cs
    //*********************************************************************************************
    class Commands
    {
        //*********************************************************************************************
        // Main / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adicionar um novo comando no interpreter, junto com seu método de chamada
        //*********************************************************************************************
        public static object Main(params object[] args){
            Interpreter.addCommand(setAccess, "setaccess");
            return ExtensionApp.EXTEND;
        }
        //*********************************************************************************************
        // SetAccess / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Entrega acesso a um jogador e salva ele na Database.
        //*********************************************************************************************
        public static void setAccess(object[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Comando inválido, para usa-lo corretamente siga o exemplo:");
                Console.WriteLine("setaccess nome_do_jogador nível_de_acesso");
                return;
            }

            if (Globals.Player_Highs <= 0) { Console.WriteLine("\n");Console.WriteLine("Não há jogadores online, busca ineficiente"); return; }

            for (int i = 0; i <= Globals.Player_Highs; i++)
            {
                if (PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].CharacterName == args[1])
                {
                    PlayerStruct.character[i, PlayerStruct.player[i].SelectedChar].Access = Convert.ToInt32(args[2]);
                    Database.Characters.saveCharacter(i, PlayerStruct.player[i].Email, PlayerStruct.player[i].SelectedChar);
                    SendData.sendMsgToPlayer(i, "Você recebeu nível de acesso, reconecte-se para vê-lo em vigor.", Globals.ColorGreen, Globals.Msg_Type_Server);
                    return;
                }
            }

            Console.WriteLine("O jogador não está conectado.");
        }
    }
}
