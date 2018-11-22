using Microsoft.VisualStudio.TestTools.UnitTesting;
using ONWServices.Initializers;
using ONWServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ONWServices.Tests.Unit
{
    //Game statuses: New, Available, Night Phase, Information Review, Discussion, Round Summary, (Back to Night Phase), Game Summary, Closed
    //Game must have a minimum of 3 players    
    //Game can only have 1 Game Master

    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void WhenNewGame_GameShouldHaveStatusOfNew()
        {
            var game = new Game();
            var initializer = new GameInitializer();
            game = initializer.Setup(game);

            Assert.AreEqual(GameStatus.New, game.Status);
        }
    }    
}
