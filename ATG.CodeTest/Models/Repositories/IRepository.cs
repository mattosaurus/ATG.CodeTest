using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Models.Repositories
{
    public interface IRepository
    {
        Lot GetLot(int id);
    }
}
