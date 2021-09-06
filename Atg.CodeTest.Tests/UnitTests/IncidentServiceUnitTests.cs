using ATG.CodeTest.Services;
using NUnit.Framework;
using Microsoft.Extensions.Options;
using ATG.CodeTest.Models.Options;
using System;
using System.Linq;

namespace Atg.CodeTest.Tests
{
    public class IncidentServiceUnitTests
    {
        [Test]
        public void IsFailOverEnabled_CheckEnabledWhenErrorThresholdReached_ShouldFailover()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            FailoverOptions options = new FailoverOptions() { FailoverModeEnabled = true, FailoverWindowMinutes = 10, MaxFailedRequests = 50 };

            IIncidentService failoverService = new IncidentService(
                Options.Create<FailoverOptions>(options), 
                testFactory.CreateMockDateTimeService().Object, 
                testFactory.CreateMockIncidentRepository(51, 10).Object
                );

            // Act
            bool isFailoverEnabled = failoverService.IsFailoverEnabled();

            // Assert
            Assert.IsTrue(isFailoverEnabled);
        }

        [Test]
        public void IsFailOverEnabled_CheckNotEnabledWhenErrorThresholdNotReached_ShouldNotFailover()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            FailoverOptions options = new FailoverOptions() { FailoverModeEnabled = true, FailoverWindowMinutes = 10, MaxFailedRequests = 50 };

            IIncidentService failoverService = new IncidentService(
                Options.Create<FailoverOptions>(options),
                testFactory.CreateMockDateTimeService().Object,
                testFactory.CreateMockIncidentRepository(50, 10).Object
                );

            // Act
            bool isFailoverEnabled = failoverService.IsFailoverEnabled();

            // Assert
            Assert.IsFalse(isFailoverEnabled);
        }

        [Test]
        public void IsFailOverEnabled_CheckNotEnabledWhenNotEnabled_ShouldNotFailover()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            FailoverOptions options = new FailoverOptions() { FailoverModeEnabled = false, FailoverWindowMinutes = 10, MaxFailedRequests = 50 };

            IIncidentService failoverService = new IncidentService(
                Options.Create<FailoverOptions>(options),
                testFactory.CreateMockDateTimeService().Object,
                testFactory.CreateMockIncidentRepository(51, 10).Object
                );

            // Act
            bool isFailoverEnabled = failoverService.IsFailoverEnabled();

            // Assert
            Assert.IsFalse(isFailoverEnabled);
        }

        [Test]
        public void IsFailOverEnabled_CheckNotEnabledWhenIncidentsOutsideOfWindow_ShouldNotFailover()
        {
            // Arrange
            TestFactory testFactory = new TestFactory();
            FailoverOptions options = new FailoverOptions() { FailoverModeEnabled = false, FailoverWindowMinutes = 10, MaxFailedRequests = 50 };

            var futureDateTime = testFactory.NowDateTime.AddYears(1);
            var dateTimeService = testFactory.CreateMockDateTimeService(futureDateTime).Object;
            var incidentRepository = testFactory.CreateMockIncidentRepository(51, 10).Object;

            IIncidentService failoverService = new IncidentService(
                Options.Create<FailoverOptions>(options),
                dateTimeService,
                incidentRepository
                );

            // Act
            bool isFailoverEnabled = failoverService.IsFailoverEnabled();

            // Assert
            Assert.IsFalse(isFailoverEnabled); // Not enabled
            Assert.AreEqual(51, incidentRepository.FailoverEvents.Count()); // 51 events have been generated as expected
            Assert.AreEqual(51, incidentRepository.FailoverEvents.Where(x => x.DateTime < dateTimeService.Now.AddMinutes(-10)).Count()); // All events occured more than 10 minutes before the "current" date so are outside the window.
        }
    }
}