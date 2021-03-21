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
            var createTourViewModel = new CreateTourViewModel();
            Assert.Throws<Exception>(() => createTourViewModel.Manager.CreateTour(new Tour()));

        }
    }
}