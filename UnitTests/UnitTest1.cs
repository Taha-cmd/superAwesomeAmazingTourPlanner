using BusinessLogic;
using Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.ViewModels;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTourCreationValidation()
        {
            var manager = Application.GetToursManager();
            Assert.ThrowsAsync<Exception>(() => manager.CreateTour(new Tour()));
        } 

        [Test]
        public void TestTourValidationFunction()
        {
            var tour1 = new Tour() { Description = null, TargetArea = "2", Name = "3", StartingArea = "4" };
            var tour2 = new Tour() { Description = "1", TargetArea = "2", Name = "3", StartingArea = "4" };


            Assert.IsFalse(Application.GetToursManager().ValidateTour(tour1));
            Assert.IsTrue(Application.GetToursManager().ValidateTour(tour2));
        }
        
        [Test] 
        public void TestTourLogCreation()
        {
            var log = new TourLog();
            var manager = Application.GetToursManager();
            Assert.ThrowsAsync<Exception>(() => manager.CreateTourLog("sometour", log));
        } 

        [Test]
        public void TestTourLogValidation()
        {
            var badLog = new TourLog();
            var goodLog = new TourLog() { Rating = 3, TotalTime = 3, Report = "good", DateTime = DateTime.Now };

            var manager = Application.GetToursManager();

            Assert.IsTrue(manager.ValidateTourLog(goodLog));
            Assert.IsFalse(manager.ValidateTourLog(badLog));
        }

    }
}
