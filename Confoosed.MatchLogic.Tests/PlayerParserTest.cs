using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class PlayerParserTest
    {
        [TestMethod]
        public void TryParse()
        {
            Player player1;
            Player player2;
            Assert.IsTrue(PlayerParser.TryParse("@playerA @playerB 1-10", out player1, out player2));
            Assert.AreEqual("@playerA", player1.Id);
            Assert.AreEqual("@playerB", player2.Id);
        }
    }
}