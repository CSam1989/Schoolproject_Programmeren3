using BLL_Opdracht_PR.Helpers;
using Moq;
using NUnit.Framework;
using System;

namespace BLL.UnitTests.Helpers
{
    [TestFixture]
    public class ExtentionsTests
    {
        ///     Tests zijn niet volledig betrouwbaar!!
        ///     In de maand december als ik de eerste test doe, ga ik niet het gewenste pad testen
        ///     Heb research gedaan naar DateTime.now te mocken 
        ///     maar ik kan hier nergens Dependency Injection gebruiken (behalve method injection)
        ///     method injection ga ik niet doen omdat ik dan in productie code ook steeds DI moet ingeven
           
        ///     Opgelost door method te dupliceren, is niet ideale oplossing, maar zo creeer ik wel betrouwbare testen
        private readonly int expectedAge = 5;
        private readonly DateTime currentDate = new DateTime(2019, 6, 6);
        private Mock<IDateTimeHelper> _fakeDateTime;

        [SetUp]
        public void SetUp()
        {
            _fakeDateTime = new Mock<IDateTimeHelper>();

            _fakeDateTime
                .Setup(x => x.GetDateTimeNow())
                .Returns(currentDate);
        }

        [Test]
        [TestCase(0,1,4)]
        [TestCase(1,0,4)]
        [TestCase(1,1,4)]
        [TestCase(0,0,5)]
        [TestCase(0,-1,5)]
        [TestCase(-1,0,5)]
        [TestCase(-1,-1,5)]
        public void Age_WhenCalled_ShouldReturnExpectedResult(int days, int months, int expectedresult)
        {
            DateTime birthday = currentDate.AddDays(days).AddMonths(months).AddYears(-expectedAge);

            var result = birthday.Age(_fakeDateTime.Object);

            Assert.That(result, Is.EqualTo(expectedresult));
        }
    }
}
