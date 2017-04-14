using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class MatchParserTest
    {
        [TestMethod]
        public void TryParse()
        {
            FoosMatch match;
            Assert.IsTrue(MatchParser.TryParse("@playerA @playerB 1-10", out match));
            Assert.AreEqual("@playerB", match.Winner.Id);
            Assert.AreEqual("@playerA", match.Looser.Id);
            Assert.AreEqual(10, match.GoalsWinner);
            Assert.AreEqual(1, match.GoalsLooser);
        }
    }
}