using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BashSoft.IO.Commands
{
    [Alias("open")]
    public class OpenFileCommand : Command
    {

        public OpenFileCommand(string input, string[] data) :base (input, data)
        {

        }


        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            string fileName = this.Data[1];
            Process.Start(SessionData.currentsPath + "\\" + fileName);
        }
    }
}
