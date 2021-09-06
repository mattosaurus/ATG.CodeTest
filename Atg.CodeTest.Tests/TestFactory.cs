using ATG.CodeTest.Models;
using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Models.Repositories;
using ATG.CodeTest.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Atg.CodeTest.Tests
{
    public class TestFactory
    {
        public TestFactory()
        {

        }

        public TestFactory(DateTime nowDateTime)
        {
            NowDateTime = nowDateTime;
        }

        public DateTime NowDateTime { get; set; } = DateTime.Now;

        public Mock<IIncidentRepository> CreateMockIncidentRepository(int numberOfEvents, int maxWindowMinutes)
        {
            IEnumerable<Incident> incidents = GetRandomIntegers(numberOfEvents, 0, 60 * maxWindowMinutes).Select(x => new Incident(NowDateTime.AddSeconds(x * -1)));

            var mockIncidentRepository = new Mock<IIncidentRepository>();
            mockIncidentRepository.Setup(service => service.FailoverEvents).Returns(incidents.AsQueryable());

            return mockIncidentRepository;
        }

        public Mock<IIncidentService> CreateMockFailoverService(bool isFailoverEnabled)
        {
            var mockFailoverService = new Mock<IIncidentService>();
            mockFailoverService.Setup(service => service.IsFailoverEnabled()).Returns(isFailoverEnabled);

            return mockFailoverService;
        }

        public Mock<IDateTimeService> CreateMockDateTimeService()
        {
            var mockDateTimeService = new Mock<IDateTimeService>();
            mockDateTimeService.Setup(service => service.Now).Returns(NowDateTime);

            return mockDateTimeService;
        }

        public Mock<IDateTimeService> CreateMockDateTimeService(DateTime nowDateTime)
        {
            var mockDateTimeService = new Mock<IDateTimeService>();
            mockDateTimeService.Setup(service => service.Now).Returns(nowDateTime);

            return mockDateTimeService;
        }

        public Mock<IArchivedRepository> CreateMockArchiveLotRepository(Lot lot)
        {
            var mockRepository = new Mock<IRepository>();
            
            mockRepository.Setup(repository => repository.GetLot(It.IsAny<int>())).Returns(lot);
            return mockRepository.As<IArchivedRepository>();
        }

        public Mock<IFailoverRepository> CreateMockFailoverLotRepository(Lot lot)
        {
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repository => repository.GetLot(It.IsAny<int>())).Returns(lot);
            return mockRepository.As<IFailoverRepository>(); ;
        }

        public Mock<IMainRepository> CreateMockMainLotRepository(Lot lot)
        {
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repository => repository.GetLot(It.IsAny<int>())).Returns(lot);
            return mockRepository.As<IMainRepository>();
        }

        public static int GetRandomInteger(int minValue = 0, int maxValue = int.MaxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException("Maximum value must be greater than minimum value");
            }
            else if (maxValue == minValue)
            {
                return 0;
            }

            Int64 diff = maxValue - minValue;

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                while (true)
                {
                    byte[] fourBytes = new byte[4];
                    crypto.GetBytes(fourBytes);

                    // Convert that into an uint.
                    UInt32 scale = BitConverter.ToUInt32(fourBytes, 0);

                    Int64 max = (1 + (Int64)UInt32.MaxValue);
                    Int64 remainder = max % diff;
                    if (scale < max - remainder)
                    {
                        return (Int32)(minValue + (scale % diff));
                    }
                }
            }
        }

        private List<int> GetRandomIntegers(int numbersToGenerate, int minValue = 0, int maxValue = int.MaxValue)
        {
            List<int> randomNumbers = new List<int>();

            for (int i = 0; i < numbersToGenerate; i++)
            {
                randomNumbers.Add(GetRandomInteger(minValue, maxValue));
            }

            return randomNumbers;
        }
    }
}
