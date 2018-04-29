using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
    public interface IDataSorter
    {

        void OrderAndTake(Dictionary<string, double> StudentsMark, string comparison,
            int studentsToTake);

    }
}
