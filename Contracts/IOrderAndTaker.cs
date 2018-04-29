using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
    public interface IOrderAndTake
    {

        void OrderAndTake(string courseName, string comparison, int? studentsToTake = null);

    }
}
