using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    [Alias("dropDb")]
    public class DropDatabaseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public DropDatabaseCommand(string input, string[] data) : base(input, data)
        {

        }


        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }
            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }

}

