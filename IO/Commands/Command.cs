using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using SimpleJudge;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {

        private string input;
        private string[] data;



        protected string Input
        {
            get
            {
                return this.input;
            }
            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.input = value;
            }

        }

        protected string[] Data
        {
            get
            {
                return this.data;
            }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                this.data = value;
            }
        }


        protected Command (string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }


        public abstract void Execute();

    }
}
