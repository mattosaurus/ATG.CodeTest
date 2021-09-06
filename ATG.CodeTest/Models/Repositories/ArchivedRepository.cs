using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Models.Repositories
{
    public class ArchivedRepository : IArchivedRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot();
        }
    }
}
