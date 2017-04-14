using Confoosed.MatchLogic.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class ResultParserTest
    {
        [TestMethod]
        public void TryParse()
        {
            int score1;
            int score2;
            Assert.IsTrue(ResultParser.TryParse("@playerA @playerB 1-10", out score1, out score2));
            Assert.AreEqual(1, score1);
            Assert.AreEqual(10, score2);
        }
    }
}
