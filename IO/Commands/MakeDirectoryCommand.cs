using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command
    {

        [Inject]
        private IDirectoryManager inputOutputManager;

        public MakeDirectoryCommand(string input, string[] data) : base(input, data)
        {

        }


        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            string folderName = this.Data[1];
            this.inputOutputManager.CreatDirectoryInCurrentFolder(folderName);
        }
    }
}
