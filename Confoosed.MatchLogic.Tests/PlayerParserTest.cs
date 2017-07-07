using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class PlayerParserTest
    {
        [TestMethod]
        public void TryParseTwoPlayers()
        {
            Assert.IsTrue(PlayerParser.TryParse(" @playerA  @playerB  1-10 ", out Player player1, out Player player2));
            Assert.AreEqual("@playerA", player1.Id);
            Assert.AreEqual("@playerB", player2.Id);
        }

        [TestMethod]
        public void TryParsOnePlayers()
        {
            Assert.IsTrue(PlayerParser.TryParse(" @playerA ", out Player player));
            Assert.AreEqual("@playerA", player.Id);

        }
    }
}