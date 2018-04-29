using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
    public interface IFilterdTaker
    {

        void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null);

    }
}
