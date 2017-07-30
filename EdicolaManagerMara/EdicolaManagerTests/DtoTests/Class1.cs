using EdicolaManager;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace EdicolaManagerTests.DtoTests
{
    [TestFixture]
    public class HistoryCRUD
    {
        Mock<DBLinqDataContext> mockRepository = new Mock<DBLinqDataContext>();
        [Test]
        public void GetHistoryBetweenDates()
        {
            //mockRepository.Setup(it => it.ViewHistories.Context).Returns(new List<ViewHistories>());
        }
    }
}
