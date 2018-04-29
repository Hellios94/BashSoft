using BashSoft.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft
{
    public  class InputReader : IReader
    {

        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public  void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentsPath}> ");
            string input = Console.ReadLine();
            input = input.Trim();
            interpreter.InterpredCommand(input);

            while (input != endcommand)
            {
                OutputWriter.WriteMessage($"{SessionData.currentsPath}> ");
                input = Console.ReadLine();
                input = input.Trim();
                interpreter.InterpredCommand(input);
            }

        }
        private const string endcommand = "quit";
    }

}

