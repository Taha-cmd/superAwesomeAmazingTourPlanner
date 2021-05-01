using BusinessLogic;
using Models;
using NUnit.Framework;
using System;
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
            var createTourViewModel = new CreateOrUpdateTourViewModel();
            Assert.Throws<Exception>(async () => await createTourViewModel.Manager.CreateTour(new Tour()));
        }

        [Test]
        public void TestTourValidationFunction()
        {
            var tour1 = new Tour() { Description = null, TargetArea = "2", Name = "3", StartingArea = "4" };
            var tour2 = new Tour() { Description = "1", TargetArea = "2", Name = "3", StartingArea = "4" };


            Assert.IsFalse(Application.GetToursManager().ValidateTour(tour1));
            Assert.IsTrue(Application.GetToursManager().ValidateTour(tour2));
        }
    }
}