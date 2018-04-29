using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    [Alias("help")]
    public class GetHelpCommand : Command
    {

        public GetHelpCommand(string input, string[] data) : base(input, data)
        {

        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }
            this.DisplayHelp();
        }

        private void DisplayHelp()
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "make directory - mkdir: path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "traverse directory - ls: depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "comparing files - cmp: path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "change directory - changeDirREl:relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "change directory - changeDir:absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "read students data base - readDb: path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -125}|", "display data entities - display students/courses ascending/descending"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 127)}");
            OutputWriter.WriteEmptyLine();
        }
    }
}
