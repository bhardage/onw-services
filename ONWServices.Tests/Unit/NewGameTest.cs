using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class NewGameTest
    {
        [TestMethod]
        public void WhenNewGame_GameShouldHaveStatusOfNew()
        {
            var game = new Game();

            Assert.AreEqual(GameStatus.New, game.Status);
        }

        [TestMethod]
        public void WhenNewGame_MustHaveDefaultRoles()
        {
            var game = new Game();

            Assert.IsTrue(game.SelectedRoles.Contains(Role.Warewolf));
            Assert.IsTrue(game.SelectedRoles.Contains(Role.Seer));
            Assert.IsTrue(game.SelectedRoles.Contains(Role.Robber));
            Assert.IsTrue(game.SelectedRoles.Contains(Role.Troublemaker));
        }
    }    
}
