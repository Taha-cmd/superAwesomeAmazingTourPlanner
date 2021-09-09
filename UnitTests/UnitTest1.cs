using BusinessLogic;
using DataAccess;
using DataAccess.Maps;
using Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Commands;
using ViewModels.ViewModels;

namespace UnitTests
{

    public class Tests
    {
        private readonly Tour invalidTour = new() { Description = null, TargetArea = "2", Name = "3", StartingArea = "4" };
        private readonly Tour validTour = new() { Description = "1", TargetArea = "2", Name = "3", StartingArea = "4" };
        private readonly TourLog validLog = new() { Accomodation = "hotel", Author = "John", DateTime = DateTime.Now, HasCampingSpots = true, HasMcDonalds = true, Members = 4, Rating = 3, TotalTime = 3.4, Report = "great", TourName = "randomTour" };
        private readonly TourLog invalidLog = new() { Accomodation = null };


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTourCreationValidation()
        {
            var manager = Application.GetToursManager();
            Assert.ThrowsAsync<Exception>(() => manager.CreateTour(invalidTour));
        } 

        [Test]
        public void TestTourValidationFunction()
        {
            Assert.IsFalse(Application.GetToursManager().ValidateTour(invalidTour));
            Assert.IsTrue(Application.GetToursManager().ValidateTour(validTour));
        }
        
        [Test] 
        public void TestTourLogCreation()
        {
            Assert.ThrowsAsync<Exception>(() => Application.GetToursManager().CreateTourLog(invalidLog));
        }
        
        [Test]
        public void TestTourLogUpdate()
        {
            Assert.ThrowsAsync<Exception>(() => Application.GetToursManager().UpdateTourLog(invalidLog));
        }

        [Test]
        public void TestTourLogValidation()
        {
            var manager = Application.GetToursManager();

            Assert.IsTrue(manager.ValidateTourLog(validLog));
            Assert.IsFalse(manager.ValidateTourLog(invalidLog));
        }

        [Test]
        public void TestImportInvalidJson()
        {
            string path = "invalidJson.json";
            var manager = Application.GetToursManager();

            Assert.ThrowsAsync<Exception>(() => manager.Import(path));
        }

        [Test]
        public void TestImportInvalidTour()
        {
            string path = "validJsonButInvalidTour.json";
            var manager = Application.GetToursManager();

            Assert.ThrowsAsync<Exception>(() => manager.Import(path));
        }

        [Test]
        public void TestImportInvalidPath()
        {
            string path = "iDontExist.json";
            var manager = Application.GetToursManager();

            Assert.ThrowsAsync<Exception>(() => manager.Import(path));
        }

        [Test]
        public void TestCreateTourViewModelCommandType()
        {
            var vm = new TourFormViewModel();
            Assert.IsTrue(vm.OperationCommand.GetType().Name == nameof(CreateTourCommand));
        }

        [Test]
        public void TestUpdateTourViewModelCommandType()
        {
            var vm = new TourFormViewModel(new Tour());
            Assert.IsTrue(vm.OperationCommand.GetType().Name == nameof(UpdateTourCommand));
        }

        [Test]
        public void TestCreateTourLogViewModelCommandType()
        {
            var vm = new TourLogFormViewModel("fake tour");
            Assert.IsTrue(vm.OperationCommand.GetType().Name == nameof(CreateTourLogCommand));
        }

        [Test]
        public void TestUpdateTourLogViewModelCommandType()
        {
            var vm = new TourLogFormViewModel(validLog);
            Assert.IsTrue(vm.OperationCommand.GetType().Name == nameof(UpdateTourLogCommand));
        }

        [Test]
        public void TestCreateTourExists()
        {
            var repo = new Mock<IToursRepository>();
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(true);
            var fakeManager = new ToursManager(repo.Object, null, null);

            Assert.ThrowsAsync<Exception>(() => fakeManager.CreateTour(invalidTour));
        }

        [Test]
        public void TestCreateTourRouteDoesNotExist()
        {
            var repo = new Mock<IToursRepository>();
            var maps = new Mock<IMapsApiClient>();

            maps.Setup(mock => mock.GetRouteInformation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(new MapsApiResponse(false)));
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(false);

            var fakeManager = new ToursManager(repo.Object, maps.Object, null);

            Assert.ThrowsAsync<Exception>(() => fakeManager.CreateTour(invalidTour));
        }

        [Test]
        public void TestGetLogsTourDoesNotExist()
        {
            var repo = new Mock<IToursRepository>();
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(false);
            var fakeManager = new ToursManager(repo.Object, null, null);

            Assert.ThrowsAsync<Exception>(() => Task.FromResult(fakeManager.GetLogs("fake tour")));
        }

        [Test]
        public void TestUpdateTourNameExists()
        {
            var repo = new Mock<IToursRepository>();
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(true);
            var fakeManager = new ToursManager(repo.Object, null, null);

            //should throw, since we are changing the name to a one that is already taken
            Assert.ThrowsAsync<Exception>(() => fakeManager.UpdateTour("new name", validTour.Image, validTour));
        }

        [Test]
        public void TestUpdateTourRouteDoesNotExist()
        {
            var repo = new Mock<IToursRepository>();
            var maps = new Mock<IMapsApiClient>();

            maps.Setup(mock => mock.GetRouteInformation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(new MapsApiResponse(false)));
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(false);

            var fakeManager = new ToursManager(repo.Object, maps.Object, null);

            Assert.ThrowsAsync<Exception>(() => fakeManager.UpdateTour(validTour.Name, validTour.Image, validTour));
        }

        [Test]
        public void TestTourCopy()
        {
            Assert.ThrowsAsync<Exception>(() => Application.GetToursManager().Copy(invalidTour));
        }

        [Test]
        public void TestTourCopy2()
        {
            var repo = new Mock<IToursRepository>();
            repo.Setup(mock => mock.TourExists(It.IsAny<string>())).Returns(true);
            var fakeManager = new ToursManager(repo.Object, null, null);

            Assert.ThrowsAsync<Exception>(() => fakeManager.Copy(validTour));
        }

        [Test]
        public void TestTourCloneMethod()
        {
            var original = new Tour() { Description = "1", TargetArea = "2", Name = "3", StartingArea = "4" };
            var clone = original.Clone();

            clone.Name = "new Name";

            Assert.IsFalse(original.Name == clone.Name);
        }


    }
}
