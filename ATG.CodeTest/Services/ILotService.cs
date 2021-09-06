using ATG.CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Services
{
    public interface ILotService
    {
        Lot GetLot(int id);

        Lot GetLot(int id, bool isLotArchived);
    }
}
