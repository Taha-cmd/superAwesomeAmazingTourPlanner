using NUnit.Framework;
using ViewModels;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var p = new Person();
            Assert.IsTrue(p.Name == "fucker");
        }
    }
}