using ATG.CodeTest.Models;
using ATG.CodeTest.Models.Repositories;
using ATG.CodeTest.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atg.CodeTest.Tests.UnitTests
{
    public class LotServiceUnitTests
    {
        [Test]
        public void GetLot_RequestAnArchivedLot_ShouldReturnAnArchivedLot()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            IIncidentService enabledFailoverService = testFactory.CreateMockFailoverService(false).Object;

            Lot archiveLot = new Lot()
            {
                Id = 1,
                Name = "Archive Lot",
                Description = "Lot from the archive repository",
                Price = 4.99m,
                IsArchived = true
            };

            Lot failoverLot = new Lot()
            {
                Id = 1,
                Name = "Failover Lot",
                Description = "Lot from the failover repository",
                Price = 4.99m,
                IsArchived = true
            };

            Lot mainLot = new Lot()
            {
                Id = 1,
                Name = "Main Lot",
                Description = "Lot from the main repository",
                Price = 4.99m,
                IsArchived = true
            };

            IEnumerable<IRepository> repositories = new List<IRepository>()
            {
                { testFactory.CreateMockArchiveLotRepository(archiveLot).Object },
                { testFactory.CreateMockFailoverLotRepository(failoverLot).Object },
                { testFactory.CreateMockMainLotRepository(mainLot).Object }
            };

            LotService lotService = new LotService(repositories, enabledFailoverService);

            // Act
            Lot returnedLot = lotService.GetLot(1);

            // Assert
            Assert.AreEqual(archiveLot, returnedLot);
        }

        [Test]
        public void GetLot_RequestAnArchivedLotFromTheArchive_ShouldReturnAnArchivedLot()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            IIncidentService enabledFailoverService = testFactory.CreateMockFailoverService(false).Object;

            Lot archiveLot = new Lot()
            {
                Id = 1,
                Name = "Archive Lot",
                Description = "Lot from the archive repository",
                Price = 4.99m,
                IsArchived = true
            };

            IEnumerable<IRepository> repositories = new List<IRepository>()
            {
                { testFactory.CreateMockArchiveLotRepository(archiveLot).Object }
            };

            LotService lotService = new LotService(repositories, enabledFailoverService);

            // Act
            Lot returnedLot = lotService.GetLot(1, true);

            // Assert
            Assert.AreEqual(archiveLot, returnedLot);
        }

        [Test]
        public void GetLot_RequestANonArchivedLot_ShouldReturnAMainLot()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            IIncidentService enabledFailoverService = testFactory.CreateMockFailoverService(false).Object;

            Lot archiveLot = new Lot()
            {
                Id = 1,
                Name = "Archive Lot",
                Description = "Lot from the archive repository",
                Price = 4.99m,
                IsArchived = true
            };

            Lot failoverLot = new Lot()
            {
                Id = 1,
                Name = "Failover Lot",
                Description = "Lot from the failover repository",
                Price = 4.99m,
                IsArchived = false
            };

            Lot mainLot = new Lot()
            {
                Id = 1,
                Name = "Main Lot",
                Description = "Lot from the main repository",
                Price = 4.99m,
                IsArchived = false
            };

            IEnumerable<IRepository> repositories = new List<IRepository>()
            {
                { testFactory.CreateMockArchiveLotRepository(archiveLot).Object },
                { testFactory.CreateMockFailoverLotRepository(failoverLot).Object },
                { testFactory.CreateMockMainLotRepository(mainLot).Object }
            };

            LotService lotService = new LotService(repositories, enabledFailoverService);

            // Act
            Lot returnedLot = lotService.GetLot(1);

            // Assert
            Assert.AreEqual(mainLot, returnedLot);
        }

        [Test]
        public void GetLot_RequestANonArchivedLotWithFailoverEnabled_ShouldReturnAFailoverLot()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            IIncidentService enabledFailoverService = testFactory.CreateMockFailoverService(true).Object;

            Lot archiveLot = new Lot()
            {
                Id = 1,
                Name = "Archive Lot",
                Description = "Lot from the archive repository",
                Price = 4.99m,
                IsArchived = true
            };

            Lot failoverLot = new Lot()
            {
                Id = 1,
                Name = "Failover Lot",
                Description = "Lot from the failover repository",
                Price = 4.99m,
                IsArchived = false
            };

            Lot mainLot = new Lot()
            {
                Id = 1,
                Name = "Main Lot",
                Description = "Lot from the main repository",
                Price = 4.99m,
                IsArchived = false
            };

            IEnumerable<IRepository> repositories = new List<IRepository>()
            {
                { testFactory.CreateMockArchiveLotRepository(archiveLot).Object },
                { testFactory.CreateMockFailoverLotRepository(failoverLot).Object },
                { testFactory.CreateMockMainLotRepository(mainLot).Object }
            };

            LotService lotService = new LotService(repositories, enabledFailoverService);

            // Act
            Lot returnedLot = lotService.GetLot(1);

            // Assert
            Assert.AreEqual(failoverLot, returnedLot);
        }

        [Test]
        public void GetLot_RequestAnArchivedLotFromTheArchiveWithFailoverEnabled_ShouldReturnAnArchivedLot()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            IIncidentService enabledFailoverService = testFactory.CreateMockFailoverService(true).Object;

            Lot archiveLot = new Lot()
            {
                Id = 1,
                Name = "Archive Lot",
                Description = "Lot from the archive repository",
                Price = 4.99m,
                IsArchived = true
            };

            Lot failoverLot = new Lot()
            {
                Id = 1,
                Name = "Failover Lot",
                Description = "Lot from the failover repository",
                Price = 4.99m,
                IsArchived = false
            };

            Lot mainLot = new Lot()
            {
                Id = 1,
                Name = "Main Lot",
                Description = "Lot from the main repository",
                Price = 4.99m,
                IsArchived = false
            };

            IEnumerable<IRepository> repositories = new List<IRepository>()
            {
                { testFactory.CreateMockArchiveLotRepository(archiveLot).Object },
                { testFactory.CreateMockFailoverLotRepository(failoverLot).Object },
                { testFactory.CreateMockMainLotRepository(mainLot).Object }
            };

            LotService lotService = new LotService(repositories, enabledFailoverService);

            // Act
            Lot returnedLot = lotService.GetLot(1, true);

            // Assert
            Assert.AreEqual(archiveLot, returnedLot);
        }
    }
}
