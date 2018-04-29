using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
    public interface IDatabase : IRequester, IFilterdTaker, IOrderAndTake
    {

        void LoadData(string fileName);

        void UnloadData();

    }
}
