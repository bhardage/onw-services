using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ONWServices.Models;
using ONWServices.Repositories;
using System;
using System.Collections.Generic;

namespace ONWServices.Tests.Integration.Repositories
{
    [TestClass]
    public class GameRepositoryTest : BaseMongoIntegrationTest
    {
        private GameRepository cut;
        
        private static string _collectionName;
        private static IMongoCollection<Game> _collection;

        [ClassInitialize]
        public static void BeforeAll(TestContext context)
        {
            _collectionName = "games";
            _collection = DbContext.Database.GetCollection<Game>(_collectionName);
        }

        [TestInitialize]
        public void BeforeEach()
        {
            //Wipe out the collection before each test
            DbContext.Database.DropCollection(_collectionName);

            cut = new GameRepository(CreateOnwDbContext(DbContext));
        }

        [TestMethod]
        public void TestFindByGameId()
        {
            Guid gameIdToFind = Guid.NewGuid();
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });
            _collection.InsertOne(new Game { GameId = gameIdToFind });
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });

            Game result = cut.FindByGameId(gameIdToFind);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreNotEqual(ObjectId.Empty, result.Id);
            Assert.AreEqual(gameIdToFind, result.GameId);
        }

        [TestMethod]
        public void TestFindByGameIdForNonexistentGameId()
        {
            Guid gameIdToFind = Guid.NewGuid();
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });

            Game result = cut.FindByGameId(gameIdToFind);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDeleteByGameId()
        {
            Guid gameIdToDelete = Guid.NewGuid();
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });
            _collection.InsertOne(new Game { GameId = gameIdToDelete });
            _collection.InsertOne(new Game { GameId = Guid.NewGuid() });

            cut.DeleteByGameId(gameIdToDelete);

            //Make sure the game was actually deleted
            List<Game> newResults = _collection.Find(_ => true).ToList();
            Assert.AreEqual(2, newResults.Count);
        }

        [ClassCleanup]
        public static void AfterAll()
        {
            _collection = null;
        }
    }
}
