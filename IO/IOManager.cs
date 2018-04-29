using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public class IOManager : IDirectoryManager
    {

        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initializeIdentation = SessionData.currentsPath.Split('\\').Length;
            Queue<string> subFolder = new Queue<string>();
            subFolder.Enqueue(SessionData.currentsPath);


            while (subFolder.Count != 0)
            {
                string currentPath = subFolder.Dequeue();
                int identation = currentPath.Split('\\').Length - initializeIdentation;


                if (depth - identation < 0)
                {
                    break;
                }
               
                
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));
                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        int indexOfLastSlash = file.LastIndexOf("\\");
                        string filename = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + filename);
                    }

                    foreach (string directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolder.Enqueue(directoryPath);

                    }
                }

                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessException);
                }
            }
        }


        public void CreatDirectoryInCurrentFolder(string name)
        {

            string path = GetCurrentDirectoryPath() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        private  string GetCurrentDirectoryPath()
        {
            return SessionData.currentsPath;
        }

        public  void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentsPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentsPath = newPath;
                }
                catch(ArgumentOutOfRangeException)
                {
                    throw new InvalidPathException();
                }
            }

            else
            {
                string currentPath = SessionData.currentsPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public  void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }

            SessionData.currentsPath = absolutePath;
        }
    }
}
