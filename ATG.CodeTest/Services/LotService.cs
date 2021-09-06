using ATG.CodeTest.Models;
using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Models.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATG.CodeTest.Services
{
    public class LotService : ILotService
    {
        private readonly IIncidentService _failoverLotService;
        private IEnumerable<IRepository> _repositories;

        public LotService(IEnumerable<IRepository> repositories, IIncidentService failoverLotService)
        {
            _repositories = repositories;
            _failoverLotService = failoverLotService;
        }

        public Lot GetLot(int id)
        {
            Lot lot = null;

            if (_failoverLotService.IsFailoverEnabled())
                lot = _repositories.Where(x => x is IFailoverRepository).FirstOrDefault().GetLot(id);
            else
                lot = _repositories.Where(x => x is IMainRepository).FirstOrDefault().GetLot(id);

            if (lot.IsArchived)
                lot = GetLot(id, true);

            return lot;
        }

        public Lot GetLot(int id, bool isLotArchived)
        {
            Lot lot = null;

            if (isLotArchived)
                lot = _repositories.Where(x => x is IArchivedRepository).FirstOrDefault().GetLot(id);
            else
                lot = GetLot(id);

            return lot;
        }
    }
}
