using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Models.Repositories
{
   public class MainRepository : IMainRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot();
        }
    }
}
