using ATG.CodeTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATG.CodeTest.App
{
    public class App
    {
        private readonly ILotService _lotService;

        public App(ILotService lotService)
        {
            _lotService = lotService;
        }

        public async Task RunAsync()
        {
            _lotService.GetLot(1);
        }
    }
}
