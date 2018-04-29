using BashSoft.Contracts;
using SimpleJudge;
using System;

namespace BashSoft
{
    class Launcher
    {
        public static void Main()
        {

            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentRepository(new  RepositoryFilter(), new RepositorySorter());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
