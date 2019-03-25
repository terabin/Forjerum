using System;
using System.Threading;

namespace __Forjerum.Extensions
{
    //*********************************************************************************************
    // O console do servidor passa a interpretar comandos
    // Interpreter.cs
    //*********************************************************************************************
    class Interpreter
    {
        //*********************************************************************************************
        // interpreter_mind / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Lista de comandos com suas ações
        //*********************************************************************************************
        public struct interpreter_mind
        {
            public string command;
            public Action<object[]> action;
        }

        public static interpreter_mind[] interpreter_lib = new interpreter_mind[1000];

        //*********************************************************************************************
        // AddCommand / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Adiciona um novo comando a ser interpretado no console junto com uma ação
        // exemplo: AddCommand(Método, "método")
        //*********************************************************************************************
        public static bool addCommand(Action<object[]> action, string command)
        {
            for (int i = 1; i < 1000; i++)
            {
                if (String.IsNullOrEmpty(interpreter_lib[i].command))
                {
                    interpreter_lib[i].command = command;
                    interpreter_lib[i].action = action;
                    return true;
                }
            }
            return false;
        }
        //*********************************************************************************************
        // start / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Inicia a Thread que vai processar os comandos do console
        //*********************************************************************************************
        public static void start(params object[] args)
        {
            // Cria a Thread principal.
            Thread iThread = new Thread(new ThreadStart(interpreterLoop));

            // Iniciar a Thread principal
            iThread.Start();
        }
        //*********************************************************************************************
        // InterpreterLoop / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Ler/processar o que é escrito ignorando a thread principal.
        //*********************************************************************************************
        public static void interpreterLoop()
        {
            // Objeto de informações do console
            ConsoleKeyInfo cki;

            // Previnir final inesperado
            Console.TreatControlCAsInput = true;

            string command = "";

            // Iniciar o loop
            while(true)
            {
                cki = Console.ReadKey();
                if ((Char.IsLetter(cki.KeyChar)) || (Char.IsNumber(cki.KeyChar)) || (Char.IsSeparator(cki.KeyChar)))
                {
                    command += cki.KeyChar;
                }

                // X,Y do cursor
                int x = Console.CursorLeft;
                int y = Console.CursorTop;

                if (cki.Key == ConsoleKey.Enter)
                {
                    // Quebra de linha
                    Console.Write('\n');

                    // Mover o cursor para o final
                    Console.SetCursorPosition(x, y + 1);

                    doInterpreter(command);
                    command = "";
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    //Apagar letra no console
                    Console.Write(' ');
                    Console.Write(cki.KeyChar);
                    if ((x <= command.Length) && (x > 1))
                    {
                        command = command.Remove(command.Length - (command.Length - x)); ;
                    }
                }
                if (cki.Key == ConsoleKey.LeftArrow)
                {
                    if (x > 1)
                    {
                        Console.SetCursorPosition(x - 2, y);
                    }
                }
                if (cki.Key == ConsoleKey.RightArrow)
                {
                    Console.SetCursorPosition(x + 1, y);
                }
            }
        }
        //*********************************************************************************************
        // DoInterpreter / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        // Verifica se existe o comando enviado, se existir chama diretamente a ação
        //*********************************************************************************************
        public static void doInterpreter(string command)
        {
            string[] data = command.Split(' ');

            for (int i = 1; i < 1000; i++)
            {
                if (data[0].Trim() == interpreter_lib[i].command)
                {
                    interpreter_lib[i].action(data);
                }
            }
        }
    }
}
