using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class MatchParserTest
    {
        [TestMethod]
        public void TryParseWithScore()
        {
            Assert.IsTrue(MatchParser.TryParse("@playerA @playerB 1-10", out Match match));
            Assert.AreEqual("@playerB", match.Winner.Id);
            Assert.AreEqual("@playerA", match.Looser.Id);
            Assert.AreEqual(10, match.GoalsWinner);
            Assert.AreEqual(1, match.GoalsLooser);
        }

        [TestMethod]
        public void TryParseWithWon()
        {
            Assert.IsTrue(MatchParser.TryParse("@playerA won over @playerB", out Match match));
            Assert.AreEqual("@playerA", match.Winner.Id);
            Assert.AreEqual("@playerB", match.Looser.Id);
        }

        [TestMethod]
        public void TryParseWithLost()
        {
            Assert.IsTrue(MatchParser.TryParse("@playerA lost for @playerB", out Match match));
            Assert.AreEqual("@playerB", match.Winner.Id);
            Assert.AreEqual("@playerA", match.Looser.Id);
        }
    }
}