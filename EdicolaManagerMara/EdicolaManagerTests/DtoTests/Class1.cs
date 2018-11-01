using EdicolaManager;
using Moq;
using NUnit.Framework;

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
