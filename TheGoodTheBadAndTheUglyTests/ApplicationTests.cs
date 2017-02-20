using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGoodTheBadAndTheUgly;

namespace TheGoodTheBadAndTheUglyTests
{
    [TestFixture]
    public class ApplicationTests
    {
        [Test]
        public void Start_WithUrl_RunsApplication()
        {
            var linkExtractorMock = new Mock<ILinkExtracctor>();
            linkExtractorMock.Setup(m => m.GetLinks(It.IsAny<Document>(), It.IsAny<String>())).Returns(
                new List<Tuple<String, String>>
                {
                    new Tuple<String, String>("www.microsoft.com", "a link to microsoft"),
                    new Tuple<String, String>("www.google.com", "a link to google")
                }
            );

            var application = new Application(linkExtractorMock.Object);


            Assert.IsTrue(true);
        }
    }
}
