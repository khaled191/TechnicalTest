using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TheGoodTheBadAndTheUgly;

namespace TheGoodTheBadAndTheUglyTests
{
    [TestFixture]
    public class RegexLinkExtractorTests
    {
        [Test]
        public void GetLinks_WithNoFilterOnClass_ReturnsLinks()
        {
            var regexLinkExtractor = new RegexLinkExtractor();

            var html = "<html>" +
                         "<body>" +
                            "<a href=\"www.microsoft.com\">A link to microsoft</a>" +
                            "<a href=\"www.google.com\">A link to google</a>" +
                         "</body>" +
                       "</html>";

            var doc = new Document(html);

            var result = regexLinkExtractor.GetLinks(doc);

            Assert.IsTrue(result.Any(r => r.Item1 == "www.microsoft.com" && r.Item2 == "A link to microsoft"));
            Assert.IsTrue(result.Any(r => r.Item1 == "www.google.com" && r.Item2 == "A link to google"));
        }

        [Test]
        public void GetLinks_WithAFilterOnClass_ReturnsLinks()
        {
            var regexLinkExtractor = new RegexLinkExtractor();

            var html = "<html>" +
                         "<body>" +
                            "<a class=\"include_me\" href=\"www.microsoft.com\">A link to microsoft</a>" +
                            "<a href=\"www.google.com\" class=\"include_me\">A link to google</a>" +
                            "<a href=\"www.apple.com\" class=\"dont_include_me\">A link to apple</a>" +
                         "</body>" +
                       "</html>";

            var doc = new Document(html);

            var result = regexLinkExtractor.GetLinks(doc, "include_me");

            Assert.IsTrue(result.Any(r => r.Item1 == "www.microsoft.com" && r.Item2 == "A link to microsoft"));
            Assert.IsTrue(result.Any(r => r.Item1 == "www.google.com" && r.Item2 == "A link to google"));

            Assert.IsFalse(result.Any(r => r.Item1 == "www.apple.com"));
        }
    }
}
