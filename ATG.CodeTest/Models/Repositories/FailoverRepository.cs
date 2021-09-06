using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATG.CodeTest.Models.Repositories
{
   public class FailoverRepository : IFailoverRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot();
        }
    }
}
