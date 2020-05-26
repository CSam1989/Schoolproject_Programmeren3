using AutoMapper;
using BLL_Opdracht_PR.Helpers;
using BLL_Opdracht_PR.Logic;
using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UnitTests.Logic
{
    [TestFixture]
    public class TeBetalenLogicTests
    {
        private Mock<IDataService> _mock;
        private TeBetalenLogic _tebetalenLogic;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IDataService>();
            _tebetalenLogic = new TeBetalenLogic(_mock.Object);

        }

        #region GetTotaleNietVerekendeUitgaven
        [Test]
        public void GetTotaleNietVerekendeUitgaven_GeenUitgave_ShouldReturnZero()
        {
            _mock
                .Setup(x => x.GetUitgavenByIsVerekend(false))
                .Returns(new ObservableCollection<UitgavenForTeBetalenDto>());

            var result = _tebetalenLogic.GetTotaleNietVerekendeUitgaven();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetTotaleNietVerekendeUitgaven_EenOfMeerdereUitgaven_ShouldReturnSumOfUitgave()
        {
            _mock
                .Setup(x => x.GetUitgavenByIsVerekend(false))
                .Returns(new ObservableCollection<UitgavenForTeBetalenDto>
                {
                    new UitgavenForTeBetalenDto {Prijs = 1},
                    new UitgavenForTeBetalenDto {Prijs = 2}
                });

            var result = _tebetalenLogic.GetTotaleNietVerekendeUitgaven();

            Assert.That(result, Is.EqualTo(3));
        }
        #endregion

        #region SetAllUitgavenToVerekend
        [Test]
        public void SetAllUitgavenToVerekend_GeenUitgave_ShouldNotCallUitgaveWijzigen()
        {
            _mock
                .Setup(x => x.GetUitgavenByIsVerekend(false))
                .Returns(new ObservableCollection<UitgavenForTeBetalenDto>());

            _tebetalenLogic.SetAllUitgavenToVerekend();

            _mock.Verify(x => x.UitgaveWijzigen(It.IsAny<UitgavenForWijzigDto>()), Times.Never);
        }

        [Test]
        public void SetAllUitgavenToVerekend_EenOfMeerdereUitgaven_ShoulCallUitgaveWijzigen()
        {
            _mock
                .Setup(x => x.GetUitgavenByIsVerekend(false))
                .Returns(new ObservableCollection<UitgavenForTeBetalenDto>
                {
                    new UitgavenForTeBetalenDto{IsVerekend = false}
                }) ;

            _tebetalenLogic.SetAllUitgavenToVerekend();

            _mock.Verify(x => x.UitgaveWijzigen(It.IsAny<UitgavenForWijzigDto>()));
        }
        #endregion
    }
}
